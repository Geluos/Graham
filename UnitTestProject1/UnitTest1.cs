using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using Graham;


namespace GrahamTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestExample()
		{
			Assert.AreEqual(1, 1);
		}

		[TestMethod]
		public void TestSpecialClass()
		{
			Assert.AreEqual(3, ExampleClass.Plus(2, 3));
		}

		[TestMethod]
		public void TestSpecialClass2()
		{
			Assert.AreEqual(false, ExampleClass.IsEven(1));

			Assert.AreEqual(false, ExampleClass.IsEven(9));

			Assert.AreEqual(true, ExampleClass.IsEven(2));

			Assert.AreEqual(true, ExampleClass.IsEven(0));
		}
	}
}
