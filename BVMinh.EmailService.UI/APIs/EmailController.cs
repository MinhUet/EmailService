using BVMinh.EmailService.API.EmailLogics;
using BVMinh.EmailService.Common.Kafka;
using BVMinh.EmailService.Common.Redis;
using BVMinh.EmailService.DL.Repositories;
using BVMinh.EmailService.Entity.DTO;
using BVMinh.EmailService.Entity.Entities;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BVMinh.EmailService.API.APIs
{
    public class EmailController : BaseController
    {
        private readonly EmailLogic _emailLogic;
        private readonly ProducerConfig _producerConfig;
        private readonly EmailTopicRepository _emailTopicRepository;
        private readonly ApplicationRepository _applicationRepository;
        private readonly SchedulerTopicRepository _schedulerRepository;
        private readonly EmailTrackingTopicRepository _emailTrackingTopicRepository;
        //public static List<Application> _applications;
        public EmailController(ProducerConfig producerConfg,
                               EmailLogic emailLogic,
                               IRepository<SchedulerTopic> schedulerRepo,
                               IRepository<Application> applicationRepo,
                               IRepository<EmailTrackingTopic> emailTrackingTopicRepo,
                               IRepository<EmailTopic> emailTopicRepo)
        {
            _producerConfig = producerConfg;
            _emailLogic = emailLogic;
            _schedulerRepository = (SchedulerTopicRepository)schedulerRepo;
            _applicationRepository = (ApplicationRepository)applicationRepo;
            _emailTrackingTopicRepository = (EmailTrackingTopicRepository)emailTrackingTopicRepo;
            _emailTopicRepository = (EmailTopicRepository)emailTopicRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutboxEmailDTO emailDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (emailDTO.Recipients.Count > _emailLogic._sumMaxRecipients)
                {
                    return BadRequest(_emailLogic._messageLimitRecipients);
                }

                //Lay appcode trong redis cache
                var _applicationCode = GetApplication.GetApp(emailDTO.ApplicationCode);
                if (_applicationCode == null)
                {
                    return BadRequest(_emailLogic._messageSendMailFail);
                }
                emailDTO.EmailID = Guid.NewGuid().ToString();
                emailDTO.CreatedDate = DateTime.Now;
                emailDTO.CreatedBy = emailDTO.EmailSenderName;
                emailDTO.EmailSenderAddress = emailDTO.EmailSenderAddress ?? _applicationCode.EmailSenderAddress;
                emailDTO.EmailSenderName = emailDTO.EmailSenderName ?? _applicationCode.EmailSenderName;
                emailDTO.CompanyCode = emailDTO.CompanyCode ?? _applicationCode.CompanyCode;
                emailDTO.RetryTime = 0;
                var nametopic = _emailLogic.ChooseTopicName(emailDTO);
                var serializedEmail = JsonConvert.SerializeObject(emailDTO);
                using (var producer = new ProducerWrapper<Null, string>(_producerConfig, nametopic))
                {
                    await producer.SendMessage(serializedEmail);
                }
                return Ok(emailDTO.EmailID);
            }
            catch (Exception ex)
            {
                return BadRequest(_emailLogic._messageSendMailFail + ": " + ex);
            }
        }


        [HttpPost("DeleteEmailScheduler")]
        public IActionResult Post([FromBody] List<string> emailList)
        {
            var deleteEmailScheduler = _schedulerRepository.DeleteManyEmailScheduler(emailList);
            if (deleteEmailScheduler == "DeleteSuccess")
            {
                return Ok("Delete EmailScheduler Success");
            }
            else
            {
                return BadRequest(deleteEmailScheduler);
            }
        }


        [HttpPost("NewApplication")]
        public async Task<IActionResult> Post([FromBody] Application application)
        {
            application.ApplicationName = application.ApplicationName ?? "BuiVanMinh";
            application.ApplicationCode = application.ApplicationCode ?? "BVMinh";
            application.CompanyCode = application.CompanyCode ?? "BVM";
            application.WebHookUrl = application.WebHookUrl ?? "https://webhook.site/#!/97fd7214-331f-4128-a461-64c919a56353";
            application.EmailServerAddress = application.EmailServerAddress ?? "smtp.sendgrid.com";
            application.EmailServerPort = application.EmailServerPort ?? "587";
            application.EmailUserName = application.EmailUserName ?? "apikey";
            application.EmailUserPassword = application.EmailUserPassword ?? "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";
            application.EmailServiceProvider = application.EmailServiceProvider ?? "SendGrid";
            application.EmailSenderName = application.EmailSenderName ?? "Bui Van Minh";
            application.EmailSenderAddress = application.EmailSenderAddress ?? "buivanminh1309@gmail.com";
            application.EmailReplyToAddress = application.EmailReplyToAddress ?? "buivanminh1309@gmail.com";
            application.CreatedDate = DateTime.Now;
            application.ModifiedDate = DateTime.Now;
            application.ModifiedBy = application.ModifiedBy ?? "bvminh";
            application.CreatedBy = application.CreatedBy ?? "bvminh";
            var applicationID = await _applicationRepository.Insert(application);

            EmailTopic emailTopic = new EmailTopic();
            emailTopic.EmailTopicName = application.ApplicationCode;
            emailTopic.CreatedDate = DateTime.Now;
            emailTopic.ModifiedDate = DateTime.Now;
            emailTopic.ModifiedBy = application.ModifiedBy ?? "bvminh";
            emailTopic.CreatedBy = application.CreatedBy ?? "bvminh";
            var emailTopicID = await _emailTopicRepository.Insert(emailTopic);

            EmailTrackingTopic emailTrackingTopic = new EmailTrackingTopic();
            emailTrackingTopic.EventTopicName = application.ApplicationCode + "_Tracking";
            emailTrackingTopic.CreatedDate = DateTime.Now;
            emailTrackingTopic.ModifiedDate = DateTime.Now;
            emailTrackingTopic.ModifiedBy = application.ModifiedBy ?? "bvminh";
            emailTrackingTopic.CreatedBy = application.CreatedBy ?? "bvminh";
            var emailTrackingTopicID = await _emailTrackingTopicRepository.Insert(emailTrackingTopic);

            return Ok(applicationID + " " + emailTopicID + " " + emailTrackingTopicID);
        }
    }
}
