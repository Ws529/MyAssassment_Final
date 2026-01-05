using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAssessment.Services
{
    public static class GradeCalculationService
    {
        public static double CalculateFinalScore(List<double> scores, string assessmentType)
        {
            if (scores == null || !scores.Any()) return 0;

            switch (assessmentType.ToUpper())
            {
                case "UH": // Ulangan Harian - Average
                    return scores.Average();
                case "PTS": // Penilaian Tengah Semester
                case "PAS": // Penilaian Akhir Semester
                    return scores.LastOrDefault();
                case "PRAKTIK":
                case "PROYEK":
                case "PORTOFOLIO":
                    return scores.Average();
                default:
                    return scores.Average();
            }
        }

        public static string GetPredicate(double finalScore, int kkm)
        {
            if (finalScore >= 90) return "A";
            if (finalScore >= 80) return "B";
            if (finalScore >= kkm) return "C";
            return "D";
        }

        public static string GetDescription(string predicate)
        {
            switch (predicate)
            {
                case "A": return "Sangat Baik";
                case "B": return "Baik";
                case "C": return "Cukup";
                case "D": return "Kurang";
                default: return "Belum Dinilai";
            }
        }
    }
}