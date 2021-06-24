using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.SysDataSetTableAdapters;
using Microsoft.Reporting.WebForms;
using System.Web.Security;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;
using System.Diagnostics;
using System.Drawing;
using ManagementSystem.Helper;

namespace ManagementSystem.Purchasing.WorkRequest.Project.Work_Request
{
  public partial class List : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    private readonly ReportParameter[] _myReportPeriodeParameters = new ReportParameter[6];
    protected IEnumerable<ReportParameter> MyParamPeriodeEnum;

    protected void Page_Load(object sender, EventArgs e)
    {
      string unitkerja = Session["UnitKerja"].ToString();

      if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          WRDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
      else if (!IsPostBack)
      {
        if (unitkerja == "T.H.O") { BtnAddWR.Visible = false; }
      }
    }

    protected void BtnAddWR_Click(object sender, EventArgs e)
    {
      Response.Redirect("Input.aspx");
    }

    protected void GridWR_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vwr01TableAdapter = new vpur_wr01TableAdapter();

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
              LblDetailScope.Text = purDataSet.vpur_wr01[0].scope_nama;
              LblDetailUsability.Text = purDataSet.vpur_wr01[0].usable_nama;
              LblDetailKategori.Text = purDataSet.vpur_wr01[0].kategori_nama;
              LblDetailNoReferensi.Text = purDataSet.vpur_wr01[0].reference;
              LblDetailStatus.Text = PurchaseUtils.GetStatusJasa(purDataSet.vpur_wr01[0].status);
              LblDetailDibuatOleh.Text = String.Concat(purDataSet.vpur_wr01[0].createdby + " (" + purDataSet.vpur_wr01[0].tglwr.ToString() + ")");
              LblDetailUnitKerja.Text = purDataSet.vpur_wr01[0].unitkerja;

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
      }
    }


    protected void GridWRDetail_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string status = item["status"].Text;
        item["status"].Text = PurchaseUtils.GetStatusJasa(status);
      }
    }

    protected void GridWR_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string status = item["status"].Text;
        string createdby = item["createdby"].Text;

        //////////////// requestor /////////////////////
        Label requestor = (item.FindControl("LblRequestor") as Label);
        string nama = item["createdby"].Text;
        char[] splitchar = { ' ' };
        string[] strr = nama.Split(splitchar);
        if (strr.Length > 1)
        {
          requestor.Text = item["createdby"].Text.Substring(0, item["createdby"].Text.IndexOf(" "));
        }
        else
        {
          requestor.Text = nama;
        }

        ///////////////// status ///////////////////////
        if (status == "Baru" || status == "WR1")
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Orange;
        }
        item["status"].Text = PurchaseUtils.GetStatusJasa(status).ToUpper();
      }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");
      string daterequest1 = TxtRangeTgl1.Text == string.Empty ? "" : Convert.ToDateTime(TxtRangeTgl1.Text).ToString("yyyy-MM-dd");
      string daterequest2 = TxtRangeTgl2.Text == string.Empty ? "" : Convert.ToDateTime(TxtRangeTgl2.Text).ToString("yyyy-MM-dd");
      string unit = Session["UnitKerja"].ToString();

      SearchByGeneral(txtKataKunci, daterequest1, daterequest2, unit);

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

    protected void GridWRDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlDetailModalPopupExtender.Show();
    }

    protected void GridWRDetail_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlDetailModalPopupExtender.Show();
    }

    protected void DListBerdasarkan_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DListBerdasarkan.SelectedValue == "3")
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

    protected void WRDataSource_Init(object sender, EventArgs e)
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

   
  }
}
