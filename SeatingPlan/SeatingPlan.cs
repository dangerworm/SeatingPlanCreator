using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatingPlanCreator
{
    public class SeatingPlan
    {
        private Dictionary<int, string> Plan;
        private Dictionary<string, int> StudentSeats;
        public List<double> WScores;
        public List<double> DScores;

        public double Fitness;

        public SeatingPlan(Dictionary<int, string> plan)
        {
            SetPlan(plan);

            WScores = new List<double>();
            DScores = new List<double>();
        }

        public void CalcFitness()
        {
            double wScore = 0;
            double dScore = 0;

            foreach (var score in WScores)
            {
                wScore += score;
            }

            foreach (var score in DScores)
            {
                dScore += score;
            }

            if (!Double.IsNaN(wScore) && !Double.IsInfinity(wScore) &&
                !Double.IsNaN(dScore) && !Double.IsInfinity(dScore))
            {
                Fitness += dScore / wScore;
            }
        }

        public Dictionary<int, string> GetPlan()
        {
            return Plan;
        }

        public int GetStudentSeat(string studentID)
        {
            return StudentSeats[studentID];
        }

        public void SetStudentSeat(string studentID, int seat)
        {
            Plan.Remove(Plan.Single(p => p.Value == studentID).Key);
            Plan.Add(seat, studentID);
            StudentSeats[studentID] = seat;
        }

        public void SetPlan(Dictionary<int, string> plan)
        {
            Plan = plan;

            StudentSeats = new Dictionary<string, int>();
            foreach (var pair in plan)
            {
                StudentSeats.Add(pair.Value, pair.Key);
            }
        }

        public override string ToString()
        {
            string output = "";

            foreach (var pair in StudentSeats)
            {
                // Seats are always in ascending order
                output += string.Format("{0},", pair.Key);
            }

            return output.Substring(0, output.Length - 1);
        }
    }
}
