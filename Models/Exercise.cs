using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }

        [ForeignKey("IdType")]
        public int IdType { get; set; }

        [ForeignKey("IdSegment")]
        public int IdSegment { get; set; }

    }
}
