using System;

namespace Doctorla.Data
{
    /// <summary>
    /// When an error occurs, these information are saved into database so that we can trace it later
    /// </summary>
    public class ErrorLog : Entity
    {
        public string Input { get; set; }
        public string MethodName { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string InnerExceptionStackTrace { get; set; }
    }
}
