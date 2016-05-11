namespace NumberParser.Business.Tests
{
	using System;
	using BusinessModels;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	
	[TestClass]
	public class NumberCollectionTests
	{
		[TestMethod]
		public void ToStringReturnsEmptyStringIfCollectionIsEmpty()
		{
			var collection = new NumberCollection();
			var str = collection.ToString();

			Assert.IsTrue(str == String.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void AddNullThrowsException()
		{
			var collection = new NumberCollection();
			collection.Add(null);
		}

		[TestMethod]
		public void AddAddsItem()
		{
			var collection = new NumberCollection();
			collection.Add(new Number("|", "|", "|", "|"));
			collection.Add(new Number("|", "|", "|", "|"));

			var str = collection.ToString();

			Assert.IsTrue(str == "1 1");
		}
	}
}
