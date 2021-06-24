using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.SysDataSetTableAdapters;
using Telerik.Web.UI;
using ManagementSystem.Helper;
using System.Diagnostics;
using System.Globalization;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchase.Material_Request.Project
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (IsPostBack) {
        if (ViewState.Count > 0) {
          MasterBarangDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
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


    public string getUnitKerja
    {
      get
      {
        string result = DlistUnitKerja.SelectedValue;
        return result;
      }
    }

    #endregion 


    #region Function Notifications

    protected void NotificationSuccess()
    {
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void NotificationFailure(Exception ex)
    {
      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya.. ", ex.Message);
      PnlMessageModalPopupExtender.Show();
      // _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), "Gs.Ac.InputAkta.BtnSaveClick()", ex.Message);       
    }

    #endregion 
    
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrRow = purDataSet.pur_mr.Newpur_mrRow();

      DateTime tanggalMR = RDateMR.SelectedDate.Value;
      string id;

      if (getUnitKerja == "ASSEMBLING")
      {
        id = CUtils.GenerateId("pur_mr", tanggalMR.Year, tanggalMR.Month, "MAMR-");
      }
      else if (getUnitKerja == "SPARE PARTS") {
        id = CUtils.GenerateId("pur_mr", tanggalMR.Year, tanggalMR.Month, "MSMR-");
      }
      else
      {
        id = CUtils.GenerateId("pur_mr", tanggalMR.Year, tanggalMR.Month, "SMR-");
      }
     
      try
      {
        mrRow.id = id;
        mrRow.reference = TxtRefer.Text;
        mrRow.tanggal = tanggalMR;
        mrRow.type = "P";
        mrRow.project_id = Convert.ToInt32(DlistNomorProject.SelectedValue);
        mrRow.departement = "-";
        mrRow.lokasi_id = Convert.ToInt32(DlistKodeLokasi.SelectedValue);
        mrRow.kategori_id = Convert.ToInt32(DlistKodeKategori.SelectedValue);
        mrRow.scope_id = Convert.ToInt32(DlistKodeScope.SelectedValue);
        mrRow.usable_id = Convert.ToInt32(DlistUsable.SelectedValue);
        mrRow.status = "MR1";
        mrRow.unitkerja = getUnitKerja;
        mrRow.createdby = Session["FullName"].ToString();
        mrRow.datecreated = DateTime.Now;

        purDataSet.pur_mr.Addpur_mrRow(mrRow);
        if (mrTableAdapter.Update(purDataSet.pur_mr) > 0)
        {
          CUtils.UpdateSeed("pur_mr", tanggalMR.Year, tanggalMR.Month, tanggalMR);
          CUtils.UpdateLog("pur_mr", id, getUsername, "Input Material Request");
        }

        TxtNoMR.Text = id;
        PnlPermintaanBarang.Enabled = false;
        PnlBarang.Enabled = true;
        NotificationSuccess();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void BtnYa_CheckedChanged(object sender, EventArgs e)
    {
      BtnRefresh.Visible = true;
      GetGenerate();
    }

    protected void BtnTidak_CheckedChanged1(object sender, EventArgs e)
    {
      ClearGenerate();
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      ClearGenerate();
      GetGenerate();
    }

    protected void ClearGenerate()
    {
      TxtRefer.Visible = true;
      TxtRefer.ReadOnly = false;
      TxtRefer.Text = string.Empty;
    }

    protected void GetGenerate()
    {
      var purDataSet = new PurDataSet();
      var lokasiTableAdapter = new pur_lokasiTableAdapter();
      var masterprojectTableAdapter = new master_projectTableAdapter();
      var kategoriTableAdapter = new pur_kategoriTableAdapter();
      var scopeTableAdapter = new pur_scopeTableAdapter();
      var usableTableAdapter = new pur_usableTableAdapter();

      DateTime date = RDateMR.SelectedDate.Value;
      int idReqLokasi = Convert.ToInt32(DlistKodeLokasi.SelectedValue);
      string kdReqLokasi = lokasiTableAdapter.ScalarGetInisialById(idReqLokasi);
      int idProject = Convert.ToInt32(DlistNomorProject.SelectedValue);
      string noProject = masterprojectTableAdapter.ScalarGetNomorProjectById(idProject);
      int idKategori = Convert.ToInt32(DlistKodeKategori.SelectedValue);
      string kdKategori = kategoriTableAdapter.ScalarGetInisialById(idKategori);
      int idScope = Convert.ToInt32(DlistKodeScope.SelectedValue);
      string kdScope = scopeTableAdapter.ScalarGetInisialById(idScope);
      int idUsable = Convert.ToInt32(DlistUsable.SelectedValue);
      string kdUsable = usableTableAdapter.ScalarGetInisialById(idUsable);

      var counter = PurchaseUtils.GetNewCounterMR(idProject, date);
      TxtRefer.Text = "MR" + "/" + kdReqLokasi + "/" + noProject + "/" + kdKategori + "/" + kdScope + "/" + counter + "/" + date.Year.ToString() + "/" + kdUsable;

      TxtRefer.ReadOnly = true;
      TxtRefer.Visible = true;
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrdetailRow = purDataSet.pur_mrdetail.Newpur_mrdetailRow();
      var mrhistoryTableAdapter = new pur_mrhistoryTableAdapter();
      var mrhistoryRow = purDataSet.pur_mrhistory.Newpur_mrhistoryRow();

      try
      {
        mrdetailRow.mr_id = TxtNoMR.Text;
        mrdetailRow.type = "P";
        mrdetailRow.barang_kode = HdKdBarang.Value;
        mrdetailRow.jumlah = TxtJumlahBarang.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlahBarang.Text);
        mrdetailRow.satuan_id = Convert.ToInt32(DlisJenisSatuan.SelectedValue);
        mrdetailRow.tglpemenuhan = RDatePemenuhan.SelectedDate.Value;
        mrdetailRow.status = "B1";
        mrdetailRow.keterangan = TxtKeterangan.Text;
        mrdetailRow.createdby = Session["FullName"].ToString();
        mrdetailRow.datecreated = DateTime.Now;
                
        purDataSet.pur_mrdetail.Addpur_mrdetailRow(mrdetailRow);
        if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
        {
          CUtils.UpdateLog("pur_mrdetail", (mrdetailTableAdapter.ScalarGetMaxId() + 1).ToString(), getUsername, "Input MR Detail");
        }

        //Cek MRHistory..
        //Update MR History        
        if (mrhistoryTableAdapter.FillByMrIdBarangKode(purDataSet.pur_mrhistory, TxtNoMR.Text, HdKdBarang.Value) > 0)
        {
          string idMrHistory = purDataSet.pur_mrhistory[0].id.ToString();
          int jumlah1 = purDataSet.pur_mrhistory[0].jumlah;

          mrhistoryTableAdapter.FillById(purDataSet.pur_mrhistory, Convert.ToInt32(idMrHistory));
          purDataSet.pur_mrhistory[0].jumlah = jumlah1 + Convert.ToInt32(TxtJumlahBarang.Text);

          if (mrhistoryTableAdapter.Update(purDataSet.pur_mrhistory) > 0)
          {
            CUtils.UpdateLog("pur_mrhistory", idMrHistory.ToString(), getUsername, "Update MR History.");
          }
        }
        //  Insert History Request..
        else 
        {
          mrhistoryRow.mr_id = TxtNoMR.Text;
          mrhistoryRow.barang_kode = HdKdBarang.Value;
          mrhistoryRow.jumlah = Convert.ToInt32(TxtJumlahBarang.Text);
          mrhistoryRow.jumlahsisa = 0;

          purDataSet.pur_mrhistory.Addpur_mrhistoryRow(mrhistoryRow);
          if (mrhistoryTableAdapter.Update(purDataSet.pur_mrhistory) > 0)
          {
            CUtils.UpdateLog("pur_mrhistory", (mrhistoryTableAdapter.ScalarGetMaxId() + 1).ToString(), getUsername, "Insert MR History.");
          }
        }

        GridRequestBarang.Visible = true;
        PnlBarang.Visible = true;
        PnlPermintaanBarang.Enabled = false;  
        GridRequestBarang.DataBind();
        ClearForm();
      }
      catch (Exception ex) {
        NotificationFailure(ex);
      }
    }

    private void ClearForm() {
      HdIdReqBarang.Value = null;
      HdKdBarang.Value = null;
      TxtNamaBarang.Text = string.Empty;
      TxtJumlahBarang.Text = string.Empty;
      DlisJenisSatuan.SelectedIndex = 0;
      TxtKeterangan.Text = string.Empty;
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
      //var purDataSet = new PurDataSet();
      //var requestbarangTableAdapter = new pur_requestbarangTableAdapter();
      //var vrequestbarang01TableAdapter = new vpur_requestbarang01TableAdapter();
      //var historyrequestTableAdapter = new pur_historyrequestTableAdapter();

      //vrequestbarang01TableAdapter.FillById(purDataSet.vpur_requestbarang01, Convert.ToInt32(HdIdReqBarang));

      //try
      //{
      //  purDataSet.pur_requestbarang[0].requestnote_id = TxtNoMR.Text;
      //  purDataSet.pur_requestbarang[0].type = "P";
      //  purDataSet.pur_requestbarang[0].barang_kode = HdKdBarang.Value;
      //  purDataSet.pur_requestbarang[0].jumlah = Convert.ToInt32(TxtJumlahBarang.Text);
      //  purDataSet.pur_requestbarang[0].satuan_id = Convert.ToInt32(DlisJenisSatuan.SelectedValue);
      //  purDataSet.pur_requestbarang[0].tglpemenuhan = RDatePemenuhan.SelectedDate.Value;
      //  purDataSet.pur_requestbarang[0].keterangan = TxtKeterangan.Text;

      //  if (requestbarangTableAdapter.Update(purDataSet.pur_requestbarang) > 0) {
      //    CUtils.UpdateLog("pur_requestbarang", HdIdReqBarang.Value, getUsername, "Update Request Barang.");
      //  }

      //  GridRequestBarang.Visible = true;
      //  GridRequestBarang.DataBind();
      //  ClearForm();
      //  BtnAdd.Visible = true;
      //  BtnUpdate.Visible = false;
      //}
      //catch (Exception ex)
      //{
      //  NotificationFailure(ex);
      //}          
    }

    protected void BtnCariBarang_Click(object sender, EventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
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
        MasterBarangDataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByKodeUnitKerja");
      }
      else if (pilihan == "1") // Nama Barang..
      {
        MasterBarangDataSource.SelectMethod = "GetDataByNamaUnitKerja";
        MasterBarangDataSource.SelectParameters.Clear();
        MasterBarangDataSource.SelectParameters.Add("nama", txtKataKunci);
        MasterBarangDataSource.SelectParameters.Add("unitkerja", getUnitKerja);

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
      MasterBarangDataSource.SelectParameters.Add("unitkerja", getUnitKerja);

      ViewState.Add("SelectMethod", "GetDataByUnitKerja");
      
      PnlListBarangModalPopupExtender.Show();
      GridListBarang.CurrentPageIndex = 0;
      GridListBarang.DataBind();
    }

    protected void GridRequestBarang_ItemCommand(object source, GridCommandEventArgs e)
    {
      var gridDataItem = (GridDataItem)e.Item;
      if (gridDataItem == null) return;

      var purDataSet = new PurDataSet();
      var vmrdetail01TableAdapter = new vpur_mrdetail01TableAdapter();

      var id = gridDataItem["id"].Text;

      if (e.CommandName == "RowClick") 
      {
        if (vmrdetail01TableAdapter.FillById(purDataSet.vpur_mrdetail01, Convert.ToInt32(id)) <= 0) return;

        try
        {
          BtnUpdate.Visible = true;
          BtnAdd.Visible = false;

          HdIdReqBarang.Value = purDataSet.vpur_mrdetail01[0].id.ToString();
          HdKdBarang.Value = purDataSet.vpur_mrdetail01[0].barang_kode;
          TxtNamaBarang.Text = purDataSet.vpur_mrdetail01[0].barang_nama;
          TxtJumlahBarang.Text = purDataSet.vpur_mrdetail01[0].jumlah.ToString();
          DlisJenisSatuan.SelectedValue = purDataSet.vpur_mrdetail01[0].satuan_id.ToString();
          RDatePemenuhan.SelectedDate = purDataSet.vpur_mrdetail01[0].tglpemenuhan;
          TxtKeterangan.Text = purDataSet.vpur_mrdetail01[0].keterangan;
        }
        catch (Exception ex)
        {
          NotificationFailure(ex);
        }
      }
    }

    //protected void GridRequestBarang_ItemDataBound(object sender, GridItemEventArgs e)
    //{
    //  if (e.Item is GridDataItem)
    //  {
    //    GridDataItem item = e.Item as GridDataItem;
    //    string status = item["status"].Text;
    //    status = PurchaseUtils.GetStatusBarang(status);
    //  }    
    //}

    protected void GridListBarang_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var purDataSet = new PurDataSet();
        var masterbarang01TableAdapter = new vmaster_barang01TableAdapter();

        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        masterbarang01TableAdapter.FillByKode(purDataSet.vmaster_barang01, gridDataItem["kode"].Text);

        HdKdBarang.Value = purDataSet.vmaster_barang01[0].kode;
        TxtNamaBarang.Text = purDataSet.vmaster_barang01[0].nama;

        PnlListBarangModalPopupExtender.Hide();
      }
    }      

    protected void GridListBarang_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void GridListBarang_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void TxtClose_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }   
  }
}
