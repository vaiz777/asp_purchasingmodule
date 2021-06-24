using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.SysDataSetTableAdapters;
using Microsoft.Reporting.WebForms;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;
using ManagementSystem.Helper;
using System.Web.Security;
using System.Diagnostics;
using System.Drawing;

namespace ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Request
{
  public partial class List : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    private readonly ReportParameter[] _myReportPeriodeParameters = new ReportParameter[7];
    protected IEnumerable<ReportParameter> MyParamPeriodeEnum;

    protected void Page_Load(object sender, EventArgs e)
    {
      string unitkerja = Session["UnitKerja"].ToString();
      if (unitkerja == "SHIPYARD") { LblTitle.Text = "Daftar Work Request (Non Project)"; }
      else { LblTitle.Text = "Daftar Work Request"; }

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

    protected void GridRequestNote_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
              LblDetailTglWR.Text = purDataSet.vpur_wr01[0].tglwr.ToShortDateString();
              LblDetailLokasi.Text = purDataSet.vpur_wr01[0].Islokasi_namaNull() ? "" : purDataSet.vpur_wr01[0].lokasi_nama;
              LblDetailNoReferensi.Text = purDataSet.vpur_wr01[0].IsreferenceNull() ? "" : purDataSet.vpur_wr01[0].reference;
              LblDetailDepartment.Text = purDataSet.vpur_wr01[0].departement;
              LblDetailKategori.Text = purDataSet.vpur_wr01[0].Isusable_namaNull() ? "" : purDataSet.vpur_wr01[0].usable_nama;
              LblDetailScope.Text = purDataSet.vpur_wr01[0].Isscope_namaNull() ? "" : purDataSet.vpur_wr01[0].scope_nama;
              LblDetailStatus.Text = PurchaseUtils.GetStatusJasa(purDataSet.vpur_wr01[0].status);
              LblDetailDibuatOleh.Text = String.Concat(purDataSet.vpur_wr01[0].createdby + " (" + purDataSet.vpur_wr01[0].datecreated + ")");

              //if unit SM
              string[] rolesArray = Roles.GetRolesForUser();

              string unitkerja = purDataSet.vpur_wr01[0].unitkerja;
              LblDetailUnitKerja.Text=unitkerja;
              if (unitkerja == "SHIPYARD") { PnlScope.Visible = true; PnlUsable.Visible = true; }
              else { PnlUsable.Visible = false; PnlScope.Visible = false; }

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

    protected void GridRequestJasa_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;
        
        string unitkerja = LblDetailUnitKerja.Text;

        if (unitkerja != "SHIPYARD")
        {
          (GridRequestJasa.MasterTableView.GetColumn("jmlhari") as GridBoundColumn).Display = false;
          (GridRequestJasa.MasterTableView.GetColumn("jmlorang") as GridBoundColumn).Display = false;
        }

        string status = item["status"].Text;
        item["status"].Text = PurchaseUtils.GetStatusJasa(status);
      }
    }

    protected void GridRequestJasa_ItemCreated(object sender, GridItemEventArgs e) { }

    protected void GridRequestNote_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string unitkerja = item["unitkerja"].ToString();

        string status = item["status"].Text;
        string createdby = item["createdby"].Text;

        //
        if (status == "Baru" || status == "WR1")
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Orange;
        }
        item["status"].Text = PurchaseUtils.GetStatusJasa(status).ToUpper();

        //
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
      }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%" + TxtKataKunci.Text + "%");
      string daterequest1 = TxtRangeTgl1.Text == string.Empty ? "" : Convert.ToDateTime(TxtRangeTgl1.Text).ToString("yyyy-MM-dd");
      string daterequest2 = TxtRangeTgl2.Text == string.Empty ? "" : Convert.ToDateTime(TxtRangeTgl2.Text).ToString("yyyy-MM-dd");
      string unitkerja = Session["UnitKerja"].ToString();

      string[] rolesArray = Roles.GetRolesForUser();

      SearchGeneral(txtKataKunci, daterequest1, daterequest2, unitkerja);

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          SearchDirektur(txtKataKunci, daterequest1, daterequest2);
        }
        else if (roles == "Purchasing Manufacturing")
        {
          SearchManufacturing(txtKataKunci, daterequest1, daterequest2);
        }
      }
    }

    protected void SearchDirektur(string txtKataKunci, string daterequest1, string daterequest2)
    {
      ViewState.Remove("SelectMethod");

      if (DlistBerdasarkan.SelectedValue == "0") // NO WR
      {
        WRDataSource.SelectMethod = "GetDataByTypeId";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("id", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeId");
      }
      else if (DlistBerdasarkan.SelectedValue == "1")
      {
        WRDataSource.SelectMethod = "GetDataByTypeReference";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("reference", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeReference");
      }
      else if (DlistBerdasarkan.SelectedValue == "2")
      {
        WRDataSource.SelectMethod = "GetDataByTypeStatus";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("status", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (DlistBerdasarkan.SelectedValue == "3")
      {
        WRDataSource.SelectMethod = "GetDataByTypeTglWr";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("tglwr1", daterequest1);
        WRDataSource.SelectParameters.Add("tglwr2", daterequest2);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeTglWr");
      }
      GridRequestNote.DataBind();
    }

    protected void SearchManufacturing(string txtKataKunci, string daterequest1, string daterequest2)
    {
      ViewState.Remove("SelectMethod");

      if (DlistBerdasarkan.SelectedValue == "0") // No WR ...
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureId";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("id", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureId");
      }
      else if (DlistBerdasarkan.SelectedValue == "1") // No Reference
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureReference";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("reference", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureReference");
      }
      else if (DlistBerdasarkan.SelectedValue == "2") // Status
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureStatus";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("status", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureStatus");
      }
      else if (DlistBerdasarkan.SelectedValue == "3") // Tanggal
      {
        WRDataSource.SelectMethod = "GetDataByTypeUnitManufactureTglWr";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("tglwr1", daterequest1);
        WRDataSource.SelectParameters.Add("tglwr2", daterequest2);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureTglWr");
      }
      GridRequestNote.DataBind();
    }

    protected void SearchGeneral(string txtKataKunci, string daterequest1, string daterequest2, string unitkerja)
    {
      ViewState.Remove("SelectMethod");

      if (DlistBerdasarkan.SelectedValue == "0")
      {
        WRDataSource.SelectMethod = "GetDataByTypeId";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("id", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeId");
      }
      else if (DlistBerdasarkan.SelectedValue == "1")
      {
        WRDataSource.SelectMethod = "GetDataByTypeReference";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("reference", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeReference");
      }
      else if (DlistBerdasarkan.SelectedValue == "2")
      {
        WRDataSource.SelectMethod = "GetDataByTypeStatus";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("status", txtKataKunci);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (DlistBerdasarkan.SelectedValue == "3")
      {
        WRDataSource.SelectMethod = "GetDataByTypeTglWr";
        WRDataSource.SelectParameters.Clear();
        WRDataSource.SelectParameters.Add("tglwr1", daterequest1);
        WRDataSource.SelectParameters.Add("tglwr2", daterequest2);
        WRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeTglWr");
      }
      GridRequestNote.DataBind();
      GridRequestNote.CurrentPageIndex = 0;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      PnlRangeTgl.Visible = false;
      PnlKataKunci.Visible = true;
      TxtRangeTgl1.Text = string.Empty;
      TxtRangeTgl2.Text = string.Empty;
      DlistBerdasarkan.SelectedIndex = 0;

      ViewState.Remove("SelectMethod");

      CancelGeneral();

      string[] rolesArray = Roles.GetRolesForUser();

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

      GridRequestNote.CurrentPageIndex = 0;
      GridRequestNote.DataBind();
    }

    private void CancelDirektur()
    {
      ViewState.Add("SelectMethod", "GetDataByType");

      WRDataSource.SelectMethod = "GetDataByType";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("type", "NP");

    }

    private void CancelManufacture()
    {
      ViewState.Add("SelectMethod", "GetDataByTypeUnitManufacture");

      WRDataSource.SelectMethod = "GetDataByTypeUnitManufacture";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("type", "NP");
    }

    private void CancelGeneral()
    {
      ViewState.Add("SelectMethod", "GetDataByTypeUnitKerja");

      WRDataSource.SelectMethod = "GetDataByTypeUnitKerja";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      WRDataSource.SelectParameters.Add("type", "NP");
    }

    protected void GridRequestJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlDetailModalPopupExtender.Show();
    }

    protected void GridRequestJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlDetailModalPopupExtender.Show();
    }

    protected void DlistBerdasarkan_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DlistBerdasarkan.SelectedValue == "2")
      {
        PnlKataKunci.Visible = false;
        PnlRangeTgl.Visible = true;
      }
      else
      {
        PnlKataKunci.Visible = true;
        PnlRangeTgl.Visible = false;
      }
    }

    protected void RequestNoteDataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray)
      {
        if (roles == "Purchasing Manufacturing")
        {
          WRDataSource.SelectMethod = "GetDataByTypeUnitManufacture";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("type", "NP");
        }
        else if (roles == "Direktur Purchasing")
        {
          WRDataSource.SelectMethod = "GetDataByType";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("type", "NP");
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing D.E.M" || roles == "Purchasing M.C")
        {
          WRDataSource.SelectMethod = "GetDataByTypeUnitKerja";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
          WRDataSource.SelectParameters.Add("type", "NP");
        }
      }
    }
  }
}
