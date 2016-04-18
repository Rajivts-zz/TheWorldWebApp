using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMail(string To, string From, string Subject, string body)
        {
            Debug.WriteLine($"Sending Mail From: {From}, To: {To}, with Subject: {Subject}");
            return true;
        }
    }
}
