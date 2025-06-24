namespace ToyRobotSimulator.Models
{
    // Represents the square tabletop on which the robot moves.
    // Dimensions are fixed at 5x5 units.
    public class Tabletop
    {
        public int Rows { get; }
        public int Columns { get; }

        // Constructor for the tabletop, defaults to 5x5
        public Tabletop(int rows = 5, int columns = 5)
        {
            Rows = rows;
            Columns = columns;
        }

        // Checks if a given position is within the bounds of the tabletop.
        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X < Columns &&
                   position.Y >= 0 && position.Y < Rows;
        }
    }
}