using System;
using Doberman.Model;
using Doberman.Services;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Collections.Generic;

namespace Doberman.Checks
{
    public class SendingEmailCheck : ICheck
    {
        private const string CheckName = "Sending Emails";

        public SmtpSettings SmtpSettings { get; private set; }

        private string _firstResponse;
        private string _secondResponse;

        public SendingEmailCheck(SmtpSettings smtpSettings)
        {
            SmtpSettings = smtpSettings;
        }

        public DobermanResult Execute()
        {
            var result = new DobermanResult { Check = CheckName };

            var connected = CheckConnectionToHost();

            if (connected == true && _firstResponse.StartsWith("220") && _secondResponse.StartsWith("250"))
            {
                result.Success = true;
                result.Detail = "Connected to SMTP server that is ready to send emails successfully.";
            }
            else if (connected == false)
            {
                result.Success = false;
                result.Detail = "Unable to connect to the SMTP server (" + SmtpSettings.Host + ":" + SmtpSettings.Port + ")";
            }
            else
            {
                result.Success = false;
                result.Detail = _firstResponse + " & " + _secondResponse;
            }


            return result;
        }

        /// <summary>
        /// Attempts to establish a connection with the Smtp server and then records
        /// the first two responses from the server.
        /// </summary>
        private bool CheckConnectionToHost()
        {
            using (var client = new TcpClient())
            {
                try
                {
                    client.Connect(SmtpSettings.Host, SmtpSettings.Port);
                } catch {
                    return false; 
                }

                using (var stream = client.GetStream())
                {
                    if (SmtpSettings.Ssl)
                    {
                        using (var sslStream = new SslStream(stream))
                        {
                            sslStream.AuthenticateAsClient(SmtpSettings.Host);
                            SendServerMessageAndSaveResponse(sslStream);
                        }
                    }
                    else
                        SendServerMessageAndSaveResponse(stream);
                }
            }

            return true;
        }

        private void SendServerMessageAndSaveResponse(Stream stream)
        {
            using (var writer = new StreamWriter(stream))
            using (var reader = new StreamReader(stream))
            {
                writer.WriteLine("EHLO " + SmtpSettings.Host);
                writer.Flush();

                _firstResponse = reader.ReadLine();
                _secondResponse = reader.ReadLine();
            }
        }
    }
}
