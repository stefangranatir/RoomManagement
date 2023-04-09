using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomManagment_v2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public class osobaA
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
        //public class osoba prijavljena { get; set; }

        public osobaA prijavljena = new osobaA();
        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;

            if (prijavljena.VratiRezProstorije())
            {
                button2.Visible = true;
            }
            if (prijavljena.VratiBrisRezervacije())
            {
                button3.Visible = true;
            }
            if (prijavljena.VratiPreRezervacije())
            {
                button4.Visible = true;
            }
            if(prijavljena.VratiDodKorisnika() && prijavljena.VratiUrediKorisnika() && prijavljena.VratiBrisKorisnika())
            {
                button1.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 FormaRezervacije = new Form3();
            

            FormaRezervacije.prijavljenb.Prijavljen(prijavljena.VratiID(), prijavljena.VratiUsername(), prijavljena.VratiPassword(), prijavljena.VratiFullname(), prijavljena.VratiAktivan());
            FormaRezervacije.prijavljenb.PromjeniPrava(prijavljena.VratiRezProstorije(), prijavljena.VratiBrisRezervacije(), prijavljena.VratiPreRezervacije(), prijavljena.VratiDodKorisnika(), prijavljena.VratiBrisKorisnika(), prijavljena.VratiUrediKorisnika());
            FormaRezervacije.Show();
        }
    }
}
