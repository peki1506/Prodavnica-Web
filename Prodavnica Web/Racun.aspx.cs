using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Prodavnica_Web
{
	public partial class Racun : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                dataGridPopulate();
            }
        }
        private void dataGridPopulate()
        {
            SqlConnection veza = Konekcija.Connect();
            StringBuilder naredba = new StringBuilder(" SELECT racun.id, artikli.naziv, tipovi.boja, tipovi.velicina, tipovi.brend, ");
            naredba.Append(" lager.cena, racun_stavke.id, racun_stavke.kolicina, klijenti.popust, racun.datum, ");
            naredba.Append(" (SELECT SUM(cena)*(100-klijenti.popust)/100 FROM racun_stavke where id_racuna = (SELECT MAX(id) FROM racun)) AS ukupno ");
            naredba.Append(" FROM racun_stavke ");
            naredba.Append(" JOIN artikli ON racun_stavke.id_artikla = artikli.id JOIN tipovi ON tipovi.id = artikli.id_tipa ");
            naredba.Append(" JOIN lager ON lager.id_artikla = artikli.id JOIN racun ON racun.id = racun_stavke.id_racuna ");
            naredba.Append(" JOIN klijenti on klijenti.id = racun.id_klijenta ");
            naredba.Append(" WHERE racun.id = (SELECT MAX(id) FROM racun) ");

            SqlDataAdapter adapter = new SqlDataAdapter(naredba.ToString(), veza);
            DataTable dt_grid = new DataTable();
            adapter.Fill(dt_grid);
            ukupno.InnerText = dt_grid.Rows[0]["ukupno"].ToString();
            vreme.InnerText = dt_grid.Rows[0]["datum"].ToString();
            popust.InnerText = dt_grid.Rows[0]["popust"].ToString() + "%";
            GridView1.DataSource = dt_grid;
            GridView1.DataBind();

            
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string artikalId = GridView1.DataKeys[e.RowIndex].Values["ID1"].ToString();
            ObrisiArtikal(artikalId);
            dataGridPopulate();
        }
        private void ObrisiArtikal(string articleID)
        {
            string deleteQuery = "DELETE FROM racun_stavke WHERE id = @id";
            using (SqlConnection connection = Konekcija.Connect())
            {
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", Convert.ToInt32(articleID));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void Btn_Plati_Click(object sender, EventArgs e)
        {
            SqlConnection veza = Konekcija.Connect();
            string updateQuery = "UPDATE racun SET gotovo = 1 WHERE id = (SELECT MAX(ID) FROM racun)";
            using (veza)
            {
                veza.Open();
                using (SqlCommand command = new SqlCommand(updateQuery, veza))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        string script = "UspesnaKupovina();";
                        ScriptManager.RegisterStartupScript(this, GetType(), "UspesnaKupovinaScript", script, true);
                        Response.Redirect("Login.aspx");
                    }
                }
            }
        }
    }
}