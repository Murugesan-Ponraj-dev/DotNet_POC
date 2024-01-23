using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Entities;

public class Product
{
    [BsonElement("id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    [Required]
    public string? Name { get; set; }

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("brand")]
    public string? Brand { get; set; }

    [BsonElement("price")]
    public decimal? Price { get; set; } 

    [BsonElement("reviews")]
    public IEnumerable<Reviews> Reviews { get; }
}

public class Reviews
{
    [BsonElement("id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("comments")]
    [Required]
    public string? Comments { get; set; }

    [BsonElement("rating")]
    [Required]
    public int Rating { get; set; }

    [BsonElement("reviewedby")]
    public string? Reviewedby { get; set; } 
}