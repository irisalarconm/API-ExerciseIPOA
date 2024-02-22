using DAL.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class TypeService : ITypeService
    {
        private readonly IGenericRepository<Models.Type> _repository;

        public TypeService(IGenericRepository<Models.Type> repository)
        {
            _repository = repository;
        }
        public bool Create(Models.Type model)
        {
            return _repository.Create(model);
        }

        public string Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<Models.Type> GetAll()
        {
            return _repository.GetAll();
        }

        public Models.Type GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Update(Models.Type model)
        {
            return _repository.Update(model);
        }
    }
}
