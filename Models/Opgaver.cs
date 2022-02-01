/**
 * Opgaver Model
 */
namespace WebApp.Models {
    public class Opgaver {

        public int id { get; set; }
        public string navn { get; set; }

        public int minAlder { get; set; }
        
        public string medarbejder { get; set; }
    }
}
