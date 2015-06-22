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
    public partial class Obiekty : PhoneApplicationPage
    {
        ObservableCollection<RezerwacjaBlueprint> x;
        public Obiekty()
        {
            InitializeComponent();
            x = getValues();

            ListaObiektow.ItemsSource = x;
        } 

        public static ObservableCollection<RezerwacjaBlueprint> getValues()
        {
            ObservableCollection<RezerwacjaBlueprint> list = new ObservableCollection<RezerwacjaBlueprint>();

            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statment = connection.Prepare(@"Select Id, Nazwa, Ilosc, Cena FROM Obiekty;"))
                {
                    while (statment.Step() == SQLiteResult.ROW)
                    {
                        
                        list.Add(new RezerwacjaBlueprint()
                        {
                            Id = statment[0].ToString(),
                            Nazwa = (string)statment[1],
                            Ilosc = statment[2].ToString(),
                            Cena = statment[3].ToString()
                        });
                    }
                }
            }
            return list;
        }

    public void ListaObiektow_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string msg = "";
        RezerwacjaBlueprint element = (RezerwacjaBlueprint)(sender as LongListSelector).SelectedItem;
        if (NavigationContext.QueryString.TryGetValue("msg", out msg))
        NavigationService.Navigate(new Uri("/Edytuj.xaml?msg=" + element.Nazwa + "&cena=" + element.Cena, UriKind.Relative));
        else
        NavigationService.Navigate(new Uri("/DodaniePage.xaml?msg="+element.Nazwa+"&cena="+element.Cena, UriKind.Relative));

    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        RezerwacjaBlueprint obiekt = (RezerwacjaBlueprint)(sender as MenuItem).DataContext;
       MessageBoxResult result = MessageBox.Show("Uwaga za chwię usuniesz obiekt o nazwie: " + obiekt.Nazwa + " Czy jesteś pewien ?", "Usuwanie", MessageBoxButton.OKCancel);
       if (result == MessageBoxResult.OK)
       {
           UsunClass.delete("Obiekty", obiekt.Id);
           x.Remove(obiekt);
       }
    }
    
    }
}