using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeizi.Curso.SignalrHub.Events
{
    public class Notification
    {
        public Notification(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }
        public string UserName { get; }
        public string Message { get; }

    }
}
