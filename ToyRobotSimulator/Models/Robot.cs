namespace ToyRobotSimulator.Models
{
    // Represents the toy robot.
    public class Robot
    {
        public Position CurrentPosition { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public bool IsPlaced { get; private set; }

        // Constructor for the robot. Initially not placed.
        public Robot()
        {
            IsPlaced = false;
        }

        // Places the robot on the table at a specified position and direction.
        public void Place(Position position, Direction direction)
        {
            CurrentPosition = position;
            CurrentDirection = direction;
            IsPlaced = true;
        }

        // Moves the robot one unit forward in its current direction.
        // It's the responsibility of the caller (Simulator) to check if the move is valid.
        public Position GetNextPosition()
        {
            int newX = CurrentPosition.X;
            int newY = CurrentPosition.Y;

            switch (CurrentDirection)
            {
                case Direction.NORTH:
                    newY++;
                    break;
                case Direction.EAST:
                    newX++;
                    break;
                case Direction.SOUTH:
                    newY--;
                    break;
                case Direction.WEST:
                    newX--;
                    break;
            }
            return new Position(newX, newY);
        }

        // Updates the robot's position after a valid move.
        public void MoveTo(Position newPosition)
        {
            CurrentPosition = newPosition;
        }

        // Rotates the robot 90 degrees to the left.
        public void RotateLeft()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.NORTH => Direction.WEST,
                Direction.WEST => Direction.SOUTH,
                Direction.SOUTH => Direction.EAST,
                Direction.EAST => Direction.NORTH,
                _ => CurrentDirection, // Should not happen
            };
        }

        // Rotates the robot 90 degrees to the right.
        public void RotateRight()
        {
            CurrentDirection = CurrentDirection switch
            {
                Direction.NORTH => Direction.EAST,
                Direction.EAST => Direction.SOUTH,
                Direction.SOUTH => Direction.WEST,
                Direction.WEST => Direction.NORTH,
                _ => CurrentDirection, // Should not happen
            };
        }

        // Reports the current position and direction of the robot.
        public string Report()
        {
            if (!IsPlaced)
            {
                return "Robot is not placed on the table.";
            }
            return $"Output: {CurrentPosition.X},{CurrentPosition.Y},{CurrentDirection}";
        }
    }
}