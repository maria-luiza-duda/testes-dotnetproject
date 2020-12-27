using AutoMapper;
using System;
using ONGColab.Domain.Entities;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Service.AutoMapper
{
    public class ONGColabOnLineMappingProfile : Profile
    {
        public ONGColabOnLineMappingProfile()
        {   
            CreateMap<Voluntarix, VoluntarioxViewModel>();
            CreateMap<Voluntariado, VoluntariadoViewModel>();
            CreateMap<Experiencia, ExperienciaViewModel>();
            CreateMap<Causa, CausaViewModel>();

            CreateMap<Voluntarix, VoluntarixViewModel>()
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.DadosPessoais.Nome))
                .ForMember(dest => dest.Candidatura, m => m.MapFrom(src => src.Candidatura))
                .ForMember(dest => dest.HabilidadesVaga, m => m.MapFrom(src => src.DadosPessoais.HabilidadesVaga))

            CreateMap<VoluntarixViewModel, Voluntarix>()
                .ConstructUsing(src => new Voluntarix(Guid.NewGuid(), src.Nome, src.Email, src.Candidatura, src.HabilidadesVaga));

           CreateMap<CausaViewModel, Causa>()
                .ConstructUsing(src => new Causa(Guid.NewGuid(), src.ONG, src.Cidade, src.Estado));

            CreateMap<ExperienciaViewModel, Experiencia>()
                .ConstructUsing(src => new Experiencia(Guid.NewGuid(), src.Formacao, src.ExperienciaProfissional, src.TempoTrabalho, src.AtividadesExercidas));

            CreateMap<VoluntariadoViewModel, Voluntariado>()
                .ForCtorParam("dadosPessoais", opt => opt.MapFrom(src => src.DadosPessoais))
                .ForCtorParam("funcaoTrabalho", opt => opt.MapFrom(src => src.FuncaoTrabalho))
                .ForCtorParam("xp", opt => opt.MapFrom(src => src.Xp));
        }
    }
}