var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/* namespace Parkfinder.Tests
{
    [TestClass()]
    public class Parkeringsomr�deTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            Parkeringsomr�de pO = new Parkeringsomr�de { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
            string result = pO.ToString();

            Assert.AreEqual("Id: 1, LedigParkeringsplads: 5, Parkeringsnavn: Pavillion 1, Dag: 2021-01-22T08:38:00", result);

            pO.Dag = null;
            result = pO.ToString();
            Assert.AreEqual("Id: 1, LedigParkeringsplads: 5, Parkeringsnavn: Pavillion 1, Dag: N/A", result);
        }

        [TestMethod()]
        public void ValidateParkeringsnavnTest()
        {
            Parkeringsomr�de pO = new Parkeringsomr�de { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
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
            Parkeringsomr�de pO = new Parkeringsomr�de { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
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
            Parkeringsomr�de pO = new Parkeringsomr�de { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };
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
*/

/* namespace Parkfinder.Tests
{
    [TestClass()]
    public class PFRepoTests
    {

        private PFRepo _parkingList;
        private Parkeringsomr�de pMTest = new Parkeringsomr�de { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 4", Dag = new DateTime(2021, 01, 22, 8, 38, 0) };

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
            Parkeringsomr�de? pM = _parkingList.GetID(1);
            Assert.IsNotNull(pM);
            Assert.AreEqual(1, pM.Id);
            Assert.IsNull(_parkingList.GetID(100));
        }

        [TestMethod]
        public void AddTest()
        {
            _parkingList.Add(pMTest);
            IEnumerable<Parkeringsomr�de> parkingMeasurements = _parkingList.GetParkingList();
            Assert.AreEqual(5, parkingMeasurements.Count());
            Assert.IsNotNull(_parkingList.GetID(6));
        }

        [TestMethod]
        public void RemoveTest()
        {
            _parkingList.Remove(4);
            IEnumerable<Parkeringsomr�de> parkingMeasurements = _parkingList.GetParkingList();
            Assert.AreEqual(3, parkingMeasurements.Count());
            Assert.IsNull(_parkingList.GetID(4));
            Assert.IsNull(_parkingList.Remove(100));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Parkeringsomr�de? pM = _parkingList.Update(1, pMTest);
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
*/