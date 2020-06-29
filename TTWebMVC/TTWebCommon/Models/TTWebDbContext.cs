using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using SNGCommon.Sql.MySql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebCommon.Models
{
   public class TTWebDbContext : IDisposable
   {
      public MySqlConnection Connection { get; set; }
      public TTWebDbContext(string connectionString)
      {
         Connection = new MySqlConnection(connectionString);
         Connection.OpenAsync();
      }

      public void Dispose()
      {
         if (Connection != null)
         {
            Connection.Dispose();
         }
      }

      public MySqlCommand CreateCommand(string cmdStr)
      {
         return Connection.CreateMySqlDbCommand(cmdStr);
      }
   }
}
