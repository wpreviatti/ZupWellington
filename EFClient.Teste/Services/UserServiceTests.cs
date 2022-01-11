using AutoMapper;
using EFClient.Repo;
using EFClient.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;


namespace EFClient.Teste.Services
{
    public class ClientServices
    {
        private ClienteController clienteController;
        public ClientServices()
        {
            //clienteController = new ClienteController(new Mock<IEFCoreRepository>().Object, new Mock<IMapper>().Object);
        }
    }
}
