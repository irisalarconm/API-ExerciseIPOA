using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
   public interface ISegmentService
    {
        List<Segment> GetAll();
        bool Create(Segment model);
        Segment GetById(int id);
        bool Update(Segment model);
        string Delete(int id);
    }
}
