using AutoMapper;
using ClientAdministrationView.Models;
using ClientAdministrationView.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EFClient.Dominio;

namespace ClientAdministrationView.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IMapper _mapper;
        public ClientesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Gerenciar");
        }

        public async Task<IActionResult> GerenciarAsync()
        {
            ObjetoGenerico clientesMod = new ObjetoGenerico();

            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Get, $"Cliente");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var client = JsonConvert.DeserializeObject<List<Cliente>>(await response.Content.ReadAsStringAsync());
                clientesMod.Clientes = new List<ClientViewModel>();
                if (client != null)
                {
                    foreach (var linha in client)
                        clientesMod.Clientes.Add(_mapper.Map<ClientViewModel>(linha));
                }
            }
            else
            {
                clientesMod.Erro = new ErrorViewModel() { Mensagem = "Ooops, algo deu errado na pesquisa dos clientes. Verifique com o administrador do sistema para mais detalhes." };
            }

            return View(clientesMod);
        }

        public IActionResult Adicionar()
        {
            ClientViewModel clientesMod = new ClientViewModel();

            return View(clientesMod);
        }

        public async Task<IActionResult> EditarAsync(int idCliente)
        {
            ObjetoGenerico clientesMod = new ObjetoGenerico();

            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Get, $"Cliente/{idCliente}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var client = JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());
                clientesMod.Cliente = _mapper.Map<ClientViewModel>(client);
            }
            else
            {
                clientesMod.Erro = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            }

            return View(clientesMod);
        }

        public async Task<JsonResult> DoExcluirClienteAsync(int IdCliente)
        {
            var cliente = new ClientViewModel();
            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Delete, $"Cliente/{IdCliente}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                cliente.Erro = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
                //cliente.Erro = new ErroModel() { Mensagem = $"Não foi possível encontra o cliente {cliente.IdCliente}." };
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                cliente.Erro = new ErrorViewModel() { Mensagem = "Ooops, algo deu de errado na exclusão do cliente. Entre em contato com o administrador do sistema para mais detalhes." };
            }
            return new JsonResult(cliente);
        }

        public async Task<JsonResult> DoAdicionarClienteAsync([FromBody] ClientViewModel cliente)
        {
            ClientViewModel clienteModel = new ClientViewModel();
            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Post, "Cliente", cliente);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clienteModel.Erro = new ErrorViewModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clienteModel.Erro = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clienteModel);
        }

        public async Task<JsonResult> DoEditarClienteAsync(int id, [FromBody] ClientViewModel cliente)
        {
            ClientViewModel clienteModel = new ClientViewModel();
            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Put, $"Cliente/{id}", cliente);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clienteModel.Erro = new ErrorViewModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clienteModel.Erro = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clienteModel);
        }

        public async Task<JsonResult> DoExcluirTelefoneAsync([FromBody] ClientViewModel clientes)
        {

            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Delete, $"Telefones/{clientes.IdTelefone}");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clientes.Erro = new ErrorViewModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clientes.Erro = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clientes);
        }

        public async Task<JsonResult> DoAdicionarTelefoneAsync(int id, [FromBody] TelefoneViewModel telefone)
        {
            ClientViewModel clientes = new ClientViewModel();
            HttpResponseMessage response = APIRequisicao.SendRequest(HttpMethod.Put, $"Telefones/novoTelefone/{id}", telefone);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clientes.Erro = new ErrorViewModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clientes.Erro = JsonConvert.DeserializeObject<ErrorViewModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clientes);
        }
    }
}
