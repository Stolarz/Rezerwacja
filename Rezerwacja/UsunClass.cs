using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;
namespace Rezerwacja
{
    class UsunClass
    {
        public static void delete(string name, string id)
        {
            using (var connection = new SQLiteConnection("database.db"))
            {
                if (name == "Obiekty")
                {
                    using (var statement = connection.Prepare(@"DELETE FROM Obiekty WHERE Id=?;"))
                    {
                        statement.Bind(1, id);
                        statement.Step();
                    }
                }
                else
                {
                    using (var statement = connection.Prepare(@"DELETE FROM Rezerwacje WHERE Id=?;"))
                    {
                        statement.Bind(1, id);
                        statement.Step();
                    }
                }

            }
        }

        public static void edit(string id, string imie, string nazwisko, string obiekt, string osoby, string dni, string data,int zaplata)
        {
            using (var connection = new SQLiteConnection("database.db"))
            {
                using (var statement = connection.Prepare(@"UPDATE Rezerwacje SET Imie=?,Nazwisko=?,Ilosc=?,Obiekt=?,Data=?,Telefon=?,IloscDni=?,DoZaplaty=? Where ID=?"))
                {
                    statement.Bind(1, imie);
                    statement.Bind(2, nazwisko);
                    statement.Bind(3, osoby);
                    statement.Bind(4, obiekt);
                    statement.Bind(5, data);
                    statement.Bind(6, "123");
                    statement.Bind(7, dni);
                    statement.Bind(8, zaplata);
                    statement.Bind(9, id);
                    statement.Step();
                }
            }

        }

    }
}
