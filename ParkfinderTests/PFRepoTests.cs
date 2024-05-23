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
    public class PFRepoTests
    {

        private PFRepo _parkingList;
        private Parkeringsområde pMTest = new Parkeringsområde { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 4", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };

        [TestInitialize]
        public void Initialize()
        {
            _parkingList = new PFRepo();
        }

        [TestMethod]
        public void GetParkingListTest()
        {
            var parkingMeasurements = _parkingList.GetParkingList();
            Assert.AreEqual(4, parkingMeasurements.Count());

            var parkingMeasurementsDate = _parkingList.GetParkingList(orderby: "Dag");
            Assert.AreEqual(4, parkingMeasurementsDate.Count());
            Assert.AreEqual(parkingMeasurementsDate.First().Dag, DateTime.Parse("2021-01-22T08:38:00"));

            var parkingMeasurementsLedigParkeringsplads = _parkingList.GetParkingList(orderby: "LedigParkeringsplads");
            Assert.AreEqual(4, parkingMeasurementsLedigParkeringsplads.Count());
            Assert.AreEqual(parkingMeasurementsLedigParkeringsplads.First().Ledig_parkeringsplads, 5);

            var parkingMeasurementsParkeringsnavn = _parkingList.GetParkingList(orderby: "Parkeringsnavn");
            Assert.AreEqual(4, parkingMeasurementsParkeringsnavn.Count());
            Assert.AreEqual(parkingMeasurementsParkeringsnavn.First().Parkeringsnavn, "Pavillion 1");

            var parkingMeasurementsId = _parkingList.GetParkingList(orderby: "Id");
            Assert.AreEqual(4, parkingMeasurementsId.Count());
            Assert.AreEqual(parkingMeasurementsId.First().Id, 1);

            var parkingMeasurementsLedigParkeringspladsDesc = _parkingList.GetParkingList(orderby: "LedigParkeringspladsDesc");
            Assert.AreEqual(4, parkingMeasurementsLedigParkeringspladsDesc.Count());
            Assert.AreEqual(parkingMeasurementsLedigParkeringspladsDesc.First().Ledig_parkeringsplads, 59);

            var parkingMeasurementsParkeringsnavnDesc = _parkingList.GetParkingList(orderby: "ParkeringsnavnDesc");
            Assert.AreEqual(4, parkingMeasurementsParkeringsnavnDesc.Count());
            Assert.AreEqual(parkingMeasurementsParkeringsnavnDesc.First().Parkeringsnavn, "Pavillion 4");

            var parkingMeasurementsDateDesc = _parkingList.GetParkingList(orderby: "DagDesc");
            Assert.AreEqual(4, parkingMeasurementsDateDesc.Count());
            Assert.AreEqual(parkingMeasurementsDateDesc.First().Dag, DateTime.Parse("2024-01-15T08:29:10"));

            var parkingMeasurementsIdDesc = _parkingList.GetParkingList(orderby: "IdDesc");
            Assert.AreEqual(4, parkingMeasurementsIdDesc.Count());
            Assert.AreEqual(parkingMeasurementsIdDesc.First().Id, 4);
        }

        [TestMethod]
        public void GetIDTest()
        {
            Parkeringsområde? pM = _parkingList.GetID(1);
            Assert.IsNotNull(pM);
            Assert.AreEqual(1, pM.Id);
            Assert.IsNull(_parkingList.GetID(100));
        }

        [TestMethod]
        public void AddTest()
        {
            _parkingList.Add(pMTest);
            IEnumerable<Parkeringsområde> parkingMeasurements = _parkingList.GetParkingList();
            Assert.AreEqual(5, parkingMeasurements.Count());
            Assert.IsNotNull(_parkingList.GetID(6));
        }

        [TestMethod]
        public void RemoveTest()
        {
            _parkingList.Remove(4);
            IEnumerable<Parkeringsområde> parkingMeasurements = _parkingList.GetParkingList();
            Assert.AreEqual(3, parkingMeasurements.Count());
            Assert.IsNull(_parkingList.GetID(4));
            Assert.IsNull(_parkingList.Remove(100));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Parkeringsområde? pM = _parkingList.Update(1, pMTest);
            Assert.IsNotNull(pM);
            Assert.AreEqual(1, pM.Id);
            Assert.AreEqual(5, pM.Ledig_parkeringsplads);
            Assert.AreEqual("Pavillion 4", pM.Parkeringsnavn);
            Assert.AreEqual(new DateTime(2021, 01, 22, 8, 38, 0), pM.Dag);
            Assert.IsNull(_parkingList.Update(100, pMTest));
        }

        [TestMethod]
        public void ToStringTest()
        {
            string str = _parkingList.ToString();
            Assert.IsTrue(str.Contains("Id: 1, LedigParkeringsplads: 5, Parkeringsnavn: Pavillion 1, Dag: 2021-01-22T08:38:00"));
        }
    }

}

