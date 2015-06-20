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


        public class Rezerwacje
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string Obiekt { get; set; }
        }

        public static ObservableCollection<Rezerwacje> getValues()
        {
            ObservableCollection<Rezerwacje> list = new ObservableCollection<Rezerwacje>();

            using (var connection  = new SQLiteConnection("database.db"))
            {
                using(var statment = connection.Prepare(@"Select * FROM Rezerwacje;"))
                {
                    while(statment.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new Rezerwacje()
                        {
                            Imie = (string)statment[1],
                            Nazwisko = (string)statment[2],
                            Obiekt = (string)statment[4]
                        });
                    }
                }
            }
            return list;
        }
    }
}