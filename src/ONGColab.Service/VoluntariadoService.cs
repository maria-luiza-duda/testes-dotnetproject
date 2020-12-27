using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain;
using ONGColab.Domain.Entities;
using ONGColab.Domain.ViewModels;

namespace ONGColab.Service
{
    public class VoluntariadoService : IVoluntariadoService
    {
        private readonly IMapper _mapper;
        private readonly IVoluntariadoRepository _voluntariadoRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public VoluntariadoService(IMapper mapper,
                             IVoluntariadoRepository voluntariadoRepository,
                             IDomainNotificationService domainNotificationService)
        {
            _mapper = mapper;
            _voluntariadoRepository = voluntariadoRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task RealizarDoacaoAsync(VoluntariadoViewModel model)
        {
            var entity = _mapper.Map<VoluntariadoViewModel, Voluntariado>(model);

            entity.AtualizarDataCompra();

            if (entity.Valido())
            {
                await _doacaoRepository.AdicionarAsync(entity);
                return;
            }

            _domainNotificationService.Adicionar(entity);
        }

        public async Task<IEnumerable<VoluntarixViewModel>> RecuperarVoluntarixsAsync(int pageIndex = 0)
        {
            var voluntarixs = await _voluntariadoRepository.RecuperarVoluntarixsAsync(pageIndex);
            return _mapper.Map<IEnumerable<Voluntariado>, IEnumerable<VoluntarixViewModel>>(voluntarixs);
        }
    }
}