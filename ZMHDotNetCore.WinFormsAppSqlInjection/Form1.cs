using System.Data.Common;
using ZMHDotNetCore.Shared;

namespace ZMHDotNetCore.WinFormsAppSqlInjection
{
    public partial class Form1 : Form
    {
        DapperServices _dapper;

        public Form1()
        {
            InitializeComponent();
            _dapper = new DapperServices(DBConnection.ConnectionBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // string query = $"SELECT * FROM [dbo].[Tbl_User] WHERE Email = '{txtEmail.Text.Trim()}' AND Password = '{txtPassword.Text.Trim()}'";
            string query = $"SELECT * FROM [dbo].[Tbl_User] WHERE Email = @email AND Password = @password";

            UserModel model = _dapper.QueryFirstOrDefault<UserModel>(query, new
            {
                email = txtEmail.Text.Trim(),
                password = txtPassword.Text.Trim(),
            });

            if(model == null)
            {
                MessageBox.Show("no user found!");
                return;
            }

            MessageBox.Show($"Is Admin : {model.IsAdmin}");
        }
    }

    public class UserModel
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
