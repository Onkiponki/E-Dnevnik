using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace E_Dnevnik
{
    class Konekcija
    {
        static public SqlConnection zakonektuj()
        {
            string cs = @"Data source = DESKTOP-HLB8M6E\SQLEXPRESS; Initial catalog = ednevnik; Integrated security = true";
            return new SqlConnection(cs);

        }
    }
}
