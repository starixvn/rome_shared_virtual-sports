namespace Fanex.PTVirtualSports.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Web;
    using Fanex.PTEngine.BusinessObjects;
    using Fanex.PTVirtualSports.BusinessObjects;
    using Fanex.PTVirtualSports.DataAccess;
    using Fanex.PTVirtualSports.Extension;
    using Fanex.PTVirtualSports.Utilities;

    /// <summary>
    /// CustomerService class
    /// </summary>
    public class CustomerService : BaseCustomerService, ICustomerService
    {
        private readonly byte _systemId;

        public CustomerService(byte systemId = 1)
        {
            _systemId = systemId;
        }

        /// <summary>
        /// DefaultCustomer method
        /// </summary>
        /// <returns>return ICustomer</returns>
        public ICustomer DefaultCustomer()
        {
            // Default value for Super
            throw new NotImplementedException();
        }

        #region ICustomerService Members

        /// <summary>
        /// SaveCustomer method
        /// </summary>
        /// <param name="oldCustomer">oldCustomer param</param>
        /// <param name="newCustomer">newCustomer param</param>
        /// <param name="context">context param</param>
        /// <param name="parameters">parameters param</param>
        /// <returns>return VSResult</returns>
        public VSResult SaveCustomer(ICustomer oldCustomer, ICustomer newCustomer, IRequestContext context, Hashtable parameters)
        {
            // For update case, compare old customer and new customer, remove betting limit item and sport types which is not changed
            // Mark is auto pt changed, is min pt changed
            VSResult resultDto = new VSResult();
            var results = new List<Fanex.PTVirtualSports.BusinessObjects.Result>();
            switch (context.TargetRoleId)
            {
                case Constant.SUPER_ROLEID:
                    resultDto = this.UpdateSuperSettings(oldCustomer, newCustomer, context, parameters);
                    break;
                case Constant.MASTER_ROLEID:
                    resultDto = this.UpdateMasterSettings(oldCustomer, newCustomer, context, parameters);
                    break;
                case Constant.AGENT_ROLEID:
                    resultDto = this.UpdateAgentSettings(oldCustomer, newCustomer, context, parameters);
                    break;
                case Constant.MEMBER_ROLEID:
                    if (context.RoleId == Constant.SUPER_ROLEID)
                    {
                        // super edit on member
                        resultDto = this.SuperUpdateMemberSettings(oldCustomer, newCustomer, context);
                    }
                    else if (context.RoleId == Constant.MASTER_ROLEID)
                    {
                        // master edit on member
                        resultDto = MasterUpdateMemberSettings(oldCustomer, newCustomer, context);
                    }
                    else
                    {
                        // agent edit on member
                        resultDto = UpdateMemberSettings(oldCustomer, newCustomer, context);
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }

            this.AnalyseResult(resultDto);
            return resultDto;
        }

        /// <summary>
        /// GetCustomer method
        /// </summary>
        /// <param name="context">context param</param>
        /// <returns>return ICustomer</returns>
        public ICustomer GetCustomer(IRequestContext context)
        {
            var customerDataAccess = new CustomerDataAccess();
            DataSet dataSet = null;
            switch (context.TargetRoleId)
            {
                case Constant.SUPER_ROLEID:
                    dataSet = customerDataAccess.Acc_CustomerSelEditSuperVS(context);
                    break;
                case Constant.MASTER_ROLEID:
                    dataSet = customerDataAccess.Acc_CustomerSelEditMasterVS(context);
                    break;
                case Constant.AGENT_ROLEID:
                    dataSet = customerDataAccess.Acc_CustomerSelEditAgentVS(context);
                    break;
                case Constant.MEMBER_ROLEID:
                    switch (context.RoleId)
                    {
                        case Constant.SUPER_ROLEID:
                            dataSet = customerDataAccess.Acc_CustomerSelSEditCustomerVS(context);
                            break;
                        case Constant.MASTER_ROLEID:
                            dataSet = customerDataAccess.Acc_CustomerSelMEditCustomerVS(context);
                            break;
                        case Constant.AGENT_ROLEID:
                            dataSet = customerDataAccess.Acc_CustomerSelEditMemberVS(context);
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }

            var customerDataTable = dataSet.Tables[0];
            var betSettingDataTable = dataSet.Tables[1];
            return this.CreateCustomer(context, customerDataTable, betSettingDataTable);
        }

        #endregion ICustomerService Members

        /// <summary>
        /// CreateCustomer method
        /// </summary>
        /// <param name="context">context param</param>
        /// <param name="information">information param</param>
        /// <param name="setting">setting param</param>
        /// <returns>return ICustomer</returns>
        private ICustomer CreateCustomer(IRequestContext context, DataTable information, DataTable setting)
        {
            // General information
            var customer = this.GetCustomer(context, information.Rows[0]);

            // commission
            customer.CommissionSetting = null;
            customer.CommissionSetting2 = null;

            // Status Setting
            customer.StatusSetting = this.GetStatusSetting(context, information.Rows[0]);

            // Bet Setting
            BettingLimit bettingLimit = new BettingLimit();
            PTGrid objPtGrid = new PTGrid();
            foreach (DataRow dr in setting.Rows)
            {
                var bettingLimitItem = this.GetBettingLimitItem(context, dr);
                bettingLimit.BettingLimitItems.Add(bettingLimitItem);
                var sportType = this.GetSportType(context, dr);
                objPtGrid.SportTypes.Add(sportType);
            }

            customer.BettingLimit = bettingLimit;
            customer.PtSetting = objPtGrid;

            return customer;
        }

        /// <summary>
        /// GetCustomer method
        /// </summary>
        /// <param name="context">context param</param>
        /// <param name="informationItem">informationItem param</param>
        /// <returns>return ICustomer</returns>
        private ICustomer GetCustomer(IRequestContext context, DataRow informationItem)
        {
            var customer = new Fanex.PTEngine.BusinessObjects.Customer();

            customer.UserName = informationItem.ToString("username");
            customer.FirstName = informationItem.ToString("firstname");
            customer.LastName = informationItem.ToString("lastname");
            customer.Phone = informationItem.ToString("phone");
            customer.MobilePhone = informationItem.ToString("mobilephone");
            customer.Email = informationItem.ToString("email");
            customer.Fax = informationItem.ToString("fax");
            customer.CurrencyId = informationItem.ToInt32("currency");

            return customer;
        }

        /// <summary>
        /// GetStatusSetting method
        /// </summary>
        /// <param name="context">context param</param>
        /// <param name="informationItem">informationItem param</param>
        /// <returns>return IStatusSetting</returns>
        private IStatusSetting GetStatusSetting(IRequestContext context, DataRow informationItem)
        {
            StatusSetting statusSetting = new StatusSetting();
            statusSetting.DisableVirtualSports = informationItem.ToBoolean("disablevirtualsports");
            statusSetting.HideVirtualSports = informationItem.ToBoolean("hidevirtualsports");

            switch (context.TargetRoleId)
            {
                case Constant.SUPER_ROLEID:
                    statusSetting.DisableDownlinesVirtualSports = informationItem.ToBoolean("disabledownlinesvirtualsports");
                    break;
                case Constant.MASTER_ROLEID:
                    statusSetting.DisableDownlinesVirtualSports = informationItem.ToBoolean("disabledownlinesvirtualsports");
                    statusSetting.DisableUplineVirtualSports = informationItem.ToBoolean("disableuplinevirtualsports");
                    statusSetting.DisableAutoMPT = informationItem.ToBoolean("disableautompt");
                    break;
                case Constant.AGENT_ROLEID:
                    statusSetting.DisableDownlinesVirtualSports = informationItem.ToBoolean("disabledownlinesvirtualsports");
                    statusSetting.DisableUplineVirtualSports = informationItem.ToBoolean("disableuplinevirtualsports");
                    statusSetting.DisableAutoMPT = informationItem.ToBoolean("disableautompt");
                    break;
                case Constant.MEMBER_ROLEID:
                    if (context.RoleId == Constant.SUPER_ROLEID)
                    {
                    }
                    else if (context.RoleId == Constant.MASTER_ROLEID)
                    {
                        statusSetting.DisableAutoMPT = informationItem.ToBoolean("disableautompt");
                    }
                    else if (context.RoleId == Constant.AGENT_ROLEID)
                    {
                        statusSetting.DisableUplineVirtualSports = informationItem.ToBoolean("disableuplinevirtualsports");
                    }

                    break;
            }

            return statusSetting;
        }

        /// <summary>
        /// method GetBettingLimitItem
        /// </summary>
        /// <param name="context">context param</param>
        /// <param name="settingItem">settingItem param</param>
        /// <returns>return BettingLimitItem</returns>
        private BettingLimitItem GetBettingLimitItem(IRequestContext context, DataRow settingItem)
        {
            BettingLimitItem bettingLimitItem = new BettingLimitItem();

            bettingLimitItem.Id = settingItem.ToInt32("sporttype");
            bettingLimitItem.Name = this.GetSportNameById(bettingLimitItem.Id);
            bettingLimitItem.BetTypeId = settingItem.ToInt32("bettype");
            bettingLimitItem.MinBet = settingItem.ToNullableDecimal("minbet");
            bettingLimitItem.MinBetMax = settingItem.ToNullableDecimal("minbet_min");
            bettingLimitItem.MaxBet = settingItem.ToNullableDecimal("maxbet");
            bettingLimitItem.MaxBetMin = settingItem.ToNullableDecimal("maxbet_max");
            bettingLimitItem.MaxPerMatch = settingItem.ToNullableDecimal("maxpermatch");
            bettingLimitItem.MaxPerMatchMin = settingItem.ToNullableDecimal("maxpermatch_min");
            bettingLimitItem.MaxPerMatchMax = settingItem.ToNullableDecimal("maxpermatch_max");

            return bettingLimitItem;
        }

        /// <summary>
        /// GetSportNameById method
        /// </summary>
        /// <param name="sportType">sportType param</param>
        /// <returns>return string</returns>
        private string GetSportNameById(int sportType)
        {
            return CacheRef.GetSportName(sportType);
        }

        /// <summary>
        /// GetBetTypeNameById method
        /// </summary>
        /// <param name="betType">betType param</param>
        /// <returns>return string</returns>
        private string GetBetTypeNameById(int betType)
        {
            if (betType == 1)
            {
                return "Soccer";
            }
            else
            {
                return "Other";
            }

            // return CacheRef.GetBettype(betType);
        }

        /// <summary>
        /// GetSportType method
        /// </summary>
        /// <param name="context">context param</param>
        /// <param name="settingItem">settingItem param</param>
        /// <returns>return SportType</returns>
        private SportType GetSportType(IRequestContext context, DataRow settingItem)
        {
            SportType sportType = new SportType();
            sportType.Id = settingItem.ToInt32("sporttype");
            sportType.Name = this.GetSportNameById(sportType.Id);

            BetTypeGroupSet groupSet = new BetTypeGroupSet();
            sportType.BetTypeGroupSets.Add(groupSet);

            BetTypeGroup group = new BetTypeGroup();
            groupSet.BetTypeGroups.Add(group);

            BetType betType = this.GetBetType(context, settingItem);
            group.BetTypes.Add(betType);

            return sportType;
        }

        /// <summary>
        /// GetBetType method
        /// </summary>
        /// <param name="context">context param</param>
        /// <param name="settingItem">settingItem param</param>
        /// <returns>return BetType</returns>
        private BetType GetBetType(IRequestContext context, DataRow settingItem)
        {
            BetType betType = new BetType();

            betType.Id = settingItem.ToInt32("bettype");
            int sportType = settingItem.ToInt32("sporttype");
            betType.Name = betType.Title = this.GetSportNameById(sportType);

            PT pt = new PT();
            PropertyInfo[] propertyInfos = typeof(PT).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType == typeof(bool?))
                {
                    object value = settingItem.ToNullableBool(propertyInfo.Name);
                    propertyInfo.SetValue(pt, value, null);
                }
                else
                {
                    object value = settingItem.ToNullableDecimal(propertyInfo.Name);
                    propertyInfo.SetValue(pt, value, null);
                }
            }

            switch (context.TargetRoleId)
            {
                case Constant.SUPER_ROLEID:
                    break;
                case Constant.MASTER_ROLEID:
                    break;
                case Constant.AGENT_ROLEID:
                    break;
                case Constant.MEMBER_ROLEID:
                    switch (context.RoleId)
                    {
                        case Constant.SUPER_ROLEID:
                            pt.N4 = settingItem.ToNullableDecimal("s1_min");
                            pt.S4 = settingItem.ToNullableDecimal("s1_max");

                            pt.L4 = settingItem.ToNullableDecimal("us1_max");
                            break;
                        case Constant.MASTER_ROLEID:
                            pt.N3 = settingItem.ToNullableDecimal("m1_min");
                            pt.M3 = settingItem.ToNullableDecimal("m1_max");

                            pt.L3 = settingItem.ToNullableDecimal("um1_max");
                            break;
                        case Constant.AGENT_ROLEID:
                            pt.A2 = settingItem.ToNullableDecimal("a1_max");
                            pt.N2 = settingItem.ToNullableDecimal("a1_min");
                            break;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }

            betType.Pt = pt;

            return betType;
        }

        #region Mapping object to Sp parameters
        /// <summary>
        /// MapCustomerInfoToSpParameters method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <param name="context">context param</param>
        /// <param name="customer">customer param</param>
        private void MapCustomerInfoToSpParameters(Hashtable parameters, IRequestContext context, ICustomer customer)
        {
            parameters.Add("userid", context.CustId);
            parameters.Add("custid", customer.CustId);
            parameters.Add("username", customer.UserName == null ? string.Empty : customer.UserName);
            parameters.Add("address", customer.Address == null ? string.Empty : HttpUtility.HtmlDecode(customer.Address));
            parameters.Add("postal", customer.Postal == null ? string.Empty : HttpUtility.HtmlDecode(customer.Postal));
            parameters.Add("city", customer.City == null ? string.Empty : HttpUtility.HtmlDecode(customer.City));
            parameters.Add("state", customer.State == null ? string.Empty : HttpUtility.HtmlDecode(customer.State));
            parameters.Add("country", customer.Country == null ? string.Empty : HttpUtility.HtmlDecode(customer.Country));
            parameters.Add("lastname", customer.LastName == null ? string.Empty : HttpUtility.HtmlDecode(customer.LastName));
            parameters.Add("firstname", customer.FirstName == null ? string.Empty : HttpUtility.HtmlDecode(customer.FirstName));
            parameters.Add("email", customer.Email == null ? string.Empty : HttpUtility.HtmlDecode(customer.Email));
            parameters.Add("phone", customer.Phone == null ? string.Empty : HttpUtility.HtmlDecode(customer.Phone));
            parameters.Add("mobilephone", customer.MobilePhone == null ? string.Empty : HttpUtility.HtmlDecode(customer.MobilePhone));
            parameters.Add("fax", customer.Fax == null ? string.Empty : HttpUtility.HtmlDecode(customer.Fax));

            parameters.Add("srecommend", customer.SRecommend);
            parameters.Add("mrecommend", context.CustId);
            parameters.Add("recommend", context.CustId);
            parameters.Add("language", customer.Language == null ? string.Empty : customer.Language);
            parameters.Add("ip", context.IP);
            parameters.Add("site", customer.SiteName == null ? string.Empty : customer.SiteName);
            parameters.Add("sitename", customer.SiteName == null ? string.Empty : customer.SiteName); // For parameter of Add New Agent
            parameters.Add("currency", customer.CurrencyId > 0 ? customer.CurrencyId.ToString() : string.Empty);
            parameters.Add("isinternal", customer.IsInternal);
            parameters.Add("ugroup", customer.Group);
            parameters.Add("creator", context.AdminId);
        }

        /// <summary>
        /// MapBettingLimitToSpParameters method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <param name="bettingLimitItem">bettingLimitItem param</param>
        private void MapBettingLimitToSpParameters(Hashtable parameters, BettingLimitItem bettingLimitItem)
        {
            parameters.Add("sporttype", bettingLimitItem.Id);
            parameters.Add("bettype", bettingLimitItem.BetTypeId);

            parameters.Add("minbet", bettingLimitItem.MinBet);
            parameters.Add("maxbet", bettingLimitItem.MaxBet);
            parameters.Add("maxpermatch", bettingLimitItem.MaxPerMatch);
        }

        /// <summary>
        /// MapStatusSettingToSpParameters method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <param name="customer">customer param</param>
        private void MapStatusSettingToSpParameters(Hashtable parameters, ICustomer customer)
        {
            parameters.Add("disable", customer.StatusSetting.DisableVirtualSports);
            parameters.Add("disabledownlines", customer.StatusSetting.DisableDownlinesVirtualSports);
        }

        /// <summary>
        /// MapPTSettingToSpParameters method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <param name="customer">customer param</param>
        private void MapPTSettingToSpParameters(Hashtable parameters, SportType sporttype, BetType bettype)
        {
            parameters.Add("sporttype", sporttype.Id);
            parameters.Add("bettype", bettype.Id);

            parameters.Add("s4", bettype.Pt.S4);
            parameters.Add("s3", bettype.Pt.S3);
            parameters.Add("s1", bettype.Pt.S1);

            parameters.Add("m4", bettype.Pt.M4);
            parameters.Add("m3", bettype.Pt.M3);
            parameters.Add("m2", bettype.Pt.M2);
            parameters.Add("m1", bettype.Pt.M1);

            parameters.Add("a4", bettype.Pt.A4);
            parameters.Add("a2", bettype.Pt.A2);
            parameters.Add("a1", bettype.Pt.A1);

            parameters.Add("us3", bettype.Pt.Us3);
            parameters.Add("us1", bettype.Pt.Us1);

            parameters.Add("is3", bettype.Pt.Is3);
            parameters.Add("is1", bettype.Pt.Is1);

            parameters.Add("um2", bettype.Pt.Um2);
            parameters.Add("um1", bettype.Pt.Um1);

            parameters.Add("im2", bettype.Pt.Im2);
            parameters.Add("im1", bettype.Pt.Im1);

            parameters.Add("n4", bettype.Pt.N4);
            parameters.Add("n3", bettype.Pt.N3);
            parameters.Add("n2", bettype.Pt.N2);

            parameters.Add("l4", bettype.Pt.L4);
            parameters.Add("l3", bettype.Pt.L3);
            parameters.Add("l2", bettype.Pt.L2);
        }

        #endregion Mapping object to Sp parameters

        #region Private functions
        /// <summary>
        /// ComparingObjectProperties method
        /// </summary>
        /// <typeparam name="T">T typeparam</typeparam>
        /// <param name="oldCustomer">oldCustomer param</param>
        /// <param name="newCustomer">newCustomer param</param>
        /// <returns>return string</returns>
        private List<string> ComparingObjectProperties<T>(T oldCustomer, T newCustomer)
        {
            var resultDiffProperty = new List<string>();
            if (oldCustomer != null && newCustomer != null)
            {
                Type type = typeof(T);
                PropertyInfo[] propertyInfos;
                propertyInfos = typeof(T).GetProperties();
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    var propertyName = propertyInfo.Name;
                    var oldValue = type.GetProperty(propertyName).GetValue(oldCustomer, null);
                    var newValue = type.GetProperty(propertyName).GetValue(newCustomer, null);

                    if (newValue != null && !newValue.Equals(oldValue))
                    {
                        resultDiffProperty.Add(propertyName.ToLower());
                    }
                }
            }

            return resultDiffProperty;
        }

        #region Update functions
        /// <summary>
        /// UpdateSuperSettings method
        /// </summary>
        /// <param name="oldCustomerSettings">oldCustomerSettings param</param>
        /// <param name="newCustomerSettings">newCustomerSettings param</param>
        /// <param name="context">context param</param>
        /// <param name="otherSettings">otherSettings param</param>
        /// <returns>return VSResult</returns>
        private VSResult UpdateSuperSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, IRequestContext context, Hashtable otherSettings)
        {
            VSResult resultDto = new VSResult();
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            // Compare betting limit and update
            var bettingLimitIndex = 0;
            if (oldCustomerSettings.BettingLimit != null && newCustomerSettings.BettingLimit != null)
            {
                foreach (BettingLimitItem bettingLimitItem in newCustomerSettings.BettingLimit.BettingLimitItems)
                {
                    var parameters = new Hashtable();
                    parameters.Add("custid", context.TargetCustId);
                    parameters.Add("userid", context.AdminId);
                    parameters.Add("subaccid", context.SubAccId);
                    parameters.Add("language", context.Language);
                    parameters.Add("ip", context.IP);

                    var bettingEqual = this.ComparingObjectProperties(oldCustomerSettings.BettingLimit.BettingLimitItems[bettingLimitIndex], bettingLimitItem);
                    if (bettingEqual.Count > 0)
                    {
                        this.MapBettingLimitToSpParameters(parameters, bettingLimitItem);
                        var rs = customerDataAccess.Acc_CustomerUpdSuperVS_MinMaxBet(parameters);
                        resultDto.ResultBetSetting.Add(rs.ToNewResult(bettingLimitItem.Id));
                    }

                    bettingLimitIndex++;
                }
            }

            // Compare status and update
            var statusEqual = this.ComparingObjectProperties(oldCustomerSettings.StatusSetting, newCustomerSettings.StatusSetting);
            if (statusEqual.Count > 0)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);
                parameters.Add("systemid", _systemId);

                this.MapStatusSettingToSpParameters(parameters, newCustomerSettings);

                resultDto.ResultStatus.Add(customerDataAccess.Acc_CustomerUpdDisableVS(parameters));
            }

            // Compare PT setting and update
            var sportTypeIndex = 0;
            foreach (SportType sport in newCustomerSettings.PtSetting.SportTypes)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);

                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            var oldValue = (PT)oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt;
                            var compareResult = this.ComparingObjectProperties(oldValue, bettype.Pt);
                            if (compareResult.Contains("l4"))
                            {
                                compareResult.Remove("l4");
                            }
                            if (compareResult.Count > 0)
                            {
                                if (compareResult.Contains("n4"))
                                {
                                    parameters.Add("updateminpositiontaking", true);
                                }

                                if (compareResult.Contains("s4") || compareResult.Contains("m4") || compareResult.Contains("a4"))
                                {
                                    parameters.Add("updatepositiontaking", true);
                                }

                                this.MapPTSettingToSpParameters(parameters, sport, bettype);
                                resultDto.ResultPT.Add(customerDataAccess.Acc_CustomerUpdSuperVS_Positiontaking(parameters).ToNewResult(sport.Id));
                            }

                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }

            return resultDto;
        }

        /// <summary>
        /// UpdateMasterSettings method
        /// </summary>
        /// <param name="oldCustomerSettings">oldCustomerSettings param</param>
        /// <param name="newCustomerSettings">newCustomerSettings param</param>
        /// <param name="context">context param</param>
        /// <param name="otherSettings">otherSettings param</param>
        /// <returns>return VSResult</returns>
        private VSResult UpdateMasterSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, IRequestContext context, Hashtable otherSettings)
        {
            VSResult resultDto = new VSResult();
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            // Compare betting limit and update
            var bettingLimitIndex = 0;
            if (oldCustomerSettings.BettingLimit != null && newCustomerSettings.BettingLimit != null)
            {
                foreach (BettingLimitItem bettingLimitItem in newCustomerSettings.BettingLimit.BettingLimitItems)
                {
                    var parameters = new Hashtable();
                    parameters.Add("custid", context.TargetCustId);
                    parameters.Add("userid", context.AdminId);
                    parameters.Add("subaccid", context.SubAccId);
                    parameters.Add("language", context.Language);
                    parameters.Add("ip", context.IP);

                    var bettingEqual = this.ComparingObjectProperties(oldCustomerSettings.BettingLimit.BettingLimitItems[bettingLimitIndex], bettingLimitItem);
                    if (bettingEqual.Count > 0)
                    {
                        this.MapBettingLimitToSpParameters(parameters, bettingLimitItem);
                        var rs = customerDataAccess.Acc_CustomerUpdMasterVS_MinMaxBet(parameters);
                        resultDto.ResultBetSetting.Add(rs.ToNewResult(bettingLimitItem.Id));
                    }

                    bettingLimitIndex++;
                }
            }

            // Compare status and update
            var statusEqual = this.ComparingObjectProperties(oldCustomerSettings.StatusSetting, newCustomerSettings.StatusSetting);
            if (statusEqual.Count > 0)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);
                parameters.Add("systemid", _systemId);

                this.MapStatusSettingToSpParameters(parameters, newCustomerSettings);

                resultDto.ResultStatus.Add(customerDataAccess.Acc_CustomerUpdDisableVS(parameters));
            }

            // Compare PT setting and update
            var sportTypeIndex = 0;
            foreach (SportType sport in newCustomerSettings.PtSetting.SportTypes)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);

                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            PT oldValue = (PT)oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt;
                            var compareResult = this.ComparingObjectProperties(oldValue, bettype.Pt);
                            if (compareResult.Count > 0)
                            {
                                this.MapPTSettingToSpParameters(parameters, sport, bettype);

                                if (compareResult.Contains("n3"))
                                {
                                    parameters.Add("updateminpositiontaking", true);
                                }

                                if (compareResult.Contains("s3") || compareResult.Contains("m3") || compareResult.Contains("a3") || compareResult.Contains("s1"))
                                {
                                    parameters.Add("updatepositiontaking", true);
                                }

                                if (compareResult.Contains("us3"))
                                {
                                    parameters.Add("updateautopositiontaking", true);
                                }

                                if (compareResult.Contains("is3"))
                                {
                                    if (!parameters.ContainsKey("updateautopositiontaking"))
                                    {
                                        parameters.Add("updateautopositiontaking", true);
                                    }
                                }

                                if (compareResult.Contains("s1"))
                                {
                                    if (!bettype.Pt.Is3.Value)
                                    {
                                        parameters["s1"] = bettype.Pt.S1;
                                        parameters.Add("updateallmembers", bool.Parse(otherSettings["isupdateallmembers"].ToString()));
                                    }
                                }

                                resultDto.ResultPT.Add(customerDataAccess.Acc_CustomerUpdMasterVS_Positiontaking(parameters).ToNewResult(sport.Id));
                            }

                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }

            return resultDto;
        }

        /// <summary>
        /// UpdateAgentSettings method
        /// </summary>
        /// <param name="oldCustomerSettings">oldCustomerSettings param</param>
        /// <param name="newCustomerSettings">newCustomerSettings param</param>
        /// <param name="context">context param</param>
        /// <param name="otherSettings">otherSettings param</param>
        /// <returns>return VSResult</returns>
        private VSResult UpdateAgentSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, IRequestContext context, Hashtable otherSettings)
        {
            VSResult resultDto = new VSResult();
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            // Compare betting limit and update
            var bettingLimitIndex = 0;
            if (oldCustomerSettings.BettingLimit != null && newCustomerSettings.BettingLimit != null)
            {
                foreach (BettingLimitItem bettingLimitItem in newCustomerSettings.BettingLimit.BettingLimitItems)
                {
                    var parameters = new Hashtable();
                    parameters.Add("custid", context.TargetCustId);
                    parameters.Add("userid", context.AdminId);
                    parameters.Add("subaccid", context.SubAccId);
                    parameters.Add("language", context.Language);
                    parameters.Add("ip", context.IP);

                    var bettingEqual = this.ComparingObjectProperties(oldCustomerSettings.BettingLimit.BettingLimitItems[bettingLimitIndex], bettingLimitItem);
                    if (bettingEqual.Count > 0)
                    {
                        this.MapBettingLimitToSpParameters(parameters, bettingLimitItem);
                        var rs = customerDataAccess.Acc_CustomerUpdAgentVS_MinMaxBet(parameters);
                        resultDto.ResultBetSetting.Add(rs.ToNewResult(bettingLimitItem.Id));
                    }

                    bettingLimitIndex++;
                }
            }

            // Compare status and update
            var statusEqual = this.ComparingObjectProperties(oldCustomerSettings.StatusSetting, newCustomerSettings.StatusSetting);
            if (statusEqual.Count > 0)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);
                parameters.Add("systemid", _systemId);

                this.MapStatusSettingToSpParameters(parameters, newCustomerSettings);

                resultDto.ResultStatus.Add(customerDataAccess.Acc_CustomerUpdDisableVS(parameters));
            }

            // Compare PT setting and update
            var sportTypeIndex = 0;
            foreach (SportType sport in newCustomerSettings.PtSetting.SportTypes)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);

                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            var oldValue = (PT)oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt;
                            var compareResult = this.ComparingObjectProperties(oldValue, bettype.Pt);
                            if (compareResult.Count > 0)
                            {
                                this.MapPTSettingToSpParameters(parameters, sport, bettype);

                                if (compareResult.Contains("n2"))
                                {
                                    parameters.Add("updateminpositiontaking", true);
                                }

                                if (compareResult.Contains("s2") || compareResult.Contains("m2") || compareResult.Contains("a2") || compareResult.Contains("m1"))
                                {
                                    parameters.Add("updatepositiontaking", true);
                                }

                                if (compareResult.Contains("um2"))
                                {
                                    parameters.Add("updateautopositiontaking", true);
                                }

                                if (compareResult.Contains("im2"))
                                {
                                    if (!parameters.ContainsKey("updateautopositiontaking"))
                                    {
                                        parameters.Add("updateautopositiontaking", true);
                                    }
                                }

                                if (compareResult.Contains("m1"))
                                {
                                    if (!bettype.Pt.Im2.Value)
                                    {
                                        parameters["m1"] = bettype.Pt.M1.Value;
                                        parameters.Add("updateallmembers", bool.Parse(otherSettings["isupdateallmembers"].ToString()));
                                    }
                                }

                                resultDto.ResultPT.Add(customerDataAccess.Acc_CustomerUpdAgentVS_PositionTaking(parameters).ToNewResult(sport.Id));
                            }

                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }

            return resultDto;
        }

        /// <summary>
        /// UpdateMemberSettings method
        /// </summary>
        /// <param name="oldCustomerSettings">oldCustomerSettings param</param>
        /// <param name="newCustomerSettings">newCustomerSettings param</param>
        /// <param name="context">context param</param>
        /// <returns>return VSResult</returns>
        private VSResult UpdateMemberSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, IRequestContext context)
        {
            VSResult resultDto = new VSResult();
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            // Compare betting limit and update
            var bettingLimitIndex = 0;
            if (oldCustomerSettings == null || (oldCustomerSettings.BettingLimit != null && newCustomerSettings.BettingLimit != null))
            {
                foreach (BettingLimitItem bettingLimitItem in newCustomerSettings.BettingLimit.BettingLimitItems)
                {
                    var parameters = new Hashtable();
                    parameters.Add("custid", context.TargetCustId);
                    parameters.Add("userid", context.AdminId);
                    parameters.Add("subaccid", context.SubAccId);
                    parameters.Add("language", context.Language);
                    parameters.Add("ip", context.IP);

                    var bettingEqual = oldCustomerSettings != null ? this.ComparingObjectProperties(oldCustomerSettings.BettingLimit.BettingLimitItems[bettingLimitIndex], bettingLimitItem) : null;
                    if (oldCustomerSettings == null || bettingEqual.Count > 0)
                    {
                        this.MapBettingLimitToSpParameters(parameters, bettingLimitItem);
                        var rs = customerDataAccess.Acc_CustomerUpdMemberVS_MinMaxBet(parameters);
                        resultDto.ResultBetSetting.Add(rs.ToNewResult(bettingLimitItem.Id));
                    }

                    bettingLimitIndex++;
                }
            }

            // Compare status and update
            var statusEqual = oldCustomerSettings != null ? this.ComparingObjectProperties(oldCustomerSettings.StatusSetting, newCustomerSettings.StatusSetting) : null;
            if (oldCustomerSettings == null || statusEqual.Count > 0)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);
                parameters.Add("systemid", _systemId);

                this.MapStatusSettingToSpParameters(parameters, newCustomerSettings);

                resultDto.ResultStatus.Add(customerDataAccess.Acc_CustomerUpdDisableVS(parameters));
            }

            // Compare PT setting and update
            var sportTypeIndex = 0;

            foreach (SportType sport in newCustomerSettings.PtSetting.SportTypes)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);

                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            var oldValue = oldCustomerSettings != null ? (PT)oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt : null;
                            var compareResult = oldCustomerSettings != null ? this.ComparingObjectProperties(oldValue, bettype.Pt) : null;
                            if (oldCustomerSettings == null || compareResult.Count > 0)
                            {
                                this.MapPTSettingToSpParameters(parameters, sport, bettype);
                                resultDto.ResultPT.Add(customerDataAccess.Acc_CustomerUpdMemberVS_Positiontaking(parameters).ToNewResult(sport.Id));
                            }

                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }

            return resultDto;
        }

        /// <summary>
        /// MasterUpdateMemberSettings method
        /// </summary>
        /// <param name="oldCustomerSettings">oldCustomerSettings param</param>
        /// <param name="newCustomerSettings">newCustomerSettings param</param>
        /// <param name="context">context param</param>
        /// <returns>return VSResult</returns>
        private VSResult MasterUpdateMemberSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, IRequestContext context)
        {
            VSResult resultDto = new VSResult();
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            // Compare PT setting and update
            var sportTypeIndex = 0;
            foreach (SportType sport in newCustomerSettings.PtSetting.SportTypes)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);

                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            var oldValue = (PT)oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt;
                            if (bettype.Pt.Im2.Value && !bettype.Pt.Im1.Value &&
                                bettype.Pt.Im2.Value == oldValue.Im2.Value &&
                                bettype.Pt.Im1.Value == oldValue.Im1.Value) // the special case
                            {
                                //resultDto.ResultPT.Add(new Result("98", string.Empty));
                            }
                            else
                            {
                                var compareResult = this.ComparingObjectProperties(oldValue, bettype.Pt);
                                if (compareResult.Count > 0)
                                {
                                    bool isUpdate = false;

                                    if (compareResult.Contains("m1"))
                                    {
                                        parameters.Add("updatepositiontaking", true);
                                        isUpdate = true;
                                    }

                                    if (compareResult.Contains("um1") | compareResult.Contains("im1"))
                                    {
                                        parameters.Add("updateautopositiontaking", true);
                                        isUpdate = true;
                                    }

                                    if (isUpdate)
                                    {
                                        this.MapPTSettingToSpParameters(parameters, sport, bettype);
                                        resultDto.ResultPT.Add(customerDataAccess.Acc_CustomerUpdMemberVS_MAg(parameters).ToNewResult(sport.Id));
                                    }
                                }
                            }

                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }

            return resultDto;
        }

        /// <summary>
        /// SuperUpdateMemberSettings method
        /// </summary>
        /// <param name="oldCustomerSettings">oldCustomerSettings param</param>
        /// <param name="newCustomerSettings">newCustomerSettings param</param>
        /// <param name="context">context param</param>
        /// <returns>return VSResult</returns>
        private VSResult SuperUpdateMemberSettings(ICustomer oldCustomerSettings, ICustomer newCustomerSettings, IRequestContext context)
        {
            VSResult resultDto = new VSResult();
            ICustomerDataAccess customerDataAccess = new CustomerDataAccess();

            // Compare PT setting and update
            var sportTypeIndex = 0;
            foreach (SportType sport in newCustomerSettings.PtSetting.SportTypes)
            {
                var parameters = new Hashtable();
                parameters.Add("custid", context.TargetCustId);
                parameters.Add("userid", context.AdminId);
                parameters.Add("subaccid", context.SubAccId);
                parameters.Add("language", context.Language);
                parameters.Add("ip", context.IP);

                var bettypeGroupSetIndex = 0;
                foreach (BetTypeGroupSet bettypeGroupSet in sport.BetTypeGroupSets)
                {
                    var bettypeGroupIndex = 0;
                    foreach (BetTypeGroup bettypeGroup in bettypeGroupSet.BetTypeGroups)
                    {
                        var bettypeIndex = 0;
                        foreach (BetType bettype in bettypeGroup.BetTypes)
                        {
                            var oldValue = (PT)oldCustomerSettings.PtSetting.SportTypes[sportTypeIndex].BetTypeGroupSets[bettypeGroupSetIndex].BetTypeGroups[bettypeGroupIndex].BetTypes[bettypeIndex].Pt;
                            if (bettype.Pt.Is3.Value && !bettype.Pt.Is1.Value &&
                                bettype.Pt.Is3.Value == oldValue.Is3.Value &&
                                bettype.Pt.Is1.Value == oldValue.Is1.Value) // the special case
                            {
                                //resultDto.ResultPT.Add(new Result("99", string.Empty));
                            }
                            else
                            {
                                var compareResult = this.ComparingObjectProperties(oldValue, bettype.Pt);
                                if (compareResult.Count > 0)
                                {
                                    bool isUpdate = false;

                                    if (compareResult.Contains("s1"))
                                    {
                                        parameters.Add("updatepositiontaking", true);
                                        isUpdate = true;
                                    }

                                    if (compareResult.Contains("us1") | compareResult.Contains("is1"))
                                    {
                                        parameters.Add("updateautopositiontaking", true);
                                        isUpdate = true;
                                    }

                                    if (isUpdate)
                                    {
                                        this.MapPTSettingToSpParameters(parameters, sport, bettype);
                                        resultDto.ResultPT.Add(customerDataAccess.Acc_CustomerUpdMemberVS_SAg(parameters).ToNewResult(sport.Id));
                                    }
                                }
                            }

                            bettypeIndex++;
                        }

                        bettypeGroupIndex++;
                    }

                    bettypeGroupSetIndex++;
                }

                sportTypeIndex++;
            }

            return resultDto;
        }
        #endregion

        #endregion
    }
}
