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
            string[] buffer2 = new string[int.Parse(raw[0])*2];
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
                    if(buffer2[j] == buf[0])
                    {
                        found1 = true;
                    }
                    if(buffer2[j] == buf[1])
                    {
                        found2 = true;
                    }
                    j++;
                }
                if (!found1)
                {
                    buffer2[count] = buf[0];
                    count++;
                }
                if (!found2)
                {
                    buffer2[count] = buf[1];
                    count++;
                }
            }

            number = count;
            string[] buffer21 = new string[number];
            for (int i=0; i<number; i++)
            {
                buffer21[i] = buffer2[i];
            }
            buffer = SortStringList(buffer21);
            //urutkan array tersebut
            /*
            int length = buffer.Length;
            int offset;
            string temp;
            for (int i = 0; i < length; i++){
                for(int j = i+1; j < length; j++){
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
            */
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
                        hubungan[i][j]=Find(buffer, buf[1]).ToString();
                        j++;
                    }else if(buffer[i]==buf[1]){
                        hubungan[i][j]=Find(buffer, buf[0]).ToString();
                        j++;
                    }
                }
            }

        }
        public string[] ExploreFriendBFS(string init, string dest) {
            //lakukan pencarian lokasi init di matriks buffer
            int awal=Find(buffer, init);
            int akhir = Find(buffer, dest);
            
            string track ="";
            string[][] bangkit = new string[number][];
            //isinya orang, dan jalur ke orang itu, orang disini berupa lokasi dalam array buffer
            bangkit[0] = new string[2];
            bangkit[0][0]=awal.ToString();
            bangkit[0][1]="";
            int bangkitNow=1;
            bool found = false;
            int m=0;
            while(!found && bangkit[m] != null){
                int now = int.Parse(bangkit[m][0]);
                for(int l=0; l<hubungan[now].Length;l++){
                    if(int.Parse(hubungan[now][l])==akhir){
                        found = true;
                        string temp = bangkit[m][1] + "-" + now.ToString() + "-" +hubungan[now][l];
                        track += temp;
                    }else{
                        if(!Exist(bangkit,hubungan[now][l], bangkitNow)){
                            bangkit[bangkitNow] = new string[2];
                            bangkit[bangkitNow][0]=hubungan[now][l];
                            string temp = bangkit[m][1] + "-" + now.ToString();
                            bangkit[bangkitNow][1] = temp;
                            bangkitNow++;
                        }
                    }
                }
                m++;
                
            }
            if(found){
                string[] trackHasil = track.Split('-');
                string[] hasil = new string[trackHasil.Length-1];
                for(int z=1; z<trackHasil.Length;z++){
                    if (trackHasil[z] != "")
                    {
                        int x = int.Parse(trackHasil[z]);
                        hasil[z-1] = buffer[x];
                    }
                    
                }
                return hasil;
            }else{
                string[] hasil = new string[1];
                hasil[0]="not_found";
                return hasil;
            }
            
        }
        public int Find(string[] arr, string target){
            bool found = false;
            int lokasi = 0;
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
        public bool Exist(string[][] arr, string target, int akhir){
            bool found = false;
            int i=0;
            while (!found && i<akhir){
                if(arr[i][0] == target){
                    found=true;
                }
                i++;
            }
            return found;
        }
        public string ShowBFS (string[] track){
            int i = track.Length;
            string result = "(";

            if (i == 1){
                result = result + track[1];
            }
            else{
                for (int j = 0; j < i; j++){
                    if (j == i-1){
                        result = String.Concat(result, track[j], ", ",i-2," Degree)" );
                    }
                    else
                    {
                        result = String.Concat(result, track[j], " -> ");
                    }
                }
            }
            return result;
        }
        public int Length(string[] target)
        {
            int sum = 0;
            foreach(string i in target)
            {
                sum++;
            }
            return sum;
        }
        public string[] SortStringList(string[] buffer2)
        {
            int length = buffer2.Length;
            int offset;
            string temp;
            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    offset = 0;
                    if (buffer2[i][offset] > buffer2[j][offset])
                    {
                        temp = buffer2[i];
                        buffer2[i] = buffer2[j];
                        buffer2[j] = temp;
                    }
                    if (buffer2[i][offset] == buffer2[j][offset])
                    {
                        offset++;
                        bool done = false;
                        while (offset < buffer2[j].Length && offset < buffer2[i].Length && !done)
                        {
                            if (buffer2[i][offset] > buffer2[j][offset])
                            {
                                temp = buffer2[i];
                                buffer2[i] = buffer2[j];
                                buffer2[j] = temp;
                                done = true;
                            }
                            else
                            {
                                offset++;
                            }
                        }
                    }
                }
            }
            return buffer2;
        }
    }
}