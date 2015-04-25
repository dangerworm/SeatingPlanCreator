using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatingPlanCreator
{
    public class Class
    {
        public Dictionary<string, string> IDNames;
        public Dictionary<string, string> NameIDs;

        public string ID;
        public string Name { get; set; }
        public int Size { get { return students.Count; } }
                
        private List<Student> students;

        public Class()
        {
            Initialise();
        }

        public Class(string name)
        {
            Initialise();
            Name = name;
        }

        public Class(string id, string name)
        {
            Initialise();
            ID = id;
            Name = name;
        }

        public Class(string name, List<Student> students)
        {
            Initialise();
            this.Name = name;

            foreach (var student in students)
            {
                this.students.Add(student);
            }
        }

        private void Initialise()
        {
            ID = Guid.NewGuid().ToString();
            IDNames = new Dictionary<string, string>();
            NameIDs = new Dictionary<string, string>();
            students = new List<Student>();
        }

        public string AddStudent(Student student)
        {
            if (students.Any(s => s.Name == student.Name))
            {
                student.Name += string.Format(" ({0})", students.Where(s => s.Name.StartsWith(student.Name)).Count() + 1);
            }

            IDNames.Add(student.ID, student.Name);
            NameIDs.Add(student.Name, student.ID);
            students.Add(student);

            return student.Name;
        }

        public List<Student> GetStudents()
        {
            return students;
        }

        public Dictionary<int, string> GetSeatNames()
        {
            Dictionary<int, string> seatNames = new Dictionary<int, string>();

            foreach (Student student in students)
            {
                seatNames.Add(student.Seat, student.Name);
            }

            return seatNames;
        }
        
        public bool IsReadyForRoomLayout()
        {
            if (students.Count == 0)
            {
                return false;
            }
            else
            {
                bool wwdbDataExistsForAllStudents = true;

                foreach (Student s in students)
                {
                    if (!s.HasWWData || !s.HasDBData)
                    {
                        wwdbDataExistsForAllStudents = false;
                    }
                }

                return wwdbDataExistsForAllStudents;
            }
        }

        public void RemoveStudent(Student student)
        {
            if (students.Contains(student))
            {
                IDNames.Remove(student.ID);
                NameIDs.Remove(student.Name);

                foreach (var s in students)
                {
                    s.RemoveWorksWell(student.ID);
                    s.RemoveDistractedBy(student.ID);
                }

                students.Remove(student);
            }
        }

        public override string ToString()
        {
            string classDetails = string.Format("C~{0},{1}", ID, Name);
            return classDetails;
        }
    }
}
