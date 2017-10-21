using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace filldata2db
{
    class Program
    {
        static void Main(string[] args)
        {
            //проверка параметра
            if (args.Length == 0)
            {
                Console.Out.WriteLine("Не задан параметр <имя_файла_настроек>");
                Environment.Exit(0);
            }
            Console.Out.WriteLine("Создание файла SQL для построения БД и наполнения данными таблиц...");
            Console.Out.WriteLine("Прочитан файл "+args[0]);
            creatorsql _cr = new creatorsql(args[0]);
            _cr.GenerateParam();
            Console.In.Read();
        }
    }
}
