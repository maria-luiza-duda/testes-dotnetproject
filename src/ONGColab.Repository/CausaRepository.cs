using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ONGColab.Domain;
using ONGColab.Domain.Entities;
using ONGColab.Repository.Context;

namespace ONGColab.Repository
{
    public class CausaRepository : ICausaRepository
    {
        private readonly ONGColabOnlineDBContext _dbContext;

        public CausaRepository(ONGColabOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Causa> Adicionar(Causa causa)
        {
            await _dbContext.AddAsync(causa);
            await _dbContext.SaveChangesAsync();

            return causa;
        }

        public async Task<IEnumerable<Causa>> RecuperarCausas()
        {
            return await _dbContext.Causas.ToListAsync();
        }
    }
}