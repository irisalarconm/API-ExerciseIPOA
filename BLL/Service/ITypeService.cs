using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface ITypeService
    {
        List<Models.Type> GetAll();
        bool Create(Models.Type model);
        Models.Type GetById(int id);
        bool Update(Models.Type model);
        string Delete(int id);
    }
}
