using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleUMayKnow
{
    public class Edge
    {
        // atribut
        public string Node1 { get; set; }
        public string Node2 { get; set; }

        public Edge()
        {

        }

        public bool isAdjacent(string node)
        {
            return ((this.Node1 == node) || (this.Node2 == node)) && this.Node1 != this.Node2;
        }

        
    }

    public class Graph
    {
        // atribut
        // aku ga tau kenapa harus public
        public int Total { get; set; }
        public List<Edge> Edges { get; set; }
        public List<string> Nodes { get; set; }

        public Graph() // ctor
        {
            this.Total = 0;
            this.Edges = new List<Edge> { };
            this.Nodes = new List<string> { };
        }
        public void setGraph(string[] raw)
        {
            // Membuat graph handler dari array of string raw yang telah dibaca dari file
            this.Total = int.Parse(raw[0]);
            for(int i=1; i<this.Total+1; i++)
            {
                string[] temp = raw[i].Split(' ');
                Edge e = new Edge();
                e.Node1 = temp[0];
                e.Node2 = temp[1];
                this.Edges.Add(e);
                if (!this.Nodes.Contains(temp[0]))
                {
                    this.Nodes.Add(temp[0]);
                }
                if (!this.Nodes.Contains(temp[1]))
                {
                    this.Nodes.Add(temp[1]);
                }

            }
        }

        public void clearGraph()
        {
            this.Total = 0;
            this.Edges.Clear();
            this.Nodes.Clear();
        }

        
    }

}
