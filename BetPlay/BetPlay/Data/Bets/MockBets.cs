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
        public bool LoadBet(Entities.Bets bet)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    var query = $"INSERT INTO [BetPlay].[dbo].[Bets](RouletteId,UserId,Number,Color,[Money]) " +
                        $"VALUES({bet.RouletteId},{bet.UserId},{bet.Number},{bet.Color},{bet.Money});";
                    SqlCommand command = new SqlCommand(query, connection);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }
        }
        public decimal MoneyByRoulette(int rouletteId)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                var query = $"SELECT [Money] = ISNULL(CAST(SUM([Money]) AS DECIMAL(10,2)),0) FROM [BetPlay].[dbo].[Bets] WHERE RouletteId ={rouletteId}";
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    decimal response = (decimal)reader[0];
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
    }
}
