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
        ObservableCollection<AktualneRezerwacje> x;
        public Rezerwacje()
        {
            InitializeComponent();
            x = getValues();
            ListaRezerwacje.ItemsSource = x;
        }


        public class AktualneRezerwacje
        {
            public string Id { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Obiekt { get; set; }
        }

        public static ObservableCollection<AktualneRezerwacje> getValues()
        {
            ObservableCollection<AktualneRezerwacje> list = new ObservableCollection<AktualneRezerwacje>();

            using (var connection  = new SQLiteConnection("database.db"))
            {
                using(var statment = connection.Prepare(@"Select ID,Imie,Nazwisko,Obiekt FROM Rezerwacje;"))
                {
                    while(statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new AktualneRezerwacje()
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
                AktualneRezerwacje info = (AktualneRezerwacje)(sender as LongListSelector).SelectedItem;
                MessageBox.Show(info.Id);
                NavigationService.Navigate(new Uri("/RezerwacjaInfo.xaml?msg=" + info.Id, UriKind.Relative));
                ListaRezerwacje.SelectedItem = null;
            }
            
        }
    }
}