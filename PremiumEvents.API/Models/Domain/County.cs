using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PremiumEvents.API.Models.Domain
{
    public class County
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> City { get; set; }
    }
}