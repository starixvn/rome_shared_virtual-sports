using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.PTVirtualSports.Utilities
{
    public static class CommonExtension
    {
        public static Fanex.PTVirtualSports.BusinessObjects.Result ToNewResult(this Fanex.PTEngine.BusinessObjects.Result r, int sportTypeId)
        {
            return new Fanex.PTVirtualSports.BusinessObjects.Result(r.ErrorCode, r.ErrorMessage, sportTypeId);
        }
    }
}