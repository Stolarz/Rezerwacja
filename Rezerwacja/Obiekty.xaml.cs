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
        ObservableCollection<SzablonObiekt> x;
        public Obiekty()
        {
            InitializeComponent();
            x = getValues();

            ListaObiektow.ItemsSource = x;
        } 


        public class SzablonObiekt
        {
            public string Nazwa { get; set; }
            public string Ilosc { get; set; }

        }
        public static ObservableCollection<SzablonObiekt> getValues()
        {
            ObservableCollection<SzablonObiekt> list = new ObservableCollection<SzablonObiekt>();

            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statment = connection.Prepare(@"Select Nazwa,Ilosc FROM Obiekty;"))
                {
                    while (statment.Step() == SQLiteResult.ROW)
                    {
                        
                        list.Add(new SzablonObiekt()
                        {
                            Nazwa = (string)statment[0],
                            Ilosc = statment[1].ToString()

                            
                        });
                    }
                }
            }
            return list;
        }

    }
}