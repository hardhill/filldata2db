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
                }
            }
        }
        //создание файла параметров для примера
        public void GenerateParam()
        {
            if (_param != null)
            {
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
    }
}
