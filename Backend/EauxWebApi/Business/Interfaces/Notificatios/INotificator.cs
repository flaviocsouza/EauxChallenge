using Business.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Notificatios
{
    public interface INotificator
    {
        public bool HasNotifications();
        public IEnumerable<Notification> GetNotifications();
        public void AddNotification(Notification notification);
    }
}
