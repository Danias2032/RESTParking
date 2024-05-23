namespace Parkfinder.Models
{
    public class Parkeringsområde
    {
        public int Id { get; set; }
        public int Ledig_parkeringsplads { get; set; }
        public string? Parkeringsnavn { get; set; }
        public DateTime? Dag { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + ", LedigParkeringsplads: " + Ledig_parkeringsplads + ", Parkeringsnavn: " + Parkeringsnavn + ", Dag: " + (Dag.HasValue ? Dag.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "N/A");
        }

        public void ValidateParkeringsnavn()
        {
            if (string.IsNullOrWhiteSpace(Parkeringsnavn))
            {
                throw new ArgumentNullException("Parkeringsnavn is required");
            }

            if (Parkeringsnavn.Length > 15)
            {
                throw new ArgumentOutOfRangeException("Parkeringsnavn must be 100 characters or fewer");
            }
        }

        public void ValidateDate()
        {
            if (Dag == null)
            {
                throw new ArgumentNullException("Dag is required");
            }

            if (Dag.Value.Year < 2020 || Dag.Value.Year > 2024)
            {
                throw new ArgumentOutOfRangeException("Dag must be between 2020 and 2024");
            }
        }

        public void Validate()
        {
            ValidateParkeringsnavn();
            ValidateDate();
        }

    }
}
