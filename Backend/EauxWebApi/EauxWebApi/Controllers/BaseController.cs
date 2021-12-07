using Business.Interfaces.Notificatios;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EauxWebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            return !_notificator.HasNotifications();
        }

        protected ActionResult CustonResponse(object result = null)
        {
            if(ValidOperation()) return Ok( new { sucess = true, data = result });
            
            return BadRequest( new { sucess = false, errors = _notificator.GetNotifications().Select(err => err.Message)});
        }

        protected ActionResult CustonResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificateErrorsModelState(modelState);
            return CustonResponse();
        }

        protected void NotificateErrorsModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(err => err.Errors);
            foreach(var error in errors)
            {
                var errMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificateError(errMessage);
            }
        }

        protected void NotificateError(string errMessage)
        {
            _notificator.AddNotification(new Notification(errMessage));
        }
    }
}
