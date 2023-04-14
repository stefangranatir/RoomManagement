using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RoomManagment_v2
{
    public partial class Form4 : Form
    {
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
                return "" + this.ID + "-" + this.username + "-" + this.password + "-" + this.fullname + "-" + this.active + '\n' + " RezProstorije = " + this.RezProstorije + '\n' + " BrisKorisnika = " + this.BrisRezervacije + '\n' +  "PreRezervacije = " + this.PreRezervacije + '\n' + " DodKorisnika = " + this.DodKorisnika + '\n' + " BrisKorisnika = " + this.BrisKorisnika + '\n' + " UrediKorisnika = " + this.UrediKorisnika + '\n';
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

        public List<osoba> korisnici = new List<osoba>();
        public Form4()
        {
            InitializeComponent();
        }
        public SqlConnection cnn;
        public SqlDataReader dataReader;
        public SqlCommand naredba;
        public SqlDataAdapter adapter;


        public string output = "";
        public string cs;
        public string sql;
        public string username = " ";
        public string password = " ";
        private void Form4_Load(object sender, EventArgs e)
        {
            cs = @"Data Source=localhost\sqlexpress;Initial Catalog=UpraviteljSoba_v2;User ID=SobeAdmin;Password=12345";
            cnn=new SqlConnection(cs);
            cnn.Open();
            sql = "Select * from Users";
            naredba= new SqlCommand(sql,cnn);
            dataReader=naredba.ExecuteReader();
            while(dataReader.Read())
            {
                osoba p=new osoba();
                if (bool.Parse(dataReader.GetValue(4).ToString()) == true)
                {
                    p.Prijavljen(int.Parse(dataReader.GetValue(0).ToString()), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), bool.Parse(dataReader.GetValue(4).ToString()));
                    korisnici.Add(p);
                }
            }
            naredba.Dispose();
            dataReader.Close();
            

           /* sql = "select * from UserPrava";
            naredba = new SqlCommand(sql, cnn);
            dataReader = naredba.ExecuteReader();*/


            /*foreach (osoba o in korisnici) {
                
                bool g = false, b = false, c = false, d = false, h = false, f = false;
                while (dataReader.Read()) {
                    output = dataReader.GetValue(2).ToString();
                    if (o.VratiID() == int.Parse(dataReader.GetValue(1).ToString())){
                        if (output == "1")
                        {
                            g = true;
                        }
                        else if (output == "2")
                        {
                            b = true;
                        }
                        else if (output == "3")
                        {
                            c = true;
                        }
                        else if (output == "4")
                        {
                            d = true;
                        }
                        else if (output == "5")
                        {
                            h = true;
                        }
                        else if (output == "6")
                        {
                            f = true;
                        }
                    }
                }
                o.PromjeniPrava(g, b, c, d, h, f);
            }*/

            foreach(osoba o in korisnici)
            {
                bool g = false, b = false, c = false, d = false, h = false, f = false;
                sql = "select * from UserPrava where UserID =" + o.VratiID();
                naredba=new SqlCommand(sql,cnn);
                dataReader=naredba.ExecuteReader();
                while (dataReader.Read())
                {
                    output = dataReader.GetValue(2).ToString();

                    if (output == "1")
                    {
                        g = true;
                    }
                    else if (output == "2")
                    {
                        b = true;
                    }
                    else if (output == "3")
                    {
                        c = true;
                    }
                    else if (output == "4")
                    {
                        d = true;
                    }
                    else if (output == "5")
                    {
                        h = true;
                    }
                    else if (output == "6")
                    {
                        f = true;
                    }
                }
                naredba.Dispose();
                dataReader.Close();
                o.PromjeniPrava(g, b, c, d, h, f);
            }
            foreach(osoba o in korisnici)
            {
                listBox1.Items.Add(o.Ispisi());
            }
            


            cnn.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private string Kriptiraj(string kriptiraj)
        {
            var sb = new StringBuilder();
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(kriptiraj));

                foreach (var c in data)
                {
                    sb.Append(c.ToString("x2"));
                }
                kriptiraj = sb.ToString();
            }
            return kriptiraj;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string username;
            string password;
            string fullname;

            bool a = false;
            bool b = false;
            bool c = false;
            bool d=false;
            bool g=false;
            bool f = false;

            username = textBox1.Text.ToString();

            password = Kriptiraj(textBox2.Text.ToString());
            fullname = textBox3.Text.ToString();
            cs = @"Data Source=localhost\sqlexpress;Initial Catalog=UpraviteljSoba_v2;User ID=SobeAdmin;Password=12345";
            cnn = new SqlConnection(cs);

            cnn.Open();
            adapter = new SqlDataAdapter();

            //MessageBox.Show("" + prijavljenb.VratiID());
            sql = "insert into Users(Username, Password, FullName, Active) values('"+username+"','"+password+"','"+fullname+"',1)";
            naredba = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            adapter.Dispose();
            naredba.Dispose();

           /* if (checkBox1.Checked == true)
            {
                adapter = new SqlDataAdapter();
                sql="insert into UserPrava(UserID,PravoID,Aktivno)values('"++"',)";
            }*/

            cnn.Close();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        { 
            int trazeniID = 0; 
            string odabrani = "";
            try
            {
                odabrani = listBox1.SelectedItem.ToString();
            }
            catch { }
            string[] splitano = odabrani.Split('-');
             trazeniID= int.Parse(splitano[0]);
            //MessageBox.Show("" + trazeniID);
            foreach(osoba o in korisnici)
            {
                if (trazeniID == o.VratiID())
                {

                }
            }
        }
    }
}
