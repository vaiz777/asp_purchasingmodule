using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using TmsBackDataController.SysDataSetTableAdapters;
using Telerik.Web.UI;
using ManagementSystem.Helper;
using System.Diagnostics;
using Microsoft.Reporting.WebForms;
using ManagementSystem.ReportsDataSetTableAdapters;
using System.Web.Security;
using System.Drawing;
using TmsBackDataController.PurDataSetTableAdapters;


namespace ManagementSystem.Purchase.Material_Request.NonProject.Barang_Request
{
  public partial class List : System.Web.UI.Page
  {
    private readonly ReportParameter[] _myReportParameters = new ReportParameter[6];
    protected IEnumerable<ReportParameter> MyParamEnum;
    public static string unitkerja, statusB;
    public static string dateNoteCreated, dateDeadline, keterangan, reference, requestor;

    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      // Unit Kerja ...
      if (getUnitKerja == "S.M") { LblJudul.Text = "Daftar Material Request (Non Project)"; }
      else { LblJudul.Text = "Daftar Material Request"; }
      if (getUnitKerja == "T.H.O") { BtnAdd.Visible = false; }

      // IsPostBack ...
      if (IsPostBack)
      {

      }
    }
      
    protected void BtnSearchClick(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");

      string status = DlistStatus.SelectedValue;
      string kategori = DlistKategoriPencarian.SelectedValue;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          SearchGeneral(kategori, txtKataKunci);
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M")
        {
          SearchByUnitKerja(kategori, txtKataKunci);
        }
        else if (roles == "Purchasing Manufacturing") {
          SearchByUnitManufacture(kategori, txtKataKunci);
        }
      }

