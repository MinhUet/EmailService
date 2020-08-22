using BVMinh.EmailService.DL.Database;
using BVMinh.EmailService.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BVMinh.EmailService.DL.Repositories
{
	public class SendMailSuccessRepo : BaseRepository<SendMailSuccess>
	{
		public SendMailSuccessRepo(IDatabaseContext databaseContext) : base(databaseContext)
		{

		}
	}
}
