using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain;
using ONGColab.Domain.Entities;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Service
{
    public class CausaService : ICausaService
    {
        private readonly IMapper _mapper;
        private readonly ICausaRepository _causaRepository;

        public CausaService(ICausaRepository causaRepository,
                            IMapper mapper)
        {
            _mapper = mapper;
            _causaRepository = causaRepository;
        }

        public async Task Adicionar(CausaViewModel model)
        {
            var causa = _mapper.Map<CausaViewModel, Causa>(model);
            await _causaRepository.Adicionar(causa);
        }

        public async Task<IEnumerable<CausaViewModel>> RecuperarCausas()
        {
            var causas = await _causaRepository.RecuperarCausas();
            return _mapper.Map<IEnumerable<Causa>, IEnumerable<CausaViewModel>>(causas);
        }
    }
}