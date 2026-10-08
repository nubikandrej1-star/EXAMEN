using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;

namespace EXAMEN.Domain.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Car>? Cars { get; set; }
    }
}
