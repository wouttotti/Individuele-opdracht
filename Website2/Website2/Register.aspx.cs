using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Website2
{
    public partial class Register : System.Web.UI.Page
    {
        private Databaseclass Connectie = new Databaseclass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            LbError.Visible = false;
            LbError.ForeColor = System.Drawing.Color.Red;
            if(TbEmail.Text == null || TbNaam.Text == null || TbWachtwoord.Text == null || cGeboortedatum.SelectedDate == null)
            {
                LbError.Text = "Vul geldige Informatie in";
                LbError.Visible = true;
            }
            else
            {
                if (Connectie.CheckEmail(TbEmail.Text) == false)
                {
                    LbError.Text = "Deze email is al in gebruik";
                    LbError.Visible = true;
                }
                else
                {
                    string dt = cGeboortedatum.SelectedDate.ToShortDateString();
                    Connectie.RegisterGebruiker(TbNaam.Text, TbEmail.Text, TbWachtwoord.Text, dt);
                    LbError.Text = "Account is aangemaakt";
                    LbError.ForeColor = System.Drawing.Color.Green;
                }
                
            }
        }
    }
}