namespace MapboxApi.DTOs
{
    public class PinReadDTO
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
