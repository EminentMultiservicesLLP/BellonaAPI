using System;

namespace BellonaAPI.Models
{
    public class ActionUserDetail
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBY { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}