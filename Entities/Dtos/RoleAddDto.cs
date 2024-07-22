using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class RoleAddDto : IDto
    {
        public string Name { get; set; }

        public List<string> SelectedItems { get; set; }
    }
}
