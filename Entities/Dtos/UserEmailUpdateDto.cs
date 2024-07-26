using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class UserEmailUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
