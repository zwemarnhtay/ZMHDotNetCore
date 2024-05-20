using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.RestAPI
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder StringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-1U67J66",     // server name
            InitialCatalog = "DotNetTrainingBt4",     // database name
            UserID = "sa",                         //user name
            Password = "sa@123",                  //server password
            TrustServerCertificate = true,
        };
    }
}
