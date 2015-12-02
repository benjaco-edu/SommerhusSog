using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SommerhusSog.Models;
using SommerhusSog.ViewModels;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void AlleResultaterAntal() {
            Sogside side = new Sogside();
            side.UpdateCount();
            Assert.AreEqual(side.AntalReultater, 6 + " resultater");
        }
        [TestMethod]
        public void AlleResultater()
        {
            Sogside side = new Sogside();
            side.Soeg();
            Assert.AreEqual(side.SoegteHuse[0].Navn, "Hus #1");
        }
        [TestMethod]
        public void LandResultater()
        {
            Sogside side = new Sogside();
            side.Sog_Sted = 1;
            side.Soeg();
            foreach (Hus hus in side.SoegteHuse) {
                Assert.AreEqual(hus.Land, "Frankrig");
            }
            Assert.AreEqual(side.AntalReultater, 4 + " resultater");
        }

        [TestMethod]
        public void LandPrisResultater()
        {
            Sogside side = new Sogside();
            side.Sog_Sted = 1;
            side.Sog_Pris = "5000";
            side.Soeg();
            foreach (Hus hus in side.SoegteHuse) {
                Assert.AreEqual(hus.Land, "Frankrig");
                Assert.IsTrue(hus.Pris<=5000);
            }
            Assert.AreEqual(side.AntalReultater, 3 + " resultater");
        }

        [TestMethod]
        public void LandPrisVaeerlserResultater()
        {
            Sogside side = new Sogside();
            side.Sog_Sted = 1;
            side.Sog_Pris = "5000";
            side.Sog_AntalVaerelser = "2";
            side.Soeg();
            foreach (Hus hus in side.SoegteHuse) {
                Assert.AreEqual(hus.Land, "Frankrig");
                Assert.IsTrue(hus.Pris<=5000);
                Assert.IsTrue(hus.AntalVaerelser>=2);
            }
            Assert.AreEqual(side.AntalReultater, 2 + " resultater");
        }
        [TestMethod]
        public void LandPrisVaeerlserNavnResultater()
        {
            Sogside side = new Sogside();
            side.Sog_Sted = 1;
            side.Sog_Pris = "5000";
            side.Sog_AntalVaerelser = "2";
            side.Sog_Navn = "Hus #2";
            side.Soeg();
            foreach (Hus hus in side.SoegteHuse) {
                Assert.AreEqual(hus.Land, "Frankrig");
                Assert.IsTrue(hus.Pris<=5000);
                Assert.IsTrue(hus.AntalVaerelser>=2);
                Assert.AreEqual(hus.Navn, "Hus #2");
            }
            Assert.AreEqual(side.AntalReultater, 1 + " resultater");
        }

    }
}
