﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptWalks.Core.Models.Domain
{
    public class Walk : BaseModel<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? ImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        //Navigation Properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
