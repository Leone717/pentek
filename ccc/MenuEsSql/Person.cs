using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuEsSql
{
    class Person
    {
        public Person()
        {
            int Id = new Random().Next(1, 1000);//Azonosító létrehozása
        } 

    public int Id {get; set; }

    public String nev{ get; set; }

    public String adoszam {get; set; }

    public override string ToString()
    {
        var reszletek = String.Format("{0} - {1} {2}", Id, nev, adoszam);
        return reszletek;
    }
    }
}
