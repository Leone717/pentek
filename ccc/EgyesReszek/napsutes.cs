using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyesReszek
    {
    class napsutes
        {
        private int evszam;
        private double osszes_napsutes;
        private double napilegtobb;
        private DateTime legtobb_datum;
        private int osszes_nap;
        private int legtobbnap_egymasutan;

        public int Evszam
            {
            get => evszam;
            set => evszam = value;
            }
        public double Osszes_napsutes
            {
            get => osszes_napsutes;
            set => osszes_napsutes = value;
            }
        public double Napilegtobb
            {
            get => napilegtobb;
            set => napilegtobb = value;
            }
        public DateTime Legtobb_datum
            {
            get => legtobb_datum;
            set => legtobb_datum = value;
            }
        public int Osszes_nap
            {
            get => osszes_nap;
            set => osszes_nap = value;
            }
        public int Legtobbnap_egymasutan
            {
            get => legtobbnap_egymasutan;
            set => legtobbnap_egymasutan = value;
            }
 
        public napsutes(string egysor)
            {
            string[] adatreszek = egysor.Split(';');
            
            this.Evszam = Convert.ToInt32(adatreszek[0]);
            this.Osszes_napsutes = Convert.ToDouble(adatreszek[1]);
            this.Napilegtobb = Convert.ToDouble(adatreszek[2]);
            string[] datumelem = adatreszek[3].Split('.');
            string nap = String.Concat(datumelem[0], ",", datumelem[1], ",", datumelem[2]);
            this.Legtobb_datum = Convert.ToDateTime(nap);
            this.Osszes_nap = Convert.ToInt32(adatreszek[4]);
            this.Legtobbnap_egymasutan = Convert.ToInt32(adatreszek[5]);
            }

        }
    }
