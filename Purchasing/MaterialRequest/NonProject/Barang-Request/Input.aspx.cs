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
using System.Globalization;
using TmsBackDataController.PurDataSetTableAdapters;


namespace ManagementSystem.Purchase.Material_Request.NonProject.Barang_Request
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (getUnitKerja == "SHIPYARD")
      {
        LblJudul.Text = "Input Material Request (Non Project)"; PnlSM.Visible = true;
      }
      else
      {
        LblJudul.Text = "Input Material Request"; PnlSM.Visible = false;
      }

      if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          MasterBarangDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrRow = purDataSet.pur_mr.Newpur_mrRow();

      DateTime tanggalMR = RadTglMR.SelectedDate.Value;
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
        mrRow.type = "NP";
        mrRow.project_id = 0;
        mrRow.departement = DlistDepartemen.SelectedValue;
        mrRow.lokasi_id = Convert.ToInt32(DlistLokasi.SelectedValue);
        mrRow.kategori_id = 0;
        if (getUnitKerja == "SHIPYARD")
        {
          mrRow.scope_id = Convert.ToInt32(DlistScope.SelectedValue);
          mrRow.usable_id = Convert.ToInt32(DlistUsable.SelectedValue);
        }
        else {
          mrRow.scope_id = 0;
          mrRow.usable_id = 0;
        }        
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
        PnlKeteranganBarang.Enabled = false;
        PnlBarang.Enabled = true;
        NotificationSuccess();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    private void ClearForm()
    {
      HdIdBarang.Value = null;
      TxtNamaBarang.Text = string.Empty;
      TxtJumlahBarang.Text = string.Empty;
      DlisJenisSatuan.SelectedIndex = 0;
      TxtKeterangan.Text = string.Empty;
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

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void BtnYa_CheckedChanged(object sender, EventArgs e)
    {
      ClearGenerate();
      GetGenerate();
      BtnRefresh.Visible = true;
    }

    private void GetGenerate()
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var scopeTableAdapter = new pur_scopeTableAdapter();
      var usableTableAdapter = new pur_usableTableAdapter();
      var lokasiTableAdapter = new pur_lokasiTableAdapter();

      DateTime tgl = RadTglMR.SelectedDate.Value;
      int year = tgl.Year;
      string type = "NP";
      string departement = DlistDepartemen.SelectedValue;
      string kdDepartement = PurchaseUtils.KodeDepartement(departement);      
      string idLokasi = DlistLokasi.SelectedValue;
      string lokasi = lokasiTableAdapter.ScalarGetInisialById(Convert.ToInt32(idLokasi));

      if (getUnitKerja == "SHIPYARD")
      {
        string idScope = DlistScope.SelectedValue;
        string scope = scopeTableAdapter.ScalarGetInisialById(Convert.ToInt32(idScope));
        string idUsable = DlistUsable.SelectedValue;
        string usable = usableTableAdapter.ScalarGetInisialById(Convert.ToInt32(idUsable));

        // Hitung Counter ...
        int count = Convert.ToInt32(mrTableAdapter.ScalarGetCountByTypeDepartementScopeIdUsableIdUnitKerjaTanggal(type, departement, Convert.ToInt32(idScope), Convert.ToInt32(idUsable), getUnitKerja, year));
        int countMR = count + 1;
        var result = countMR.ToString();
        var x = 4 - result.Length;
        for (var i = 0; i < x; i++)
        {
          result = string.Concat("0", result);
        }
        result = string.Concat(result);
        string counter = result;

        TxtRefer.Text = "MR" + "/" + lokasi + "/" + kdDepartement + "/" + scope + "/" + counter + "/" + year + "/" + usable;
      }
      else 
      {
        // Hitung Counter ..
        int count = Convert.ToInt32(mrTableAdapter.ScalarGetCountByTypeDepartementTanggalUnitKerja(type, departement, year, getUnitKerja));
        int countMR = count + 1;
        var result = countMR.ToString();
        var x = 4 - result.Length;
        for (var i = 0; i < x; i++)
        {
          result = string.Concat("0", result);
        }
        result = string.Concat(result);
        string counter = result;

        TxtRefer.Text = "MR" + "/" + lokasi + "/" + kdDepartement + "/" + counter + "/" + year + "/" + getUnitKerja;
      }
    }

    protected void BtnTidak_CheckedChanged1(object sender, EventArgs e)
    {
      ClearGenerate();
      BtnRefresh.Visible = false;
    }

    private void ClearGenerate()
    {
      TxtRefer.Visible = true;
      TxtRefer.ReadOnly = false;
      TxtRefer.Text = string.Empty;
    }

    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
      ClearGenerate();
      GetGenerate();
      
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
        mrdetailRow.type = "NP";
        mrdetailRow.barang_kode = HdIdBarang.Value;
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
        if (mrhistoryTableAdapter.FillByMrIdBarangKode(purDataSet.pur_mrhistory, TxtNoMR.Text, HdIdBarang.Value) > 0)
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
          mrhistoryRow.barang_kode = HdIdBarang.Value;
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
        PnlKeteranganBarang.Enabled = false;
        GridRequestBarang.DataBind();
        ClearForm();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
          //string id_barang = TxtIdBarang.Text;
          //string id_mrb = TxtIdMRB.Text;

          //var purDataSet = new NewPurDataSet();
          //var tabelRequestBarang = new pur_requestbarangTableAdapter();
          //tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_mrb));
          //try
          //{
          //    purDataSet.pur_requestbarang[0].requestnote_id = TxtNomorRequest.Text;
          //    purDataSet.pur_requestbarang[0].barang_kode = id_barang;
          //    purDataSet.pur_requestbarang[0].jumlah = Convert.ToInt32(TxtJumlahBarang.Text);
          //    purDataSet.pur_requestbarang[0].unit = 0;
          //    purDataSet.pur_requestbarang[0].namasatuan_id = DlisJenisSatuan.SelectedValue;
          //    purDataSet.pur_requestbarang[0].karyawan_createdbynik = getNIK;
          //    purDataSet.pur_requestbarang[0].deadline = DateDeadline.SelectedDate.Value;
          //    purDataSet.pur_requestbarang[0].status = "MR4";
          //    purDataSet.pur_requestbarang[0].keterangan = TxtKeterangan.Text;
          //    tabelRequestBarang.Update(purDataSet.pur_requestbarang);
          //    updateEditLogTabelRequestBarang(getUnitKerja, id_mrb);
          //    GridRequestBarang.Visible = true;
          //    GridRequestBarang.CurrentPageIndex = 0;
          //    GridRequestBarang.DataBind();
          //    BtnAdd.Visible = true;
          //    EnableAllInputBarang();
          //    notificationSuccess();
          //}
          //catch (Exception ex)
          //{
          //    notificationFailure(ex);
          //}
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

    protected void GridListBarang_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var purDataSet = new PurDataSet();
        var masterbarang01TableAdapter = new vmaster_barang01TableAdapter();

        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        masterbarang01TableAdapter.FillByKode(purDataSet.vmaster_barang01, gridDataItem["kode"].Text);

        HdIdBarang.Value = purDataSet.vmaster_barang01[0].kode;
        TxtNamaBarang.Text = purDataSet.vmaster_barang01[0].nama;

        PnlListBarangModalPopupExtender.Hide();
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

    public string getNIK
    {
      get
      {
        string result = Session["Nik"].ToString();
        return result;
      }
    }

    protected void BtnCariBarang_Click(object sender, EventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void TxtClose_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void GridListBarang_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void GridListBarang_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }
  }
}
