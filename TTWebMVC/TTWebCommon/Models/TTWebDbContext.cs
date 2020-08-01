using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using SNGCommon.Sql.MySql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTWebCommon.Models
{
    public class TTWebDbContext : IDisposable
    {
        public MySqlConnection Connection { get; set; }
        public TTWebDbContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
            }
        }

        public async Task<MySqlCommand> CreateCommand(string cmdStr)
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Closed)
            {
                await Connection.OpenAsync();
            }
            return Connection.CreateMySqlDbCommand(cmdStr);
        }

        public async Task<MySqlCommand> CreateCommand(StringBuilder cmdStringBuilder)
        {
            return await CreateCommand(cmdStringBuilder.ToString());
        }
    }
}
