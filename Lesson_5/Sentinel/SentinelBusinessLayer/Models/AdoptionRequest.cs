namespace SentinelBusinessLayer.Models
{
    public class AdoptionRequest
    {
        public Guid PetId { get; set; }
        public Guid CustomerId { get; set; }
    }
}