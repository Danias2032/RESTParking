using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parkfinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkfinder.Tests
{
    [TestClass()]
    public class ParkeringsområdeTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            Parkeringsområde pO = new Parkeringsområde { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
            string result = pO.ToString();

            Assert.AreEqual("Id: 1, LedigParkeringsplads: 5, Parkeringsnavn: Pavillion 1, Dag: 2021-01-22T08:38:00", result);

            pO.Dag = null;
            result = pO.ToString();
            Assert.AreEqual("Id: 1, LedigParkeringsplads: 5, Parkeringsnavn: Pavillion 1, Dag: N/A", result);
        }

        [TestMethod()]
        public void ValidateParkeringsnavnTest()
        {
            Parkeringsområde pO = new Parkeringsområde { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
            pO.ValidateParkeringsnavn();

            Assert.AreEqual("Pavillion 1", pO.Parkeringsnavn);

            pO.Parkeringsnavn = null;
            Assert.ThrowsException<ArgumentNullException>(() => pO.ValidateParkeringsnavn());

            pO.Parkeringsnavn = new string('a', 101);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pO.ValidateParkeringsnavn());
        }
            
        [TestMethod()]
        public void ValidateDateTest()
        {
            Parkeringsområde pO = new Parkeringsområde { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
            pO.ValidateDate();

            Assert.AreEqual(new DateTime(2021, 01, 22, 8, 38, 0), pO.Dag);

            pO.Dag = new DateTime(2019, 12, 31);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pO.ValidateDate());

            pO.Dag = new DateTime(2025, 1, 1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pO.ValidateDate());

            pO.Dag = null;
            Assert.ThrowsException<ArgumentNullException>(() => pO.ValidateDate());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            Parkeringsområde pO = new Parkeringsområde { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
            pO.Validate();

            Assert.AreEqual("Pavillion 1", pO.Parkeringsnavn);
            Assert.AreEqual(new DateTime(2021, 01, 22, 8, 38, 0), pO.Dag);

            pO.Parkeringsnavn = null;
            Assert.ThrowsException<ArgumentNullException>(() => pO.Validate());

            pO.Parkeringsnavn = "Pavillion 1";
            pO.Dag = new DateTime(2019, 12, 31);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pO.Validate());

            pO.Dag = new DateTime(2021, 01, 22, 8, 38, 0);
            pO.Parkeringsnavn = new string('a', 101);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => pO.Validate());

            pO.Parkeringsnavn = "Pavillion 1";
            pO.Dag = null;
            Assert.ThrowsException<ArgumentNullException>(() => pO.Validate());
        }
    }

}
