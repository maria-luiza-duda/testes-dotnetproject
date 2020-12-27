using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Threading.Tasks;
using ONGColab.Domain;
using ONGColab.Domain.ViewModels;

namespace ONGColab.MVC.Controllers
{
    public class VoluntariadoController : BaseController
    {
        private readonly IVoluntariadoService _voluntariadoService;
        private readonly IDomainNotificationService _domainNotificationService;

        public DoacoesController(IVoluntariadoService voluntariadoService,
                                 IDomainNotificationService domainNotificationService,
                                 IToastNotification toastNotification) : base(domainNotificationService, toastNotification)
        {
            _voluntariadoService = voluntariadoService;
            _domainNotificationService = domainNotificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(nameof(Index), await _voluntariadoService.RecuperarVoluntariadosAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VoluntariadoViewModel model)
        {
            _voluntariadoService.RealizarVoluntariadoAsync(model);

            if (PossuiErrosDominio())
            {
                AdicionarErrosDominio();
                return View(model);
            }

            AdicionarNotificacaoOperacaoRealizadaComSucesso("Voluntariado disponível!<p>Faça já sua candidatura </p>");
            return RedirectToAction("Index", "Home");
        }

        private bool PossuiErrosDominio()
        {
            return _domainNotificationService.PossuiErros;
        }
    }
}