ENGLISH
### Project Documentation for Visual Programming - Cat Warrior
A simple Windows Form application in C#.

The theme of the project is a simple game where the player controls a warrior cat that lives in a house full of mice. Since the cat is no longer able to catch the mice, it declares war on them. The goal is to avoid being touched by a mouse.

When ammunition runs low, new ammo appears on the screen, which the player can collect to continue chasing the mice. To increase health, a heart appears on the screen, which restores the cat's health. Shooting is done by pressing the space bar.

The initial appearance of the game is shown below.
![catwarriorgame](https://user-images.githubusercontent.com/40269242/41558017-dbdc7044-733f-11e8-866b-5b27d89fc4b5.PNG)

In my first commit, the basic design of the simple game was created. Several necessary events were set up, including KeyDown and KeyUp. A timer was also added.
In my second commit, I implemented the KeyDown and KeyUp events for player movement. Below, you can find the code for the timer1_Tick event. The code is explained with the help of comments.

```C#
 private void timer1_Tick(object sender, EventArgs e)
        {
            if(playerHealth > 1) // for the player's health to be displayed in the progress bar.
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                // if the player's health is 0
                DeadCatFamousMouses dc = new DeadCatFamousMouses(); // show new form
                dc.ShowDialog();
                timer1.Stop(); // stop the timer
                gameOver = true; 
            }
            ammo.Text =""+ municija; // setting up the ammunition
            kill.Text = "" + score;  
            
            if (playerHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red; // red progress bar
            }   
            if(goLeft==true && player.Left > 0)
            {
                player.Left -= brzina;
                // move the player to the left
            }
            if(goRight==true && player.Left + player.Width < 925)
            {
                player.Left += brzina;
                // move the player to the right
            }
            if(goUp==true && player.Top > 60)
            {
                player.Top -= brzina;
                // move the player up
            }
            if(goDown == true && player.Top + player.Height < 650)
            {
                player.Top += brzina;
                // move the player down
            }
            
            foreach(Control x in this.Controls)
            {
                
                if (x is PictureBox && x.Tag == "municija")
                {
                    // check if x has hit the player's PictureBox
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // after the player collects the ammunition
                        this.Controls.Remove((PictureBox)x); 
                        ((PictureBox)x).Dispose(); 
                        municija += 5; // increase ammo by 5
                    }
                }
                //if the bullet touches the four sides of the game
                // if x is Picture Box and have tag = bullet
                if(x is PictureBox &&  x.Tag == "bullet")
                {
                    if(((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 925 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // delete the bullet from the display
                        ((PictureBox)x).Dispose();

                    }

                }
                // when the player hit the mouse
                if(x is PictureBox && x.Tag == "mouse")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; // if the player is hit by a mouse, reduce health by 1
                    }
                    // move the mouse to the player's picture box
                    if(((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= brzinaGluvche; // move it to the left of the player
                        ((PictureBox)x).Image = Properties.Resources.mouse_levo; // change mouse image to left
                    }
                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_levo; // top
                    }
                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_desno; // right
                    }
                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_desno; // down
                    }
                }
                foreach(Control j in this.Controls)
                {
                    // bullet and mouse identification
                    if((j is PictureBox && j.Tag == "bullet") &&(x is PictureBox && x.Tag == "mouse"))
                    {
                        // if bullet hit a mouse 
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; 
                            this.Controls.Remove(j); // delete the bullet
                            j.Dispose();
                            this.Controls.Remove(x); // delete the mouse 
                            x.Dispose();
                            makeMouses(); // show new mice
                        }
                    }
                }

                
           }

        }
```
In the new commit, the Bullets class can be seen. In it, we have a separate function for creating bullets. Below is the code for this function, explained with comments.
```C#
public void makeBullet(Form form)
        {
            Bullet.BackColor = System.Drawing.Color.Yellow; // yellow bullets
            Bullet.Size = new Size(5, 5); // size: 5px
            Bullet.Tag = "bullet"; // bullet's tag
            Bullet.Left = bulletLeft; // set bullet left
            Bullet.Top = bulletTop; // set bulet right
            Bullet.BringToFront();
            form.Controls.Add(Bullet); // adding a bullet to the screen
            timer.Interval = speed; 
            timer.Tick += new EventHandler(tm_Tick);
            timer.Start(); // start the timer
        }
```
In the last commit, the necessary changes were made for the game to function. I also added a function that allows us to increase the player's health. If we lose, a new form appears that looks like this:
![gameover](https://user-images.githubusercontent.com/40269242/41621972-7ceec62a-740e-11e8-9ea2-efc99e097e35.PNG)

For the implementation of the game, I have images of all the necessary participants in the game in the Resources folder. The main controls I used are PictureBox and Timer. The main functions for creating ammunition are in the public class Bullets. Above, the makeBullet() function from the Bullets class is explained.

MACEDONIAN
### Документација за проектот по Визуелно програмирање - Cat Warrior
<p>Едноставна Windows Form апликација во C#.</p>
<p>Темата на проектот е едноставна игричка каде играчот на играта е маче борец кое живее во куќа полна со глувчиња. Веќе не бил способен да ги фаќа глувчињата, па затоа објавува војна. Целта е да не бидеме допрени од глувче. Кога муницијата се намали тогаш на екран се појавува муниција која може да ја земеме и продолжиме со бркање на глувчињата. За зголемување на здравјето, на екран се појавува срце кое го зголемува здравјето. За пукање тоа го правиме со притискање на space. Почетниот изглед на игричката е во продолжение.</p>

![catwarriorgame](https://user-images.githubusercontent.com/40269242/41558017-dbdc7044-733f-11e8-866b-5b27d89fc4b5.PNG)

<p>Во мојот прв commit е направен основниот дизајн на едноставната игричка. Поставени се неколку events кој ќе се потребни и тоа KeyDown,KeyUp. Поставен е и тајмер.</p>

<p>Во мојот втор commit ги напишав настаните KeyDown и KeyUp за движење на играчот. Во продолжение го имате кодот за timer1_Tick настанот. Кодот е објаснет со помош на коментари.</p>

```C#
 private void timer1_Tick(object sender, EventArgs e)
        {
            if(playerHealth > 1) // for the player's health to be displayed in the progress bar.
            {
                progressBar1.Value = Convert.ToInt32(playerHealth);
            }
            else
            {
                // if the player's health is 0
                DeadCatFamousMouses dc = new DeadCatFamousMouses(); // show new form
                dc.ShowDialog();
                timer1.Stop(); // stop the timer
                gameOver = true; 
            }
            ammo.Text =""+ municija; // setting up the ammunition
            kill.Text = "" + score;  
            
            if (playerHealth < 20)
            {
                progressBar1.ForeColor = System.Drawing.Color.Red; // red progress bar
            }   
            if(goLeft==true && player.Left > 0)
            {
                player.Left -= brzina;
                // move the player to the left
            }
            if(goRight==true && player.Left + player.Width < 925)
            {
                player.Left += brzina;
                // move the player to the right
            }
            if(goUp==true && player.Top > 60)
            {
                player.Top -= brzina;
                // move the player up
            }
            if(goDown == true && player.Top + player.Height < 650)
            {
                player.Top += brzina;
                // move the player down
            }
            
            foreach(Control x in this.Controls)
            {
                
                if (x is PictureBox && x.Tag == "municija")
                {
                    // check if x has hit the player's PictureBox
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        // after the player collects the ammunition
                        this.Controls.Remove((PictureBox)x); 
                        ((PictureBox)x).Dispose(); 
                        municija += 5; // increase ammo by 5
                    }
                }
                //if the bullet touches the four sides of the game
                // if x is Picture Box and have tag = bullet
                if(x is PictureBox &&  x.Tag == "bullet")
                {
                    if(((PictureBox)x).Left < 1 || ((PictureBox)x).Left > 925 || ((PictureBox)x).Top < 10 || ((PictureBox)x).Top > 700)
                    {
                        this.Controls.Remove(((PictureBox)x)); // delete the bullet from the display
                        ((PictureBox)x).Dispose();

                    }

                }
                // when the player hit the mouse
                if(x is PictureBox && x.Tag == "mouse")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealth -= 1; // if the player is hit by a mouse, reduce health by 1
                    }
                    // move the mouse to the player's picture box
                    if(((PictureBox)x).Left > player.Left)
                    {
                        ((PictureBox)x).Left -= brzinaGluvche; // move it to the left of the player
                        ((PictureBox)x).Image = Properties.Resources.mouse_levo; // change mouse image to left
                    }
                    if (((PictureBox)x).Top > player.Top)
                    {
                        ((PictureBox)x).Top -= brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_levo; // top
                    }
                    if (((PictureBox)x).Left < player.Left)
                    {
                        ((PictureBox)x).Left += brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_desno; // right
                    }
                    if (((PictureBox)x).Top < player.Top)
                    {
                        ((PictureBox)x).Top += brzinaGluvche;
                        ((PictureBox)x).Image = Properties.Resources.mouse_desno; // down
                    }
                }
                foreach(Control j in this.Controls)
                {
                    // bullet and mouse identification
                    if((j is PictureBox && j.Tag == "bullet") &&(x is PictureBox && x.Tag == "mouse"))
                    {
                        // if bullet hit a mouse 
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; 
                            this.Controls.Remove(j); // delete the bullet
                            j.Dispose();
                            this.Controls.Remove(x); // delete the mouse 
                            x.Dispose();
                            makeMouses(); // show new mice
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
            Bullet.BackColor = System.Drawing.Color.Yellow; // yellow bullets
            Bullet.Size = new Size(5, 5); // size: 5px
            Bullet.Tag = "bullet"; // bullet's tag
            Bullet.Left = bulletLeft; // set bullet left
            Bullet.Top = bulletTop; // set bulet right
            Bullet.BringToFront();
            form.Controls.Add(Bullet); // adding a bullet to the screen
            timer.Interval = speed; 
            timer.Tick += new EventHandler(tm_Tick);
            timer.Start(); // start the timer
        }
```
Во последниот commit се направени потребните измени за да функционира играта. Додадов и функција со која може да го зголемиме здравјето на играчот. Доколку изгубиме се појавува нова форма која изгледа вака:
![gameover](https://user-images.githubusercontent.com/40269242/41621972-7ceec62a-740e-11e8-9ea2-efc99e097e35.PNG)

<p>За имплементација на играта имам во Resources слики од сите потребни учесници во играта. Контролите кои ги користев главно се PictureBox и Timer. Главните функции за создавање на муниција се во класата <b>public class Bullets</b>Погоре е објаснета функцијата makeBullet() од класата Bullets.</p>

Изработила: Славица Габрешанец
