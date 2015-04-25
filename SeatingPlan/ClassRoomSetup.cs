using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SeatingPlanCreator
{
    public partial class ClassRoomSetup : Form
    {
        private Class CurrentClass;
        private Room CurrentRoom;
        private Incubator Incubator;
        private SeatingPlan CurrentSeatingPlan;
        public int PlanIndex;

        public bool DisplayLinks { get { return Incubator != null && Incubator.HasPlans; } }
        public bool DisplayStudents { get { return Incubator.HasPlans && !Evolving; } }
        public bool Evolving;

        private static int ALLDATA = 0;
        private static int USERDATA = 1;
        private static int CLASSDATA = 2;
        private static int ROOMDATA = 3;
        private static int CLASS = 4;
        private static int ROOM = 5;

        private List<User> users;
        private List<Room> rooms;
        private List<Class> classes;
        private List<Tuple<string, string>> classRooms;
        private Dictionary<int, string> listStudents;
        private Dictionary<int, string> listWW;
        private Dictionary<int, string> listDB;

        private User currentUser;
        private Student currentStudent;

        private PasswordPrompt prompt;
        private RoomSetup roomSetup;

        private bool changesMade;
        private bool refreshingTree;
        private bool highlightNextSteps;

        private int flashes;

        private string quickSaveFileName;
        private string sorcerer;

        public ClassRoomSetup()
        {
            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            users = new List<User>();
            Incubator = new Incubator();

            dlgOpenFile.CheckFileExists = true;
            dlgOpenFile.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Seating Plan Creator Files");
            dlgSaveFile.InitialDirectory = dlgOpenFile.InitialDirectory;

            ClearData();
            ClearForm(false);
            changesMade = false;

            flashes = 0;

            quickSaveFileName = Path.Combine(dlgOpenFile.InitialDirectory, "data.sps");

            if (!Directory.Exists(dlgOpenFile.InitialDirectory))
            {
                Directory.CreateDirectory(dlgOpenFile.InitialDirectory);
                Directory.CreateDirectory(Path.Combine(dlgOpenFile.InitialDirectory, "Evolution Data"));
            }

            if (File.Exists(quickSaveFileName))
            {
                ReadSPSFile(quickSaveFileName, USERDATA);
            }
        }

        private void ClearData()
        {
            if (rooms == null)
                rooms = new List<Room>();
            else
                rooms.Clear();

            if (classes == null)
                classes = new List<Class>();
            else
                classes.Clear();

            if (listStudents == null)
                listStudents = new Dictionary<int, string>();
            else
                listStudents.Clear();

            if (listWW == null)
                listWW = new Dictionary<int, string>();
            else
                listWW.Clear();

            if (listDB == null)
                listDB = new Dictionary<int, string>();
            else
                listDB.Clear();

            if (classRooms == null)
                classRooms = new List<Tuple<string, string>>();
            else
                classRooms.Clear();

            CurrentRoom = null;
            CurrentClass = null;
            currentStudent = null;
            Incubator = null;

            users = new List<User>();
            rooms = new List<Room>();
            classes = new List<Class>();
            classRooms = new List<Tuple<string,string>>();
            listStudents = new Dictionary<int,string>();
            listWW = new Dictionary<int, string>();
            listDB = new Dictionary<int,string>();

            grpLayout.BackgroundImage = new Bitmap(grpLayout.Width, grpLayout.Height);
        }

        private void ClearForm(bool trySave)
        {
            if (trySave)
            {
                AskWhetherToSave();
            }

            trvClassesRooms.Nodes.Clear();
            grpGroups.Enabled = false;

            ClearClass();
            grpClass.Enabled = false;

            ClearRoom();
            grpRoomLayout.Enabled = false;
        }

        private void ClearClass()
        {
            txtClassName.Clear();
            ClearStudent();
            lstStudents.Items.Clear();
            lstWorksWell.Items.Clear();
            lstDistractedBy.Items.Clear();
        }

        private void ClearStudent()
        {
            lblName.Text = "";
            lblDoB.Text = "";
            lblAge.Text = "";
        }

        private void ClearRoom()
        {
            txtRoomName.Clear();
            txtColumns.Text = "1";
            txtRows.Text = "1";
            txtTableDepth.Text = "1";
            txtTableWidth.Text = "1";

            if (grpLayout.BackgroundImage != null)
            {
                Graphics.FromImage(grpLayout.BackgroundImage).Clear(SystemColors.Control);
            }

            Incubator = null;
        }

        public Class GetCurrentClass()
        {
            return CurrentClass;
        }

        public Room GetCurrentRoom()
        {
            return CurrentRoom;
        }

        public Incubator GetIncubator()
        {
            return Incubator;
        }

        public SeatingPlan GetCurrentSeatingPlan()
        {
            return CurrentSeatingPlan;
        }
        
        private void Login()
        {
            prompt = new PasswordPrompt(this, users, !users.Any(u => u.Username == Environment.UserName));
            prompt.Show();
        }

        public void DoLogin(bool newUser, string username, string password)
        {
            if (newUser)
            {
                currentUser = new User(username, password);
                users.Add(currentUser);
                SaveData(false);
                RefreshForm();
            }
            else
            {
                if (users.Any(u => u.Username == username))
                {
                    User tmpUser = users.Single(u => u.Username == username);
                    if (tmpUser.PasswordIs(password))
                    {
                        currentUser = tmpUser;
                        ReadSPSFile(quickSaveFileName, ALLDATA);
                        RefreshForm();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password. Please try again.", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        prompt.ClearPassword();
                        return;
                    }
                }
            }

            prompt.Dispose();
            loginToolStripMenuItem.Text = "&Logout";
        }

        private void Logout()
        {
            ClearForm(true);
            string prevUser = currentUser.Username;
            currentUser = null;
            loginToolStripMenuItem.Text = "&Login";
            stsText.Text = string.Format("{0} logged out", prevUser);
        }



        private void OpenData(int dataType, bool clearCurrentData)
        {
            dlgOpenFile.Use(f =>
            {
                if (dataType == ALLDATA)
                {
                    f.FileName = "data.sps";
                    f.Filter = "Seating Plan Setup Files (*.sps)|*.sps";

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (clearCurrentData)
                        {
                            ClearData();
                            ClearForm(false);
                        }
                        ReadSPSFile(f.FileName, dataType);
                    }
                }
                if (dataType == CLASSDATA)
                {
                    f.FileName = "Student Names.txt";
                    f.Filter = "Seating Plan Setup Files (*.sps)|*.sps|Comma Separated Value Files (*.csv)|*.csv|Text Files (*.txt)|*.txt";

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        switch (Path.GetExtension(f.FileName))
                        {
                            case ".sps":
                                ReadSPSFile(f.FileName, dataType);
                                break;
                            case ".csv":
                                ReadStudentCSVFile(f.FileName);
                                break;
                            case ".txt":
                                ReadStudentTXTFile(f.FileName);
                                break;
                            default:
                                break;
                        }
                    }
                }
            });
        }

        private void ReadSPSFile(string path, int dataType)
        {
            if (File.Exists(path))
            {
                StreamReader input = new StreamReader(path);

                int numStudents = 0;
                string line;
                while (!string.IsNullOrEmpty(line = input.ReadLine()))
                {
                    if (line.StartsWith("U~") && (dataType == USERDATA || dataType == ALLDATA))
                    {
                        User tmpUser = new User(line);

                        if (!users.Any(u => u.Username == tmpUser.Username))
                        {
                            users.Add(new User(line));
                        }
                    }

                    if (line.StartsWith("CR~") && (dataType == ROOMDATA || dataType == ALLDATA))
                    {
                        string[] data = line.Substring(3, line.Length - 4).Split(",".ToCharArray());

                        for (int d = 0; d < data.Length; d += 2)
                        {
                            Tuple<string, string> cr = new Tuple<string, string>(data[d], data[d + 1]);

                            if (!classRooms.Contains(cr))
                            {
                                classRooms.Add(cr);
                            }
                        }
                    }

                    if (line.StartsWith("C~") && (dataType == CLASSDATA || dataType == ALLDATA))
                    {
                        string[] data = line.Substring(2).Split(",".ToCharArray());

                        Class tmpClass = new Class(data[0], data[1]);

                        while (!string.IsNullOrEmpty(line = input.ReadLine()) && line.StartsWith("S~"))
                        {
                            tmpClass.AddStudent(new Student(line, false));
                            numStudents++;
                        }

                        CurrentClass = tmpClass;
                        AddCurrentClass();
                    }

                    if (line.StartsWith("R~") && (dataType == ROOMDATA || dataType == ALLDATA))
                    {
                        Room tmpRoom = new Room(line, false);

                        if (!rooms.Any(r => r.ID == tmpRoom.ID))
                        {
                            CurrentRoom = tmpRoom;
                            AddCurrentRoom();
                        }
                    }
                }

                input.Close();

                if (users.Count > 0)
                {
                    stsText.Text = string.Format("Loaded {0} user{1}", users.Count, users.Count == 1 ? "" : "s");
                }
                if (classes.Count > 0)
                {
                    stsText.Text += string.Format(", {0} class{1}", classes.Count, classes.Count == 1 ? "" : "es");
                }
                if (numStudents > 0)
                {
                    stsText.Text += string.Format(", {0} student{1}", numStudents, numStudents == 1 ? "" : "s");
                }
                if (rooms.Count > 0)
                {
                    stsText.Text += string.Format(" and {0} room layout{1}", rooms.Count, rooms.Count == 1 ? "" : "s");
                }
            }
        }

        private void ReadStudentTXTFile(string path)
        {
            CurrentClass = new Class();

            StreamReader studentNames = new StreamReader(path);
            string line = "";
            while (!string.IsNullOrEmpty(line = studentNames.ReadLine()))
            {
                if (line.Contains(','))
                {
                    string[] data = line.Split(",".ToCharArray());
                    CurrentClass.AddStudent(new Student(data[1].Trim() + " " + data[0].Trim(), true));
                }
                else
                {
                    CurrentClass.AddStudent(new Student(line.Trim(), true));
                }
            }

            studentNames.Close();

            if (CurrentClass.Size > 0)
            {
                stsText.Text = string.Format("Loaded {0} student{1}", CurrentClass.Size, CurrentClass.Size == 1 ? "" : "s");
            }
            else
            {
                CurrentClass = null;
                stsText.Text = string.Format("Could not load class data");
                tmrFlashStatus.Enabled = true;
            }

            AddCurrentClass();
        }

        private void ReadStudentCSVFile(string path)
        {
            Dictionary<string, string> tmpStudentIDs = new Dictionary<string, string>();
            List<Student> tmpStudents;
            Class tmpClass = new Class();
            Student tmpStudent;
            StreamReader studentData = new StreamReader(path);
            string line = studentData.ReadLine();
            string[] data = line.Split(",".ToCharArray());

            if (data[0] == "Name" && data[1].StartsWith("Works") && data[2].StartsWith("Distracted"))
            {
                // Read in student names only, assign them an ID and store to tmpStudents
                while (!string.IsNullOrEmpty(line = studentData.ReadLine()))
                {
                    data = line.Split(",".ToCharArray());
                    tmpStudent = new Student(data[0], true);

                    // AddStudent returns the name of a student, with " (<n>)" appended to it if it has been changed
                    tmpStudentIDs.Add(tmpClass.AddStudent(tmpStudent), tmpStudent.ID);
                }

                studentData.Close();

                // Re-open file and read in student data, using studentIDs to match up WW and DB fields
                // and assigning all duplicate student names where necessary
                studentData = new StreamReader(path);
                studentData.ReadLine();

                tmpStudents = tmpClass.GetStudents();
                tmpClass = new Class();

                foreach (Student s in tmpStudents)
                {
                    line = studentData.ReadLine();
                    data = line.Split(",".ToCharArray());

                    if (!string.IsNullOrEmpty(data[1]))
                    {
                        if (tmpStudentIDs.Keys.Any(ts => ts.StartsWith(data[1]) && ts.Length > data[1].Length))
                        {
                            // Duplicate student name found. Find all duplicate students' IDs and add them to WorksWell
                            foreach (var dup in tmpStudentIDs.Keys.Where(d => d.StartsWith(data[1]) && d.Length >= data[1].Length))
                            {
                                s.AddWorksWell(tmpStudentIDs[dup]);
                            }
                        }
                        else
                        {
                            s.AddWorksWell(tmpStudentIDs[data[1]]);
                        }
                    }
                    if (!string.IsNullOrEmpty(data[2]))
                    {
                        if (tmpStudentIDs.Keys.Any(ts => ts.StartsWith(data[2]) && ts.Length > data[2].Length))
                        {
                            // As above but to DistractedBy
                            foreach (var dup in tmpStudentIDs.Keys.Where(d => d.StartsWith(data[2]) && d.Length >= data[2].Length))
                            {
                                s.AddDistractedBy(tmpStudentIDs[dup]);
                            }
                        }
                        else
                        {
                            s.AddDistractedBy(tmpStudentIDs[data[2]]);
                        }
                    }

                    tmpClass.AddStudent(s);
                }
            }
            else
            {
                studentData.ReadLine();
                line = studentData.ReadLine();
                data = line.Split(",".ToCharArray());

                if (data[0] == "Full Name" && data[1] == "Gender" && data[2] == "Date of Birth")
                {
                    while (!string.IsNullOrEmpty(line = studentData.ReadLine()))
                    {
                        data = line.Replace("\"","").Split(",".ToCharArray());

                        DateTime dob = DateTime.MinValue;
                        DateTime.TryParse(data[3], out dob);

                        // data[2] is gender
                        tmpClass.AddStudent(new Student(data[1].Trim() + " " + data[0].Trim(), data[2], dob));
                    }
                }
            }

            tmpStudents = tmpClass.GetStudents();
            CurrentClass = tmpClass;
        }

        private void AddCurrentClass()
        {
            if (CurrentClass != null)
            {
                UpdateClasses();
                grpClass.Enabled = true;
            }
        }

        private void AddCurrentRoom()
        {
            if (CurrentRoom != null)
            {
                UpdateRooms();

                grpRoomLayout.Enabled = true;
                txtRoomName.Use(rn =>
                {
                    rn.Text = CurrentRoom.Name;
                    rn.Focus();
                    rn.SelectAll();
                });
            }
        }
        


        private void AskWhetherToSave()
        {
            if (changesMade)
            {
                DialogResult result = MessageBox.Show("Do you want to save your changes?", "Save changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveData(false);
                }
            }
        }

        private void SaveData(bool openSaveDialog)
        {
            dlgSaveFile.Use(f =>
            {
                if (openSaveDialog)
                {
                    f.FileName = "data.sps";
                    f.Filter = "Seating Plan Setup Files (*.sps)|*.sps";

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        WriteData(f.FileName);
                        quickSaveFileName = f.FileName;
                    }
                }
                else
                {
                    WriteData(quickSaveFileName);
                }
            });

            stsText.Text = "Data saved successfully";
            changesMade = false;
        }

        private void WriteData(string path)
        {
            StreamWriter output = new StreamWriter(path);

            foreach (var user in users)
            {
                output.WriteLine(user.ToString());
            }

            if (classRooms.Any())
            {
                output.Write("CR~");
                foreach (var classRoom in classRooms)
                {
                    output.Write(string.Format("{0},{1},", classRoom.Item1, classRoom.Item2));
                }

                output.WriteLine();
            }

            foreach (var cClass in classes)
            {
                output.WriteLine(cClass.ToString());

                foreach (var student in cClass.GetStudents())
                {
                    output.WriteLine(student.ToString());
                }

                output.WriteLine();
            }

            foreach (var room in rooms)
            {
                output.WriteLine(room.ToString());
            }

            output.Close();
        }



        private void UpdateLists()
        {
            // Must be done in this order
            UpdateRooms();
            UpdateStudents();
            UpdateUsers();
        }

        private void UpdateUsers()
        {
            if (currentUser != null)
            {
                if (users.Any(u => u.Username == currentUser.Username))
                {
                    users.Remove(users.Single(u => u.Username == currentUser.Username));
                }

                users.Add(currentUser);
            }

            changesMade = true;
        }

        private void UpdateClasses()
        {
            if (CurrentClass != null)
            {
                if (classes.Any(c => c.ID == CurrentClass.ID))
                {
                    classes.Remove(classes.Single(c => c.ID == CurrentClass.ID));
                }

                classes.Add(CurrentClass);
            }

            changesMade = true;
        }

        private void UpdateStudents()
        {
            Student matchingStudent = CurrentClass.GetStudents().Single(s => s.ID == currentStudent.ID);

            if (currentStudent != null && currentStudent != matchingStudent)
            {
                CurrentClass.GetStudents().Single(s => s.ID == currentStudent.ID).Use(cs =>
                {
                    cs.CopyProperties(currentStudent);
                });
            }

            UpdateClasses();
            lstStudents.SelectedItem = currentStudent.Name;
        }

        private void UpdateRooms()
        {
            if (CurrentRoom != null)
            {
                if (!classRooms.Any(cr => cr.Item2 == CurrentRoom.ID))
                {
                    classRooms.Add(new Tuple<string, string>(CurrentClass.ID, CurrentRoom.ID));
                }

                if (rooms.Any(r => r.ID == CurrentRoom.ID))
                {
                    rooms.Remove(rooms.Single(r => r.ID == CurrentRoom.ID));
                }

                rooms.Add(CurrentRoom);
            }

            changesMade = true;
        }



        private void LoadClass(string classID)
        {
            if (CurrentClass == null || CurrentClass.ID != classID)
            {
                CurrentClass = null;
                ClearClass();
                grpClass.Enabled = false;
                CurrentClass = classes.Single(c => c.ID == classID);
                RefreshClassData();
                grpClass.Enabled = true;
            }
        }

        private void LoadRoom(string roomID)
        {
            if (CurrentRoom == null || CurrentRoom.ID != roomID)
            {
                CurrentRoom = null;
                ClearRoom();
                grpRoomLayout.Enabled = false;
                CurrentRoom = rooms.Single(r => r.ID == roomID);
                RefreshRoomData();
                grpRoomLayout.Enabled = true;
            }
        }

        private void RefreshForm()
        {
            grpGroups.Enabled = true;

            if (classes.Count == 0)
            {
                rdoClass.Enabled = false;
                rdoRoom.Enabled = false;
                btnAddClass.Enabled = true;
                btnAddRoom.Enabled = false;

                ClearClass();
                grpClass.Enabled = false;

                ClearRoom();
                grpRoomLayout.Enabled = false;

                btnAddClass.Focus();
            }
            else
            {
                rdoClass.Enabled = true;
                rdoRoom.Enabled = rooms.Count > 0;

                refreshingTree = true;
                RefreshTree();
                refreshingTree = false;

                RefreshClassData();

                if (rooms.Count > 0)
                {
                    var goodRooms = classRooms.Where(cr => cr.Item1 == CurrentClass.ID);
                    
                    if (goodRooms.Count() > 0)
                    {
                        CurrentRoom = rooms.Single(r => r.ID == goodRooms.First().Item2);
                    }
                    else
                    {
                        CurrentRoom = null;
                    }

                    RefreshRoomData();
                }
            }
        }

        private void RefreshClassData()
        {
            if (classes.Count > 0)
            {
                grpClass.Enabled = true;

                if (!string.IsNullOrEmpty(CurrentClass.Name))
                {
                    txtClassName.Text = CurrentClass.Name;
                }

                int selectedStudentIndex = lstStudents.SelectedIndex;

                lstStudents.Items.Clear();
                listStudents = new Dictionary<int, string>();

                foreach (var student in CurrentClass.GetStudents())
                {
                    listStudents.Add(lstStudents.Items.Add(student.Name), student.ID);
                }

                lstWorksWell.Items.Clear();
                lstDistractedBy.Items.Clear();

                if (lstStudents.Items.Count > 0 && selectedStudentIndex >= 0)
                {
                    RefreshStudentData(selectedStudentIndex);
                    lstStudents.SelectedIndex = selectedStudentIndex;
                }
                else
                {
                    ClearStudent();
                }

                CheckClassReadyForRoomLayout();
            }
            else
            {
                ClearClass();
                grpClass.Enabled = false;
            }
        }

        private void RefreshStudentData(int studentIndex)
        {
            if (studentIndex < listStudents.Count)
            {
                currentStudent = CurrentClass.GetStudents().Single(s => s.ID == listStudents[studentIndex]);

                currentStudent.Use(s =>
                {
                    lblName.Text = s.Name;

                    if (!string.IsNullOrEmpty(s.Gender))
                    {
                        if (s.Gender.ToLower().StartsWith("m"))
                        {
                            picSilhouette.Image = Properties.Resources.boy;
                        }
                        else
                        {
                            picSilhouette.Image = Properties.Resources.girl;
                        }
                    }

                    if (s.DateOfBirth > DateTime.MinValue)
                    {
                        lblDoB.Text = string.Format("DoB: {0}", s.DateOfBirth.ToShortDateString());
                        lblAge.Text = string.Format("Age: {0}", Convert.ToInt32((DateTime.Now - s.DateOfBirth).TotalDays / 365.25));
                    }

                    listWW.Clear();
                    lstWorksWell.Items.Clear();
                    foreach (string id in s.GetWWList())
                    {
                        listWW.Add(lstWorksWell.Items.Add(CurrentClass.IDNames[id]), id);
                    }

                    listDB.Clear();
                    lstDistractedBy.Items.Clear();
                    foreach (string id in s.GetDBList())
                    {
                        listDB.Add(lstDistractedBy.Items.Add(CurrentClass.IDNames[id]), id);
                    }
                });
            }
        }

        private void RefreshRoomData()
        {
            if (CurrentRoom == null)
            {
                ClearRoom();
                grpRoomLayout.Enabled = false;
            }
            else
            {
                txtRoomName.Text = CurrentRoom.Name;

                if (CurrentRoom.HasBeenEdited)
                {
                    btnResetRoom.Enabled = true;
                    pnlRoomSettings.Enabled = false;
                }
                else
                {
                    btnResetRoom.Enabled = false;
                    pnlRoomSettings.Enabled = false;
                    txtColumns.Text = CurrentRoom.NumColumns.ToString();
                    txtRows.Text = CurrentRoom.NumRows.ToString();
                    txtTableWidth.Text = CurrentRoom.TableWidth.ToString();
                    txtTableDepth.Text = CurrentRoom.TableDepth.ToString();
                    pnlRoomSettings.Enabled = true;
                    txtRoomName.Focus();
                    UpdateRoomLayoutImage();
                }
            }
        }

        private void RefreshTree()
        {
            if (classes.Count > 0)
            {
                grpGroups.Enabled = true;
                trvClassesRooms.Nodes.Clear();

                if (rdoClass.Enabled && rdoClass.Checked)
                {
                    foreach (var cClass in classes)
                    {
                        if (cClass.Name == null)
                        {
                            cClass.Name = string.Format("Class of {0}", cClass.Size);
                        }

                        TreeNode tn = new TreeNode();
                        tn.Tag = cClass.ID;
                        trvClassesRooms.Nodes.Add(tn);

                        foreach (var classRoom in classRooms.Where(cr => cr.Item1 == cClass.ID))
                        {
                            TreeNode tn2 = new TreeNode();
                            tn2.Tag = classRoom.Item2;
                            tn.Nodes.Add(tn2);
                            RefreshTreeNodeText(tn2);
                        }

                        RefreshTreeNodeText(tn);
                    }
                }
                else if (rdoRoom.Enabled && rdoRoom.Checked)
                {
                    List<Class> tmpClasses = new List<Class>();
                    foreach (Class c in classes)
                    {
                        tmpClasses.Add(c);
                    }

                    foreach (var room in rooms)
                    {
                        if (room.Name == null)
                        {
                            room.Name = string.Format("Room for {0}", room.NumSeats);
                        }

                        TreeNode tn = new TreeNode();
                        tn.Tag = room.ID;
                        trvClassesRooms.Nodes.Add(tn);

                        foreach (var classRoom in classRooms.Where(cr => cr.Item2 == room.ID))
                        {
                            if (tmpClasses.Any(tc => tc.ID == classRoom.Item1))
                            {
                                tmpClasses.Remove(tmpClasses.Single(tc => tc.ID == classRoom.Item1));
                            }

                            TreeNode tn2 = new TreeNode();
                            tn2.Tag = classRoom.Item1;
                            tn.Nodes.Add(tn2);
                            RefreshTreeNodeText(tn2);
                        }

                        RefreshTreeNodeText(tn);
                    }

                    if (tmpClasses.Count > 0)
                    {
                        TreeNode tn = new TreeNode();
                        tn.Text = "<No room added>";
                        trvClassesRooms.Nodes.Add(tn);

                        foreach (var c in tmpClasses)
                        {
                            TreeNode tn2 = new TreeNode();
                            tn2.Tag = c.ID;
                            tn.Nodes.Add(tn2);
                            RefreshTreeNodeText(tn2);
                        }

                        RefreshTreeNodeText(tn);
                    }
                }
            }
        }

        private void RefreshTreeNodeText(int type)
        {
            string nodeID;

            foreach (TreeNode node in trvClassesRooms.Nodes)
            {
                nodeID = (string)(node.Tag);

                if ((type == CLASS && rdoClass.Checked && nodeID == CurrentClass.ID) ||
                    (type == ROOM && rdoRoom.Checked && nodeID == CurrentRoom.ID))
                {
                    RefreshTreeNodeText(node);
                    break;
                }
                else
                {
                    foreach (TreeNode subNode in node.Nodes)
                    {
                        nodeID = (string)(subNode.Tag);

                        if ((type == CLASS && rdoRoom.Checked && nodeID == CurrentClass.ID) ||
                            (type == ROOM && rdoClass.Checked && nodeID == CurrentRoom.ID))
                        {
                            RefreshTreeNodeText(subNode);
                            break;
                        }
                    }
                }
            }
        }

        private void RefreshTreeNodeText(TreeNode node)
        {
            string nodeID = (string)node.Tag;

            if (classes.Any(c => c.ID == nodeID))
            {
                Class c = classes.Single(cs => cs.ID == nodeID);
                node.Text = string.Format("{0} ({1} students)", c.Name, c.Size);
            }
            else if (rooms.Any(r => r.ID == nodeID))
            {
                Room r = rooms.Single(rm => rm.ID == nodeID);
                node.Text = string.Format("{0} ({1} seats)", r.Name, r.NumSeats);
            }

            node.EnsureVisible();
        }

        private void CheckClassReadyForRoomLayout()
        {
            if (CurrentClass.IsReadyForRoomLayout())
            {
                btnRandomise.Enabled = false;
                btnAddRoom.Enabled = true;
                btnAddRoom.Focus();
            }
            else
            {
                btnRandomise.Enabled = true;
                btnAddRoom.Enabled = false;
            }
        }



        private void HighlightNextSteps()
        {
            highlightNextStepsToolStripMenuItem.Checked = highlightNextSteps = !highlightNextSteps;

            if (highlightNextSteps)
            {
                if (currentUser == null)
                {
                    fileToolStripMenuItem.BackColor = Color.Gold;
                    loginToolStripMenuItem.BackColor = Color.Gold;
                }
                else if (classes.Count == 0)
                {
                    btnAddClass.BackColor = Color.Gold;
                }
            }
            else
            {
                fileToolStripMenuItem.BackColor = SystemColors.MenuBar;
                loginToolStripMenuItem.BackColor = Color.Transparent;
            }
        }



        private void ProcessRadioButtons()
        {
            RefreshTree();
        }

        private void BringAboutTheMagicFloatingTextBox(Button sender)
        {
            txtMagicFloatingTextBox.Location = new Point(397, sender.Parent.Top + sender.Top + 2);
            txtMagicFloatingTextBox.Text = "";
            txtMagicFloatingTextBox.Visible = true;
            txtMagicFloatingTextBox.Focus();
        }

        private void BringAboutTheMagicFloatingComboBox(Button sender)
        {
            if (lstStudents.SelectedIndex >= 0)
            {
                cmbMagicFloatingComboBox.Location = new Point(397, sender.Parent.Top + sender.Top + 2);
                cmbMagicFloatingComboBox.Items.Clear();

                sorcerer = sender.Name;

                foreach (string s in lstStudents.Items)
                {
                    cmbMagicFloatingComboBox.Items.Add(s);
                }

                cmbMagicFloatingComboBox.Items.Remove(currentStudent.Name);

                foreach (string w in lstWorksWell.Items)
                {
                    cmbMagicFloatingComboBox.Items.Remove(w);
                }

                foreach (string d in lstDistractedBy.Items)
                {
                    cmbMagicFloatingComboBox.Items.Remove(d);
                }

                if (cmbMagicFloatingComboBox.Items.Count > 0)
                {
                    cmbMagicFloatingComboBox.SelectedIndex = 0;
                    cmbMagicFloatingComboBox.Focus();
                    cmbMagicFloatingComboBox.Visible = true;
                    cmbMagicFloatingComboBox.Focus();
                }
                else
                {
                    MessageBox.Show("You have exhausted the possible relationships for this student.", "Not Enough Students",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("You haven't selected a student to add this relationship to.", "Need More Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lstStudents.Focus();
            }
        }

        private DialogResult CheckOKToRemoveStudent(ref ListBox list)
        {
            if (list.Items.Count == 1)
            {
                list.SelectedIndex = 0;
            }

            if (list.SelectedIndex > -1)
            {
                return MessageBox.Show(string.Format("Are you sure you want to remove {0} from this list?", list.SelectedItem),
                                       "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                MessageBox.Show("You haven't selected a student to remove.", "Need More Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return DialogResult.No;
            }
        }

        private void ParseRoomValues()
        {
            if (!Int32.TryParse(txtColumns.Text, out CurrentRoom.NumColumns))
            {
                CurrentRoom.NumColumns = Math.Abs(CurrentRoom.NumColumns);
            }
            if (CurrentRoom.NumColumns < 1)
            {
                CurrentRoom.NumColumns = 1;
            }
            txtColumns.Text = CurrentRoom.NumColumns.ToString();

            if (!Int32.TryParse(txtRows.Text, out CurrentRoom.NumRows))
            {
                CurrentRoom.NumRows = Math.Abs(CurrentRoom.NumRows);
            }
            if (CurrentRoom.NumRows < 1)
            {
                CurrentRoom.NumRows = 1;
            }
            txtRows.Text = CurrentRoom.NumRows.ToString();

            if (!Int32.TryParse(txtTableDepth.Text, out CurrentRoom.TableDepth))
            {
                CurrentRoom.TableDepth = Math.Abs(CurrentRoom.TableDepth);
            }
            if (CurrentRoom.TableDepth < 1)
            {
                CurrentRoom.TableDepth = 1;
            }
            txtTableDepth.Text = CurrentRoom.TableDepth.ToString();

            if (!Int32.TryParse(txtTableWidth.Text, out CurrentRoom.TableWidth))
            {
                CurrentRoom.TableWidth = Math.Abs(CurrentRoom.TableWidth);
            }
            if (CurrentRoom.TableWidth < 1)
            {
                CurrentRoom.TableWidth = 1;
            }
            txtTableWidth.Text = CurrentRoom.TableWidth.ToString();

            SetLayout();

            lblNumTables.Text = string.Format("Number of tables: {0}", CurrentRoom.NumTables);
            lblNumSeats.Text = string.Format("Number of seats: {0}", CurrentRoom.NumSeats);
        }

        public bool ValidateNumSeats()
        {
            return CurrentRoom.NumSeats >= CurrentClass.Size ? true : false;
        }

        public void UpdateSeats()
        {
            if (Evolving)
            {
                CurrentSeatingPlan = Incubator.GetSeatingPlan(PlanIndex);
            }
            else
            {
                CurrentSeatingPlan = Incubator.GetGoodSeatingPlan(PlanIndex);
            }

            foreach (Student student in CurrentClass.GetStudents())
            {
                student.Seat = CurrentSeatingPlan.GetStudentSeat(student.ID);
            }
        }

        public void UpdateRoomLayoutImage()
        {
            int blockWidth = Convert.ToInt32((double)grpLayout.Width / CurrentRoom.RoomWidth);
            int blockHeight = Convert.ToInt32((double)grpLayout.Height / CurrentRoom.RoomHeight);
            int blockSize = Math.Min(blockWidth, blockHeight);

            int gapLeftRight = (grpLayout.Width - (CurrentRoom.RoomWidth * blockSize)) / 2;
            int gapTopBottom = (grpLayout.Height - (CurrentRoom.RoomHeight * blockSize)) / 2;

            Graphics gTS = Graphics.FromImage(grpLayout.BackgroundImage);
            gTS.Clear(SystemColors.Control);

            foreach (var table in CurrentRoom.GetTables())
            {
                foreach (var seat in CurrentRoom.GetSeatsAroundTable(table.Key))
                {
                    gTS.FillRectangle(Brushes.LightGray,
                                      gapLeftRight + blockSize * seat.Value.X,
                                      gapTopBottom + blockSize * seat.Value.Y,
                                      blockSize,
                                      blockSize);

                    gTS.DrawRectangle(Pens.Black,
                                      gapLeftRight + blockSize * seat.Value.X,
                                      gapTopBottom + blockSize * seat.Value.Y,
                                      blockSize,
                                      blockSize);
                }

                gTS.FillRectangle(Brushes.Brown,
                                  gapLeftRight + blockSize * table.Value.X,
                                  gapTopBottom + blockSize * table.Value.Y,
                                  blockSize * table.Value.Width,
                                  blockSize * table.Value.Height);

                gTS.DrawRectangle(Pens.Black,
                                  gapLeftRight + blockSize * table.Value.X,
                                  gapTopBottom + blockSize * table.Value.Y,
                                  blockSize * table.Value.Width,
                                  blockSize * table.Value.Height);
            }

            if (Incubator != null && DisplayLinks)
            {
                List<Student> students = CurrentClass.GetStudents();
                Dictionary<int, Point> seats = CurrentRoom.GetSeats();

                foreach (Student student in students)
                {
                    Student tmpStudent;
                    Point start = new Point(gapLeftRight + (seats[student.Seat].X * blockSize) + (blockSize / 2),
                                            gapTopBottom + (seats[student.Seat].Y * blockSize) + (blockSize / 2));
                    Point wwEnd = new Point(0, 0);
                    Point dbEnd = new Point(0, 0);

                    foreach (var ww in student.GetWWList())
                    {
                        tmpStudent = students.Single(s => s.ID == ww);
                        wwEnd = new Point(gapLeftRight + (seats[tmpStudent.Seat].X * blockSize) + (blockSize / 2),
                                          gapTopBottom + (seats[tmpStudent.Seat].Y * blockSize) + (blockSize / 2));
                    }

                    foreach (var db in student.GetDBList())
                    {
                        tmpStudent = students.Single(s => s.ID == db);
                        dbEnd = new Point(gapLeftRight + (seats[tmpStudent.Seat].X * blockSize) + (blockSize / 2),
                                          gapTopBottom + (seats[tmpStudent.Seat].Y * blockSize) + (blockSize / 2));
                    }

                    gTS.DrawLine(new Pen(Color.Green, 2), start, wwEnd);
                    gTS.DrawLine(new Pen(Color.Red, 2), start, dbEnd);
                }
            }

            grpLayout.Refresh();

            if (!Evolving && Incubator != null && Incubator.HasPlans)
            {
                grpLayout.Text = string.Format("Room Layout ({0} of {1} candidates)", PlanIndex + 1, Incubator.GetNumGoodSeatingPlans());
            }
            else if (!Evolving)
            {
                grpLayout.Text = "Room Layout";
            }

            UpdateRoomLayoutForm();
        }

        private void SetNavVisibility()
        {
            if (Incubator.HasPlans)
            {
                btnLeft.Visible = true;
                btnRight.Visible = true;
            }
            else
            {
                btnLeft.Visible = false;
                btnRight.Visible = false;
            }
        }

        private void ChangeSeatingPlan(int d)
        {
            if (Incubator != null)
            {
                int numGoodSeatingPlans = Incubator.GetNumGoodSeatingPlans();
                if (numGoodSeatingPlans > 0)
                {
                    PlanIndex += d % numGoodSeatingPlans;

                    if (PlanIndex <= -1)
                    {
                        PlanIndex += numGoodSeatingPlans;
                    }
                    else if (PlanIndex >= numGoodSeatingPlans)
                    {
                        PlanIndex -= numGoodSeatingPlans;
                    }

                    UpdateSeats();
                    UpdateRoomLayoutImage();
                }
            }
        }

        public void SetLayout()
        {
            if (Incubator != null && Incubator.HasPlans)
            {
                Incubator = null;
                stsText.Text = "Layout changed. Seating plans have been deleted.";
                tmrFlashStatus.Enabled = true;
            }

            CurrentRoom.SetLayout();
            UpdateRoomLayoutImage();
            RefreshTreeNodeText(ROOM);

            if (roomSetup != null && !roomSetup.IsDisposed)
            {
                roomSetup.UpdateLayout();
            }
        }



        private void SetEvolving(bool evolving)
        {
            Evolving = evolving;

            grpGroups.Enabled = !evolving;
            grpClass.Enabled = !evolving;

            btnAddRoom.Enabled = !evolving;
            btnEditRoom.Enabled = !evolving;
            btnResetRoom.Enabled = evolving ? false : CurrentRoom.HasBeenEdited;

            stsProgress.Visible = evolving;
        }

        public void SetProgressBarValue(int value)
        {
            stsProgress.Value = value;
        }



        private void UpdateRoomLayoutForm()
        {
            if (roomSetup != null && !roomSetup.IsDisposed)
            {
                roomSetup.UpdateLayout();
            }
        }



        private void SeatingPlans_Activated(object sender, EventArgs e)
        {
            this.Activated -= new EventHandler(SeatingPlans_Activated);
            Login();
        }

        private void SeatingPlans_FormClosing(object sender, FormClosingEventArgs e)
        {
            AskWhetherToSave();
        }



        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskWhetherToSave();
            OpenData(ALLDATA, true);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenData(ALLDATA, false);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quickSaveFileName != "")
            {
                SaveData(false);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                Login();
            }
            else
            {
                Logout();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskWhetherToSave();
            Close();
        }



        private void highlightNextStepsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighlightNextSteps();
        }



        private void rdoClass_CheckedChanged(object sender, EventArgs e)
        {
            ProcessRadioButtons();
        }

        private void rdoRoom_CheckedChanged(object sender, EventArgs e)
        {
            ProcessRadioButtons();
        }



        private void trvClassesRooms_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!refreshingTree)
            {
                string nodeID = (string)trvClassesRooms.SelectedNode.Tag;

                if (classRooms.Any(cr => cr.Item2 == nodeID))
                {
                    // User has selected a room
                    LoadClass(classRooms.Where(cr => cr.Item2 == nodeID).First().Item1);
                    LoadRoom(nodeID);
                }
                else
                {
                    // User has selected a class or there is no class/room key in classRooms
                    LoadClass(nodeID);

                    if (rdoClass.Checked && trvClassesRooms.SelectedNode.Nodes.Count > 0)
                    {
                        // Select the first room available
                        LoadRoom((string)trvClassesRooms.SelectedNode.Nodes[0].Tag);
                    }
                    else if (rdoRoom.Checked)
                    {
                        LoadRoom((string)trvClassesRooms.SelectedNode.Parent.Tag);
                    }
                    else
                    {
                        CurrentRoom = null;
                        ClearRoom();
                        grpRoomLayout.Enabled = false;
                    }
                }
            }
        }

        private void trvClassesRooms_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 46) // Delete key
            {
                string message = string.Format("You are about to delete the selected class ({0}). Do you wish to continue?", CurrentClass.Name);
                DialogResult result = MessageBox.Show(message, "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    classes.Remove(CurrentClass);
                    CurrentClass = null;
                    trvClassesRooms.Nodes.Remove(trvClassesRooms.SelectedNode);

                    RefreshForm();
                }
            }
        }



        private void btnAddClass_Click(object sender, EventArgs e)
        {
            DialogResult openFromFile = MessageBox.Show("Do you want to load class and student data from a file?", "Add Class From File?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (openFromFile != DialogResult.Cancel)
            {
                if (openFromFile == DialogResult.Yes)
                {
                    OpenData(CLASSDATA, false);
                    AddCurrentClass();
                }
                else if (openFromFile == DialogResult.No)
                {
                    CurrentClass = new Class("Empty Class");
                    AddCurrentClass();
                }

                RefreshForm();
            }
        }

        private void btnRemoveClass_Click(object sender, EventArgs e)
        {
            if (trvClassesRooms.SelectedNode != null)
            {
                string nodeID = (string)trvClassesRooms.SelectedNode.Tag;

                DialogResult removeOK = MessageBox.Show(string.Format("Are you sure you want to remove {0} from this list?", trvClassesRooms.SelectedNode.Text),
                                                        "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (removeOK == DialogResult.Yes)
                {
                    List<Tuple<string, string>> toRemove = new List<Tuple<string, string>>();

                    if (classes.Any(c => c.ID == nodeID))
                    {
                        classes.Remove(classes.Single(c => c.ID == nodeID));

                        foreach (var classRoom in classRooms.Where(cr => cr.Item1 == nodeID))
                        {
                            toRemove.Add(classRoom);
                        }

                        foreach (var tr in toRemove)
                        {
                            classRooms.Remove(tr);
                        }
                    }
                    else
                    {
                        rooms.Remove(rooms.Single(r => r.ID == nodeID));

                        foreach (var classRoom in classRooms.Where(cr => cr.Item2 == nodeID))
                        {
                            toRemove.Add(classRoom);
                        }

                        foreach (var tr in toRemove)
                        {
                            classRooms.Remove(tr);
                        }
                    }

                    if (trvClassesRooms.Nodes.Count > 0)
                    {
                        trvClassesRooms.SelectedNode = trvClassesRooms.Nodes[0];
                    }

                    RefreshForm();
                }
            }
            else
            {
                MessageBox.Show("You haven't selected an item to remove.", "Need More Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void txtClassName_TextChanged(object sender, EventArgs e)
        {
            if (CurrentClass != null)
            {
                CurrentClass.Name = txtClassName.Text;

                foreach (TreeNode node in trvClassesRooms.Nodes)
                {
                    if ((string)(node.Tag) == CurrentClass.ID)
                    {
                        RefreshTreeNodeText(node);
                        break;
                    }
                }
            }
        }

        private void lstStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStudentData(lstStudents.SelectedIndex);
        }

        private void lstStudents_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'w' || e.KeyChar == 'd')
            {
                if (e.KeyChar == 'w')
                {
                    BringAboutTheMagicFloatingComboBox(btnAddWW);
                }
                else
                {
                    BringAboutTheMagicFloatingComboBox(btnAddDB);
                }
             
                e.Handled = true;
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            BringAboutTheMagicFloatingTextBox((Button)sender);
        }

        private void txtMagicFloatingTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int c = (int)e.KeyChar;
            if (c == 13 || c == 27)
            {
                if (c == 13)
                {
                    CurrentClass.AddStudent(new Student(txtMagicFloatingTextBox.Text, true));
                    RefreshClassData();
                }

                txtMagicFloatingTextBox.Visible = false;
                e.Handled = true;
            }
        }

        private void btnAddWW_Click(object sender, EventArgs e)
        {
            BringAboutTheMagicFloatingComboBox((Button)sender);
        }

        private void btnAddDB_Click(object sender, EventArgs e)
        {
            BringAboutTheMagicFloatingComboBox((Button)sender);
        }

        private void cmbMagicFloatingComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int c = (int)e.KeyChar;
            if (c == 13 || c == 27)
            {
                if (c == 13)
                {
                    if (sorcerer == "btnAddWW")
                    {
                        currentStudent.AddWorksWell(CurrentClass.NameIDs[(string)cmbMagicFloatingComboBox.SelectedItem]);
                        lstWorksWell.Items.Add(cmbMagicFloatingComboBox.SelectedItem);
                    }
                    else
                    {
                        currentStudent.AddDistractedBy(CurrentClass.NameIDs[(string)cmbMagicFloatingComboBox.SelectedItem]);
                        lstDistractedBy.Items.Add(cmbMagicFloatingComboBox.SelectedItem);
                    }

                    UpdateStudents();
                    CheckClassReadyForRoomLayout();
                }

                cmbMagicFloatingComboBox.Visible = false;
                e.Handled = true;
                lstStudents.Focus();
            }
        }



        private void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            if (CheckOKToRemoveStudent(ref lstStudents) == DialogResult.Yes)
            {
                CurrentClass.RemoveStudent(currentStudent);
                UpdateClasses();
                RefreshTreeNodeText(CLASS);
                RefreshClassData();
            }
        }

        private void btnRemoveWW_Click(object sender, EventArgs e)
        {
            if (CheckOKToRemoveStudent(ref lstWorksWell) == DialogResult.Yes)
            {
                currentStudent.RemoveWorksWell(lstWorksWell.SelectedIndex);
                UpdateStudents();
                RefreshClassData();
                lstStudents.Focus();
            }
        }

        private void btnRemoveDB_Click(object sender, EventArgs e)
        {
            if (CheckOKToRemoveStudent(ref lstDistractedBy) == DialogResult.Yes)
            {
                currentStudent.RemoveDistractedBy(lstDistractedBy.SelectedIndex);
                UpdateStudents();
                RefreshClassData();
                lstStudents.Focus();
            }
        }

        private void btnRandomise_Click(object sender, EventArgs e)
        {
            List<Student> students = CurrentClass.GetStudents().ToList();
            List<Student> studentsWW = CurrentClass.GetStudents().ToList();
            List<Student> studentsDB = CurrentClass.GetStudents().ToList();

            Random rnd = new Random();

            int rndWW = 0;
            int rndDB = 0;

            foreach (Student student in students)
            {
                int currentStudentIndex = students.IndexOf(student);

                do
                {
                    rndWW = rnd.Next(students.Count);
                    rndDB = rnd.Next(students.Count);
                } while (rndWW == rndDB || rndWW == currentStudentIndex || rndDB == currentStudentIndex);

                student.AddRelationshipDetails(students[rndWW].ID, students[rndDB].ID);
            }

            if (lstStudents.SelectedIndex >= 0)
            {
                RefreshStudentData(lstStudents.SelectedIndex);
            }

            CheckClassReadyForRoomLayout();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            CurrentRoom = new Room("Empty Room", true);
            AddCurrentRoom();
            RefreshForm();
            ParseRoomValues();
        }



        private void txtRoomName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtColumns.Focus();
                e.Handled = true;
            }
        }

        private void txtRoomName_TextChanged(object sender, EventArgs e)
        {
            if (CurrentRoom != null)
            {
                CurrentRoom.Name = txtRoomName.Text;
                RefreshTreeNodeText(ROOM);
            }
        }

        private void btnEditRoom_Click(object sender, EventArgs e)
        {
            if (roomSetup == null || roomSetup.IsDisposed)
            {
                roomSetup = new RoomSetup(this);
            }

            roomSetup.Show();
        }

        private void btnResetRoom_Click(object sender, EventArgs e)
        {
            DialogResult resetOK = MessageBox.Show("If you continue you will lose any changes you have made." + Environment.NewLine +
                                                   "Are you sure you want to reset the room to its original layout?",
                                                   "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resetOK == DialogResult.Yes)
            {
                pnlRoomSettings.Enabled = true;
                ParseRoomValues();
                CurrentRoom.SetLayout();
            }
        }

        private void txtRoomSettings_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txtRoomSettings_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txtRoomSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38) // Up
            {
                ((TextBox)sender).Text = (Int32.Parse(((TextBox)sender).Text) + 1).ToString();
            }
            else if (e.KeyValue == 40) // Down
            {
                ((TextBox)sender).Text = (Int32.Parse(((TextBox)sender).Text) - 1).ToString();
            }
        }

        private void txtRoomSettings_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                switch (((TextBox)sender).Name)
                {
                    case "txtColumns":
                        txtRows.Focus();
                        break;
                    case "txtRows":
                        txtTableWidth.Focus();
                        break;
                    case "txtTableWidth":
                        txtTableDepth.Focus();
                        break;
                    case "txtTableDepth":
                        txtColumns.Focus();
                        break;
                    default:
                        break;
                }

                e.Handled = true;
            }
        }

        private void txtRoomSettings_TextChanged(object sender, EventArgs e)
        {
            if (pnlRoomSettings.Enabled && CurrentRoom != null)
            {
                ParseRoomValues();
                UpdateRooms();
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            ChangeSeatingPlan(-1);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            ChangeSeatingPlan(1);
        }

        private void chkSideSeats_CheckedChanged(object sender, EventArgs e)
        {
            CurrentRoom.SideSeats = chkSideSeats.Checked;
            UpdateRooms();
            SetLayout();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ValidateNumSeats())
            {
                int basis = 0;
                if (rdoAttainment.Checked)
                {
                    basis = Incubator.ATTAINMENT;
                }
                else if (rdoStudentSettings.Checked)
                {
                    basis = Incubator.STUDENT_SETTINGS;
                }

                Incubator = new Incubator(basis, Convert.ToInt32(nudNumBreeders.Value), Convert.ToInt32(nudNumGenerations.Value), this);
                stsProgress.Value = 0;
                stsProgress.Maximum = Convert.ToInt32(nudNumGenerations.Value);

                SetEvolving(true);
                grpLayout.Text = "";
                stsText.Text = "Generating seating plans, please wait... ";
                Incubator.CalculateSeatingPlan();
                stsText.Text = string.Format("Generated {0} seating plans", Incubator.GetNumGoodSeatingPlans());
                SetEvolving(false);

                UpdateSeats();
                UpdateRoomLayoutImage();
            }
            else
            {
                stsText.Text = "Too few seats to accommodate all students";
                tmrFlashStatus.Enabled = true;
            }
        }

        private void tmrFlashStatus_Tick(object sender, EventArgs e)
        {
            if (flashes < 3)
            {
                if (stsText.BackColor == Color.Red)
                {
                    stsText.BackColor = SystemColors.Control;
                    stsText.ForeColor = Color.Black;
                    flashes++;
                }
                else
                {
                    stsText.BackColor = Color.Red;
                    stsText.ForeColor = Color.White;
                }
            }
            else
            {
                tmrFlashStatus.Enabled = false;
                flashes = 0;
            }
        }
    }
}