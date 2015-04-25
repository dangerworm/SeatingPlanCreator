using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatingPlanCreator
{
    public class Incubator
    {
        public static int ATTAINMENT = 0;
        public static int STUDENT_SETTINGS = 1;

        public bool HasPlans { get { return seatingPlans.Count > 0; } }

        private List<SeatingPlan> seatingPlans;
        private List<SeatingPlan> goodSeatingPlans;
        
        private ClassRoomSetup parent;
        private int basis;
        private int breeders;
        private int generations;
        private bool chainSeed;

        public Incubator()
        {
            Initialise();
        }

        public Incubator(int basis, int breeders, int generations, ClassRoomSetup parent)
        {
            Initialise();
            this.basis = basis;
            this.breeders = breeders;
            this.generations = generations;
            this.parent = parent;
        }

        private void Initialise()
        {
            seatingPlans = new List<SeatingPlan>();
            goodSeatingPlans = new List<SeatingPlan>();
            chainSeed = true;
        }

        public SeatingPlan GetBestSeatingPlan()
        {
            return goodSeatingPlans[0];
        }

        public int GetNumGoodSeatingPlans()
        {
            return goodSeatingPlans.Count;
        }

        public SeatingPlan GetSeatingPlan(int p)
        {
            return seatingPlans[p];
        }

        public SeatingPlan GetGoodSeatingPlan(int p)
        {
            return goodSeatingPlans[p];
        }

        public void SetGoodSeatingPlan(int p, SeatingPlan plan)
        {
            goodSeatingPlans[p] = plan;
        }

        private static int CompareChainLength(List<string> a, List<string> b)
        {
            if (a.Count > b.Count)
            {
                return -1;
            }
            else if (a.Count < b.Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private static int CompareFitness(SeatingPlan a, SeatingPlan b)
        {
            if (a.Fitness > b.Fitness)
            {
                return -1;
            }
            else if (a.Fitness < b.Fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void CalcScores()
        {
            // Match students to seats and calculate fitness
            List<Student> students = parent.GetCurrentClass().GetStudents();

            foreach (var seatingPlan in seatingPlans)
            {
                foreach (Student student in students)
                {
                    student.Seat = seatingPlan.GetStudentSeat(student.ID);
                }

                Dictionary<int, Point> seats = parent.GetCurrentRoom().GetSeats();
                seatingPlan.WScores = new List<double>();
                seatingPlan.DScores = new List<double>();

                foreach (var student in students)
                {
                    Point sLocation = seats[student.Seat];

                    double wScore = 0;
                    foreach (Student ww in students.Where(s => student.WorksWellWith(s.ID)))
                    {
                        Point wLocation = seats[ww.Seat];
                        wScore += Math.Sqrt(Math.Pow(wLocation.X - sLocation.X, 2) +
                                            Math.Pow(wLocation.Y - sLocation.Y, 2));
                    }

                    double dScore = 0;
                    foreach (Student db in students.Where(s => student.IsDistractedBy(s.ID)))
                    {
                        Point dLocation = seats[db.Seat];
                        dScore += Math.Sqrt(Math.Pow(dLocation.X - sLocation.X, 2) +
                                            Math.Pow(dLocation.Y - sLocation.Y, 2));
                    }

                    seatingPlan.WScores.Add(wScore);
                    seatingPlan.DScores.Add(Math.Pow(dScore, 2));
                }

                seatingPlan.CalcFitness();
            }
        }

        public void CalculateSeatingPlan()
        {
            seatingPlans.Clear();
            Evolve();

            parent.PlanIndex = 0;
        }

        private void Seed()
        {
            /* Generate entirely new seating plans */

            List<Student> students = parent.GetCurrentClass().GetStudents();
            Dictionary<int, string> plan;
            List<int> seatNumbers = new List<int>();

            for (int b = 0; b < breeders; b++)
            {
                // A plan is a list of seat numbers with a student ID attached
                plan = new Dictionary<int, string>();

                // Populate seat numbers 1...seatcount
                
                seatNumbers = parent.GetCurrentRoom().GetSeatIndices();

                /* 
                 * A chained seed is one where students' relationships
                 * govern the initial placement.
                 */

                if (chainSeed)
                {
                    List<string> seen = new List<string>();
                    List<string> currentChain = new List<string>();
                    List<List<string>> chains = new List<List<string>>();

                    Random rnd = new Random();

                    // Look through all students, finding the next one who works well with the current one

                    // TODO: If a valid long chain exists but contains students from a previous shorter chain,
                    // the longer chain is never created. Fix.

                    Student currentStudent = students[rnd.Next(students.Count)];
                    Student nextStudent;

                    while (seen.Count < students.Count)
                    {
                        // If we haven't seen currentStudent before, add them to the current chain
                        if (!seen.Contains(currentStudent.ID))
                        {
                            currentChain.Add(currentStudent.ID);
                            seen.Add(currentStudent.ID);
                        }

                        string randomWWID = currentStudent.GetRandomWWID();

                        nextStudent = students.Single(s => s.ID == randomWWID);

                        // If we've seen nextStudent already, get a new student and start a new chain.
                        if (seen.Contains(nextStudent.ID))
                        {
                            foreach (Student student in students)
                            {
                                if (!seen.Contains(student.ID))
                                {
                                    nextStudent = student;
                                    chains.Add(currentChain);
                                    currentChain = new List<string>();
                                    break;
                                }
                            }
                        }

                        // Make sure the new student doesn't distract anyone in the chain.
                        foreach (string id in currentChain)
                        {
                            if (nextStudent.IsDistractedBy(id))
                            {
                                chains.Add(currentChain);
                                currentChain = new List<string>();
                                break;
                            }
                        }

                        currentStudent = nextStudent;
                    }

                    chains.RemoveAll(c => c.Count < 2);

                    /* Place chains of students around tables */
                    
                    Dictionary<int, Rectangle> tables = parent.GetCurrentRoom().GetTables();
                    OrderedDictionary tableSize = new OrderedDictionary();
                    int maxSeatsAtATable = 0;
                    
                    // For loop to ensure tables are added in ascending order
                    foreach (var table in tables)
                    {
                        tableSize.Add(table.Key, parent.GetCurrentRoom().GetNumSeatsAroundTable(table.Key));

                        // Find out the maximum number of seats around a table
                        if (maxSeatsAtATable < (int)tableSize[(object)table.Key]) // (object) so key is treated as a key, not an index
                        {
                            maxSeatsAtATable = (int)tableSize[(object)table.Key];

                            // Sort the OD as you go
                            tableSize.Remove(table.Key);
                            tableSize.Insert(0, table.Key, parent.GetCurrentRoom().GetNumSeatsAroundTable(table.Key));
                        }
                    }

                    // Remove any chains longer than this and sort by length
                    chains.RemoveAll(c => c.Count > maxSeatsAtATable);
                    chains.Sort(CompareChainLength);

                    // Match chains to tables, taking into account different sizes of table,
                    // then add to plan
                    if (chains.Count > 0)
                    {
                        int chainIndex = 0;

                        foreach (var table in tables)
                        {
                            // If the chain fits around the table, add it
                            if (chains[chainIndex].Count <= (int)tableSize[(object)table.Key])
                            {
                                List<int> seats = parent.GetCurrentRoom().GetSeatIndicesAroundTable(table.Key);

                                // Add seats and students to plan
                                for (int s = 0; s < chains[chainIndex].Count; s++)
                                {
                                    plan.Add(seats[s], chains[chainIndex][s]);
                                    seatNumbers.Remove(seats[s]);
                                }

                                chainIndex++;
                            }
                            else
                            {
                                // Go through the chains to find one small enough to fit
                                while (chainIndex < chains.Count && chains[chainIndex].Count > (int)tableSize[(object)table.Key])
                                {
                                    chainIndex++;
                                }
                            }

                            // Stop if we've run out of chains
                            if (chainIndex == chains.Count)
                            {
                                break;
                            }
                        }
                    }

                    // For any students left over, put each in the first available seat
                    foreach (Student student in students)
                    {
                        if (!plan.ContainsValue(student.ID))
                        {
                            plan.Add(seatNumbers[0], student.ID);
                            seatNumbers.RemoveAt(0);
                        }
                    }
                }
                else
                {
                    Random rnd = new Random();

                    foreach (Student student in students)
                    {
                        int seat = rnd.Next(seatNumbers.Count);
                        plan.Add(seatNumbers[seat], student.ID);
                        seatNumbers.RemoveAt(seat);
                    }
                }

                seatingPlans.Add(new SeatingPlan(plan));
            }
        }

        public void Breed()
        {
            List<SeatingPlan> tmpSeatingPlans;

            do
            {
                CalcScores();
                seatingPlans.Sort(CompareFitness);

                tmpSeatingPlans = new List<SeatingPlan>();

                //Swap out
                foreach (var seatingPlan in seatingPlans)
                {
                    tmpSeatingPlans.Add(seatingPlan);
                }

                // Remove seating plans that are the same
                double last = Double.MaxValue;
                foreach (var seatingPlan in tmpSeatingPlans)
                {
                    if (seatingPlan.Fitness < last)
                    {
                        last = seatingPlan.Fitness;
                    }
                    else if (seatingPlan.Fitness == last)
                    {
                        seatingPlans.Remove(seatingPlan);
                    }
                }

                // Remove all but the top <breeders> seating plans...
                if (seatingPlans.Count > breeders)
                {
                    for (int b = breeders; b < seatingPlans.Count; b++)
                    {
                        seatingPlans.RemoveAt(b);
                    }
                }
                // ...or fill up to quota
                else
                {
                    Seed();
                }
            }
            while (seatingPlans.Count < breeders); // In case many were the same

            CalcScores();

            tmpSeatingPlans.Clear();
            foreach (var seatingPlan in seatingPlans)
            {
                tmpSeatingPlans.Add(seatingPlan);
            }

            /* Breeding - crossover plus mutation */
            Random rnd = new Random();
            Dictionary<int, string>[] newPlans = new Dictionary<int, string>[2];

            int planSize = 0; // Efficiency (see below)

            // Crossover

            Dictionary<int, string>[] plan = new Dictionary<int, string>[2];
            List<int>[] planSeats = new List<int>[2];
            List<string>[] planStudents = new List<string>[2];

            seatingPlans.Clear();

            for (int p = 0; p < tmpSeatingPlans.Count - 2; p += 2) // -2 stops overflow
            {
                // Take two plans

                // Break link between students and seats and generate splice point
                for (int i = 0; i < 2; i++)
                {
                    plan[i] = tmpSeatingPlans[p + i].GetPlan();
                    planSeats[i] = new List<int>();
                    planStudents[i] = new List<string>();

                    foreach (var pairing in plan[i])
                    {
                        planSeats[i].Add(pairing.Key);
                        planStudents[i].Add(pairing.Value);
                    }
                }

                planSize = planSeats[0].Count; // Efficiency

                // Create a splice point somwhere in the first quarter of the plans.
                // Any more and we run the risk of just swapping the lists completely.
                int splicePoint = rnd.Next(planSize / 4);

                // Combine/crossover seat indices
                int tmpSeatNum = -1;
                int swapIndex = -1;

                for (int s = 0; s < splicePoint; s++)
                {
                    // Find what index in planSeats[0] holds the same seat number as planSeats[1][s]
                    for (int s2 = s; s2 < planSize; s2++)
                    {
                        if (planSeats[0][s2] == planSeats[1][s])
                        {
                            swapIndex = s2;
                            break;
                        }
                    }

                    if (swapIndex > -1) // The seat may no longer exist in the latter part of the plan
                    {
                        // Swap those seats
                        tmpSeatNum = planSeats[0][s];
                        planSeats[0][s] = planSeats[0][swapIndex];
                        planSeats[0][swapIndex] = tmpSeatNum;

                        // Swap again so the number that was in planSeats[0][s] goes in planSeats[1][s]
                        // by swapping the seats in planSeat[1]

                        for (int s2 = s; s2 < planSize; s2++)
                        {
                            if (planSeats[1][s2] == tmpSeatNum)
                            {
                                swapIndex = s2;
                                break;
                            }
                        }

                        // Swap those seats
                        tmpSeatNum = planSeats[1][s];
                        planSeats[1][s] = planSeats[1][swapIndex];
                        planSeats[1][swapIndex] = tmpSeatNum;
                    }
                }

                // Mutation 

                for (int i = 0; i < 2; i++)
                {
                    int randomMatch = rnd.Next(planSize);

                    for (int s = 0; s < planSize; s++)
                    {
                        // Mutation probability per seat is 1 / planSize
                        if (rnd.Next(planSize) == randomMatch)
                        {
                            int[] swapIndices = new int[2];

                            for (int j = 0; j < 2; j++)
                            {
                                swapIndices[j] = rnd.Next(planSize);
                            }

                            // Make sure indices are different
                            while (swapIndices[0] == swapIndices[1])
                            {
                                swapIndices[1] = rnd.Next(planSize);
                            }

                            int tmpSeat = planSeats[i][swapIndices[0]];
                            planSeats[i][swapIndices[0]] = planSeats[i][swapIndices[1]];
                            planSeats[i][swapIndices[1]] = tmpSeat;
                        }
                    }

                    // Recombine plans
                    newPlans[i] = new Dictionary<int, string>();
                    for (int s = 0; s < planSize; s++)
                    {
                        newPlans[i].Add(planSeats[i][s], planStudents[i][s]);
                    }

                    seatingPlans.Add(new SeatingPlan(newPlans[i]));
                }
            }
        }

        private bool CheckSeatsAreDistinct(List<int> plan)
        {
            List<int> plan2 = plan.Distinct().ToList();

            bool problemFound = false;
            if (plan.Count != plan2.Count)
            {
                problemFound = true;
            }

            return problemFound;
        }

        private void Evolve()
        {
            double maxFitness = 0;

            StreamWriter output = new StreamWriter(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                                 "Seating Plan Creator Files",
                                 "Evolution Data",
                                 DateTime.Today.Date.Year + "-" +
                                 DateTime.Today.Date.Month + "-" + DateTime.Today.Date.Day + " " +
                                 DateTime.Now.Hour + "-" + DateTime.Now.Minute + " " +
                                 parent.GetCurrentClass().Name + " in " + parent.GetCurrentRoom().Name + ".csv"));

            output.WriteLine("Generation,Max Fitness");

            for (int i = 0; i <= generations; i++)
            {
                if (i == 0)
                {
                    seatingPlans.Clear();
                    Seed();
                }
                else
                {
                    Breed();
                }

                CalcScores();
                seatingPlans.Sort(CompareFitness);

                SeatingPlan bestOfCurrentSeatingPlans = null;
                foreach (var seatingPlan in seatingPlans)
                {
                    if (seatingPlan.Fitness > maxFitness)
                    {
                        maxFitness = seatingPlan.Fitness;
                        bestOfCurrentSeatingPlans = seatingPlan;

                        if (!goodSeatingPlans.Contains(seatingPlan))
                        {
                            goodSeatingPlans.Insert(0, seatingPlan);
                        }
                    }
                }

                output.Write(i + ",");
                output.WriteLine(maxFitness);

                parent.SetProgressBarValue(i);

                int bestIndex = seatingPlans.IndexOf(bestOfCurrentSeatingPlans);
                if (bestIndex >= 0)
                {
                    parent.PlanIndex = bestIndex;
                }
                else
                {
                    parent.PlanIndex = 0;
                }

                parent.UpdateSeats();
                parent.UpdateRoomLayoutImage();
                Application.DoEvents();
            }

            goodSeatingPlans.Sort(CompareFitness);
            goodSeatingPlans = goodSeatingPlans.Take(10).ToList();

            foreach (var plan in goodSeatingPlans)
            {
                output.WriteLine(plan.ToString());
            }

            output.Close();
        }
    }
}
