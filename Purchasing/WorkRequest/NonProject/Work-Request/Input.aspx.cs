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
using ManagementSystem.Helper;
using Telerik.Web.UI;

namespace ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Request
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

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

    protected void Page_Load(object sender, EventArgs e)
    {
      string unitkerja = Session["UnitKerja"].ToString();
      if (unitkerja == "SHIPYARD")
      {
        LblTitle.Text = "Input Work Request (Non Project)";
        PnlScope.Visible = true;
        PnlUsable.Visible = true;
      }
      else
      {
        LblTitle.Text = "Input Work Request";
        PnlScope.Visible = false;
        PnlUsable.Visible = false;
        PnlJmlOrang.Visible = false;
        PnlHari.Visible = false;
      }

      if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          MasterJasaDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }    

    protected void RbYes_CheckedChanged(object sender, EventArgs e)
    {
      BtnRefresh.Visible = true;
      GetGenerate();
    }

    private void GetGenerate()
    {
      var purDataSer = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();
      var lokasiTableAdapter = new pur_lokasiTableAdapter();

      string kodelokasi = lokasiTableAdapter.ScalarGetInisialById(Convert.ToInt32(DlistLokasiProject.SelectedValue));
      string kodedepartment = PurchaseUtils.KodeDepartement(DListDepartment.SelectedValue);
      string kodescope = TxtKodeScope.Text;
      int dateYear = RDPickerTxtTgl.SelectedDate.Value.Year;
      string tahun = dateYear.ToString();
      string jeniskategori = TxtUsable.Text;



      if (getUnitKerja == "SHIPYARD")
      {
        int idScope = Convert.ToInt32(HdIdScope.Value);
        int idUsable = Convert.ToInt32(HdIdUsable.Value);
        //start hitung counter
        int countWR = Convert.ToInt32(
                          wrTableAdapter.ScalarGetCountByTypeDepartementScopeIdUsableIdUnitKerjaTglWr
                                                ("NP", DListDepartment.SelectedValue, idScope, idUsable, getUnitKerja, dateYear));
        int count = countWR + 1;
        var result = count.ToString();
        var x = 4 - result.Length;

        for (var i = 0; i < x; i++)
        {
          result = string.Concat("0", result);
        }

        result = string.Concat(result);
        string counter = result;
        //end hitung counter

        TxtReference.Visible = true;
        TxtReference.ReadOnly = true;
        TxtReference.Text = string.Concat("WR/" + kodelokasi + "/" + kodedepartment + "/" + kodescope + "/" + counter + "/" + tahun + "/" + jeniskategori);
      }
      else
      {
        int countWR = Convert.ToInt32(wrTableAdapter.ScalarGetCountByTypeDepartementTglWrUnitKerja("NP", DListDepartment.SelectedValue, dateYear, getUnitKerja));
        int count = countWR + 1;
        var result = count.ToString();
        var x = 4 - result.Length;

        for (var i = 0; i < x; i++)
        {
          result = string.Concat("0", result);
        }

        result = string.Concat(result);
        string counter = result;

        TxtReference.Visible = true;
        TxtReference.ReadOnly = true;
        TxtReference.Text = string.Concat("WR/" + kodelokasi + "/" + kodedepartment + "/" + counter + "/" + tahun);
      }
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

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      ClearGenerate();
      GetGenerate();
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

    protected void BtnBrowseScope_Click(object sender, EventArgs e)
    {
      PnlBrowseScopeProjectModalPopupExtender.Show();
    }

    protected void BtnBrowseUsable_Click(object sender, EventArgs e)
    {
      PnlBrowseUsableProjectModalPopupExtender.Show();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrRow = purDataSet.pur_wr.Newpur_wrRow();

      DateTime date = RDPickerTxtTgl.SelectedDate.Value;
      string id = "";
      if (getUnitKerja == "SHIPYARD")
      {
        id = CUtils.GenerateId("pur_wr", date.Year, date.Month, "SWR-");
      }
      else if (getUnitKerja == "ASSEMBLING")
      {
        id = CUtils.GenerateId("pur_wr", date.Year, date.Month, "MAWR-"); ;
      }
      else if (getUnitKerja == "SPARE PARTS")
      {
        id = CUtils.GenerateId("pur_wr", date.Year, date.Month, "MSWR-"); ;
      }


      try
      {
        wrRow.id = id;
        wrRow.reference = TxtReference.Text;
        wrRow.tglwr = RDPickerTxtTgl.SelectedDate.Value;
        wrRow.type = "NP";
        wrRow.project_id = 0;
        wrRow.lokasi_id = Convert.ToInt32(DlistLokasiProject.SelectedValue);
        wrRow.kategori_id = 0;
        wrRow.scope_id = HdIdScope.Value == string.Empty ? 0 : Convert.ToInt32(HdIdScope.Value);
        wrRow.usable_id = HdIdUsable.Value == string.Empty ? 0 : Convert.ToInt32(HdIdUsable.Value);
        wrRow.departement = DListDepartment.SelectedValue;
        wrRow.status = "WR1";
        wrRow.createdby = getFullName;
        wrRow.datecreated = DateTime.Now;
        wrRow.unitkerja = DlistUnitKerja.SelectedValue;

        purDataSet.pur_wr.Addpur_wrRow(wrRow);
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateSeed("pur_wr", date.Year, date.Month, date);
          CUtils.UpdateLog("pur_wr", id, getUsername, "Insert WR Detail");
        }

        NotificationSuccess();
        TxtNoWR.Text = id;
        PnlTransaksi.Enabled = false;
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
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void BtnBrowseJasa_Click(object sender, EventArgs e)
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
        var id = gridDataItem["kode"].Text;

        masterjasaTableAdapter.FillByKode(masterDataset.master_jasa, id);

        HdKdJasa.Value = masterDataset.master_jasa[0].kode;
        TxtJasa.Text = masterDataset.master_jasa[0].nama;

        PnlBrowseMasterJasaModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterJasaModalPopupExtender.Show();
      }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      RbNo.Checked = false;
      RbYes.Checked = false;
      TxtReference.Visible = false;
      BtnRefresh.Visible = false;
      RDPickerTxtTgl.SelectedDate = null;      
      DlistLokasiProject.SelectedIndex = 0;
      TxtKodeScope.Text = string.Empty;
      TxtUsable.Text = string.Empty;
    }

    protected void BtnAddInputJasa_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrdetailRow = purDataSet.pur_wrdetail.Newpur_wrdetailRow();

      try
      {
        wrdetailRow.wr_id = TxtNoWR.Text;
        wrdetailRow.jasa_kode = HdKdJasa.Value;
        wrdetailRow.jmljasa = TxtJmlJasa.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlJasa.Text);
        wrdetailRow.satuan = TxtSatuan.Text;
        wrdetailRow.jmlhari = TxtJmlHari.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlHari.Text);
        wrdetailRow.jmlorang = TxtJmlOrang.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlOrang.Text);
        wrdetailRow.tanggal = RDateTxtTglPelaksanaan.SelectedDate.Value;
        wrdetailRow.keterangan = TxtKeterangan.Text;
        wrdetailRow.status = "J1";
        wrdetailRow.type = "NP";
        wrdetailRow.createdby = getFullName;
        wrdetailRow.supplier_id = 0;
        wrdetailRow.harga = 0;
        wrdetailRow.currency = "-";

        purDataSet.pur_wrdetail.Addpur_wrdetailRow(wrdetailRow);
        if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
        {
          int tahun = DateTime.Now.Year;
          CUtils.UpdateLog("pur_wrdetail", (Convert.ToInt32(wrdetailTableAdapter.ScalarGetMaxId()) + 1).ToString(), getFullName, "Insert WR Detail");
        }
        GridRequestJasa.DataBind();
        ClearForm();
        GridRequestJasa.DataBind();
        NotificationSuccess();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void ClearForm()
    {
      HdKdJasa.Value = null;
      TxtJasa.Text = string.Empty;
      TxtJmlJasa.Text = string.Empty;
      RDateTxtTglPelaksanaan.SelectedDate = null;
      TxtJmlHari.Text = string.Empty;
      TxtKeterangan.Text = string.Empty;
      TxtSatuan.Text = string.Empty;
      PnlInputJasa.Enabled = true;
      GridRequestJasa.Visible = true;
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
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

    protected void GridRequestJasa_ItemDataBound(object sender, GridItemEventArgs e)
    {
      string unitkerja = DlistUnitKerja.SelectedValue;
      if (unitkerja != "SHIPYARD")
      {
        (GridRequestJasa.MasterTableView.GetColumn("jmlhari") as GridBoundColumn).Display = false;
        (GridRequestJasa.MasterTableView.GetColumn("jmlorang") as GridBoundColumn).Display = false;
      }
    }

    protected void GridMasterJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void GridMasterJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseMasterJasaModalPopupExtender.Show();
    }

    protected void DlistUnitKerja_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DlistUnitKerja.SelectedValue == "SHIPYARD")
      {
        LblTitle.Text = "Input Work Request (Non Project)";
        PnlScope.Visible = true;
        PnlUsable.Visible = true;
      }
      else
      {
        LblTitle.Text = "Input Work Request";
        PnlScope.Visible = false;
        PnlUsable.Visible = false;
        PnlJmlOrang.Visible = false;
        PnlHari.Visible = false;
      }
    }

  }
}
