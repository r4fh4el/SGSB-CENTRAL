using Microsoft.JSInterop;
using SGSB.DAL.Context;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Linq;
using System;
namespace SGSB.Web.Data
{
    public class EmpresaService
    {
   
        private DatabaseContext db = new DatabaseContext();
        private readonly IJSRuntime js;

        public List<EMPRESA> GetEmpresa()
        {
            db = new DatabaseContext();
            return db.EMPRESA.ToList();

        }
        public EMPRESA GetEmpresaId(int id)
        {
            db = new DatabaseContext();
            return db.EMPRESA.Where(i => i.ID == id).FirstOrDefault();

        }
        public EMPRESA EditEmpresa(int id)
        {
            db = new DatabaseContext();
            return db.EMPRESA.Where(i => i.ID == id).FirstOrDefault();

        }
        public EMPRESA VerEmpresa(int id)
        {
            db = new DatabaseContext();
            return db.EMPRESA.Where(i => i.ID == id).FirstOrDefault();

        }
        public EMPRESA Delete(int id)
        {
            db = new DatabaseContext();
            return db.EMPRESA.Where(i => i.ID == id).FirstOrDefault();

        }
        public void DeleteEmpresa(EMPRESA model)
        {
            db = new DatabaseContext();

            db.EMPRESA.Remove(model);
            db.SaveChanges();
            db.Dispose();

        }
        public void EditarEmpresa(EMPRESA model)
        {
            try
            {

                //var result = await new EMPRESAUserStore.c(model);
                var result = db.EMPRESA.Update(model);

                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public void CadastrarEmpresa(EMPRESA model)
        {
            try
            {

                //var result = await new EMPRESAUserStore.c(model);
                var result = db.EMPRESA.Add(model);

                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
