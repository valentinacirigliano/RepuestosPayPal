﻿
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Repuestos2023.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
