namespace Fanex.PTVirtualSports.BusinessObjects
{
    using System;

    using Fanex.PTEngine.BusinessObjects;
    using System.Collections;

    public class Customer : ICustomer
    {
        public Customer()
        {
            this.Password = string.Empty;
            this.BettingLimit = new BettingLimit();
            this.StatusSetting = new StatusSetting();
            this.PtSetting = new PTGrid();
        }

        #region ICustomer Members

        public int CustId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string NewUserName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Password
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public int Creator
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string MobilePhone
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Fax
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string Postal
        {
            get;
            set;
        }

        public int CurrencyId
        {
            get;
            set;
        }

        public string Group
        {
            get;
            set;
        }

        public decimal Credit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal MinCredit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal MaxCredit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal MemberMaxCredit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal MemberMinCredit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SiteName
        {
            get;
            set;
        }

        public bool IsInternal
        {
            get;
            set;
        }

        public int SRecommend
        {
            get;
            set;
        }

        public int MRecommend
        {
            get;
            set;
        }

        public int ARecommend
        {
            get;
            set;
        }

        public string Language
        {
            get;
            set;
        }

        public string TransferOption
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IBettingLimit<BettingLimitItem> BettingLimit
        {
            get;
            set;
        }

        public IStatusSetting StatusSetting
        {
            get;
            set;
        }

        public ICommissionSetting CommissionSetting
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ICommissionSetting CommissionSetting2
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IPTGrid<SportType> PtSetting
        {
            get;
            set;
        }

        /// <summary>
        /// throw new NotImplementedException
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public System.Collections.Hashtable MapToSpParameters(IRequestContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throw new NotImplementedException
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        public void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// throw new NotImplementedException
        /// </summary>
        public void InitDefaultValue()
        {
            throw new NotImplementedException();
        }

        public void MapToSpUpdateMultipleParameters(ref Hashtable parameters, Hashtable updateParameters, bool isUpadateBettingLimit)
        {
            throw new NotImplementedException();
        }

        #endregion ICustomer Members
    }
}