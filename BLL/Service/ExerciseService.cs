using DAL.Data.Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class ExerciseService : IExerciseService
    {
        private readonly IGenericRepository<Exercise> _exerciseRepository;
        public ExerciseService(IGenericRepository<Exercise> exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }
        public bool Create(Exercise model)
        {
            return _exerciseRepository.Create(model);
        }

        public string Delete(int id)
        {
            return _exerciseRepository.Delete(id);
        }

        public List<Exercise> GetAll()
        {
            return _exerciseRepository.GetAll();
        }

        public Exercise GetById(int id)
        {
           return _exerciseRepository.GetById(id);
        }

        public bool Update(Exercise model)
        {
            return _exerciseRepository.Update(model);
        }
    }
}
