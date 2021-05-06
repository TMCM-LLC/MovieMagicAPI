using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieMagic.Models {
    public class Movie {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        [Required]
        [Range(0, 5)]
        [BsonElement("Rating")]
        public int Rating { get; set; }
        [Required]
        [BsonElement("ExternalId")]
        public string ExternalId { get; set; }
    }
}