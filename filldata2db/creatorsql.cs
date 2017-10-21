using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace filldata2db
{
    class creatorsql
    {
        private parameters _param;
        public creatorsql(string jsonfile)
        {
            _param = new parameters();
            if (File.Exists(jsonfile))
            {
                // deserialize JSON directly from a file
                using (StreamReader file = File.OpenText(jsonfile))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _param = (parameters)serializer.Deserialize(file, typeof(parameters));
                    Console.Out.WriteLine("Прочитан файл " + jsonfile);
                }
            }
        }
        //создание файла параметров для примера
        public void GenerateParam()
        {
            if (_param != null)
            {
                Console.WriteLine("Генерация шаблона файла схемы \"schema.json\"");
                _param.FileCSV = "input.csv";
                _param.Table = "DB2ADMIN.PROCESSES";
                _param.Delimeter = ";";
                _param.Create = "CREATE TABLE DB2ADMIN.PROCESSES(ID_PROCESS BIGINT NOT NULL,DATEOFCOMMING TIMESTAMP NOT NULL,DATEOFCOMPLETION TIMESTAMP,ID_STATUS SMALLINT NOT NULL,ID_TYPE_PROCESS SMALLINT NOT NULL,ID_DEPARTMENT SMALLINT NOT NULL, PRIMARY KEY(ID_PROCESS)); ";
                _param.Fields="INSERT INTO PROCESSES (ID_PROCESS, DATEOFCOMMING, DATEOFCOMPLETION, ID_STATUS, ID_TYPE_PROCESS, ID_DEPARTMENT)";
                _param.Values ="VALUES({0},'{1}','{2}',{3},{4},{5});";
                using (StreamWriter file = File.CreateText(@"schema.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, _param);
                }
            }
        }

        public void GenerateSQLFile(string fname)
        {
            //var re = new Regex("#.*?;");
            if (File.Exists(_param.FileCSV))
            {
                //создаем файл SQL
                using (StreamWriter fs = new StreamWriter(fname, false, Encoding.UTF8))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("DROP TABLE "+_param.Table+";");
                    sb.AppendLine(_param.Create);
                    //sb.AppendLine("CREATE TABLE DB2ADMIN.PROCESSES(ID_PROCESS BIGINT NOT NULL,");
                    //sb.AppendLine("DATEOFCOMMING TIMESTAMP NOT NULL,");
                    //sb.AppendLine("DATEOFCOMPLETION TIMESTAMP,");
                    //sb.AppendLine("ID_STATUS SMALLINT NOT NULL,");
                    //sb.AppendLine("ID_TYPE_PROCESS SMALLINT NOT NULL,");
                    //sb.AppendLine("ID_DEPARTMENT SMALLINT NOT NULL,PRIMARY KEY(ID_PROCESS));");
                    sb.AppendLine("");
                    using (StreamReader fr = new StreamReader(_param.FileCSV))
                    {
                        while (fr.Peek() >= 0)
                        {
                            string sline = fr.ReadLine();
                            string[] arrFields = sline.Split(_param.Delimeter[0]);
                            sb.AppendLine(_param.Fields);
                            sline = String.Format(_param.Values, arrFields);
                            sline = sline.Replace("\"", "");
                            sb.AppendLine(sline);
                        }
                        
                        sb.AppendLine("");
                    }
                    fs.WriteLine(sb.ToString());
                }
                Console.WriteLine("Создание SQL файла завершено.");
            }
            else
            {
                Console.WriteLine("Нет файла указанного в файле параметров программы.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

    }
}
