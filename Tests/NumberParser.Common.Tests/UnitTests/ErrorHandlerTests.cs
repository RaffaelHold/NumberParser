namespace NumberParser.Common.Tests.UnitTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class ErrorHandlerTests
	{
		[TestMethod]
		public void ResetClearsTheErrorsCollection()
		{
			ErrorHandler.Errors.Add("Test error");
			ErrorHandler.Reset();

			Assert.IsTrue(ErrorHandler.Errors.Count == 0);
		}
	}
}
