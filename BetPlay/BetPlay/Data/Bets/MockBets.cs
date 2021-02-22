using BetPlay.Data.Roulettes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BetPlay.Data.Bets
{
    public class MockBets : IBets
    {
        private readonly IConfiguration configuration;
        public MockBets(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<Entities.Bets> GetBets(int RouletteId)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"SELECT Id,Fecha,RouletteId,UserId,BetNumber,Number,Color,Money FROM [BetPlay].[dbo].[Bets] WHERE RouletteId = {RouletteId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Entities.Bets> response = ReadRowsForBets(reader);
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
        public bool LoadBet(Entities.Bets bet)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"INSERT INTO [BetPlay].[dbo].[Bets](Fecha,RouletteId,UserId,BetNumber,Number,Color,[Money]) " +
                        $"VALUES('{DateTime.UtcNow:O}',{bet.RouletteId},{bet.UserId},'{bet.BetNumber}',{bet.Number},{bet.Color},{bet.Money})";
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
        public decimal MoneyByRoulette(int rouletteId)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"SELECT [Money] = ISNULL(CAST(SUM([Money]) AS DECIMAL(10,2)),0) FROM [BetPlay].[dbo].[Bets] WHERE RouletteId ={rouletteId}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    var response = ReadRows(reader);
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
        public bool ValidationRouletteIsOpen(int RouletteId)
        {
            MockRoulettes mockRoulettes = new MockRoulettes(configuration);
            return mockRoulettes.GetRoulette(RouletteId).State;
        }
        private decimal ReadRows(SqlDataReader reader)
        {
            decimal money = 0;
            while (reader.Read())
            {
                money = (decimal)reader[0];
            }
            return money;
        }
        private List<Entities.Bets> ReadRowsForBets(SqlDataReader reader)
        {
            List<Entities.Bets> bets = new List<Entities.Bets>();
            while (reader.Read())
            {
                bets.Add(new Entities.Bets
                {
                    Id = (int)reader[0],
                    Fecha = (string)reader[1],
                    RouletteId = (int)reader[2],
                    UserId = (int)reader[3],
                    BetNumber =(bool)reader[4],
                    Number = (int)reader[5],
                    Color = (int)reader[6],
                    Money = (decimal)reader[7]
                }) ;
            }
            return bets ;
        }
    }
}
