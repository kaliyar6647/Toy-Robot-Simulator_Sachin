using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Models;
using System;

namespace ToyRobotSimulator
{
    // The main simulator class that orchestrates the robot and tabletop interactions.
    public class Simulator
    {
        private readonly Tabletop _tabletop;
        private readonly Robot _robot;

        // Constructor initializes the tabletop and robot.
        public Simulator()
        {
            _tabletop = new Tabletop(); // Default 5x5
            _robot = new Robot();
        }

        // Processes a single command.
        public string ProcessCommand(string commandString)
        {
            Command command = CommandParser.Parse(commandString);

            // If the robot is not placed, only a valid PLACE command is accepted.
            if (!_robot.IsPlaced && command.Type != CommandType.PLACE)
            {
                return "Robot not placed. Ignoring command: " + commandString;
            }

            switch (command.Type)
            {
                case CommandType.PLACE:
                    // Ensure PLACE command has valid arguments
                    if (command.X.HasValue && command.Y.HasValue && command.Direction.HasValue)
                    {
                        Position newPosition = new Position(command.X.Value, command.Y.Value);
                        if (_tabletop.IsValidPosition(newPosition))
                        {
                            _robot.Place(newPosition, command.Direction.Value);
                            return string.Empty; // Success, no output needed for PLACE
                        }
                        else
                        {
                            return $"Invalid PLACE command. Position {newPosition} is outside the table.";
                        }
                    }
                    else
                    {
                        return "Invalid PLACE command format. Expected: PLACE X,Y,F";
                    }

                case CommandType.MOVE:
                    // Get the next intended position and check if it's valid.
                    Position nextPosition = _robot.GetNextPosition();
                    if (_tabletop.IsValidPosition(nextPosition))
                    {
                        _robot.MoveTo(nextPosition);
                        return string.Empty; // Success
                    }
                    else
                    {
                        return "Robot cannot move. Next position would be off the table.";
                    }

                case CommandType.LEFT:
                    _robot.RotateLeft();
                    return string.Empty; // Success

                case CommandType.RIGHT:
                    _robot.RotateRight();
                    return string.Empty; // Success

                case CommandType.REPORT:
                    return _robot.Report(); // Returns the robot's state

                case CommandType.UNKNOWN:
                default:
                    return $"Unknown command: {commandString}";
            }
        }
    }
}
