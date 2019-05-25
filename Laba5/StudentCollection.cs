using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba5
{
    class StudentCollection : EventArgs
    {
        public List<Student> StudentInfo = new List<Student>();
        public string CollectionName { get; set; }

        //EVENTS
        public delegate void StudentListHandler(object source, StudentListHandlerEventArgs args);
        public event StudentListHandler StudentCountChanged;
        public event StudentListHandler StudentReferenceChanged;

        public void AddDefaults(int n)
        {
            StudentInfo.Add(TestCollections.GenerateInfo(n));
            StudentListHandlerEventArgs obj = new StudentListHandlerEventArgs("StudentInfo", "Добавлено новий елемент", StudentInfo.Last());
            //Console.WriteLine(obj);
            if (StudentCountChanged != null)
                StudentCountChanged(StudentInfo.Last(), obj);   //Need to think
        }

        public void AddStudents(params Student[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                StudentInfo.Add(input[i]);

                StudentListHandlerEventArgs obj = new StudentListHandlerEventArgs("StudentInfo", "Добавлено новий елемент", StudentInfo.Last());
                if (StudentCountChanged != null)
                    StudentCountChanged(StudentInfo.Last(), obj);   //Need to think
            }
        }

        public bool Remove(int j)
        {
            if (j < StudentInfo.Count || j > 0)
            {
                StudentListHandlerEventArgs obj = new StudentListHandlerEventArgs("StudentInfo", "Видалено " + j + " елемент", StudentInfo[j]);
                if (StudentCountChanged != null)
                    StudentCountChanged(StudentInfo[j], obj);   //Need to think

                StudentInfo.RemoveAt(j);
                return true;
            }
            else
                return false;
        }

        public Student this[int j]  //Need to think
        {
            get
            {
                return StudentInfo[j];
            }
            set
            {
                StudentListHandlerEventArgs obj = new StudentListHandlerEventArgs("StudentInfo", "Змiнено " + j + " елемент", StudentInfo[j]);
                if (StudentReferenceChanged != null)
                    StudentReferenceChanged(StudentInfo[j], obj);

                StudentInfo[j] = value;
            }
        }

        public void SortSurname()
        {
            StudentInfo.Sort();// (a, b) => a.CompareTo(b)); //need to think
        }

        public void SortDate()
        {
            StudentInfo.Sort((a, b) => a.Compare(a, b));    //need to think
        }

        public void SortMark()
        {
            Student.Support U = new Student.Support();
            StudentInfo.Sort((a, b) => U.Compare(a, b));    //need to think
        }

        public double MaxMark  //Find Max MediumMark in List
        {
            get
            {
                return StudentInfo.Max(a => a.MediumMark);
            }
        }

        public IEnumerable<Student> EducationForm()
        {
            return StudentInfo.Where(x => x.FormInfo == Person.Education.Master);
        }

        public List<Student> AverageMarkGroup(double value)
        {
            return StudentInfo.GroupBy(p => p.MediumMark == 4.5).Where(p => p.Key).First().ToList();
        }

        public override string ToString()
        {
            //return base.ToString();
            foreach (Student v in StudentInfo)
                Console.WriteLine(v + Environment.NewLine);
            return "";
        }

        public string ToShortString()
        {
            foreach (Student v in StudentInfo)
                Console.WriteLine(v.ToShortString() + Environment.NewLine);
            return "";
        }
    }
}
