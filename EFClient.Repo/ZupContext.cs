using EFClient.Dominio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFClient.Repo
{
    public class ZupContext : DbContext
    {
        public ZupContext()
        {

        }
        public ZupContext(DbContextOptions<ZupContext> options) : base(options) {}
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder
            { //Password=123;Persist Security Info=True;User ID=SA;Initial Catalog=master;Data Source=192.168.1.211
                DataSource = "192.168.1.211",
                InitialCatalog = "BancoZUP",
                UserID = "SA",
                Password = "123"
            };

            var connectionString = builder.ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
