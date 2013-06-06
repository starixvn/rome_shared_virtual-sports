namespace Fanex.PTVirtualSports.DataAccess
{
    using System.Collections;
    using System.Data;
    using Fanex.Data;
    using Fanex.PTEngine.BusinessObjects;

    /// <summary>
    /// CustomerDataAccess method
    /// </summary>
    public class CustomerDataAccess : DataAccessBase, ICustomerDataAccess
    {
        #region  //Add New Customer
        /// <summary>
        /// Acc_CustomerInsMember_Com method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerInsMember_Com(System.Collections.Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_Com", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_CustomerInsMember_SAg method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerInsMember_SAg(System.Collections.Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_SAg", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_CustomerInsMember_MAg method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerInsMember_MAg(System.Collections.Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_MAg", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_CustomerInsMember_Ag method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerInsMember_Ag(System.Collections.Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsMember_Ag", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_CustomerInsUserInfo method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerInsUserInfo(System.Collections.Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerInsUserInfo", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion

        #region //Select for editing customer
        /// <summary>
        /// Acc_CustomerSelEditSuperVS method
        /// </summary>
        /// <param name="requestContext">requestContext of this method</param>
        /// <returns>Result of this method</returns>
        public DataSet Acc_CustomerSelEditSuperVS(IRequestContext requestContext)
        {
            DataSet dataSet;
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditSuperVS", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                dataSet = db.ExecuteDataSet();
            }

            return dataSet;
        }

        /// <summary>
        /// Acc_CustomerSelEditMasterVS method
        /// </summary>
        /// <param name="requestContext">requestContext of this method</param>
        /// <returns>Result of this method</returns>
        public DataSet Acc_CustomerSelEditMasterVS(IRequestContext requestContext)
        {
            DataSet dataSet;
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditMasterVS", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                dataSet = db.ExecuteDataSet();
            }

            return dataSet;
        }

        /// <summary>
        /// Acc_CustomerSelEditAgentVS method
        /// </summary>
        /// <param name="requestContext">requestContext of this method</param>
        /// <returns>Result of this method</returns>
        public DataSet Acc_CustomerSelEditAgentVS(IRequestContext requestContext)
        {
            DataSet dataSet;
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditAgentVS", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                dataSet = db.ExecuteDataSet();
            }

            return dataSet;
        }

        /// <summary>
        /// Acc_CustomerSelEditMemberVS method
        /// </summary>
        /// <param name="requestContext">requestContext of this method</param>
        /// <returns>Result of this method</returns>
        public DataSet Acc_CustomerSelEditMemberVS(IRequestContext requestContext)
        {
            DataSet dataSet;
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelEditMemberVS", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                dataSet = db.ExecuteDataSet();
            }

            return dataSet;
        }

        /// <summary>
        /// Acc_CustomerSelMEditCustomerVS method
        /// </summary>
        /// <param name="requestContext">requestContext of this method</param>
        /// <returns>Result of this method</returns>
        public DataSet Acc_CustomerSelMEditCustomerVS(IRequestContext requestContext)
        {
            DataSet dataSet;
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelMEditCustomerVS", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                dataSet = db.ExecuteDataSet();
            }

            return dataSet;
        }

        /// <summary>
        /// Acc_CustomerSelSEditCustomerVS method
        /// </summary>
        /// <param name="requestContext">requestContext of this method</param>
        /// <returns>Result of this method</returns>
        public DataSet Acc_CustomerSelSEditCustomerVS(IRequestContext requestContext)
        {
            DataSet dataSet;
            using (ObjectDb db = new ObjectDb("Acc_CustomerSelSEditCustomerVS", 120))
            {
                db.AddInParameter("custid", DbType.Int32, requestContext.TargetCustId);
                dataSet = db.ExecuteDataSet();
            }

            return dataSet;
        }
        #endregion
        
        #region super
        /// <summary>
        /// Acc_CompanyUpdateSuper_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdSuperVS_MinMaxBet(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdSuperVS_MinMaxBet", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_CompanyUpdateSuper_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdSuperVS_Positiontaking(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdSuperVS_Positiontaking", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_CustomerUpdateStatus method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdDisableVS(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdDisableVS", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion

        #region master
        /// <summary>
        /// Acc_SuperUpdateMaster_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdMasterVS_MinMaxBet(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMasterVS_MinMaxBet", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_SuperUpdateMaster_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdMasterVS_Positiontaking(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMasterVS_Positiontaking", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion

        #region agent
        /// <summary>
        /// Acc_MasterUpdateAgent_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdAgentVS_MinMaxBet(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdAgentVS_MinMaxBet", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_MasterUpdateAgent_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdAgentVS_PositionTaking(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdAgentVS_PositionTaking", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion

        #region member
        /// <summary>
        /// Acc_AgentUpdateMember_MinMaxBet method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdMemberVS_MinMaxBet(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMemberVS_MinMaxBet", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }

        /// <summary>
        /// Acc_AgentUpdateMember_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdMemberVS_Positiontaking(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMemberVS_Positiontaking", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion

        #region master update on member
        /// <summary>
        /// Acc_MasterUpdateMember_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdMemberVS_MAg(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMemberVS_MAg", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion

        #region super update on member
        /// <summary>
        /// Acc_SuperUpdateMember_PositionTaking method
        /// </summary>
        /// <param name="parameters">parameters of this method</param>
        /// <returns>Result of this method</returns>
        public Result Acc_CustomerUpdMemberVS_SAg(Hashtable parameters)
        {
            using (ObjectDb db = new ObjectDb("Acc_CustomerUpdMemberVS_SAg", 120))
            {
                db.AssignParamValues(parameters);
                db.ExecuteNonQuery();
                return this.GetResultParams(db.Database, db.DbCommand);
            }
        }
        #endregion
    }        
}
