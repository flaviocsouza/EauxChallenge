using Business.Interfaces.Notificatios;
using Business.Models;
using Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services
{
    public abstract class BaseService
    {
        private INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        public void Notificate(ValidationResult validation)
        {
            foreach (var error in validation.Errors)
                Notificate(error.ErrorMessage);
        }

        public void Notificate(string validation)
        {
            _notificator.AddNotification(new Notification(validation));
        }

        public bool ExecuteValidation<TValidator, TEntity>(TValidator validator, TEntity entity) 
            where TValidator : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validation = validator.Validate(entity);

            if (validation.IsValid) return true;
            Notificate(validation);
            return false;
        }

    }
}
