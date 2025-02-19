using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data_Transfer_API.Model
{
    public class User_Info
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Basic user information
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]  // Store hashed password, never plain text!
        public string PasswordHash { get; set; }

        // Optional fields
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
