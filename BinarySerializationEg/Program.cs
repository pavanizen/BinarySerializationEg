using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace BinarySerializationEg
{
    [Serializable]
    class Person : IDeserializationCallback
    {
        public DateTime YearOfBirth, today_date;
        [NonSerialized]
        public int age;
        //public int month,days,tt;
        public Person()
        {

        }
        public Person(DateTime yob)
        {
            YearOfBirth = yob;
            today_date = DateTime.Now;
            //var dateAndTime = DateTime.Now;
            //// time_date = today_date.Date;

        }

        public void OnDeserialization(object sender)
        {
            //tt=(int)((today_date - YearOfBirth).TotalDays);
            age = (int)(today_date - YearOfBirth).TotalDays / 365;
            // month=(int)(((today_date - YearOfBirth).TotalDays-age*365)/30);
            // days=(int)((today_date - YearOfBirth).TotalDays- age*365-month*30);
        }


    }
    class Program
    {
        static void Main(string[] args)
        {


            Console.Write("Enter a day: ");
            int day = int.Parse(Console.ReadLine());
            Console.Write("Enter a month: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Enter a year: ");
            int year = int.Parse(Console.ReadLine());


            DateTime inputtedDate = new DateTime(year, month, day);
            Person p = new Person(inputtedDate);
            Console.WriteLine("Today's date:" + p.today_date);

            FileStream fs = new FileStream(@"DateTime.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, p);
            fs.Seek(0, SeekOrigin.Begin);
            Person result = (Person)bf.Deserialize(fs);
            //Console.WriteLine(result.tt);
            Console.WriteLine("Age in Years:" + result.age);
            // Console.WriteLine("Month:" +result.month+"Days:"+result.days);

            Console.ReadLine();

        }

        }
    }
