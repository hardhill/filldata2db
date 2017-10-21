using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


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
                _param.Table = @"CREATE TABLE 'CS'.'PROCESSES' ";
                _param.Delimeter = ";";
                _param.Fields.Add("ID_PROCESS");
                _param.Types.Add("INT");
                using (StreamWriter file = File.CreateText(@"schema.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, _param);
                }
            }
        }

        public void GenerateSQLFile(string fname)
        {
            if (File.Exists(_param.FileCSV))
            {
                //создаем файл SQL
                using (StreamWriter fs = new StreamWriter(fname, false, Encoding.UTF8))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("BEGIN");
                    sb.AppendLine("IF EXISTS(SELECT PROCESSES FROM syscat.tables WHERE tabschema='DB2ADMIN' and tabname='PROCESSES') THEN");
                    sb.AppendLine("DROP TABLE DB2ADMIN.PROCESSES;");
                    sb.AppendLine("END IF;");
                    sb.AppendLine("END;");
                    sb.AppendLine("CREATE TABLE DB2ADMIN.PROCESSES(ID_PROCESS BIGINT NOT NULL,");
                    sb.AppendLine("DATEOFCOMMING TIMESTAMP NOT NULL,");
                    sb.AppendLine("DATEOFCOMPLETION TIMESTAMP,");
                    sb.AppendLine("ID_STATUS SMALLINT NOT NULL,");
                    sb.AppendLine("ID_TYPE_PROCESS SMALLINT NOT NULL,");
                    sb.AppendLine("ID_DEPARTMENT SMALLINT NOT NULL,");
                    sb.AppendLine("PRIMARY KEY(ID_PROCESS)) IN SYSCATSPACE;");
                    sb.AppendLine("");
                    sb.AppendLine("");
                    sb.AppendLine("");
                          
                    fs.WriteLine(sb.ToString());
                }
            }
        }

    }
}
