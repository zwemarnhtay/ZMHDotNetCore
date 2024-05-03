using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ZMHDotNetCore.ConsoleApp.AdoDotNet
{
    internal class AdoDotNetCRUD
    {

        private readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-1U67J66",     // server name
            InitialCatalog = "DotNetTrainingBt4",     // database name
            UserID = "sa",                         //user name
            Password = "sa@123",                  //server password
        };

        public void read()  //void => no return
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);

            connection.Open();
            //start code for CRUD here  

            string query = "select * from tbl_Blog";
            SqlCommand cmd = new SqlCommand(query, connection);    //
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            //end code for CRUD here
            connection.Close();

            //loop data from database for output
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("BolgId => " + dr["BlogId"]);
                Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
                Console.WriteLine("BolgContent => " + dr["BlogContent"]);
                Console.WriteLine("BolgAuthor => " + dr["BlogAuthor"]);
                Console.WriteLine("------------------------------------");
            }
        }

        public void create(string title, string content, string author)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                            (@BlogTitle,
                             @BlogAuthor,
                             @BlogContent)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogTitle", title); // insert title
            command.Parameters.AddWithValue("@BlogAuthor", author);
            command.Parameters.AddWithValue("@BlogContent", content);

            int result = command.ExecuteNonQuery(); // execute

            connection.Close();

            string msg = result > 0 ? "success" : "failed, smth wrong";
            Console.WriteLine(msg);
        }

        public void update(int id, string title, string content, string author)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            command.Parameters.AddWithValue("@BlogTitle", title);
            command.Parameters.AddWithValue("@BlogAuthor", author);
            command.Parameters.AddWithValue("@BlogContent", content);

            int result = command.ExecuteNonQuery(); // execute

            connection.Close();

            string msg = result > 0 ? "updated successfully." : "failed, smth wrong";
            Console.WriteLine(msg);
        }

        public void delete(int id)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);

            int result = command.ExecuteNonQuery(); // execute

            connection.Close();

            string msg = result > 0 ? "deleted successfully." : "failed, smth wrong";
            Console.WriteLine(msg);
        }

        public void edit(int id)
        {
            SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);

            connection.Open();

            string query = "select * from tbl_Blog where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);    //
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("BolgId => " + dr["BlogId"]);
                Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
                Console.WriteLine("BolgContent => " + dr["BlogContent"]);
                Console.WriteLine("BolgAuthor => " + dr["BlogAuthor"]);
                Console.WriteLine("------------------------------------");
            }
        }

    }
}


