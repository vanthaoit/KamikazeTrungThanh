using KamikazeTrungThanh.Common;
using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Service;
using KamikazeTrungThanh.Web.Infrastructure.Extensions;
using KamikazeTrungThanh.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KamikazeTrungThanh.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactService, IFeedbackService feedbackService)
        {
            _contactDetailService = contactService;
            _feedbackService = feedbackService;
        }
        private ContactDetailViewModel GetDetail()
        {
            var modelData = _contactDetailService.GetDefaultContact();
            var viewModelResponseData = AutoMapper.Mapper.Map<ContactDetail, ContactDetailViewModel>(modelData);
            return viewModelResponseData;
        }
        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModelFeedbackResponseData = new FeedbackViewModel();
            viewModelFeedbackResponseData.ContactDetail = GetDetail();
            return View(viewModelFeedbackResponseData);
        }

        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(newFeedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";
                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Nội dung phản hồi", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Email = "";
                feedbackViewModel.Message = "";

            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index",feedbackViewModel);
        }
    }
}