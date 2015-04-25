using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SeatingPlanCreator
{
    public class Room
    {
        public string ID { get; set;  }
        public string Name { get; set; }

        public int NumColumns;
        public int NumRows;
        public int NumTables { get { return tables.Count; } }
        public int NumSeats { get { return seats.Count; } }
        public int SmallestSeatIndex { get { return seats.Keys.First(); } }

        public int TableDepth;
        public int TableWidth;

        public int RoomWidth;
        public int RoomHeight;

        public bool LayoutSet;
        public bool HasBeenEdited;
        public bool SideSeats;

        private Dictionary<int, Rectangle> tables;
        private Dictionary<int, Point> seats;
        private Dictionary<int, int> seatTables;

        public Room()
        {
            Initialise();
        }

        public Room(string data, bool nameOnly)
        {
            Initialise();

            if (nameOnly)
            {
                Name = data;
                SetRoomDimensions();
            }
            else
            {
                string[] details = data.Split("~".ToCharArray());

                string[] idName = details[1].Split(",".ToCharArray());

                ID = idName[0];
                Name = idName[1];

                string[] tables = details[2].Split(",".ToCharArray());
                string[] seats = details[3].Split(",".ToCharArray());
                string[] seatTables = details[4].Split(",".ToCharArray());
                SideSeats = bool.Parse(details[5]);

                int i = 0;
                while (i < tables.Length - 1)
                {
                    this.tables.Add(Convert.ToInt32(tables[i++]),
                                    new Rectangle(Convert.ToInt32(tables[i++]),
                                             Convert.ToInt32(tables[i++]),
                                             Convert.ToInt32(tables[i++]),
                                             Convert.ToInt32(tables[i++])));
                }

                i = 0;
                while (i < seats.Length - 1)
                {
                    this.seats.Add(Convert.ToInt32(seats[i++]),
                                   new Point(Convert.ToInt32(seats[i++]), Convert.ToInt32(seats[i++])));
                }

                i = 0;
                while (i < seatTables.Length - 1)
                {
                    this.seatTables.Add(Convert.ToInt32(seatTables[i++]), Convert.ToInt32(seatTables[i++]));
                }

                int lastY = 0;
                NumColumns = 1;
                NumRows = 1;
                foreach (var table in this.tables)
                {
                    if (table.Key == 1)
                    {
                        lastY = table.Value.Y;
                        TableWidth = table.Value.Width;
                        TableDepth = table.Value.Height;
                    }
                    else if (table.Value.Width != TableWidth || table.Value.Height != TableDepth)
                    {
                        HasBeenEdited = true;
                        NumColumns = 0;
                        NumRows = 0;
                        TableWidth = 0;
                        TableDepth = 0;
                        break;
                    }
                    else
                    {
                        if (table.Value.Y == lastY)
                        {
                            NumColumns++;
                        }
                        else
                        {
                            lastY = table.Value.Y;
                            NumRows++;

                            NumColumns = 1;
                        }
                    }
                }

                if (!HasBeenEdited)
                {
                    SetRoomDimensions();
                }
            }
        }

        public Room(Dictionary<int, Rectangle> tables, Dictionary<int, Point> seats,
                    Dictionary<int, int> seatTables, int numColumns, int numRows,
                    int tableDepth, int tableWidth)
        {
            Initialise();

            this.seats = seats;
            this.tables = tables;
            this.seatTables = seatTables;

            NumColumns = numColumns;
            NumRows = numRows;
            TableDepth = tableDepth;
            TableWidth = tableWidth;

            SetRoomDimensions();
        }

        private void Initialise()
        {
            ID = Guid.NewGuid().ToString();

            tables = new Dictionary<int, Rectangle>();
            seats = new Dictionary<int, Point>();
            seatTables = new Dictionary<int, int>();

            NumColumns = 1;
            NumRows = 1;
            TableDepth = 1;
            TableWidth = 1;

            LayoutSet = false;
            HasBeenEdited = false;
            SideSeats = true;
        }

        public void AddSeat(int seatIndex, int tableIndex, Point location)
        {
            seats.Add(seatIndex, location);
            seatTables.Add(seatIndex, tableIndex);
        }

        public void AddSeat(int seatIndex, int tableIndex, int x, int y)
        {
            seats.Add(seatIndex, new Point(x, y));
            seatTables.Add(seatIndex, tableIndex);
        }

        public void AddTable(int tableIndex, Rectangle shape)
        {
            tables.Add(tableIndex, shape);
        }

        public void AddTable(int tableIndex, int x, int y, int w, int h)
        {
            tables.Add(tableIndex, new Rectangle(x, y, w, h));
        }

        public void ClearRoom()
        {
            seats.Clear();
            tables.Clear();
            seatTables.Clear();

            SetRoomDimensions();
        }

        public Dictionary<int, Point> GetSeats()
        {
            return seats;
        }

        public List<int> GetSeatIndices()
        {
            return seats.Keys.ToList();
        }

        public Point GetSeatPosition(int index)
        {
            return seats[index];
        }

        public List<Point> GetSeatPositions()
        {
            List<Point> seatLocations = new List<Point>();
            foreach (var seat in seats)
            {
                seatLocations.Add(seat.Value);
            }

            return seatLocations;
        }

        public int GetNumSeatsAroundTable(int tableIndex)
        {
            return seatTables.Where(st => st.Value == tableIndex).Count();
        }

        public Dictionary<int, Point> GetSeatsAroundTable(int tableIndex)
        {
            Dictionary<int, Point> tableSeats = new Dictionary<int, Point>();
            foreach (var seat in seats)
            {
                if (seatTables[seat.Key] == tableIndex)
                {
                    tableSeats.Add(seat.Key, seat.Value);
                }
            }

            return tableSeats;
        }

        public List<int> GetSeatIndicesAroundTable(int tableIndex)
        {
            List<int> tableSeats = new List<int>();
            foreach (var seat in seats)
            {
                if (seatTables[seat.Key] == tableIndex)
                {
                    tableSeats.Add(seat.Key);
                }
            }

            return tableSeats;
        }

        public Rectangle GetTable(int index)
        {
            return tables[index];
        }

        public Dictionary<int, Rectangle> GetTables()
        {
            return tables;
        }

        public List<Rectangle> GetTableLocations()
        {
            List<Rectangle> tableLocations= new List<Rectangle>();
            foreach (var table in tables)
            {
                tableLocations.Add(table.Value);
            }

            return tableLocations;
        }

        public Dictionary<int, int> GetSeatTables()
        {
            return seatTables;
        }

        public void RemoveTable(int index)
        {
            foreach (var pair in this.seatTables.Where(st => st.Value == index).ToList())
            {
                this.seatTables.Remove(pair.Key);
                this.seats.Remove(pair.Key);
            }

            this.tables.Remove(index);
        }

        public void SetRoomDimensions()
        {
            /* Set basic constraints on plan size */
            // 1 column between tables = (TableWidth + 1) * NumColumns
            // plus an extra one for room 'margin' spacing
            RoomWidth = ((TableWidth + 1) * NumColumns) + 1;

            // Add extra columns if there are seats at the sides of tables
            if (SideSeats)
            {
                RoomWidth += NumColumns * 2;
            }

            // 2 rows between tables (1 for spacing, 1 for seats) = (TableDepth + 2) * NumRows
            // plus an extra one for room 'margin'
            RoomHeight = (TableDepth + 2) * NumRows + 1;
        }

        public void SetSeatPosition(int key, Point position)
        {
            this.seats[key] = position;
        }

        public void SetSeats(Dictionary<int, Point> seats)
        {
            this.seats = seats;
        }

        public void SetTable(int key, Point position)
        {
            Rectangle newRect = new Rectangle(position.X, position.Y, this.tables[key].Width, this.tables[key].Height);
            this.tables[key] = newRect;
        }

        public void SetTables(Dictionary<int, Rectangle> tables)
        {
            this.tables = tables;
        }

        public void SetSeatTables(Dictionary<int, int> seatTables)
        {
            this.seatTables = seatTables;
        }

        public void SetLayout()
        {
            int seatIndex = 1;
            int tableIndex = 1;

            ClearRoom();

            /* Think of room as grid with coordinates from top left */

            for (int j = 2; j < RoomHeight; j += TableDepth + 2)
            {
                for (int i = SideSeats ? 2 : 1; i < RoomWidth; i += SideSeats ? TableWidth + 3 : TableWidth + 1)
                {
                    // Strange order of doing things but ensures seats are numbered around a table.
                    // Why? Porquoi pas?

                    if (SideSeats)
                    {
                        for (int y = j + TableDepth - 1; y >= j; y--)
                        {
                            AddSeat(seatIndex++, tableIndex, i - 1, y);
                        }
                    }

                    for (int x = i; x < i + TableWidth; x++)
                    {
                        AddSeat(seatIndex++, tableIndex, x, j - 1);
                    }

                    if (SideSeats)
                    {
                        for (int y = j; y < j + TableDepth; y++)
                        {
                            AddSeat(seatIndex++, tableIndex, i + TableWidth, y);
                        }
                    }

                    // Add table at the end so we can use tableIndex++
                    AddTable(tableIndex++, i, j, TableWidth, TableDepth);
                }
            }

            LayoutSet = true;
        }

        public override string ToString()
        {
            string roomDetails = string.Format("R~{0},{1}~", ID, Name);

            foreach (var table in tables)
            {
                roomDetails += string.Format("{0},{1},{2},{3},{4},", table.Key, table.Value.X, table.Value.Y,
                                                                     table.Value.Width, table.Value.Height);
            }

            roomDetails += "~";

            foreach (var seat in seats)
            {
                roomDetails += string.Format("{0},{1},{2},", seat.Key, seat.Value.X, seat.Value.Y);
            }

            roomDetails += "~";

            foreach (var st in seatTables)
            {
                roomDetails += string.Format("{0},{1},", st.Key, st.Value);
            }

            roomDetails += "~";

            roomDetails += SideSeats;

            return roomDetails;
        }
    }
}
