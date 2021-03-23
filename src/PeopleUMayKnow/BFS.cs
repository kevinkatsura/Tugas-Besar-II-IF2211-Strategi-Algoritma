using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleUMayKnow
{
    class BFS
    {
        public string[][] hubungan;
        public string[] buffer;
        public int number;

        public BFS(string[] raw)
        {
            string[][] vertex;
            //buat dalam bentuk array of string yang isinya daftar orang
            buffer = new string[int.Parse(raw[0])*2];
            int max = int.Parse(raw[0]);
            int count = 0;
            for(int i = 1; i <= max; i++)
            {
                string[] buf = raw[i].Split(' ');
                int j = 0;
                bool found1 = false; 
                bool found2 = false;
                while(j < count && (!found1 || !found2))
                {
                    if(buffer[j] == buf[0])
                    {
                        found1 = true;
                    }
                    if(buffer[j] == buf[1])
                    {
                        found2 = true;
                    }
                    j++;
                }
                if (!found1)
                {
                    buffer[count] = buf[0];
                    count++;
                }
                if (!found2)
                {
                    buffer[count] = buf[1];
                    count++;
                }
            }

            number = count;
            //urutkan array tersebut
            int length = buffer.Length;
            int offset;
            string temp;
            for (int i = 0; i <= length; i++){
                for(int j = i+1; j <= length; j++){
                    offset = 0;
                    if(buffer[i][offset] > buffer[j][offset]){
                        temp = buffer[i];
                        buffer[i] = buffer[j];
                        buffer[j] = temp;
                    }
                    if(buffer[i][offset] == buffer[j][offset]){
                        offset++;
                        bool done = false;
                        while (offset < buffer[j].Length && offset < buffer[i].Length && !done){
                            if(buffer[i][offset] > buffer[j][offset]){
                                temp = buffer[i];
                                buffer[i] = buffer[j];
                                buffer[j] = temp;
                                done = true;
                            }
                            else{
                                offset++;
                            }
                        }
                    }
                }
            }

            //buat dalam bentuk array of array yang elemennya adalah array yang elemen pertama orang, kedua ialah jumlah teman

            vertex = new string[count][];
            for(int k = 0; k < count; k++)
            {
                vertex[k] = new string[2];
                vertex[k][0] = buffer[k];
                int sum=0;
                for(int i = 1; i <= max; i++){
                    string[] buf = raw[i].Split(' ');
                    if(vertex[k][0]==buf[0] || vertex[k][0] == buf[1]){
                        sum++;
                    }
                }
                vertex[k][1] = sum.ToString();
            }

            //buat dalam bentuk matriks yang setiap barisnya , misal baris i, isinya ialah teman dari orang pada buffer yang baris i
            hubungan= new string[count][];
            for(int i=0; i<count; i++){
                int jumlah = int.Parse(vertex[i][1]);
                hubungan[i]= new string[jumlah];
                int j=0;
                for(int k = 1; k <= max; k++){
                    string[] buf = raw[k].Split(' ');
                    if(buffer[i]==buf[0]){
                        hubungan[i][j]=find(buffer, buf[1]).ToString();
                        j++;
                    }else if(buffer[i]==buf[1]){
                        hubungan[i][j]=find(buffer, buf[0]).ToString();
                        j++;
                    }
                }
            }

        }
        public string[] ExploreFriendBFS(string init, string dest) {
            //lakukan pencarian lokasi init di matriks buffer
            int awal=find(buffer, init);
            int akhir = find(buffer, dest);
            
            string track;
            string[][] bangkit = new string[number][2];
            //isinya orang, dan jalur ke orang itu, orang disini berupa lokasi dalam array buffer
            bangkit[0][0]=awal.ToString();
            bangkit[0][1]="";
            int bangkitNow=1;
            bool found = false;
            int m=0;
            while(!found){
                int now =int.Parse(bangkit[m][0]) 
                for(int l=0; l<hubungan[now].Length;l++;){
                    if(int.Parse(hubungan[now][l])==akhir){
                        found = true;
                        string temp = bangkit[m][1] + "-" + now.ToString + "-" +hubungan[now][l];
                        track = temp;;
                    }else{
                        if(!exist(bangkit,hubungan[now][l])){
                            bangkit[bangkitNow][0]=hubungan[now][l];
                            string temp = bangkit[m][1] + "-" + now.ToString;
                            bangkitNow++;
                        }
                    }
                }
                m++;
            }

            string[] trackHasil = track.Split("-");
            string[] hasil;
            for(int z=0; z<trackHasil.Length;z++){
                hasil[z] = buffer[int.Parse(trackHasil[z])];
            }
            return hasil;
        }
        public int find(string[] arr, string target){
            bool found = false;
            int lokasi;
            int i=0;
            while (!found && i<arr.Length){
                if(arr[i] == target){
                    found=true;
                    lokasi=i;
                }
                i++;
            }
            return lokasi;
        }
        public bool exist(string[][] arr, string target){
            bool found = false;
            int lokasi;
            int i=0;
            while (!found && i<arr.Length){
                if(arr[i][0] == target){
                    found=true;
                }
                i++;
            }
            return found;
        }
        public string showBFS (string[] track){
            int i = track.Length;
            string result = "";
            
            if (i == 1)
            {
                result = result + x[1];
            }
            else
            {
                for (int j = 0; j < i; j++)
                {
                    if (j == i)
                    {
                        result = String.Concat(result, x[j]);
                    }
                    else
                    {
                        result = String.Concat(result, x[j], " -> ");
                    }
                }
            }
            return result;
        }
        public int length(string[] target)
        {
            int sum = 0;
            foreach(string i in target)
            {
                sum++;
            }
            return sum;
        }
    }
}