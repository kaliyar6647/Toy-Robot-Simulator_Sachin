using NUnit.Framework;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests
{
    [TestFixture]
    public class TabletopTests
    {
        private Tabletop _tabletop;

        [SetUp]
        public void Setup()
        {
            _tabletop = new Tabletop(); // Default 5x5 tabletop
        }

        [TestCase(0, 0, true)]   // South-West corner
        [TestCase(4, 4, true)]   // North-East corner
        [TestCase(2, 2, true)]   // Middle
        [TestCase(0, 4, true)]   // North-West corner
        [TestCase(4, 0, true)]   // South-East corner
        [TestCase(-1, 0, false)] // Off left edge
        [TestCase(0, -1, false)] // Off bottom edge
        [TestCase(5, 0, false)]  // Off right edge (index 5 is out of bounds for 0-4)
        [TestCase(0, 5, false)]  // Off top edge (index 5 is out of bounds for 0-4)
        [TestCase(-1, -1, false)]// Off both edges
        [TestCase(5, 5, false)]  // Off both edges
        public void Tabletop_IsValidPosition_ReturnsCorrectly(int x, int y, bool expected)
        {
            Position pos = new Position(x, y);
            Assert.AreEqual(expected, _tabletop.IsValidPosition(pos));
        }

        [Test]
        public void Tabletop_DimensionsAreCorrect()
        {
            Assert.AreEqual(5, _tabletop.Rows);
            Assert.AreEqual(5, _tabletop.Columns);
        }
    }
}