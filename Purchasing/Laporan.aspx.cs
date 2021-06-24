using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using Telerik.Web.UI;
using Microsoft.Reporting.WebForms;
using TmsBackDataController.MasterDataSetTableAdapters;
using TmsBackDataController.PurDataSetTableAdapters;
using System.Web.Security;
using vmaster_barang01TableAdapter = TmsBackDataController.PurDataSetTableAdapters.vmaster_barang01TableAdapter;

namespace ManagementSystem.Purchasing
{
  public partial class Laporan : System.Web.UI.Page
  {
    private readonly ReportParameter[] _myReportParameters = new ReportParameter[4];
    protected IEnumerable<ReportParameter> MyParamEnum;

    private readonly ReportParameter[] _myReportParameters01 = new ReportParameter[3];
    protected IEnumerable<ReportParameter> MyParamEnum01;

    protected void Page_Load(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Staff Purchasing")
        {
          PnlLaporan.Visible = true;
          PnlWarning.Visible = false;
        }
      }

      if (IsPostBack) 
      {
        if (ViewState.Count > 1) 
        {
          SupplierDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          MasterBarangDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          MasterJasaDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          DepartementDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          ProjectDataSource.SelectMethod = ViewState["SelectMethod"].ToString();

        }
      }
    }

    protected void PnlBarangDlist1_SelectedIndexChanged(object sender, EventArgs e)
    {
      string kategori = PnlBarangDlist1.SelectedValue;
      if (kategori == "1" || kategori == "7" || kategori == "8" || kategori == "10") //Tgl
      {
        PnlBarangPnlTgl.Visible = true;
        PnlBarangPnlNama.Visible = false;
      }
      else 
      {
        PnlBarangPnlTgl.Visible = true;
        PnlBarangPnlNama.Visible = true;
      }
    }

    protected void PnlBarangBtnBrowse_Click(object sender, EventArgs e)
    {
      string kategori = PnlBarangDlist1.SelectedValue;
      if (kategori == "2" || kategori == "8") //brg
      {
        PnlListBarangModalPopupExtender.Show();
      }
      else if (kategori == "3") //project
      {
        PnlBrowseMasterProjectModalPopupExtender.Show();
      }
      else if (kategori == "4" ) //departement
      {
        PnlBrowseDeptModalPopupExtender.Show();
      }
      else if (kategori == "5" || kategori == "7")
      {
        PnlBrowseSupplierModalPopupExtender.Show();
      }
      else if(kategori == "6" || kategori == "10"){
        PnlBrowseMasterJasaModalPopupExtender.Show();
      }
    }

    #region Function Panel

    protected void GridListBarang_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var purDataSet = new PurDataSet();
        var masterbarang01TableAdapter = new vmaster_barang01TableAdapter();

        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        masterbarang01TableAdapter.FillByKode(purDataSet.vmaster_barang01, gridDataItem["kode"].Text);

        PnlBarangKode.Value = purDataSet.vmaster_barang01[0].kode;
        PnlBarangTxtNama.Text = purDataSet.vmaster_barang01[0].nama;

