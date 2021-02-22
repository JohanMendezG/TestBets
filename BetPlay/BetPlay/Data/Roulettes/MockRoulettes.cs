﻿using Microsoft.Extensions.Configuration;
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
        public Entities.Roulettes GetRoulette(int id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"SELECT Id, State FROM [BetPlay].[dbo].[Roulettes] WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    Entities.Roulettes response = ReadRows(reader).FirstOrDefault();
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
                try
                {
                    connection.Open();
                    var query = "SELECT	Id, State FROM [BetPlay].[dbo].[Roulettes]";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Entities.Roulettes> response = ReadRows(reader);
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
        public int CreateRoulette()
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"INSERT INTO [BetPlay].[dbo].[Roulettes]([State]) VALUES(0);" +
                           $"SELECT Id = CAST(SCOPE_IDENTITY() AS INT), CAST(0 AS bit)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    Entities.Roulettes response = ReadRows(reader).FirstOrDefault();
                    reader.Close();
                    connection.Close();
                    return response.Id;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool OpenRoulette(int id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"UPDATE [BetPlay].[dbo].[Roulettes] SET [State]=1 WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool CloseRoulette(int id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"UPDATE [BetPlay].[dbo].[Roulettes] SET [State]=0 WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private List<Entities.Roulettes> ReadRows(SqlDataReader reader)
        {
            List<Entities.Roulettes> roulettes = new List<Entities.Roulettes>();
            while (reader.Read())
            {
                roulettes.Add(new Entities.Roulettes
                {
                    Id = (int)reader[0],
                    State = (bool)reader[1]
                });
            }
            return roulettes;
        }
    }
}
