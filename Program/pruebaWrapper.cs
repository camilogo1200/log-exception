using System.ComponentModel.DataAnnotations;

namespace Program
{
    public class PruebaWrapper
    {
        [Required]
        public int pru1 { get; set; }

        public string pru2 { get; set; }
        public bool pru3 { get; set; }
    }
}