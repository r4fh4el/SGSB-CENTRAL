using SGSB.AppPopulacao.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSB.AppPopulacao.MVVM.ViewModel
{
    public class TicketViewModel
    {
        public TicketModel TicketModel { get; set; }
        public TicketViewModel() {

            TicketModel = new TicketModel();

        }
    }
}
