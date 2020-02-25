using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PizzeriaWebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ILogger Logger { get; set; }
        protected void LogException(Exception ex)
        {
            Logger.LogError(ex.Message);
            while (null != ex.InnerException)
            {
                Logger.LogError(ex.InnerException.Message);
                ex = ex.InnerException;
            }
        }
    }
}
