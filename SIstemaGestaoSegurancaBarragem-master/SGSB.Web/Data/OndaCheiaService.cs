using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SGSB.Web.Models;

namespace SGSB.Web.Data
{
    public class OndaCheiaService
    {
        HttpClient http;
        public OndaCheiaService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


   
    }
}

