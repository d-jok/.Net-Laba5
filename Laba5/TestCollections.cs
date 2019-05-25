using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba5
{
    class TestCollections : Dictionary<Person, Student> //need to think
    {
        public List<Person> PersonInfo = new List<Person>();
        public List<string> text = new List<string>();
        public Dictionary<Person, Student> keyValues = new Dictionary<Person, Student>();
        public Dictionary<string, Student> pairs = new Dictionary<string, Student>();

        public TestCollections(int n)
        {
            for(int i = 0; i < n; i++)
            {
                PersonInfo.Add(GenerateInfo(i));
                text.Add(PersonInfo[i].ToString());
                keyValues.Add(PersonInfo[i], GenerateInfo(i));
                pairs[text[i]] = keyValues[PersonInfo[i]];
            }
        }

        public static Student GenerateInfo(int n)
        {
            Student T = new Student();
            var test = new List<Test>();
            var exam = new List<Exam>();
            Person.Education Ed = Person.Education.Bachelor;
            switch(n % 3)
            {
                case 0:
                    Ed = Person.Education.Bachelor;
                    break;
                case 1:
                    Ed = Person.Education.Master;
                    break;
                case 2:
                    Ed = Person.Education.SecondEducation;
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                test.Add(new Test("Test" + i, true));
                exam.Add(new Exam("Exam" + i, i*i % 5, new DateTime(2015 + i % 40, 1 + i % 12, 1 + i % 27)));                
            }

            return new Student("Student" + n, "Surname" + n, new DateTime(2000 + n % 20, 1 + n % 12, 1 + n % 28), Ed, 301, test, exam);
        }

        public Dictionary<string,int> GetTime(Student search)
        {
            var time = new Dictionary<string, int>();
            int n = PersonInfo.Count;
            long starTime = DateTime.UtcNow.Ticks;
            PersonInfo.Contains(search);
            time.Add("PersonInfo: ", (int)(DateTime.UtcNow.Ticks - starTime));

            starTime = DateTime.UtcNow.Ticks;
            text.Contains(search.ToString());
            time.Add("text: ", (int)(DateTime.UtcNow.Ticks - starTime));

            starTime = DateTime.UtcNow.Ticks;
            keyValues.ContainsKey(search);
            time.Add("keyValue: ", (int)(DateTime.UtcNow.Ticks - starTime));

            starTime = DateTime.UtcNow.Ticks;
            pairs.ContainsKey(search.ToString());
            time.Add("pairs: ", (int)(DateTime.UtcNow.Ticks - starTime));

            return time;
        }
    }
}
