using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace catWarrior
{
    public partial class Form1 : Form
    {
        bool goUp; // za player-ot za da odi nagore
        bool goDown; // za player-ot za da odi nadole
        bool goLeft; // za player-ot za da odi levo
        bool goRight; // za player-ot za da odi desno
        string nasokaKurshumi = "up"; //na kade da bidat kurshumite
        double playerHealth = 100; // zdravje na igrachot
        int brzina = 10; // brzina na igrachot
        int municija = 10; // so kolku municija pochnuva igrahot
        int brzinaGluvche = 3; // so koja brzina se dvizat gluvchinjata
        int score = 0; // kolku boda postignal igrachot vo tekot na igrata
        bool gameOver = false; // na pocetok false, pa koga kje zavrshi igrata stanuva True
        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver == true) return; // ako e game over ne pravi nishto
            //ako e pretisnato kopcheto za levo
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                nasokaKurshumi = "left";
                player.Image = Properties.Resources.cat_levo;
            }
            // ako e pretisnato kopcheto za desno
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                nasokaKurshumi = "right";
                player.Image = Properties.Resources.cat_desno;
            }
            //ako e pretisnato kopcheto za nagore
            if(e.KeyCode == Keys.Up)
            {
                goUp = true;
                nasokaKurshumi = "up";
                player.Image = Properties.Resources.cat_levo;
            }
            //ako e pretisnato kopcheto za nadole
            if (e.KeyCode == Keys.Up)
            {
                goDown = true;
                nasokaKurshumi = "down";
                player.Image=Properties.Resources.cat_desno
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (gameOver == true) return; // ako e game over ne pravi nishto
            if(e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            //so space ispushtame municja
            if(e.KeyCode == Keys.Space && municija > 0)
            {
                municija = municija - 1;
                PlayerShoot(nasokaKurshumi);
                if(municija < 1) //ako snemame municja ja povikuvame funkcijata NeedMoreAmmunition()
                {
                    NeedMoreAmmunition();
                }
            } 
        }

        private void gameEngine(object sender, EventArgs e)
        {
            
        }
        //ja povikuvame ovaa funkcija koga ni e potrebno povekje municija
        private void NeedMoreAmmunition()
        {

        }
        //koga puka igrachot se povikuva ovaa funkcija
        private void PlayerShoot(string direct)
        {

        }
        //za dodavanje na povekje gluvchinja
        private void makeMouses()
        {

        }

    }
}
