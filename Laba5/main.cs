using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Laba5
{
    class main
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Student T = new Student();
            Person J = new Person();
            Console.WriteLine("Введіть ім'я файлу: ");
            string filename = Console.ReadLine();

            T.AddExam(new Exam("net", 5, new DateTime(2019/10/15)), new Exam("D6", 5, new DateTime(2019/11/12)));
            Console.WriteLine(T);

            if (File.Exists(filename))
                T.Load(filename);
            else
            {
                Console.WriteLine("Файлу " + filename + " не існує. Його буде створенно автоматично");
                File.Create(filename);
            }

            //Student.DeepCopy(T);

            Console.WriteLine(T);
            T.AddFromConsole();
            T.Save(filename);
            Console.WriteLine(T);

            Student.LoadNew(filename, T.AccessExam);
            T.AddFromConsole();
            Student.SaveNew(filename, T.AccessExam);
            Console.WriteLine(T);

            Console.ReadKey();
        }
    }
}
