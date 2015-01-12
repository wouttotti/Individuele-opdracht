using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;
using System.Web.Configuration;

namespace Website2
{
    public class Databaseclass
    {
        private OracleConnection connectie = new OracleConnection();
        private string conn = WebConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

        //In de onderstaande methode wordt er een connectie gemaakt met mijn Oracle Database.
        public void ConnectieOpen() 
        {
            try
            {
                connectie = new OracleConnection();
                connectie.ConnectionString = WebConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                connectie.Open();
            }
            catch
            {
                connectie.Close();
            }
        }

        // In de onderstaande methode ga het Gebruiker_ID aanvragen van het meegeleverde Email
        public int GetGebruikerIDEmail(string Email)
        {
            int ID = 0;
            string sql = "SELECT GEBRUIKER_ID FROM GEBRUIKER where Email = '" + Email + "'";
            try
            {
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ID = Convert.ToInt32(reader["GEBRUIKER_ID"]);
                }
            }
            catch
            {
                connectie.Close();
            }
            return ID;
        }


        //In de onderstaande methode ga ik kijken of er in de database of de email die ik meegeef hetzelfde is als de email die al in de database staat.
        //Als de email al in de database zit geeft de methode False terug.
        public bool CheckEmail( string email)
        {
            List<string> Emails = new List<string>();
            string sql = "SELECT EMAIL FROM GEBRUIKER";
            try
            {
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Emails.Add(Convert.ToString(reader["EMAIL"]));
                }

                foreach (string Temp in Emails)
                {
                    if (Temp == email)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                connectie.Close();
            }
            return true;
        }


