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
using System.Globalization;
using System.Drawing;
using System.Web.Security;
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchasing.Material_Request.NonProject.Purchase_Order
{
  public partial class List : System.Web.UI.Page
  {
    private readonly ReportParameter[] _myReportParameters = new ReportParameter[24];
    protected IEnumerable<ReportParameter> MyParamEnum;

    string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
    string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
    string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
    string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (getUnitKerja == "T.H.O")
        {
          BtnInput.Visible = false;
          choosePanel(roles);
        }
        else
        {
          choosePanel(roles);
        }
      }

      if (getUnitKerja == "SHIPYARD") { LblJudul.Text = "Daftar Purchase Order (NonProject)"; }
      else { LblJudul.Text = "Daftar Purchase Order"; }
    }

    private void choosePanel(string roles)
    {
      if (roles == "Manager Purchasing")
      {
        PnlData1.Visible = false;
        PnlData2.Visible = true;
      }
      else if (roles == "GM Purchasing")
      {
        PnlData1.Visible = false;
        PnlData2.Visible = true;
      }
      else if (roles == "Supervisor Purchasing")
      {
        PnlData1.Visible = false;
        PnlData2.Visible = true;
      }
      else if (roles == "Direktur Purchasing")
      {
        PnlData1.Visible = false;
        PnlData2.Visible = true;
      }
      else if (roles == "Staff Purchasing")
      {
        PnlData1.Visible = false;
        PnlData2.Visible = true;
      }
      else if (roles == "Purchasing Manufacturing")
      {
        PnlData1.Visible = false;
        PnlData2.Visible = true;
      }
    }


    private void loadGridDefault()
    {
      ViewState.Remove("SelectMethod");

      PODataSource.SelectMethod = "GetDataByUnitKerjaType";
      PODataSource.SelectParameters.Clear();
      PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);
      PODataSource.SelectParameters.Add("type", "NP");

      ViewState.Add("SelectMethod", "GetDataByUnitKerjaType");
      GridPO.DataBind();
    }

    private void loadGridManufacture()
    {
      ViewState.Remove("SelectMethod");

      PODataSource.SelectMethod = "GetDataUnitManufactureByType";
      PODataSource.SelectParameters.Clear();
      PODataSource.SelectParameters.Add("type", "NP");

      ViewState.Add("SelectMethod", "GetDataUnitManufactureByType");
      GridPO.DataBind();
    }

    private void loadGridAll()
    {
      ViewState.Remove("SelectMethod");

      PODataSource.SelectMethod = "GetDataByType";
      PODataSource.SelectParameters.Clear();
      PODataSource.SelectParameters.Add("type", "NP");

      ViewState.Add("SelectMethod", "GetDataByType");
      GridPO.DataBind();
    }

    protected void GridPO_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["id"].Text;
        string status = gridDataItem["status"].Text;

        if (status == "PO Pending" || status == "PO2")
        {
          NoptificationPOPending();
        }
        else
        {
          var url = string.Concat("Detail.aspx?pId=", id);
          Response.Redirect(url);
        }
      }
      else if (e.CommandName == "Print")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var nomerpo = gridDataItem["nomerpo"].Text;
        LblNoPO.Text = nomerpo;
        HdIdPO.Value = gridDataItem["id"].Text;

        PnlKonfirmModalPopupExtender.Show();
      }
      else if (e.CommandName == "Check")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["nomerpo"].Text;
        PnlShowLblNoPO.Text = id;

        PnlShowConfirmPopupExtender.Show();
      }
      else if (e.CommandName == "Update")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["id"].Text;
        var url = string.Concat("Edit.aspx?pId=", id);

        Response.Redirect(url);
      }
      else if (e.CommandName == "Close")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["nomorpo"].Text;
        PnlCloseLblNoPO.Text = id;

        PnlCloseModalPopupExtender.Show();
      }
      else if (e.CommandName == "Batal")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["nomorpo"].Text;
        PnlBatalTxtNoPO.Text = id;

        PnlBatalModalPopupExtender.Show();
      }

    }

    protected void PnlKonfirmPrintOk_Click(object sender, EventArgs e)
    {
      cetakLaporan(HdIdPO.Value);
    }

    protected void GridPO_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var podetailTableAdapter = new pur_podetailTableAdapter();

      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;
        Button btnPrint = item.FindControl("BtnPrint") as Button;
        Button btnUpdate = item.FindControl("BtnUpdate") as Button;
        Button btnCheck = item.FindControl("BtnCheck") as Button;
        Button btnClose = item.FindControl("BtnClose") as Button;
        Button btnBatal = item.FindControl("BtnBatal") as Button;

        var status = item["status"].Text;
        var idPO = item["id"].Text;
        var currency = item["currency"].Text;
        var stotalPO = item["totalpo"].Text;
        //var kurs = item["kurs"].Text;
        decimal kurs = Convert.ToDecimal(podetailTableAdapter.ScalarCountGetKursByPoId(idPO));

        string[] rolesArray = Roles.GetRolesForUser();
        decimal dtotalPO = stotalPO == null ? 0 : Convert.ToDecimal(stotalPO);

        if (stotalPO == "" || stotalPO == null)
        {
          item["totalpo"].Text = "0";
        }

        // Perhitungan Total..
        decimal total;
        if (currency == "IDR") { total = dtotalPO; }
        else { total = kurs * dtotalPO; }

        foreach (string roles in rolesArray)
        {
          if (total <= 10000000)
          {
            if (roles == "Supervisor Purchasing" || roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnCheck.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
            }
            else if (roles == "Staff Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
              else if (status == "PO4" || status == "PO Lengkap")
              {
                btnClose.Visible = true;
              }
            }
          }
          else if (100000000 <= total || total < 50000000)
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnCheck.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
            }
            else if (roles == "Staff Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
              else if (status == "PO4" || status == "PO Lengkap")
              {
                btnClose.Visible = true;
              }
            }
          }
          else if (50000000 <= total || total < 100000000)
          {
            if (roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnCheck.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
            }
            else if (roles == "Staff Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
              else if (status == "PO4" || status == "PO Lengkap")
              {
                btnClose.Visible = true;
              }
            }
          }
          else if (total > 100000000)
          {
            if (roles == "Direktur Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnCheck.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
            }
            else if (roles == "Staff Purchasing")
            {
              if (status == "PO1" || status == "PO Baru")
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "PO2" || status == "PO Pending")
              {
                btnUpdate.Visible = true;
              }
              else if (status == "PO3" || status == "PO Disetujui")
              {
                btnPrint.Visible = true;
              }
              else if (status == "PO4" || status == "PO Lengkap")
              {
                btnClose.Visible = true;
              }
            }
          }
        }

        item["status"].Text = PurchaseUtils.GetStatusBarang(status);
      }
    }

    protected void BtnSearchClick(object sender, EventArgs e)
    {
      var katakunci = string.Concat("%", TxtKataKunci.Text, "%");
      string pilihan = DlistKategoriPencarian.SelectedValue;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          SearchAll(katakunci, DlistStatus.SelectedValue);
        }
        else if (roles == "Purchasing S.M")
        {
          SearchDefault(katakunci, DlistStatus.SelectedValue);
        }
        else if (roles == "Purchasing Manufacturing") {
          SearchManufacture(katakunci, DlistStatus.SelectedValue);
        }
      }

      GridPO.CurrentPageIndex = 0;
      GridPO.DataBind();
    }

    private void SearchAll(string katakunci, string status)
    {
      string kategori = DlistKategoriPencarian.SelectedValue;

      if (kategori == "0") // No PO
      {
        PODataSource.SelectMethod = "GetDataByTypeNomerPo";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("type", "NP");
        PODataSource.SelectParameters.Add("nomerpo", katakunci);

        ViewState.Add("SelectMethod", "GetDataByTypeNomerPo");
      }
      else if (kategori == "1") // Status
      {
        PODataSource.SelectMethod = "GetDataByTypeStatus";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("status", status);
        PODataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (kategori == "2") // Tanggal
      {
        DateTime dateStart = Convert.ToDateTime(TxtTanggal1.Text);
        DateTime dateEnd = Convert.ToDateTime(TxtTanggal2.Text);
        string periodeStart = dateStart.ToString("yyyy-MM-dd");
        string periodeEnd = dateEnd.ToString("yyyy-MM-dd");

        PODataSource.SelectMethod = "GetDataByTypeTglPo";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("tglpo1", periodeStart);
        PODataSource.SelectParameters.Add("tglpo2", periodeEnd);
        PODataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeTglPo");
      }
      else if (kategori == "3") // Supplier
      {
        PODataSource.SelectMethod = "GetDataByTypeSupplierNama";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("supplierNama", katakunci);
        PODataSource.SelectParameters.Add("type", "NP");

        ViewState.Add("SelectMethod", "GetDataByTypeSupplierNama");
      }
    }

    private void SearchDefault(string katakunci, string status)
    {
      string kategori = DlistKategoriPencarian.SelectedValue;

      if (kategori == "0") // No PO
      {
        PODataSource.SelectMethod = "GetDataByTypeUnitKerjaNomerPo";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("type", "NP");
        PODataSource.SelectParameters.Add("nomerpo", katakunci);
        PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaNomerPo");
      }
      else if (kategori == "1") // Status
      {
        PODataSource.SelectMethod = "GetDataByTypeUnitKerjaStatus";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("status", status);
        PODataSource.SelectParameters.Add("type", "NP");
        PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaStatus");
      }
      else if (kategori == "2") // Tanggal
      {
        DateTime dateStart = Convert.ToDateTime(TxtTanggal1.Text);
        DateTime dateEnd = Convert.ToDateTime(TxtTanggal2.Text);
        string periodeStart = dateStart.ToString("yyyy-MM-dd");
        string periodeEnd = dateEnd.ToString("yyyy-MM-dd");

        PODataSource.SelectMethod = "GetDataByTypeUnitKerjaTglPo";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("tglpo1", periodeStart);
        PODataSource.SelectParameters.Add("tglpo2", periodeEnd);
        PODataSource.SelectParameters.Add("type", "NP");
        PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaTglPo");
      }
      else if (kategori == "3") // Supplier
      {
        PODataSource.SelectMethod = "GetDataByTypeUnitKerjaSupplierNama";
        PODataSource.SelectParameters.Clear();
        PODataSource.SelectParameters.Add("supplierNama", katakunci);
        PODataSource.SelectParameters.Add("type", "NP");
        PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaSupplierNama");
      }
    }

    private void SearchManufacture(string katakunci, string status)
    {
      //string kategori = DlistKategoriPencarian.SelectedValue;

      //if (kategori == "0") // No PO
      //{
      //  PODataSource.SelectMethod = "GetDataByTypeUnitKerjaNomerPo";
      //  PODataSource.SelectParameters.Clear();
      //  PODataSource.SelectParameters.Add("type", "NP");
      //  PODataSource.SelectParameters.Add("nomerpo", katakunci);
      //  PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

      //  ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaNomerPo");
      //}
      //else if (kategori == "1") // Status
      //{
      //  PODataSource.SelectMethod = "GetDataByTypeUnitKerjaStatus";
      //  PODataSource.SelectParameters.Clear();
      //  PODataSource.SelectParameters.Add("status", status);
      //  PODataSource.SelectParameters.Add("type", "NP");
      //  PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

      //  ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaStatus");
      //}
      //else if (kategori == "2") // Tanggal
      //{
      //  DateTime dateStart = Convert.ToDateTime(TxtTanggal1.Text);
      //  DateTime dateEnd = Convert.ToDateTime(TxtTanggal2.Text);
      //  string periodeStart = dateStart.ToString("yyyy-MM-dd");
      //  string periodeEnd = dateEnd.ToString("yyyy-MM-dd");

      //  PODataSource.SelectMethod = "GetDataByTypeUnitKerjaTglPo";
      //  PODataSource.SelectParameters.Clear();
      //  PODataSource.SelectParameters.Add("tglpo1", periodeStart);
      //  PODataSource.SelectParameters.Add("tglpo2", periodeEnd);
      //  PODataSource.SelectParameters.Add("type", "NP");
      //  PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

      //  ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaTglPo");
      //}
      //else if (kategori == "3") // Supplier
      //{
      //  PODataSource.SelectMethod = "GetDataByTypeUnitKerjaSupplierNama";
      //  PODataSource.SelectParameters.Clear();
      //  PODataSource.SelectParameters.Add("supplierNama", katakunci);
      //  PODataSource.SelectParameters.Add("type", "NP");
      //  PODataSource.SelectParameters.Add("unitkerja", getUnitKerja);

      //  ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaSupplierNama");
      //}
    }

    protected void BtnCancelClick(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      TxtTanggal1.Text = string.Empty;
      TxtTanggal2.Text = string.Empty;
      DlistStatus.SelectedIndex = 0;
      DlistKategoriPencarian.SelectedIndex = 0;

      PnlKataKunci.Visible = true;
      PnlTanggal.Visible = false;
      PnlStatus.Visible = false;

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          loadGridAll();
        }
        else if (roles == "Purchasing S.M")
        {
          loadGridDefault();
        }
        else if (roles == "Purchasing Manufacturing") {
          loadGridManufacture();
        }
      }


      GridPO.DataBind();
    }

    protected void BtnInput_Click(object sender, EventArgs e)
    {
      var url = string.Concat("Input.aspx");
      Response.Redirect(url);
    }

    protected void NoptificationPOPending()
    {
      PnlMessageLblTitlebar.Text = "Information";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
      PnlMessageLblMessage.Text = "Data Kosong, harap isi data!";
      PnlMessageModalPopupExtender.Show();
    }

    protected void NotificationSuccess()
    {
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Sukses";
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

      _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
    }

    //protected void notifUpdateGagal() 
    //{
    //    // Pnl Confirmation
    //    PnlMessageLblTitlebar.Text = "Confirm";
    //    PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
    //    PnlMessageLblMessage.Text = "Update Status Barang Gagal";
    //    PnlMessageModalPopupExtender.Show();
    //}

    protected void cetakLaporan(String idPO)
    {
      var reportDataSet = new ReportsDataSet();
      var purDataSet = new PurDataSet();
      var vpodetail02TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_podetail02TableAdapter();
      var vpo01TableAdapter = new TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter();
      var supplierTableAdapter = new master_supplierTableAdapter();
      var podetailTableAdapter = new pur_podetailTableAdapter();

      string nopo, tglpo, namaSup, telpSupKantor, telpSupPribadi, cp, alamatSup, unitKerja, currency;
      string xDiskon, yDiskon, xBiayaLain, yBiayaLain, xPpn, yPpn, totalakhir, notes,
          tglpenerimaan, tempatpenerimaan, payment, pOperator, terbilang1, terbilang2, keterangan;

      // ====================== Parameter ==================== //
      double tpd = Convert.ToDouble(podetailTableAdapter.ScalarGetSumTotalByPoId(idPO)); // total PO Detail ..

      vpo01TableAdapter.FillById(purDataSet.vpur_po01, idPO);
      nopo = purDataSet.vpur_po01[0].nomerpo;
      tglpo = purDataSet.vpur_po01[0].tglpo.ToShortDateString();
      unitKerja = purDataSet.vpur_po01[0].unitkerja;
      totalakhir = Convert.ToDouble(purDataSet.vpur_po01[0].totalpo).ToString();
      notes = purDataSet.vpur_po01[0].catatan;
      tglpenerimaan = purDataSet.vpur_po01[0].tglpenyelesaian.ToShortDateString();
      tempatpenerimaan = purDataSet.vpur_po01[0].lokasigudang_nama;
      payment = purDataSet.vpur_po01[0].payment;
      pOperator = getFullName + " (" + DateTime.Now + " )";
      keterangan = purDataSet.vpur_po01[0].keterangan;

      // Currency & Terbilang ...
      string c = purDataSet.vpur_po01[0].currency;
      if (c == "IDR") { currency = "Rp"; terbilang2 = "rupiah"; }
      else if (c == "USD" || c == "AUD" || c == "SGD") { currency = "$"; terbilang2 = "dollar"; }
      else { currency = "€"; terbilang2 = "euro"; }
      terbilang1 = this.Terbilang(Convert.ToDecimal(tpd)).TrimStart();

      // Diskon..
      string typediskon = purDataSet.vpur_po01[0].typediskon;
      double totalF = 0;
      if (typediskon == "-")
      {
        xDiskon = "";
        yDiskon = "0";
        totalF = tpd;
      }
      else if (typediskon == "%")
      {
        xDiskon = Convert.ToDouble(purDataSet.vpur_po01[0].diskon).ToString() + "%";
        double i = Convert.ToDouble(purDataSet.vpur_po01[0].diskon);
        double d = Convert.ToDouble((i * tpd) / 100);
        yDiskon = string.Format("{0,20:N2}", d);
        totalF = tpd - d;
      }
      else
      {
        xDiskon = "";
        yDiskon = String.Format("{0,20:N2}", purDataSet.vpur_po01[0].diskon);
        totalF = tpd - Convert.ToDouble(yDiskon);
      }

      // Biaya Lain
      string b = purDataSet.vpur_po01[0].jasalain;
      if (b == null || b == "")
      {
        xBiayaLain = ""; yBiayaLain = "0";
      }
      else
      {
        xBiayaLain = b; yBiayaLain = String.Format("{0,20:N2}", purDataSet.vpur_po01[0].biayajasalain);
      }

      // Ppn..
      double ppn = purDataSet.vpur_po01[0].ppn;
      if (ppn == 0)
      {
        xPpn = ""; yPpn = "0";
      }
      else
      {
        xPpn = ppn + " %";
        double bppn = (ppn * totalF) / 100;
        yPpn = String.Format("{0,20:N2}", bppn);
      }

      // Total Akhir
      string pTotalAkhir = String.Format("{0,20:N2}", totalakhir);

      string idSup = purDataSet.vpur_po01[0].supplier_id.ToString();
      supplierTableAdapter.FillById(purDataSet.master_supplier, Convert.ToInt32(idSup));
      namaSup = purDataSet.master_supplier[0].nama;
      telpSupKantor = purDataSet.master_supplier[0].IsnotelpkantorNull() ? "" : purDataSet.master_supplier[0].notelpkantor;
      telpSupPribadi = purDataSet.master_supplier[0].IsnotelppribadiNull() ? "" : purDataSet.master_supplier[0].notelppribadi;
      alamatSup = purDataSet.master_supplier[0].IsalamatNull() ? "" : purDataSet.master_supplier[0].alamat;
      cp = purDataSet.master_supplier[0].IskontakpersonNull() ? "" : purDataSet.master_supplier[0].kontakperson;

      // =================================================== //

      var reportViewer = new ReportViewer();
      reportViewer.ProcessingMode = ProcessingMode.Local;
      if (getUnitKerja == "SHIPYARD")
      {
        if (keterangan == "" || keterangan == null || keterangan == string.Empty)
        {
          reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptPOC.rdlc");
        }
        else
        {
          reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptPOD.rdlc");
        }
      }
      else {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptPOJ.rdlc");
      }
      
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      _myReportParameters[0] = new ReportParameter("pNoPO", nopo);
      _myReportParameters[1] = new ReportParameter("pTanggalPO", tglpo);
      _myReportParameters[2] = new ReportParameter("pNamaSupplier", namaSup);
      _myReportParameters[3] = new ReportParameter("pTeleponKantor", telpSupKantor);
      _myReportParameters[4] = new ReportParameter("pTeleponPribadi", telpSupPribadi);
      _myReportParameters[5] = new ReportParameter("pKontakPerson", cp);
      _myReportParameters[6] = new ReportParameter("pAlamatSupplier", alamatSup);
      _myReportParameters[7] = new ReportParameter("pUnitKerja", unitKerja);
      _myReportParameters[8] = new ReportParameter("pCurrency", currency);
      _myReportParameters[9] = new ReportParameter("pXDiskon", xDiskon);
      _myReportParameters[10] = new ReportParameter("pYDiskon", yDiskon);
      _myReportParameters[11] = new ReportParameter("pXBiayaLain", xBiayaLain);
      _myReportParameters[12] = new ReportParameter("pYBiayaLain", yBiayaLain);
      _myReportParameters[13] = new ReportParameter("pXPpn", xPpn);
      _myReportParameters[14] = new ReportParameter("pYPpn", yPpn);
      _myReportParameters[15] = new ReportParameter("pTotalAkhir", pTotalAkhir);
      _myReportParameters[16] = new ReportParameter("pNotes", notes);
      _myReportParameters[17] = new ReportParameter("pTglPenerimaan", tglpenerimaan);
      _myReportParameters[18] = new ReportParameter("pTempatPenerimaan", tempatpenerimaan);
      _myReportParameters[19] = new ReportParameter("pPayment", payment);
      _myReportParameters[20] = new ReportParameter("pOperator", pOperator);
      _myReportParameters[21] = new ReportParameter("pTerbilang1", terbilang1);
      _myReportParameters[22] = new ReportParameter("pTerbilang2", terbilang2);
      _myReportParameters[23] = new ReportParameter("pKeterangan", keterangan);


      vpodetail02TableAdapter.FillByPoId(reportDataSet.vpur_podetail02, idPO);
      MyParamEnum = _myReportParameters;
      var reportDataSource = new ReportDataSource();
      reportDataSource.Name = "vpur_podetail02";
      reportDataSource.Value = reportDataSet.vpur_podetail02;
      reportViewer.LocalReport.SetParameters(MyParamEnum);
      reportViewer.LocalReport.DataSources.Add(reportDataSource);

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
      Response.AddHeader("content-disposition", string.Concat("inline; filename=PO", getUnitKerja + "_", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();
    }

    string Terbilang(Decimal d)
    {
      string strHasil = "";
      Decimal frac = d - Decimal.Truncate(d);

      if (Decimal.Compare(frac, 0.0m) != 0)
        strHasil = Terbilang(Decimal.Round(frac * 100)) + " sen";
      else
        strHasil = "";
      int xDigit = 0;
      int xPosisi = 0;

      string strTemp = Decimal.Truncate(d).ToString();
      for (int i = strTemp.Length; i > 0; i--)
      {
        string tmpx = "";
        xDigit = Convert.ToInt32(strTemp.Substring(i - 1, 1));
        xPosisi = (strTemp.Length - i) + 1;
        switch (xPosisi % 3)
        {
          case 1:
            bool allNull = false;
            if (i == 1)
              tmpx = satuan[xDigit] + " ";
            else if (strTemp.Substring(i - 2, 1) == "1")
              tmpx = belasan[xDigit] + " ";
            else if (xDigit > 0)
              tmpx = satuan[xDigit] + " ";
            else
            {
              allNull = true;
              if (i > 1)
                if (strTemp.Substring(i - 2, 1) != "0")
                  allNull = false;
              if (i > 2)
                if (strTemp.Substring(i - 3, 1) != "0")
                  allNull = false;
              tmpx = "";
            }

            if ((!allNull) && (xPosisi > 1))
              if ((strTemp.Length == 4) && (strTemp.Substring(0, 1) == "1"))
                tmpx = "se" + ribuan[(int)Decimal.Round(xPosisi / 3m)] + " ";
              else
                tmpx = tmpx + ribuan[(int)Decimal.Round(xPosisi / 3)] + " ";
            strHasil = tmpx + strHasil;
            break;
          case 2:
            if (xDigit > 0)
              strHasil = puluhan[xDigit] + " " + strHasil;
            break;
          case 0:
            if (xDigit > 0)
              if (xDigit == 1)
                strHasil = "seratus " + strHasil;
              else
                strHasil = satuan[xDigit] + " ratus " + strHasil;
            break;
        }
      }
      strHasil = strHasil.Trim().ToLower();
      if (strHasil.Length > 0)
      {
        strHasil = strHasil.Substring(0, 1).ToUpper() +
          strHasil.Substring(1, strHasil.Length - 1);
      }
      return strHasil;
    }

    protected void DlistKategoriPencarian_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DlistKategoriPencarian.SelectedValue == "0") // nopo
      {
        PnlKataKunci.Visible = true;
        PnlTanggal.Visible = false;
        PnlStatus.Visible = false;
      }
      else if (DlistKategoriPencarian.SelectedValue == "1")  //status
      {
        PnlKataKunci.Visible = false;
        PnlTanggal.Visible = false;
        PnlStatus.Visible = true;
      }
      else if (DlistKategoriPencarian.SelectedValue == "2") //tgl
      {
        PnlKataKunci.Visible = false;
        PnlTanggal.Visible = true;
        PnlStatus.Visible = false;
      }
      else if (DlistKategoriPencarian.SelectedValue == "3") //supp
      {
        PnlKataKunci.Visible = true;
        PnlTanggal.Visible = false;
        PnlStatus.Visible = false;
      }

    }

    protected void PnlShowBtnSetuju_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var poTableAdapter = new pur_poTableAdapter();

      string id = PnlShowLblNoPO.Text;

      if (poTableAdapter.FillByNomerPo(purDataSet.pur_po, id) > 0)
      {
        purDataSet.pur_po[0].status = "PO3";
        purDataSet.pur_po[0].approvedby = getFullName;

        if (poTableAdapter.Update(purDataSet.pur_po) > 0)
        {
          CUtils.UpdateLog("pur_po", id, Session["Username"].ToString(), "Update PO, status Disetujui.");
        }
      }
      NotificationSuccess();
      GridPO.CurrentPageIndex = 0;
      GridPO.DataBind();
    }

    protected void PnlShowBtnReject_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var poTableAdpter = new pur_poTableAdapter();

      string id = PnlShowLblNoPO.Text;

      if (poTableAdpter.FillByNomerPo(purDataSet.pur_po, id) > 0)
      {
        purDataSet.pur_po[0].status = "PO7";
        purDataSet.pur_po[0].approvedby = getFullName;

        if (poTableAdpter.Update(purDataSet.pur_po) > 0)
        {
          CUtils.UpdateLog("pur_po", id, Session["Username"].ToString(), "Update PO, status Ditolak.");
        }
      }

      NotificationSuccess();
      GridPO.CurrentPageIndex = 0;
      GridPO.DataBind();
    }

    protected void PnlCloseBtnOk_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var poTableAdpter = new pur_poTableAdapter();

      string id = PnlShowLblNoPO.Text;

      if (poTableAdpter.FillByNomerPo(purDataSet.pur_po, id) > 0)
      {
        purDataSet.pur_po[0].status = "PO5";
        purDataSet.pur_po[0].approvedby = getFullName;

        if (poTableAdpter.Update(purDataSet.pur_po) > 0)
        {
          CUtils.UpdateLog("pur_po", id, Session["Username"].ToString(), "Update PO, status Ditutup.");
        }
      }

      NotificationSuccess();
      GridPO.CurrentPageIndex = 0;
      GridPO.DataBind();
    }

    protected void PnlBatalBtnOk_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var poTableAdapter = new pur_poTableAdapter();

      string id = PnlShowLblNoPO.Text;

      if (poTableAdapter.FillByNomerPo(purDataSet.pur_po, id) > 0)
      {
        purDataSet.pur_po[0].status = "PO6";
        purDataSet.pur_po[0].approvedby = getFullName;

        if (poTableAdapter.Update(purDataSet.pur_po) > 0)
        {
          CUtils.UpdateLog("pur_po", id, Session["Username"].ToString(), "Update PO, status Dibatalkan.");
        }
      }
      NotificationSuccess();
      GridPO.CurrentPageIndex = 0;
      GridPO.DataBind();
    }

    //protected void updateStatusBarangApproved(String id) 
    //{
    //    string id_harga, id_barang;
    //    var purDataSet = new NewPurDataSet();
    //    var vtabelPODetail = new vpur_orderdetail01TableAdapter();
    //    var vtabelHarga = new vpur_requesthargaTableAdapter();
    //    var tabelRequestBarang = new pur_requestbarangTableAdapter();
    //    vtabelPODetail.FillByIdPO(purDataSet.vpur_orderdetail01, id);
    //    id_harga = purDataSet.vpur_orderdetail01[0].id;

    //    vtabelHarga.FillById(purDataSet.vpur_requestharga,Convert.ToInt32(id_harga));
    //    id_barang = purDataSet.vpur_requestharga[0].id.ToString();

    //    if (tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_barang)) > 0)
    //    {
    //        tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_barang));
    //        purDataSet.pur_requestbarang[0].status = "PO3";
    //        tabelRequestBarang.Update(purDataSet.pur_requestbarang);
    //    }
    //    else 
    //    {
    //        notifUpdateGagal();
    //    }
    //}

    //protected void updateStatusBarangRejected(String id) 
    //{
    //    string id_harga, id_barang;
    //    var purDataSet = new NewPurDataSet();
    //    var vtabelPODetail = new vpur_orderdetail01TableAdapter();
    //    var vtabelHarga = new vpur_requesthargaTableAdapter();
    //    var tabelRequestBarang = new pur_requestbarangTableAdapter();
    //    vtabelPODetail.FillByIdPO(purDataSet.vpur_orderdetail01, id);
    //    id_harga = purDataSet.vpur_orderdetail01[0].id;

    //    vtabelHarga.FillById(purDataSet.vpur_requestharga,Convert.ToInt32(id_harga));
    //    id_barang = purDataSet.vpur_requestharga[0].id.ToString();

    //    if (tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_barang)) > 0)
    //    {
    //        tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_barang));
    //        purDataSet.pur_requestbarang[0].status = "PO6";
    //        tabelRequestBarang.Update(purDataSet.pur_requestbarang);
    //    }
    //    else
    //    {
    //        notifUpdateGagal();
    //    }
    //}

    //protected void updateStatusBarangDitutup(String id) 
    //{
    //    string id_harga, id_barang;
    //    var purDataSet = new NewPurDataSet();
    //    var vtabelPODetail = new vpur_orderdetail01TableAdapter();
    //    var vtabelHarga = new vpur_requesthargaTableAdapter();
    //    var tabelRequestBarang = new pur_requestbarangTableAdapter();
    //    vtabelPODetail.FillByIdPO(purDataSet.vpur_orderdetail01, id);
    //    id_harga = purDataSet.vpur_orderdetail01[0].id;

    //    vtabelHarga.FillById(purDataSet.vpur_requestharga, Convert.ToInt32(id_harga));
    //    id_barang = purDataSet.vpur_requestharga[0].id.ToString();

    //    if (tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_barang)) > 0)
    //    {
    //        tabelRequestBarang.FillById(purDataSet.pur_requestbarang, Convert.ToInt32(id_barang));
    //        purDataSet.pur_requestbarang[0].status = "PO8";
    //        tabelRequestBarang.Update(purDataSet.pur_requestbarang);
    //    }
    //    else
    //    {
    //        notifUpdateGagal();
    //    }
    //}

    public string getUnitKerja
    {
      get
      {
        string result = Session["UnitKerja"].ToString();
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

    protected void PODataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          loadGridAll();
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M")
        {
          loadGridDefault();
        }
        else if (roles == "Purchasing Manufacturing") {
          loadGridManufacture();
        }
      }
    }   
  }
}
