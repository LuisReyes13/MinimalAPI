using AutoMapper;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearGeneroDTO, Genero>();
            CreateMap<Genero, GeneroDTO>();

            CreateMap<CrearActorDTO, Actor>()
                .ForMember(a => a.Foto, opciones => opciones.Ignore());
            CreateMap<Actor, ActorDTO>();

            CreateMap<CrearPeliculaDTO, Pelicula>()
                .ForMember(p => p.Poster, opciones => opciones.Ignore());
            CreateMap<Pelicula, PeliculaDTO>();
        }
    }
}
