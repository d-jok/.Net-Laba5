using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Laba5
{
    [Serializable]
    class Student: Person, IDateAndCopy
    {
        public Education FormInfo { get; set; }
        private int GroupNumber { get; set; }

        private List<Test> Credit = new List<Test>();
        private List<Exam> ListOfExam = new List<Exam>();    //private Exam ListOfExam
        private Person person;

        public Student(string newName, string newSurname, DateTime Date, Education form, int group, List<Test> test, List<Exam> exam) : base(newName, newSurname, Date)
        {
            FormInfo = form;
            GroupNumber = group;
            Credit = test;
            ListOfExam = exam;
        }

        public Student(string newName, string newSurname, DateTime Date, Education form, int group) : base(newName, newSurname, Date)
        {
            FormInfo = form;
            GroupNumber = group;
        }

        public static Student DeepCopy<Student>(Student SerializableObject)   //SERIALIZE
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, SerializableObject);
            stream.Seek(0, SeekOrigin.Begin);

            return (Student)formatter.Deserialize(stream);
        }

        public bool Save(string Filename)
        {
            FileStream SaveInFile = File.OpenWrite(Filename);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(SaveInFile, ListOfExam);
                return true;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                SaveInFile.Close();
            }
        }

        public bool Load(string Filename)
        {
            FileStream LoadFromFile = File.OpenRead(Filename);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                ListOfExam = (List<Exam>)formatter.Deserialize(LoadFromFile);
                return true;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                LoadFromFile.Close();
            }
        }

        public static bool Save(string Filename, Student Object)
        {
            FileStream SaveInFile = File.OpenWrite(Filename);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(SaveInFile, Object);
                return true;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                SaveInFile.Close();
            }
        }

        public static bool Load(string Filename, Student Object)
        {
            FileStream LoadFromFile = File.OpenRead(Filename);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Object = (Student)formatter.Deserialize(LoadFromFile);
                return true;
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                return false;
            }
            finally
            {
                LoadFromFile.Close();
            }
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Список екзаменів: \nВведіть через кому: назву предмету, оцінку та дату іспиту (Рік.Місяць.День)");

            string[] words = Console.ReadLine().Split(',');
            string Name;
            int Total;
            DateTime Date;

            //Name
            Name = words[0];

            //Total
            try
            {
                Total = System.Convert.ToInt32(words[1]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Mark - OK");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введіть коректну оцінку");
                Console.ForegroundColor = ConsoleColor.White;
                AddFromConsole();
            }

            //Date
            try
            {
                Date = Convert.ToDateTime(words[2]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Date - OK");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введіть коректну дату");
                Console.ForegroundColor = ConsoleColor.White;
                AddFromConsole();
            }

            //ListOfExam.Add(new Exam(Name, Total, Date));
            return true;            
        }

        public Student(): base()
        {
            FormInfo = Education.Bachelor;
            GroupNumber = 301;
        }

        public double MediumMark
        {
            get
            {
                double Value = 0;

                if (ListOfExam == null) return 0;
                else
                {
                    for (int i = 0; i < ListOfExam.Count; i++)
                    {
                        Value += ListOfExam[i].Mark;
                    }
                    Value = Value / ListOfExam.Count;
                    return Value;
                }
            }
        }

        public class Support : IComparer<Student>  //додатковий клас
        {
            public int Compare(Student x, Student y)
            {
                return x.MediumMark.CompareTo(y.MediumMark);
            }
        }

        public Student(Person person)
        {
            this.person = person;
        }

        public object DeepCopy()
        {
            Student J = new Student(this.Name, this.Surname, this.DateOfBirth, FormInfo, GroupNumber, AccessCredit, AccessExam);
            return J;
        }



        public List<Test> AccessCredit
        {
            get
            {
                return Credit;
            }
            set
            {
                Credit = value;
            }
        }

        public List<Exam> AccessExam
        {
            get
            {
                return ListOfExam;
            }
            set
            {
                ListOfExam = value;
            }
        }

        public Person Info
        {
            get
            {
                return new Person(Name, Surname, DateOfBirth);
            }
            set
            {
                Student R = new Student(value.DeepCopy() as Person);
                Name = R.Name;
                Surname = R.Surname;
                DateOfBirth = R.DateOfBirth;
            }
        }

        public int AccessGroupNumber
        {
            get
            {
                return GroupNumber;
            }
            set
            {
                
                    if (value > 100 && value < 699)
                        GroupNumber = value;
                    else
                        throw new ArgumentOutOfRangeException("Error", "Group Number must be > 100 and < 699");
               
            }
        }

        public bool this[Education Check]
        {
            get
            {
                return Check == FormInfo;
            }
        }

        public IEnumerable<Exam> ExamBiggerThan(int Grade)
        {
            foreach(var v in ListOfExam)
            {
                if (v.Mark > Grade)
                {
                    yield return v;
                }
            }
        }

        public IEnumerable<object> ExamsAndTests()
        {
            foreach (var v in Credit)
                yield return v;

            foreach (var v in ListOfExam)
                yield return v;
        }

        public void AddExam(params Exam[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                ListOfExam.Add(input[i]);
            }            
        }

        public void AddCredit(params Test[] input)
        {
            for (int i = 0; i < input.Length; i++)
                Credit.Add(input[i]);
        }

        public override string ToString()
        {
            if (ListOfExam == null) return base.ToString() + " " + FormInfo + " " + GroupNumber + " ";
            else
            {
                Console.WriteLine("Exams: ");
                for (int i = 0; i < ListOfExam.Count; i++)
                {
                    Console.WriteLine(ListOfExam[i]);
                }
                Console.WriteLine("Tests: ");

                for (int i = 0; i < Credit.Count; i++)
                {                    
                    Console.WriteLine(Credit[i]);
                }
                return base.ToString() + " " + FormInfo + " " + GroupNumber + " ";
            }
        }

        public string ToShortString()
        {
            return base.ToString() + " " + FormInfo + " " + GroupNumber + " " + "Середнiй бал: " + MediumMark;  //+ T.ToShortString()
        }

    }
}
