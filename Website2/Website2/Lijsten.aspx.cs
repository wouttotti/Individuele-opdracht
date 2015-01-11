using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website2
{
    public partial class Contact : Page
    {

        private Databaseclass Connectie = new Databaseclass();
        private string ItemNaam;
        private string ItemSoort;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                string SessionName = Convert.ToString(Session["EMAIL"]);
                int SessionID = Connectie.GetGebruikerIDEmail(SessionName);
                GridView1.DataSource = Connectie.GetLijsten(SessionID);
                GridView1.DataBind();
            }
        }

        protected void BtnVerwijder_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow == null)
            {
                LbError.Visible = true;
            }
            else
            {
                GridViewRow row = GridView1.SelectedRow;
                this.ItemNaam = row.Cells[1].Text;
                this.ItemSoort = row.Cells[2].Text;
                Connectie.DeleteLijstItem(this.ItemNaam, this.ItemSoort, Convert.ToString(Session["EMAIL"]));
                Response.Redirect("Lijsten.aspx");
            }
        }
    }
}