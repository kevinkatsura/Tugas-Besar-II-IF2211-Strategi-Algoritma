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

        

        // Deklarasi file opener
        OpenFileDialog ofd = new OpenFileDialog();

        // Deklarasi graph
        Graph g = new Graph();
        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text Documents (*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                g.clearGraph();
                // Dekalrasi graph drawer
                Microsoft.Msagl.Drawing.Graph graphDrawer = new Microsoft.Msagl.Drawing.Graph("graph");

                // Membuat graph dari text yang di buka
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
            if(!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Belum memilih metode!");
                return;
            }
            if(comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Belum memilih account!");
                return;
            }
            textBox2.Clear();
            DFS dfs = new DFS(this.contents);
            string init = comboBox1.SelectedItem.ToString();
            string dest = comboBox2.SelectedItem.ToString();
            MutualFriend mutualfriend = new MutualFriend(init, this.contents);
            mutualfriend.search(this.contents);
            mutualfriend.sortRelation();
            textBox2.AppendText(" Friend Recommendations for "+ init +" :\r\n");
            for(int i = 0; i < mutualfriend.NumOfRelation; i++){
                if(int.Parse(mutualfriend.relation[i][0]) != 0){
                    textBox2.AppendText((i + 1).ToString()+". "+mutualfriend.relation[i][1] + " ");
                    if (mutualfriend.relation[i][1] == dest){
                        if (radioButton1.Checked){
                            textBox2.AppendText(dfs.showDFS(init, dest, this.contents));
                            
                        }
                        if (radioButton2.Checked){
                            BFS bfs = new BFS(this.contents);
                            textBox2.AppendText(bfs.ShowBFS(bfs.ExploreFriendBFS(init, dest)));
                        }
                    }
                    textBox2.AppendText("\r\n    ");
                    textBox2.AppendText(mutualfriend.relation[i][0] + " Mutual Friends : ");
                    for (int j = 0; j < int.Parse(mutualfriend.relation[i][0]); j++){
                        textBox2.AppendText(mutualfriend.relation[i][j + 2]);
                    }
                    textBox2.AppendText("\r\n");
                    
                }
            }
            textBox2.AppendText("\r\n");
            textBox2.AppendText("Nama akun : "+init+" dan "+dest+"\r\n");
            if (radioButton1.Checked){
                // mencari dengan DFS
                DFS dfs2 = new DFS(this.contents);
                string text = dfs2.showDFS(init, dest, this.contents);

                // memprint hasil ke textBox2
                textBox2.AppendText(text);
                
                // mendeklarasikan delimiter untuk parsing text menjadi nodes
                char[] dilimiter = { ' ', '-', '>', ',', '(', ')' };
                string[] hasil;

                // jika ditemukan koneksi
                if(!text.Equals("Tidak ada jalur koneksi yang tersedia")){
                    hasil = text.Split(dilimiter);
                    // membuat list of node yg dikunjungi
                    List<string> edges = new List<string> { };
                    foreach(string t in hasil)
                    {
                        if (g.Nodes.Contains(t))
                        {
                            edges.Add(t);
                        }
                    }
                    
                    // merubah warna edge dan node
                    for(int i =0; i < edges.Count()-1; i++)
                    {
                        foreach(Microsoft.Msagl.Drawing.Edge E in gViewer1.Graph.Edges)
                        {
                            if ((E.Source.Equals(edges[i]) && E.Target.Equals(edges[i + 1])) || (E.Source.Equals(edges[i + 1]) && E.Target.Equals(edges[i])))
                            {
                                E.Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                            }
                        }
                        /*
                        if (i != 0 || i != edges.Count() - 1)
                        {
                            gViewer1.Graph.FindNode(edges[i]).Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
                        }
                        */
                    }
                    gViewer1.Refresh();
                    
                }
            }
            if (radioButton2.Checked){
                // mencari dengan bfs
                BFS bfs2 = new BFS(this.contents);

                string text = bfs2.ShowBFS(bfs2.ExploreFriendBFS(init, dest));
                textBox2.AppendText(text);

                // mendeklarasikan delimiter untuk parsing text menjadi nodes
                char[] dilimiter = { ' ', '-', '>', ',', '(', ')' };
                string[] hasil;

                // jika ditemukan koneksi
                if (!text.Equals("Tidak ada jalur koneksi yang tersedia"))
                {
                    hasil = text.Split(dilimiter);
                    // membuat list of node yg dikunjungi
                    List<string> edges = new List<string> { };
                    foreach (string t in hasil)
                    {
                        if (g.Nodes.Contains(t))
                        {
                            edges.Add(t);
                            
                        }
                    }
                    textBox2.AppendText(edges[0]);
                    // merubah warna edge dan node
                    for (int i = 0; i < edges.Count() - 1; i++)
                    {
                        foreach (Microsoft.Msagl.Drawing.Edge E in gViewer1.Graph.Edges)
                        {
                            if ((E.Source.Equals(edges[i]) && E.Target.Equals(edges[i + 1])) || (E.Source.Equals(edges[i + 1]) && E.Target.Equals(edges[i])))
                            {
                                E.Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                            }
                        }
                        /*
                        if (i != 0 || i != edges.Count() - 1)
                        {
                            gViewer1.Graph.FindNode(edges[i]).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGreen;
                        }
                        */
                    }
                    gViewer1.Refresh();

                }
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
            foreach (Microsoft.Msagl.Drawing.Edge E in gViewer1.Graph.Edges)
            {
                E.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
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
            foreach (Microsoft.Msagl.Drawing.Edge E in gViewer1.Graph.Edges)
            {
                E.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
