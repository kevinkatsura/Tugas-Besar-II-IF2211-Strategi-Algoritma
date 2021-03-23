using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace PeopleUMayKnow
{
    class MutualFriend{
        public string[][] relation;
        public int NumOfRelation;
        public string accountName;
        public MutualFriend(string init,string[] raw){
            this.accountName = init;
            DFS dfs = new DFS(raw);
            this.relation = new string[dfs.NumOfVertex-1][];
            int count = 0;
            for(int i = 0; i < dfs.NumOfVertex; i++){
                if(dfs.vertex[i][0] != init && !isFriend(init,dfs.vertex[i][0],raw)){
                    this.relation[count] = new string[dfs.NumOfVertex];
                    this.relation[count][1] = dfs.vertex[i][0];
                    this.relation[count][0] = 0.ToString();
                    count++;
                }
            }
            this.NumOfRelation = count;
        }

        public bool isFriend(string init, string dest, string[] raw){
            int length = int.Parse(raw[0]);
            int i = 1;
            string[] buffer = new string[2];
            while(i <= length){
                buffer = raw[i].Split(' ');
                if(buffer[0] == init){
                    if(buffer[1] == dest){
                        return true;
                    }
                }
                if (buffer[1] == init)
                {
                    if (buffer[0] == dest)
                    {
                        return true;
                    }
                }
                i++;
            }
            return false;
        } 

        public  void search(string[] raw){
            string[] buffer;
            int length = int.Parse(raw[0]);
            for(int i = 0; i<this.NumOfRelation; i++){
                for(int j = 1; j<= length; j++){
                    buffer = raw[j].Split(' ');
                    if(buffer[0] == this.relation[i][1]){
                        if (isFriend(buffer[1], this.accountName,raw)){
                            this.relation[i][0] = (int.Parse(this.relation[i][0]) + 1).ToString();
                            this.relation[i][int.Parse(this.relation[i][0]) + 1] = buffer[1];
                        }
                    }
                    if (buffer[1] == this.relation[i][1]){
                        if (isFriend(buffer[0], this.accountName, raw)){
                            this.relation[i][0] = (int.Parse(this.relation[i][0]) + 1).ToString();
                            this.relation[i][int.Parse(this.relation[i][0]) + 1] = buffer[0];
                        }
                    }
                }
            }
        }

        public void sortRelation(){
            string[] temp;
            for(int i = 0; i < this.NumOfRelation; i++){
                for(int j = i+1; j < this.NumOfRelation; j++){
                    if(int.Parse(this.relation[j][0]) > int.Parse(this.relation[i][0])){
                        temp = this.relation[j];
                        this.relation[j] = this.relation[i];
                        this.relation[i] = temp;
                    }
                }
            }
        }
        public void showFriendRecomendation(string[] raw){
            sortRelation();
            for(int i = 0;i < this.NumOfRelation; i++){
                if(int.Parse(this.relation[i][0]) != 0){
                    /*for(int j = 2; )*/
                }
            }
        }
    }
}
