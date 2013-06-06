using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.PTVirtualSports.BusinessObjects
{
    [Serializable]
    public class Result : Fanex.PTEngine.BusinessObjects.Result
    {
        public int SportTypeId { get; set; }

        public Result()
        {

        }

        public Result(string errorCode, string errorMessage, int sportTypeId)
            : base(errorCode, errorMessage)
        {
            this.SportTypeId = sportTypeId;
        }
    }
}