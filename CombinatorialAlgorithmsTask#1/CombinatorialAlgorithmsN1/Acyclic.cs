using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CombinatorialAlgorithmsN1
{
    public class Acyclic
    {
        public Acyclic(string address)
        {
            string[] file = File.ReadAllLines(address);
            var lengthFile = file.GetLength(0);
            for (var i = 0; i < lengthFile; i++)
            {
                if (i == 0)
                {
                    int n = int.Parse(file[i]);
                    father = new int[n];
                }
                else
                {
                    var str = file[i].Split(new[] {' '});
                    var list = str.Select(int.Parse).Where(l => l != 0).ToList();
                    graph.Add(list);
                }
            }
        }

        private readonly List<List<int>> graph = new List<List<int>>();
        private readonly List<int> visited = new List<int>();
        private readonly int[] father;

        public void Result()
        {
            var c = new string[2];
            for (var i = 1; i <= graph.Count; i++)
            {
                if (visited.Contains(i)) continue;
                var b = BFS(i);
                c = b;
                if (b[0] != "A")
                    break;
            }
            File.WriteAllLines("out.txt", c);
        }

        private string[] BFS(int i)
        {
            var stack = new Stack<int>();
            var queue = new Queue<int>();
            queue.Enqueue(i);
            while (queue.Count > 0)
            {
                var j = queue.Dequeue();
                visited.Add(j);
                List<int> notQueueFromNotFather = graph[j - 1]
                    .Where(x => x != father[j - 1])
                    .Where(x => !queue.Contains(x))
                    .Where(x => visited.Contains(x))
                    .ToList();
                if (notQueueFromNotFather.Count == 0)
                {
                    foreach (var e in graph[j - 1])
                    {
                        if (!visited.Contains(e))
                        {
                            father[e - 1] = j;
                            if (!queue.Contains(e))
                            {
                                queue.Enqueue(e);
                            }
                        }
                    }
                    stack.Push(j);
                }
                else
                {
                    var str = new List<int>();
                    var s = stack.Pop();
                    while (stack.Count != 0)
                    {
                        str.Add(s);
                        s = stack.Pop();
                    }
                    str.Add(s);
                    str.Add(j);
                    str.Sort();
                    var str2 = str.Aggregate("", (current, l) => current + (l + " "));
                    return new[] {"N", str2};
                }
            }
            return new[] {"A"};
        }
    }
}
