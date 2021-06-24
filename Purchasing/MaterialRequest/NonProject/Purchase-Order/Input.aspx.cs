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
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchasing.Material_Request.NonProject.Purchase_Order
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        HdIdPO.Value = GetIdPO();

        //Supplier & No Project
        var purDataSet = new PurDataSet();
        var vpodetail02TableAdapter = new TmsBackDataController.PurDataSetTableAdapters.vpur_podetail02TableAdapter();

        if (vpodetail02TableAdapter.FillByPoId(purDataSet.vpur_podetail02, HdIdPO.Value) > 0)
        {
          HdIdSupplier.Value = purDataSet.vpur_podetail02[0].supplier_id.ToString();
          TxtSupplier.Text = purDataSet.vpur_podetail02[0].supplier_nama;
          BtnBrowseSupplier.Visible = false;
        }
      }
      else if (IsPostBack) {
        if (ViewState.Count > 0)
        {
          SupplierDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        } 
      }
    }

    private string GetIdPO()
    {
      string id = "";
      DateTime date = DateTime.Now;

      if (getUnitKerja == "ASSEMBLING")
      {
        id = CUtils.GenerateId("pur_po_assembling", date.Year, "D");
      }
      else if (getUnitKerja == "SPARE PARTS")
      {
        id = CUtils.GenerateId("pur_po_spareparts", date.Year, "C");
      }
      else if (getUnitKerja == "SHIPYARD")
      {
        id = CUtils.GenerateId("pur_po_shipyard", date.Year, "S");
      }

      return id;
    }

    protected void GridDetailMR_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        HdIdMRDetail.Value = gridDataItem["id"].Text;
        TxtNamaBarang.Text = gridDataItem["barang_nama"].Text;
        TxtJumlah.Text = gridDataItem["jumlah"].Text;
        RadTxtHarga.Value = Convert.ToDouble(gridDataItem["harga"].Text);
        TxtSatuan.Text = gridDataItem["satuan_nama"].Text;
        string currency = gridDataItem["currency"].Text;
        TxtCurrency.Text = currency;

        if (currency != "IDR")
        {
          PnlKurs.Visible = true;
        }

        PnlViewRequestBarangModalPopupExtender.Hide();
      }
    }

    protected void GridMR_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        TxtNoMR.Text = gridDataItem["id"].Text;

        PnlRequestNoteModalPopupExtender.Hide();
      }
    }

    protected void BtnCariSupplier_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtCariSupplier.Text, "%");

      ViewState.Remove("SelectMethod");

      SupplierDataSource.SelectMethod = "GetDataLikeNama";
      SupplierDataSource.SelectParameters.Clear();
      SupplierDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataLikeNama");

      GridSupplier.DataBind();

      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        HdIdSupplier.Value = gridDataItem["id"].Text;
        TxtSupplier.Text = gridDataItem["nama"].Text;

        PnlViewSupplierModalPopupExtender.Hide();
      }
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
      var url = string.Concat("List.aspx");
      Response.Redirect(url);
    }

    private void GeUpdateSeedPO(DateTime tanggal)
    {
      if (getUnitKerja == "ASSEMBLING")
      {
        CUtils.UpdateSeed("pur_po_assembling", tanggal.Year, tanggal);
      }
      else if (getUnitKerja == "SPARE PARTS")
      {
        CUtils.UpdateSeed("pur_po_spareparts", tanggal.Year, tanggal);
      }
      else if (getUnitKerja == "SHIPYARD")
      {
        CUtils.UpdateSeed("pur_po_shipyard", tanggal.Year, tanggal);
      }
    }

    private string GetNomerPO(DateTime tanggal)
    {
      string nomerpo = "";
      if (getUnitKerja == "ASSEMBLING")
      {
        nomerpo = CUtils.GenerateId("pur_nopo", tanggal.Year, tanggal.Month, "MAPO-");
      }
      else if (getUnitKerja == "SPARE PARTS") {
        nomerpo = CUtils.GenerateId("pur_nopo", tanggal.Year, tanggal.Month, "MSPO-");
      }
      else if (getUnitKerja == "SHIPYARD")
      {
        nomerpo = CUtils.GenerateId("pur_nopo", tanggal.Year, tanggal.Month, "SPO-");
      }
      return nomerpo;
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var poTableAdapter = new pur_poTableAdapter();
      var poRow = purDataSet.pur_po.Newpur_poRow();

      try
      {
        poRow.id = HdIdPO.Value;
        poRow.nomerpo = TxtTanggalPO.Text == string.Empty ? GetNomerPO(DateTime.Now) : GetNomerPO(Convert.ToDateTime(TxtTanggalPO.Text));
        poRow.tglpo = TxtTanggalPO.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(TxtTanggalPO.Text);
        poRow.type = "NP";
        poRow.ppn = 0;
        poRow.ppnval = 0;
        poRow.diskon = 0;
        poRow.typediskon = PgPODlistDiskon.SelectedValue;
        poRow.diskonval = 0;
        poRow.jasalain = "";
        poRow.biayajasalain = 0;
        poRow.totalpo = 0;
        poRow.jenispo = PgPODlistJenisBeli.SelectedValue;
        poRow.payment = "";
        poRow.lokasigudang_id = Convert.ToInt32(PgPODlistPenempatan.SelectedValue);
        poRow.tglpenyelesaian = DateTime.Now;
        poRow.catatan = "";
        poRow.keterangan = "";
        poRow.status = "PO2";
        poRow.createdby = getFullName;
        poRow.approvedby = "";
        poRow.unitkerja = getUnitKerja;
        poRow.datecreated = DateTime.Now;

        purDataSet.pur_po.Addpur_poRow(poRow);
        if (poTableAdapter.Update(purDataSet.pur_po) > 0)
        {
          if (TxtTanggalPO.Text==string.Empty)
          {
            GeUpdateSeedPO(DateTime.Now);
            CUtils.UpdateSeed("pur_nopo", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now);
          }
          else
          {
            GeUpdateSeedPO(Convert.ToDateTime(TxtTanggalPO.Text));
            CUtils.UpdateSeed("pur_nopo", Convert.ToDateTime(TxtTanggalPO.Text).Year, Convert.ToDateTime(TxtTanggalPO.Text).Month, Convert.ToDateTime(TxtTanggalPO.Text));
          }

          CUtils.UpdateLog("pur_po", HdIdPO.Value, getUsername, "Input PO, Cancel PO");
        }

        var url = string.Concat("List.aspx");
        Response.Redirect(url);
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void BtnSaveAll_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var poTableAdapter = new pur_poTableAdapter();
      var poRow = purDataSet.pur_po.Newpur_poRow();

      try
      {
        poRow.id = HdIdPO.Value;
        poRow.nomerpo = GetNomerPO(Convert.ToDateTime(TxtTanggalPO.Text));
        poRow.tglpo = TxtTanggalPO.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(TxtTanggalPO.Text);
        poRow.type = "NP";
        poRow.ppn = PgPOTxtPPN.Value == null ? 0 : Convert.ToSingle(PgPOTxtPPN.Value);
        poRow.ppnval = PgPOHdPPNVal.Value == string.Empty ? 0 : Convert.ToDecimal(PgPOHdPPNVal.Value);
        poRow.diskon = PgPOTxtDiskon.Value == null ? 0 : Convert.ToDecimal(PgPOTxtDiskon.Value);
        poRow.typediskon = PgPODlistDiskon.SelectedValue;        
        poRow.diskonval = PgPOHdDiskonVal.Value == string.Empty ? 0 : Convert.ToDecimal(PgPOHdDiskonVal.Value);
        poRow.jasalain = PgPOTxtLain.Text;
        poRow.biayajasalain = PgPOTxtBiayaLain.Value == null ? 0 : Convert.ToDecimal(PgPOTxtBiayaLain.Value);
        poRow.totalpo = PgPOTxtTotal.Value == null ? 0 : Convert.ToDecimal(PgPOTxtTotal.Value);
        poRow.jenispo = PgPODlistJenisBeli.SelectedValue;
        if (PgPODlistPembayaran.SelectedValue == "Cash" || PgPODlistPembayaran.SelectedValue == "Termin")
        {
          poRow.payment = string.Concat(PgPODlistPembayaran.SelectedValue + PgPOTxtPembayaran.Text);
        }
        else
        {
          poRow.payment = PgPOTxtPembayaran.Text;
        }
        poRow.lokasigudang_id = Convert.ToInt32(PgPODlistPenempatan.SelectedValue);
        poRow.tglpenyelesaian = PgPOTglPenyelesaian.SelectedDate.Value;
        poRow.catatan = PgPOTxtCatatan.Text;
        poRow.keterangan = PgPOTxtKeterangan.Text;
        poRow.status = "PO1";
        poRow.createdby = getFullName;
        poRow.approvedby = "";
        poRow.unitkerja = getUnitKerja;
        poRow.datecreated = DateTime.Now;

        purDataSet.pur_po.Addpur_poRow(poRow);
        if (poTableAdapter.Update(purDataSet.pur_po) > 0)
        {
          GeUpdateSeedPO(Convert.ToDateTime(TxtTanggalPO.Text));
          CUtils.UpdateSeed("pur_nopo", Convert.ToDateTime(TxtTanggalPO.Text).Year, Convert.ToDateTime(TxtTanggalPO.Text).Month, Convert.ToDateTime(TxtTanggalPO.Text));
          CUtils.UpdateLog("pur_po", HdIdPO.Value, getUsername, "Input PO");
        }

        var url = string.Concat("List.aspx");
        Response.Redirect(url);
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }

    }

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

    protected void BtnBrowseSupplier_Click(object sender, EventArgs e)
    {
      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void BtnBrowseMR_Click(object sender, EventArgs e)
    {
      PnlRequestNoteModalPopupExtender.Show();
    }

    protected void BtnBrowseMRDetail_Click(object sender, EventArgs e)
    {
      PnlViewRequestBarangModalPopupExtender.Show();
    }

    protected void BtnHitung_Click(object sender, EventArgs e)
    {
      int jumlah = TxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(TxtJumlah.Text);
      decimal harga = RadTxtHarga.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtHarga.Text);
      decimal diskon = RadTxtDiskon.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtDiskon.Text);
      decimal totalA, totalB, total;
      totalA = jumlah * harga;
      totalB = (totalA * diskon) / 100;
      total = totalA - totalB;
      RadTxtSubTotal.Text = Convert.ToDouble(total).ToString();
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
      if (HdIdMRDetail.Value == "" || RadTxtSubTotal.Text == "")
      {
        validationError();
      }
      else
      {
        var purDataSet = new PurDataSet();
        var podetailTableAdapter = new pur_podetailTableAdapter();
        var podetailRow = purDataSet.pur_podetail.Newpur_podetailRow();
        var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
        var mrTableAdapter = new pur_mrTableAdapter();

        try
        {
          podetailRow.po_id = HdIdPO.Value;
          podetailRow.mrdetail_id = Convert.ToInt32(HdIdMRDetail.Value);
          podetailRow.diskon = RadTxtDiskon.Text == string.Empty ? 0 : Convert.ToSingle(RadTxtDiskon.Text);
          podetailRow.kurs = RadTxtKurs.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtKurs.Text);
          podetailRow.total = RadTxtSubTotal.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtSubTotal.Text);
          podetailRow.status = "D1";

          purDataSet.pur_podetail.Addpur_podetailRow(podetailRow);
          if (podetailTableAdapter.Update(purDataSet.pur_podetail) > 0)
          {
            CUtils.UpdateLog("pur_podetail", (Convert.ToInt32(podetailTableAdapter.ScalarGetMaxId()) + 1).ToString(), getUsername, "Input PO Detail");
          }

          UpdateStatusMRDetail(HdIdMRDetail.Value);
          HandleSuccess();
          NotificationSuccess();
        }
        catch (Exception ex)
        {
          NotificationFailure(ex);
        }
      }
    }

    private void UpdateStatusMRDetail(string idMrDetail)
    {
      var purDataSet = new PurDataSet();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var mrTableAdapter = new pur_mrTableAdapter();

      // Update Status MR Detail..
      mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(idMrDetail));
      string mrid = purDataSet.pur_mrdetail[0].mr_id;
      purDataSet.pur_mrdetail[0].status = "B8";
      if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
      {
        CUtils.UpdateLog("pur_mrdetail", idMrDetail, getUsername, "Update MR Detail, Status 'Add PO'");
      }

      // Cek di MR
      PurchaseUtils.UpdateStatusRequestNote('p', mrid, getUsername);
    }

    private void HandleSuccess()
    {
      TxtSupplier.Enabled = false;
      BtnBrowseSupplier.Enabled = false;
      TxtNoMR.Text = string.Empty;
      TxtNamaBarang.Text = string.Empty;
      HdIdMRDetail.Value = null;
      TxtJumlah.Text = string.Empty;
      TxtSatuan.Text = string.Empty;
      RadTxtHarga.Text = string.Empty;
      TxtCurrency.Text = string.Empty;
      RadTxtDiskon.Text = string.Empty;
      RadTxtSubTotal.Text = string.Empty;
      RadTxtKurs.Text = string.Empty;

      GridMR.DataBind();
      GridPODetail.DataBind();
      GridDetailMR.DataBind();
    }

    protected void validationError()
    {
      PnlMessageLblTitlebar.Text = "Information";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
      PnlMessageLblMessage.Text = "Form input tidak boleh kosong.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
      foreach (GridDataItem item in GridPODetail.MasterTableView.Items)
      {
        CheckBox chkbx = (CheckBox)item["CheckTemp"].FindControl("CheckBox1");
        chkbx.Checked = !chkbx.Checked;
      }
    }

    protected void CheckBoxError()
    {
      PnlMessageLblTitlebar.Text = "Information";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
      PnlMessageLblMessage.Text = "Pilih dulu data yang akan dihapus";
      PnlMessageModalPopupExtender.Show();
    }

    protected void BtnCalculate_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var podetailTableAdapter = new pur_podetailTableAdapter();

      string poid = HdIdPO.Value;
      decimal totalHarga = Convert.ToDecimal(podetailTableAdapter.ScalarGetSumTotalByPoId(poid));
      decimal biayalain = PgPOTxtBiayaLain.Text == string.Empty ? 0 : Convert.ToDecimal(PgPOTxtBiayaLain.Text);
      decimal ppn = PgPOTxtPPN.Text == string.Empty ? 0 : Convert.ToDecimal(PgPOTxtPPN.Text);
      decimal diskon = PgPOTxtDiskon.Text == string.Empty ? 0 : Convert.ToDecimal(PgPOTxtDiskon.Text);

      // total Awal after diskon
      decimal totalA = 0;
      if (PgPODlistDiskon.SelectedValue == "-")
      {
        totalA = totalHarga;
        PgPOHdDiskonVal.Value = "0";
      }
      else if (PgPODlistDiskon.SelectedValue == "%")
      {
        decimal bdiskon = (diskon * totalHarga) / 100;
        totalA = totalHarga - bdiskon;
        PgPOHdDiskonVal.Value = Convert.ToDouble(bdiskon).ToString();
      }
      else if (PgPODlistDiskon.SelectedValue == "Nominal")
      {
        totalA = totalHarga - diskon;
        PgPOHdDiskonVal.Value = Convert.ToDouble(diskon).ToString();
      }
      
      // besar ppn
      decimal xppn = (ppn * totalA) / 100;
      PgPOHdPPNVal.Value = Convert.ToDouble(xppn).ToString();

      decimal totalB = totalA + xppn + biayalain;
      PgPOTxtTotal.Text = Convert.ToDouble(totalB).ToString();
    }

    protected void PgPODlistDiskon_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (PgPODlistDiskon.SelectedValue == "-")
      {
        PgPOTxtDiskon.Visible = false;
      }
      else if (PgPODlistDiskon.SelectedValue == "%" || PgPODlistDiskon.SelectedValue == "Nominal")
      {
        PgPOTxtDiskon.Visible = true;
      }
    }

    //protected void BtnReset_Click(object sender, EventArgs e)
    //{
    //  string jus = LblResetValue.Text;
    //  PgPOPPN.Text = string.Empty;
    //  DlistDiskon.SelectedValue = "-";
    //  PgPOBiayaLain.Text = string.Empty;
    //  PgPODiskon1.Visible = false;
    //  PgPODiskon1.Text = string.Empty;
    //  PgPOJenisBiayaLain.Text = string.Empty;
    //  PgPOBiayaLain.Text = string.Empty;
    //  PgPOTotalNota.Text = jus;


    //}

    protected void kursKosong()
    {
      PnlMessageLblTitlebar.Text = "Information";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
      PnlMessageLblMessage.Text = "Input Kurs kosong, mohon masukkan dulu kursnya";
      PnlMessageModalPopupExtender.Show();
    }

    //protected void GridItemPO_ItemDataBound(object sender, GridItemEventArgs e)
    //{
    //  if (e.Item is GridDataItem)
    //  {
    //    var gridDataItem = (GridDataItem)e.Item;
    //    string diskon = gridDataItem["diskon"].Text;
    //    string harga = gridDataItem["subtotal"].Text;

    //    if (diskon == "0")
    //    {
    //      gridDataItem["subtotal"].Text = harga;
    //      double fieldvalue = double.Parse(gridDataItem["subtotal"].Text);
    //      totalC += fieldvalue;
    //    }
    //    else
    //    {
    //      gridDataItem["subtotal"].Text = harga;
    //      double fieldvalue = double.Parse(gridDataItem["subtotal"].Text);
    //      totalC += fieldvalue;
    //    }

    //  }

    //  if (e.Item is GridFooterItem)
    //  {
    //    GridFooterItem footerItem = e.Item as GridFooterItem;
    //    footerItem["subtotal"].Text = "Total: " + getNominal(totalC.ToString());
    //    double totalD = Math.Round(totalC, 2);
    //    PgPOTotalNota.Text = getNominal(totalD.ToString());
    //    LblResetValue.Text = getNominal(totalD.ToString());
    //  }
    //}

    protected void Delete_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var podetailTableAdapter = new pur_podetailTableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();

      foreach (GridDataItem item in GridPODetail.MasterTableView.Items)
      {
        if (item is GridDataItem)
        {
          GridDataItem gridataitem = item as GridDataItem;
          CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;

          if (CheckBox1.Checked && CheckBox1 != null)
          {
            // Access data key
            string id = item["id"].Text;
            string idmrdetail = item["mrdetail_id"].Text;
            string idmr = item["mr_id"].Text;

            if (mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(idmrdetail)) > 0)
            {
              purDataSet.pur_mrdetail[0].status = "B5";
              if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
              {
                CUtils.UpdateLog("pur_mrdetail", idmrdetail, getUsername, "Update MR Detail, Status Disetujui (Delete PO Detail)");
              }

              PurchaseUtils.UpdateStatusRequestNote('a', idmr, getUsername);
            }

            if (podetailTableAdapter.DeleteQueryById(Convert.ToInt32(id)) > 0)
            {
              CUtils.UpdateLog("pur_podetail", id, getUsername, "Delete PO Detail");
            }


            // Pnl Confirmation
            PnlMessageLblTitlebar.Text = "Confirm";
            PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
            PnlMessageLblMessage.Text = "Data berhasil dihapus.";
            PnlMessageModalPopupExtender.Show();
          }
          else
          {
            CheckBoxError();
          }
        }
      }
      GridPODetail.DataBind();
      GridMR.DataBind();
      GridDetailMR.DataBind();
      Response.Redirect(Request.RawUrl);
    }

    protected void PgPODlistPembayaran_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (PgPODlistPembayaran.SelectedValue == "Cash")
      {
        PgPOTxtPembayaran.Visible = false;
        LblHari.Visible = false;
      }
      else if (PgPODlistPembayaran.SelectedValue == "Termin")
      {
        PgPOTxtPembayaran.Visible = true;
        LblHari.Visible = true;
      }
      else if (PgPODlistPembayaran.SelectedValue == "Custom")
      {
        PgPOTxtPembayaran.Visible = true;
        LblHari.Visible = false;
      }
    }

    protected void GridMR_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string status = item["mr_status"].Text;
        item["mr_status"].Text = PurchaseUtils.GetStatusBarang(status);
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

    protected void GridPODetail_ItemDataBound(object sender, GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var podetailTableAdapter = new pur_podetailTableAdapter();

      string idpo = HdIdPO.Value;

      if (e.Item is GridFooterItem)
      {
        double total = Convert.ToDouble(podetailTableAdapter.ScalarGetSumTotalByPoId(idpo));
        GridFooterItem footerItem = e.Item as GridFooterItem;
        footerItem["total"].Text = String.Concat("TOTAL : " + String.Format("{0,20:N2}", total));
        PgPOTxtTotal.Text = Convert.ToString(total);
      }
    }

    protected void GridSupplier_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlViewSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlViewSupplierModalPopupExtender.Show();
    }



    //public string getNominal(string hasil)
    //{
    //  System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("id-ID");
    //  int valueBefore = Int32.Parse(hasil, System.Globalization.NumberStyles.AllowThousands);
    //  string nominal = String.Format(culture, "{0:N0}", valueBefore);
    //  return nominal;
    //}
  }
}
