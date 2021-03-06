﻿namespace NumberParser.Business.Tests.UnitTests
{
	using BusinessModels;
	using BusinessObjects;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class NumberParserTests
	{
		[TestMethod]
		public void ParseReturnsEmptyCollectionIfPassedNull()
		{
			var parser = new NumberParser();
			var parsedNumbers = parser.Parse(null);

			Assert.IsTrue(parsedNumbers.Count == 0);
		}
	}
}
