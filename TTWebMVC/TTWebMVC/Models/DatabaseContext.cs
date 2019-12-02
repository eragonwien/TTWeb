using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVC.Models
{
   public class DatabaseContext
   {
      public string ConnectionString { get; set; }

      public DatabaseContext(string connectionString)
      {
         ConnectionString = connectionString;
      }

      public MySqlConnection GetConnection()
      {
         MySqlConnection connection = new MySqlConnection(ConnectionString);
         return connection;
      }
   }
}
