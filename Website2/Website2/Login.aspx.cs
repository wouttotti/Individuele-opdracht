using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website2
{
    public partial class Login : System.Web.UI.Page
    {
        private Databaseclass Connectie = new Databaseclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["EMAIL"] != null)
            {
                LbError.Text = "U bent ingelogd";
                LbError.ForeColor = System.Drawing.Color.Green;
                LbError.Visible = true;
                BtnUitlog.Visible = true;
            }
        }

        protected void BtnInlog_Click(object sender, EventArgs e)
        {
            LbError.Visible = false;
            if (TbEmail.Text == "" || TbWachtwoord.Text == "")
            {
                LbError.Text = "Vul geldige informatie in";
                LbError.ForeColor = System.Drawing.Color.Red;
                LbError.Visible = true;
            }
            else if (Connectie.CheckEmail(TbEmail.Text) == true)
            {
                LbError.Text = "Deze Email bestaat niet";
                LbError.ForeColor = System.Drawing.Color.Red;
                LbError.Visible = true;
            }
            else if (Connectie.CheckWachtwoord(TbWachtwoord.Text, TbEmail.Text) == false)
            {
                LbError.Text = "Wachtwoord komt niet overeen met de email";
                LbError.ForeColor = System.Drawing.Color.Red;
                LbError.Visible = true;
            }
            else
            {
                Session["EMAIL"] = TbEmail.Text;
                Response.Redirect("Login.aspx");
            }
        }

        protected void BtnUitlog_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}