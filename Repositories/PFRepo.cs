using System.Xml.Linq;
using Parkfinder.Models;

namespace Parkfinder.RESTParking.Repositories
{
    public class PFRepo
    {
        private int _nextId = 6;
        private List<Parkeringsområde> _parkingList = new List<Parkeringsområde>
        {
            new Parkeringsområde { Id = 1, Ledig_parkeringsplads = 5, Parkeringsnavn = "Pavillion 1", Dag = new DateTime(2021, 01, 22, 8, 38, 0) },
            new Parkeringsområde { Id = 2, Ledig_parkeringsplads = 30, Parkeringsnavn = "Pavillion 2", Dag = new DateTime(2022, 01, 18, 8, 19, 18) },
            new Parkeringsområde { Id = 3, Ledig_parkeringsplads = 59, Parkeringsnavn = "Pavillion 3", Dag = new DateTime(2023, 01, 11, 8, 20, 24) },
            new Parkeringsområde { Id = 4, Ledig_parkeringsplads = 32, Parkeringsnavn = "Pavillion 4", Dag = new DateTime(2024, 01, 15, 8, 29, 10) },
        };

        public IEnumerable<Parkeringsområde> GetParkingList(DateTime? date = null, string? orderby = null)
        {
            IEnumerable<Parkeringsområde> result = new List<Parkeringsområde>(_parkingList);
            if (date != null)
            {
                result = result.Where(pf => pf.Dag.HasValue && pf.Dag.Value.Date == date.Value.Date);
            }

            if (orderby != null)
            {
                switch (orderby)
                {
                    case "LedigParkeringsplads":
                        result = result.OrderBy(pf => pf.Ledig_parkeringsplads);
                        break;
                    case "Parkeringsnavn":
                        result = result.OrderBy(pf => pf.Parkeringsnavn);
                        break;
                    case "Dag":
                        result = result.OrderBy(pf => pf.Dag);
                        break;
                    case "Id":
                        result = result.OrderBy(pf => pf.Id);
                        break;
                    case "LedigParkeringspladsDesc":
                        result = result.OrderByDescending(pf => pf.Ledig_parkeringsplads);
                        break;
                    case "ParkeringsnavnDesc":
                        result = result.OrderByDescending(pf => pf.Parkeringsnavn);
                        break;
                    case "DagDesc":
                        result = result.OrderByDescending(pf => pf.Dag);
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(pf => pf.Id);
                        break;
                }
            }
            return result;
        }

        public Parkeringsområde? GetID(int id)
        {
            return _parkingList.Find(pS => pS.Id == id);
        }

        public Parkeringsområde Add(Parkeringsområde pS)
        {
            pS.Validate();
            pS.Id = _nextId++;
            _parkingList.Add(pS);
            return pS;
        }

        public Parkeringsområde? Remove(int id)
        {
            Parkeringsområde? existingPS = GetID(id);
            if (existingPS == null)
            {
                return null;
            }
            _parkingList.Remove(existingPS);
            return existingPS;
        }

        public Parkeringsområde? Update(int id, Parkeringsområde pS)
        {
            pS.Validate();
            Parkeringsområde? existingPS = GetID(id);
            if (existingPS == null)
            {
                return null;
            }
            existingPS.Ledig_parkeringsplads = pS.Ledig_parkeringsplads;
            existingPS.Parkeringsnavn = pS.Parkeringsnavn;
            existingPS.Dag = pS.Dag;
            return existingPS;
        }

        public override string ToString()
        {
            return string.Join(",", _parkingList);
        }
    }
}
