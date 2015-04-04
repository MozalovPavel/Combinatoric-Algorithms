using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace ка_2
{
    public class Result
    {
        private static Dictionary<int, string> dict = new Dictionary<int, string>() {
            {0 ,"a"},
            {1,"b"},
            {2,"c"},
            {3,"d"},
            {4,"e"},
            {5,"f"},
            {6,"g"},
            {7,"h"}
        };
        public Dictionary<Tuple<int, int>, Tuple<int,int>> path = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
        public bool[,] used = new bool[8, 8];
        public int[,] graph;
        public bool CanMove(int x, int y)
        {
            if ( x >= 0 && y >= 0 && x <= 7 && y <= 7 && graph[x, y] != 3 && (used[x,y] == false))
                return true;
            return false;
        }
        public static Dictionary<int, string> visual = new Dictionary<int, string>()
        {
            {0, " "},
            {1, "1"},
            {2, "2"},
            {3, "3"}
        };
        public void Visualizator()
        {
            //Thread.Sleep(500);
            for (var i=0; i< graph.GetLength(0);i++)
            {
                for (var j = 0; j < graph.GetLength(1); j++)
                {
                    if (!used[i, j])
                        Console.Write(visual[graph[i, j]]);
                    else
                        Console.Write("+");
                }
                Console.WriteLine();
            }
        }
        private static Stack<Tuple<int,int>> stack =  new Stack<Tuple<int,int>>();
        public void Rest()
        {
            Reader r = new Reader();
            graph = r.Read();
            Dfs(r.ReturnCoodrHorse.Item1, r.ReturnCoodrHorse.Item2);
            var goal = r.ReturnCoodrPawn;
            Tuple<int,int> s = goal;
            List<string> answer = new List<string>();
            while(r.ReturnCoodrHorse != s && path.ContainsKey(s))
            //foreach(var e in path)
            {
                
                var x = s.Item1;
                var y = s.Item2;
                //Console.WriteLine(dict[x] + (y+1).ToString());
                answer.Add(dict[x] + (y + 1).ToString());
                //Console.WriteLine(e);
                s  = path[s];
            }
            answer.Add(dict[r.ReturnCoodrHorse.Item1] + (r.ReturnCoodrHorse.Item2 + 1).ToString());
            answer.Reverse();
            string[] str = new string[answer.Count];
            //for(var i=0;i<answer.Count;i++)
            //    str[i] = answer[i];
            File.WriteAllLines("out.txt", answer);
        }
        public bool Motion(int coordX, int coordY, Tuple<int, int> s)
        {
            if (CanMove(coordX, coordY))
            {
                used[coordX, coordY] = true;
                Visualizator();
                var item1 = s.Item1;
                var item2 = s.Item2;
                path.Add(new Tuple<int, int>(coordX, coordY), new Tuple<int, int>(item1, item2));
                stack.Push(new Tuple<int,int>(coordX, coordY));
                if (graph[coordX, coordY] == 2)
                    return true;
            }
            return false;
        }
        public void Dfs(int coordXHorse, int coordYHorse)
        {
            var coord = new Tuple<int, int>(0,0);
            int coordX = coordXHorse;
            int coordY = coordYHorse;
            used[coordX, coordY] = true;
            stack.Push(new Tuple<int, int>(coordXHorse, coordYHorse));
            while (stack.Count > 0)
            {
                coord = stack.Pop();
                coordX = coord.Item1;
                coordY = coord.Item2;
                if(Motion(coordX + 1, coordY + 2, coord)) break;
                if(Motion(coordX - 1, coordY + 2,coord))break;
                if(Motion(coordX - 2, coordY + 1,coord))break;
                if(Motion(coordX - 2, coordY - 1,coord))break;
                if(Motion(coordX - 1, coordY - 2,coord))break;
                if(Motion(coordX + 1, coordY - 2,coord))break;
                if(Motion(coordX + 2, coordY - 1,coord))break;
                if (Motion(coordX + 2, coordY + 1,coord)) break;
            }
            //if (Motion(coordXHorse + 1, coordYHorse + 2)) return "";
            //if (Motion(coordXHorse - 1, coordYHorse + 2)) return "";
            //if (Motion(coordXHorse - 2, coordYHorse + 1)) return "";
            //if (Motion(coordXHorse - 2, coordYHorse - 1)) return "";
            //if (Motion(coordXHorse - 1, coordYHorse - 2)) return "";
            //if (Motion(coordXHorse + 1, coordYHorse - 2)) return "";
            //if (Motion(coordXHorse + 1, coordYHorse - 1)) return "";
            //if (Motion(coordXHorse + 2, coordYHorse + 1)) return "";
            //path.Remove(path[path.Count - 1]);
            //return "";
        }
    }
}
