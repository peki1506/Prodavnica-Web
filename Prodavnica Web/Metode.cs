using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prodavnica_Web
{
    public class Metode
    {
        public static string KorisnikLogin(string email, string lozinka)
        {
            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                string query = "SELECT id FROM klijenti WHERE email = @Email and lozinka = @Lozinka";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Lozinka", lozinka);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}