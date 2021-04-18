using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.TestFiles
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_InputIsLessZero_ReturnArgumentOutOfRangeException(int speed)
        {
            var demeritPoints = new DemeritPointsCalculator();

            Assert.That(() => demeritPoints.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        //[Test]
        //public void CalculateDemeritPoints_InputIsLessOrEqualSpeedLimit_ReturnZero()
        //{

        [Test]
        [TestCase(0,0)]
        [TestCase(64,0)]
        [TestCase(65,0)]
        [TestCase(66,0)]
        [TestCase(70, 1)]
        public void CalculateDemeritPoints_InputIsComputed_ReturnDemeritPoints(int speed, int expectedResult)
        {
        var calculator = new DemeritPointsCalculator();

        var result = calculator.CalculateDemeritPoints(speed);

        Assert.That(result, Is.EqualTo(expectedResult));

        }

    }
}
