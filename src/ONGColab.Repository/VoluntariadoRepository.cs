using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain;
using ONGColab.Domain.Entities;
using ONGColab.Repository.Context;

namespace ONGColab.Repository
{
    public class VoluntariadoRepository : IVoluntariadoRepository
    {
        private readonly ILogger<VoluntariadoRepository> _logger;
        private readonly GloballAppConfig _globalSettings;
        private readonly ONGColabOnlineDBContext _ongcolabOnlineDBContext;

        public VoluntariadoRepository(GloballAppConfig globalSettings,
                                ONGColabOnlineDBContext ongcolabDbContext,
                                ILogger<VoluntariadoRepository> logger)
        {
            _globalSettings = globalSettings;
            _ongcolabOnlineDBContext = ongcolabDbContext;
            _logger = logger;
        }

        public async Task AdicionarAsync(Voluntariado model)
        {
            await _ongcolabOnlineDBContext.Doacoes.AddAsync(model);
            await _ongcolabOnlineDBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Voluntariado>> RecuperarVoluntarixsAsync(int pageIndex = 0)
        {
            return await _ongcolabOnlineDBContext.Voluntariado.Include("DadosPessoais").ToListAsync();
        }
    }
}