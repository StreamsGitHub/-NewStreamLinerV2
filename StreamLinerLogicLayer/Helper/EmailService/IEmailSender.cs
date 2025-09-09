using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Helper.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string htmlMessage);
    }
}
