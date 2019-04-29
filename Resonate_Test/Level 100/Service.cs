using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resonate_Test.Level_100
{
    class Service
    {

        private Stack<int> _list = new Stack<int>();


        public Stack<int> RemoveDuplicate(Stack<int> list)
        {
            var v = list.Pop();

            if (!list.Contains(v))
                _list.Push(v);

            return list.Count == 0 ? _list : RemoveDuplicate(list);
        }


        
        public bool Permutation(string s1, string s2)
        {
            var stack = new Stack<char>(s1.ToCharArray());
            var queue = new Queue<char>(s2.ToCharArray());
            if (stack.Count != queue.Count)
                return false;

            var length = queue.Count;

            while (stack.Count > 0)
            {
                var s = stack.Pop();

                for (int i = 0; i < length; i++)
                {
                    var q = queue.Dequeue();

                    if (q == s) break;
                    else queue.Enqueue(q);
                }
            }

            return queue.Count > 0 ? false : true;

        }



    }
}
