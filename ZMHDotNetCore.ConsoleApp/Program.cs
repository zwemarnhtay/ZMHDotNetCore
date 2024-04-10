// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "DESKTOP-1U67J66";       // server name
stringBuilder.InitialCatalog = "DotNetTrainingBt4";     // database name
stringBuilder.UserID = "sa";                         //user name
stringBuilder.Password = "sa@123";                  //server password
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

connection.Open();
//start code for CRUD here

String query = "select * from tbl_Blog";
SqlCommand cmd = new SqlCommand(query, connection);    //
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

//end code for CRUD here
connection.Close();

//loop data from database for output
foreach(DataRow dr in dt.Rows)
{
    Console.WriteLine("BolgId => " + dr["BlogId"]);
    Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
    Console.WriteLine("BolgContent => " + dr["BlogContent"]);
    Console.WriteLine("BolgAuthor => " + dr["BlogAuthor"]);
    Console.WriteLine("------------------------------------");
}

Console.ReadKey();