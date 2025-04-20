using DAL_Core.Enums;

namespace SentinelBusinessLayer.Models
{
    public class PetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public PetTypes Type { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
    }
}