using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kalkulator
{
    public partial class Form1 : Form
    {
        //promenljiva koja cuva rezultat operacija
        double rezultat = 0;
        //promenljiva koja cuva poslednje odabranu operaciju
        string operacija = "";
        //promenljiva koja prikazuje da li je na displeju prikazan rezultat racunanja ili broj koji pritisnemo
        bool prikazanRezultat = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void NumbersClick(object sender, EventArgs e)
        { //klikom na dugmice sa brojevima unosimo ih na displej(jedan pored drugog), ako je na displeju rezultat onda se displej krece ispocetka i pojavljuje se broj koji smo pritisnuli
            if (prikazanRezultat)
            {
                txtDisplay.Text = ((Button)sender).Text;
                prikazanRezultat = false;
            }
            //u suprotnom se novi broj dodaje iza vec postojecih brojeva
            else
            {
                txtDisplay.AppendText(((Button)sender).Text);
            }
        }
        private void Point(object sender, EventArgs e)
        {
            //ako nije prikazan rezultat i ako je u tekstu negde "." 
            if (!prikazanRezultat && txtDisplay.Text.Contains("."))
            {
                //ako tacka nije na poslednjem mestu a pritisnemo je opet izbacice nam gresku
                if (txtDisplay.Text.IndexOf(".") != txtDisplay.Text.Length - 1)
                {
                    txtDisplay.Text = "Ne mozete opet uneti decimalni zarez";
                    prikazanRezultat = true;
                }
                else //ako je tacka na prvom ili poslednjem mestu kada je opet pritisnemo samo ce nestati
                {
                    txtDisplay.Text = txtDisplay.Text.Replace(".", "");
                }
            }
            else if (prikazanRezultat) //ako je prikazan rezultat i pritisnemo tacku na displeju se ona samo prikazuje a prethodni rezultat nestaje
            {
                txtDisplay.Text = ((Button)sender).Text;
                prikazanRezultat = false;
            }
            else
            {         //ako pritisnemo tacku nakon nekih brojeva ona ce se upisati iza njih
                txtDisplay.AppendText(((Button)sender).Text);
            }
        } 
        private void PosNeg(object sender,EventArgs e)
        {
            if (txtDisplay.Text == "" || Convert.ToDouble(txtDisplay.Text)==0)   //ako je displej prazan ili ako je na displeju nula ,nista se ne menja
            {
                txtDisplay.Text = txtDisplay.Text;
            }
            else if (Convert.ToDouble(txtDisplay.Text) > 0)
            {
                txtDisplay.Text = "-" + txtDisplay.Text;          //dodaje - ispred positivnih brojeva
            }
            else if(Convert.ToDouble(txtDisplay.Text) < 0)
            {
                txtDisplay.Text=txtDisplay.Text.Remove(0, 1);     //ako je broj vec negativan brise minus
            }
            
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";                                            
            rezultat = 0;
            operacija = "";
            prikazanRezultat = false;
        }
        private void OperationsClick(object sender, EventArgs e)
        {//klikom na dugme sa operacijama,racuna se rezultat operacija i prikazuje na displeju 
            try
            {
                checked
                {
                    switch (operacija)
                    {
                        case "":
                           rezultat = Convert.ToDouble(txtDisplay.Text);
                           prikazanRezultat = true;
                           txtDisplay.Text = rezultat.ToString();
                           operacija = ((Button)sender).Text;
                           break;
                        case "+":
                            rezultat += Convert.ToDouble(txtDisplay.Text);
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                        case "*":
                            if (Convert.ToDouble(txtDisplay.Text) > 5000)                   
                            {
                                Exception MaksimalanMnozilac = new Exception("Maksimalni mnozilac je 5000!");
                                throw MaksimalanMnozilac;
                            }
                            rezultat *= Convert.ToDouble(txtDisplay.Text);
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                        case "/":
                            if (Convert.ToDouble(txtDisplay.Text) == 0)
                            {
                                Exception deljenjeNulom = new Exception("Ne mozete deliti broj sa nulom!");
                                throw deljenjeNulom;
                            }
                            rezultat /= Convert.ToDouble(txtDisplay.Text);
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                        case "-":
                            rezultat -= Convert.ToDouble(txtDisplay.Text);
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                        case "^2":
                            rezultat = Math.Pow(Convert.ToDouble(txtDisplay.Text), 2);                                                  //Convert.ToDouble(txtDisplay.Text) * Convert.ToDouble(txtDisplay.Text);
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                        case "√":
                            if (Convert.ToDouble(txtDisplay.Text) < 0)
                            {
                                Exception NegativanKoren = new Exception("Negativni brojevi nemaju svoj koren!");
                                throw NegativanKoren;
                            }

                            rezultat = Math.Sqrt(Convert.ToDouble(txtDisplay.Text));
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                        case "1/x":
                            rezultat = 1 / Convert.ToDouble(txtDisplay.Text);
                            prikazanRezultat = true;
                            txtDisplay.Text = rezultat.ToString();
                            operacija = ((Button)sender).Text;
                            break;
                     

                    }
                    //ukoliko kliknemo na taster = resetuju se podesavanja
                    if (operacija == "=")
                    {
                        rezultat = 0;
                        operacija = "";
                        prikazanRezultat = true;
                    }
                }
            }
            catch(FormatException)
            {
                txtDisplay.Text = "Greska u formatu";
                rezultat = 0;
                operacija = "";
                prikazanRezultat = true;
            }
            catch(OverflowException)
            {
                txtDisplay.Text = "Prekoracenje opsega";
                rezultat = 0;
                operacija = "";
                prikazanRezultat = true;
            }
            
            
            catch (Exception izuzetak)
            {
                txtDisplay.Text = izuzetak.Message;
                rezultat = 0;
                operacija = "";
                prikazanRezultat = true;
            }

        }

        
    }
}
