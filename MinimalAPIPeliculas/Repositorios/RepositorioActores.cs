using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public class RepositorioActores : IRepositorioActores
    {
        private readonly ApplicationDbContext context;
        public RepositorioActores(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Actor>> ObtenerTodos()
        {
            return await context.Actores.OrderBy(a => a.Nombre).ToListAsync();
        }

        public async Task<Actor?> ObtenerPorId(int id)
        {
            return await context.Actores.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> Crear(Actor actor)
        {
            context.Add(actor);
            await context.SaveChangesAsync();
            return actor.Id;
        }

        public async Task<bool> Existe(int id)
        {
            return await context.Actores.AnyAsync(a => a.Id == id);
        }

        public async Task Actualizar(Actor actor)
        {
            context.Update(actor);
            await context.SaveChangesAsync();
        }

        public async Task Borrar(int id)
        {
            await context.Actores.Where(a => a.Id == id).ExecuteDeleteAsync();
        }
    }
}
