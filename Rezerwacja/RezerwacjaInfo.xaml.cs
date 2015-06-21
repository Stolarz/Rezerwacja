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
        ObservableCollection<Info> x;
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



        public class Info
        {
            public string Id { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Obiekt { get; set; }
            public string IloscGosci { get; set; }
            public string Data { get; set; }
            public string Telefon { get; set; }
            public string IloscDni { get; set; }
            public string Cena { get; set; }
        }



        public static ObservableCollection<Info> getValues(string id_msg)
        {
            ObservableCollection<Info> list = new ObservableCollection<Info>();

            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statment = connection.Prepare(@"Select * FROM Rezerwacje WHERE Id=?;"))
                {
                    
                   statment.Bind(1, id_msg);
                    
                    while (statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new Info()
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
                Info rezerwacja = (Info)this.ListaRezerwacjeInfo.SelectedItem;
               
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
            Info rezerwacja = (Info)this.ListaRezerwacjeInfo.SelectedItem;
            if (ListaRezerwacjeInfo.SelectedItem != null)
                NavigationService.Navigate(new Uri("/Edytuj.xaml?Id=" + rezerwacja.Id + "&Imie=" + rezerwacja.Imie + "&Nazwisko=" + rezerwacja.Nazwisko + "&Obiekt=" + rezerwacja.Obiekt + "&Dni=" + rezerwacja.IloscDni + "&Goscie=" + rezerwacja.IloscGosci + "&Data=" + rezerwacja.Data + "&Cena=" + rezerwacja.Cena, UriKind.Relative));
            else
                MessageBox.Show("Zaznacz rezerwację");
        }

    }
}