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
    public partial class DodajObiekt : PhoneApplicationPage
    {
        public DodajObiekt()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection("database.db"))
                {
                    using (var statment = connection.Prepare(@"INSERT INTO Obiekty(Nazwa,Ilosc) VALUES (?,?);"))
                    {
                        statment.Bind(1, ObiektNazwaTextbox.Text);
                        statment.Bind(2, ObiektIloscTextbox.Text);
                       
                        statment.Step();
                        statment.Reset();
                        statment.ClearBindings();
                        MessageBox.Show("Pomyślnie dodano obiekt");
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception\n" + ex.ToString());
            }
        }
    }
}