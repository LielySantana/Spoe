using System;
using Xunit;

namespace XUnitTestProject1
{
	public class TestFixture
	{
		public SQLiteConnection ConnectionFactory() => new SQLiteConnection("DataSource=:memory:");

	}
	public class UnitTest1
	{
		
			
		[Fact]
		public void Index_ReturnAViewResult_WithAllStudentsRegister()
		{
			

		}
	}



}
