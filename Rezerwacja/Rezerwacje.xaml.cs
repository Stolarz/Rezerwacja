using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using SQLitePCL;

namespace Rezerwacja
{
    public partial class Rezerwacje : PhoneApplicationPage
    {
        ObservableCollection<RezerwacjeClass> x;
        ObservableCollection<RezerwacjeClass> y;

        public Rezerwacje()
        {
            InitializeComponent();
            x = getValues();
            ListaRezerwacje.ItemsSource = x;
            y = getOldValues();
            ListaPrzyszlychRezerwacji.ItemsSource = y;

        }


        public class RezerwacjeClass
        {
            public string Id { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Obiekt { get; set; }
        }
     

        public static ObservableCollection<RezerwacjeClass> getValues()
        {
            ObservableCollection<RezerwacjeClass> list = new ObservableCollection<RezerwacjeClass>();
            DateTime date = DateTime.Today;
            using (var connection  = new SQLiteConnection("database.db"))
            {
                using(var statment = connection.Prepare(@"Select ID,Imie,Nazwisko,Obiekt FROM Rezerwacje WHERE ? >= Data;"))
                {
                    statment.Bind(1, date.ToString("d"));
                    while(statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new RezerwacjeClass()
                        {
                            Id = statment[0].ToString(),
                            Imie = (string)statment[1],
                            Nazwisko = (string)statment[2],
                            Obiekt = (string)statment[3]
                        });
                    }
                }
            }
            return list;
        }


        public static ObservableCollection<RezerwacjeClass> getOldValues()
        {
            ObservableCollection<RezerwacjeClass> list = new ObservableCollection<RezerwacjeClass>();
            DateTime date = DateTime.Today;
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statment = connection.Prepare(@"Select ID,Imie,Nazwisko,Obiekt FROM Rezerwacje WHERE ? < Data;"))
                {
                    statment.Bind(1, date.ToString("d"));
                    while (statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new RezerwacjeClass()
                        {
                            Id = statment[0].ToString(),
                            Imie = (string)statment[1],
                            Nazwisko = (string)statment[2],
                            Obiekt = (string)statment[3]
                        });
                    }
                }
            }
            return list;
        }





        private void ListaRezerwacje_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListaRezerwacje.SelectedItem != null)
            {
                RezerwacjeClass info = (RezerwacjeClass)(sender as LongListSelector).SelectedItem;
                
                NavigationService.Navigate(new Uri("/RezerwacjaInfo.xaml?msg=" + info.Id, UriKind.Relative));
                ListaRezerwacje.SelectedItem = null;
            }
            
        }

        private void ListaPrzyszlychRezerwacji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListaPrzyszlychRezerwacji.SelectedItem != null)
            {
                RezerwacjeClass info = (RezerwacjeClass)(sender as LongListSelector).SelectedItem;
                NavigationService.Navigate(new Uri("/RezerwacjaInfo.xaml?msg=" + info.Id, UriKind.Relative));
                ListaPrzyszlychRezerwacji.SelectedItem = null;
            }
        }
    }
}