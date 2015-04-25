using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeatingPlanCreator
{
    public partial class RoomSetup : Form
    {
        private ClassRoomSetup parent;

        private Dictionary<int, Rectangle> tables;
        private Dictionary<int, Rectangle> seats;

        private Point initialPosition;

        private bool mouseDown;
        private int blockSize;
        private int gapLeftRightRoom;
        private int gapTopBottomRoom;

        private int selectedTableIndex;

        public RoomSetup(ClassRoomSetup parent)
        {
            InitializeComponent();

            this.parent = parent;
            tables = new Dictionary<int, Rectangle>();
            seats = new Dictionary<int, Rectangle>();

            grpLayout.BackgroundImage = new Bitmap(grpLayout.Width, grpLayout.Height);
            picTableAndSeats.Image = new Bitmap(picTableAndSeats.Width, picTableAndSeats.Height);
        }


        private void RoomSetup_Load(object sender, EventArgs e)
        {
            UpdateLayout();
            Size = new System.Drawing.Size((3 * Screen.GetWorkingArea(this).Width) / 4,
                                           (3 * Screen.GetWorkingArea(this).Height) / 4);
            CenterToScreen();
        }

        private void RoomLayout_SizeChanged(object sender, EventArgs e)
        {
            grpLayout.Width = Width - grpTableSettings.Width - (6 + 40);
            grpLayout.Height = Height - 62;

            grpTableSettings.Left = 12 + grpLayout.Width + 6;
            grpChangeLayout.Left = 12 + grpLayout.Width + 6;

            grpLayout.BackgroundImage = new Bitmap(grpLayout.Width, grpLayout.Height);

            UpdateLayout();
        }

        private void ClearRoom()
        {
            Controls.Remove(grpLayout);
            grpLayout.Controls.Clear();
            seats.Clear();
            tables.Clear();
        }

        public void UpdateLayout()
        {
            int blockWidth = Convert.ToInt32((double)grpLayout.Width / parent.GetCurrentRoom().RoomWidth);
            int blockHeight = Convert.ToInt32((double)grpLayout.Height / parent.GetCurrentRoom().RoomHeight);
            blockSize = Math.Min(blockWidth, blockHeight);

            gapLeftRightRoom = (grpLayout.Width - (parent.GetCurrentRoom().RoomWidth * blockSize)) / 2;
            gapTopBottomRoom = (grpLayout.Height - (parent.GetCurrentRoom().RoomHeight * blockSize)) / 2;

            if (!parent.Evolving)
            {
                ClearRoom();

                foreach (var table in parent.GetCurrentRoom().GetTables())
                {
                    AddTable(table.Key,
                             gapLeftRightRoom + blockSize * table.Value.X,
                             gapTopBottomRoom + blockSize * table.Value.Y,
                             blockSize * table.Value.Width,
                             blockSize * table.Value.Height);
                }
                
                foreach (var table in parent.GetCurrentRoom().GetTables())
                {
                    foreach (var seat in parent.GetCurrentRoom().GetSeatsAroundTable(table.Key))
                    {
                        AddSeat(seat.Key,
                                gapLeftRightRoom + blockSize * seat.Value.X,
                                gapTopBottomRoom + blockSize * seat.Value.Y,
                                blockSize,
                                blockSize);
                    }
                }
            }

            if (parent.DisplayLinks)
            {
                DrawLinks();
            }

            if (parent.GetIncubator() != null)
            {
                if (parent.GetIncubator().HasPlans && parent.DisplayStudents)
                {
                    DisplayStudents();
                }

                if (!parent.Evolving && parent.GetIncubator().GetNumGoodSeatingPlans() > 0)
                {
                    grpLayout.Text = string.Format("Room Layout ({0} of {1} candidates)", parent.PlanIndex + 1, parent.GetIncubator().GetNumGoodSeatingPlans());
                }
            }
            else
            {
                grpLayout.Text = "Room Layout";
            }

            Controls.Add(grpLayout);
            SetNavVisibility();
            Invalidate();
        }

        private void DrawLinks()
        {
            List<Student> students = parent.GetCurrentClass().GetStudents();
            Dictionary<int, Point> seats = parent.GetCurrentRoom().GetSeats();

            Graphics gTS = Graphics.FromImage(grpLayout.BackgroundImage);
            gTS.Clear(SystemColors.Control);

            foreach (Student student in students)
            {
                Student tmpStudent;
                Point start = ConvertSeatLocationToLayoutCoordinates(seats[student.Seat].X, seats[student.Seat].Y);
                Point wwEnd = new Point(0, 0);
                Point dbEnd = new Point(0, 0);

                foreach (var ww in student.GetWWList())
                {
                    tmpStudent = students.Single(s => s.ID == ww);
                    wwEnd = ConvertSeatLocationToLayoutCoordinates(seats[tmpStudent.Seat].X, seats[tmpStudent.Seat].Y);
                }

                foreach (var db in student.GetDBList())
                {
                    tmpStudent = students.Single(s => s.ID == db);
                    dbEnd = ConvertSeatLocationToLayoutCoordinates(seats[tmpStudent.Seat].X, seats[tmpStudent.Seat].Y);
                }

                gTS.DrawLine(new Pen(Color.Green, 2), start, wwEnd);
                gTS.DrawLine(new Pen(Color.Red, 2), start, dbEnd);
            }
        }

        private void DisplayStudents()
        {
            Dictionary<int, string> seatNames = parent.GetCurrentClass().GetSeatNames();

            foreach (Label label in grpLayout.Controls)
            {
                if (label.Name.StartsWith("lblSeat"))
                {
                    int labelIndex = Convert.ToInt32(label.Name.Replace("lblSeat", ""));

                    if (seatNames.ContainsKey(labelIndex))
                    {
                        label.Text = seatNames[labelIndex];
                    }
                }
            }
        }

        private void AddSeat(int index, int x, int y, int w, int h)
        {
            seats.Add(index, new Rectangle(x, y, w, h));

            Label seatLabel = new Label();
            seatLabel.Name = string.Format("lblSeat{0}", index);
            seatLabel.Left = x;
            seatLabel.Top = y;
            seatLabel.Width = w;
            seatLabel.Height = h;
            seatLabel.BackColor = Color.LightGray;
            seatLabel.BorderStyle = BorderStyle.FixedSingle;
            seatLabel.TextAlign = ContentAlignment.MiddleCenter;

            seatLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSeat_MouseDown);
            seatLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblSeat_MouseMove);
            seatLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSeat_MouseUp);

            grpLayout.Controls.Add(seatLabel);
        }

        private void AddTable(int index, int x, int y, int w, int h)
        {
            tables.Add(index, new Rectangle(x, y, w, h));

            Label tableLabel = new Label();
            tableLabel.Name = string.Format("lblTable{0}", index);
            tableLabel.Left = x;
            tableLabel.Top = y;
            tableLabel.Width = w;
            tableLabel.Height = h;
            tableLabel.BackColor = Color.Brown;
            tableLabel.BorderStyle = BorderStyle.FixedSingle;
            tableLabel.ForeColor = Color.White;
            tableLabel.Text = string.Format("Table {0}", index);
            tableLabel.TextAlign = ContentAlignment.MiddleCenter;

            tableLabel.Click += new System.EventHandler(this.lblTable_Click);
            tableLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTable_MouseDown);
            tableLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTable_MouseMove);
            tableLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblTable_MouseUp);

            grpLayout.Controls.Add(tableLabel);
        }

        private void ChangeSeatingPlan(int d)
        {
            int numGoodSeatingPlans = parent.GetIncubator().GetNumGoodSeatingPlans();
            if (numGoodSeatingPlans > 0)
            {
                parent.PlanIndex += d % numGoodSeatingPlans;

                if (parent.PlanIndex <= -1)
                {
                    parent.PlanIndex += numGoodSeatingPlans;
                }
                else if (parent.PlanIndex >= numGoodSeatingPlans)
                {
                    parent.PlanIndex -= numGoodSeatingPlans;
                }

                parent.UpdateSeats();
                parent.UpdateRoomLayoutImage(); // Also calls this.UpdateLayout
            }
        }

        private void SetNavVisibility()
        {
            grpChangeLayout.Visible = parent.GetIncubator() != null && parent.GetIncubator().HasPlans;
        }

        private Point ConvertSeatLocationToLayoutCoordinates(int x, int y)
        {
            return new Point(gapLeftRightRoom + (x * blockSize) + (blockSize / 2),
                             gapTopBottomRoom + (y * blockSize) + (blockSize / 2));
        }

        private Point ConvertLayoutCoordinatesToGridLocation(int x, int y)
        {
            return new Point(Convert.ToInt32(Math.Round((x - gapLeftRightRoom) / (double)blockSize, 0, MidpointRounding.AwayFromZero)),
                             Convert.ToInt32(Math.Round((y - gapTopBottomRoom) / (double)blockSize, 0, MidpointRounding.AwayFromZero)));
        }

        private void lblSeat_MouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).Use(l =>
            {
                l.BringToFront();
                initialPosition = new Point(e.X, e.Y);
            });

            mouseDown = true;
        }

        private void lblSeat_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ((Label)sender).Use(l =>
                {
                    l.Location = new Point(l.Left + (e.X - initialPosition.X),
                                           l.Top + (e.Y - initialPosition.Y));
                });
            }
        }

        private void lblSeat_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            int seatIndex = 0;
            Point finalSeatPosition = new Point(0, 0);

            ((Label)sender).Use(l =>
            {
                seatIndex = Convert.ToInt32(l.Name.Replace("lblSeat", ""));
                finalSeatPosition = ConvertLayoutCoordinatesToGridLocation(l.Location.X, l.Location.Y);
                parent.GetCurrentRoom().SetSeatPosition(seatIndex, finalSeatPosition);
            });

            Point initialSeatPosition = parent.GetCurrentRoom().GetSeatPosition(seatIndex);

            if (initialSeatPosition.X != finalSeatPosition.X || initialSeatPosition.Y != finalSeatPosition.Y)
            {
                parent.GetCurrentRoom().HasBeenEdited = true;
            }

            UpdateLayout();
        }

        private void DrawTablePreview(object sender)
        {
            selectedTableIndex = Convert.ToInt32(((Label)sender).Name.Replace("lblTable", ""));
            Rectangle table = parent.GetCurrentRoom().GetTable(selectedTableIndex);

            int minX = table.X;
            int maxX = table.X + 1;
            int xRange = 0;
            
            int minY = table.Y;
            int maxY = table.Y + 1;
            int yRange = 0;

            foreach (var seat in parent.GetCurrentRoom().GetSeatsAroundTable(selectedTableIndex))
            {
                if (seat.Value.X < minX)
                {
                    minX = seat.Value.X;
                    xRange = table.X - seat.Value.X;
                }
                else if (seat.Value.X > table.X && seat.Value.X + 1 > maxX)
                {
                    maxX = seat.Value.X + 1;
                }

                if (seat.Value.Y < minY)
                {
                    minY = seat.Value.Y;
                    yRange = table.Y - seat.Value.Y;
                }
                else if (seat.Value.Y > table.Y && seat.Value.Y + 1 > maxY)
                {
                    maxY = seat.Value.Y + 1;
                }
            }

            int tableWindowWidth = maxX - minX;
            int tableWindowHeight = maxY - minY;

            int blockWidth = Convert.ToInt32((double)picTableAndSeats.Width / (tableWindowWidth + 2));
            int blockHeight = Convert.ToInt32((double)picTableAndSeats.Height / (tableWindowHeight + 2));
            int blockSize = Math.Min(blockWidth, blockHeight);

            int gapLeftRight = (picTableAndSeats.Width - (tableWindowWidth * blockSize)) / 2;
            int gapTopBottom = (picTableAndSeats.Height - (tableWindowHeight * blockSize)) / 2;

            Graphics gTS = Graphics.FromImage(picTableAndSeats.Image);
            gTS.Clear(SystemColors.Control);

            foreach (var seat in parent.GetCurrentRoom().GetSeatsAroundTable(selectedTableIndex))
            {
                gTS.FillRectangle(Brushes.LightGray, new Rectangle(gapLeftRight + blockSize * (seat.Value.X - table.X + xRange),
                                                                   gapTopBottom + blockSize * (seat.Value.Y - table.Y + yRange),
                                                                   blockSize,
                                                                   blockSize));
                gTS.DrawRectangle(Pens.Black, new Rectangle(gapLeftRight + blockSize * (seat.Value.X - table.X + xRange),
                                                            gapTopBottom + blockSize * (seat.Value.Y - table.Y + yRange),
                                                            blockSize,
                                                            blockSize));
            }


            gTS.FillRectangle(Brushes.Brown, new Rectangle(gapLeftRight + blockSize * xRange,
                                                           gapTopBottom + blockSize * yRange,
                                                           blockSize * table.Width,
                                                           blockSize * table.Height));

            gTS.DrawRectangle(Pens.Black, new Rectangle(gapLeftRight + blockSize * xRange,
                                                        gapTopBottom + blockSize * yRange,
                                                        blockSize * table.Width,
                                                        blockSize * table.Height));

            picTableAndSeats.Refresh();

            txtTableWidth.Text = table.Width.ToString();
            txtTableDepth.Text = table.Height.ToString();
            grpTableSettings.Text = string.Format("Table {0} Settings", selectedTableIndex);
            grpTableSettings.Enabled = true;
        }

        private void lblTable_Click(object sender, EventArgs e)
        {
            DrawTablePreview(sender);
        }

        private void lblTable_MouseDown(object sender, MouseEventArgs e)
        {
            ((Label)sender).Use(l =>
            {
                l.BringToFront();
                initialPosition = new Point(e.X, e.Y);
            });

            mouseDown = true;
        }

        private void lblTable_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ((Label)sender).Use(l =>
                {
                    l.Location = new Point(l.Left + (e.X - initialPosition.X),
                                           l.Top + (e.Y - initialPosition.Y));
                });
            }
        }

        private void lblTable_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

            ((Label)sender).Use(l =>
            {
                parent.GetCurrentRoom().SetTable(Convert.ToInt32(l.Name.Replace("lblTable", "")), ConvertLayoutCoordinatesToGridLocation(l.Location.X, l.Location.Y));
            });

            DrawTablePreview(sender);
            UpdateLayout();
        }

        private void btnRemoveTable_Click(object sender, EventArgs e)
        {
            if (parent.GetCurrentRoom().NumSeats - parent.GetCurrentRoom().GetNumSeatsAroundTable(selectedTableIndex) >= parent.GetCurrentClass().Size)
            {
                List<int> tableSeats = parent.GetCurrentRoom().GetSeatIndicesAroundTable(selectedTableIndex);

                // Check to see if there are students in those seats and, if so, move them.
                Dictionary<int, bool> seatsAvailable = new Dictionary<int, bool>();
                foreach (int seat in parent.GetCurrentRoom().GetSeatIndices())
                {
                    seatsAvailable.Add(seat, true);
                }

                // Remove taken seats and keep track of who to move.
                List<string> studentsToMove = new List<string>();
                foreach (var student in parent.GetCurrentClass().GetStudents())
                {
                    seatsAvailable[student.Seat] = false;
                    if (tableSeats.Contains(student.Seat))
                    {
                        studentsToMove.Add(student.ID);
                    }
                }

                parent.GetCurrentRoom().RemoveTable(selectedTableIndex);
                parent.GetCurrentRoom().HasBeenEdited = true;

                if (parent.GetIncubator() != null)
                {
                    SeatingPlan newPlan = parent.GetIncubator().GetGoodSeatingPlan(parent.PlanIndex);
                    foreach (var student in parent.GetCurrentClass().GetStudents().Where(s => studentsToMove.Contains(s.ID)))
                    {
                        foreach (var seat in seatsAvailable)
                        {
                            if (seat.Value)
                            {
                                student.Seat = seat.Key;
                                seatsAvailable[seat.Key] = false;
                                newPlan.SetStudentSeat(student.ID, seat.Key);
                                break;
                            }
                        }
                    }

                    parent.GetIncubator().SetGoodSeatingPlan(parent.PlanIndex, newPlan);
                    parent.UpdateSeats();
                }
             
                parent.UpdateRoomLayoutImage(); // Also calls this.UpdateLayout
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
    }
}
