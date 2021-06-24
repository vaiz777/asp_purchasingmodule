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

namespace ManagementSystem.Purchasing.Material_Request.Project.Price_Request
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    //CultureInfo indo = new CultureInfo("id-ID");
    //CultureInfo amerika = new CultureInfo("en-US");
    //CultureInfo euro = new CultureInfo("fr-FR");
    //CultureInfo aussie = new CultureInfo("en-AU");
    //CultureInfo singapore = new CultureInfo("en-SG");
    //public static string id;

    protected void Page_Load(object sender, EventArgs e)
    {
      var id = Request.QueryString["pId"];
      TxtNomorRequest.Text = id;
            
      if (IsPostBack) 
      {
        if (ViewState.Count > 0) 
        {
          SupplierDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    #region Function Notification

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

    protected void NotificationSuccess()
    {
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    #endregion 

    protected void BtnChoice_Click(object sender, EventArgs e)
    {
      PnlRequestedBarangModalPopupExtender.Show();
    }

    protected void BtnOption1_Click(object sender, EventArgs e)
    {
      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrhistoryTableAdapter = new pur_mrhistoryTableAdapter();
      var historyRow = purDataSet.pur_mrhistory.Newpur_mrhistoryRow();
      var mrTableAdapter = new pur_mrTableAdapter();

      string idReqNote = TxtNomorRequest.Text;
      string idReqBarang = HdIdReqBarang.Value;
      mrTableAdapter.FillById(purDataSet.pur_mr, idReqNote);
      string statusAwal = purDataSet.pur_mr[0].status;

      try
      {
        if (mrhistoryTableAdapter.FillByMrIdBarangKode(purDataSet.pur_mrhistory, idReqNote, HdKodeBarang.Value) > 0)
        {
          actionifItemExist(idReqBarang, idReqNote);
        }
        else
        {
          actionifItemNew(idReqBarang, idReqNote);
        }
        //Update status Request Note        
        purDataSet.pur_mr[0].status = PurchaseUtils.GetStatusSetHarga(statusAwal);
        if (mrTableAdapter.Update(purDataSet.pur_mr) > 0)
        {
          CUtils.UpdateLog("pur_mr", idReqNote, getUsername, "Update status MR (Set Harga)");
        }
        ClearForm();
        GridHarga.DataBind();
        GridBarangRequested.DataBind();
      }
      catch (Exception ex) 
      {
        NotificationFailure(ex);
      }      
    }

    private void actionifItemExist(string idReqBarang, string idReqNote)
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();

      if (mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(idReqBarang)) > 0)
      {
        purDataSet.pur_mrdetail[0].jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
        purDataSet.pur_mrdetail[0].keterangan = TxtKeterangan.Text;
        purDataSet.pur_mrdetail[0].supplier_id = Convert.ToInt32(HdIdSupplier.Value);
        purDataSet.pur_mrdetail[0].harga = RadTxtHarga.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtHarga.Text);
        purDataSet.pur_mrdetail[0].currency = DlistCurrency.SelectedValue;
        purDataSet.pur_mrdetail[0].status = "B2";

        if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
        {
          CUtils.UpdateLog("pur_mrdetail", idReqBarang, getUsername, "Update Set Harga MR Detail ");
        }
        //int jumlah = Convert.ToInt32(historyrequestTableAdapter.FillByRequestnoteIdBarangKode(purDataSet.pur_historyrequest, idReqNote, HdKodeBarang.Value));
        //if (jumlah >= Convert.ToInt32(TxtJumlah.Text))
        //{
        //  if (requestbarangTableAdapter.Update(purDataSet.pur_requestbarang) > 0)
        //  {
        //    CUtils.UpdateLog("pur_requestbarang", idReqBarang, getUsername, "Request barang Set Harga");
        //  }
        //}
        //else
        //{
        //  PnlMessageLblTitlebar.Text = "Oops!";
        //  PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
        //  PnlMessageLblMessage.Text = "Gagal. Jumlah melebihi inputan awal.";
        //  PnlMessageModalPopupExtender.Show();
        //}
      }
    }

    private void actionifItemNew(string idReqBarang, string idReqnote) 
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrhistoryTableAdapter = new pur_mrhistoryTableAdapter();
      var historyRow = purDataSet.pur_mrhistory.Newpur_mrhistoryRow();

      // Insert History Request
      historyRow.mr_id = TxtNomorRequest.Text;
      historyRow.barang_kode = HdKodeBarang.Value;
      historyRow.jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
      historyRow.jumlahsisa = 0;

      purDataSet.pur_mrhistory.Addpur_mrhistoryRow(historyRow);
      if (mrhistoryTableAdapter.Update(purDataSet.pur_mrhistory) > 0)
      {
        CUtils.UpdateLog("pur_mrhistory", (Convert.ToInt32(mrhistoryTableAdapter.ScalarGetMaxId() + 1)).ToString(), getUsername, "Insert MR History");
      }

      // Update Request Barang
      if (mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(idReqBarang)) > 0)
      {
        purDataSet.pur_mrdetail[0].jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
        purDataSet.pur_mrdetail[0].keterangan = TxtKeterangan.Text;
        purDataSet.pur_mrdetail[0].supplier_id = Convert.ToInt32(HdIdSupplier.Value);
        purDataSet.pur_mrdetail[0].harga = RadTxtHarga.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtHarga.Text);
        purDataSet.pur_mrdetail[0].currency = DlistCurrency.SelectedValue;
        purDataSet.pur_mrdetail[0].status = "B2";

        if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
        {
          CUtils.UpdateLog("pur_mrdetail", idReqBarang, getUsername, "Update Set Harga MR Detail");
        }
      }
    }

    protected void PnlViewBarangBtnClose_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void GridSupplier_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;
        string id = gridDataItem["id"].Text;

        var purDataSet = new PurDataSet();
        var supplierTableAdapter = new master_supplierTableAdapter();

        supplierTableAdapter.FillById(purDataSet.master_supplier, Convert.ToInt32(id));

        TxtSupplier.Text = purDataSet.master_supplier[0].nama;
        HdIdSupplier.Value = purDataSet.master_supplier[0].id.ToString();

        PnlViewSupplierModalPopupExtender.Hide();
      }
    }

    protected void GridBarangRequested_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick") 
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string idReqBarang = gridDataItem["id"].Text;

        var purDataSet = new PurDataSet();
        var vmrdetail01TableAdapter = new vpur_mrdetail01TableAdapter();

        vmrdetail01TableAdapter.FillById(purDataSet.vpur_mrdetail01, Convert.ToInt32(idReqBarang));

        try
        {
          HdIdReqBarang.Value = purDataSet.vpur_mrdetail01[0].id.ToString();
          HdKodeBarang.Value = purDataSet.vpur_mrdetail01[0].barang_kode;
          TxtNamaBarang.Text = purDataSet.vpur_mrdetail01[0].barang_nama;
          TxtJumlah.Text = purDataSet.vpur_mrdetail01[0].jumlah.ToString();
          TxtSatuan.Text = purDataSet.vpur_mrdetail01[0].satuan_nama;
          TxtKeterangan.Text = purDataSet.vpur_mrdetail01[0].keterangan;
        }
        catch (Exception ex)
        {
          NotificationFailure(ex);
        }
        
        PnlRequestedBarangModalPopupExtender.Hide();
      }
    }

    protected void BtnCariSupplier_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");

      ViewState.Remove("SelectMethod");

      SupplierDataSource.SelectMethod = "GetDataLikeNama";
      SupplierDataSource.SelectParameters.Clear();
      SupplierDataSource.SelectParameters.Add("nama", txtKataKunci);
      ViewState.Add("SelectMethod", "GetDataLikeNama");

      GridSupplier.DataBind();

      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void ClearForm()
    {
      HdIdReqBarang.Value = null;
      TxtNamaBarang.Text = string.Empty;
      HdKodeBarang.Value = null;
      TxtJumlah.Text = string.Empty;
      TxtSatuan.Text = string.Empty;
      TxtKeterangan.Text = string.Empty;
      HdIdSupplier.Value = null;
      TxtSupplier.Text = string.Empty;
      DlistCurrency.SelectedIndex = 0;
      RadTxtTotal.Text = string.Empty;

      GridBarangRequested.CurrentPageIndex = 0;
      GridBarangRequested.DataBind();
    }

    protected void GridHarga_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "DeleteClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;
        string id = gridDataItem["id"].Text;

        DeleteItem(id);
        GridBarangRequested.DataBind();
        GridHarga.DataBind();
      }
    }

    protected void BtnSuggest_Click(object sender, EventArgs e)
    {
      PnlSuggestionsPriceModalPopupExtender.Show();
    }

    protected void GridSuggestions_ItemCommand(object source, GridCommandEventArgs e)
    {
        //if (e.CommandName == "RowClick")
        //{
        //    var gridDataItem = (GridDataItem)e.Item;
        //    if (gridDataItem == null) return;
        //    string history = gridDataItem["harga"].Text;
        //    TxtHarga1.Text = history;

        //    double sum = 0;
        //    double num1;
        //    double num2;

        //    if (!Double.TryParse(TxtHarga1.Text, out num1))
        //    {
        //        return;
        //    }
        //    if (!Double.TryParse(TxtJumlah.Text, out num2))
        //    {
        //        return;
        //    }
        //    sum = num1 * num2;
        //    TxtTotal.Text = sum.ToString();

            
        //}
    }

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

   
    protected void BtnTambah_Click(object sender, EventArgs e)
    {
      var url = string.Concat("EditMR.aspx?pId=", TxtNomorRequest.Text);
      Response.Redirect(url);
    }

    protected void GridSupplier_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void RadTxtHarga_TextChanged(object sender, EventArgs e)
    {
      int jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
      decimal harga = RadTxtHarga.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtHarga.Text);

      RadTxtTotal.Text = (jumlah * harga).ToString();
    }

    protected void GridHarga_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;
        string status = gridDataItem["status"].Text;
        string id = gridDataItem["id"].Text;
        Button btnHapus = gridDataItem.FindControl("btnDelete") as Button;

        if (status == "B2" || status == "Set Harga") 
        {
          btnHapus.Visible = true;
        }

        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status);
      }
    }

    private void DeleteItem(string idReqBarang) 
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrTableAdapter = new pur_mrTableAdapter();

      mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(idReqBarang));
      purDataSet.pur_mrdetail[0].status = "B1";
      purDataSet.pur_mrdetail[0].supplier_id = 0;
      purDataSet.pur_mrdetail[0].harga = 0;
      purDataSet.pur_mrdetail[0].currency = "-";
      if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0) 
      {
        CUtils.UpdateLog("pur_mrdetail", idReqBarang, getUsername, "Update MR Detail (Delete Set Harga)");
      }
    }

    protected void GridBarangRequested_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;
        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status);
      }
    }

    protected void GridCekHarga_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        RadTxtHarga.Value = Convert.ToDouble(gridDataItem["harga"].Text);
        
        int jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
        double harga = Convert.ToDouble(gridDataItem["harga"].Text);

        RadTxtTotal.Text = (jumlah * harga).ToString();

        PnlSuggestionsPriceModalPopupExtender.Hide();
      }
    }
  }
}
