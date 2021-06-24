using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using ManagementSystem.Helper;
using TmsBackDataController.SysDataSetTableAdapters;
using System.Data;
using Telerik.Web.UI;
using System.Web.Security;
using System.Drawing;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchasing.Material_Request.Project.Price_Request
{
  public partial class EditMR : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();
    //public static string id, note, predef;

    protected void Page_Load(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vmr01TableAdapter = new vpur_mr01TableAdapter();

      var id = Request.QueryString["pId"];
      LblNoMR.Text = id;
      
      if (!IsPostBack)
      {
        if (vmr01TableAdapter.FillById(purDataSet.vpur_mr01, id) <= 0) return;
        try
        {
          LblNoMR.Text = purDataSet.vpur_mr01[0].id;
          LblTglMR.Text = purDataSet.vpur_mr01[0].tanggal.ToShortDateString();
          LblReference.Text = purDataSet.vpur_mr01[0].reference;
          LblNomorProject.Text = purDataSet.vpur_mr01[0].project_nomor;
          LblLokasi.Text = purDataSet.vpur_mr01[0].lokasi_nama;
          LblKategori.Text = purDataSet.vpur_mr01[0].kategori_nama;
          LblScope.Text = purDataSet.vpur_mr01[0].scope_nama;
          LblUsable.Text = purDataSet.vpur_mr01[0].usable_nama;
          LblCreatedby.Text = String.Concat(purDataSet.vpur_mr01[0].createdby + " ( " + purDataSet.vpur_mr01[0].datecreated + " ) ");
        }
        catch (Exception ex)
        {
          NotificationFailure(ex);
        }
      }
    }

    #region Panel Messages

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
      var stackTrace = new StackTrace(ex, true);
      var frame = stackTrace.GetFrame(0);

      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>", frame.GetMethod(), "(", frame.GetFileLineNumber(), "):<br>", ex.Message);
      PnlMessageModalPopupExtender.Show();

      _errorLogTableAdapter.Insert(DateTime.Now, getUsername, string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
    }    

    protected void NotAuthorize()
    {
      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Operasi tidak diijinkan.");
      PnlMessageModalPopupExtender.Show();
    }
    #endregion
    

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrhistoryTableAdapter = new pur_mrhistoryTableAdapter();

      string idRequestBarang = HdIdReqBarang.Value;
      mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(idRequestBarang));

      try
      {
        purDataSet.pur_mrdetail[0].barang_kode = HdKdBarang.Value;
        purDataSet.pur_mrdetail[0].jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
        purDataSet.pur_mrdetail[0].satuan_id = Convert.ToInt32(DlistSatuan.SelectedValue);
        purDataSet.pur_mrdetail[0].tglpemenuhan = RDateTgl.SelectedDate.Value;
        purDataSet.pur_mrdetail[0].keterangan = TxtKeterangan.Text;

        if (mrhistoryTableAdapter.FillByMrIdBarangKode(purDataSet.pur_mrhistory, LblNoMR.Text, HdKdBarang.Value) > 0)
        {
          string idHistory = purDataSet.pur_mrhistory[0].id.ToString();
          int jmlsisa = Convert.ToInt32(purDataSet.pur_mrhistory[0].jumlahsisa);
          int jml = Convert.ToInt32(purDataSet.pur_mrhistory[0].jumlah);
          if (jml >= Convert.ToInt32(TxtJumlah.Text))
          {
            //Update Jml sisa..
            mrhistoryTableAdapter.FillById(purDataSet.pur_mrhistory, Convert.ToInt32(idHistory));
            if (jmlsisa == 0)
            {
              purDataSet.pur_mrhistory[0].jumlahsisa = jmlsisa + (jml - (Convert.ToInt32(TxtJumlah.Text)));
            }
            else 
            {
              purDataSet.pur_mrhistory[0].jumlahsisa = jmlsisa + (jmlsisa - (Convert.ToInt32(TxtJumlah.Text)));
            }
            if (mrhistoryTableAdapter.Update(purDataSet.pur_mrhistory) > 0) 
            {
              CUtils.UpdateLog("pur_mrhistory", idHistory, getUsername, "Update Jumlah sisa");
            }

            // Update MR Detail
            if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
            {
              CUtils.UpdateLog("pur_mrdetail", idRequestBarang, getUsername, "Update MR Detail");
            }
            NotificationSuccess();
          }
          else
          {
            PnlMessageLblTitlebar.Text = "Oops!";
            PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
            PnlMessageLblMessage.Text = "Gagal. Jumlah melebihi inputan awal.";
            PnlMessageModalPopupExtender.Show();
          }
        }
        else 
        {
          if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
          {
            CUtils.UpdateLog("pur_mrdetail", idRequestBarang, getUsername, "Update MR Detail");
          }
          NotificationSuccess();
        }

        ClearForm();
        PnlViewModalPopupExtender.Hide();
        GridBarangRequested.CurrentPageIndex = 0;
        GridBarangRequested.DataBind();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void GridBarangRequested_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vmrdetail01TableAdapter = new vpur_mrdetail01TableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrhistoryTableAdapter = new pur_mrhistoryTableAdapter();

      string noMR = LblNoMR.Text;

      if (e.CommandName == "RowClick")
      {
        //var gridDataItem = (GridDataItem)e.Item;
        //if (gridDataItem == null) return;

        //var id = gridDataItem["id"].Text;

        //vmrdetail01TableAdapter.FillById(purDataSet.vpur_mrdetail01, Convert.ToInt64(id));
        //string status = purDataSet.vpur_mrdetail01[0].status;

        //if (status == "B1" || status == "Baru")
        //{
        //  HdIdReqBarang.Value = purDataSet.vpur_mrdetail01[0].id.ToString();
        //  TxtBarang.Text = purDataSet.vpur_mrdetail01[0].barang_nama;
        //  RDateTgl.SelectedDate = purDataSet.vpur_mrdetail01[0].tglpemenuhan;
        //  HdKdBarang.Value = purDataSet.vpur_mrdetail01[0].barang_kode;
        //  TxtJumlah.Text = purDataSet.vpur_mrdetail01[0].jumlah.ToString();
        //  DlistSatuan.SelectedValue = purDataSet.vpur_mrdetail01[0].satuan_id.ToString();
        //  TxtKeterangan.Text = purDataSet.vpur_mrdetail01[0].keterangan;

        //  LblNamaForm.Text = "Update Barang";
        //  PnlViewBtnUpdate.Visible = true;
        //  PnlViewBtnInsert.Visible = false;
        //  RDateTgl.Enabled = false;
        //  TxtBarang.ReadOnly = true;

        //  PnlViewModalPopupExtender.Show();
        //}
        //else
        //{
        //  NotAuthorize();
        //}
      }
      else if (e.CommandName == "DeleteClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["id"].Text;

        //Update MR History
        mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(id));
        string barangkode = purDataSet.pur_mrdetail[0].barang_kode;
        int jmlMRDetail = purDataSet.pur_mrdetail[0].jumlah;

        if (mrhistoryTableAdapter.FillByMrIdBarangKode(purDataSet.pur_mrhistory, noMR, barangkode) > 0)
        {
          string idHistory = purDataSet.pur_mrhistory[0].id.ToString();
          int jmlMRHistory = purDataSet.pur_mrhistory[0].jumlahsisa;

          mrhistoryTableAdapter.FillById(purDataSet.pur_mrhistory, Convert.ToInt32(idHistory));
          purDataSet.pur_mrhistory[0].jumlahsisa = jmlMRDetail + jmlMRHistory;
          if (mrhistoryTableAdapter.Update(purDataSet.pur_mrhistory) > 0)
          {
            CUtils.UpdateLog("pur_mrhistory", idHistory, getUsername, "Update MR History");
          }
        }

        // Detele MR Detail
        if (mrdetailTableAdapter.DeleteQueryById(Convert.ToInt64(id)) > 0)
        {
          CUtils.UpdateLog("pur_mrdetail", id, getUsername, "Delete MR Detail.");
        }

        // Update status MR
        mrTableAdapter.FillById(purDataSet.pur_mr, noMR);
        string statusMR = purDataSet.pur_mr[0].status;
        if (statusMR == "HR2" || statusMR == "HR3") // status MR Verified
        {
          PurchaseUtils.UpdateStatusRequestNote('v', noMR, getUsername);
        }
        else if (statusMR == "HR4" || statusMR == "HR5") // status MR Approved
        {
          PurchaseUtils.UpdateStatusRequestNote('a', noMR, getUsername);
        }     
        
        GridBarangRequested.DataBind();
        Response.Redirect(Request.RawUrl);
      }
    }

    private void DeleteMRDetail(string id, string noMR) 
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrTableAdapter = new pur_mrTableAdapter();
      

            
    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
      var url = string.Concat("Input.aspx?pId=", LblNoMR.Text);
      Response.Redirect(url);
    }

    protected void BtnTambah_Click(object sender, EventArgs e)
    {
      PnlViewModalPopupExtender.Show();
      PnlViewBtnInsert.Visible = true;
      PnlViewBtnUpdate.Visible = false;
      LblNamaForm.Text = "Tambah Barang";
    }

    protected void GridListBarang_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var purDataSet = new PurDataSet();
        var vmasterbarang01TableAdapter = new vmaster_barang01TableAdapter();

        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string kode = gridDataItem["kode"].Text;

        vmasterbarang01TableAdapter.FillByKode(purDataSet.vmaster_barang01, kode);

        HdKdBarang.Value = purDataSet.vmaster_barang01[0].kode;
        TxtBarang.Text = purDataSet.vmaster_barang01[0].nama;

        PnlViewModalPopupExtender.Show();
      }
    }

    protected void PnlViewBtnInsert_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrdetailRow = purDataSet.pur_mrdetail.Newpur_mrdetailRow();
      var mrhistoryTableAdapter = new pur_mrhistoryTableAdapter();
      //var historyrequestRow = purDataSet.pur_historyrequest.Newpur_historyrequestRow();

      try
      {
        // Insert request_barang..
        mrdetailRow.mr_id = LblNoMR.Text;
        mrdetailRow.type = "P";
        mrdetailRow.barang_kode = HdKdBarang.Value;
        mrdetailRow.jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
        mrdetailRow.satuan_id = Convert.ToInt32(DlistSatuan.SelectedValue);
        mrdetailRow.tglpemenuhan = RDateTgl.SelectedDate.Value;
        mrdetailRow.createdby = Session["FullName"].ToString();
        mrdetailRow.status = "B1";
        mrdetailRow.keterangan = TxtKeterangan.Text;
        mrdetailRow.supplier_id = 0;
        mrdetailRow.harga = 0;
        mrdetailRow.currency = "-";
        mrdetailRow.datecreated = DateTime.Now;

        if (mrhistoryTableAdapter.FillByMrIdBarangKode(purDataSet.pur_mrhistory, LblNoMR.Text, HdKdBarang.Value) > 0) 
        {
          int y = Convert.ToInt32(purDataSet.pur_mrhistory[0].jumlahsisa);
          if (y >= Convert.ToInt32(TxtJumlah.Text))
          {
            purDataSet.pur_mrdetail.Addpur_mrdetailRow(mrdetailRow);
            if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
            {
              CUtils.UpdateLog("pur_mrdetail", (Convert.ToInt32(mrdetailTableAdapter.ScalarGetMaxId()) + 1).ToString(), getUsername, "Insert MR Detail");
            }
            NotificationSuccess();
          }
          else 
          {
            PnlMessageLblTitlebar.Text = "Oops!";
            PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
            PnlMessageLblMessage.Text = "Gagal. Jumlah melebihi inputan awal.";
            PnlMessageModalPopupExtender.Show();
          }
        }        
        else
        {
          purDataSet.pur_mrdetail.Addpur_mrdetailRow(mrdetailRow);
          if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
          {
            CUtils.UpdateLog("pur_mrdetail", (Convert.ToInt32(mrdetailTableAdapter.ScalarGetMaxId()) + 1).ToString(), getUsername, "Insert MR Detail");
          }

          NotificationSuccess();
          GridBarangRequested.DataBind();
        }

        ClearForm();
        GridBarangRequested.DataBind();
        PnlViewModalPopupExtender.Hide();

        // Insert history request..
        //historyrequestRow.requestnote_id = LblNoMR.Text;
        //historyrequestRow.barang_kode = HdKdBarang.Value;
        //historyrequestRow.jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);

        //purDataSet.pur_historyrequest.Addpur_historyrequestRow(historyrequestRow);
        //if (historyrequestTableAdapter.Update(purDataSet.pur_historyrequest) > 0) {
        //  CUtils.UpdateLog("pur_historyrequest", (Convert.ToInt32(historyrequestTableAdapter.ScalarGetMaxId()) + 1).ToString(), getUsername, "Insert History Request");
        //}        
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void PnlViewBtnClose_Click(object sender, EventArgs e)
    {
      ClearForm();
      PnlViewModalPopupExtender.Hide();
    }

    private void ClearForm() 
    {
      HdIdReqBarang.Value = null;
      RDateTgl.SelectedDate = null;
      TxtBarang.Text = string.Empty;
      HdKdBarang.Value = null;
      TxtJumlah.Text = string.Empty;
      DlistSatuan.SelectedIndex = 0;
      TxtKeterangan.Text = string.Empty;
    }

    protected void BtnCariDataBarang_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");

      ViewState.Remove("SelectMethod");

      string pilihan = DlistJenisBarang.SelectedValue;

      if (pilihan == "0")
      {
        MasterBarangDataSource.SelectMethod = "GetDataByKodeUnitKerja";
        MasterBarangDataSource.SelectParameters.Clear();
        MasterBarangDataSource.SelectParameters.Add("kode", txtKataKunci);
        MasterBarangDataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByKodeUnitKerja");
      }
      else if (pilihan == "1")
      {
        MasterBarangDataSource.SelectMethod = "GetDataByNamaUnitKerja";
        MasterBarangDataSource.SelectParameters.Clear();
        MasterBarangDataSource.SelectParameters.Add("nama", txtKataKunci);
        MasterBarangDataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByNamaUnitKerja");
      }

      PnlListBarangModalPopupExtender.Show();
      GridListBarang.CurrentPageIndex = 0;
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
      
      GridListBarang.CurrentPageIndex = 0;
      GridListBarang.DataBind();

      PnlListBarangModalPopupExtender.Show();
    }
          
    protected void GridBarangRequested_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;
        Button btnDelete = item.FindControl("RadPushButton1") as Button;
        string status = item["status"].Text;

        if (status == "B1") // Baru..
        {
          btnDelete.Visible = true;
        }

        item["status"].Text = PurchaseUtils.GetStatusBarang(status);
      }
    }

    #region FUnction GetSession

    public string getUnitKerja
    {
      get
      {
        string result = Session["UnitKerja"].ToString();
        return result;
      }
    }

    public string getUsername
    {
      get
      {
        string result = Session["Username"].ToString();
        return result;
      }
    }

    #endregion


    protected void GridListBarang_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void GridListBarang_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

    protected void BtnBrowseBarang_Click(object sender, EventArgs e)
    {
      PnlListBarangModalPopupExtender.Show();
    }

  }    
}
