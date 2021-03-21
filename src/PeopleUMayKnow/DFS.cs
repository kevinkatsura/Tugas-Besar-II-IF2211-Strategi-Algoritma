using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleUMayKnow
{
    class DFS
    {
        private string[][] vertex;
        private int NumOfVertex;
        public DFS(string[] raw) {
            string[] buffer = new string[int.Parse(raw[0])*2];
            int max = int.Parse(raw[0]);
            int count = 0;
            for(int i = 1; i <= max; i++)
            {
                string[] buf = raw[i].Split(' ');
                int j = 0;
                bool found1 = false; 
                bool found2 = false;
                while(j < count && (!found1 || !found2)){
                    if(buffer[j] == buf[0]){
                        found1 = true;
                    }
                    if(buffer[j] == buf[1]){
                        found2 = true;
                    }
                    j++;
                }
                if (!found1){
                    buffer[count] = buf[0];
                    count++;
                }
                if (!found2){
                    buffer[count] = buf[1];
                    count++;
                }
            }
            this.NumOfVertex = count;
            this.vertex = new string[count][];
            for(int k = 0; k < count; k++)
            {
                vertex[k] = new string[2];
                this.vertex[k][0] = buffer[k];
                this.vertex[k][1] = bool.FalseString;
            }
        }
        private bool isBranchVisited(string init, string[] raw)
        {
            int length = int.Parse(raw[0]);
            bool visited = true;
            string[] buf = new string[2];
            for (int i = 1; i <= length; i++)
            {
                buf = raw[i].Split(' ');
                if(buf[0] == init) { 
                    int j = 0;
                    bool found = false;
                    while(j < this.NumOfVertex && !found){
                        if (this.vertex[j][0] == buf[1] && !(bool.Parse(this.vertex[j][1])))
                        {
                            visited = false;
                            found = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
                if(buf[1] == init)
                {
                    int j = 0;
                    bool found = false;
                    while (j < this.NumOfVertex && !found)
                    {
                        if (this.vertex[j][0] == buf[0] && !(bool.Parse(this.vertex[j][1])))
                        {
                            visited = false;
                            found = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
            }
            return visited;
        }

        private string[] listBranch(string prevVertex,string vertex, string[] raw)
        {
            int length = int.Parse(raw[0]);
            string[] buf = new string[2];
            string[] tList = new string[this.NumOfVertex+1];
            int count = 1;
            for(int i = 1; i <= length; i++)
            {
                buf = raw[i].Split(' ');
                if((buf[0] == vertex) && (buf[1] != prevVertex))
                {
                    tList[count] = buf[1];
                    count++;
                }
                if ((buf[1] == vertex) && (buf[0] != prevVertex))
                {
                    tList[count] = buf[0];
                    count++;
                }
            }
            tList[0] = (count-1).ToString();
            return tList;
        }

        private string[] sortStringList(string[] tList)
        {
            int length = int.Parse(tList[0]);
            int offset;
            string buffer;
            for (int i = 1; i <= length; i++){
                for(int j = i+1; j <= length; j++){
                    offset = 0;
                    if(tList[i][offset] > tList[j][offset]){
                        buffer = tList[i];
                        tList[i] = tList[j];
                        tList[j] = buffer;
                    }
                    if(tList[i][offset] == tList[j][offset]){
                        offset++;
                        bool done = false;
                        while (offset < tList[j].Length && offset < tList[i].Length && !done){
                            if(tList[i][offset] > tList[j][offset]){
                                buffer = tList[i];
                                tList[i] = tList[j];
                                tList[j] = buffer;
                                done = true;
                            }
                            else{
                                offset++;
                            }
                        }
                    }
                }
            }
            return tList;
        }

        private void setInactive(string prevVertex, string vertex, string[] raw)
        {
            string[] buffer;
            buffer = listBranch(prevVertex, vertex, raw);
            int length = int.Parse(buffer[0]);
            for(int i = 1; i <= length; i++){
                if(prevVertex != buffer[i]){
                    int j = 0;
                    while(buffer[i] != this.vertex[j][0])
                    {
                        j++; 
                    }
                    this.vertex[j][1] = bool.FalseString;
                }
            }
        }

        private void setActiveVertex(string vertex)
        {
            int i = 0;
            while(this.vertex[i][0] != vertex)
            {
                i++;
            }
            this.vertex[i][1] = bool.TrueString;
        }

        private string selectBranch(string[] tList)
        {
            int i = 1;
            int j;
            int length = int.Parse(tList[0]);
            while (i <= length)
            {
                j = 0;
                while(j < this.NumOfVertex)
                {
                    if(tList[i] == this.vertex[j][0] && !bool.Parse(this.vertex[j][1]))
                    {
                        return this.vertex[j][0];
                    }
                    else
                    {
                        j++;
                    }
                }
                i++;
            }
            return "";
        }

        public string[] ExploreFriend(string init, string dest, string[] raw)
        {
            // Create array of string for tracking
            string[] track = new string[this.NumOfVertex];
            int count = 1;
            track[count] = init;
            //
            bool found = false;
            int length = int.Parse(raw[0]);

            //
            string next;
            setActiveVertex(init);
            next = sortStringList(listBranch("", init, raw))[0];

            //
            while(!found){
                if(next == dest){
                    count++;
                    track[count] = next;
                    found = true;
                }else{
                    if(int.Parse(listBranch(track[count],next,raw)[0]) == 0){
                        next = track[count];
                        count--;
                    }
                    else{
                        if (isBranchVisited(next, raw)) {
                            if (count == 1) {
                                track[count] = "Tidak ada jalur koneksi yang tersedia";
                                found = true;
                            }
                            else {
                                setInactive(track[count],next, raw);
                                next = track[count];
                                count--;
                            }
                        }
                        else{
                            setActiveVertex(next);
                            count++;
                            track[count] = next;
                            next = selectBranch(sortStringList(listBranch(track[count], next, raw)));
                        }
                    }

                }
            }
            track[0] = count.ToString();
            return track;
        }

        public void showDFS(string init, string dest, string[] raw)
        {
            int i = int.Parse(ExploreFriend(init, dest, raw)[0]);
            if(i == 1)
            {

            }
            else
            {
                string result = "";
                string[] buffer = new string[i];
                buffer = ExploreFriend(init, dest, raw);
                for(int j = 1; j <= i; j++)
                {
                    result = result + buffer[j] + " ";
                }
            }
        }

    }
}
