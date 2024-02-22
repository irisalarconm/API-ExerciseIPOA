using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IExerciseService
    {
        List<Exercise> GetAll();
        bool Create(Exercise model);
        Exercise GetById(int id);
        bool Update(Exercise model);
        string Delete(int id);
    }
}
