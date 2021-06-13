using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data;
using MySql.Data;
using System.Management;
using System.Runtime.Remoting.Contexts;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AcsopNapfeny
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
            //mysqlserveren mysqldump alkalmazás, feladata a mysql-ben kiválasztott adatbázis tábláiban tárolt adatok elmentése mysql formátumba
            private static string adatimporter = @"C:\xampp\mysql\bin\mysql";
            //mysql: maga az adatbázis műveletek

            //Konstruktor
            public Kapcsolodas()
            {
                datasource = "localhost";
                dataport = "3306"; //alapértelmezett: dataport = "3306";
                database = "napfeny";
                username = "root";
                password = "";
                string connectionString;
                connectionString = "datasource=" + datasource + ";" + "Port=" + dataport + ";" + "DATABASE=" + database + ";" + "username=" + username + ";" + "PASSWORD=" + password + ";";
                //datasource=localhost;Port="3306";DATATABLE=piac;username=root;PASSWORD=;

                connection = new MySqlConnection(connectionString);
            }

            //Csatlakozás az adatbázishoz, megnézi milyen állapotban van a MySqlConnection
            public bool OpenConnection()
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
                    }

                    catch   //egy nyitott adatcsatorna bármikor megszakadhat, így kell a catch ág
                    {
                        //MessageBox.Show("Ismeretlen hiba, próbálja újra", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally //mindeképp lezárom a kapcsolatot
                    {
                        this.CloseConnection();
                    }
                }
            }

            //Adatok módosítása     //UPDATE 
            public void Modosit(string valtoztat)   //amit átadunk annak bármilyen neve lehet
            {
                adat = valtoztat;
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(adat, connection);  //cmd = MySql utasítás 
                        cmd.CommandText = adat;
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                    }

                    catch
                    {
                        MessageBox.Show("Ismeretlen hiba, próbálja újra", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.CloseConnection();
                    }
                }
            }

            //Rekord törlése 
            public void Torol(string torles)
            {
                adat = torles;
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(adat, connection);
                        cmd.ExecuteNonQuery();
                    }

                    catch
                    {
                        MessageBox.Show("Ismeretlen hiba, próbálja újra", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.CloseConnection();
                    }
                }
            }

            //Adatok betöltése egy táblába
            public DataTable Tablaz(string kerdes)
            {
                adat = kerdes;
                DataTable dt = new DataTable();
                if (this.OpenConnection() == true)
                {
                    try
                    {
                        MySqlCommand lekerdez = new MySqlCommand(adat, connection);
                        MySqlDataReader megnyit = lekerdez.ExecuteReader();
                        dt.Load(megnyit);
                        if (dt.Rows.Count > 0)
                        {
                            megnyit.Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ismeretlen hiba, próbálja újra", "HIBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    finally
                    {
                        this.CloseConnection();
                    }
                }
                return dt;
            }
            //String => objektum
            //string => adattípus

            //Adatok betöltése listába
            public List<string> Nevsor(string valogat)
            {
                adat = valogat;
                List<string> egyMezoslista = new List<string>();

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(adat, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        egyMezoslista.Add(dataReader.GetString(0));
                    }
                    dataReader.Close();
                    this.CloseConnection();
                    return egyMezoslista;
                }
                else
                {
                    return egyMezoslista;
                }
            }

            //Rekordok számolása
            public int Count(string szamol)
            {
                adat = szamol; //query = "SELECT Count(*) FROM partnerek";
                int Count = -1; //-1 jelzi, hogy a gyűjteményban a keresett érték nem található
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(adat, connection);
                    Count = int.Parse(cmd.ExecuteScalar() + "");//utasítés végén nem vár adatokat csak egyet
                    this.CloseConnection();                     //"":biztos hogy stringgé válik ettől 
                    return Count;
                }
                else
                {
                    return Count;
                }
            }

            //Adatbázis mentése
            public void Mentes(string mappa)
            {
                string hely = mappa + "\\";     //string hely = mappa +@"\";    

                DateTime MentesIdopont = DateTime.Now;
                //DateTime MentesIdopont = DateTime.Today;
                int year = MentesIdopont.Year;
                int month = MentesIdopont.Month;
                int day = MentesIdopont.Day;
                int hour = MentesIdopont.Hour;
                int minute = MentesIdopont.Minute;
                int second = MentesIdopont.Second;
                int millisecond = MentesIdopont.Millisecond;

                //A file mentése a C:\ valahová az aktuális dátummal

                String path = @hely + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";

                try
                {
                    StreamWriter file = new StreamWriter(path);

                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = adatexporter;
                    psi.RedirectStandardInput = false;  //billentyűzet letiltva, mivel nem onnan veszi az adatokat
                    psi.RedirectStandardOutput = true;  //képernyőn megjelenik valami
                    psi.Arguments = String.Format(@"-u{0} -p{1} -h{2} {3}", username, password, datasource, database);
                    psi.UseShellExecute = false;//konzolt tiltjuk, így az a háttérben fut majd

                    Process process = Process.Start(psi);//process objektum létrehpzűsa
                    output = process.StandardOutput.ReadToEnd();//hajtsd végre amíg a végig nem érsz
                    file.WriteLine(output);//kimeneti fájl kiírása
                    process.WaitForExit();//várjon amíg a művelet elkészül
                    file.Close();       //bezárások
                    process.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("A mentés nem valósult meg!");
                }
            }

            //Adatbázis visszatöltése egy kiváalszott mentett file-ból
            public void Visszatolt(String fullPath)
            {
                try
                {
                    String adat = fullPath;
                    StreamReader file = new StreamReader(adat);
                    input = file.ReadToEnd();//mindent beolvas a memóriába
                    file.Close();

                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = adatimporter;
                    psi.RedirectStandardInput = true;   //innen kapja az adatot
                    psi.RedirectStandardOutput = false;//nem érdekel minket hova írja
                    psi.Arguments = String.Format(@"-u{0} -p{1} -h{2} {3}", username, password, datasource, database);
                    psi.UseShellExecute = false;

                    Process process = Process.Start(psi);
                    process.StandardInput.WriteLine(input);
                    process.StandardInput.Close();
                    process.WaitForExit();
                    process.Close();

                }
                catch (IOException ex)
                {
                    MessageBox.Show("Az adatok visszatöltése sikertelen volt!");
                }
            }

        }
}
