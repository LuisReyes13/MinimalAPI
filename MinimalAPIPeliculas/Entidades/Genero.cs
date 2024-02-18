using System.ComponentModel.DataAnnotations;

namespace MinimalAPIPeliculas.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public List<GeneroPelicula> GeneroPeliculas { get; set; } = new List<GeneroPelicula>();
    }
}
