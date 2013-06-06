namespace Fanex.PTVirtualSports.Services
{
    using System.Collections.Generic;
    using Fanex.PTVirtualSports.BusinessObjects;

    /// <summary>
    /// BaseCustomerService class
    /// </summary>
    public class BaseCustomerService
    {
        /// <summary>
        /// AnalyseResult method
        /// </summary>
        /// <param name="results">results param</param>
        /// <returns>return IList</returns>
        protected IList<Result> AnalyseResult(IList<Result> results)
        {
            IList<Result> newResults = new List<Result>();
            foreach (Result result in results)
            {
                string[] errCodes = result.ErrorCode.Split('\a');
                string[] errMsgs = result.ErrorMessage.Split('\a');

                for (int i = 0; i < errCodes.Length; i++)
                {
                    if (!string.IsNullOrEmpty(errCodes[i]))
                    {
                        Result rs = new Result
                        {
                            ErrorCode = errCodes[i],
                            ErrorMessage = errMsgs[i],
                            SportTypeId = result.SportTypeId
                        };

                        newResults.Add(rs);
                    }
                }
            }

            return newResults;
        }

        protected IList<Fanex.PTEngine.BusinessObjects.Result> AnalyseResult(IList<Fanex.PTEngine.BusinessObjects.Result> results)
        {
            var newResults = new List<Fanex.PTEngine.BusinessObjects.Result>();
            foreach (var result in results)
            {
                string[] errCodes = result.ErrorCode.Split('\a');
                string[] errMsgs = result.ErrorMessage.Split('\a');

                for (int i = 0; i < errCodes.Length; i++)
                {
                    if (!string.IsNullOrEmpty(errCodes[i]))
                    {
                        Fanex.PTEngine.BusinessObjects.Result rs = new Fanex.PTEngine.BusinessObjects.Result
                        {
                            ErrorCode = errCodes[i],
                            ErrorMessage = errMsgs[i],
                        };

                        newResults.Add(rs);
                    }
                }
            }

            return newResults;
        }

        /// <summary>
        /// AnalyseResult method: -101 Updated Position Taking Successfully. -102 Updated Min Position Successfully. -103 UnUsed. -104 Updated Min Bet/Max Bet Successfully. -105 Updated Max Per Match Successfully.
        /// </summary>
        /// <param name="results">Results param</param>
        protected void AnalyseResult(Fanex.PTVirtualSports.BusinessObjects.VSResult results)
        {
            results.ResultStatus = this.AnalyseResult(results.ResultStatus);
            results.ResultBetSetting = this.AnalyseResult(results.ResultBetSetting);
            results.ResultPT = this.AnalyseResult(results.ResultPT);
        }
    }
}