        //Hier word er gechecked of het wachtwoord wel overeen komt met het wachtwoord van de gebruiker
        public bool CheckWachtwoord(string wachtwoord, string email)
        {
            string sql = "SELECT WACHTWOORD FROM GEBRUIKER WHERE EMAIL = :Email";
            try
            {
                List<string> Wachtwoorden = new List<string>();
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                cmd.Parameters.Add(":Email", email);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Wachtwoorden.Add(Convert.ToString(reader["WACHTWOORD"]));
                }

                foreach (string Temp in Wachtwoorden)
                {
                    if(Temp == wachtwoord)
                    {
                        return true;
                    }
                }

            }
            catch
            {
                connectie.Close();
            }
            return false;
        }


        //In de onderstaande methode haal ik het aantal gebruikers op die er op de site zitten.
        public int AantalGebruikers()
        {
            List<int> Gebruikers = new List<int>();
            string sql = "SELECT GEBRUIKER_ID FROM GEBRUIKER";
            int count = 0;
            try
            {
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Gebruikers.Add(Convert.ToInt32(reader["GEBRUIKER_ID"]));
                }
                foreach (int Temp in Gebruikers)
                {
                    count += 1;
                }
            }
            catch
            {
                connectie.Close();
            }
            return count;
        }


        //In de methode ga ik een nieuwe gebruiker toevoegen aan de datbase.
        public void RegisterGebruiker(string Naam, string Email, string Wachtwoord, string GeboorteDatum)
        {
           
            string sqlNieuwAccount = "INSERT INTO GEBRUIKER (NAAM, EMAIL, WACHTWOORD, GEBOORTEDATUM) VALUES(:Naam, :Email, :Wachtwoord, :Geboortedatum)";
            try
            {
                ConnectieOpen();
                OracleCommand cmdNieuwAccount = new OracleCommand(sqlNieuwAccount, connectie);

                cmdNieuwAccount.Parameters.Add(":Naam", Naam);
                cmdNieuwAccount.Parameters.Add(":Email", Email);
                cmdNieuwAccount.Parameters.Add(":Wachtwoord", Wachtwoord);
                cmdNieuwAccount.Parameters.Add(":Geboortedatum", GeboorteDatum);
                
                cmdNieuwAccount.ExecuteNonQuery();
            }
            catch
            {
                connectie.Close();
            }
        }

        //In deze methode wordt de lijst van een gebruiker opgehaald
        public DataSet GetLijsten( int gebruiker_ID)
        {
            string sql = "SELECT distinct i.TITEL, i.SOORT from LIJST l, ITEM i, GEBRUIKER g WHERE l.GEBRUIKER = :Gebruiker AND l.ITEM = i.ITEMNR";
            OracleCommand cmd = new OracleCommand(sql, connectie);
            cmd.Parameters.Add(":Gebruiker", gebruiker_ID);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            ConnectieOpen();

            DataSet ds = new DataSet();

            try
            {
                // Fill the DataSet.
                adapter.Fill(ds);

            }
            catch (OracleException e)
            {
                // The connection failed. Display an error message            
            }
            finally
            {
                connectie.Close();
            }
            return ds;
        }


        //Hier wordt een item uit de lijst van de gebruiker verwijderd
        public void DeleteLijstItem(string Naam, string Soort, string gebruikersnaam)
        {
            string sql = "DELETE FROM LIJST WHERE ITEM = (SELECT ITEMNR FROM ITEM WHERE TITEL = :Naam and SOORT = :Soort) AND GEBRUIKER = (SELECT GEBRUIKER_ID FROM GEBRUIKER WHERE EMAIL = :Gebruikersnaam)";
           
            try
            {
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                cmd.Parameters.Add(":Naam", Naam);
                cmd.Parameters.Add(":Soort", Soort);
                cmd.Parameters.Add(":Gebruikersnaam", gebruikersnaam);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                connectie.Close();
            }
        }

        //Hier worden Items uit de database gehaald om zo in de Gridview te kunnen zetten
        public DataSet GetItems(string soort, string naam)
        {
            string sql = "Select i.ITEMNR, i.TITEL, i.JAAR, i.GEMIDDELDESCORE, i.SOORT FROM ITEM i where i.SOORT = :soort and i.TITEL = :naam";
            if(soort == "Personage")
            {
                sql = "SELECT p.Naam, i.TITEL, p.Kenmerken, p.Tags from Personages p, ITEM i WHERE p.Naam = 'Alice' and p.ITEMNR = i.ITEMNR";
            }
            ConnectieOpen();
            OracleCommand cmd = new OracleCommand(sql, connectie);
            cmd.Parameters.Add(":soort", soort);
            cmd.Parameters.Add(":naam", naam);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                // Fill the DataSet.
                

                
                adapter.Fill(ds);

            }
            catch (OracleException e)
            {
                // The connection failed. Display an error message            
            }
            finally
            {
                connectie.Close();
            }
            return ds;
        }

        //Hierin wordt gekeken of er een item al aanwezig is in een lijst zodat er niet twee dezelfde toegevoegd kunnen worden
        public bool checkLijst(string email, string naam, string soort, int ItemNr)
        {
            List<int> Items = new List<int>();
            int ItemID = GetItemID(naam, soort);
            int Gebruiker = GetGebruikerIDEmail(email);
            string sql = "SELECT i.ITEMNR FROM ITEM i, LIJST l WHERE l.ITEM = i.ITEMNR AND l.GEBRUIKER = :Gebruiker and i.ITEMNR = :itemNr";
            try
            {
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                cmd.Parameters.Add(":Gebruiker", Gebruiker);
                cmd.Parameters.Add(":ItemNr", ItemID);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Items.Add(Convert.ToInt32(reader["ITEMNR"]));
                }

                foreach(int Temp in Items)
                {
                    if (Temp == ItemNr)
                    {
                        return false;
                    }
                }
            }
            catch
            {
                connectie.Close();
            }
            return true;
        }

        //Hier wordt het ID van een item opgehaald door gebruik te maken van de naam en de soort
        public int GetItemID(string naam, string soort)
        {
            string sql = "SELECT ITEMNR FROM ITEM WHERE TITEL = :Naam AND SOORT = :Soort";
            int ItemId = 0;

            try
            {
                ConnectieOpen();
                OracleCommand cmd = new OracleCommand(sql, connectie);
                cmd.Parameters.Add(":Naam", naam);
                cmd.Parameters.Add(":Soort", soort);
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ItemId = Convert.ToInt32(reader["ITEMNR"]);
                }
            }
            catch
            {
                connectie.Close();
            }
            return ItemId;
        }

        //Hier wordt er een nieuw item toegevoegd aan de lijst van een gebruiker
        public bool VoegToe(string naam, string soort, string email)
        {
            int Gebruiker = GetGebruikerIDEmail(email);
            int ItemNr = GetItemID(naam, soort);
            string sqlNieuwAccount = "INSERT INTO LIJST (LIJST_ID, GEBRUIKER, ITEM) VALUES(1, :Gebruiker, :ITEM)";

            try
            {
                ConnectieOpen();
                OracleCommand cmdVoegToe = new OracleCommand(sqlNieuwAccount, connectie);
                cmdVoegToe.Parameters.Add(":Gebruiker", Gebruiker);
                cmdVoegToe.Parameters.Add(":ITEM", ItemNr);

                cmdVoegToe.ExecuteNonQuery();
            }
            catch
            {
                connectie.Close();
            }
            return true;
        }
    }
}