namespace NumberParser.Common.Tests.UnitTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class ErrorHandlerTests
	{
		[TestMethod]
		public void AddAddsAErrorToCollection()
		{
			ErrorHandler.Errors.Add("Test error");

			Assert.IsTrue(ErrorHandler.Errors.Count == 1);
		}

		[TestMethod]
		public void ResetClearsTheErrorsCollection()
		{
			ErrorHandler.Errors.Add("Test error");
			ErrorHandler.Reset();

			Assert.IsTrue(ErrorHandler.Errors.Count == 0);
		}
	}
}
