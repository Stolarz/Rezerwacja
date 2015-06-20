using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
namespace Rezerwacja
{
    class CreateDatabase
    {
        public static void LoadDatabase(SQLiteConnection db)
        {
            string sql = @"CREATE TABLE IF NOT EXISTS
                                Rezerwacje (Id      INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                            Imie    VARCHAR( 140 ),
                                            Nazwisko    VARCHAR( 140 ),
                                            Ilosc INTEGER,
                                            Obiekt VARCHAR( 140 ),
                                            Data VARCHAR( 140 ),
                                            Telefon VARCHAR ( 15 ) 
                            );";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }

            sql = @"CREATE TABLE IF NOT EXISTS
                                Obiekty (Id          INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                         Nazwa        VARCHAR( 140 ),
                                         Ilosc INTEGER
                            )";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }

            // Turn on Foreign Key constraints
            sql = @"PRAGMA foreign_keys = ON";
            using (var statement = db.Prepare(sql))
            {
                statement.Step();
            }
        }

    }
}
