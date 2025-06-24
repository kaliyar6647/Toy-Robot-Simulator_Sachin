using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Commands
{
    // Enum representing the types of commands the simulator can process.
    public enum CommandType
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT,
        UNKNOWN
    }

    // Represents a command for the robot simulator.
    public class Command
    {
        public CommandType Type { get; }
        public int? X { get; }
        public int? Y { get; }
        public Direction? Direction { get; }

        // Constructor for general commands (MOVE, LEFT, RIGHT, REPORT)
        public Command(CommandType type)
        {
            Type = type;
        }

        // Constructor for PLACE command
        public Command(int x, int y, Direction direction)
        {
            Type = CommandType.PLACE;
            X = x;
            Y = y;
            Direction = direction;
        }
    }
}
