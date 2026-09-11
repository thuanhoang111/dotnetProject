using Microsoft.EntityFrameworkCore;
using MyAppApi.Models;
using System.Collections.Generic;

namespace MyAppApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Riyosya> Riyosya{ get; set; }
}