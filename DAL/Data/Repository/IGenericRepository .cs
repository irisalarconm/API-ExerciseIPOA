using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Repository
{
    public interface IGenericRepository<TModel> where TModel : class
    {
            List<TModel> GetAll();
            bool Create(TModel model);
            TModel GetById(int id);
            bool Update(TModel model);
            string Delete(int id);
    }
}
