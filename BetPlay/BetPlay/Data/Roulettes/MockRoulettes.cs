using BetPlay.Data.Bets;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
                    var query = $"SELECT Id,State,Apertura,Cierre FROM [BetPlay].[dbo].[Roulettes] WHERE Id = {id}";
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
                    var query = "SELECT	Id,State,Apertura,Cierre FROM [BetPlay].[dbo].[Roulettes]";
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
                    var query = $"INSERT INTO [BetPlay].[dbo].[Roulettes]([State],Apertura,Cierre) VALUES(0,'',''); " +
                           $"SELECT Id = CAST(SCOPE_IDENTITY() AS INT), [State] = CAST(0 AS bit), Apertura = '', Cierre = ''";
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
                    var query = $"UPDATE [BetPlay].[dbo].[Roulettes] SET [State]=1, Apertura='{DateTime.UtcNow:O}', Cierre='' WHERE Id = {id}";
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
                    var query = $"UPDATE [BetPlay].[dbo].[Roulettes] SET [State]=0, Cierre='{DateTime.UtcNow:O}' WHERE Id = {id}";
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
        public List<Entities.Bets> BetsResults(int roulettesId)
        {
            var roulette = GetRoulette(roulettesId);
            return FilterBets(roulette);
        }
        private List<Entities.Bets> FilterBets(Entities.Roulettes roulette)
        {
            var apertura = DateTime.Parse(roulette.Apertura, null, System.Globalization.DateTimeStyles.RoundtripKind);
            var cierre = DateTime.Parse(roulette.Cierre, null, System.Globalization.DateTimeStyles.RoundtripKind);
            List<Entities.Bets> bets = GetBets(roulette.Id);
            List<Entities.Bets> filterBets = new List<Entities.Bets>();
            foreach (var bet in bets)
            {
                var fecha = DateTime.Parse(bet.Fecha, null, System.Globalization.DateTimeStyles.RoundtripKind);
                if (fecha >= apertura && fecha <= cierre)
                    filterBets.Add(bet);
            }
            return filterBets;
        }
        private List<Entities.Bets> GetBets(int roulettesId)
        {
            MockBets mockRoulettes = new MockBets(configuration);
            return mockRoulettes.GetBets(roulettesId);
        }
        private List<Entities.Roulettes> ReadRows(SqlDataReader reader)
        {
            List<Entities.Roulettes> roulettes = new List<Entities.Roulettes>();
            while (reader.Read())
            {
                roulettes.Add(new Entities.Roulettes
                {
                    Id = (int)reader[0],
                    State = (bool)reader[1],
                    Apertura = (string)reader[2],
                    Cierre = (string)reader[3]
                });
            }
            return roulettes;
        }
    }
}
