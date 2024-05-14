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
    }
}