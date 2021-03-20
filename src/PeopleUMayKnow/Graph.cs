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
    }

    public class GraphHandler
    {
        // atribut
        // aku ga tau kenapa harus public
        public int Total { get; set; }
        public List<Edge> Edges { get; set; }

        public GraphHandler() // ctor
        {
            this.Total = 0;
            this.Edges = new List<Edge> { };
        }
        public void setGraphHandler(string[] raw)
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
                
            }
        }
    }

}
