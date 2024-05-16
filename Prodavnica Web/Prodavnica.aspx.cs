using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Text;

namespace Prodavnica_Web
{
    public partial class Prodavnica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListPolPopulate();
            }
        }
        private void DropDownListPolPopulate()
        {
            //DropDownList1.Items.Insert(0, new ListItem("Odaberi pol", ""));

            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT pol FROM tipovi", conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        DropDownListPol.DataTextField = dt.Columns["pol"].ToString();
                        DropDownListPol.DataValueField= dt.Columns["pol"].ToString();
                        DropDownListPol.DataSource = dt;
                        DropDownListPol.DataBind();
                    }
                }
            }
        }

        protected void DropDownListPol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownListPol.SelectedValue))
            {
                DropDownListVrstaPopulate();// Call your method to populate cmb_vrsta
            }
        }
        private void DropDownListVrstaPopulate()
        {
            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                StringBuilder naredba = new StringBuilder("SELECT DISTINCT naziv FROM artikli ");
                naredba.Append(" JOIN tipovi on id_tipa = tipovi.id ");
                naredba.Append(" WHERE pol = @comboboxVrednost");
                SqlCommand cmd = new SqlCommand(naredba.ToString(), conn);
                using (cmd)
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@comboboxVrednost", DropDownListPol.SelectedValue.ToString());
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        DropDownListVrsta.DataTextField = dt.Columns["naziv"].ToString();
                        DropDownListVrsta.DataValueField = dt.Columns["naziv"].ToString();
                        DropDownListVrsta.DataSource = dt;
                        DropDownListVrsta.DataBind();
                    }
                }
            }
        }

        protected void DropDownListVrsta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownListPol.SelectedValue))
            {
                DropDownListBrendPopulate();// Call your method to populate cmb_vrsta
            }
        }
        private void DropDownListBrendPopulate()
        {
            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                StringBuilder naredba = new StringBuilder("SELECT DISTINCT brend FROM  artikli ");
                naredba.Append(" JOIN tipovi on id_tipa = tipovi.id ");
                naredba.Append(" WHERE naziv = @comboboxVrednost");
                SqlCommand cmd = new SqlCommand(naredba.ToString(), conn);
                using (cmd)
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@comboboxVrednost", DropDownListVrsta.SelectedValue.ToString());
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        DropDownListBrend.DataTextField = dt.Columns["brend"].ToString();
                        DropDownListBrend.DataValueField = dt.Columns["brend"].ToString();
                        DropDownListBrend.DataSource = dt;
                        DropDownListBrend.DataBind();
                    }
                }
            }
        }
        protected void DropDownListBrend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DropDownListPol.SelectedValue))
            {
                DataGridPopulate();// Call your method to populate cmb_vrsta
            }
        }
        private void DataGridPopulate()
        {
            using (SqlConnection conn = Konekcija.Connect())
            {
                conn.Open();
                StringBuilder naredba = new StringBuilder("SELECT artikli.id, naziv, pol, brend, velicina, boja, cena, kolicina  FROM lager ");
                naredba.Append(" JOIN artikli on id_artikla = artikli.id ");
                naredba.Append(" JOIN tipovi on artikli.id_tipa = tipovi.id ");
                naredba.Append(" WHERE pol = @comboboxVrednost1 ");
                naredba.Append(" AND naziv = @comboboxVrednost2 ");
                naredba.Append(" AND brend = @comboboxVrednost3 ");
                naredba.Append(" AND kolicina > 0 ");
                SqlCommand cmd = new SqlCommand(naredba.ToString(), conn);
                using (cmd)
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@comboboxVrednost1", DropDownListPol.SelectedValue.ToString());
                        adapter.SelectCommand.Parameters.AddWithValue("@comboboxVrednost2", DropDownListVrsta.SelectedValue.ToString());
                        adapter.SelectCommand.Parameters.AddWithValue("@comboboxVrednost3", DropDownListBrend.SelectedValue.ToString());
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection veza = Konekcija.Connect();
            veza.Open();

            string artikalId = GridView1.SelectedRow.Cells[0].Text;
            string kolicina = GridView1.SelectedRow.Cells[7].Text;

            StringBuilder naredba = new StringBuilder("INSERT INTO racun_stavke (kolicina, cena, id_magacina, id_artikla, id_racuna) ");
            naredba.Append(" VALUES (@Kolicina, @Cena, @MagacinID, @ArtikalID, @RacunID)");
            SqlDataAdapter adapter = new SqlDataAdapter(naredba.ToString(), veza);
            adapter.SelectCommand.Parameters.AddWithValue("@Kolicina", 1);
            adapter.SelectCommand.Parameters.AddWithValue("@Cena", Convert.ToDouble(GridView1.SelectedRow.Cells[6].Text));
            adapter.SelectCommand.Parameters.AddWithValue("@MagacinID", 1);
            adapter.SelectCommand.Parameters.AddWithValue("@ArtikalID", Convert.ToInt32(artikalId));
            //popuniRacun(GetCurrentRacunID(veza)+1, DateTime.Now);
            adapter.SelectCommand.Parameters.AddWithValue("@RacunID", GetCurrentRacunID(veza));
            adapter.SelectCommand.ExecuteNonQuery();

            StringBuilder naredba3 = new StringBuilder("UPDATE lager SET kolicina = @Kolicina WHERE id_artikla = @ArtikalID");
            SqlDataAdapter adapter3 = new SqlDataAdapter(naredba3.ToString(), veza);
            adapter3.SelectCommand.Parameters.AddWithValue("@Kolicina", Convert.ToInt32(kolicina) - 1);
            adapter3.SelectCommand.Parameters.AddWithValue("@ArtikalID", Convert.ToInt32(artikalId));
            adapter3.SelectCommand.ExecuteNonQuery();

            DataGridPopulate();
        }
        private int GetCurrentRacunID(SqlConnection veza)
        {
            SqlCommand command = new SqlCommand("SELECT MAX(id) FROM racun", veza);
            object result = command.ExecuteScalar();
            if (result != DBNull.Value && result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return 0;
            }
        }
    }
}