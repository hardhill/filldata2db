using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace filldata2db
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Утилита наполнения данными из файла CSV создана 21-10-2017");
            //проверка параметра
            if (args.Length == 0)
            {
                Console.Out.WriteLine("Не задан параметр <имя_файла_настроек>");
                Environment.Exit(0);
            }
            creatorsql _cr = new creatorsql(args[0]);
            if (!File.Exists(args[0]))
            {
                _cr.GenerateParam();
            }
            
            Console.Out.WriteLine("Создание файла SQL для построения БД и наполнения данными таблиц...");
            _cr.GenerateSQLFile("exportsqldata.sql");


            Console.In.Read();

        }
    }
}
