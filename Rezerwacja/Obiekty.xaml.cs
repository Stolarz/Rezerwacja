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
        public Obiekty()
        {
            InitializeComponent();
            getValues();
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
                using (var statment = connection.Prepare(@"Select * FROM Obiekty;"))
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