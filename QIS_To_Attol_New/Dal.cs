using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Configuration;
using System.IO;
using System.Data;
namespace QIS_To_Attol_New
{
    public class Dal
    {
        string strquery = "";

        public OracleConnection Connect(string astrConnectionString)
        {
            try
            {
                return new OracleConnection(ConfigurationManager.ConnectionStrings[astrConnectionString].ConnectionString);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void updateGSMCELL(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
                strquery = "Update GTRANSMITTERS  set CONTROL_CHANNEL = 'BCCHNO', BSIC = 'BSIC_QIS' where tx_id = 'cell'";
                string strBCCHNO = "";
                string strCELLNAME = "";
                string strBSIC = "";

                strBCCHNO = General.dt_CURR_FDDCELL.Rows[i]["BCCHNO"].ToString();
                strCELLNAME = General.dt_CURR_FDDCELL.Rows[i]["cell"].ToString();
                strBSIC = General.dt_CURR_FDDCELL.Rows[i]["BSIC"].ToString();

                strquery = strquery.Replace("BCCHNO", strBCCHNO).Replace("BSIC_QIS", strBSIC).Replace("cell", strCELLNAME);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }

        }
        public void updateLTECELLS(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
               
                strquery = "Update LCELLS  set TAC  = 'QIS_TAC', PHY_CELL_ID = '((PHYSICALLAYERCELLIDGROUP*3) + PHYSICALLAYERSUBCELLID)' where CELL_ID = 'EUTRANCELLFDDID'";
                string strTAC = "";
                string strEUTRANCELLFDDID = "";
                string strPHYSICALLAYERSUBCELLID = "";
                string strPHYSICALLAYERCELLIDGROUP = "";
                int intPHYSICALLAYERCELLIDGROUP = 0;
                int intPHYSICALLAYERSUBCELLID = 0;
                int intPHY_CELL_ID = 0;

                strTAC = General.dt_CURR_FDDCELL.Rows[i]["TAC"].ToString();
                strEUTRANCELLFDDID = General.dt_CURR_FDDCELL.Rows[i]["EUTRANCELLFDDID"].ToString();
                strPHYSICALLAYERCELLIDGROUP = General.dt_CURR_FDDCELL.Rows[i]["PHYSICALLAYERCELLIDGROUP"].ToString();
                strPHYSICALLAYERSUBCELLID = General.dt_CURR_FDDCELL.Rows[i]["PHYSICALLAYERSUBCELLID"].ToString();

                intPHYSICALLAYERCELLIDGROUP = Convert.ToInt32(strPHYSICALLAYERCELLIDGROUP);
                intPHYSICALLAYERSUBCELLID = Convert.ToInt32(strPHYSICALLAYERSUBCELLID);
                intPHY_CELL_ID = intPHYSICALLAYERCELLIDGROUP * 3 + intPHYSICALLAYERSUBCELLID;

                strquery = strquery.Replace("QIS_TAC", strTAC).Replace("EUTRANCELLFDDID", strEUTRANCELLFDDID).Replace("((PHYSICALLAYERCELLIDGROUP*3) + PHYSICALLAYERSUBCELLID)", Convert.ToString(intPHY_CELL_ID));
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }

        }
        public void updateFDDCELLS(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
               
                strquery = "Update UCELLS  set LAC = 'LOCATIONAREACODE', RAC = 'ROUTINGAREACODE', SCRAMBLING_CODE = 'PRIMARYSCRAMBLINGCODE' where CELL_ID = 'USERLABEL'";
                string strLOCATIONAREACODE = "";
                string strROUTINGAREACODE = "";
                string strUSERLABEL = "";
                string strPRIMARYSCRAMBLINGCODE = "";
                strLOCATIONAREACODE = General.dt_CURR_FDDCELL.Rows[i]["LOCATIONAREACODE"].ToString();
                strROUTINGAREACODE = General.dt_CURR_FDDCELL.Rows[i]["ROUTINGAREACODE"].ToString();
                strUSERLABEL = General.dt_CURR_FDDCELL.Rows[i]["USERLABEL"].ToString();
                strPRIMARYSCRAMBLINGCODE = General.dt_CURR_FDDCELL.Rows[i]["PRIMARYSCRAMBLINGCODE"].ToString();
                strquery = strquery.Replace("PRIMARYSCRAMBLINGCODE", strPRIMARYSCRAMBLINGCODE).Replace("LOCATIONAREACODE", strLOCATIONAREACODE).Replace("ROUTINGAREACODE", strROUTINGAREACODE).Replace("USERLABEL", strUSERLABEL);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            }

