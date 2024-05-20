using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.DTO
{
    public class DepartmentDTO
    {
        [Display(Name = "선택")]
        public bool? IsSelect { get; set; } = false;

        [Display(Name = "부서인덱스")]
        public int? Id { get; set; }

        [Display(Name = "부서명")]
        public string? Name { get; set; }
        
    }
}
