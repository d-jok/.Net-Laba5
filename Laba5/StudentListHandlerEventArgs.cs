using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba5
{
    class StudentListHandlerEventArgs : System.EventArgs
    {
        public string CollectionName { get; set; }
        public string CollectionChanges { get; set; }
        public Student S { get; set; }

        public StudentListHandlerEventArgs()
        {

        }

        public StudentListHandlerEventArgs(string name, string change, Student g)
        {
            CollectionName = name;
            CollectionChanges = change;
            S = g;
        }

        public override string ToString()
        {
            Console.WriteLine("CollectionName: " + CollectionName + "\n" + "CollectionChanges: " + CollectionChanges);
            return "";
        }

    }
}
