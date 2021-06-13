using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEsSql
{
    class szemelyek
    {
        public string vnev { get; set; }
        public string knev { get; set; }
        public string szuldat { get; set; }

        public override string ToString()
        {
            var teljesneve = String.Format("{0} {1} {2}", vnev, knev, szuldat);
            // string teljesneve = vnev+" "+knev+", "+szuldat
            return teljesneve;
        }
    }
}
