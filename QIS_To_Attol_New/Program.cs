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
  public  class Program
    {
        static void Main(string[] args)
        {

            DataSet ds_QIS_Data = new DataSet();
            DataTable dt_CURR_FDDCELL = new DataTable();

            //Get Data from QIS
            Dal dal = new Dal();
            OracleCommand oCommand;
            OracleDataAdapter oDataAdapter;
            OracleConnection oConn = dal.Connect("QISConnection");
            oConn.Open();

            oCommand = new OracleCommand();
            oCommand.Connection = oConn;
            oCommand.CommandText = "select BCCHNO,BSIC,cell FROM CNA";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "GSMCELL");
            var reader1 = oCommand.ExecuteReader();


            oCommand.CommandText = "select PHYSICALLAYERSUBCELLID,PHYSICALLAYERCELLIDGROUP,TAC,EUTRANCELLFDDID FROM CURR_LTECELLS";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "LTECELLS");
            var reader2 = oCommand.ExecuteReader();

            oCommand.CommandText = "select PRIMARYSCRAMBLINGCODE,LOCATIONAREACODE,ROUTINGAREACODE,userlabel from QIS.CURR_FDDCELL";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "FDDCELL");
            var reader3 = oCommand.ExecuteReader();

            oCommand.CommandText = "select RNC_NAME, substr(userlabel,2,6) from CURR_fddCELL";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "FDDCELL_Site");
            var reader4 = oCommand.ExecuteReader();

            oCommand.CommandText = "select BSC, site_ID from CNA";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "CNA");
            var reader5 = oCommand.ExecuteReader();

            oCommand.CommandText = "select Clusterid, substr (erbsname,2,6) from CURR_LTECELLS";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "LTECELLS_Site");
            var reader6 = oCommand.ExecuteReader();

            oCommand.CommandText = "SELECT USER1, SITE_NO FROM V_ALLTECHNOLOGIESSITES";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "ALLTECHNOLOGIESSITES");
            var reader7 = oCommand.ExecuteReader();

            oCommand.CommandText = "Select uralist, cellname,hdate from H_EU_UtranCell WHERE  hdate =  to_char(sysdate-1,'YYYYMMDD') ";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "H_EU_UtranCell");
            var reader8 = oCommand.ExecuteReader();

            oCommand.CommandText = "select LAC, cell from CNA";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "gtransmitters");
            var reader9 = oCommand.ExecuteReader();

            oCommand.CommandText = "select distinct Utrancellid, SIB1PLMNSCOPEVALUETAG FROM H_EU_UTRANCELL";
            oDataAdapter = new OracleDataAdapter(oCommand);
            oDataAdapter.Fill(ds_QIS_Data, "SIB1");
            var reader10 = oCommand.ExecuteReader();


            oConn.Close();

            foreach (DataTable table in ds_QIS_Data.Tables)
                   {
                       string strTBLname = "";
                       strTBLname = table.TableName;
                       switch (strTBLname)
                       {
                           case "FDDCELL":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["FDDCELL"];
                               dal.updateFDDCELLS(General.dt_CURR_FDDCELL);
                               break;

                           case "LTECELLS":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["LTECELLS"];
                               dal.updateLTECELLS(General.dt_CURR_FDDCELL);
                               break;

                           case "GSMCELL":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["GSMCELL"];
                               dal.updateGSMCELL(General.dt_CURR_FDDCELL);
                               break;

                           case "FDDCELL_Site":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["FDDCELL_Site"];
                               dal.updateFDDCELL_Site(General.dt_CURR_FDDCELL);
                               break;

                           case "CNA":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["CNA"];
                               dal.updateCNA(General.dt_CURR_FDDCELL);
                               break;

                           case "LTECELLS_Site":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["LTECELLS_Site"];
                               dal.updateLTECELLS_Site(General.dt_CURR_FDDCELL);
                               break;

                           case "ALLTECHNOLOGIESSITES":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["ALLTECHNOLOGIESSITES"];
                               dal.updateALLTECHNOLOGIESSITES(General.dt_CURR_FDDCELL);
                               break;

                           case "H_EU_UtranCell":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["H_EU_UtranCell"];
                               dal.updateUcells(General.dt_CURR_FDDCELL);
                               break;

                           case "gtransmitters":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["gtransmitters"];
                               dal.updategtransmitters(General.dt_CURR_FDDCELL);
                               break;

                           case "SIB1":
                               General.dt_CURR_FDDCELL = ds_QIS_Data.Tables["SIB1"];
                               dal.updateSIB(General.dt_CURR_FDDCELL);
                               break;

                       }

                   }


        }
    }
}
