using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace catWarrior
{
    class Bullets
    {
        public string direction;
        public int speed = 20; // postavuvanje na brzinata na vrednost 20
        PictureBox Bullet = new PictureBox(); // kreiranje PictureBox
        Timer timer = new Timer(); //kreiranje na timer 
        public int bulletLeft;
        public int bulletTop;

        public void makeBullet(Form form)
        {
            Bullet.BackColor = System.Drawing.Color.Yellow; // postavuvanje na zolta boja za kurshumite
            Bullet.Size = new Size(5, 5); // golemina 5px na 5px
            Bullet.Tag = "bullet"; // postavuvanje na tag-ot na bullet
            Bullet.Left = bulletLeft; // set bullet left
            Bullet.Top = bulletTop; // set bulet right
            Bullet.BringToFront();
            form.Controls.Add(Bullet); // dodavanje na Bullet na screen-ot
            timer.Interval = speed; // postavuvanje na intervalot na timerot na brzinata
            timer.Tick += new EventHandler(tm_Tick);
            timer.Start(); // start the timer
        }
            public void tm_Tick(object sender, EventArgs e)
        {
            if(direction == "left")
            {
                Bullet.Left -= speed; // dvizi go kurshumot levo
            }
            if (direction == "right")
            {
                Bullet.Left += speed; // dvizi go kurshumot desno
            }
            if (direction == "up")
            {
                Bullet.Top -= speed; // dvizi go kurshumot nagore
            }
            if (direction == "down")
            {
                Bullet.Top += speed; // dvizi go kurshumot nadole
            }
            if(Bullet.Left < 16 || Bullet.Left > 900 || Bullet.Top < 10 || Bullet.Top > 600)
            {
                timer.Stop(); // stopiranje na timer-ot
                timer.Dispose();
                Bullet.Dispose();
                timer = null;
                Bullet = null;

            }

        }


        }




    }

