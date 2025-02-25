using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        public Program()
        {
            SqlConnection conn = null;
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\v11.0; Initial Catalog=Library; Integrated Security=SSPI;";
            // или
            //SqlConnection conn = null;
            //conn = new SqlConnection(@"Data Source=(localdb)\v11.0; Initial Catalog=Library; Integrated Security=SSPI;");
        }
        static void Main(string[] args)
        {
            
        }
    }
}
