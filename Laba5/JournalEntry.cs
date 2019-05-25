using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba5
{
    class JournalEntry
    {
        public string CollectionName { get; set; }
        public string CollectionChanges { get; set; }
        public string ChangesInfo { get; set; }

        public JournalEntry()
        {
            CollectionName = "Default";
            CollectionChanges = "Default";
            ChangesInfo = "Default";
        }

        public JournalEntry(string name, string changes, Student info)
        {
            CollectionName = name;
            CollectionChanges = changes;
            //ChangesInfo = info.ToString();
        }

        public override string ToString()
        {
            Console.WriteLine(CollectionName + " " + CollectionChanges);
            return base.ToString();
        }
    }
}
