using AutoMapper;
using ClientAdministrationView.Models;
using EFClient.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAdministrationView.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            ProgramaMapper();
        }

        public void ProgramaMapper()
        {
            CreateMap<Cliente, ClientViewModel>();
            CreateMap<ClientViewModel, Cliente>();

            CreateMap<Telefone, TelefoneViewModel>().ReverseMap();
        }
    }
}
