namespace Fanex.PTVirtualSports.BusinessObjects
{
    using System.Collections.Generic;

    public class VSResult
    {
        public IList<Fanex.PTEngine.BusinessObjects.Result> ResultStatus { get; set; }

        public IList<Result> ResultBetSetting { get; set; }

        public IList<Result> ResultPT { get; set; }

        public VSResult()
        {
            ResultStatus = new List<Fanex.PTEngine.BusinessObjects.Result>();
            ResultBetSetting = new List<Result>();
            ResultPT = new List<Result>();
        }
    }
}