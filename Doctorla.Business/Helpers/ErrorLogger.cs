using Doctorla.Core;
using Doctorla.Data;
using Doctorla.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Helpers
{
    public static class ErrorLogger
    {
        public static async Task Log(string input, string exceptionMessage, string exceptionStackTrace, string innerExceptionMessage, string innerExceptionStackTrace, string methodName)
        {
            await using var context = new Context(ServiceLocator.AppSettings.DatabaseConnectionString);
            var log = new ErrorLog
            {
                CreatedAt = context.Now,
                Input = input,
                ExceptionMessage = exceptionMessage,
                ExceptionStackTrace = exceptionStackTrace,
                InnerExceptionMessage = innerExceptionMessage,
                InnerExceptionStackTrace = innerExceptionStackTrace,
                MethodName = methodName
            };
            
            context.ErrorLogs.Add(log);
            await context.SaveChangesAsync();
        }
    }
}
