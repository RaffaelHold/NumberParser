namespace NumberParser.Business.Tests.UnitTests
{
	using BusinessModels;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class NumberTests
	{
		[TestMethod]
		public void NumberConstructorHandlesNullValues()
		{
			var number = new Number(null, null, null, null);

			Assert.IsTrue(string.Empty == number.GetNumber());
		}

		[TestMethod]
		public void GetNumberReturnsNumber()
		{
			var number = new Number("|", "|", "|", "|");

			Assert.IsTrue("1" == number.GetNumber());
		}

		[TestMethod]
		public void GetNumberReturnsEmptyStringIfInvalidNumber()
		{
			var number = new Number("asd", null, null, null);

			Assert.IsTrue(string.Empty == number.GetNumber());
		}
	}
}
