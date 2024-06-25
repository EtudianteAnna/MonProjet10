using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DiabetesRiskAssessment.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("PatientId")]
        public string PatientId { get; set; }

        [BsonElement("DoctorId")]
        public string DoctorId { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}