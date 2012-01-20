﻿using System;
using Doberman.Model;
using Doberman.Services;

namespace Doberman.Checks
{
    public class SendingEmailCheck : ICheck
    {
        private const string CheckName = "Sending Emails";
        private const string From = "donotreply@moov2.com";
        private const string To = "peter@moov2.com";
        private const string Subject = "Doberman Email Check";
        private const string Message = "Please ignore, this is a test email from Doberman.";

        public IEmailProvider EmailProvider { get; private set; }

        public SendingEmailCheck(IEmailProvider emailProvider)
        {
            EmailProvider = emailProvider;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            try
            {
                EmailProvider.Send(From, To, Subject, Message);

                result.Success = true;
                result.Detail = "Sent e-mail to " + To + " successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Detail = ex.Message;
            }

            return result;
        }
    }
}
