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
using System.Security.Cryptography;

namespace RoomManagment_v2
{
    public partial class Form1 : Form
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
            bool BrisKorisnika=false;
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
                this.PreRezervacije= c;
                this.DodKorisnika= d;
                this.BrisKorisnika=e;
                this.UrediKorisnika= f;
            }
            public string Ispisi()
            {
                return ""+this.ID+" - "+this.username+" - "+this.password+" - "+this.fullname+" - "+this.active+'\n'+ "RezProstorije = "+this.RezProstorije+'\n'+"BrisKorisnika = "+this.BrisRezervacije+'\n'+ "PreRezervacije = " + this.PreRezervacije + '\n' + "DodKorisnika = " + this.DodKorisnika + '\n' + "BrisKorisnika = " + this.BrisKorisnika + '\n' + "UrediKorisnika = " + this.UrediKorisnika + '\n';
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

        public static osoba prijavljen=new osoba();
        public Form1()
        {
            InitializeComponent();
        }
        public SqlConnection cnn;
        public SqlDataReader dataReader;
        public SqlCommand naredba;



        public string output = "";
        public string cs;
        public string sql;
        public string username = " ";
        public string password = " ";


       
        private string Kriptiraj(string kriptiraj)
        {
            var sb = new StringBuilder();
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var data=md5.ComputeHash(Encoding.UTF8.GetBytes(kriptiraj));
                
                foreach(var c in data)
                {
                    sb.Append(c.ToString("x2"));
                }
                kriptiraj= sb.ToString();
            }    
            return kriptiraj;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cs = @"Data Source=localhost\sqlexpress;Initial Catalog=UpraviteljSoba_v2;User ID=SobeAdmin;Password=12345";
            cnn=new SqlConnection(cs);

            cnn.Open();
            username=textUsername.Text.ToString();
            password=Kriptiraj(textPassword.Text.ToString());
            sql = "select * from Users where Username='" + username + "' and Password='" + password + "'";
            naredba = new SqlCommand(sql, cnn);
            output = "greska kod prijave";
            dataReader = naredba.ExecuteReader();

            while (dataReader.Read())
            {
                output = "";
                output += dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + '\n';
                prijavljen.Prijavljen(int.Parse(dataReader.GetValue(0).ToString()), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), bool.Parse(dataReader.GetValue(4).ToString()));
                prijavljen.Prijava(true);
            }
            
            
            naredba.Dispose();
            sql = "select * from UserPrava Where UserID=" + prijavljen.VratiID()+"and Aktivno =1 ";
            naredba = new SqlCommand(sql, cnn);
            dataReader.Close();
            dataReader = naredba.ExecuteReader();
            output = "";

            bool g =false, b=false, c = false, d = false, h = false, f=false;
            while (dataReader.Read())
            {
                
                output = dataReader.GetValue(2).ToString();

                if (output == "1")
                {
                    g = true;
                }
                else if(output == "2")
                {
                    b = true;
                }
                else if (output == "3")
                {
                    c= true;
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
            prijavljen.PromjeniPrava(g, b, c, d, h, f);


            //MessageBox.Show(prijavljen.Ispisi());
            cnn.Close();
            if (prijavljen.ProvjeraPrijave())
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.prijavljena.Prijavljen(prijavljen.VratiID(), prijavljen.VratiUsername(), prijavljen.VratiPassword(), prijavljen.VratiFullname(), prijavljen.VratiAktivan());
                form2.prijavljena.PromjeniPrava(g, b, c, d, h, f);
                form2.Show();
                

            }
        }
    }
}
