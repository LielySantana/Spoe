using System;
using System.Collections.Generic;
using System.Text;
using Spoe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace XUnitTestProject1
{
    class TestResolver
    {
        public TestResolver()
        {
            _users = new List<Empleado>
            {
                 new Empleado() { Id = "1", UserName = "User1", Email ="user1@bv.com"}
            };
            UserManager = MockUserManager(_users).Object;
        }
        public SQLiteConnection ConnectionFactory() => new SQLiteConnection("DataSource=:memory:");
        public DbContextOptions<ApplicationDbContext> DbOptionsFactory(SQLiteConnection connection) =>
        new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite(connection)
        .Options;


        public Estudiante EstudianteFactory() => new Estudiante {
            Nombres = Guid.NewGuid().ToString(),
            Apellidos = Guid.NewGuid().ToString(),
            GradoCursar = new Random().Next(1, 12),
            NombreTutor = Guid.NewGuid().ToString(),
            TelefonoTutor = new Random().Next(1000000, 9999999),
            Direccion = Guid.NewGuid().ToString(),
            Documentos = Guid.NewGuid().ToString(),
            Fecha = DateTime.Now,
        };
        private List<Empleado> _users;
        public UserManager<Empleado> UserManager{get; set;}
        public void RunWithDatabase(
        Action<ApplicationDbContext> arrange,
        Func<ApplicationDbContext, IActionResult> act,
        Action<IActionResult> assert)
        {
            var connection = ConnectionFactory();
            connection.Open();

            try
            {
                var options = DbOptionsFactory(connection);

                using (var context = new ApplicationDbContext(options))
                {
                    context.Database.EnsureCreated();
                    // Arrange
                    arrange?.Invoke(context);
                }

                using (var context = new ApplicationDbContext(options))
                {
                    // Act (and pass result into assert)
                    var result = act.Invoke(context);
                    // Assert
                    assert.Invoke(result);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task RunWithDatabaseAsync(
        Func<ApplicationDbContext, Task<dynamic>> arrange,
        Func<ApplicationDbContext, dynamic, Task<IActionResult>> act,
        Action<IActionResult, dynamic> assert)
        {
            var connection = ConnectionFactory();
            await connection.OpenAsync();

            try
            {
                object data;
                var options = DbOptionsFactory(connection);

                using (var context = new ApplicationDbContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                    data = arrange != null
                        ? await arrange?.Invoke(context)
                        : null;
                }

                using (var context = new ApplicationDbContext(options))
                {
                    var result = await act.Invoke(context, data);
                    assert.Invoke(result, data);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

    }
}
