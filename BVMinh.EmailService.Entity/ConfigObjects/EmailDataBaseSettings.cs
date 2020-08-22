using BVMinh.EmailService.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.ConfigObjects
{
    public class EmailDataBaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
