using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.MasterDataSetTableAdapters;
using TmsBackDataController.SysDataSetTableAdapters;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;
using ManagementSystem.Helper;

namespace ManagementSystem.Purchasing.WorkRequest.Project.Work_Request
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          MasterJasaDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          ProjectDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }

    }

    #region Function Get

    public string getUsername
    {
      get
      {
        string result = Session["Username"].ToString();
        return result;
      }
    }

    public string getFullName
    {
      get
      {
        string result = Session["FullName"].ToString();
        return result;
      }
    }


    public string getUnitKerja
    {
      get
      {
        string result = DlistUnitKerja.SelectedValue;
        return result;
      }
    }

    #endregion 

    protected void BtnBrowseProject_Click(object sender, EventArgs e)
    {
      PnlBrowseMasterProjectModalPopupExtender.Show();
    }

    protected void BtnBrowseLokasi_Click(object sender, EventArgs e)
    {
      PnlBrowseLokasiProjectModalPopupExtender.Show();
    }

    protected void BtnBrowseKategori_Click(object sender, EventArgs e)
    {
      PnlBrowseKategoriProjectModalPopupExtender.Show();
    }

    protected void BtnBrowseKodeScope_Click(object sender, EventArgs e)
    {
      PnlBrowseScopeProjectModalPopupExtender.Show();
    }

    protected void BtnBrowseUsable_Click(object sender, EventArgs e)
    {
      PnlBrowseUsableProjectModalPopupExtender.Show();
    }

    protected void GridMasterProject_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var masterDataset = new MasterDataSet();
      var vmasterproject01TableAdapter = new TmsBackDataController.MasterDataSetTableAdapters.vmaster_project01TableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        vmasterproject01TableAdapter.FillById(masterDataset.vmaster_project01, Convert.ToInt32(id));

        HdIdMasterProject.Value = masterDataset.vmaster_project01[0].id.ToString();
        TxtNomorProject.Text = masterDataset.vmaster_project01[0].nomorproject;

        PnlBrowseMasterProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterProjectModalPopupExtender.Show();
      }
    }

    protected void GridLokasi_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataset = new PurDataSet();
      var lokasiTableAdapter = new pur_lokasiTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        lokasiTableAdapter.FillById(purDataset.pur_lokasi, Convert.ToInt32(id));

        HdIdLokasi.Value = purDataset.pur_lokasi[0].id.ToString();
        TxtLokasi.Text = purDataset.pur_lokasi[0].inisial;

        PnlBrowseLokasiProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseLokasiProjectModalPopupExtender.Show();
      }
    }

    protected void GridCategory_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataset = new PurDataSet();
      var kategoriTableAdapter = new pur_kategoriTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        kategoriTableAdapter.FillById(purDataset.pur_kategori, Convert.ToInt32(id));

        HdIdKategori.Value = purDataset.pur_kategori[0].id.ToString();
        TxtKategori.Text = purDataset.pur_kategori[0].inisial;

        PnlBrowseKategoriProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseKategoriProjectModalPopupExtender.Show();
      }
    }

    protected void GridScope_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataset = new PurDataSet();
      var scopeTableAdapter = new pur_scopeTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        scopeTableAdapter.FillById(purDataset.pur_scope, Convert.ToInt32(id));

        HdIdScope.Value = purDataset.pur_scope[0].id.ToString();
        TxtKodeScope.Text = purDataset.pur_scope[0].inisial;

        PnlBrowseScopeProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseScopeProjectModalPopupExtender.Show();
      }
    }

    protected void GridUsable_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataset = new PurDataSet();
      var usableTableAdapter = new pur_usableTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        usableTableAdapter.FillById(purDataset.pur_usable, Convert.ToInt32(id));

        HdIdUsable.Value = purDataset.pur_usable[0].id.ToString();
        TxtUsable.Text = purDataset.pur_usable[0].inisial;

        PnlBrowseUsableProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseUsableProjectModalPopupExtender.Show();
      }
    }

    protected void RbYes_CheckedChanged(object sender, EventArgs e)
    {
      BtnRefresh.Visible = true;
      GetGenerate();
    }

    private void GetGenerate()
    {
      string lokasi = TxtLokasi.Text;
      string nomorproject = TxtNomorProject.Text;
      string kodesection = TxtKategori.Text;
      string kodescope = TxtKodeScope.Text;

      DateTime date = RDatePickerTxtTgl.SelectedDate.Value;
      string tahun = date.Year.ToString();

      var idproject = HdIdMasterProject.Value;
      var counter = GetCounterWR(idproject, date);

      string usable = TxtUsable.Text;

      TxtReference.Visible = true;
      TxtReference.ReadOnly = true;
      TxtReference.Text = String.Concat("WR/" + lokasi + "/" + nomorproject + "/" + kodesection + "/" + kodescope + "/" + counter + "/" + tahun + "/" + usable);
      

    }

    private static string GetCounterWR(string id, DateTime date)
    {
      var masterDataSet = new MasterDataSet();
      var masterprojectTableAdapter = new TmsBackDataController.MasterDataSetTableAdapters.master_projectTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();
      int counter;

      if (masterprojectTableAdapter.FillById(masterDataSet.master_project, Convert.ToInt32(id)) > 0)
      {
        counter = Convert.ToInt32(wrTableAdapter.ScalarGetCountByTypeProjectIdTglWr("P", Convert.ToInt32(id), date.Year));
      }
      else { counter = 0; }

      var result = Convert.ToString(counter + 1);
      var x = 4 - result.Length;

      for (var i = 0; i < x; i++)
      {
        result = string.Concat("0", result);
      }
      result = string.Concat(result);
      return result;
    }

    protected void RbNo_CheckedChanged(object sender, EventArgs e)
    {
      ClearGenerate();
    }

    private void ClearGenerate()
    {
      TxtReference.Visible = true;
      TxtReference.ReadOnly = false;
      TxtReference.Text = string.Empty;
    }

 
    protected void BtnSaveTransaksiWR_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrRow = purDataSet.pur_wr.Newpur_wrRow();

      DateTime date = RDatePickerTxtTgl.SelectedDate.Value;
      string id = "";
      if (getUnitKerja == "SHIPYARD")
      {
        id = CUtils.GenerateId("pur_wr", date.Year, date.Month, "SWR-");
      }
      else if (getUnitKerja == "ASSEMBLING") {
        id = CUtils.GenerateId("pur_wr", date.Year, date.Month, "MAWR-"); 
      }
      else
      {
        id = CUtils.GenerateId("pur_wr", date.Year, date.Month, "MSWR-"); 
      }

      try
      {
        wrRow.id = id;
        wrRow.reference = TxtReference.Text == string.Empty ? "" : TxtReference.Text;
        wrRow.tglwr = RDatePickerTxtTgl.SelectedDate == null ? RDatePickerTxtTgl.MinDate : RDatePickerTxtTgl.SelectedDate.Value;
        wrRow.type = "P";
        wrRow.project_id = Convert.ToInt32(HdIdMasterProject.Value);
        wrRow.lokasi_id = Convert.ToInt32(HdIdLokasi.Value);
        wrRow.kategori_id = Convert.ToInt32(HdIdKategori.Value);
        wrRow.scope_id = HdIdScope.Value == null ? 0 : Convert.ToInt32(HdIdScope.Value);
        wrRow.usable_id = HdIdUsable.Value == null ? 0 : Convert.ToInt32(HdIdUsable.Value);
        wrRow.departement = "-";
        wrRow.status = "WR1";
        wrRow.createdby = getFullName;
        wrRow.datecreated = DateTime.Now;
        wrRow.unitkerja = getUnitKerja;

        purDataSet.pur_wr.Addpur_wrRow(wrRow);
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateSeed("pur_wr", date.Year, date.Month, date);
          CUtils.UpdateLog("pur_wr", id, getUsername, "Insert WR Detail");
        }

        NotificationSuccess();
        TxtNoWR.Text = id;
        Panel1.Enabled = false;
        PnlInputJasa.Enabled = true;
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }

    }

    private void NotificationFailure(Exception ex)
    {
      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya.. ", ex.Message);
      PnlMessageModalPopupExtender.Show();
    }

    private void NotificationSuccess()
    {
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    private void ClearForm()
    {
      RDatePickerTxtTgl.SelectedDate = null;
      TxtNomorProject.Text = string.Empty;
      TxtLokasi.Text = string.Empty;
      TxtKategori.Text = string.Empty;
      TxtKodeScope.Text = string.Empty;
      TxtUsable.Text = string.Empty;
      TxtReference.Text = string.Empty;
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      ClearGenerate();
      GetGenerate();
    }

    protected void BtnCancelTransaksiWR_Click(object sender, EventArgs e)
    {
      RbNo.Checked = false;
      RbYes.Checked = false;
      TxtReference.Visible = false;
      BtnRefresh.Visible = false;
      RDatePickerTxtTgl.SelectedDate = null;
      TxtNomorProject.Text = string.Empty;
      TxtLokasi.Text = string.Empty;
      TxtKategori.Text = string.Empty;
      TxtKodeScope.Text = string.Empty;
      TxtUsable.Text = string.Empty;
    }

    protected void BtnBrowseMasterJasa_Click(object sender, EventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void GridMasterJasa_ItemCommand(object source, GridCommandEventArgs e)
    {
      var masterDataset = new MasterDataSet();
      var masterjasaTableAdapter = new master_jasaTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var kode = gridDataItem["kode"].Text;

        masterjasaTableAdapter.FillByKode(masterDataset.master_jasa, kode);

        HdKdMasterJasa.Value = masterDataset.master_jasa[0].kode;
        TxtInisialJasa.Text = masterDataset.master_jasa[0].nama;

        PnlBrowseMasterJasaModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterJasaModalPopupExtender.Show();
      }
    }

    protected void BtnAddJasa_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrdetailRow = purDataSet.pur_wrdetail.Newpur_wrdetailRow();

      try
      {
        wrdetailRow.wr_id = TxtNoWR.Text;
        wrdetailRow.jasa_kode = HdKdMasterJasa.Value;
        wrdetailRow.jmljasa = TxtJmlJasa.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlJasa.Text);
        wrdetailRow.satuan = TxtSatuan.Text == string.Empty ? "" : TxtSatuan.Text;
        wrdetailRow.tanggal = RDTxtTglDibutuhkan.SelectedDate.Value;
        wrdetailRow.jmlhari = TxtJumlahHari.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlahHari.Text);
        wrdetailRow.jmlorang = TxtJmlOrang.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlOrang.Text);
        wrdetailRow.keterangan = TxtKeterangan.Text;

        wrdetailRow.supplier_id = 0;
        wrdetailRow.harga = 0;
        wrdetailRow.currency = "-";

        wrdetailRow.status = "J1";
        wrdetailRow.type = "P";

        wrdetailRow.createdby = getFullName;

        purDataSet.pur_wrdetail.Addpur_wrdetailRow(wrdetailRow);
        if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", (Convert.ToInt32(wrdetailTableAdapter.ScalarGetMaxId())+1).ToString(), getUsername, "Insert WR Detail");
        }

        GridRequestJasa.DataBind();
        NotificationSuccess();
        ClearJasa();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void ClearJasa()
    {
      HdKdMasterJasa.Value = null;
      TxtInisialJasa.Text = string.Empty;
      TxtJmlJasa.Text = string.Empty;
      TxtSatuan.Text = string.Empty;
      RDTxtTglDibutuhkan.SelectedDate = null;
      TxtJumlahHari.Text = string.Empty;
      TxtKeterangan.Text = string.Empty;
      GridRequestJasa.Visible = true;
    }

    protected void BtnBackTransaksiWR_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
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

    protected void PnlKategoriBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + PnlKategoriTxtKataKunci.Text + "%");

      ViewState.Remove("SelectMethod");

      KategoriDataSource.SelectMethod = "GetDataByNama";
      KategoriDataSource.SelectParameters.Clear();
      KategoriDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByNama");

      PnlBrowseKategoriProjectModalPopupExtender.Show();
      GridCategory.DataBind();
      GridCategory.CurrentPageIndex = 0;
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

    protected void PnlJasaBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%", PnlJasaTxtKataKunci.Text, "%");

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

    protected void GridMasterJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void GridMasterJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void GridUsable_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseUsableProjectModalPopupExtender.Show();
    }

    protected void GridUsable_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseUsableProjectModalPopupExtender.Show();
    }

    protected void GridScope_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseScopeProjectModalPopupExtender.Show();
    }

    protected void GridScope_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseScopeProjectModalPopupExtender.Show();
    }

    protected void GridCategory_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseKategoriProjectModalPopupExtender.Show();
    }

    protected void GridCategory_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseKategoriProjectModalPopupExtender.Show();
    }

    protected void GridLokasi_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseLokasiProjectModalPopupExtender.Show();
    }

    protected void GridLokasi_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseLokasiProjectModalPopupExtender.Show();
    }

    protected void GridMasterProject_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseMasterProjectModalPopupExtender.Show();
    }

    protected void GridMasterProject_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseMasterProjectModalPopupExtender.Show();
    }
  }
}
