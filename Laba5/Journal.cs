using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba5
{
    class Journal
    {
        private List<JournalEntry> JournalCollection = new List<JournalEntry>();

        public void StudentCountChanged()
        {
            StudentListHandlerEventArgs G = new StudentListHandlerEventArgs();
            JournalCollection.Add(new JournalEntry("JournalCollection ", "Додано новий елемент ", G.S));
            G = new StudentListHandlerEventArgs("JournalCollection ", "Додано новий елемент ", G.S);
            Console.WriteLine(G);
            // T.StudentCountChanged(JournalCollection.Last(), G);
        }

        public void StudentReferenceChanged()
        {
            StudentListHandlerEventArgs G = new StudentListHandlerEventArgs();
            JournalCollection.Add(new JournalEntry("JournalCollection ", "Додано новий елемент ", G.S));
            G = new StudentListHandlerEventArgs("JournalCollection ", "Додано новий елемент ", G.S);
            Console.WriteLine(G);
        }

        public override string ToString()
        {
            for (int i = 0; i < JournalCollection.Count; i++)
                Console.WriteLine(JournalCollection[i]);
            return "";
        }
    }
}