      GridMR.CurrentPageIndex = 0;
      GridMR.DataBind();
    }


    private void SearchGeneral(string kategori, string txtKataKunci)
    {
      if (kategori == "0")
      {
        MRDataSource.SelectMethod = "GetDataByTypeId";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("type", "NP");
        MRDataSource.SelectParameters.Add("id", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeId");
      }
      else if (kategori == "1")
      {
        MRDataSource.SelectMethod = "GetDataByTypeNomorProject";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("type", "NP");
        MRDataSource.SelectParameters.Add("noproject", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeNomorProject");
      }
      else if (kategori == "2")
      {
        MRDataSource.SelectMethod = "GetDataByTypeStatus";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("type", "NP");
        MRDataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (kategori == "3")
      {
        DateTime datereq1 = Convert.ToDateTime(TxtTanggalStart.Text);
        DateTime datereq2 = Convert.ToDateTime(TxtTanggalEnd.Text);

        MRDataSource.SelectMethod = "GetDataByTypeTanggal";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("type", "NP");
        MRDataSource.SelectParameters.Add("tanggal", datereq1.ToString("yyyy-MM-dd"));
        MRDataSource.SelectParameters.Add("tanggal", datereq2.ToString("yyyy-MM-dd"));

        ViewState.Add("SelectMethod", "GetDataByTypeTanggal");
      }
    }

    private void SearchByUnitKerja(string kategori, string txtKataKunci)
    {
      if (kategori == "0")
      {
        MRDataSource.SelectMethod = "GetDataByTypeUnitKerjaId";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MRDataSource.SelectParameters.Add("id", txtKataKunci);
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaId");
      }
      else if (kategori == "1")
      {
        MRDataSource.SelectMethod = "GetDataByTypeUnitKerjaProjectNomor";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MRDataSource.SelectParameters.Add("noproject", txtKataKunci);
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaProjectNomor");
      }
      else if (kategori == "2")
      {
        MRDataSource.SelectMethod = "GetDataByTypeUnitKerjaStatus";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MRDataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaStatus");
      }
      else if (kategori == "3")
      {
        DateTime datereq1 = Convert.ToDateTime(TxtTanggalStart.Text);
        DateTime datereq2 = Convert.ToDateTime(TxtTanggalEnd.Text);

        MRDataSource.SelectMethod = "GetDataByTypeUnitKerjaTanggal";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MRDataSource.SelectParameters.Add("tanggal1", datereq1.ToString("yyyy-MM-dd"));
        MRDataSource.SelectParameters.Add("tanggal2", datereq2.ToString("yyyy-MM-dd"));
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaTanggal");
      }
    }

    private void SearchByUnitManufacture(string kategori, string txtKataKunci)
    {
      if (kategori == "0")
      {
        MRDataSource.SelectMethod = "GetDataManufactureByTypeId";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("id", txtKataKunci);
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataManufactureByTypeId");
      }
      else if (kategori == "1")
      {
        MRDataSource.SelectMethod = "GetDataManufactureByTypeNoProject";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("noproject", txtKataKunci);
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataManufactureByTypeNoProject");
      }
      else if (kategori == "2")
      {
        MRDataSource.SelectMethod = "GetDataManufactureByTypeStatus";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataManufactureByTypeStatus");
      }
      else if (kategori == "3")
      {
        DateTime datereq1 = Convert.ToDateTime(TxtTanggalStart.Text);
        DateTime datereq2 = Convert.ToDateTime(TxtTanggalEnd.Text);

        MRDataSource.SelectMethod = "GetDataManufactureByTypeTanggal";
        MRDataSource.SelectParameters.Clear();
        MRDataSource.SelectParameters.Add("tanggal1", datereq1.ToString("yyyy-MM-dd"));
        MRDataSource.SelectParameters.Add("tanggal2", datereq2.ToString("yyyy-MM-dd"));
        MRDataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataManufactureByTypeTanggal");
      }
    }     

    protected void BtnCancelClick(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      DlistKategoriPencarian.SelectedIndex = 0;
      TxtTanggalStart.Text = string.Empty;
      TxtTanggalEnd.Text = string.Empty;
      DlistStatus.SelectedIndex = 0;

      PnlKataKunci.Visible = true;
      PnlTanggal.Visible = false;
      PnlStatus.Visible = false;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          MRDataSource.SelectMethod = "GetDataByType";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("type", "NP");

          ViewState.Add("SelectMethod", "GetDataProject");
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M")
        {
          MRDataSource.SelectMethod = "GetDataByUnitKerjaType";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
          MRDataSource.SelectParameters.Add("type", "NP");

          ViewState.Add("SelectMethod", "GetDataByUnitKerjaType");
        }
        else if (roles == "Purchasing Manufacturing")
        {
          MRDataSource.SelectMethod = "GetDataByUnitManufactureByType";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("type", "NP");

          ViewState.Add("SelectMethod", "GetDataByUnitManufactureByType");
        }
      }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
      var url = string.Concat("Input.aspx");
      Response.Redirect(url);
    }      

    protected void GridMR_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var purDataSet = new PurDataSet();
        var vmr01TableAdapter = new vpur_mr01TableAdapter();

        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        vmr01TableAdapter.FillById(purDataSet.vpur_mr01, gridDataItem["id"].Text);

        LblNoMR.Text = purDataSet.vpur_mr01[0].id;
        LblTglMR.Text = purDataSet.vpur_mr01[0].tanggal.ToShortDateString();
        LblReference.Text = purDataSet.vpur_mr01[0].reference;
        LblDepartment.Text = purDataSet.vpur_mr01[0].departement;
        LblLokasi.Text = purDataSet.vpur_mr01[0].lokasi_nama;        
        LblCreatedby.Text = String.Concat(purDataSet.vpur_mr01[0].createdby + " (" + purDataSet.vpur_mr01[0].datecreated + " ) ");
        if (getUnitKerja == "SHIPYARD")
        {
          LblKategori.Text = purDataSet.vpur_mr01[0].kategori_nama;
          LblScope.Text = purDataSet.vpur_mr01[0].scope_nama;
          LblUsable.Text = purDataSet.vpur_mr01[0].usable_nama;
          PnlSM.Visible = true;
        }
        else {
          PnlSM.Visible = false;
        }

        PnlViewBarangModalPopupExtender.Show();

      }

    }

    protected void GridMR_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;
        string status = item["status"].Text;

        ////Variabel Color
        if (status == "MR1")
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Blue;
          item["status"].Text = "MR BARU";
        }
        item["status"].Text = PurchaseUtils.GetStatusBarang(status);

      }
    }

    protected void GridDataBarang_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string kdStatus = item["status"].Text;

        string status = PurchaseUtils.GetStatusBarang(kdStatus);

        item["status"].Text = status;
      }
    }

    public string getUsername
    {
      get
      {
        string result = Session["UserName"].ToString();
        return result;
      }
    }
     
    public string getUnitKerja
    {
      get
      {
        string result = Session["UnitKerja"].ToString();
        return result;
      }
    }

    protected void DlistKategoriPencarian_SelectedIndexChanged(object sender, EventArgs e)
    {
      string kategori = DlistKategoriPencarian.SelectedValue;
      if (kategori == "0" || kategori == "1")
      {
        PnlKataKunci.Visible = true;
        PnlStatus.Visible = false;
        PnlTanggal.Visible = false;
      }
      else if (kategori == "2")
      {
        PnlKataKunci.Visible = false;
        PnlStatus.Visible = false;
        PnlTanggal.Visible = true;
      }
      else if (kategori == "3")
      {
        PnlKataKunci.Visible = false;
        PnlStatus.Visible = true;
        PnlTanggal.Visible = false;
      }
    }

    protected void ListMRDataSource_Init(object sender, EventArgs e)
    {
      string kategori = DlistKategoriPencarian.SelectedValue;
      if (kategori == "0" || kategori == "1")
      {
        PnlKataKunci.Visible = true;
        PnlStatus.Visible = false;
        PnlTanggal.Visible = false;
      }
      else if (kategori == "2")
      {
        PnlKataKunci.Visible = false;
        PnlStatus.Visible = false;
        PnlTanggal.Visible = true;
      }
      else if (kategori == "3")
      {
        PnlKataKunci.Visible = false;
        PnlStatus.Visible = true;
        PnlTanggal.Visible = false;
      }
    }
  }
}
