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
        public Page1()
        {
            InitializeComponent();
            

        }

      
      




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var connection = new SQLiteConnection("database.db"))
                {
                    using(var statment = connection.Prepare(@"INSERT INTO Rezerwacje(Imie,Nazwisko,Ilosc,Obiekt,Data,Telefon) VALUES (?,?,?,?,?,?);"))
                    {
                        
                        statment.Bind(1, ImieTextBox.Text);
                        statment.Bind(2, NazwiskoTextBox.Text);
                        statment.Bind(3, IloscSlider.Value);
                        statment.Bind(4, ObiektText.Text);
                        statment.Bind(5, DataTextBlock.Text);
                        statment.Step();
                        statment.Reset();
                        statment.ClearBindings();
                    }
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception\n" + ex.ToString());
            }

        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Obiekty.xaml", UriKind.Relative));
            string msg = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                ObiektText.Text = msg;
            }
        }
    }
}