using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public interface IRepositorioGeneros
    {
        Task<List<Genero>> ObtenerTodos();
        Task<Genero> ObtenerPorId(int id);
        Task<int> Crear(Genero genero);
    }
}
