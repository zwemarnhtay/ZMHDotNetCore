using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZMHDotNetCore.WindowsFormApp;

namespace ZMHDotNetCore.WinFormsApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBlog frm = new frmBlog();
            frm.ShowDialog();
        }

        private void blogsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmList frm = new FrmList();
            frm.ShowDialog();
        }
    }
}
