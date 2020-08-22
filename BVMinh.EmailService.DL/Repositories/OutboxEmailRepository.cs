using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
    public class OutboxEmailRepository : BaseRepository<OutboxEmail>
    {
        public OutboxEmailRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
