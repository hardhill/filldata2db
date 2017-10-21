using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace filldata2db
{
    
    class parameters
    {
        private List<string> _fields;
        private List<string> _types;

        public parameters()
        {
            _fields = new List<string>();
            _types = new List<string>();
        }
        public string FileCSV { get; set; }
        public string Delimeter { get; set; }
        public string Create { get; set; }
        public string Table { get; set; }
        public List<string> Fields { get { return _fields; } set { this._fields = value;} }
        public List<string> Types { get { return _types; } set { this._types = value; } }

    }
}
