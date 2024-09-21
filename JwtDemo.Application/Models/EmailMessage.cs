using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JwtDemo.Application.Models
{
    public class EmailMessage
    {
         private EmailMessage(IEnumerable<string> to, string subject, string body)
        {
			To = new List<MailAddress>();
			To.AddRange(to.Select(x => new MailAddress(x)));
			Subject = subject;
			Body = body;
		}
        public List<MailAddress> To { get; }
		public string Subject { get; }
		public string Body { get; }
		public static EmailMessage Create(IEnumerable<string> to, string subject, string body)
		{
			return new EmailMessage(to, subject, body);
		}
    }
}