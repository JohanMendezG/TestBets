using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BetPlay.Data.Roulettes
{
    public class MockRoulettes : IRoulettes
    {
        private readonly IConfiguration configuration;
        public MockRoulettes(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool AddRoulette(Entities.Roulettes roulette)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    var query = $"INSERT INTO [BetPlay].[dbo].[Roulettes]([Name], [State]) VALUES('{roulette.Name}',{(roulette.State ? 1 : 0)})";
                    SqlCommand command = new SqlCommand(query, connection);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
        }
        public Entities.Roulettes GetRoulette(int id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                var query = $"SELECT	Id, Name, State FROM [BetPlay].[dbo].[Roulettes] WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Entities.Roulettes response = ReadRoulettes(reader).FirstOrDefault();
                    reader.Close();
                    connection.Close();
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<Entities.Roulettes> GetRoulettes()
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                var query = "SELECT	Id, Name, State FROM [BetPlay].[dbo].[Roulettes]";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Entities.Roulettes> response = ReadRoulettes(reader);
                    reader.Close();
                    connection.Close();
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool EditRoulette(Entities.Roulettes roulette)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    var query = $"UPDATE [BetPlay].[dbo].[Roulettes] SET [Name]='{roulette.Name}', [State]={(roulette.State ? 1 : 0)} WHERE Id = {roulette.Id}";
                    SqlCommand command = new SqlCommand(query, connection);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
        }
        private List<Entities.Roulettes> ReadRoulettes(SqlDataReader reader)
        {
            List<Entities.Roulettes> roulettes = new List<Entities.Roulettes>();
            while (reader.Read())
            {
                roulettes.Add(new Entities.Roulettes
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    State = reader.GetBoolean(2)
                });
            }
            return roulettes;
        }
    }
}
