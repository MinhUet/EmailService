using BVMinh.EmailService.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BVMinh.EmailService.Common.Interfaces
{
	public interface IEmailSender<T> where T : class
	{
		Task<bool> SendMailNoMerge(T entity);
		Task<List<RecipientDTO>> SendMailMerge(T entity);
	}
}
