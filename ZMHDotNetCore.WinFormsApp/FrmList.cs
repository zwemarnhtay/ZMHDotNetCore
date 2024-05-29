using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMHDotNetCore.RestAPIWithNLayer;
using ZMHDotNetCore.RestAPIWithNLayer.Models;
using ZMHDotNetCore.Shared;
using ZMHDotNetCore.WinFormsApp.Queries;

namespace ZMHDotNetCore.WinFormsApp
{
    public partial class FrmList : Form
    {
        private readonly DapperServices _dapper;
        public FrmList()
        {
            InitializeComponent();
            _dapper = new DapperServices(DBConnection.ConnectionBuilder.ConnectionString);
        }

        private void FrmList_Load(object sender, EventArgs e)
        {
            List<BlogModel> BlogList = _dapper.Query<BlogModel>("SELECT * FROM [Tbl_Blog]");
            dgvData.DataSource = BlogList;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
