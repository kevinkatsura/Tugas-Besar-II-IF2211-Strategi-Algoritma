using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        GraphHandler g = new GraphHandler();
        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text Documents (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.SafeFileName;
                string[] isi = File.ReadAllLines(ofd.FileName);
                g.setGraphHandler(isi);
                textBox2.Text = g.Edges[0].Node1;
            }
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Microsoft.Msagl.Drawing.Graph drawinggraph = new Microsoft.Msagl.Drawing.Graph("graph");
            for (int i=0; i<g.Total; i++)
            {
                drawinggraph.AddEdge(g.Edges[i].Node1, g.Edges[i].Node2);
            }
            gViewer1.Graph = drawinggraph;
        }
        
    }
}
