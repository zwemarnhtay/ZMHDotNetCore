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
using ZMHDotNetCore.WindowsFormApp;
using ZMHDotNetCore.WinFormsApp.Models;
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
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> BlogList = _dapper.Query<BlogModel>("SELECT * FROM [Tbl_Blog]");
            dgvData.DataSource = BlogList;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            int blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

            //MessageBox.Show($"{e.RowIndex} - {e.ColumnIndex}");

            #region if case
            /*
            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                frmBlog frm = new frmBlog(blogId);
                frm.ShowDialog();

                BlogList();
            }
            else if(e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;

                DeleteBlog(blogId);

                BlogList();
            } 
            */
            #endregion

            #region Switch
            int index = e.ColumnIndex;
            EnumFormControlType Type = (EnumFormControlType)index;
            switch (Type)
            {
                case EnumFormControlType.Edit:
                    frmBlog frm = new frmBlog(blogId);
                    frm.ShowDialog();

                    BlogList();

                    break;
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes) return;

                    DeleteBlog(blogId);

                    BlogList();

                    break;
                case EnumFormControlType.None:
                default:
                    break;
            }
            #endregion
        }

        private void DeleteBlog(int blogId)
        {
            int result = _dapper.Execute(BlogQuery.DeleteQuery, new {BlogId = blogId});
            string msg = result > 0 ? "deleted success" : "failed";
            MessageBox.Show(msg);
        }
    }
}
