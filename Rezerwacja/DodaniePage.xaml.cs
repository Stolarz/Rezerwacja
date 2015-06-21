using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLitePCL;
using System.Diagnostics;
namespace Rezerwacja
{
    public partial class Page1 : PhoneApplicationPage
    {
        string msg = string.Empty;
        string cena = string.Empty;
        int cenafinalna = 0;
        public Page1()
        {
            InitializeComponent();
            

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                NavigationContext.QueryString.TryGetValue("cena", out cena);
                CenaTextBlock.Text = cena+" zł";
                ObiektText.Text = "Nazwa: "+msg;
                ImieTextBox.IsEnabled = true;
                NazwiskoTextBox.IsEnabled = true;
                IloscDni.IsEnabled = true;
                LiczbaGosci.IsEnabled = true;
                WyborDaty.IsEnabled = true;
                DodajButton.IsEnabled = true;
                NumerTelefonu.IsEnabled = true;
            }
        }
      




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var connection = new SQLiteConnection("database.db"))
                {
                   cenafinalna = Convert.ToInt32(cena) * Convert.ToInt32(IloscDni.Text);
                  
                    using(var statment = connection.Prepare(@"INSERT INTO Rezerwacje (Imie,Nazwisko,Ilosc,Obiekt,Data,Telefon,IloscDni,DoZaplaty) VALUES (?,?,?,?,?,?,?,?);"))
                    {
                        
                        statment.Bind(1, ImieTextBox.Text);
                        statment.Bind(2, NazwiskoTextBox.Text);
                        statment.Bind(3, LiczbaGosci.Text);
                        statment.Bind(4, msg);
                        statment.Bind(5, WyborDaty.Value.ToString().Substring(0,9));
                        statment.Bind(6, NumerTelefonu.Text);
                        statment.Bind(7, IloscDni.Text);
                        statment.Bind(8, cenafinalna);
                        statment.Step();
                        statment.Reset();
                        statment.ClearBindings();

                    }
                }
                MessageBox.Show("Pomyślnie dodano rezerwację");
                NavigationService.Navigate(new Uri("/Rezerwacje.xaml", UriKind.Relative));
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception\n" + ex.ToString());
            }

        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Obiekty.xaml", UriKind.Relative));
           
        }
    }
}