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
        }

    }

