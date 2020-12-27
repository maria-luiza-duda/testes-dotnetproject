using System.ComponentModel;

namespace ONGColab.Domain.ViewModels
{
    public class VoluntarixViewModel
    {
        private string _nome { get; set; }
        public string Nome
        {
            get { return Candidatura ? "Candidatura feita" : _nome; }
            set { _nome = value; }
        }

        public string Email { get; set; }

        [DisplayName("Candidatura feita")]
        public bool Candidatura { get; set; }

        public string HabilidadesVaga { get; set; }
    }
}