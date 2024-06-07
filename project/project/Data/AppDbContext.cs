using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project.Models;
using System;

namespace project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
 
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CallBack> CallBack { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Lastnewsupdate> Lastnewsupdate { get; set; }
        public virtual DbSet<Rent> Rent { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
    }
 }
