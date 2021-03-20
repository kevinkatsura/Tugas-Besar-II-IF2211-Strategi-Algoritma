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
        public string Node1;
        public string Node2;

        public Edge()
        {
            this.Node1 = "";
            this.Node2 = "";
        }
    }

    public class GraphHandler
    {
        // atribut
        // aku ga tau kenapa harus public
        public int Total;
        public Edge[] Edges;
        public int NumOfEdges;

        public GraphHandler() // ctor
        {
            this.Total = 0;
            this.Edges = new Edge[100];
            this.NumOfEdges = 0;
        }
        public void setGraphHandler(string[] raw)
        {
            // Membuat graph handler dari array of string raw yang telah dibaca dari file
            this.Total = int.Parse(raw[0]);
            for(int i=1; i<this.Total; i++)
            {
                string[] temp = raw[i].Split(' ');
                Edge e = new Edge();
                e.Node1 = temp[0];
                e.Node2 = temp[1];
                this.Edges[i - 1] = e;
                this.NumOfEdges++;
                
            }
        }
    }

}
