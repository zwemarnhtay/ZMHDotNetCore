using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMHDotNetCore.RestAPIWithNLayer;
using ZMHDotNetCore.RestAPIWithNLayer.Models;
using ZMHDotNetCore.Shared;
using ZMHDotNetCore.WinFormsApp.Queries;

namespace ZMHDotNetCore.WindowsFormApp
{
    public partial class frmBlog : Form
    {
        private readonly DapperServices _dapper;
        public frmBlog()
        {
            InitializeComponent();
            _dapper = new DapperServices(dbconnection.connectionBuilder.ConnectionString);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                blogModel blog = new blogModel();
                blog.blogTitle = txtTitle.Text.Trim();
                blog.blogAuthor = txtAuthor.Text.Trim();
                blog.blogContent = txtContent.Text.Trim();

                int result = _dapper.Execute(blogQuery.blogInsert, blog);
                string msg = result > 0 ? "created success" : "something failed";
                MessageBox.Show(msg, "blog", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                if(result > 0) clearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void clearForm()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();

            txtTitle.Focus();
        }
    }
}
