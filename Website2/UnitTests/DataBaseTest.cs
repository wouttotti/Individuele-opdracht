using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website2;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;

namespace UnitTests
{
    [TestClass]
    public class DataBaseTest
    {
        Databaseclass TestClasse = new Databaseclass();
        [TestMethod]

        //Deze Test checked of de meegegeven email al bestaat of niet.
        //Als de Email bestaat geeft hij false terug
        public void TestCheckEmail()
        {
            Assert.IsFalse(TestClasse.CheckEmail("wout_kamp@hotmail.com"));

            Assert.IsTrue(TestClasse.CheckEmail("Jake_De_Hond@Adventure.Time"));
        }

        //Deze Test checked of het wachtwoord klopt bij de bijbehorden email.
        //Als het wachtwoord klopt geeft hij True terug.
        [TestMethod]
        public void TestCheckWachtwoord()
        {
            Assert.IsTrue(TestClasse.CheckWachtwoord("12345", "wout_kamp@hotmail.com"));

            Assert.IsFalse(TestClasse.CheckWachtwoord("11111", "wout_kamp@hotmail.com"));
        }
       
   
        //Deze test kijkt of de methode het goed ItemID teruggeeft.
          [TestMethod]
        public void TestGetItemID()
        {
            int TestItemID_1 = TestClasse.GetItemID("Pandora Hearts", "Anime");
            Assert.AreEqual(1, TestItemID_1);

            int TestItemID_2 = TestClasse.GetItemID("Hellsing", "Anime");
            Assert.AreEqual(4, TestItemID_2);
            
            int TestItemID_3 = TestClasse.GetItemID("Deadman Wonderland", "Manga");
            Assert.AreEqual(6, TestItemID_3);

        }

        //Deze test kijk of er een item al in een lijst aanwezig is zo ja dan returned hij false.
        //Als het item nog niet aanwezig is returned hij true.
        [TestMethod]
        public void TestCheckLijsten()
          {
            Assert.IsFalse(TestClasse.checkLijst("wout_kamp@hotmail.com", "Pandora Hearts", "Anime", 1));
            Assert.IsFalse(TestClasse.checkLijst("harold@gmail.nl", "Deadman Wonderland", "Manga", 6));

            Assert.IsTrue(TestClasse.checkLijst("wout_kamp@hotmail.com", "Naruto", "Anime", 1));
            Assert.IsTrue(TestClasse.checkLijst("harold@gmail.nl", "Hellsing", "Anime", 2));

          }

        //Deze test kijkt of de methode door middel van een email de goeie Gebruiker ID krijgt.
        [TestMethod]
        public void TestGetGebruikerID()
        {
            int GebruikerID = TestClasse.GetGebruikerIDEmail("wout_kamp@hotmail.com");
            Assert.AreEqual(1, GebruikerID);

            int GebruikerID2 = TestClasse.GetGebruikerIDEmail("harold@gmail.nl");
            Assert.AreEqual(2, GebruikerID2);
        }
    
    }
}