        PnlListBarangModalPopupExtender.Hide();
      }
    }

    protected void BtnCariDataBarang_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");
      string pilihan = DlistJenisBarang.SelectedValue;

      ViewState.Remove("SelectMethod");

      if (pilihan == "0") // Kode Barang..
      {
        MasterBarangDataSource.SelectMethod = "GetDataByKodeUnitKerja";
        MasterBarangDataSource.SelectParameters.Clear();
        MasterBarangDataSource.SelectParameters.Add("kode", txtKataKunci);
        MasterBarangDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());

        ViewState.Add("SelectMethod", "GetDataByKodeUnitKerja");
      }
      else if (pilihan == "1") // Nama Barang..
      {
        MasterBarangDataSource.SelectMethod = "GetDataByNamaUnitKerja";
        MasterBarangDataSource.SelectParameters.Clear();
        MasterBarangDataSource.SelectParameters.Add("nama", txtKataKunci);
        MasterBarangDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());

        ViewState.Add("SelectMethod", "GetDataByNamaUnitKerja");
      }

      PnlListBarangModalPopupExtender.Show();
      GridListBarang.DataBind();
    }

    protected void BtnClearDataBarang_Click(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      DlistJenisBarang.SelectedIndex = 0;

      ViewState.Remove("SelectMethod");

      MasterBarangDataSource.SelectMethod = "GetDataByUnitKerja";
      MasterBarangDataSource.SelectParameters.Clear();
      MasterBarangDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());

      ViewState.Add("SelectMethod", "GetDataByUnitKerja");

      PnlListBarangModalPopupExtender.Show();
      GridListBarang.CurrentPageIndex = 0;
      GridListBarang.DataBind();
    }

    protected void PnlJasaBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%", PnlJasaTxtNmJasa.Text, "%");

      ViewState.Remove("SelectMethod");

      MasterJasaDataSource.SelectMethod = "GetDataByUnitKerjaNama";
      MasterJasaDataSource.SelectParameters.Clear();
      MasterJasaDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      MasterJasaDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByUnitKerjaNama");

      PnlBrowseMasterJasaModalPopupExtender.Show();
      GridMasterJasa.DataBind();
      GridMasterJasa.CurrentPageIndex = 0;
    }

    protected void GridMasterJasa_ItemCommand(object source, GridCommandEventArgs e)
    {
      var masterDataset = new MasterDataSet();
      var jasaTableAdapter = new master_jasaTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var kode = gridDataItem["kode"].Text;

        jasaTableAdapter.FillByKode(masterDataset.master_jasa, kode);

        PnlBarangKode.Value = masterDataset.master_jasa[0].kode;
        PnlBarangTxtNama.Text = masterDataset.master_jasa[0].nama;

        PnlBrowseMasterJasaModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterJasaModalPopupExtender.Show();
      }
    }

    protected void PnlSupplierBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + PnlSupplierTxtSerchBy.Text + "%");

      ViewState.Remove("SelectMethod");

      SupplierDataSource.SelectMethod = "GetDataLikeNama";
      SupplierDataSource.SelectParameters.Clear();
      SupplierDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataLikeNama");

      PnlBrowseSupplierModalPopupExtender.Show();
      GridSupplier.DataBind();
      GridSupplier.CurrentPageIndex = 0;
    }

    protected void GridSupplier_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        PnlBarangKode.Value = id;
        PnlBarangTxtNama.Text = gridDataItem["nama"].Text;
      }
      else
      {
        PnlBrowseSupplierModalPopupExtender.Show();
      }
    }

    protected void GridDepartement_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;       

        PnlBarangTxtNama.Text = gridDataItem["nama"].Text;
        PnlBarangKode.Value = gridDataItem["nama"].Text; 
      }
      else
      {
        PnlBrowseDeptModalPopupExtender.Show();
      }
    }

    protected void PnlMasterProjectBtnSearch_Click(object sender, EventArgs e)
    {
      var txtkatakunci = string.Concat("%" + PnlMasterProjectTxtKataKunci.Text + "%");
      var unitkerja = Session["UnitKerja"].ToString();

      ViewState.Remove("SelectMethod");

      if (PnlMasterProjectDlistSerachBy.SelectedValue == "0")
      {
        ProjectDataSource.SelectMethod = "GetDataByUnitKerjaNoProject";
        ProjectDataSource.SelectParameters.Clear();
        ProjectDataSource.SelectParameters.Add("unitkerja", unitkerja);
        ProjectDataSource.SelectParameters.Add("nomorproject", txtkatakunci);

        ViewState.Add("SelectMethod", "GetDataByUnitKerjaNoProject");
      }
      else
      {
        ProjectDataSource.SelectMethod = "GetDataByUnitKerjaSalesCustNama";
        ProjectDataSource.SelectParameters.Clear();
        ProjectDataSource.SelectParameters.Add("unitkerja", unitkerja);
        ProjectDataSource.SelectParameters.Add("salescustomernama", txtkatakunci);

        ViewState.Add("SelectMethod", "GetDataByUnitKerjaSalesCustNama");
      }

      PnlBrowseMasterProjectModalPopupExtender.Show();
      GridMasterProject.DataBind();
      GridMasterProject.CurrentPageIndex = 0;
    }

    protected void PnlMasterProjectBtnCancel_Click(object sender, EventArgs e)
    {
      PnlMasterProjectTxtKataKunci.Text = string.Empty;
      PnlMasterProjectDlistSerachBy.SelectedIndex = 0;

      ViewState.Remove("SelectMethod");
      ViewState.Add("SelectMethod", "GetDataByUnitKerja");

      ProjectDataSource.SelectMethod = "GetDataByUnitKerja";
      ProjectDataSource.SelectParameters.Clear();
      ProjectDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());

      PnlBrowseMasterProjectModalPopupExtender.Show();
      GridMasterProject.DataBind();
      GridMasterProject.CurrentPageIndex = 0;
    }

    protected void GridListBarang_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void GridListBarang_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void GridMasterJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void GridMasterJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void GridSupplier_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridDepartement_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseDeptModalPopupExtender.Show();
    }

    protected void GridDepartement_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseDeptModalPopupExtender.Show();
    }

    protected void GridMasterProject_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseMasterProjectModalPopupExtender.Show();
    }

    protected void GridMasterProject_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseMasterProjectModalPopupExtender.Show();
    }

    protected void GridMasterProject_ItemCommand(object source, GridCommandEventArgs e)
    {
      var masterDataset = new MasterDataSet();
      var vmasterproject01TableAdapter = new TmsBackDataController.MasterDataSetTableAdapters.vmaster_project01TableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        vmasterproject01TableAdapter.FillById(masterDataset.vmaster_project01, Convert.ToInt32(id));

        PnlBarangKode.Value = masterDataset.vmaster_project01[0].id.ToString();
        PnlBarangTxtNama.Text = masterDataset.vmaster_project01[0].nomorproject;

        PnlBrowseMasterProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterProjectModalPopupExtender.Show();
      }
    }

    #endregion

    private void Clear() {
      PnlBarangRbMR.Checked = false;
      PnlBarangRbPO.Checked = false;
      PnlBarangRbWR.Checked = false;
      PnlBarangRbWO.Checked = false;
    }

    protected void PnlBarangRbBg_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbBg.Checked == true)
      {
        PnlBarangRbMR.Visible = true;
        PnlBarangRbPO.Visible = true;
        PnlBarangRbWR.Visible = false;
        PnlBarangRbWO.Visible = false;
      }
      Clear();
      Pnl2.Visible = true;
      Panel1.Enabled = false;
    }

    protected void PnlBarangRbJs_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbJs.Checked == true)
      {
        PnlBarangRbMR.Visible = false;
        PnlBarangRbPO.Visible = false;
        PnlBarangRbWR.Visible = true;
        PnlBarangRbWO.Visible = true;
      }
      Clear();
      Pnl2.Visible = true;
      Panel1.Enabled = false;
    }

    private void FuncAuto() {
      string unitkerja = Session["UnitKerja"].ToString();
      if (unitkerja != "SHIPYARD") {
        PnlBarangRbNP.Checked = true;

        if (PnlBarangRbNP.Checked == true)
        {
          PnlBarangDlist1.Items.FindByValue("3").Enabled = false;
        }
        PnlBarangDlist1.Visible = true;
        Pnl3.Enabled = false;

      }
    }

    protected void PnlBarangRbMR_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbMR.Checked == true)
      {
        PnlBarangDlist1.Items.FindByValue("5").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("7").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("6").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("8").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("10").Enabled = false;
      }
      Pnl3.Visible = true;
      Pnl2.Enabled = false;
      FuncAuto();
    }

    protected void PnlBarangRbP_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbP.Checked == true)
      {
        PnlBarangDlist1.Items.FindByValue("4").Enabled = false;
      }
      PnlBarangDlist1.Visible = true;
      Pnl3.Enabled = false;
    }

    protected void PnlBarangRbNP_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbNP.Checked == true)
      {
        PnlBarangDlist1.Items.FindByValue("3").Enabled = false;
      }
      PnlBarangDlist1.Visible = true;
      Pnl3.Enabled = false;
    }


    protected void PnlBarangRbPO_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbPO.Checked == true)
      {
        PnlBarangDlist1.Items.FindByValue("6").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("10").Enabled = false;
      }
      Pnl3.Visible = true;
      Pnl2.Enabled = false;
      FuncAuto();
    }

    protected void PnlBarangRbWR_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbWR.Checked == true)
      {
        PnlBarangDlist1.Items.FindByValue("5").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("7").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("2").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("8").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("10").Enabled = false;
      }
      Pnl3.Visible = true;
      Pnl2.Enabled = false;
      FuncAuto();
    }

    protected void PnlBarangRbWO_CheckedChanged(object sender, EventArgs e)
    {
      if (PnlBarangRbWO.Checked == true)
      {
        PnlBarangDlist1.Items.FindByValue("2").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("3").Enabled = false;
        PnlBarangDlist1.Items.FindByValue("8").Enabled = false;
      }
      Pnl3.Visible = true;
      Pnl2.Enabled = false;
      FuncAuto();
    }

    protected void PnlBarangPrint_Click(object sender, EventArgs e)
    {
      // Tipe 1
      string var1 = "";
      if (PnlBarangRbBg.Checked == true) {
        var1 = "BRG";
      }
      else if (PnlBarangRbJs.Checked == true) {
        var1 = "JS";
      }

      // Tipe 2
      string var2 = "";
      if (PnlBarangRbMR.Checked == true) {
        var2 = "MR";
      }
      else if (PnlBarangRbWR.Checked == true) {
        var2 = "WR";
      }
      else if (PnlBarangRbPO.Checked == true) {
        var2 = "PO";
      }
      else if (PnlBarangRbWO.Checked == true) {
        var2 = "WO";
      }

      //Tipe 3
      string var3 = "";
      if (PnlBarangRbP.Checked == true) {
        var3 = "P";
      }
      else if (PnlBarangRbNP.Checked == true) {
        var3 = "NP";
      }

      //Tipe 4
      string var4 = PnlBarangDlist1.SelectedValue;

      string type = var3;
      string tgl1 = PnlBarangTgl1.SelectedDate.Value.ToShortDateString();
      string tgl2 = PnlBarangTgl2.SelectedDate.Value.ToShortDateString();
      string kode = PnlBarangKode.Value;
      string nama = PnlBarangTxtNama.Text;

      string[] input = { var1, var2, var3, var4 };

      #region FunctionMR

      string[] criteria1 = { "BRG", "MR", "P", "1" }; // Per Tgl
      string[] criteria2 = { "BRG", "MR", "P", "2" }; // Per Brg
      string[] criteria3 = { "BRG", "MR", "P", "3" }; // Per Project
      
      string[] criteria5 = { "BRG", "MR", "NP", "1" };
      string[] criteria6 = { "BRG", "MR", "NP", "2" };
      string[] criteria7 = { "BRG", "MR", "NP", "4" };
      
      if (input.SequenceEqual(criteria1) || input.SequenceEqual(criteria5))
      {
        printDataMR01(type, tgl1, tgl2);
      }
      else if (input.SequenceEqual(criteria2) || input.SequenceEqual(criteria6))
      {
        printDataMR02(type, tgl1, tgl2, kode, nama, 'b'); // barang
      }
      else if (input.SequenceEqual(criteria3))
      {
        printDataMR02(type, tgl1, tgl2, kode, nama, 'p'); //project
      }
      else if (input.SequenceEqual(criteria7))
      {
        printDataMR02(type, tgl1, tgl2, kode, nama, 'd'); // departement
      }

      #endregion MR

      #region FunctionPO

      string[] criteria9 = { "BRG", "PO", "P", "1" };
      string[] criteria10 = { "BRG", "PO", "P", "2" };
      string[] criteria11 = { "BRG", "PO", "P", "5" };
      string[] criteria12 = { "BRG", "PO", "P", "7" };
      string[] criteria13 = { "BRG", "PO", "P", "8" };

      string[] criteria14 = { "BRG", "PO", "NP", "1" };
      string[] criteria15 = { "BRG", "PO", "NP", "2" };
      string[] criteria16 = { "BRG", "PO", "NP", "5" };
      string[] criteria17 = { "BRG", "PO", "NP", "7" };
      string[] criteria18 = { "BRG", "PO", "NP", "8" };

      if (input.SequenceEqual(criteria9) || input.SequenceEqual(criteria14)) 
      {
        printDataPO01(type, tgl1, tgl2);
      }
      else if (input.SequenceEqual(criteria10) || input.SequenceEqual(criteria15)) {
        printDataPO02(type, tgl1, tgl2, kode, 'a'); // per barang
      }
      else if (input.SequenceEqual(criteria11) || input.SequenceEqual(criteria16)) {
        printDataPO02(type, tgl1, tgl2, kode, 'b'); // per supplier
      }
      else if (input.SequenceEqual(criteria12) || input.SequenceEqual(criteria17)) {
        printDataPO02(type, tgl1, tgl2, kode, 'c'); // rekap supplier
      }
      else if (input.SequenceEqual(criteria13) || input.SequenceEqual(criteria18)) {
        printDataPO02(type, tgl1, tgl2, kode, 'd'); //rekap barang
      }

      #endregion

      #region FunctionWR

      string[] criteria19 = { "JS", "WR", "P", "1" };
      string[] criteria20 = { "JS", "WR", "P", "3" };
      string[] criteria21 = { "JS", "WR", "P", "6" };
      
      string[] criteria23 = { "JS", "WR", "NP", "1" };
      string[] criteria24 = { "JS", "WR", "NP", "4" };
      string[] criteria25 = { "JS", "WR", "NP", "6" };

      if (input.SequenceEqual(criteria19) || input.SequenceEqual(criteria23)) 
      {
        printDataWR01(type, tgl1, tgl2);
      }
      else if (input.SequenceEqual(criteria20)) 
      {
        printDataWR02(type, tgl1, tgl2, kode, nama, 'p'); //project
      }
      else if (input.SequenceEqual(criteria21) || input.SequenceEqual(criteria25)) 
      {
        printDataWR02(type, tgl1, tgl2, kode, nama, 'j'); //jasa
      }
      else if (input.SequenceEqual(criteria24)) {
        printDataWR02(type, tgl1, tgl2, kode, nama, 'd'); // departement
      }

      #endregion


      string[] criteria27 = { "JS", "WO", "P", "1" };
      string[] criteria28 = { "JS", "WO", "P", "5" };
      string[] criteria29 = { "JS", "WO", "P", "6" };
      string[] criteria30 = { "JS", "WO", "P", "7" };
      string[] criteria31 = { "JS", "WO", "P", "10" };

      string[] criteria32 = { "JS", "WO", "NP", "1" };
      string[] criteria33 = { "JS", "WO", "NP", "5" };
      string[] criteria34 = { "JS", "WO", "NP", "6" };
      string[] criteria35 = { "JS", "WO", "NP", "7" };
      string[] criteria36 = { "JS", "WO", "NP", "10" };


      if (input.SequenceEqual(criteria27) || input.SequenceEqual(criteria32)) {
        printDataWO01(type, tgl1, tgl2);
      }
      else if (input.SequenceEqual(criteria28) || input.SequenceEqual(criteria33)) {
        printDataWO02(type, tgl1, tgl2, kode, 'a'); //supplier
      }
      else if (input.SequenceEqual(criteria29) || input.SequenceEqual(criteria34)) {
        printDataWO02(type, tgl1, tgl2, kode, 'b'); //jasa
      }
      else if (input.SequenceEqual(criteria30) || input.SequenceEqual(criteria35)) {
        printDataWO02(type, tgl1, tgl2, kode, 'c'); // rekap supplier
      }
      else if (input.SequenceEqual(criteria31) || input.SequenceEqual(criteria36)) {
        printDataWO02(type, tgl1, tgl2, kode, 'd'); //rekap jasa
      }

      
    }



    #region print MR

    protected void printDataMR01(string type, string pPeriodeStart, string pPeriodeEnd)
    {
      var reportDataSet = new ReportsDataSet();
      var vmr02TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_mr02TableAdapter();
      var reportViewer = new ReportViewer();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");


      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapMR.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      vmr02TableAdapter.FillByTypeUnitKerjaTanggal(reportDataSet.vpur_mr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd);
      string pTitle = "Laporan MR per Tanggal";      

      _myReportParameters[0] = new ReportParameter("pDate1", periodeStart);
      _myReportParameters[1] = new ReportParameter("pDate2", periodeEnd);
      _myReportParameters[2] = new ReportParameter("pTitle", pTitle);
      _myReportParameters[3] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString().ToUpper());

      MyParamEnum = _myReportParameters;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_mr02";
      tabelPrintDataSource.Value = reportDataSet.vpur_mr02;

      reportViewer.LocalReport.SetParameters(MyParamEnum);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporan_mr_"+ DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    protected void printDataMR02(string type, string pPeriodeStart, string pPeriodeEnd, string kode, string nama, char x)
    {
      var reportDataSet = new ReportsDataSet();
      var vmr02TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_mr02TableAdapter();
      var reportViewer = new ReportViewer();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");


      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapMR.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      string pTitle;
      if (x == 'b')
      {
        vmr02TableAdapter.FillByTypeUnitKerjaTanggalKodeBarang(reportDataSet.vpur_mr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, kode);
        pTitle = "Laporan MR per-Barang " + nama;
      }
      else if (x == 'p')
      {
        vmr02TableAdapter.FillByTypeUnitKerjaTanggalProjectId(reportDataSet.vpur_mr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, Convert.ToInt32(kode));
        pTitle = "Laporan MR per-Project " + nama;
      }
      else {
        vmr02TableAdapter.FillByTypeUnitKerjaTanggalDepartement(reportDataSet.vpur_mr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, kode);
        pTitle = "Laporan MR perDepartement " + nama;
      }      

      _myReportParameters[0] = new ReportParameter("pDate1", periodeStart);
      _myReportParameters[1] = new ReportParameter("pDate2", periodeEnd);
      _myReportParameters[2] = new ReportParameter("pTitle", pTitle);
      _myReportParameters[3] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString().ToUpper());

      MyParamEnum = _myReportParameters;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_mr02";
      tabelPrintDataSource.Value = reportDataSet.vpur_mr02;

      reportViewer.LocalReport.SetParameters(MyParamEnum);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporan_mr_" + DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    #endregion


    #region print WR

    protected void printDataWR01(string type, string pPeriodeStart, string pPeriodeEnd)
    {
      var reportDataSet = new ReportsDataSet();
      var vwr02TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_wr02TableAdapter();
      var reportViewer = new ReportViewer();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");

      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWR.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      vwr02TableAdapter.FillByTypeUnitKerjaTglWR(reportDataSet.vpur_wr02, type,Session["UnitKerja"].ToString(), dateStart, dateEnd);
      string pTitle = "Laporan Transaksi WR";

      _myReportParameters[0] = new ReportParameter("pDate1", periodeStart);
      _myReportParameters[1] = new ReportParameter("pDate2", periodeEnd);
      _myReportParameters[2] = new ReportParameter("pTitle", pTitle);
      _myReportParameters[3] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString().ToUpper());


      MyParamEnum = _myReportParameters;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_wr02";
      tabelPrintDataSource.Value = reportDataSet.vpur_wr02;

      reportViewer.LocalReport.SetParameters(MyParamEnum);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporan_wr_", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    protected void printDataWR02(string type, string pPeriodeStart, string pPeriodeEnd, string kode, string nama, char x)
    {
      var reportDataSet = new ReportsDataSet();
      var vwr02TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_wr02TableAdapter();
      var reportViewer = new ReportViewer();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");

      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWR.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      string pTitle;
      if (x == 'p') {
        vwr02TableAdapter.FillByTypeUnitKerjaTglWrProjectId(reportDataSet.vpur_wr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, Convert.ToInt32(kode));
        pTitle = "Laporan Transaksi WR per-Project [ " + nama + " ]";
      }
      else if (x == 'j')
      {
        vwr02TableAdapter.FillByTypeUnitKerjaTglWRJasaKode(reportDataSet.vpur_wr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, kode);
        pTitle = "Laporan Transaksi WR per-Jasa [ " + nama + " ]";
      }
      else {
        vwr02TableAdapter.FillByTypeUnitKerjaTglWRDepartement(reportDataSet.vpur_wr02, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, kode);
        pTitle = "Laporan Transaksi WR per-Departement [ " + nama + " ]";
      }


      _myReportParameters[0] = new ReportParameter("pDate1", periodeStart);
      _myReportParameters[1] = new ReportParameter("pDate2", periodeEnd);
      _myReportParameters[2] = new ReportParameter("pTitle", pTitle);
      _myReportParameters[3] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString().ToUpper());


      MyParamEnum = _myReportParameters;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_wr02";
      tabelPrintDataSource.Value = reportDataSet.vpur_wr02;

      reportViewer.LocalReport.SetParameters(MyParamEnum);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporan_wr_", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    #endregion


    #region print PO

    private void printDataPO01(string type, string pPeriodeStart, string pPeriodeEnd)
    {
      var reportViewer = new ReportViewer();
      var reportDataSet = new ReportsDataSet();
      var vpo01TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_po01TableAdapter();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");


      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapPOPerTransaksi.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      vpo01TableAdapter.FillByTypeUnitKerjaTglPo(reportDataSet.vpur_po01,type, Session["UnitKerja"].ToString(), dateStart, dateEnd);

      _myReportParameters01[0] = new ReportParameter("pPeriodeStart", periodeStart);
      _myReportParameters01[1] = new ReportParameter("pPeriodeEnd", periodeEnd);
      _myReportParameters01[2] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString());

      MyParamEnum01 = _myReportParameters01;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_po01";
      tabelPrintDataSource.Value = reportDataSet.vpur_po01;

      reportViewer.LocalReport.SetParameters(MyParamEnum01);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporanpo", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }


    private void printDataPO02(string type, string pPeriodeStart, string pPeriodeEnd, string kode, char x)
    {
      var reportViewer = new ReportViewer();
      var reportDataSet = new ReportsDataSet();
      var vpo01TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_po01TableAdapter();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");


      reportViewer.ProcessingMode = ProcessingMode.Local;
      if (x == 'a') 
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapPOPerBarang.rdlc");
        vpo01TableAdapter.FillByTipeUnitKerjaTglPoBarangKode(reportDataSet.vpur_po01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, kode);
      }
      else if (x == 'b') {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapPOPerSupplier.rdlc");
        vpo01TableAdapter.FillByTipeUnitKerjaTglPoSupplierId(reportDataSet.vpur_po01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, Convert.ToInt32(kode));
      }
      else if (x == 'c') {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapPOPerSupplier.rdlc");
        vpo01TableAdapter.FillByTypeUnitKerjaTglPo(reportDataSet.vpur_po01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd);
      }
      else if (x == 'd') {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapPOPerBarang.rdlc");
        vpo01TableAdapter.FillByTypeUnitKerjaTglPoGroupById(reportDataSet.vpur_po01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd);
      }
      
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      

      _myReportParameters01[0] = new ReportParameter("pPeriodeStart", periodeStart);
      _myReportParameters01[1] = new ReportParameter("pPeriodeEnd", periodeEnd);
      _myReportParameters01[2] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString());

      MyParamEnum01 = _myReportParameters01;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_po01";
      tabelPrintDataSource.Value = reportDataSet.vpur_po01;

      reportViewer.LocalReport.SetParameters(MyParamEnum01);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporanpo", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    #endregion


    #region print WO

    private void printDataWO01(string type, string pPeriodeStart, string pPeriodeEnd)
    {
      var reportViewer = new ReportViewer();
      var reportDataSet = new ReportsDataSet();
      var vpo01TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_wo01TableAdapter();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");


      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWOPerTransaksi.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      vpo01TableAdapter.FillByTypeUnitKerjaTglWo(reportDataSet.vpur_wo01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd);

      _myReportParameters01[0] = new ReportParameter("pPeriodeStart", periodeStart);
      _myReportParameters01[1] = new ReportParameter("pPeriodeEnd", periodeEnd);
      _myReportParameters01[2] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString());

      MyParamEnum01 = _myReportParameters01;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_wo01";
      tabelPrintDataSource.Value = reportDataSet.vpur_wo01;

      reportViewer.LocalReport.SetParameters(MyParamEnum01);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporanwo", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    private void printDataWO02(string type, string pPeriodeStart, string pPeriodeEnd, string kode, char x)
    {
      var reportViewer = new ReportViewer();
      var reportDataSet = new ReportsDataSet();
      var vpo01TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_wo01TableAdapter();

      DateTime dateStart = Convert.ToDateTime(pPeriodeStart);
      DateTime dateEnd = Convert.ToDateTime(pPeriodeEnd);

      string periodeStart = dateStart.ToString("dd MMMM yyyy");
      string periodeEnd = dateEnd.ToString("dd MMMM yyyy");


      reportViewer.ProcessingMode = ProcessingMode.Local;
      if (x == 'a')
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWOPerSupplier.rdlc");
        vpo01TableAdapter.FillByTypeUnitKerjaTglWoSupplierId(reportDataSet.vpur_wo01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, Convert.ToInt32(kode));        
      }
      else if (x == 'b')
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWOPerJasa.rdlc");
        vpo01TableAdapter.FillByTypeUnitKerjaTglWoJasaKode(reportDataSet.vpur_wo01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd, kode);
      }
      else if (x == 'c')
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWOPerSupplier.rdlc");
        vpo01TableAdapter.FillByTypeUnitKerjaTglWo(reportDataSet.vpur_wo01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd);
      }
      else if (x == 'd')
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptLapWOPerJasa.rdlc");
        vpo01TableAdapter.FillByTypeUnitKerjaTglWo(reportDataSet.vpur_wo01, type, Session["UnitKerja"].ToString(), dateStart, dateEnd);
      }

      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..



      _myReportParameters01[0] = new ReportParameter("pPeriodeStart", periodeStart);
      _myReportParameters01[1] = new ReportParameter("pPeriodeEnd", periodeEnd);
      _myReportParameters01[2] = new ReportParameter("pUnitKerja", Session["UnitKerja"].ToString());

      MyParamEnum01 = _myReportParameters01;

      var tabelPrintDataSource = new ReportDataSource();
      tabelPrintDataSource.Name = "vpur_wo01";
      tabelPrintDataSource.Value = reportDataSet.vpur_wo01;

      reportViewer.LocalReport.SetParameters(MyParamEnum01);
      reportViewer.LocalReport.DataSources.Add(tabelPrintDataSource);

      //Code For Download Direct PDF    

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
      // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=laporanpo", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }


    #endregion

    protected void PnlBtnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("Laporan.aspx");
    }
  }
}
