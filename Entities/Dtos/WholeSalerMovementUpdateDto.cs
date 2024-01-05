using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class WholeSalerMovementUpdateDto : IDto
    {
        public int Id { get; set; }
        public int WholeSalerId { get; set; }
        public string Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public byte ProcessType { get; set; }
        public string? Note { get; set; }
        public string? Image { get; set; }
        public bool ImageChanged { get; set; }
        public bool Status { get; set; }
        public int UpdateUserId { get; set; }
    }
}
