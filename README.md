### Документација за проектот по Визуелно програмирање - Cat Warrior

<p>Темата на проектот е едноставна игричка каде играчот на играта е маче борец кое живее во куќа полна со глувчиња. Веќе не бил способен да ги фаќа глувчињата, па затоа објавува војна. Целта е да не бидеме допрени од глувче. Кога муницијата се намали тогаш на екран се појавува муниција која може да ја земеме и продолжиме со бркање на глувчињата. За зголемување на здравјето, на екран се појавува срце кое го зголемува здравјето. Почетниот изглед на игричката е во продолжение.</p>

![catwarriorgame](https://user-images.githubusercontent.com/40269242/41558017-dbdc7044-733f-11e8-866b-5b27d89fc4b5.PNG)

<p>Во мојот прв commit е направен основниот дизајн на едноставната игричка. Поставени се неколку events кој ќе се потребни и тоа KeyDown,KeyUp. Поставен е и тајмер.</p>

<p>Во мојот втор commit ги напишав настаните KeyDown и KeyUp за движење на играчот. Во продолжение го имате кодот за timer1_Tick настанот. Кодот е објаснет со помош на коментари.</p>

```C#
 private void timer1_Tick(object sender, EventArgs e)
        {
            if(playerHealth > 1) //za zdravjeto na igrachot da bide pretstaveno vo progress bar-ot
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                //ako zdravjeto na igrachot e pomal od 1 odnosno ednakov na nula
                DeadCatFamousMouses dc = new DeadCatFamousMouses(); // pokazi nova forma
                dc.ShowDialog();
                timer1.Stop(); // stopiraj tajmer
                gameOver = true; //postavi gameOver na true
            }
            ammo.Text =""+ municija; // posTavuvanje na municijata 
            kill.Text = "" + score;  // kolku ubieni gluvchinja
            //ako e pomalo od 20 zdravjeto na igrachot
            if (playerHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red; //oboj go vo crveno progress baro-ot
            }   
            if(goLeft==true && player.Left > 0)
            {
                player.Left -= brzina;
                // dvizi go igracot levo
            }
            if(goRight==true && player.Left + player.Width < 925)
            {
                player.Left += brzina;
                //dvizi go igrachot desno
            }
            if(goUp==true && player.Top > 60)
            {
                player.Top -= brzina;
                //dvizi go igracot na gore
            }
            if(goDown == true && player.Top + player.Height < 650)
            {
                player.Top += brzina;
                //dvizi go igracot nadole
            }
            //x e kontrola
            foreach(Control x in this.Controls)
            {
                // ako x e PictureBox i ima tag ammo
                if (x is PictureBox && x.Tag == "municija")
                {// proverete dali x ja pogodil na igracho PictureBox-ot
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        //otkako igrachot ke ja zeme municijata
                        this.Controls.Remove((PictureBox)x); //izbrishi ja ammo picture box
                        ((PictureBox)x).Dispose(); // brishenje celosno
                        municija += 5; //zgolemuvanje na municijata za 5
                    }
                }
                //ako kurshumot gi dopre chetirite strani na igrata
                //ako x e picture box i ima tag bullet
                if(x is PictureBox &&  x.Tag == "bullet")
                {
                    if(((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 925 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // izbrishi go kurshumot od display-ot
                        ((PictureBox)x).Dispose();

                    }

                }
                //ovaa e za koga igrachot kje pogodi gluvche
                if(x is PictureBox && x.Tag == "mouse")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; // ako e igracot pogoden od gluvche namali zdravje za 1
                    }
                    //dvizi go gluvcheto kon picture box-ot na igrachot
                    if(((PictureBox)x).Left > player.Left)
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
                foreach(Control j in this.Controls)
                {
                    //identifikacija shto e kurshum shto e gluvche
                    if((j is PictureBox && j.Tag == "bullet") &&(x is PictureBox && x.Tag == "mouse"))
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
```
Во новиот commit може да се види класата Bullets. Во неа имаме посебна функција која е за создавање на куршуми. Во продолжение кодот за оваа функција е објаснет со коментари.
```C#
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
```
Во последниот commit се направени потребните измени за да функционира играта. Додадов и функција со која може да го зголемиме здравјето на играчот. Доколку изгубиме се појавува нова форма која изгледа вака:
