using ToyRobotSimulator.Models;
using System;
using System.Text.RegularExpressions;

namespace ToyRobotSimulator.Commands
{
    // Parses raw string commands into structured Command objects.
    public static class CommandParser
    {
        // Regex for parsing the PLACE command with X, Y, and F (Facing) arguments.
        private static readonly Regex PlaceCommandRegex = new Regex(@"^PLACE\s+(\d+),(\d+),(NORTH|SOUTH|EAST|WEST)$", RegexOptions.IgnoreCase);

        // Parses a single command string into a Command object.
        public static Command Parse(string commandString)
        {
            commandString = commandString.Trim().ToUpper();

            // Try to match the PLACE command first
            Match placeMatch = PlaceCommandRegex.Match(commandString);
            if (placeMatch.Success)
            {
                int x = int.Parse(placeMatch.Groups[1].Value);
                int y = int.Parse(placeMatch.Groups[2].Value);
                Direction direction = (Direction)Enum.Parse(typeof(Direction), placeMatch.Groups[3].Value, true);
                return new Command(x, y, direction);
            }

            // Parse other commands
            return commandString switch
            {
                "MOVE" => new Command(CommandType.MOVE),
                "LEFT" => new Command(CommandType.LEFT),
                "RIGHT" => new Command(CommandType.RIGHT),
                "REPORT" => new Command(CommandType.REPORT),
                _ => new Command(CommandType.UNKNOWN), // For invalid or unrecognized commands
            };
        }
    }
}