using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace naveen_fainal_1
{
    internal class DBConnection
    {
        private static SqlConnection conn;

        public static SqlConnection GetSqlConnection()
        {
            conn = new SqlConnection("Data Source=SHAHE;Initial Catalog=Library;Integrated Security=True");
            return conn;
        }
    }
}
