using System.Threading.Tasks;
using CleanArchitecture.Application.Mails.models;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}