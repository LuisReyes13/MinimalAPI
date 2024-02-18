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
            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(p => p.Generos, entidad => entidad.MapFrom(p => 
                p.GeneroPeliculas.Select(gp => 
                    new GeneroDTO { 
                        Id = gp.GeneroId, 
                        Nombre = gp.Genero.Nombre})))
                .ForMember(p => p.Actores, entidad => entidad.MapFrom(p => 
                p.ActoresPeliculas.Select(ap => 
                    new ActorPeliculaDTO { 
                        Id = ap.ActorId, 
                        Nombre = ap.Actor.Nombre, 
                        Personaje = ap.Personaje})));

            CreateMap<CrearComentarioDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();

            CreateMap<AsignarActorPeliculaDTO, ActorPelicula>();
        }
    }
}
