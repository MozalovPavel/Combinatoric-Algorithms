using System;
using System.Collections.Generic;
using System.IO;

namespace CombinatorialAlgorithms_2
{
    public class Result
    {
        private static readonly Dictionary<int, string> Dict = new Dictionary<int, string>
        {
            {0, "a"},
            {1, "b"},
            {2, "c"},
            {3, "d"},
            {4, "e"},
            {5, "f"},
            {6, "g"},
            {7, "h"}
        };

        private static readonly Dictionary<int, string> Visual = new Dictionary<int, string>
        {
            {0, " "},
            {1, "1"},
            {2, "2"},
            {3, "3"}
        };

        private static readonly Stack<Tuple<int, int>> Stack = new Stack<Tuple<int, int>>();
        public int[,] graph;
        public Dictionary<Tuple<int, int>, Tuple<int, int>> path = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

        public bool[,] used = new bool[8, 8];

        public bool CanMove(int x, int y)
        {
            if (x >= 0 && y >= 0 && x <= 7 && y <= 7 && graph[x, y] != 3 && (used[x, y] == false))
                return true;
            return false;
        }

        public void Visualizator()
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (!used[i, j])
                        Console.Write(Visual[graph[i, j]]);
                    else
                        Console.Write("+");
                }
                Console.WriteLine();
            }
        }

        public void Rest()
        {
            var r = new Reader();
            graph = r.Read();
            Dfs(r.ReturnCoodrHorse.Item1, r.ReturnCoodrHorse.Item2);
            Tuple<int, int> goal = r.ReturnCoodrPawn;
            Tuple<int, int> s = goal;
            var answer = new List<string>();
            while (r.ReturnCoodrHorse != s && path.ContainsKey(s))
            {
                int x = s.Item1;
                int y = s.Item2;
                answer.Add(Dict[x] + (y + 1));
                s = path[s];
            }
            answer.Add(Dict[r.ReturnCoodrHorse.Item1] + (r.ReturnCoodrHorse.Item2 + 1));
            answer.Reverse();
            File.WriteAllLines("out.txt", answer);
        }

        public bool Motion(int coordX, int coordY, Tuple<int, int> s)
        {
            if (!CanMove(coordX, coordY)) return false;
            used[coordX, coordY] = true;
            Visualizator();
            int item1 = s.Item1;
            int item2 = s.Item2;
            path.Add(new Tuple<int, int>(coordX, coordY), new Tuple<int, int>(item1, item2));
            Stack.Push(new Tuple<int, int>(coordX, coordY));
            if (graph[coordX, coordY] == 2)
                return true;
            return false;
        }

        public void Dfs(int coordXHorse, int coordYHorse)
        {
            int coordX = coordXHorse;
            int coordY = coordYHorse;
            used[coordX, coordY] = true;
            Stack.Push(new Tuple<int, int>(coordXHorse, coordYHorse));
            while (Stack.Count > 0)
            {
                Tuple<int, int> coord = Stack.Pop();
                coordX = coord.Item1;
                coordY = coord.Item2;
                if (Motion(coordX + 1, coordY + 2, coord)) break;
                if (Motion(coordX - 1, coordY + 2, coord)) break;
                if (Motion(coordX - 2, coordY + 1, coord)) break;
                if (Motion(coordX - 2, coordY - 1, coord)) break;
                if (Motion(coordX - 1, coordY - 2, coord)) break;
                if (Motion(coordX + 1, coordY - 2, coord)) break;
                if (Motion(coordX + 2, coordY - 1, coord)) break;
                if (Motion(coordX + 2, coordY + 1, coord)) break;
            }
        }
    }
}