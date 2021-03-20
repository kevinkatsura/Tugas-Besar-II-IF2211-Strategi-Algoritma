using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeopleUMayKnow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gViewer1_Load(object sender, EventArgs e)
        {

        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text Documents (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.SafeFileName;
            }
            
        }
    }
}
