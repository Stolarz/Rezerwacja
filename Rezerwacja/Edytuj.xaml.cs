using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Rezerwacja
{
    public partial class Edytuj : PhoneApplicationPage
    {
        
        string id="";
        string msg = "";
        public Edytuj()
        {
            InitializeComponent();
            
        }
             protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("Id", out id))
            {
                EditValues.Id = id;
                EditValues.Imie = NavigationContext.QueryString["Imie"];
                EditValues.Nazwisko = NavigationContext.QueryString["Nazwisko"];
                EditValues.Obiekt = NavigationContext.QueryString["Obiekt"];
                EditValues.Dni = NavigationContext.QueryString["Dni"];
                EditValues.Goscie = NavigationContext.QueryString["Goscie"];
                EditValues.Data = NavigationContext.QueryString["Data"];
                EditValues.Cena = NavigationContext.QueryString["Cena"];
                EditValues.Telefon = NavigationContext.QueryString["Telefon"];
                ObiektText.Text = EditValues.Obiekt;
                WyborDaty.Value = Convert.ToDateTime(EditValues.Data);
                //CenaTextBlock.Text = EditValues.Cena;
            }
            id = EditValues.Id;
            ImieTextBox.Text = EditValues.Imie;
            NazwiskoTextBox.Text = EditValues.Nazwisko;
            IloscDni.Text = EditValues.Dni;
            LiczbaGosci.Text = EditValues.Goscie;
            NumerTelefonu.Text = EditValues.Telefon;
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                ObiektText.Text = msg;
                CenaTextBlock.Text = NavigationContext.QueryString["cena"];
            }
            
            


           
        }

             private void Button_Click(object sender, RoutedEventArgs e)
             {
                 int pelnacena = Convert.ToInt32(CenaTextBlock.Text) * Convert.ToInt32(IloscDni.Text);
                 UsunClass.edit(id, ImieTextBox.Text, NazwiskoTextBox.Text, ObiektText.Text, LiczbaGosci.Text, IloscDni.Text, WyborDaty.Value.ToString().Substring(0, 9), pelnacena,NumerTelefonu.Text);
                 MessageBox.Show("Edycja wykonana pomyślnie. Nastąpi przejście do listy rezerwacji");
                 NavigationService.Navigate(new Uri("/Rezerwacje.xaml", UriKind.Relative));
             }

             private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
             {
                 NavigationService.Navigate(new Uri("/Obiekty.xaml?msg=abc", UriKind.Relative));
             }
    }
}