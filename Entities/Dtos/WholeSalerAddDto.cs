using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class WholeSalerAddDto : IDto
    {
        public string Name { get; set; }
        public string AuthorizedPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }
        public int CreateUserId { get; set; }
    }
}
