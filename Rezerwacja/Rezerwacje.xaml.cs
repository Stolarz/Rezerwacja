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
using Rezerwacja;
namespace Rezerwacja
{
    public partial class Rezerwacje : PhoneApplicationPage
    {
        public Rezerwacje()
        {
            InitializeComponent();
        }


        public class AktualneRezerwacje
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Obiekt { get; set; }
        }

        public static ObservableCollection<AktualneRezerwacje> getValues()
        {
            ObservableCollection<AktualneRezerwacje> list = new ObservableCollection<AktualneRezerwacje>();

            using (var connection  = new SQLiteConnection("database.db"))
            {
                using(var statment = connection.Prepare(@"Select Imie,Nazwisko,Obiekt FROM Rezerwacje;"))
                {
                    while(statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new AktualneRezerwacje()
                        {
                            Imie = (string)statment[0],
                            Nazwisko = (string)statment[1],
                            Obiekt = (string)statment[2]
                        });
                    }
                }
            }
            return list;
        }
    }
}