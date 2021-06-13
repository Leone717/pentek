using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BcsopAdoszam
{
    class Kapcsolodas
    {
        private MySqlConnection connection;
        private string datasource;
        private string dataport;
        private string database;
        private string username;
        private string password;
        private string adat;
        private string input;
        private string output;
        private static string adatexporter = @"C:\xampp\mysql\bin\mysqldump";
        //mysqlserveren mysqldump alkalmazás, feladata a mysql-ben kiválasztott adatbázis tábláiban 
        //tárolt adatok elmentése mysql formátumba
        private static string adatimporter = @"C:\xampp\mysql\bin\mysql";
        //mysql: maga az adatbázis műveletek

        //Konstruktor
        public Kapcsolodas()
        {
            datasource = "localhost";
            dataport = "3306"; //alapértelmezett: dataport = "3306";
            database = "ado";
            username = "root";
            password = "";
            string connectionString;
            connectionString = "datasource=" + datasource + ";" + "Port=" + dataport + ";" + "DATABASE=" 
                + database + ";" + "username=" + username + ";" + "PASSWORD=" + password + ";";
            //datasource=localhost;Port="3306";DATATABLE=piac;username=root;PASSWORD=;

            connection = new MySqlConnection(connectionString);
        }

        //Csatlakozás az adatbázishoz, megnézi milyen állapotban van a MySqlConnection
        public bool OpenConnection()    //pl. btnHozzaad gombnál használjuk
        {
            try
            {
                connection.Open();
                return true;            //ha megnyílik a kapcsolat, true-t ad vissza
            }
            catch (MySqlException ex)    //többféle hibaérték kezelése adatbázishoz kapcsolódáskor
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Nem lehet kapcsolódni a szerverhez. Keresse a rendszer adminisztrátort!", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1045:
                        MessageBox.Show("Nem megfelelő username/password, próbálja újra!", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Ismeretlen hiba, próbálja újra!", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                return false;   //akkor hasznos, ha lekérdezésnél meg akarjuk nézni, nyitva van-e a kapcsolat! 
            }
        }

        //Kapcsolat lezárása
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //ÚJ adat rögzítése     ujatir = SQL string INSERT INTO....
        public void Ujadat(string ujatir)
        {
            adat = ujatir;
            if (this.OpenConnection() == true) //Openconnection megnyitási metódus, ha megnyílik(true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(adat, connection);//adat: átadott sql, connection: kapcsolat
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Új partner hozzáadva az adatbázishoz!");//VISSZAJELZÉS
                }

                catch   //egy nyitott adatcsatorna bármikor megszakadhat, így kell a catch ág
                {
                    MessageBox.Show("Ismeretlen hiba, próbálja újra", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally //mindeképp lezárom a kapcsolatot
                {
                    this.CloseConnection();
                }
            }
        }
    }
}
