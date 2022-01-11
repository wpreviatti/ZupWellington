using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAdministrationView.Models
{
    public class ObjetoGenerico
    {
        public List<ClientViewModel> Clientes { get; set; }
        public ErrorViewModel Erro { get; set; }
        public ClientViewModel Cliente { get; set; }
        public ObjetoGenerico()
        {
            Erro = new ErrorViewModel();
            Cliente = new ClientViewModel();
            Clientes = new List<ClientViewModel>();
        }
    }
}
