using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BetPlay.Data.Users
{
    public class MockUsers : IUsers
    {
        private readonly IConfiguration configuration;
        public MockUsers(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Entities.Users GetUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                try
                {
                    connection.Open();
                    var query = $"SELECT Id, Name, Money FROM [BetPlay].[dbo].[Users] WHERE Id = {id}";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    Entities.Users response = readRows(reader).FirstOrDefault();
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
        private List<Entities.Users> readRows(SqlDataReader reader)
        {
            List<Entities.Users> users = new List<Entities.Users>();
            while (reader.Read())
            {
                users.Add(new Entities.Users
                {
                    Id = (int)reader[0],
                    Name = (string)reader[1],
                    Money = (decimal)reader[2]
                });
            }
            return users;
        }
    }
}
