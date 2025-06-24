using NUnit.Framework;
using ToyRobotSimulator.Commands;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests
{
    [TestFixture]
    public class CommandParserTests
    {
        [TestCase("PLACE 0,0,NORTH", CommandType.PLACE, 0, 0, Direction.NORTH)]
        [TestCase("PLACE 4,2,SOUTH", CommandType.PLACE, 4, 2, Direction.SOUTH)]
        [TestCase("place 1,3,west", CommandType.PLACE, 1, 3, Direction.WEST)] // Case insensitive
        public void Parse_PlaceCommand_ReturnsCorrectCommand(string commandString, CommandType expectedType, int expectedX, int expectedY, Direction expectedDirection)
        {
            Command command = CommandParser.Parse(commandString);
            Assert.AreEqual(expectedType, command.Type);
            Assert.AreEqual(expectedX, command.X);
            Assert.AreEqual(expectedY, command.Y);
            Assert.AreEqual(expectedDirection, command.Direction);
        }

        [TestCase("MOVE", CommandType.MOVE)]
        [TestCase("LEFT", CommandType.LEFT)]
        [TestCase("RIGHT", CommandType.RIGHT)]
        [TestCase("REPORT", CommandType.REPORT)]
        [TestCase("move", CommandType.MOVE)] // Case insensitive
        public void Parse_SimpleCommands_ReturnsCorrectCommand(string commandString, CommandType expectedType)
        {
            Command command = CommandParser.Parse(commandString);
            Assert.AreEqual(expectedType, command.Type);
            Assert.IsFalse(command.X.HasValue);
            Assert.IsFalse(command.Y.HasValue);
            Assert.IsFalse(command.Direction.HasValue);
        }

        [TestCase("UNKNOWN_COMMAND")]
        [TestCase("PLACE 0,0")] // Missing direction
        [TestCase("PLACE 0,0,INVALID")] // Invalid direction
        [TestCase("PLACECORRECTLY 0,0,NORTH")] // Typo in command
        [TestCase("MOVE ")] // Trailing space
        [TestCase(" PLACE 0,0,NORTH")] // Leading space
        public void Parse_InvalidCommands_ReturnsUnknownCommand(string commandString)
        {
            Command command = CommandParser.Parse(commandString);
            Assert.AreEqual(CommandType.UNKNOWN, command.Type);
        }
    }
}
