using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website2
{
    public partial class Zoeken : System.Web.UI.Page
    {
        private Databaseclass Connectie = new Databaseclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            LbErrorZoek.Visible = false;
            LbErrorToevoeg.Visible = false;
        }

        protected void BtnZoeken_Click(object sender, EventArgs e)
        {
            if (TbSearch.Text != "")
            {
                string soort = DdlCategorie.SelectedItem.ToString();
                string naam = TbSearch.Text;
               
                GridViewItems.DataSource = Connectie.GetItems(soort, naam);
                GridViewItems.DataBind();
                if (GridViewItems.Rows.Count >= 1)
                {    
                    if (soort == "Personages")
                    {
                        GridViewItems.AutoGenerateSelectButton = false;
                        BtnVoegToe.Visible = false;
                    }
                    else
                    {
                        BtnVoegToe.Visible = true;
                    }
                }
                else
                {
                    LbErrorZoek.Text = "Geen items met deze naam aanwezig";
                    LbErrorZoek.ForeColor = System.Drawing.Color.Red;
                    LbErrorZoek.Visible = true;
                }
            }
            else
            {
                LbErrorZoek.ForeColor = System.Drawing.Color.Red;
                LbErrorZoek.Text = "Vul geldige informatie in";
                LbErrorZoek.Visible = true;
            }
        }

        protected void BtnVoegToe_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridViewItems.SelectedRow;
            if (row != null)
            {
                int ItemNr = Convert.ToInt32(row.Cells[1].Text);
                string Titel = row.Cells[2].Text;
                string Soort = row.Cells[5].Text;
                string email = Convert.ToString(Session["EMAIL"]);
                if (Connectie.checkLijst(email, Titel, Soort, ItemNr) == false)
                {
                    LbErrorToevoeg.Text = "Dit Item zit al in je lijst";
                    LbErrorToevoeg.ForeColor = System.Drawing.Color.Red;
                    LbErrorToevoeg.Visible = true;
                }
                else
                {
                    Connectie.VoegToe(Titel, Soort, email);
                    LbErrorToevoeg.Text = "Item is succesvol toegevoegd";
                    LbErrorToevoeg.ForeColor = System.Drawing.Color.Green;
                    LbErrorToevoeg.Visible = true;
                }
            }
            else
            {
                LbErrorToevoeg.Text = "Geen Items geselecteerd";
                LbErrorToevoeg.ForeColor = System.Drawing.Color.Red;
                LbErrorToevoeg.Visible = true;
            }
        }
    }
}