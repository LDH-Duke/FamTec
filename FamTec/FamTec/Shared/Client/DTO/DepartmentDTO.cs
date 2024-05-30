using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Client.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public bool IsSelect { get; set; } = false;
        public string? Name { get; set; }
        public string? Description { get; set; } = null;
    }
}
