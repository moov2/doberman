using System;

namespace Doberman.Model
{
    public class SmtpSettings
    {
        public virtual string Host { get; set; }
        public virtual int Port { get; set; }
        public virtual bool Ssl { get; set; }

        public SmtpSettings()
        {
            Ssl = false;
        }
    }
}
