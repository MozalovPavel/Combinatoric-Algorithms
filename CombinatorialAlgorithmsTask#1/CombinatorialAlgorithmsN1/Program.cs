using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinatorialAlgorithmsN1
{
    class Program
    {
        static void Main(string[] args)
        {
            var acicl = new Acyclic("in.txt");
            acicl.Result();
        }
    }
}
