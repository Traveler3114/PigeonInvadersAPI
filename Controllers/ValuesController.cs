using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic; // To store a list of players
using PigeonInvadersAPI;

namespace PigeonInvadersAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MySqlConnectionManager _connectionManager;

        public ValuesController()
        {
            _connectionManager = new MySqlConnectionManager();
        }

        [HttpPost("AddPlayer")]
        public IActionResult AddScore([FromBody] Player player)
        {
            try
            {
                // Ensure the Timer is in the correct format
                TimeSpan timerSpan;
                if (!TimeSpan.TryParse(player.Timer, out timerSpan))
                {
                    return BadRequest("Invalid Timer format. Please use 'HH:mm:ss'.");
                }

                using (var connection = _connectionManager.OpenConnection())
                {
                    var query = "INSERT INTO Players (username, score, Timer) VALUES (@username, @score, @timer)";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", player.username);
                        command.Parameters.AddWithValue("@score", player.Score);
                        command.Parameters.AddWithValue("@timer", timerSpan.ToString(@"hh\:mm\:ss"));  // Format the Timer correctly

                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Score added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpGet("GetPlayers")]
        public IActionResult GetPlayers()
        {
            try
            {
                var Players = new List<Player>();  // List to store all Players

                using (var connection = _connectionManager.OpenConnection())
                {
                    var query = "SELECT username, score, Timer FROM Players";  // SQL query to retrieve all Players
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())  // Read each row in the result set
                            {
                                var player = new Player
                                {
                                    username = reader.GetString("username"),
                                    Score = reader.GetInt32("score"),
                                    Timer = reader.GetTimeSpan("Timer").ToString(@"hh\:mm\:ss")  // Read Timer as TimeSpan and format it
                                };
                                Players.Add(player);  // Add the player object to the list
                            }
                        }
                    }
                }

                return Ok(Players);  // Return the list of Players as a JSON response
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


    }

    public class Player
    {
        public string username { get; set; }
        public int Score { get; set; }
        public string Timer { get; set; }
    }
}
