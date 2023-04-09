using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RoomManagment_v2
{
    public partial class Form3 : Form
    {
        public class Prostorija
        {
            int id;
            string nazivProstorije;
            public Prostorija(int id, string nazivProstorije)
            {
                this.id = id;
                this.nazivProstorije = nazivProstorije;
            }
            public int vratiID()
            {
                return this.id;
            }
            public string vratiNaziv()
            {
                return this.nazivProstorije;
            }
        }
        public class Prostorije
        {
            List<Prostorija> prostorijeList=new List<Prostorija>();
            Prostorija p;

            public void dodajProstorije(int id, string nazivProstorije)
            {
                p=new Prostorija(id, nazivProstorije);
                prostorijeList.Add(p);
            }
            public string IspisInf()
            {
                string ispis = "";
                foreach(Prostorija p in prostorijeList)
                {
                    ispis += p.vratiID() + p.vratiNaziv() + '\n';
                }
                return ispis;
            }
        }
        public class Ponavljanje
        {
            int id;
            string naziv;
            public Ponavljanje(int id, string naziv)
            {
                this.id = id;
                this.naziv = naziv;
            }

            public int VratiId()
            {
                return this.id;
            }
            public string VratiNaziv()
            {
                return this.naziv;
            }
        }
        public class Vrijeme
        {
            int ID;
            string vrijeme;
            public Vrijeme(int iD, string datum)
            {
                ID = iD;
                this.vrijeme = datum;
            }
            public int VratiID()
            {
                return this.ID;
            }
            public string VratiVrijeme()
            {
                return this.vrijeme;
            }
        }
        public class osoba
        {
            public int ID;
            public string username;
            public string password;
            public string fullname;
            public bool active;

            //prava
            bool RezProstorije = false;
            bool BrisRezervacije = false;
            bool PreRezervacije = false;
            bool DodKorisnika = false;
            bool BrisKorisnika = false;
            bool UrediKorisnika = false;


            bool prijavljen = false;

            public void Prijava(bool a)
            {
                this.prijavljen = a;
            }
            public bool ProvjeraPrijave()
            {
                return prijavljen;
            }
            public void Prijavljen(int iD, string username, string password, string fullname, bool active)
            {
                this.ID = iD;
                this.username = username;
                this.password = password;
                this.fullname = fullname;
                this.active = active;
            }

            public void PromjeniPrava(bool a, bool b, bool c, bool d, bool e, bool f)
            {
                this.RezProstorije = a;
                this.BrisRezervacije = b;
                this.PreRezervacije = c;
                this.DodKorisnika = d;
                this.BrisKorisnika = e;
                this.UrediKorisnika = f;
            }
            public string Ispisi()
            {
                return "" + this.ID + " - " + this.username + " - " + this.password + " - " + this.fullname + " - " + this.active + '\n' + "RezProstorije = " + this.RezProstorije + '\n' + "BrisKorisnika = " + this.BrisRezervacije + '\n' + "PreRezervacije = " + this.PreRezervacije + '\n' + "DodKorisnika = " + this.DodKorisnika + '\n' + "BrisKorisnika = " + this.BrisKorisnika + '\n' + "UrediKorisnika = " + this.UrediKorisnika + '\n';
            }

            public int VratiID()
            {
                return this.ID;
            }
            public string VratiUsername()
            {
                return this.username;
            }
            public string VratiPassword()
            {
                return this.password;
            }
            public string VratiFullname()
            {
                return this.fullname;
            }
            public bool VratiAktivan()
            {
                return this.active;
            }

            public bool VratiRezProstorije()
            {
                return this.RezProstorije;
            }
            public bool VratiBrisRezervacije()
            {
                return this.BrisRezervacije;
            }
            public bool VratiPreRezervacije()
            {
                return this.PreRezervacije;
            }
            public bool VratiDodKorisnika()
            {
                return this.DodKorisnika;
            }
            public bool VratiBrisKorisnika()
            {
                return this.BrisKorisnika;
            }
            public bool VratiUrediKorisnika()
            {
                return this.UrediKorisnika;
            }
        }

        public osoba prijavljenb=new osoba();

        public Form3()
        {
            InitializeComponent();
        }

        public List<Ponavljanje> Ponavljanja=new List<Ponavljanje>();
        public List<Vrijeme>Vremena=new List<Vrijeme>();

        public SqlConnection cnn;
        public SqlDataReader dataReader;
        public SqlCommand naredba;
        public SqlDataAdapter adapter;



        public string output = "";
        public string cs;
        public string sql;

        public int idPocetak = 0;
        public int idKraj = 0;
        public int Pon = 0;
        public int ProsID = 0;
        

        DateTime dt=new DateTime();
        public string datumRezervacije = "";

        Prostorije ucionice = new Prostorije();
        private void Form3_Load(object sender, EventArgs e)
        {
            cs = @"Data Source=localhost\sqlexpress;Initial Catalog=UpraviteljSoba_v2;User ID=SobeAdmin;Password=12345";
            cnn = new SqlConnection(cs);

            cnn.Open();
            sql = "select * from Prostorija";
            naredba = new SqlCommand(sql, cnn);
            
            dataReader = naredba.ExecuteReader();

            while (dataReader.Read())
            {
                comboBox1.Items.Add(dataReader.GetValue(0)+" - "+dataReader.GetValue(1));
                ucionice.dodajProstorije(int.Parse(dataReader.GetValue(0).ToString()), dataReader.GetValue(1).ToString());
            }


            naredba.Dispose();
            dataReader.Close();

            sql = "select * from Ponavljanja";
            naredba = new SqlCommand(sql, cnn);
            dataReader = naredba.ExecuteReader();

            while (dataReader.Read())
            {
                comboBox2.Items.Add(dataReader.GetValue(0) + " - " + dataReader.GetValue(1));
                Ponavljanje p = new Ponavljanje(int.Parse(dataReader.GetValue(0).ToString()), dataReader.GetValue(1).ToString());
                Ponavljanja.Add(p);
            }


            naredba.Dispose();
            dataReader.Close();

            sql = "select * from Vremena";
            naredba = new SqlCommand(sql, cnn);
            dataReader = naredba.ExecuteReader();

            while (dataReader.Read())
            {
                comboBox3.Items.Add(dataReader.GetValue(0) + "-" + dataReader.GetValue(1));
                Vrijeme p = new Vrijeme(int.Parse(dataReader.GetValue(0).ToString()), dataReader.GetValue(1).ToString());
                Vremena.Add(p);
            }



            cnn.Close();
            //label2.Text = ucionice.IspisInf();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void comboBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        
        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            string odabrani = "";
            try
            {
                odabrani = comboBox3.SelectedItem.ToString();
            }
            catch { }
            string[] splitano = odabrani.Split('-');
            idPocetak = int.Parse(splitano[0]);
            //MessageBox.Show(""+idPocetak);
            
            foreach(Vrijeme v in Vremena)
            {
                if (v.VratiID() > idPocetak)
                {
                    comboBox4.Items.Add(v.VratiID() + "-" + v.VratiVrijeme());
                }
                
            }
        }
        
        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            string odabrani = "";
            try
            {
                odabrani = comboBox4.SelectedItem.ToString();
            }
            catch { }
            string[] splitano = odabrani.Split('-');
            idKraj = int.Parse(splitano[0]);
            //MessageBox.Show("pocetakId: " + idPocetak + " idKraj :" + idKraj);
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            string dat=dateTimePicker1.Value.ToString();
            dt = Convert.ToDateTime(dat);
            datumRezervacije=dt.ToString("yyyy-MM-dd");
            //MessageBox.Show(datumRezervacije.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cs = @"Data Source=localhost\sqlexpress;Initial Catalog=UpraviteljSoba_v2;User ID=SobeAdmin;Password=12345";
            cnn = new SqlConnection(cs);

            cnn.Open();
            adapter = new SqlDataAdapter();

            //MessageBox.Show("" + prijavljenb.VratiID());
            sql="insert into Rezervacije(Datum, UserID, PocetakID, KrajID, ProstorijaID, PonavljanjeID, Aktivno)values(CAST(N'"+datumRezervacije.ToString()+"' as date),"+prijavljenb.VratiID()+", "+idPocetak+", "+idKraj+","+ProsID+","+Pon+",1  )";
            naredba=new SqlCommand(sql,cnn);
            adapter.InsertCommand=new SqlCommand(sql,cnn);
            adapter.InsertCommand.ExecuteNonQuery();
            
            cnn.Close();
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            string odabrani = "";
            try
            {
                odabrani = comboBox2.SelectedItem.ToString();
            }
            catch { }
            string[] splitano = odabrani.Split('-');
            Pon = int.Parse(splitano[0]);
            //MessageBox.Show("" + Pon);
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            string odabrani = "";
            try
            {
                odabrani = comboBox1.SelectedItem.ToString();
            }
            catch { }
            string[] splitano = odabrani.Split('-');
            ProsID = int.Parse(splitano[0]);
            //MessageBox.Show("" + ProsID);
        }
    }
}
