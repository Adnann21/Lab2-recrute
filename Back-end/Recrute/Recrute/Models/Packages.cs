using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Recrute.Models
{
    public class Packages
    {
        [BsonId]
        [BsonElement("id"),BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

       
        [BsonElement("Lloji"), BsonRepresentation(BsonType.String)]

        public string Lloji { get; set; }
        
        [BsonElement("Qmimi"), BsonRepresentation(BsonType.Double)]

        public Double Qmimi { get; set; }
       
    }
}
