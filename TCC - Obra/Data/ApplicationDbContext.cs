using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TCC___Obra.Models;

namespace TCC___Obra.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TCC___Obra.Models.Agendamento> Agendamento { get; set; } = default!;
        public DbSet<TCC___Obra.Models.Incidente> Incidente { get; set; } = default!;
        public DbSet<TCC___Obra.Models.Relatorio> Relatorio { get; set; } = default!;
        public DbSet<TCC___Obra.Models.Obra> Obra { get; set; } = default!;
    }
}
