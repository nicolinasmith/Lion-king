using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion_king.DAL
{
    public class DbRepository
    {
        // koppla upp sig mot databas, i detta fall POSTGRESQL

        private readonly string _connectionString;

        public DbRepository()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<DbRepository>().Build();
            _connectionString = config.GetConnectionString("develop");

        }
    }
}
