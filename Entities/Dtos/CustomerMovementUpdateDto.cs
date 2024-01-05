﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities.Dtos
{
    public class CustomerMovementUpdateDto : IDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Amount { get; set; }
        public byte ProcessType { get; set; }
        public string? ProductInformation { get; set; }
        public string? Note { get; set; }
        public bool Status { get; set; }
        public int UpdateUserId { get; set; }
    }
}
