using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp
{

    public partial class helloappdbContext : DbContext
    {
        public helloappdbContext()
        {
        }
        public helloappdbContext(DbContextOptions<helloappdbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)
\\mssqllocaldb; Database = helloappdb; Trusted_Connection = True; ");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }



    class Program
    {
        static void Main(string[] args)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                // получаем объекты из БД и выводим на консоль
                var users = db.Users.ToList();
                Console.WriteLine("Список объектов:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");
                }
            }
            Console.ReadKey();
        }
    }
}
