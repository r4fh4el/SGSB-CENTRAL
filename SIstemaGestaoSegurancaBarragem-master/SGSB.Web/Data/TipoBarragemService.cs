using Microsoft.JSInterop;
using SGSB.DAL.Context;
using System.Net.Http.Json;
using System.Linq;
using System;
namespace SGSB.Web.Data
{
    public class TipoBarragemService
    {
   
        private DatabaseContext db = new DatabaseContext();
        private readonly IJSRuntime js;

        public List<TIPO_BARRAGEM> GetTipoBarragem()
        {
            db = new DatabaseContext();
            return db.TIPO_BARRAGEM.ToList();

        }
        public TIPO_BARRAGEM GetTipoBarragemId(int id)
        {
            db = new DatabaseContext();
            return db.TIPO_BARRAGEM.Where(i => i.id == id).FirstOrDefault();

        }
        public TIPO_BARRAGEM EditTipoBarragem(int id)
        {
            db = new DatabaseContext();
            return db.TIPO_BARRAGEM.Where(i => i.id == id).FirstOrDefault();

        }
        public TIPO_BARRAGEM VerTipoBarragem(int id)
        {
            db = new DatabaseContext();
            return db.TIPO_BARRAGEM.Where(i => i.id == id).FirstOrDefault();

        }
        public TIPO_BARRAGEM Delete(int id)
        {
            db = new DatabaseContext();
            return db.TIPO_BARRAGEM.Where(i => i.id == id).FirstOrDefault();

        }
        public void DeleteTipoBarragem(TIPO_BARRAGEM model)
        {
            db = new DatabaseContext();

            db.TIPO_BARRAGEM.Remove(model);
            db.SaveChanges();
            db.Dispose();

        }
        public void EditarTipoBarragem(TIPO_BARRAGEM model)
        {
            try
            {

                //var result = await new TIPO_BARRAGEMUserStore.c(model);
                var result = db.TIPO_BARRAGEM.Update(model);

                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public void CadastrarTipoBarragem(TIPO_BARRAGEM model)
        {
            try
            {

                //var result = await new TIPO_BARRAGEMUserStore.c(model);
                var result = db.TIPO_BARRAGEM.Add(model);

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
