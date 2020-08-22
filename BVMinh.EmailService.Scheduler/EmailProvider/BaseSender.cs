using BVMinh.EmailService.Common.Interfaces;
using BVMinh.EmailService.Entity.DTO;
using BVMinh.EmailService.Entity.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BVMinh.EmailService.Scheduler.EmailProvider
{
	public class BaseSender<T> : IEmailSender<T>, IDisposable where T : BaseEntity
	{
		protected IConfiguration _configuration;
		protected readonly string _userName;
		protected readonly string _passWord;
		public readonly string SecretCode;

		public BaseSender(IConfiguration configuration, string user, string pass)
		{
			_configuration = configuration;
			_userName = user;
			_passWord = pass;
			SecretCode = _configuration.GetSection("IdentifyCode:SecretCode").Value;
		}

		public virtual Task<bool> SendMailNoMerge(T entity)
		{
			throw new NotImplementedException();
		}

		public virtual Task<List<RecipientDTO>> SendMailMerge(T entity)
		{
			throw new NotImplementedException();
		}

		public string MergeData(Dictionary<String, string> data, string input)
		{
			foreach (var mergeData in data)
			{
				string patern = String.Format(@"##{0}##", mergeData.Key);
				Regex regex = new Regex(patern);
				input = regex.Replace(input ?? "", mergeData.Value);
			}
			return input;

		}

		public void Dispose()
		{
			Dispose();
		}
	}
}
