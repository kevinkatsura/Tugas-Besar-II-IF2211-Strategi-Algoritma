﻿using System;
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
                textBox1.Text = ofd.SafeFileName;
                string[] isi = File.ReadAllLines(ofd.FileName);
                g.setGraph(isi);
                textBox2.Text = g.Edges[0].Node1;

                // Membuat graph drawer
                for (int i = 0; i < g.Total; i++)
                {
                    graphDrawer.AddEdge(g.Edges[i].Node1, g.Edges[i].Node2);
                }
                gViewer1.Graph = graphDrawer;
                
                // Menambah item pada box choose account
                foreach(string node in g.Nodes)
                {
                    comboBox1.Items.Add(node);
                }

            }
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Menambah atau memperbaharui item di box Explore friends with
            comboBox2.Items.Clear();
            foreach(Edge ed in g.Edges)
            {
                if (ed.isAdjacent(comboBox1.Text))
                {
                    if(ed.Node1 != comboBox1.Text)
                    {
                        comboBox2.Items.Add(ed.Node1);
                    }
                    else
                    {
                        comboBox2.Items.Add(ed.Node2);
                    }
                }
            }

            gViewer1.Graph.FindNode(comboBox1.Text).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
        }
    }
}