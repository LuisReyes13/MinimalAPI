namespace MinimalAPIPeliculas.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaNacimietno { get; set; }
        public string? Foto { get; set; }
    }
}
