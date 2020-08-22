using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.DTO
{
    [Serializable]
    public class RecipientDTO
    {
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public Dictionary<String, string> MergeData { get; set; }
        public Dictionary<String, string> CustomArgs { get; set; }
    }
}
