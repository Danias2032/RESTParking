namespace Parkfinder
{
    public class PFRepoDB
    {
        private readonly PFDBContext _context;

        public PFRepoDB(PFDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Parkeringsområde> GetParkingList(DateTime? date = null, string? orderBy = null)
        {
            IEnumerable<Parkeringsområde> result = new List<Parkeringsområde>(_context.Parkeringsområde);

            if (date != null)
            {
                result = result.Where(pS => pS.Dag.HasValue && pS.Dag.Value.Date == date.Value.Date);
            }

            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "LedigParkeringsplads":
                        result = result.OrderBy(pS => pS.Ledig_parkeringsplads);
                        break;
                    case "Parkeringsnavn":
                        result = result.OrderBy(pS => pS.Parkeringsnavn);
                        break;
                    case "Dag":
                        result = result.OrderBy(pS => pS.Dag);
                        break;
                    case "Id":
                        result = result.OrderBy(pS => pS.Id);
                        break;
                    case "LedigParkeringspladsDesc":
                        result = result.OrderByDescending(pS => pS.Ledig_parkeringsplads);
                        break;
                    case "ParkeringsnavnDesc":
                        result = result.OrderByDescending(pS => pS.Parkeringsnavn);
                        break;
                    case "DagDesc":
                        result = result.OrderByDescending(pS => pS.Dag);
                        break;
                    case "IdDesc":
                        result = result.OrderByDescending(pS => pS.Id);
                        break;
                }
            }

            return result.ToList();
        }

        public Parkeringsområde? GetID(int id)
        {
            return _context.Parkeringsområde.FirstOrDefault(pS => pS.Id == id);
        }

        public Parkeringsområde Add(Parkeringsområde pS)
        {
            pS.Validate();
            _context.Parkeringsområde.Add(pS);
            _context.SaveChanges();
            return pS;
        }

        public Parkeringsområde? Delete(int id)
        {
            Parkeringsområde? pS = GetID(id);
            if (pS == null)
            {
                return null;
            }
            _context.Parkeringsområde.Remove(pS);
            _context.SaveChanges();
            return pS;
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
            _context.SaveChanges();
            return existingPS;
        }
    }
}
