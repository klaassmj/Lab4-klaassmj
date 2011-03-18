using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestGetCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String Sriram = "Sriram's House";
            String Rose = "Rose";
            using (mocks.Record())
            {
                // The mock will return "Whale Rider" when the call is made with 24
                mockDatabase.getCarLocation(1);
                LastCall.Return(Sriram);
                mockDatabase.getCarLocation(2);
                LastCall.Return(Rose);
            }
            var target = new Car(10);
            target.Database = mockDatabase;
            String result;
            result = target.getCarLocation(1);
            Assert.AreEqual(result, Sriram);
            result = target.getCarLocation(2);
            Assert.AreEqual(result, Rose);
        }

        [Test()]
        public void TestMileage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Mil = 100;
            mockDatabase.Miles = Mil;
            var target = ObjectMother.BMW();
            target.Database = mockDatabase;
            int miles = target.Mileage;
            Assert.AreEqual(miles, Mil);
        }
	}
}
