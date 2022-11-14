using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    internal class Class1
    {
        int a = 10;

        public Class1(int value)
        {
            this.a = value;
        }

        public int A
        {
            set { this.a = value; }
            get { return this.a; }
        }
        public void show()
        {
            Console.WriteLine(a);
        }
    }
}
