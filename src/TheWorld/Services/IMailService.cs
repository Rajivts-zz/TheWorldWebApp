using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public interface IMailService
    {
        bool SendMail(string To, string From, string Subject, string body);
    }
}
