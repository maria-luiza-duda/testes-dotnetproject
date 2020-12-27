using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ONGColab.Domain;
using ONGColab.Domain.ViewModels;
using ONGColab.Repository.Context;

namespace ONGColab.Repository
{
    public class HomeInfoRepository : IHomeInfoRepository
    {
        private readonly ONGColabOnlineDBContext _dbContext;

        public HomeInfoRepository(ONGColabOnlineDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HomeViewModel> RecuperarDadosIniciaisHomeAsync()
        {
            var totalVoluntarixs = _dbContext.Voluntariado.CountAsync();
        }
    }
}