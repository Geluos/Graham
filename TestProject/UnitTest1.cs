namespace TestProject
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
			Assert.AreEqual(5, Graham.ExampleClass.Plus(2, 3));
		}

		[TestMethod]
		public void TestSpecialClass2()
		{
			Assert.AreEqual(false, Graham.ExampleClass.IsEven(1));

			Assert.AreEqual(false, Graham.ExampleClass.IsEven(9));

			Assert.AreEqual(true, Graham.ExampleClass.IsEven(2));

			Assert.AreEqual(true, Graham.ExampleClass.IsEven(0));
		}
	}
}