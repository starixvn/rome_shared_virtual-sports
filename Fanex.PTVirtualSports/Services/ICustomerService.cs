namespace Fanex.PTVirtualSports.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Fanex.PTEngine.BusinessObjects;
    using Fanex.PTVirtualSports.BusinessObjects;

    /// <summary>
    /// ICustomerService interface
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Add new or update customer with specified context
        /// </summary>
        /// <param name="oldCustomer">If this is null, add new customer case is specified</param>
        /// <param name="newCustomer">newCustomer param</param>
        /// <param name="context">SMA context</param>
        /// <param name="parameters">Additional parameters</param>
        /// <returns>return VSResult</returns>
        VSResult SaveCustomer(ICustomer oldCustomer, ICustomer newCustomer, IRequestContext context, Hashtable parameters);

        /// <summary>
        /// Get customer with specified context
        /// </summary>
        /// <param name="context">context param</param>
        /// <returns>ICustomer return</returns>
        ICustomer GetCustomer(IRequestContext context);
    }
}