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
        ObservableCollection<RezerwacjaBlueprint> x;
        ObservableCollection<RezerwacjaBlueprint> y;

        public Rezerwacje()
        {
            InitializeComponent();
            x = getValues();
            ListaRezerwacje.ItemsSource = x;
            y = getOldValues();
            ListaPrzyszlychRezerwacji.ItemsSource = y;

        }


        public static ObservableCollection<RezerwacjaBlueprint> getValues()
        {
            ObservableCollection<RezerwacjaBlueprint> list = new ObservableCollection<RezerwacjaBlueprint>();
            DateTime date = DateTime.Today;
            using (var connection  = new SQLiteConnection("database.db"))
            {
                using(var statment = connection.Prepare(@"Select ID,Imie,Nazwisko,Obiekt FROM Rezerwacje WHERE ? >= Data;"))
                {
                    statment.Bind(1, date.ToString("d"));
                    while(statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new RezerwacjaBlueprint()
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


        public static ObservableCollection<RezerwacjaBlueprint> getOldValues()
        {
            ObservableCollection<RezerwacjaBlueprint> list = new ObservableCollection<RezerwacjaBlueprint>();
            DateTime date = DateTime.Today;
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statment = connection.Prepare(@"Select ID,Imie,Nazwisko,Obiekt FROM Rezerwacje WHERE ? < Data;"))
                {
                    statment.Bind(1, date.ToString("d"));
                    while (statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new RezerwacjaBlueprint()
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
                RezerwacjaBlueprint info = (RezerwacjaBlueprint)(sender as LongListSelector).SelectedItem;
                
                NavigationService.Navigate(new Uri("/RezerwacjaInfo.xaml?msg=" + info.Id, UriKind.Relative));
                ListaRezerwacje.SelectedItem = null;
            }
            
        }

        private void ListaPrzyszlychRezerwacji_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListaPrzyszlychRezerwacji.SelectedItem != null)
            {
                RezerwacjaBlueprint info = (RezerwacjaBlueprint)(sender as LongListSelector).SelectedItem;
                NavigationService.Navigate(new Uri("/RezerwacjaInfo.xaml?msg=" + info.Id, UriKind.Relative));
                ListaPrzyszlychRezerwacji.SelectedItem = null;
            }
        }
    }
}