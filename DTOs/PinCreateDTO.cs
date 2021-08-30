using System.ComponentModel.DataAnnotations;

namespace MapboxApi.DTOs
{
    public class PinCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
