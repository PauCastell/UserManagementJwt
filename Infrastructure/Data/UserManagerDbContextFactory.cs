using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    /*
     * En tiempo de diseño, Entity Framework Core necesita una forma de crear una instancia del DbContext.
     * Esta clase implementa IDesignTimeDbContextFactory para proporcionar esa funcionalidad.
     */
    internal class UserManagerDbContextFactory : IDesignTimeDbContextFactory<UserManagerDbContext>
    {
        public UserManagerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserManagerDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserApiDb;Trusted_Connection=True;");

            return new UserManagerDbContext(optionsBuilder.Options);
        }
    }
}
