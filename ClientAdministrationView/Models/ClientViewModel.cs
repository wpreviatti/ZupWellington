using EFClient.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAdministrationView.Models
{
    public class ClientViewModel
    {
        public ErrorViewModel Erro { get; set; }
        public int IdTelefone { get; set; }
        public int NumeroChapaId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Mail { get; set; }
        public List<TelefoneViewModel> Telefone { get; set; }
        public Cliente NomeLider { get; set; }
        public string Senha { get; set; }
        public DateTime DtCadastro { get; set; }

        public ClientViewModel()
        {
            this.Erro = new ErrorViewModel();
        }
    }
}
