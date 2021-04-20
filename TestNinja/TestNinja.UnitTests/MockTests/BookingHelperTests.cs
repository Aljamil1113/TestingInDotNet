using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockTests
{
    [TestFixture]
    public class BookingHelperTests_OverlappingBookingsExistTests
    {
        private Booking booking;
        private Mock<IBookingRepository> repository;
        private Booking existingBooking;

        [SetUp]
        public void Setup()
        {
            booking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            repository = new Mock<IBookingRepository>();
            repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
               booking
            }.AsQueryable());

            existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAndExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(existingBooking.ArrivalDate),
            }, repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(existingBooking.ArrivalDate),
                DepartureDate = After(existingBooking.ArrivalDate)
            }, repository.Object);

            Assert.That(result, Is.EqualTo(existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(existingBooking.ArrivalDate),
                DepartureDate = After(existingBooking.DepartureDate)
            }, repository.Object);

            Assert.That(result, Is.EqualTo(existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleExistingBooking_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(existingBooking.ArrivalDate),
                DepartureDate = Before(existingBooking.DepartureDate)
            }, repository.Object);

            Assert.That(result, Is.EqualTo(existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleExistingBookingButFinishesAfter_ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(existingBooking.ArrivalDate),
                DepartureDate = After(existingBooking.DepartureDate)
            }, repository.Object);

            Assert.That(result, Is.EqualTo(existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterExistingBookingId_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(existingBooking.DepartureDate),
                DepartureDate = After(existingBooking.DepartureDate, days: 2)
            }, repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(existingBooking.ArrivalDate),
                DepartureDate = After(existingBooking.DepartureDate),
                Status = "Cancelled"
            }, repository.Object);

            Assert.That(result, Is.Empty);
        }


        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
