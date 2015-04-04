using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ка_2
{
    public class Reader
    {
        private static Dictionary<string, int> dict = new Dictionary<string,int>() {
            {"a", 0},
            {"b", 1},
            {"c", 2},
            {"d", 3},
            {"e", 4},
            {"f", 5},
            {"g", 6},
            {"h", 7}
        };
        private Tuple<int, int> pawn;
        private Tuple<int, int> hourse;
        //public int[,] graph = new int[8,8];
        public Tuple<int, int> ReturnCoodrHorse { get {return hourse; }}
        public Tuple<int, int> ReturnCoodrPawn { get { return pawn; } }
        public int[,] Read()
        {
            int[,] graph = new int[8,8];
            for (var i = 0; i < graph.GetLength(0); i++)
                for (var j = 0; j < graph.GetLength(1); j++)
                    graph[i, j] = 0;
                
            string[] readMap = File.ReadAllLines("in.txt");
            int coordXHorse = dict[readMap[0][0].ToString()];
            int coordYHorse = int.Parse(readMap[0][1].ToString()) - 1;
            int coordXPawn = dict[(readMap[1][0].ToString())];
            hourse = new Tuple<int, int>(coordXHorse, coordYHorse);
            int coordYPawn = int.Parse(readMap[1][1].ToString()) - 1;
            pawn = new Tuple<int, int>(coordXPawn, coordYPawn);
            graph[coordXHorse, coordYHorse] = 1;
            graph[coordXPawn, coordYPawn] = 2;
            if((coordXPawn - 1 >= 0) && (coordYPawn - 1 >= 0))
                graph[coordXPawn - 1, coordYPawn - 1] = 3;
            if((coordXPawn + 1  <= 7) && (coordYPawn - 1 >= 0))
                graph[coordXPawn + 1, coordYPawn - 1] = 3;
            return graph;
        }
    }
}
