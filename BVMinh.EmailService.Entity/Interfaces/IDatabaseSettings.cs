using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.Entity.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
