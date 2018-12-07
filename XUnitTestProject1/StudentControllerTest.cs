using System;
using System.Data.SQLite;
using Xunit;
using Spoe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Spoe.Controllers;

namespace XUnitTestProject1
{
	
	public class StudentControllerTest
    {

        private readonly TestResolver _testDependencyResolver = new TestResolver();
        private async Task<dynamic> Arrange(ApplicationDbContext context)
        {
            context.Estudiantes.Add(_testDependencyResolver.EstudianteFactory());
            return await context.SaveChangesAsync();
        }
        [Fact]
		public async Task Estudiantes_ReturnAViewResult_WithAllStudentsRegister()
		{
            await _testDependencyResolver.RunWithDatabaseAsync(
            arrange: async context =>
            {
                context.Estudiantes.Add(_testDependencyResolver.EstudianteFactory());
                var ret = await context.SaveChangesAsync();
                return ret;
            },
            act: async (context, ret) => await new EstudiantesController(context, _testDependencyResolver.UserManager).Index(),
            assert: (result, ret) => Assert.IsType<ViewResult>(result)
        );
        }
	}



}
