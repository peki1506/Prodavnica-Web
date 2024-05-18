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
        public static string KorisnikIme()
        {
            string Ime;
            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                string queryIme = "SELECT ime FROM klijenti WHERE id = @KorisnikID";
                using (SqlCommand cmd = new SqlCommand(queryIme, conn))
                {
                    cmd.Parameters.AddWithValue("@KorisnikID", Convert.ToInt16(HttpContext.Current.Session["KorisnikID"]));
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                        Ime = "ime";
                    else
                        Ime = result.ToString();
                }
                return Ime;
            }
        }
        public static string KorisnikPrezime()
        {
            string Prezime;
            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                string queryPrezime = "SELECT prezime FROM klijenti WHERE id = @KorisnikID";
                using (SqlCommand cmd = new SqlCommand(queryPrezime, conn))
                {
                    cmd.Parameters.AddWithValue("@KorisnikID", Convert.ToInt16(HttpContext.Current.Session["KorisnikID"]));
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                        Prezime = "Prezime";
                    else
                        Prezime = result.ToString();
                }
                return Prezime;
            }
        }
    }
}