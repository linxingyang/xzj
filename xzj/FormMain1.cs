using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xzj
{
    public partial class FormMain1 : Form
    {
        public FormMain1()
        {
            InitializeComponent();
            this.Height = 500;
        }

        private void picCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void myVScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
        }

        
    }
}
