using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace SGSB.SIG.APP.Service
{
	public interface IAuthService
	{
		Task<bool> AuthenticateAsync(string username, string password);
	}
}