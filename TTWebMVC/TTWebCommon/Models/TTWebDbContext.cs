using System;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SNGCommon.Sql.MySql.Extensions;

namespace TTWebCommon.Models
{
    public class TTWebDbContext : IDisposable
    {
        public TTWebDbContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection Connection { get; set; }

        public void Dispose()
        {
            if (Connection != null) Connection.Dispose();
        }

        public async Task<MySqlCommand> CreateCommand(string cmdStr)
        {
            if (Connection != null && Connection.State == System.Data.ConnectionState.Closed)
                await Connection.OpenAsync();
            return Connection.CreateMySqlDbCommand(cmdStr);
        }

        public async Task<MySqlCommand> CreateCommand(StringBuilder cmdStringBuilder)
        {
            return await CreateCommand(cmdStringBuilder.ToString());
        }
    }
}