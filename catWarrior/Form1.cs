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
        string nasokaKurshumi = "left"; //na kade da bidat kurshumite
        double playerHealth = 100; // zdravje na igrachot
        int brzina = 5; // brzina na igrachot
        int municija = 20; // so kolku municija pochnuva igrahot
        int brzinaGluvche = 1; // so koja brzina se dvizat gluvchinjata
        int score = 0; // kolku boda postignal igrachot vo tekot na igrata
        bool gameOver = false; // na pocetok false, pa koga kje zavrshi igrata stanuva True
        Random r = new Random();
        DeadCatFamousMouses dc = new DeadCatFamousMouses(); // pokazi nova forma
        public Form1()
        {
            InitializeComponent();
         
            this.DoubleBuffered = true;
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
                player.Image = Properties.Resources.cat_top;
            }
            //ako e pretisnato kopcheto za nadole
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                nasokaKurshumi = "down";
                player.Image = Properties.Resources.cat_down;
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
                    NeedMoreHealth();
                }
            } 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (playerHealth > 1) //za zdravjeto na igrachot da bide pretstaveno vo progress bar-ot
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                //ako zdravjeto na igrachot e pomal od 1 odnosno ednakov na nula
                dc.Show();
                timer1.Stop(); // stopiraj tajmer
                gameOver = true; //postavi gameOver na true
               
            }
            ammo.Text = "" + municija; // posTavuvanje na municijata 
            kill.Text = "" + score;  // kolku ubieni gluvchinja
            //ako e pomalo od 20 zdravjeto na igrachot

            if (playerHealth < 20)
            {
                progressBar1.ForeColor = Color.Red; //oboj go vo crveno progress baro-ot
                
            }


            if (goLeft == true && player.Left > 0)
            {
                player.Left -= brzina;
                // dvizi go igracot levo
            }

            if (goRight == true && player.Left + player.Width < 925)
            {
                player.Left += brzina;
                //dvizi go igrachot desno
            }

            if (goUp == true && player.Top > 60)
            {
                player.Top -= brzina;
                //dvizi go igracot na gore
            }

            if (goDown == true && player.Top + player.Height < 650)
            {
                player.Top += brzina;
                //dvizi go igracot nadole
            }

            //x e kontrola
            foreach (Control x in this.Controls)
            {
                // ako x e PictureBox i ima tag ammo
                if (x is PictureBox && x.Tag == "ammo")
                {// proverete dali x ja pogodil na igracho PictureBox-ot
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        //otkako igrachot ke ja zeme municijata
                        this.Controls.Remove(((PictureBox)x)); //izbrishi ja ammo picture box

                        ((PictureBox)x).Dispose(); // brishenje celosno
                        municija += 5; //zgolemuvanje na municijata za 5
                    }
                }
                if (x is PictureBox && x.Tag == "heart")
                {// proverete dali x ja pogodil na igracho PictureBox-ot
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        //otkako igrachot ke ja zeme municijata
                        this.Controls.Remove(((PictureBox)x)); //izbrishi ja ammo picture box

                        ((PictureBox)x).Dispose(); // brishenje celosno
                        if(playerHealth < 75)
                        {
                            playerHealth += 20;
                        }
                         //zgolemuvanje na municijata za 5
                    }
                }

                //ako kurshumot gi dopre chetirite strani na igrata
                //ako x e picture box i ima tag bullet
                if (x is PictureBox && x.Tag == "bullet")
                {
                    if (((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 925 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // izbrishi go kurshumot od display-ot
                        ((PictureBox)x).Dispose();

                    }

                }

                //ovaa e za koga igrachot kje pogodi gluvche
                if (x is PictureBox && x.Tag == "mouse")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; // ako e igracot pogoden od gluvche namali zdravje za 1
                    }
                    //dvizi go gluvcheto kon picture box-ot na igrachot
                    if (((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= brzinaGluvche; // dvizi go kon levo od igracot
                        ((PictureBox)x).Image = Properties.Resources.mouse_levo; // smeni slika na gluvche na levo
                    }


                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_levo; // smenija kon gore
                    }

                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_desno;//kon desno
                    }

                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_desno; // na dole
                    }
                }

                foreach (Control j in this.Controls)
                {
                    //identifikacija shto e kurshum shto e gluvche
                    if ((j is PictureBox && j.Tag == "bullet") && (x is PictureBox && x.Tag == "mouse"))
                    {
                        //ako kurshum pogodi gluvche
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; // pogodocite se zgolemuvaat za 1
                            this.Controls.Remove(j); // brishenje na kurshumot 
                            j.Dispose();
                            this.Controls.Remove(x); //brishenje na gluvcheto od screen-ot  
                            x.Dispose();
                            makeMouses(); // i pojavuvanje na novi gluvchinja
                        }
                    }
                }


            }

        }

        //ja povikuvame ovaa funkcija koga ni e potrebno povekje municija
        private void NeedMoreAmmunition()
        {
            PictureBox ammo = new PictureBox(); // kreiranje instanca od PictureBox
            ammo.Image = Properties.Resources.ammo_image; // postavuvanje na slikata vo PictureBox
            ammo.Tag = "ammo";
            ammo.SizeMode = PictureBoxSizeMode.StretchImage;
            ammo.Left = r.Next(10, 890); // postavuvanje random na levo
            ammo.Top = r.Next(50, 600); // postavuvanje random nagore
            this.Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();


        }
        
        private void NeedMoreHealth()
        {
            PictureBox heart = new PictureBox();
            // kreiranje instanca od PictureBox
            heart.Image = Properties.Resources.heart; // postavuvanje na slikata vo PictureBox
            heart.Tag = "heart";
            heart.BackColor = Color.Transparent;
            heart.SizeMode = PictureBoxSizeMode.StretchImage;
            heart.Left = r.Next(10, 890); // postavuvanje random na levo
            heart.Top = r.Next(50, 600); // postavuvanje random nagore
            this.Controls.Add(heart);
            heart.BringToFront();
            player.BringToFront();


        }
        //koga puka igrachot se povikuva ovaa funkcija
        private void PlayerShoot(string direct)
        {
            Bullets shoot = new Bullets();
            shoot.direction = direct;
            shoot.bulletLeft = player.Left + (player.Width / 2);
            shoot.bulletTop = player.Top + (player.Height / 2);
            shoot.makeBullet(this);

        }
        //za dodavanje na povekje gluvchinja
        private void makeMouses()
        {
            PictureBox mouse = new PictureBox(); // kreiranje nov PictureBox 
            mouse.Tag = "mouse"; // dodavanje tag 
            mouse.Image = Properties.Resources.mouse_desno;
            mouse.Left = r.Next(0, 900);
            mouse.Top = r.Next(0,800);
            mouse.SizeMode = PictureBoxSizeMode.StretchImage;
            mouse.BackColor = Color.Transparent;
            this.Controls.Add(mouse);
            player.BringToFront();
        }

       


    }
}
