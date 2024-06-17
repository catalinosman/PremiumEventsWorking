using Microsoft.AspNetCore.Identity;
using PremiumEvents.API.Models.Domain;
using System.ComponentModel.DataAnnotations;
public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoPresentation { get; set; }
    public string? Genre { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
    public int? Capacity { get; set; }
    public Guid ServiceCategoryId { get; set; }
    public ServiceCategory ServiceCategory { get; set; }
    public ICollection<CityService> CityServices { get; set; }
    public string CreatedBy { get; set; }
}