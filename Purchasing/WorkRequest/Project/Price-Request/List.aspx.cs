﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.SysDataSetTableAdapters;
using Microsoft.Reporting.WebForms;
using System.Web.Security;
using Telerik.Web.UI;
using ManagementSystem.Helper;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using System.Diagnostics;

namespace ManagementSystem.Purchasing.WorkRequest.Project.Price_Request
{
  public partial class List : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    private readonly ReportParameter[] _myReportPeriodeParameters = new ReportParameter[6];
    protected IEnumerable<ReportParameter> MyParamPeriodeEnum;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          WRDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
      else if (!IsPostBack)
      {
        string[] rolesArray = Roles.GetRolesForUser();

        foreach (string roles in rolesArray)
        {
          if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing" || roles == "Direktur Purchasing" || roles == "Staff Purchasing" || roles == "PPIC Purchasing")
          {
            PnlContent2.Visible = true;
            PnlContent1.Visible = false;
          }
        }
      }
    }

    protected void GridReqNote_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string unitkerja = Session["UnitKerja"].ToString();
        string[] rolesArray = Roles.GetRolesForUser();

        Button btnKelola = gridDataItem.FindControl("BtnKelola") as Button; // edit, add item
        Button btnCheck = gridDataItem.FindControl("BtnCheck") as Button; //btn approve & verified
        Button btnBatal = gridDataItem.FindControl("BtnBatal") as Button;
        Button btnPrint = gridDataItem.FindControl("BtnPrint") as Button;

        string id = gridDataItem["id"].Text;
        wrTableAdapter.FillById(purDataSet.pur_wr, id);
        string status = purDataSet.pur_wr[0].status;
        

        foreach (string roles in rolesArray)
        {
          if (status == "WR1") // Baru ..
          {
            if (roles == "PPIC Purchasing")
            {
              btnKelola.Visible = false;
            }
            else if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing" || roles == "Staff Purchasing")
            {
              btnKelola.Visible = true;
            }
          }
          else if (status == "WR3") // Didaftarkan ..
          {
            if (roles == "PPIC Purchasing")
            {
              btnCheck.Visible = true;
            }
            else if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing")
            {
              btnCheck.Visible = true;
              btnKelola.Visible = true;
            }
            else if (roles == "Staff Purchasing")
            {
              btnKelola.Visible = true;
            }
          }
          else if (status == "WR2") // Didaftarkan (Pending) ...
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing" || roles == "Staff Purchasing")
            {
              btnKelola.Visible = true;
            }
          }
          else if (status == "WR5") // Diverifikasi
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing")
            {
              btnKelola.Visible = true;
              btnCheck.Visible = true;
            }
          }
          else if (status == "WR4") // Diverifikasi (Pending)
          {
            if (roles == "PPIC Purchasing")
            {
              btnCheck.Visible = true;
            }
            else if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing")
            {
              btnCheck.Visible = true;
              btnKelola.Visible = true;
            }
            else if (roles == "Staff Purchasing")
            {
              btnKelola.Visible = true;
            }
          }
          else if (status == "WR6") // Disetujui (Pending)
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing")
            {
              btnKelola.Visible = true;
              btnCheck.Visible = true;
            }
          }
          else if (status == "WR7") // Disetujui
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing")
            {
              btnBatal.Visible = true;
            }
            btnPrint.Visible = true;
          }
        }

        //
        Label requestor = (gridDataItem.FindControl("LblRequestor") as Label);
        string nama = gridDataItem["createdby"].Text;
        char[] splitchar = { ' ' };
        string[] strr = nama.Split(splitchar);
        if (strr.Length > 1)
        {
          requestor.Text = gridDataItem["createdby"].Text.Substring(0, gridDataItem["createdby"].Text.IndexOf(" "));
        }
        else
        {
          requestor.Text = nama;
        }

        //
        gridDataItem["status"].Text = PurchaseUtils.GetStatusJasa(status).ToUpper();

      }
    }

    protected void GridReqNote_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vwr01TableAdapter = new vpur_wr01TableAdapter();
      string[] rolesArray = Roles.GetRolesForUser();

      switch (e.CommandName)
      {
        case "RowClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            vwr01TableAdapter.FillById(purDataSet.vpur_wr01, gridDataItem["id"].Text);

            try
            {
              LblDetailNoWR.Text = purDataSet.vpur_wr01[0].id;
              LblDetailNoProject.Text = purDataSet.vpur_wr01[0].project_nomor;
              LblDetailLokasi.Text = purDataSet.vpur_wr01[0].lokasi_nama;
              LblDetailTglDibuat.Text = purDataSet.vpur_wr01[0].tglwr.ToShortDateString();
              LblDetailKategori.Text = purDataSet.vpur_wr01[0].kategori_nama;
              LblDetailNoReferensi.Text = purDataSet.vpur_wr01[0].reference;
              LblDetailStatus.Text = PurchaseUtils.GetStatusJasa(purDataSet.vpur_wr01[0].status);
              LblDetailDibuatOleh.Text = String.Concat(purDataSet.vpur_wr01[0].createdby + " (" + purDataSet.vpur_wr01[0].datecreated.ToString() + ")");

              string unitkerja = purDataSet.vpur_wr01[0].unitkerja;
              if (unitkerja == "SHIPYARD")
              {
                LblDetailUsable.Text = purDataSet.vpur_wr01[0].usable_nama;
                LblDetailScope.Text = purDataSet.vpur_wr01[0].scope_nama;
              }
              else
              {
                PnlScope.Visible = false;
                PnlUsable.Visible = false;
              }

            }
            catch (Exception ex)
            {
              var stackTrace = new StackTrace(ex, true);
              var frame = stackTrace.GetFrame(0);

              PnlMessageLblTitlebar.Text = "Oops!";
              PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
              PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>", frame.GetMethod(), "(", frame.GetFileLineNumber(), "):<br>", ex.Message);
              PnlMessageModalPopupExtender.Show();

              _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
            }
            PnlDetailModalPopupExtender.Show();
          }
          break;
        case "KelolaClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;
            var id = gridDataItem["id"].Text;
            ViewState["Id"] = id;

            var url = string.Concat("Kelola.aspx?pId=", ViewState["Id"].ToString());
            Response.Redirect(url);
          }
          break;
        case "CheckClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            string id = gridDataItem["id"].Text;
            PnlCheckTxtNoWR.Text = id;

            foreach (string roles in rolesArray)
            {
              if (roles == "PPIC Purchasing")
              {
                PnlCheckLblTitlebar.Text = "Permintaan Harga";
                PnlCheckBtnVerified.Visible = true;
                PnlCheckBtnApproved.Visible = false;
              }
              else if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur Purchasing")
              {
                PnlCheckLblTitlebar.Text = "Permintaan Harga";
                PnlCheckBtnVerified.Visible = false;
                PnlCheckBtnApproved.Visible = true;
              }
              PnlCheckModalPopupExtender.Show();
            }
          }
          break;
        case "BatalClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            string id = gridDataItem["id"].Text;

            PnlBatalSetujuLblNoWR.Text = id;
            PnlBatalSetujuModalPopupExtender.Show();
          }
          break;
        case "PrintClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            string id = gridDataItem["id"].Text;

            PnlPrintLblNoWR.Text = id;
            PnlPrintModalPopupExtender.Show();
          }
          break;
      }
    }

    protected void GridApproveReqJasa_ItemDataBound(object sender, GridItemEventArgs e)
    {
      foreach (GridDataItem item in GridApproveReqJasa.MasterTableView.Items)
      {
        if (item is GridDataItem)
        {
          GridDataItem current = item as GridDataItem;

          CheckBox checkBoxRow = item.FindControl("CheckBoxRow") as CheckBox;

          string status = item["status"].Text;
          string[] rolesArray = Roles.GetRolesForUser();

          foreach (string roles in rolesArray)
          {
            if ((roles == "PPIC Purchasing") && (status == "Diverifikasi PPIC" || status == "J3"))
            {
              checkBoxRow.Visible = false;
            }
            else if (status == "Disetujui" || status == "J7")
            {
              checkBoxRow.Visible = false;
            }
          }
          item["status"].Text = PurchaseUtils.GetStatusJasa(status);
        }
        
      }
      
    }

    protected void GridApproveReqJasa_ItemCreated(object sender, GridItemEventArgs e)
    {

    }

    protected void BtnApprove_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();

      string nowr = PnlCheckTxtNoWR.Text;

      foreach (GridDataItem item in GridApproveReqJasa.MasterTableView.Items)
      {
        if (item is GridDataItem)
        {
          GridDataItem dataItem = item as GridDataItem;
          CheckBox checkBoxRow = item.FindControl("CheckBoxRow") as CheckBox;

          if (checkBoxRow.Checked && checkBoxRow != null)
          {
            string idreqjasa = item["id"].Text;

            wrdetailTableAdapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(idreqjasa));
            purDataSet.pur_wrdetail[0].status = "J4";
            if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
            {
              CUtils.UpdateLog("pur_wrdetail", idreqjasa, Session["Username"].ToString(), "Update Status WR Detail 'Disetujui'");
            }
          }
        }
      }
      FunctApprovedWR(nowr);
      NotificationApprove();
      GridApproveReqJasa.DataBind();
      GridWR.DataBind();
    }

    private void FunctApprovedWR(string nowr)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();

      if (wrTableAdapter.FillById(purDataSet.pur_wr, nowr) > 0)
      {
        int countWR = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(nowr));
        int countApprovedWR = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J4", nowr));

        if (countWR == countApprovedWR)
        {
          purDataSet.pur_wr[0].status = "WR7"; // Disetujui
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update Status WR 'Disetujui'");
          }
        }
        else
        {
          purDataSet.pur_wr[0].status = "WR6"; // Disetujui (Pending)
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update Status WR 'Disetujui(Pending)'");
          }
        }
      }
    }


    private void NotificationApprove()
    {
      PnlMessageLblTitlebar.Text = "Konfirmasi";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Berhasil Disetujui.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void CheckBoxHeader_CheckedChanged(object sender, EventArgs e)
    {
      foreach (GridDataItem item in GridApproveReqJasa.MasterTableView.Items)
      {
        CheckBox chkbx = (CheckBox)item["CheckTemp"].FindControl("CheckBoxRow");
        chkbx.Checked = !chkbx.Checked;
        PnlCheckModalPopupExtender.Show();
      }

    }

    protected void GridApproveReqJasa_DataBound(object sender, EventArgs e)
    {
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");
      string daterequest1 = TxtRangeTgl1.Text == string.Empty ? "" : Convert.ToDateTime(TxtRangeTgl1.Text).ToString("yyyy-MM-dd");
      string daterequest2 = TxtRangeTgl2.Text == string.Empty ? "" : Convert.ToDateTime(TxtRangeTgl2.Text).ToString("yyyy-MM-dd");
      var unitkerja = Session["UnitKerja"].ToString();

      ViewState.Remove("SelectMethod");

      SearchByGeneral(txtKataKunci, daterequest1, daterequest2, unitkerja);

      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          SearchByDirektur(txtKataKunci, daterequest1, daterequest2);
        }
        else if (roles == "Purchasing Manufacturing")
        {
          SearchByManufacture(txtKataKunci, daterequest1, daterequest2);
        }
      }

      GridWR.DataBind();
    }

    private void SearchByManufacture(string txtKataKunci, string daterequest1, string daterequest2)
    {
      ViewState.Remove("SelectMethod");

      if (DListBerdasarkan.SelectedValue == "0") // No WR ...
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureId";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("id", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureId");
      }
      else if (DListBerdasarkan.SelectedValue == "1") // No Reference
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureReference";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("reference", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureReference");
      }
      else if (DListBerdasarkan.SelectedValue == "2") // Status
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureStatus";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("status", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureStatus");
      }
      else if (DListBerdasarkan.SelectedValue == "3") // Tanggal
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureTglWr";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("tglwr1", daterequest1);
        WRDataSource.SelectParameters.Add("tglwr2", daterequest2);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureTglWr");
      }
      GridWR.DataBind();
    }

    private void SearchByDirektur(string txtKataKunci, string daterequest1, string daterequest2)
    {

      ViewState.Remove("SelectMethod");

      if (DListBerdasarkan.SelectedValue == "0")
      {
        WRDataSource.SelectMethod = "GetDataByTypeId";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("id", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeId");
      }
      else if (DListBerdasarkan.SelectedValue == "1")
      {
        WRDataSource.SelectMethod = "GetDataByTypeReference";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("reference", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeReference");
      }
      else if (DListBerdasarkan.SelectedValue == "2")
      {
        WRDataSource.SelectMethod = "GetDataByTypeStatus";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("status", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (DListBerdasarkan.SelectedValue == "3")
      {
        WRDataSource.SelectMethod = "GetDataByTypeTglWr";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("tglwr1", daterequest1);
        WRDataSource.SelectParameters.Add("tglwr2", daterequest2);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeTglWr");
      }
      GridWR.DataBind();

    }

    private void SearchByGeneral(string txtKataKunci, string daterequest1, string daterequest2, string unit)
    {
      ViewState.Remove("SelectMethod");

      if (DListBerdasarkan.SelectedValue == "0") // No WR ...
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitKerjaId";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("unitkerja", unit);
        WRDataSource.SelectParameters.Add("id", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaId");
      }
      else if (DListBerdasarkan.SelectedValue == "1") // Refrence ..
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitKerjaReference";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("unitkerja", unit);
        WRDataSource.SelectParameters.Add("reference", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaReference");
      }
      else if (DListBerdasarkan.SelectedValue == "2") // Status ...
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitKerjaStatus";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("unitkerja", unit);
        WRDataSource.SelectParameters.Add("status", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaStatus");
      }
      else if (DListBerdasarkan.SelectedValue == "3")
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitKerjaTglWr";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("tglwr1", daterequest1);
        WRDataSource.SelectParameters.Add("tglwr2", daterequest2);
        WRDataSource.SelectParameters.Add("unitkerja", unit);
        WRDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaTglWr");
      }
      GridWR.DataBind();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      TxtRangeTgl1.Text = string.Empty;
      TxtRangeTgl2.Text = string.Empty;
      DListBerdasarkan.SelectedIndex = 0;
      PnlKataKunci.Visible = true;
      PnlTanggal.Visible = false;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();

      CancelGeneral();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          CancelDirektur();
        }
        else if (roles == "Purchasing Manufacturing")
        {
          CancelManufacture();
        }
      }

      GridWR.CurrentPageIndex = 0;
      GridWR.DataBind();
    }

    private void CancelDirektur()
    {
      ViewState.Add("SelectMethod", "GetDataByType");

      WRDataSource.SelectMethod = "GetDataByType";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("type", "P");

    }

    private void CancelManufacture()
    {
      ViewState.Add("SelectMethod", "GetDataByTypeUnitManufacture");

      WRDataSource.SelectMethod = "GetDataByTypeUnitManufacture";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("type", "P");
    }

    private void CancelGeneral()
    {
      ViewState.Add("SelectMethod", "GetDataByTypeUnitKerja");

      WRDataSource.SelectMethod = "GetDataByTypeUnitKerja";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      WRDataSource.SelectParameters.Add("type", "P");
    }

    protected void PnlCheckBtnVerified_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableadapter = new pur_wrdetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();

      string nowr = PnlCheckTxtNoWR.Text;

      foreach (GridDataItem item in GridApproveReqJasa.MasterTableView.Items)
      {
        if (item is GridDataItem)
        {
          GridDataItem dataItem = item as GridDataItem;
          CheckBox checkBoxRow = item.FindControl("CheckBoxRow") as CheckBox;

          string idreqjasa = item["id"].Text;

          if (checkBoxRow.Checked && checkBoxRow != null)
          {
            wrdetailTableadapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(idreqjasa));
            purDataSet.pur_wrdetail[0].status = "J3";
            if (wrdetailTableadapter.Update(purDataSet.pur_wrdetail) > 0)
            {
              CUtils.UpdateLog("pur_wrdetail", idreqjasa, Session["Username"].ToString(), "Update Status WR Detail 'Diverifikasi'");
            }
          }

        }
      }
      FuctVerifiedWR(nowr);
      GridWR.DataBind();
      GridRequestJasa.DataBind();
      GridApproveReqJasa.DataBind();

      //Notif Verified
      PnlMessageLblTitlebar.Text = "Konfirmasi";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Verifikasi Berhasil.";
      PnlMessageModalPopupExtender.Show();
    }

    private void FuctVerifiedWR(string nowr)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableadapter = new pur_wrdetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();

      if (wrTableAdapter.FillById(purDataSet.pur_wr, nowr) > 0)
      {
        int countWR = Convert.ToInt32(wrdetailTableadapter.ScalarGetCountByWrId(nowr));
        int countPVerWR = Convert.ToInt32(wrdetailTableadapter.ScalarGetCountByStatusWrId("J3", nowr));

        if (countWR == countPVerWR)
        {
          purDataSet.pur_wr[0].status = "WR5"; // Diverifikasi
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update Status WR 'Diverifikasi'");
          }
        }
        else
        {
          purDataSet.pur_wr[0].status = "WR4";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update Status WR 'Diverifikasi (Pending)'");
          }
        }
      }
    }

    protected void DListBerdasarkan_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DListBerdasarkan.SelectedValue == "2")
      {
        PnlTanggal.Visible = true;
        PnlKataKunci.Visible = false;
      }
      else
      {
        PnlTanggal.Visible = false;
        PnlKataKunci.Visible = true;
      }
    }

    protected void GridRequestJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlDetailModalPopupExtender.Show();
    }

    protected void GridRequestJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlDetailModalPopupExtender.Show();
    }

    protected void GridApproveReqJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlCheckModalPopupExtender.Show();
    }

    protected void GridApproveReqJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlCheckModalPopupExtender.Show();
    }

    protected void PnlPrintBtnPrint_Click(object sender, EventArgs e)
    {
      var reportDataSet = new ReportsDataSet();
      var purDataSet = new PurDataSet();
      var vwrdetail01TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_wrdetail01TableAdapter();
      var vwr01TableAdapter = new vpur_wr01TableAdapter();

      string pRequestor = string.Concat(Session["FullName"].ToString() + " (" + DateTime.Now + ")");
      string idReqNote = PnlPrintLblNoWR.Text;
      
      vwr01TableAdapter.FillById(purDataSet.vpur_wr01, idReqNote);
      string pUnitKerja = purDataSet.vpur_wr01[0].unitkerja;
      string pReference = purDataSet.vpur_wr01[0].reference;
      string pNoWR = purDataSet.vpur_wr01[0].id;
      string pTglWR = purDataSet.vpur_wr01[0].tglwr.ToShortDateString();
      string pCreatedBy = String.Concat(purDataSet.vpur_wr01[0].createdby + " (" + purDataSet.vpur_wr01[0].datecreated.ToString() + ")");

      var reportViewer = new ReportViewer();
      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptProjectWR.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline;

      vwrdetail01TableAdapter.FillByWrId(reportDataSet.vpur_wrdetail01, idReqNote);
      _myReportPeriodeParameters[0] = new ReportParameter("pRequestor", pRequestor);
      _myReportPeriodeParameters[1] = new ReportParameter("pUnitKerja", pUnitKerja);
      _myReportPeriodeParameters[2] = new ReportParameter("pReference", pReference);
      _myReportPeriodeParameters[3] = new ReportParameter("pNoWR", pNoWR);
      _myReportPeriodeParameters[4] = new ReportParameter("pTglWR", pTglWR);
      _myReportPeriodeParameters[5] = new ReportParameter("pCreatedBy", pCreatedBy);

      MyParamPeriodeEnum = _myReportPeriodeParameters;

      var rptViewerProjectWrPrintDataSource = new ReportDataSource();
      rptViewerProjectWrPrintDataSource.Name = "vpur_wrdetail01";
      rptViewerProjectWrPrintDataSource.Value = reportDataSet.vpur_wrdetail01;

      reportViewer.LocalReport.SetParameters(MyParamPeriodeEnum);
      reportViewer.LocalReport.DataSources.Add(rptViewerProjectWrPrintDataSource);

      //Code for download direct pdf

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=wrproject_", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    protected void PnlBatalSetujuBtnYa_Click(object sender, EventArgs e)
    {
      PnlInputNoteLblNomer.Text = PnlBatalSetujuLblNoWR.Text;
      PnlInputNoteModalPopupExtender.Show();
    }

    private void NotificationApproveReject()
    {
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Berhasil Dibatalkan.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void PnlInputNoteBtnSubmit_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var rejectTableAdapter = new pur_rejectTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var rejectRow = purDataSet.pur_reject.Newpur_rejectRow();

      //insert in rejectrequest
      rejectRow.id = PnlInputNoteLblNomer.Text;
      rejectRow.datecreated = DateTime.Now;
      rejectRow.jenisrequest = "WR";
      rejectRow.ket = PnlInputNoteTxtKeterangan.Text;
      rejectRow.createdby = Session["FullName"].ToString();

      purDataSet.pur_reject.Addpur_rejectRow(rejectRow);
      if ((rejectTableAdapter.Update(purDataSet.pur_reject)) > 0)
      {
        //update status requestnotewr
        wrTableAdapter.FillById(purDataSet.pur_wr, PnlInputNoteLblNomer.Text);
        purDataSet.pur_wr[0].status = "WR0";
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", PnlInputNoteLblNomer.Text, Session["Username"].ToString(), "Update Status WR 'Dibatalkan'");
        }
        //update status requestjasa
        if (wrdetailTableAdapter.UpdateQueryStatusDibatalkanByWrId(PnlInputNoteLblNomer.Text) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", PnlInputNoteLblNomer.Text, Session["Username"].ToString(), "Update Status WR Detail 'Dibatalkan'");
        }
        NotificationApproveReject();
      }

      GridWR.DataBind();
    }

    protected void ReqNoteDataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray)
      {
        if (roles == "Purchasing Manufacturing")
        {
          WRDataSource.SelectMethod = "GetDataByTypeUnitManufacture";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("type", "P");
        }
        else if (roles == "Direktur Purchasing")
        {
          WRDataSource.SelectMethod = "GetDataByType";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("type", "P");
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing D.E.M" || roles == "Purchasing M.C")
        {
          WRDataSource.SelectMethod = "GetDataByTypeUnitKerja";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
          WRDataSource.SelectParameters.Add("type", "P");
        }
      }
    }

    protected void GridRequestJasa_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusJasa(status);
      }
    }
  }
}
