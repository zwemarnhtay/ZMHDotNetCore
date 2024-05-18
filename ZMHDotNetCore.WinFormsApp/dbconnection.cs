using System.Data.SqlClient;

namespace ZMHDotNetCore.RestAPIWithNLayer
{
    public class dbconnection
    {
        public static SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-1U67J66",     // server name
            InitialCatalog = "DotNetTrainingBt4",     // database name
            UserID = "sa",                         //user name
            Password = "sa@123",                  //server password
            TrustServerCertificate = true,
        };
    }
}
