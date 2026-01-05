using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MyAssessment.Models
{
    public class Grade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("studentId")]
        public string StudentId { get; set; }

        [BsonElement("competencyId")]
        public string CompetencyId { get; set; }

        [BsonElement("assessmentType")]
        public string AssessmentType { get; set; } // UH, PTS, PAS, Praktik, Proyek, Portofolio

        [BsonElement("scores")]
        public List<double> Scores { get; set; } = new List<double>();

        [BsonElement("finalScore")]
        public double FinalScore { get; set; }

        [BsonElement("predicate")]
        public string Predicate { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("semester")]
        public int Semester { get; set; }

        [BsonElement("academicYear")]
        public string AcademicYear { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}