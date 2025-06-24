using NUnit.Framework;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests
{
    [TestFixture]
    public class RobotTests
    {
        [Test]
        public void Robot_InitializesNotPlaced()
        {
            Robot robot = new Robot();
            Assert.IsFalse(robot.IsPlaced);
        }

        [Test]
        public void Robot_CanBePlaced()
        {
            Robot robot = new Robot();
            Position initialPosition = new Position(0, 0);
            Direction initialDirection = Direction.NORTH;

            robot.Place(initialPosition, initialDirection);

            Assert.IsTrue(robot.IsPlaced);
            Assert.AreEqual(initialPosition, robot.CurrentPosition);
            Assert.AreEqual(initialDirection, robot.CurrentDirection);
        }

        [TestCase(0, 0, Direction.NORTH, 0, 1)]
        [TestCase(0, 0, Direction.EAST, 1, 0)]
        [TestCase(1, 1, Direction.SOUTH, 1, 0)]
        [TestCase(1, 1, Direction.WEST, 0, 1)]
        public void Robot_GetNextPosition_ReturnsCorrectPosition(int startX, int startY, Direction startDir, int expectedX, int expectedY)
        {
            Robot robot = new Robot();
            robot.Place(new Position(startX, startY), startDir);

            Position nextPosition = robot.GetNextPosition();
            Assert.AreEqual(new Position(expectedX, expectedY), nextPosition);
        }

        [Test]
        public void Robot_MoveTo_UpdatesPosition()
        {
            Robot robot = new Robot();
            robot.Place(new Position(0, 0), Direction.NORTH);
            Position newPosition = new Position(0, 1);

            robot.MoveTo(newPosition);
            Assert.AreEqual(newPosition, robot.CurrentPosition);
        }

        [Test]
        public void Robot_RotateLeft_CyclesDirectionsCorrectly()
        {
            Robot robot = new Robot();
            robot.Place(new Position(0, 0), Direction.NORTH);

            robot.RotateLeft(); // NORTH -> WEST
            Assert.AreEqual(Direction.WEST, robot.CurrentDirection);

            robot.RotateLeft(); // WEST -> SOUTH
            Assert.AreEqual(Direction.SOUTH, robot.CurrentDirection);

            robot.RotateLeft(); // SOUTH -> EAST
            Assert.AreEqual(Direction.EAST, robot.CurrentDirection);

            robot.RotateLeft(); // EAST -> NORTH
            Assert.AreEqual(Direction.NORTH, robot.CurrentDirection);
        }

        [Test]
        public void Robot_RotateRight_CyclesDirectionsCorrectly()
        {
            Robot robot = new Robot();
            robot.Place(new Position(0, 0), Direction.NORTH);

            robot.RotateRight(); // NORTH -> EAST
            Assert.AreEqual(Direction.EAST, robot.CurrentDirection);

            robot.RotateRight(); // EAST -> SOUTH
            Assert.AreEqual(Direction.SOUTH, robot.CurrentDirection);

            robot.RotateRight(); // SOUTH -> WEST
            Assert.AreEqual(Direction.WEST, robot.CurrentDirection);

            robot.RotateRight(); // WEST -> NORTH
            Assert.AreEqual(Direction.NORTH, robot.CurrentDirection);
        }

        [Test]
        public void Robot_Report_ReturnsCorrectStringWhenPlaced()
        {
            Robot robot = new Robot();
            robot.Place(new Position(1, 2), Direction.SOUTH);
            Assert.AreEqual("Output: 1,2,SOUTH", robot.Report());
        }

        [Test]
        public void Robot_Report_ReturnsNotPlacedMessageWhenNotPlaced()
        {
            Robot robot = new Robot();
            Assert.AreEqual("Robot is not placed on the table.", robot.Report());
        }
    }
}
