namespace Fanex.PTVirtualSports.DataAccess
{
    using System.Collections;
    using System.Data;
    using Fanex.PTEngine.BusinessObjects;

    /// <summary>
    /// ICustomerDataAccess interface
    /// </summary>
    public interface ICustomerDataAccess
    {        
        /// <summary>
        /// Acc_CustomerUpdSuperVS_MinMaxBet method
        /// </summary>
        /// <param name="parameters">param</param>
        /// <returns>Result return</returns>
        Result Acc_CustomerUpdSuperVS_MinMaxBet(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdSuperVS_Positiontaking method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdSuperVS_Positiontaking(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdDisableVS method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdDisableVS(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerInsMember_Com method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerInsMember_Com(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerInsMember_SAg method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerInsMember_SAg(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerInsMember_MAg method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerInsMember_MAg(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerInsMember_Ag method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerInsMember_Ag(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerInsUserInfo method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerInsUserInfo(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerSelEditSuperVS method
        /// </summary>
        /// <param name="requestContext">requestContext param</param>
        /// <returns>return DataSet</returns>
        DataSet Acc_CustomerSelEditSuperVS(IRequestContext requestContext);

        /// <summary>
        /// Acc_CustomerSelEditMasterVS method
        /// </summary>
        /// <param name="requestContext">requestContext param</param>
        /// <returns>return DataSet</returns>
        DataSet Acc_CustomerSelEditMasterVS(IRequestContext requestContext);

        /// <summary>
        /// Acc_CustomerSelEditAgentVS method
        /// </summary>
        /// <param name="requestContext">requestContext param</param>
        /// <returns>return DataSet</returns>
        DataSet Acc_CustomerSelEditAgentVS(IRequestContext requestContext);

        /// <summary>
        /// Acc_CustomerSelEditMemberVS method
        /// </summary>
        /// <param name="requestContext">requestContext param</param>
        /// <returns>return DataSet</returns>
        DataSet Acc_CustomerSelEditMemberVS(IRequestContext requestContext);

        /// <summary>
        /// Acc_CustomerSelMEditCustomerVS method
        /// </summary>
        /// <param name="requestContext">requestContext param</param>
        /// <returns>return DataSet</returns>
        DataSet Acc_CustomerSelMEditCustomerVS(IRequestContext requestContext);

        /// <summary>
        /// Acc_CustomerSelSEditCustomerVS method
        /// </summary>
        /// <param name="requestContext">requestContext param</param>
        /// <returns>return DataSet</returns>
        DataSet Acc_CustomerSelSEditCustomerVS(IRequestContext requestContext);

        /// <summary>
        /// Acc_CustomerUpdMasterVS_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdMasterVS_MinMaxBet(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdMasterVS_Positiontaking method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdMasterVS_Positiontaking(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdAgentVS_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdAgentVS_MinMaxBet(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdAgentVS_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdAgentVS_PositionTaking(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdMemberVS_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdMemberVS_MinMaxBet(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdMemberVS_Positiontaking method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdMemberVS_Positiontaking(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdMemberVS_MAg method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdMemberVS_MAg(Hashtable parameters);

        /// <summary>
        /// Acc_CustomerUpdMemberVS_SAg method
        /// </summary>
        /// <param name="parameters">parameters param</param>
        /// <returns>return Result</returns>
        Result Acc_CustomerUpdMemberVS_SAg(Hashtable parameters);
    }
}