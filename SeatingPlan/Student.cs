using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatingPlanCreator
{
    public class Student
    {
        public bool HasWWData { get { return WorksWell.Count > 0; } }
        public bool HasDBData { get { return DistractedBy.Count > 0; } }

        public string ID;
        public string Name;
        public string Gender;
        public DateTime DateOfBirth;

        public int Seat;

        private List<string> WorksWell;
        private List<string> DistractedBy;

        public Student(string data, bool nameOnly)
        {
            Initialise();

            if (nameOnly)
            {
                ID = Guid.NewGuid().ToString();
                Name = data;
            }
            else
            {
                string[] blobs = data.Split("~".ToCharArray());
                string[] details = blobs[1].Split(",".ToCharArray());

                ID = details[0];
                Name = details[1];
                Gender = details[2];
                DateOfBirth = DateTime.Parse(details[3]);

                if (blobs.Length > 2)
                {
                    string[] ww = blobs[2].Split(",".ToCharArray());
                    foreach (string w in ww)
                    {
                        if (!string.IsNullOrEmpty(w))
                        {
                            AddWorksWell(w);
                        }
                    }
                }

                if (blobs.Length > 3)
                {
                    string[] db = blobs[3].Split(",".ToCharArray());
                    foreach (string d in db)
                    {
                        if (!string.IsNullOrEmpty(d))
                        {
                            AddDistractedBy(d);
                        }
                    }
                }
            }
        }

        public Student(string name, string worksWell, string distractedBy)
        {
            Initialise();
            ID = Guid.NewGuid().ToString();
            Name = name;
            AddRelationshipDetails(worksWell, distractedBy);
        }

        public Student(string id, string name, string worksWell, string distractedBy)
        {
            Initialise();
            ID = id;
            Name = name;
            AddRelationshipDetails(worksWell, distractedBy);
        }

        public Student(string name, string gender, DateTime dob)
        {
            Initialise();
            ID = Guid.NewGuid().ToString();
            Name = name;
            Gender = gender;
            DateOfBirth = dob;
        }

        private void Initialise()
        {
            WorksWell = new List<string>();
            DistractedBy = new List<string>();
        }

        public void CopyProperties(Student newStudent)
        {
            Name = newStudent.Name;
            Gender = newStudent.Gender;
            DateOfBirth = newStudent.DateOfBirth;

            Seat = newStudent.Seat;

            WorksWell.Clear();
            foreach (string ww in newStudent.WorksWell)
                AddWorksWell(ww);

            DistractedBy.Clear();
            foreach (string db in newStudent.DistractedBy)
                AddDistractedBy(db);
        }

        public void AddWorksWell(string id)
        {
            if (!WorksWell.Contains(id))
            {
                WorksWell.Add(id);
            }
        }

        public void AddDistractedBy(string id)
        {
            if (!DistractedBy.Contains(id))
            {
                DistractedBy.Add(id);
            }
        }

        public void AddRelationshipDetails(string wwID, string dbID)
        {
            AddWorksWell(wwID);
            AddDistractedBy(dbID);
        }

        public List<string> GetWWList()
        {
            return WorksWell;
        }

        public List<string> GetDBList()
        {
            return DistractedBy;
        }

        public string GetRandomWWID()
        {
            Random rnd = new Random();
            return WorksWell[rnd.Next(WorksWell.Count)];
        }

        public string GetRandomDBID()
        {
            Random rnd = new Random();
            return DistractedBy[rnd.Next(DistractedBy.Count)];
        }

        public void RemoveWorksWell(int index)
        {
            WorksWell.RemoveAt(index);
        }

        public void RemoveWorksWell(string id)
        {
            if (WorksWell.Contains(id))
            {
                WorksWell.Remove(id);
            }
        }

        public void RemoveDistractedBy(int index)
        {
            DistractedBy.RemoveAt(index);
        }

        public void RemoveDistractedBy(string id)
        {
            if (DistractedBy.Contains(id))
            {
                DistractedBy.Remove(id);
            }
        }

        public bool IsDistractedBy(string id)
        {
            return DistractedBy.Contains(id);
        }

        public bool WorksWellWith(string id)
        {
            return WorksWell.Contains(id);
        }
        
        public override string ToString()
        {
            string sStudent = string.Format("S~{0},{1},{2},{3}~", ID, Name, Gender, DateOfBirth);

            foreach (string ww in WorksWell)
            {
                sStudent += ww + ",";
            }

            sStudent = sStudent.Substring(0, sStudent.Length - 1) + "~";

            foreach (string db in DistractedBy)
            {
                sStudent += db + ",";
            }

            return sStudent.Substring(0, sStudent.Length - 1) + "~";
        }
    }
}