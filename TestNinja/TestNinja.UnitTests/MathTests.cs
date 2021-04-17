using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var math = new Math();
;
            var result = math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        #region BLACKBOX
        [Test]
        public void Max_FirstArgumentsGreater_ReturnTheFirstArgument()
        {
            var math = new Math();

            var result = math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_SecondArgumentsGreater_ReturnTheSecondArgument()
        {
            var math = new Math();

            var result = math.Max(2, 3);

            Assert.That(result, Is.EqualTo(3));
        }


        [Test]
        public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        {
            var math = new Math();

            var result = math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }
        #endregion
    }
}