        public void updateFDDCELL_Site(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {            
                strquery = "update Sites set RNC_ID_  = 'RNC_NAME' where name = 'fddCELL_name'";
                string strRNC_NAME = "";
                string strfddCELL_name = "";

                strRNC_NAME = General.dt_CURR_FDDCELL.Rows[i]["RNC_NAME"].ToString();
                strfddCELL_name = General.dt_CURR_FDDCELL.Rows[i]["SUBSTR(USERLABEL,2,6)"].ToString();
                strquery = strquery.Replace("RNC_NAME", strRNC_NAME).Replace("fddCELL_name", strfddCELL_name);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }


        public void updateCNA(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
                strquery = "update Sites set HDBSC = 'BSC_QIS' where Name = 'site_ID'";
                string strBSC = "";
                string strsite_ID = "";

                strBSC = General.dt_CURR_FDDCELL.Rows[i]["BSC"].ToString();
                strsite_ID = General.dt_CURR_FDDCELL.Rows[i]["SITE_ID"].ToString();
                strquery = strquery.Replace("BSC_QIS", strBSC).Replace("site_ID", strsite_ID);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void updateLTECELLS_Site(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
                strquery = "update Sites set Cluster_ = 'Clusterid' where Name ='substr (erbsname,2,6)'";
                string strClusterid = "";
                string strsite_ID = "";

                strClusterid = General.dt_CURR_FDDCELL.Rows[i]["CLUSTERID"].ToString();
                strsite_ID = General.dt_CURR_FDDCELL.Rows[i]["SUBSTR(ERBSNAME,2,6)"].ToString();
                strquery = strquery.Replace("Clusterid", strClusterid).Replace("site_ID", strsite_ID);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void updateALLTECHNOLOGIESSITES(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
                strquery = "update Sites set ENG_NAME = 'user1' where company  LIKE '%Orange%' and  substr (name,3,6) = 'sitenum'";
                string strUser = "";
                string strsite_ID = "";

                strUser = General.dt_CURR_FDDCELL.Rows[i]["USER1"].ToString();
                strsite_ID = General.dt_CURR_FDDCELL.Rows[i]["SITE_NO"].ToString();
                strquery = strquery.Replace("user1", strUser).Replace("sitenum", strsite_ID);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }


        public void updateUcells(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
                strquery = "UPDATE Ucells SET  ura= 'uralist' WHERE cell_ID = 'cellname'";
                string struralist = "";
                string strcellname = "";

                struralist = General.dt_CURR_FDDCELL.Rows[i]["uralist"].ToString();
                strcellname = General.dt_CURR_FDDCELL.Rows[i]["cellname"].ToString();
                strquery = strquery.Replace("uralist", struralist).Replace("cellname", strcellname);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }




        public void updategtransmitters(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {
                strquery = "update gtransmitters set LAC  = 'LAC_qis' where TX_ID = 'cell_name'";
                string strLac = "";
                string strCELL_name = "";

                strLac = General.dt_CURR_FDDCELL.Rows[i]["LAC"].ToString();
                strCELL_name = General.dt_CURR_FDDCELL.Rows[i]["CELL"].ToString();
                strquery = strquery.Replace("LAC_qis", strLac).Replace("cell_name", strCELL_name);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }



        public void updateSIB(DataTable dt)
        {
            for (int i = 0; i < General.dt_CURR_FDDCELL.Rows.Count; i++)
            {

                strquery = "update UCELLS set SIB1  = 'SIB1PLMNSCOPEVALUETAG' where CELL_ID = 'Utrancellid'";
                string SIB1PLMNSCOPEVALUETAG = "";
                string Utrancellid = "";
                SIB1PLMNSCOPEVALUETAG = General.dt_CURR_FDDCELL.Rows[i]["SIB1PLMNSCOPEVALUETAG"].ToString();
                Utrancellid = General.dt_CURR_FDDCELL.Rows[i]["Utrancellid"].ToString();
                strquery = strquery.Replace("SIB1PLMNSCOPEVALUETAG", SIB1PLMNSCOPEVALUETAG).Replace("Utrancellid", Utrancellid);
                OracleConnection oConn = Connect("AttolConnection");
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandText = strquery;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

       
                
            }
        }

   
   

