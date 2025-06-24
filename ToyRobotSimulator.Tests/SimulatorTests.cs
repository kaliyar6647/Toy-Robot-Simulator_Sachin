using NUnit.Framework;
using ToyRobotSimulator;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests
{
    [TestFixture]
    public class SimulatorTests
    {
        private Simulator _simulator;

        [SetUp]
        public void Setup()
        {
            _simulator = new Simulator();
        }

        [Test]
        public void Simulator_IgnoresCommandsBeforeFirstPlace()
        {
            // Robot not placed initially
            string result = _simulator.ProcessCommand("MOVE");
            Assert.That(result, Does.StartWith("Robot not placed."));

            result = _simulator.ProcessCommand("LEFT");
            Assert.That(result, Does.StartWith("Robot not placed."));

            result = _simulator.ProcessCommand("REPORT");
            Assert.That(result, Does.StartWith("Robot not placed."));
        }

        [Test]
        public void Simulator_ProcessesFirstPlaceCommand()
        {
            _simulator.ProcessCommand("PLACE 0,0,NORTH");
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,0,NORTH", report);
        }

        [Test]
        public void Simulator_ExampleA()
        {
            _simulator.ProcessCommand("PLACE 0,0,NORTH");
            _simulator.ProcessCommand("MOVE");
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,1,NORTH", report);
        }

        [Test]
        public void Simulator_ExampleB()
        {
            _simulator.ProcessCommand("PLACE 0,0,NORTH");
            _simulator.ProcessCommand("LEFT");
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,0,WEST", report);
        }

        [Test]
        public void Simulator_ExampleC()
        {
            _simulator.ProcessCommand("PLACE 1,2,EAST");
            _simulator.ProcessCommand("MOVE");
            _simulator.ProcessCommand("MOVE");
            _simulator.ProcessCommand("LEFT");
            _simulator.ProcessCommand("MOVE");
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 3,3,NORTH", report);
        }

        [Test]
        public void Simulator_PreventsRobotFalling()
        {
            // Place at top right, facing North
            _simulator.ProcessCommand("PLACE 4,4,NORTH");
            string result = _simulator.ProcessCommand("MOVE"); // Should not move
            Assert.That(result, Does.StartWith("Robot cannot move."));

            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 4,4,NORTH", report); // Still at 4,4

            // Place at bottom left, facing West
            _simulator.ProcessCommand("PLACE 0,0,WEST");
            result = _simulator.ProcessCommand("MOVE"); // Should not move
            Assert.That(result, Does.StartWith("Robot cannot move."));

            report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,0,WEST", report); // Still at 0,0

            // Test further valid movement after an invalid one
            _simulator.ProcessCommand("RIGHT"); // WEST -> NORTH
            report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,0,NORTH", report);

            _simulator.ProcessCommand("MOVE"); // Should move to 0,1
            report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,1,NORTH", report);
        }

        [Test]
        public void Simulator_CanRePlaceRobot()
        {
            _simulator.ProcessCommand("PLACE 0,0,NORTH");
            _simulator.ProcessCommand("MOVE"); // Robot is at 0,1,NORTH
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,1,NORTH", report);

            _simulator.ProcessCommand("PLACE 2,2,SOUTH"); // Re-place robot
            report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 2,2,SOUTH", report);

            _simulator.ProcessCommand("MOVE"); // Move from new position
            report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 2,1,SOUTH", report);
        }

        [Test]
        public void Simulator_IgnoresInvalidPlaceCommands()
        {
            // Invalid PLACE command - off table
            string result = _simulator.ProcessCommand("PLACE 5,5,NORTH");
            Assert.That(result, Does.StartWith("Invalid PLACE command."));

            // Robot should still not be placed
            result = _simulator.ProcessCommand("MOVE");
            Assert.That(result, Does.StartWith("Robot not placed."));

            // Now place it correctly
            _simulator.ProcessCommand("PLACE 0,0,NORTH");
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,0,NORTH", report);
        }

        [Test]
        public void Simulator_HandlesMalformedPlaceCommands()
        {
            string result = _simulator.ProcessCommand("PLACE 0,0"); // Missing direction
            Assert.That(result, Does.StartWith("Invalid PLACE command format."));

            result = _simulator.ProcessCommand("PLACE 0,0,INVALID_DIRECTION"); // Invalid direction
            Assert.That(result, Does.StartWith("Unknown command:")); // Regex won't match, falls to unknown

            // Robot should still not be placed
            result = _simulator.ProcessCommand("MOVE");
            Assert.That(result, Does.StartWith("Robot not placed."));
        }

        [Test]
        public void Simulator_HandlesUnknownCommands()
        {
            _simulator.ProcessCommand("PLACE 0,0,NORTH");
            string result = _simulator.ProcessCommand("JUMP");
            Assert.AreEqual("Unknown command: JUMP", result);

            result = _simulator.ProcessCommand("GO AHEAD");
            Assert.AreEqual("Unknown command: GO AHEAD", result);

            // Robot state should be unchanged
            string report = _simulator.ProcessCommand("REPORT");
            Assert.AreEqual("Output: 0,0,NORTH", report);
        }
    }
}
