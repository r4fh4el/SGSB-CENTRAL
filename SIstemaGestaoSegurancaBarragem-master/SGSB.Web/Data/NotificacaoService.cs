
using OneSignal.CSharp.SDK.Resources.Notifications;
using OneSignal.CSharp.SDK;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OneSignal.CSharp.SDK;
using OneSignal.CSharp.SDK.Resources.Notifications;
using Microsoft.JSInterop;
using OneSignal.CSharp.SDK.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SGSB.Web.Data
{ 
public class NotificacaoService
{

        HttpClient http;
        public NotificacaoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

      
   
   


    }
}