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
    public partial class RezerwacjaInfo : PhoneApplicationPage
    {
        ObservableCollection<RezerwacjaBlueprint> x;

       public string id_msg;
        public RezerwacjaInfo()
        {
            InitializeComponent();
            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
                id_msg = msg;

            x = getValues(id_msg);
            ListaRezerwacjeInfo.ItemsSource = x;
        }

        public static ObservableCollection<RezerwacjaBlueprint> getValues(string id_msg)
        {
            ObservableCollection<RezerwacjaBlueprint> list = new ObservableCollection<RezerwacjaBlueprint>();

            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statment = connection.Prepare(@"Select * FROM Rezerwacje WHERE Id=?;"))
                {
                    
                   statment.Bind(1, id_msg);
                    
                    while (statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new RezerwacjaBlueprint()
                        {
                            Id = statment[0].ToString(),
                            Imie = (string)statment[1],
                            Nazwisko = (string)statment[2],
                            IloscGosci = statment[3].ToString(),
                            Obiekt = (string)statment[4],
                            Data = (string)statment[5],
                            Telefon = (string)statment[6],
                            IloscDni = statment[7].ToString(),
                            Cena = statment[8].ToString()
                        });
                    }
                }
            }
            return list;
        }

        private void Usun_Click(object sender, EventArgs e)
        {
            if (ListaRezerwacjeInfo.SelectedItem != null)
            {
                RezerwacjaBlueprint rezerwacja = (RezerwacjaBlueprint)this.ListaRezerwacjeInfo.SelectedItem;
               
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć rezerwację ?", "Usuwanie", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    UsunClass.delete("Rezerwacje", rezerwacja.Id);
                    MessageBox.Show("Pomyślnie usunięto. Nastąpi powrót do menu głównego");
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                }
            }
            else
            { MessageBox.Show("Zaznacz element"); }

        }
        private void Edytuj_Click(object sender, EventArgs e)
        {
            RezerwacjaBlueprint rezerwacja = (RezerwacjaBlueprint)this.ListaRezerwacjeInfo.SelectedItem;
            if (ListaRezerwacjeInfo.SelectedItem != null){
               int cenaodobowa;
               cenaodobowa = Convert.ToInt32(rezerwacja.Cena) /Convert.ToInt32(rezerwacja.IloscDni);
               NavigationService.Navigate(new Uri("/Edytuj.xaml?Id=" + rezerwacja.Id + "&Imie=" + rezerwacja.Imie + "&Nazwisko=" + rezerwacja.Nazwisko + "&Obiekt=" + rezerwacja.Obiekt + "&Dni=" + rezerwacja.IloscDni + "&Goscie=" + rezerwacja.IloscGosci + "&Data=" + rezerwacja.Data + "&Cena=" + cenaodobowa+"&Telefon="+rezerwacja.Telefon, UriKind.Relative));
            }
                else
                MessageBox.Show("Zaznacz rezerwację");
        }

    }
}