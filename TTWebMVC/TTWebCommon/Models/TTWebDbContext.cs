using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebCommon.Models
{
   public class TTWebDbContext : IDisposable
   {
      public MySqlConnection Connection { get; set; }
      public MySqlCommand Command { get; set; }
      public TTWebDbContext(string connectionString)
      {
         Connection = new MySqlConnection(connectionString);
      }

      public void Dispose()
      {
         if (Command != null)
         {
            Command.Dispose();
         }
         if (Connection != null)
         {
            Connection.Dispose();
         }
      }

      public MySqlCommand CreateCommand(string cmdStr)
      {
         Command = Connection.CreateCommand();
         Command.CommandText = cmdStr;
         return Command;
      }

      public async Task<bool> ReadScalarBooleanAsync()
      {
         return (int)await Command.ExecuteScalarAsync() == 1;
      }
   }
}
