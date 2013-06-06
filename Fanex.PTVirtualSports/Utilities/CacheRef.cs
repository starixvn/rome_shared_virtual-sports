namespace Fanex.PTVirtualSports.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using Fanex.Caching;
    using RemotingRef;    

    /// <summary>
    /// CacheRef class
    /// </summary>
    public static class CacheRef
    {
        /// <summary>
        /// refDataType Type
        /// </summary>
        private static Type refDataType = typeof(RefData);

        /// <summary>
        /// methodInfos Dictionary
        /// </summary>
        private static Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// velocityCache ICache
        /// </summary>
        private static ICache velocityCache = CacheService.CreateInstance(CacheType.MemoryCache);

        /// <summary>
        /// Gets: CurrentTWLanguage property
        /// </summary>
        public static string CurrentTWLanguage
        {
            get
            {
                switch (Thread.CurrentThread.CurrentCulture.Name)
                {
                    case "en-US": return "en";
                    case "zh-TW": return "ch"; // Traditional
                    case "zh-CN": return "cs"; // Simplified
                    case "ja-JP": return "jp";
                    case "th-TH": return "th"; // Thai
                    case "vi-VN": return "vn"; // Vietnamese
                    default: return "en";
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsWebService
        /// </summary>
        private static bool IsWebService
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// GetBettype method
        /// </summary>
        /// <param name="bettypeId">bettypeId param</param>
        /// <returns>return string</returns>
        public static string GetBettype(object bettypeId)
        {
            if (IsWebService)
            {
                return Invoke("GetBetTypeName", new object[] { bettypeId.ToString(), CurrentTWLanguage, string.Empty });
            }

            return RefData.GetBetTypeName(bettypeId.ToString(), CurrentTWLanguage, string.Empty);
        }

        /// <summary>
        /// GetSportName method
        /// </summary>
        /// <param name="sportId">sportId param</param>
        /// <returns>return string</returns>
        public static string GetSportName(object sportId)
        {
            if (IsWebService)
            {
                return Invoke("GetSportName", new object[] { sportId.ToString(), CurrentTWLanguage, string.Empty });
            }

            return RefData.GetSportName(sportId.ToString(), CurrentTWLanguage, string.Empty);
        }

        /// <summary>
        /// GetTeamName method
        /// </summary>
        /// <param name="teamId">teamId param</param>
        /// <returns>return string</returns>
        public static string GetTeamName(object teamId)
        {
            if (IsWebService)
            {
                return Invoke("GetTeamName", new object[] { teamId.ToString(), CurrentTWLanguage, string.Empty });
            }

            return RefData.GetTeamName(teamId.ToString(), CurrentTWLanguage, string.Empty);
        }

        /// <summary>
        /// GetCasinoBetItem method
        /// </summary>
        /// <param name="betStatusId">betStatusId param</param>
        /// <returns>return string</returns>
        public static string GetCasinoBetItem(object betStatusId)
        {
            if (IsWebService)
            {
                return Invoke("GetCasinoBetItem", new object[] { betStatusId.ToString(), CurrentTWLanguage, string.Empty });
            }

            return RefData.GetCasinoBetItem(betStatusId.ToString(), CurrentTWLanguage, string.Empty);
        }

        /// <summary>
        /// GetLeagueName method
        /// </summary>
        /// <param name="leagueId">leagueId param</param>
        /// <returns>string string</returns>
        public static string GetLeagueName(object leagueId)
        {
            if (IsWebService)
            {
                return Invoke("GetLeagueName", new object[] { leagueId.ToString(), CurrentTWLanguage, string.Empty });
            }

            return RefData.GetLeagueName(leagueId.ToString(), CurrentTWLanguage, string.Empty);
        }

        /// <summary>
        /// GetFncIndexName method
        /// </summary>
        /// <param name="indexId">indexId param</param>
        /// <returns>string string</returns>
        public static string GetFncIndexName(object indexId)
        {
            if (IsWebService)
            {
                return Invoke("GetFncIndexName", new object[] { indexId.ToString(), CurrentTWLanguage });
            }

            return RefData.GetFncIndexName(indexId.ToString(), CurrentTWLanguage);
        }

        /// <summary>
        /// GetHorseTeamName method
        /// </summary>
        /// <param name="codeId">codeId param</param>
        /// <returns>return string</returns>
        public static string GetHorseTeamName(object codeId)
        {
            if (IsWebService)
            {
                return Invoke("GetHorseTeamName", new object[] { codeId.ToString(), CurrentTWLanguage });
            }

            return RefData.GetHorseTeamName(codeId.ToString(), CurrentTWLanguage);
        }

        /// <summary>
        /// GetOddsTypeName method
        /// </summary>
        /// <param name="oddsTypeId">oddsTypeId param</param>
        /// <returns>return string</returns>
        public static string GetOddsTypeName(object oddsTypeId)
        {
            if (IsWebService)
            {
                return Invoke("GetOddsTypeName", new object[] { oddsTypeId.ToString(), CurrentTWLanguage });
            }

            return RefData.GetOddsTypeName(oddsTypeId.ToString(), CurrentTWLanguage);
        }

        /// <summary>
        /// Invoke method
        /// </summary>
        /// <param name="functionName">functionName param</param>
        /// <param name="parameters">parameters param</param>
        /// <returns>return string</returns>
        private static string Invoke(string functionName, object[] parameters)
        {
            return string.Empty;
        }
    }
}