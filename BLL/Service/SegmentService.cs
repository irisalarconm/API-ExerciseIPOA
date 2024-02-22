using DAL.Data.Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class SegmentService : ISegmentService
    {
        private readonly IGenericRepository<Segment> _segmentRepository;

        public SegmentService(IGenericRepository<Segment> segmentRepository) 
        {
            _segmentRepository = segmentRepository; 
        }

        public bool Create(Segment model)
        {
            return _segmentRepository.Create(model);
        }

        public string Delete(int id)
        {
            return _segmentRepository.Delete(id);
        }

        public List<Segment> GetAll()
        {
            return _segmentRepository.GetAll();
        }

        public Segment GetById(int id)
        {
            return _segmentRepository.GetById(id);
        }

        public bool Update(Segment model)
        {
            return _segmentRepository.Update(model);
        }
    }
}
