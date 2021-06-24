using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using TmsBackDataController.SysDataSetTableAdapters;
using System.Web.Security;
using TmsBackDataController;
using Telerik.Web.UI;
using TmsBackDataController.PurDataSetTableAdapters;
using System.Drawing;
using ManagementSystem.Helper;
using System.Diagnostics;

namespace ManagementSystem.Purchasing.WorkRequest.Project.Work_Order
{
  public partial class List : System.Web.UI.Page
  {
    private readonly ReportParameter[] _myReportParametersA = new ReportParameter[24];
    protected IEnumerable<ReportParameter> MyParamEnumA;

    string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
    string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
    string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
    string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };

    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      string unittkerja = Session["UnitKerja"].ToString();

      if (!IsPostBack)
      {
        if (unittkerja == "T.H.O") { BtnInputPO.Visible = false; }

        string[] rolesArray = Roles.GetRolesForUser();
        foreach (string roles in rolesArray)
        {
          if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing" || roles == "Direktur Purchasing" || roles == "Staff Purchasing")
          {
            PnlContent2.Visible = true;
            PnlContent1.Visible = false;
          }
        }
      }
      else {
        if (ViewState.Count > 0)
        {
          WODataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    protected void BtnInputPO_Click(object sender, EventArgs e)
    {
      Response.Redirect("Input.aspx");
    }

    protected void GridWO_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var woTableAdapter = new pur_woTableAdapter();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;
        Label createdby = (item.FindControl("LblCreatedBy") as Label);

        string id = item["id"].Text;
        woTableAdapter.FillById(purDataSet.pur_wo, id);

        string nama = purDataSet.pur_wo[0].createdby;
        char[] splitchar = { ' ' };
        string[] strr = nama.Split(splitchar);
        if (strr.Length > 1)
        {
          createdby.Text = nama.Substring(0, nama.IndexOf(" "));
        }
        else
        {
          createdby.Text = nama;
        }

        Button btnPrint = item.FindControl("BtnPrint") as Button;
        Button btnUpdate = item.FindControl("BtnUpdate") as Button;
        Button btnApprove = item.FindControl("BtnApprove") as Button;
        Button btnClose = item.FindControl("BtnClose") as Button;
        Button btnBatal = item.FindControl("BtnBatal") as Button;

        string status = purDataSet.pur_wo[0].status;
        decimal totalbiaya = Convert.ToDecimal(purDataSet.pur_wo[0].totalbiaya);

        string currency = item["currency"].Text;
        decimal kurs = Convert.ToDecimal(wodetailTableAdapter.ScalarGetKursByWoId(id));
        string[] rolesArray = Roles.GetRolesForUser();

        decimal rangetotal;
        if (currency == "IDR")
        {
          rangetotal = totalbiaya;
        }
        else
        {
          rangetotal = kurs * totalbiaya;
        }

        foreach (string roles in rolesArray)
        {
          if (rangetotal <= 10000000)
          {
            if (roles == "Supervisor Purchasing" || roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              if (status == "WO1")  // WO Dibuat ...
              {
                btnUpdate.Visible = true;
                btnApprove.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO2") // WO Pending ..
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
            else
            {
              if (status == "WO1" || status == "WO2") // WO Dibuat, WO Pending ...
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
          }
          else if (10000000 <= rangetotal && rangetotal <= 50000000)
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              if (status == "WO1") // WO Dibuat ...
              {
                btnUpdate.Visible = true;
                btnApprove.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO2") // WO Pending ...
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
            else
            {
              if (status == "WO1" || status == "WO2") // WO Dibuat, WO Pending ...
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
          }
          else if (rangetotal >= 50000000 && rangetotal <= 100000000)
          {
            if (roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              if (status == "WO1") // WO Dibuat...
              {
                btnUpdate.Visible = true;
                btnApprove.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO2") // WO Pending ...
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
            else
            {
              if (status == "WO1" || status == "WO2") // WO Dibuat, WO Pending ....
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
          }
          else if (rangetotal >= 100000000)
          {
            if (roles == "Direktur Purchasing")
            {
              if (status == "WO1") //WO Dibuat...
              {
                btnUpdate.Visible = true;
                btnApprove.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO2") // WO Pending ...
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
              }
            }
            else
            {
              if (status == "WO1" || status == "WO2") // WO Dibuat, WO Pending ...
              {
                btnUpdate.Visible = true;
                btnBatal.Visible = true;
              }
              else if (status == "WO4") // WO Disetujui ...
              {
                btnPrint.Visible = true;
                btnClose.Visible = true;
              }
            }
          }
          
          //btn wo close
          if (roles == "Staff Purchasing")
          {
            if (status == "WO4") // WO Disetujui ...
            {
              btnClose.Visible = true;
            }
          }
        }

        //color row status
        var imgIsLocked = (System.Web.UI.WebControls.Image)item.FindControl("ImgIsLocked");
        if (status == "WO1" || status == "WO2") // WO Dibuat, WO Pending ...
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Orange;
        }
        else if (status == "WO3") // WO Dibatalkan
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Gray;
        }
        else if (status == "WO4") // WO Disetujui
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.OrangeRed;
        }
        else
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Green;

          imgIsLocked.ImageUrl = "~/images/icons/icons8-check-lock-48.png";
          imgIsLocked.Visible = true;
        }
        item["status"].Text = PurchaseUtils.GetStatusJasa(status);

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

      _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
    }

    protected void GridWO_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["id"].Text;
        string status = gridDataItem["status"].Text;
        ViewState["Id"] = id;

        if (status == "WO2" || status == "WO Pending")
        {
          // Pnl Confirmation
          PnlMessageLblTitlebar.Text = "Warning";
          PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-high-priority-48.png";
          PnlMessageLblMessage.Text = "Item kosong. Harap isi terlebih dahulu.";
          PnlMessageModalPopupExtender.Show();
        }
        else
        {
          var url = string.Concat("Detail.aspx?pId=", ViewState["Id"].ToString());
          Response.Redirect(url);
        }
      }
      else if (e.CommandName == "UpdateClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;
        var id = gridDataItem["id"].Text;

        ViewState["Id"] = id;
        var url = string.Concat("Edit.aspx?pId=", ViewState["Id"].ToString());
        Response.Redirect(url);
      }
      else if (e.CommandName == "ApproveClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        var id = gridDataItem["id"].Text;

        PnlApproveHdIdWO.Value = id;
        PnlApproveLblNoWO.Text = gridDataItem["nomerwo"].Text;

        PnlApproveModalPopupExtender.Show();
      }
      else if (e.CommandName == "PrintClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        PnlPrintHdIdWO.Value = gridDataItem["id"].Text;
        PnlPrintLblNoWO.Text = gridDataItem["nomerwo"].Text;

        PnlPrintModalPopupExtender.Show();
      }
      else if (e.CommandName == "CloseClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        PnlCLoseHdIdWO.Value = gridDataItem["id"].Text;
        PnlCloseLblNoWO.Text = gridDataItem["nomerwo"].Text;

        PnlCloseModalPopupExtender.Show();
      }
      else if (e.CommandName == "BatalClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        PnlBatalHdIdWO.Value = gridDataItem["id"].Text;
        PnlBatalLblNoWO.Text = gridDataItem["nomerwo"].Text;

        PnlBatalModalPopupExtender.Show();
      }

    }

    protected void PoPending()
    {
      PnlMessageLblTitlebar.Text = "Information";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
      PnlMessageLblMessage.Text = " Ada data kosong,mohon update dulu";
      PnlMessageModalPopupExtender.Show();
    }

    protected void PnlPrintBtnPrint_Click(object sender, EventArgs e)
    {
      PrintA();
    }

    //batas
    //batas
    private void PrintA()
    {
      var reportDataSet = new ReportsDataSet();
      var purDataSet = new PurDataSet();
      var vwodetail02TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_wodetail02TableAdapter();
      var supplierTableAdapter = new master_supplierTableAdapter();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      var vwo01TableAdapter = new vpur_wo01TableAdapter();

      string pUnitKerja, pUsernameDate, pNamaSupplier, pAlamatSupplier, pTlpSupplier, pCPSupplier, pDiskon, pTotalDiskon,
        pPPn, pTotalPPn, pPPh, pTotalPPh, pTotalBiayaLain, pNoWo, pTglWO, pTerbilang1, pTerbilang2, pCurrency,
        pTglPenyelesaian, pPayment, pNotes, pKeterangan, pBiayaLain, pTotal;

      string nounit = PnlPrintHdIdWO.Value;
      vwodetail02TableAdapter.FillByWoId(reportDataSet.vpur_wodetail02, nounit);

      vwo01TableAdapter.FillById(purDataSet.vpur_wo01, nounit);
      string ket = purDataSet.vpur_wo01[0].keterangan;      
      string currency = purDataSet.vpur_wo01[0].currency;
      string terbilang, currc;
      decimal jmldiskon;
      decimal totalWO = Convert.ToDecimal(Convert.ToDouble(purDataSet.vpur_wo01[0].totalbiaya));
      //unitkerja
      string unitkerja = purDataSet.vpur_wo01[0].unitkerja;
      if (unitkerja == "ASSEMBLING")
      {
        pUnitKerja = "MANUFACTURE ASSEMBLING";
      }
      else if (unitkerja == "SPARE PARTS")
      {
        pUnitKerja = "MANUFACTURE SPARE PARTS";
      }
      else if (unitkerja == "SHIPYARD")
      {
        pUnitKerja = "SHIPYARD";
      }
      else
      {
        pUnitKerja = "-";
      }
      //username
      pUsernameDate = String.Concat(Session["FullName"].ToString() + " (" + DateTime.Now + ")");
      //supplier
      string idSupplier = purDataSet.vpur_wo01[0].supplier_id.ToString();
      supplierTableAdapter.FillById(purDataSet.master_supplier, Convert.ToInt32(idSupplier));
      pNamaSupplier = purDataSet.master_supplier[0].nama;
      pAlamatSupplier = purDataSet.master_supplier[0].alamat + " ," + purDataSet.master_supplier[0].kota;
      pTlpSupplier = purDataSet.master_supplier[0].IsnotelpkantorNull() ? "" : purDataSet.master_supplier[0].notelpkantor;
      pCPSupplier = purDataSet.master_supplier[0].IskontakpersonNull() ? "" : purDataSet.master_supplier[0].kontakperson;
      //currency
      if (currency == "IDR") { currc = "Rp"; terbilang = "rupiah"; }
      else if (currency == "USD" || currency == "AUD" || currency == "SGD") { currc = "$"; terbilang = "dollar"; }
      else { currc = "€"; terbilang = "euro"; }
      //terbilang
      pTerbilang1 = this.Terbilang(totalWO).TrimStart();
      pTerbilang2 = terbilang;
      //totalawal
      decimal totalawal = Convert.ToDecimal(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(nounit));
      //pTotalAwal = String.Format("{0,20:N2}", totalawal);
      //diskon
      string typediskon = purDataSet.vpur_wo01[0].typediskon;
      if (typediskon == "-")
      {
        pDiskon = "";
        pTotalDiskon = "";
        jmldiskon = totalawal;
      }
      else if (typediskon == "%")
      {
        pDiskon = Convert.ToDouble(purDataSet.vpur_wo01[0].diskon).ToString() + "%";

        decimal diskon = Convert.ToDecimal(purDataSet.vpur_wo01[0].diskon);
        decimal besardiskon = (totalawal * diskon) / 100;
        decimal totaldiskon = totalawal - besardiskon;
        jmldiskon = totaldiskon;

        pTotalDiskon = String.Format("{0,20:N2}", besardiskon);
      }
      else
      {
        pDiskon = "";
        pTotalDiskon = String.Format("{0,20:N2}", purDataSet.vpur_wo01[0].diskon);
        jmldiskon = Convert.ToDecimal(pTotalDiskon);
      }
      //ppn
      float ppn = Convert.ToSingle(purDataSet.vpur_wo01[0].ppn);
      if (ppn == 0) { pPPn = ""; pTotalPPn = ""; }
      else
      {
        pPPn = "10%";
        decimal totalawal2 = jmldiskon;
        decimal besarppn = (10 * totalawal2) / 100;
        decimal totalppn = totalawal2 - besarppn;
        pTotalPPn = String.Format("{0,20:N2}", besarppn);
      }
      //pph
      decimal pph = Convert.ToDecimal(purDataSet.vpur_wo01[0].pph);
      if (pph == 0) { pPPh = "";  pTotalPPh = ""; }
      else
      {
        pPPh = Convert.ToString(pph) + "%";
        decimal totalawal2 = jmldiskon;
        decimal besarpph = (pph * totalawal2) / 100;
        decimal totalpph = totalawal2 - besarpph;
        pTotalPPh = String.Format("{0,20:N2}", besarpph);
      }
      //biaya lain
      decimal biayalain = Convert.ToDecimal(purDataSet.vpur_wo01[0].hargajasalain);
      if (biayalain == 0) { pTotalBiayaLain = ""; }
      else
      {
        pTotalBiayaLain = String.Format("{0,20:N2}", purDataSet.vpur_wo01[0].hargajasalain);
      }
      //lain-lain
      pNoWo = purDataSet.vpur_wo01[0].nomerwo;
      pTglWO = Convert.ToDateTime(purDataSet.vpur_wo01[0].tglwo).ToString("dd MMMM yyyy");
      pCurrency = currency;
      pTglPenyelesaian = purDataSet.vpur_wo01[0].tglpenyelesaian.ToShortDateString();
      pPayment = purDataSet.vpur_wo01[0].payment;
      pNotes = purDataSet.vpur_wo01[0].notes;
      pKeterangan = purDataSet.vpur_wo01[0].keterangan;
      pBiayaLain = purDataSet.vpur_wo01[0].jasalain;
      pTotal = String.Format("{0,20:N2}", purDataSet.vpur_wo01[0].totalbiaya);


      var reportViewer = new ReportViewer();
      reportViewer.ProcessingMode = ProcessingMode.Local;
      if (ket == null || ket == "" || ket == string.Empty)
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptPOE.rdlc");
      }
      else
      {
        reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptPOF.rdlc");
      }

      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline;

      _myReportParametersA[0] = new ReportParameter("pUnitKerja", pUnitKerja);
      _myReportParametersA[1] = new ReportParameter("pUsernameDate", pUsernameDate);
      _myReportParametersA[2] = new ReportParameter("pNamaSupplier", pNamaSupplier);
      _myReportParametersA[3] = new ReportParameter("pAlamatSupplier", pAlamatSupplier);
      _myReportParametersA[4] = new ReportParameter("pTlpSupplier", pTlpSupplier);
      _myReportParametersA[5] = new ReportParameter("pCPSupplier", pCPSupplier);
      _myReportParametersA[6] = new ReportParameter("pTerbilang1", pTerbilang1);
      _myReportParametersA[7] = new ReportParameter("pTerbilang2", pTerbilang2);
      _myReportParametersA[8] = new ReportParameter("pDiskon", pDiskon);
      _myReportParametersA[9] = new ReportParameter("pTotalDiskon", pTotalDiskon);
      _myReportParametersA[10] = new ReportParameter("pPPn", pPPn);
      _myReportParametersA[11] = new ReportParameter("pTotalPPn", pTotalPPn);
      _myReportParametersA[12] = new ReportParameter("pPPh", pPPh);
      _myReportParametersA[13] = new ReportParameter("pTotalPPh", pTotalPPh);
      _myReportParametersA[14] = new ReportParameter("pTotalBiayaLain", pTotalBiayaLain);
      _myReportParametersA[15] = new ReportParameter("pNoWO", pNoWo);
      _myReportParametersA[16] = new ReportParameter("pTglWO", pTglWO);
      _myReportParametersA[17] = new ReportParameter("pCurrency", pCurrency);
      _myReportParametersA[18] = new ReportParameter("pTglPenyelesaian", pTglPenyelesaian);
      _myReportParametersA[19] = new ReportParameter("pPayment", pPayment);
      _myReportParametersA[20] = new ReportParameter("pNotes", pNotes);
      _myReportParametersA[21] = new ReportParameter("pKeterangan", pKeterangan);
      _myReportParametersA[22] = new ReportParameter("pBiayaLain", pBiayaLain);
      _myReportParametersA[23] = new ReportParameter("pTotal", pTotal);

      MyParamEnumA = _myReportParametersA;

      var rptViewerProjectWrPrintDataSource = new ReportDataSource();
      rptViewerProjectWrPrintDataSource.Name = "vpur_wodetail02";
      rptViewerProjectWrPrintDataSource.Value = reportDataSet.vpur_wodetail02;

      reportViewer.LocalReport.SetParameters(MyParamEnumA);
      reportViewer.LocalReport.DataSources.Add(rptViewerProjectWrPrintDataSource);

      //Code for download direct pdf

      Warning[] warnings;
      string[] streamIds;
      string mimeType = string.Empty;
      string encoding = string.Empty;
      string extension = string.Empty;

      byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

      Response.Buffer = true;
      Response.Clear();
      Response.ContentType = mimeType;
      Response.AddHeader("content-disposition", string.Concat("inline; filename=wo", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
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


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%" + TxtKataKunci.Text + "%");
      var unitkerja = Session["UnitKerja"].ToString();

      ViewState.Remove("SelectMethod");

      SearchGeneral(txtKataKunci, unitkerja);

      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          SearchDirektur(txtKataKunci);
        }
        else if (roles == "Purchasing Manufacture")
        {
          SearchManufacture(txtKataKunci);
        }
      }

      GridWO.DataBind();
    }

    private void SearchDirektur(string txtKataKunci)
    {
      if (DlistBerdasarkan.SelectedValue == "1")
      {
        WODataSource.SelectMethod = "GetDataByTypeNomerwo";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("nomerwo", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeNomerwo");
      }
      else if (DlistBerdasarkan.SelectedValue == "2")
      {
        WODataSource.SelectMethod = "GetDataByTypeSupplierNama";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("supplierNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeSupplierNama");
      }
      else if (DlistBerdasarkan.SelectedValue == "3")
      {
        WODataSource.SelectMethod = "GetDataByTypeJasaNama";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("jasaNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeJasaNama");
      }
      else if (DlistBerdasarkan.SelectedValue == "4")
      {
        WODataSource.SelectMethod = "GetDataByTypeStatus";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (DlistBerdasarkan.SelectedValue == "5")
      {
        string rangetgl1 = Convert.ToDateTime(RadPickerTanggal1.SelectedDate).ToString("yyyy-MM-dd");
        string rangetgl2 = Convert.ToDateTime(RadPickerTanggal2.SelectedDate).ToString("yyyy-MM-dd");

        WODataSource.SelectMethod = "GetDataByTypeTglwo";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("tglwo1", rangetgl1);
        WODataSource.SelectParameters.Add("tglwo2", rangetgl2);

        ViewState.Add("SelectMethod", "GetDataByTypeTglwo");
      }
    }

    private void SearchManufacture(string txtKataKunci)
    {
      if (DlistBerdasarkan.SelectedValue == "1")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitManufactureNomerwo";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("nomerwo", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureNomerwo");
      }
      else if (DlistBerdasarkan.SelectedValue == "2")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitManufactureSupplierNama";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("supplierNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureSupplierNama");
      }
      else if (DlistBerdasarkan.SelectedValue == "3")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitManufactureJasaNama";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("jasaNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureJasaNama");
      }
      else if (DlistBerdasarkan.SelectedValue == "4")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitManufactureStatus";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureStatus");
      }
      else if (DlistBerdasarkan.SelectedValue == "5")
      {
        string rangetgl1 = Convert.ToDateTime(RadPickerTanggal1.SelectedDate).ToString("yyyy-MM-dd");
        string rangetgl2 = Convert.ToDateTime(RadPickerTanggal2.SelectedDate).ToString("yyyy-MM-dd");

        WODataSource.SelectMethod = "GetDataByTypeUnitManufactureTglwo";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("tglwo1", rangetgl1);
        WODataSource.SelectParameters.Add("tglwo2", rangetgl2);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitManufactureTglwo");
      }
    }

    private void SearchGeneral(string txtKataKunci, string unitkerja)
    {
      if (DlistBerdasarkan.SelectedValue == "1")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitKerjaNomerwo";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("unitkerja", unitkerja);
        WODataSource.SelectParameters.Add("nomerwo", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaNomerwo");
      }
      else if (DlistBerdasarkan.SelectedValue == "2")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitKerjaSupplierNama";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("unitkerja", unitkerja);
        WODataSource.SelectParameters.Add("supplierNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaSupplierNama");
      }
      else if (DlistBerdasarkan.SelectedValue == "3")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitKerjaJasaNama";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("unitkerja", unitkerja);
        WODataSource.SelectParameters.Add("jasaNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaJasaNama");
      }
      else if (DlistBerdasarkan.SelectedValue == "4")
      {
        WODataSource.SelectMethod = "GetDataByTypeUnitKerjaStatus";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("unitkerja", unitkerja);
        WODataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaStatus");
      }
      else if (DlistBerdasarkan.SelectedValue == "5")
      {
        string rangetgl1 = Convert.ToDateTime(RadPickerTanggal1.SelectedDate).ToString("yyyy-MM-dd");
        string rangetgl2 = Convert.ToDateTime(RadPickerTanggal2.SelectedDate).ToString("yyyy-MM-dd");

        WODataSource.SelectMethod = "GetDataByTypeUnitKerjaTglwo";
        WODataSource.SelectParameters.Clear();
        WODataSource.SelectParameters.Add("type", "P");
        WODataSource.SelectParameters.Add("unitkerja", unitkerja);        
        WODataSource.SelectParameters.Add("tglwo1", rangetgl1);
        WODataSource.SelectParameters.Add("tglwo2", rangetgl2);

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaTglwo");
      }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      RadPickerTanggal1.SelectedDate = null;
      RadPickerTanggal2.SelectedDate = null;
      DlistBerdasarkan.SelectedIndex = 0;
      PnlKataKunci.Visible = true;
      PnlRangeTgl.Visible = false;

      ViewState.Remove("SelectMethod");

      CancelGeneral();

      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          CancelDirektur();
        }
        else if (roles == "Purchasing Manufacturing")
        {
          CancelManufacture();
        }
      }

      GridWO.DataBind();
      GridWO.CurrentPageIndex = 0;
    }

    private void CancelDirektur()
    {
      ViewState.Add("SelectMethod", "GetDataByType");

      WODataSource.SelectMethod = "GetDataByType";
      WODataSource.SelectParameters.Clear();
      WODataSource.SelectParameters.Add("type", "P");
    }

    private void CancelManufacture()
    {
      ViewState.Add("SelectMethod", "GetDataByTypeUnitManufacture");

      WODataSource.SelectMethod = "GetDataByTypeUnitManufacture";
      WODataSource.SelectParameters.Clear();
      WODataSource.SelectParameters.Add("type", "P");
    }

    private void CancelGeneral()
    {
      ViewState.Add("SelectMethod", "GetDataByTypeUnitKerja");

      WODataSource.SelectMethod = "GetDataByTypeUnitKerja";
      WODataSource.SelectParameters.Clear();
      WODataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      WODataSource.SelectParameters.Add("type", "P");
    }

    protected void PnlCloseBtnOk_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var woTableAdapter = new pur_woTableAdapter();
      string nomerUnit =  PnlCLoseHdIdWO.Value;

      woTableAdapter.FillById(purDataSet.pur_wo, nomerUnit);
      purDataSet.pur_wo[0].status = "WO5";
      if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
      {
        CUtils.UpdateLog("pur_wo", nomerUnit, Session["Username"].ToString(), "Update WO status 'Selesai'");
      }

      GridWO.DataBind();
    }

    protected void PnlApproveBtnOk_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var woTableAdapter = new pur_woTableAdapter();
      string nomerUnit = PnlApproveHdIdWO.Value;

      woTableAdapter.FillById(purDataSet.pur_wo, nomerUnit);
      purDataSet.pur_wo[0].status = "WO4";
      purDataSet.pur_wo[0].approvedby = Session["FullName"].ToString();
      purDataSet.pur_wo[0].dateapproved = DateTime.Now;
      if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
      {
        CUtils.UpdateLog("pur_wo", nomerUnit, Session["Username"].ToString(), "Update WO Status 'Disetujui'");
      }

      GridWO.DataBind();
    }

    protected void PnlBatalBtnOk_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var woTableAdapter = new pur_woTableAdapter();
      string nomerUnit = PnlBatalHdIdWO.Value;

      woTableAdapter.FillById(purDataSet.pur_wo, nomerUnit);
      purDataSet.pur_wo[0].status = "WO3";
      if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
      {
        CUtils.UpdateLog("pur_wo", nomerUnit, Session["Username"].ToString(), "Update WO Status 'Dibatalkan'");
      }

      GridWO.DataBind();
    }

    protected void DlistBerdasarkan_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DlistBerdasarkan.SelectedValue == "1" || DlistBerdasarkan.SelectedValue == "2" || DlistBerdasarkan.SelectedValue == "3")
      {
        PnlRangeTgl.Visible = false;
        PnlKataKunci.Visible = true;
        PnlStatus.Visible = false;
      }
      else if (DlistBerdasarkan.SelectedValue == "4")
      {
        PnlStatus.Visible = true;
        PnlKataKunci.Visible = false;
        PnlRangeTgl.Visible = false;
      }
      else {
        PnlStatus.Visible = false;
        PnlKataKunci.Visible = false;
        PnlRangeTgl.Visible = true;
      }
    }

    protected void PurchaseOrderDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
    }

    protected void WODataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          WODataSource.SelectMethod = "GetDataByType";
          WODataSource.SelectParameters.Clear();
          WODataSource.SelectParameters.Add("type", "P");
        }
        else if (roles == "Purchasing Manufacturing")
        {
          WODataSource.SelectMethod = "GetDataByTypeUnitManufacture";
          WODataSource.SelectParameters.Clear();
          WODataSource.SelectParameters.Add("type", "P");
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M")
        {
          WODataSource.SelectMethod = "GetDataByTypeUnitKerja";
          WODataSource.SelectParameters.Clear();
          WODataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
          WODataSource.SelectParameters.Add("type", "P");
        }
      }
    }

    
  }
}
