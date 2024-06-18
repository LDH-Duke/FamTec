using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamTec.Shared.Client.DTO.Normal.Voc
{
    public class ListVoc
    {
        public string? Location { get; set; }
        public int Type { get; set; }
        public string? Writer { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public DateTime? Occur_DT { get; set; }
        public DateTime? Compelete_DT { get; set; }
    }
}
