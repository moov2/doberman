using System;

namespace Doberman.Services
{
    public interface IEmailProvider
    {
        void Send(string from, string to, string subject, string body);
    }
}
