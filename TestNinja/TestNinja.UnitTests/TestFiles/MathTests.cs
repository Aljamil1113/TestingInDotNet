using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.TestFiles
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        //Setup
        //Teardown

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        [Ignore("I'm warning you!")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
           
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        #region BLACKBOX
        //[Test]
        //public void Max_FirstArgumentsGreater_ReturnTheFirstArgument()
        //{

        //    var result = _math.Max(2, 1);

        //    Assert.That(result, Is.EqualTo(2));
        //}

        //[Test]
        //public void Max_SecondArgumentsGreater_ReturnTheSecondArgument()
        //{
        //    var result = _math.Max(2, 3);

        //    Assert.That(result, Is.EqualTo(3));
        //}


        //[Test]
        //public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        //{
        //    var result = _math.Max(1, 1);

        //    Assert.That(result, Is.EqualTo(1));
        //}
        #endregion

        [Test]
        [TestCase(2,1,2)]
        [TestCase(2,3,3)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {

            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            //Assert.That(result, Is.Not.Empty);

            //Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result, Does.Contain(1));
            //Assert.That(result, Does.Contain(3));
            //Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }
    }
}
