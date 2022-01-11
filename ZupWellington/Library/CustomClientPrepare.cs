using CryptoWPS;
using EFClient.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFClient.WebApi.Library
{
    public class CustomClientPrepare
    {
        internal static Cliente CriptografaCliente(Cliente cliente)
        {
            if(cliente != null)
            {
                cliente.Senha = cliente.Senha = ControleCriptografia.CriptografaSenha(cliente.Senha);
            }
            return cliente;
        }
    }
}
