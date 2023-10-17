﻿using System.Net;

namespace next_generation.Models
{
    public class APIResponse
    {

      
        public HttpStatusCode statusCode { get; set; }

        public bool IsExitoso { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        //objeto resultado puede ser lista, objeto, etc, esta propiedad almacena lo que sea, ESTANDAR
        public object Resultado { get; set; }

        public int TotalPaginas { get; set; }

    }
}
