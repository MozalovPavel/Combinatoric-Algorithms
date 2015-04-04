using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CombinatorialAlgorithmsN1
{
    public class Acyclic
    {
        public Acyclic (string address)
        {
            
            string[] file = File.ReadAllLines(address);
            var lengthFile =file.GetLength(0);
            for(var i =0; i < lengthFile;i++)
            {
                if (i == 0) {
                    
                    n = int.Parse(file[i].ToString());
                    father= new int[n];
                    //Console.WriteLine(n);
                }
                else
                {
                    var str = file[i].Split (new  char[]{' '});
                    var list = new List<int>();
                    foreach (var j in str)
                    {
                        var l =  int.Parse(j.ToString());
                        if (l != 0)
                        {
                            //Console.WriteLine(l);
                            list.Add(l);
                        }   
                    }
                graph.Add(list);
                }
            }
        }
        public List<List<int>> graph = new List<List<int>>();
        public List<int> visited = new List<int>();
        public int[] father;
        public int n;
        public void Result()
        {
            var c = new string[2];
            for (var i = 1; i <= graph.Count; i++)
            {
                if (!visited.Contains(i))
                {

                    var b =bfs(i);
                    c = b;
                    if (b[0] != "A")
                        break;
                }
            }
            File.WriteAllLines( "out.txt", c );
        }
        public string[] bfs(int i)
        {
            var stack = new Stack<int>();
            var queue = new Queue<int>();
            queue.Enqueue(i);
            while (queue.Count > 0)
            {
                var j = queue.Dequeue();
                visited.Add(j);
                var notQueueFromNotFather = new List<int>();
                notQueueFromNotFather = graph[j - 1].Where(x => x != father[j - 1]).ToList()
                    .Where(x => !queue.Contains(x)).ToList()
                    .Where(x => visited.Contains(x)).ToList();
                if (notQueueFromNotFather.Count == 0)
                {
                    foreach (var e in graph[j - 1])
                    {
                        if (!visited.Contains(e))
                        {
                            //Console.WriteLine(e);
                            father[e - 1] = j;
                            if (!queue.Contains(e))
                            {
                                //Console.WriteLine(e);
                                queue.Enqueue(e);
                            }
                        }
                    }
                    stack.Push(j);
                    
                    //Console.WriteLine(stack.Count);
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
                    var str2 = "";
                    foreach (var l in str)
                        str2 += l.ToString() + " ";
                    return new string[] { "N", str2};
                }
                    //return new string[] { "N", StackToString(stack, j) };

            }
            return new string[] { "A" };
        }
        //public string StackToString(Stack<int> stack, int j)
        //{
            
        //}

    }
}
