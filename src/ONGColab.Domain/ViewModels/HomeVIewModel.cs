using System.Collections.Generic;
using System.ComponentModel;

namespace ONGColab.Domain.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Voluntarixs = new List<VoluntarixViewModel>();
            ONGs = new List<CausaViewModel>();
        }

        [DisplayName("Quantidade de Voluntarixs")]
        public int QuantidadeVoluntarixs { get; set; }

        public IEnumerable<VoluntarixViewModel> Voluntarixs { get; set; }
        public IEnumerable<CausaViewModel> ONGs { get; set; }
    }
}