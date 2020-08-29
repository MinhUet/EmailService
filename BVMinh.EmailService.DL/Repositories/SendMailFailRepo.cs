using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
    public class SendMailFailRepo : BaseRepository<SendMailFail>
    {
        public SendMailFailRepo(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
