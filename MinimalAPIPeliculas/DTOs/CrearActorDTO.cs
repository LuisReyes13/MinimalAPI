namespace MinimalAPIPeliculas.DTOs
{
    public class CrearActorDTO
    {
        public string Nombre { get; set; } = null!;
        public DateTime FechaNacimietno { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
