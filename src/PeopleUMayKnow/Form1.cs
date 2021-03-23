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
using Microsoft.Msagl.Drawing;

namespace PeopleUMayKnow
{
    public partial class Form1 : Form
    {
        string[] contents;
        public Form1()
        {
            InitializeComponent();
        }

        // Dekalrasi graph drawer
        Microsoft.Msagl.Drawing.Graph graphDrawer = new Microsoft.Msagl.Drawing.Graph("graph");

        // Deklarasi file opener
        OpenFileDialog ofd = new OpenFileDialog();

        // Deklarasi graph
        Graph g = new Graph();
        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text Documents (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Membuat graph dari text yang di buka
                g.clearGraph();
                path.Text = ofd.SafeFileName;
                string[] isi = File.ReadAllLines(ofd.FileName);
                g.setGraph(isi);
                //textBox2.Text = g.Edges[0].Node1;

                // Membuat graph drawer
                for (int i = 0; i < g.Total; i++)
                {
                    graphDrawer.AddEdge(g.Edges[i].Node1, g.Edges[i].Node2).Attr.ArrowheadLength = 1;
                }
                gViewer1.Graph = graphDrawer;
                
                // Menambah item pada box choose account
                foreach(string node in g.Nodes)
                {
                    comboBox1.Items.Add(node);
                }
                // Assign file ke field contents
                this.contents = new string[int.Parse(isi[0])+1];
                this.contents = isi;
            }
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            if (radioButton1.Checked)
            {
                string init = comboBox1.SelectedItem.ToString();
                string dest = comboBox2.SelectedItem.ToString();
                DFS dfs = new DFS(this.contents);
                textBox2.AppendText(dfs.showDFS(init, dest, this.contents));
            }
            if (radioButton2.Checked)
            {
                string init = comboBox1.SelectedItem.ToString();
                string dest = comboBox2.SelectedItem.ToString();
                BFS bfs = new BFS(this.contents);
                textBox2.AppendText(bfs.showBFS(bfs.ExploreFriendBFS(init, dest)));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Menambah atau memperbaharui item di box Explore friends with
            comboBox2.Items.Clear();
            foreach(Node n in gViewer1.Graph.Nodes)
            {
                if(n.LabelText != comboBox2.Text)
                {
                    n.Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                }
                
            }
            gViewer1.Refresh();

            foreach(string nd in g.Nodes)
            {
                if (nd != comboBox1.Text)
                {
                    comboBox2.Items.Add(nd);
                }
            }

            gViewer1.Graph.FindNode(comboBox1.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            gViewer1.Refresh();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Node n in gViewer1.Graph.Nodes)
            {
                if (n.LabelText != comboBox1.Text)
                {
                    n.Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                }

            }
            gViewer1.Graph.FindNode(comboBox2.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
            gViewer1.Refresh();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void gViewer1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
