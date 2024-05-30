using System.Data.SqlClient;

namespace ZMHDotNetCore.WinFormsApp
{
    public class DBConnection
    {
        public static SqlConnectionStringBuilder ConnectionBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-1U67J66",     // server name
            InitialCatalog = "DotNetTrainingBt4",     // database name
            UserID = "sa",                         //user name
            Password = "sa@123",                  //server password
            TrustServerCertificate = true,
        };
    }
}
