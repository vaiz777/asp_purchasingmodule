using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.SysDataSetTableAdapters;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using System.Diagnostics;
using Telerik.Web.UI;
using ManagementSystem.Helper;

namespace ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Order
{
  public partial class Edit : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      //label
      string unitkerja = Session["UnitKerja"].ToString();
      if (unitkerja == "SHIPYARD") { LblTitle.Text = "Edit Work Order (Non Project)"; PnlKeterangan.Visible = true; }
      else { LblTitle.Text = "Edit Work Order"; PnlKeterangan.Visible = false; }

      if (!IsPostBack)
      {
        //NO PO
        var id = Request.QueryString["pId"];
        Pnl1HdIdWO.Value = id;

        //TextBox Supplier & No Project
        var purDataSet = new PurDataSet();
        var woTableAdapter = new pur_woTableAdapter();
        var supplierTableAdapter = new master_supplierTableAdapter();
        var vwodetail02TableAdapter = new vpur_wodetail02TableAdapter();

        if (woTableAdapter.FillById(purDataSet.pur_wo, id) > 0)
        {
          Pnl1TxtNoWO.Text = purDataSet.pur_wo[0].nomerwo;
          RDateTglWO.SelectedDate = purDataSet.pur_wo[0].tglwo;
          PgLainTxtTglPenyelesaian.Text = purDataSet.pur_wo[0].tglpenyelesaian.ToShortDateString();
          string typediskon = purDataSet.pur_wo[0].typediskon;
          if (typediskon == "%")
          {
            PgBiayaDListDiskon.SelectedValue = "%";
            PgBiayaRadTxtDiskon.Visible = true;
            PgBiayaRadTxtDiskon.Value = Convert.ToDouble(purDataSet.pur_wo[0].diskon);
          }
          else if (typediskon == "-") { PgBiayaDListDiskon.SelectedValue = "-"; }
          else
          {
            PgBiayaDListDiskon.SelectedValue = "Nominal";
            PgBiayaRadTxtDiskon.Visible = true;
            PgBiayaRadTxtDiskon.Value = Convert.ToDouble(purDataSet.pur_wo[0].diskon);
          }
          PgBiayaHdDiskonVal.Value = Convert.ToDouble(purDataSet.pur_wo[0].diskon).ToString();
          PgBiayaDListPPN.SelectedValue = purDataSet.pur_wo[0].ppn.ToString();
          PgBiayaHdPPNVal.Value = Convert.ToDouble(purDataSet.pur_wo[0].ppn).ToString();
          PgBiayaRadTxtPPh.Value = purDataSet.pur_wo[0].pph;
          PgBiayaHdPPhVal.Value = Convert.ToDouble(purDataSet.pur_wo[0].pph).ToString();
          PgBiayaTxtLain.Text = purDataSet.pur_wo[0].jasalain;
          PgBiayaRadNmrcTxtBiayaLain.Value = Convert.ToDouble(purDataSet.pur_wo[0].hargajasalain);
          PgBiayaRAdTxtTotal.Value = Convert.ToDouble(purDataSet.pur_wo[0].totalbiaya);
          PgLainTxtPembayaran.Text = purDataSet.pur_wo[0].payment;
          PgLainTxtNotes.Text = purDataSet.pur_wo[0].notes;
          PgLainTxtKet.Text = purDataSet.pur_wo[0].keterangan;
        }

        if (vwodetail02TableAdapter.FillByWoId(purDataSet.vpur_wodetail02, id) > 0)
        {
          Pnl1HdIdSupplier.Value = purDataSet.vpur_wodetail02[0].supplier_id.ToString();
          Pnl1TxtSupplier.Text = purDataSet.vpur_wodetail02[0].supplier_nama;
          Pnl1TxtSupplier.Enabled = false;
          Pnl1BtnBrowseSupplier.Visible = false;
        }
      }
      else if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          SupplierDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          //ReqNoteDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          //JasaDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    private void NotificationFailure(Exception ex)
    {
      var stackTrace = new StackTrace(ex, true);
      var frame = stackTrace.GetFrame(0);

      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>", frame.GetMethod(), "(", frame.GetFileLineNumber(), "):<br>", ex.Message);
      PnlMessageModalPopupExtender.Show();

      _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
    }

    protected void Pnl1BtnBrowseWorkRequest_Click(object sender, EventArgs e)
    {
      PnlBrowseWorkRequestModalPopupExtender.Show();
    }

    protected void Pnl1BtnBrowseJasa_Click(object sender, EventArgs e)
    {
      PnlBrowseJasaModalPopupExtender.Show();
    }

    protected void GridWorkRequest_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        Pnl1TxtNoWR.Text = id;
        PnlBrowseWorkRequestModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseWorkRequestModalPopupExtender.Show();
      }
    }

    protected void GridOrderDetail_ItemDataBound(object sender, GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();

      string nomer = Pnl1HdIdWO.Value;

      if (e.Item is GridFooterItem)
      {
        double total = Convert.ToDouble(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(nomer));
        GridFooterItem footerItem = e.Item as GridFooterItem;
        footerItem["totalharga"].Text = String.Concat("TOTAL : " + String.Format("{0,20:N2}", total));
      }
    }

    protected void GridOrderDetail_ItemCreated(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridFooterItem)
      {
        GridFooterItem footerItem = (GridFooterItem)e.Item;
        LinkButton btn = new LinkButton();
        btn.Text = "Delete";
        btn.Click += new EventHandler(BtnDelete_Click);
        footerItem.Cells[2].Controls.Add(btn);
      }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      string idOrdernoteWr = Pnl1HdIdWO.Value;

      foreach (GridDataItem item in GridOrderDetail.MasterTableView.Items)
      {
        if (item is GridDataItem)
        {
          GridDataItem gridDataItem = item as GridDataItem;
          CheckBox checkRow = item.FindControl("CheckRow") as CheckBox;

          if (checkRow.Checked && checkRow != null)
          {
            // Access data key
            string orderdetailId = item["id"].Text;
            string idReqJasa = item["wrdetail_id"].Text;

            wodetailTableAdapter.DeleteQueryById(Convert.ToInt64(orderdetailId));
            if (wrdetailTableAdapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(idReqJasa)) > 0)
            {
              string nowr = purDataSet.pur_wrdetail[0].wr_id;
              purDataSet.pur_wrdetail[0].status = "J4";
              if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
              {
                CUtils.UpdateLog("pur_wrdetail", idReqJasa, Session["Username"].ToString(), "Update WR Detail Status ''");
              }
              UpdateRequestNoteWr(nowr);
            }
            UpdateStatusOrdernoteWr(idOrdernoteWr);

            //notif success delete
            PnlMessageLblTitlebar.Text = "Success.";
            PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
            PnlMessageLblMessage.Text = "Data berhasil di delete.";
            PnlMessageModalPopupExtender.Show();
          }
        }
        else
        {
          CheckBoxError();
        }
      }
      GridOrderDetail.DataBind();
      GridJasa.DataBind();
      GridWorkRequest.DataBind();
    }

    private void UpdateStatusOrdernoteWr(string idOrdernoteWr)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      var woTableAdapter = new pur_woTableAdapter();

      if (Convert.ToInt32(wodetailTableAdapter.ScalarGetCountByWoId(idOrdernoteWr)) == 0)
      {
        woTableAdapter.FillById(purDataSet.pur_wo, idOrdernoteWr);
        purDataSet.pur_wo[0].status = "WO2";
        if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
        {
          CUtils.UpdateLog("pur_wo", idOrdernoteWr, Session["Username"].ToString(), "Update status WO 'Pending'");
        }
      }
      else
      {
        woTableAdapter.FillById(purDataSet.pur_wo, idOrdernoteWr);
        purDataSet.pur_wo[0].status = "WO1";
        if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
        {
          CUtils.UpdateLog("pur_wo", idOrdernoteWr, Session["Username"].ToString(), "Update status WO 'Dibuat'");
        }
      }
    }

    private void UpdateRequestNoteWr(string nowr)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();

      int countItem = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(nowr));
      int countItemPriceApprove = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J4", nowr));
      int countItemAddedWO = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J5", nowr));

      if (countItemAddedWO > 0)
      {
        wrTableAdapter.FillById(purDataSet.pur_wr, nowr);
        purDataSet.pur_wr[0].status = "WR8";
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Add to WO (Pending)'");
        }
      }
      else if (countItem == countItemPriceApprove)
      {
        wrTableAdapter.FillById(purDataSet.pur_wr, nowr);
        purDataSet.pur_wr[0].status = "WR7";
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Disetujui'");
        }
      }
    }

    private void CheckBoxError()
    {
      PnlMessageLblTitlebar.Text = "Information";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-info-squared-48.png";
      PnlMessageLblMessage.Text = " Pilih dulu data yang akan dihapus";
      PnlMessageModalPopupExtender.Show();
    }

    protected void CheckHeaderCheckedChanged(object sender, EventArgs e)
    {
      foreach (GridDataItem item in GridOrderDetail.MasterTableView.Items)
      {
        CheckBox chkbx = (CheckBox)item["CheckTemp"].FindControl("CheckRow");
        chkbx.Checked = !chkbx.Checked;
      }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var woTableAdapter = new pur_woTableAdapter();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();

      string idOrdernotewr = Request.QueryString["pId"];
      woTableAdapter.FillById(purDataSet.pur_wo, idOrdernotewr);

      try
      {
        purDataSet.pur_wo[0].tglwo = RDateTglWO.SelectedDate.Value == null ? RDateTglWO.MinDate : RDateTglWO.SelectedDate.Value;
        purDataSet.pur_wo[0].ppn = Convert.ToSingle(PgBiayaDListPPN.SelectedValue);
        purDataSet.pur_wo[0].ppnval = PgBiayaHdPPNVal.Value == string.Empty ? 0 : Convert.ToDecimal(PgBiayaHdPPNVal.Value);
        purDataSet.pur_wo[0].pph = PgBiayaRadTxtPPh.Value == null ? 0 : Convert.ToSingle(PgBiayaRadTxtPPh.Value);
        purDataSet.pur_wo[0].pphval = PgBiayaHdPPhVal.Value == string.Empty ? 0 : Convert.ToDecimal(PgBiayaHdPPhVal.Value);
        purDataSet.pur_wo[0].diskon = PgBiayaRadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadTxtDiskon.Value);
        if (PgBiayaDListDiskon.SelectedValue == "-") { purDataSet.pur_wo[0].typediskon = "-"; }
        else if (PgBiayaDListDiskon.SelectedValue == "%") { purDataSet.pur_wo[0].typediskon = "%"; }
        else { purDataSet.pur_wo[0].typediskon = "Nominal"; }
        purDataSet.pur_wo[0].diskonval = PgBiayaHdDiskonVal.Value == string.Empty ? 0 : Convert.ToDecimal(PgBiayaHdDiskonVal.Value);
        purDataSet.pur_wo[0].jasalain = PgBiayaTxtLain.Text;
        purDataSet.pur_wo[0].hargajasalain = PgBiayaRadNmrcTxtBiayaLain.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadNmrcTxtBiayaLain.Value);
        purDataSet.pur_wo[0].totalbiaya = PgBiayaRAdTxtTotal.Value == null ? 0 : Convert.ToDecimal(PgBiayaRAdTxtTotal.Value);
        purDataSet.pur_wo[0].keterangan = PgLainTxtKet.Text;
        purDataSet.pur_wo[0].notes = PgLainTxtNotes.Text;
        purDataSet.pur_wo[0].tglpenyelesaian = PgLainTxtTglPenyelesaian.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PgLainTxtTglPenyelesaian.Text);
        purDataSet.pur_wo[0].payment = PgLainTxtPembayaran.Text;
        if (Convert.ToInt32(wodetailTableAdapter.ScalarGetCountByWoId(idOrdernotewr)) == 0)
        {
          purDataSet.pur_wo[0].status = "WO2";
        }
        else
        {
          purDataSet.pur_wo[0].status = "WO1";
        }

        if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
        {
          CUtils.UpdateLog("pur_wo", idOrdernotewr, Session["Username"].ToString(), "Update WO");
        }
        NotificationSuccess();
        Response.Redirect("List.aspx");
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void GridJasa_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataset = new PurDataSet();
      var vwrdetail01TableAdapter = new vpur_wrdetail01TableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        vwrdetail01TableAdapter.FillById(purDataset.vpur_wrdetail01, Convert.ToInt64(id));

        Pnl1HdIdWRDetail.Value = purDataset.vpur_wrdetail01[0].id.ToString();
        Pnl1TxtJasa.Text = purDataset.vpur_wrdetail01[0].jasa_nama;
        Pnl1TxtJmlJasa.Text = purDataset.vpur_wrdetail01[0].jmljasa.ToString();
        Pnl1TxtSatuan.Text = purDataset.vpur_wrdetail01[0].satuan;
        Pnl1TxtCurrency.Text = purDataset.vpur_wrdetail01[0].currency;
        Pnl1RadNmbrcTxtHarga.Value = Convert.ToDouble(purDataset.vpur_wrdetail01[0].harga);

        if (purDataset.vpur_wrdetail01[0].currency != "IDR")
        {
          PnlKurs.Visible = true;
        }
        else
        {
          PnlKurs.Visible = false;
        }

        PnlBrowseJasaModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseJasaModalPopupExtender.Show();
      }
    }

    protected void Pnl1BtnAdd_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      var wodetailRow = purDataSet.pur_wodetail.Newpur_wodetailRow();

      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      wrdetailTableAdapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(Pnl1HdIdWRDetail.Value));

      try
      {
        //add data in orderdetailwr
        wodetailRow.wo_id = Pnl1HdIdWO.Value;
        wodetailRow.wrdetail_id = Convert.ToInt64(Pnl1HdIdWRDetail.Value);
        wodetailRow.diskon = Pnl1RadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(Pnl1RadTxtDiskon.Value);
        wodetailRow.kurs = Pnl1RadNmrcTxtKurs.Value == null ? 0 : Convert.ToDouble(Pnl1RadNmrcTxtKurs.Value);
        wodetailRow.totalharga = Pnl1RadNmrcTxtTotal.Value == null ? 0 : Convert.ToDecimal(Pnl1RadNmrcTxtTotal.Value);
        if (Pnl1RbSatuan.Checked == true) { wodetailRow.typetotal = true; } else { wodetailRow.typetotal = false; }

        purDataSet.pur_wodetail.Addpur_wodetailRow(wodetailRow);
        if (wodetailTableAdapter.Update(purDataSet.pur_wodetail) > 0)
        {
          int tahun = DateTime.Now.Year;
          CUtils.UpdateLog("pur_wodetail", (Convert.ToInt32(wodetailTableAdapter.ScalarGetMaxById()) + 1).ToString(), Session["Username"].ToString(), "p_insert_orderdetailwr");
        }

        //update data in req jasa
        purDataSet.pur_wrdetail[0].status = "J5";
        if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", Pnl1HdIdWRDetail.Value, Session["Username"].ToString(), "Update WR Detail Status 'Add to WO'");
        }

        //update data in req note
        string noWR = Pnl1TxtNoWR.Text;
        UpdateStatusWorkReq(noWR);

        //update data in ordernotewr
        UpdateStatusOrdernoteWr(Pnl1HdIdWO.Value);

        NotificationSuccess();
        ClearPnl1();
        GridOrderDetail.DataBind();
        GridWorkRequest.DataBind();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    private void UpdateStatusWorkReq(string noWR)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();

      if (wrTableAdapter.FillById(purDataSet.pur_wr, noWR) > 0)
      {
        int dataWOAdded = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J5", noWR));
        int dataWR = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(noWR));

        if (dataWOAdded == dataWR)
        {
          wrTableAdapter.FillById(purDataSet.pur_wr, noWR);
          purDataSet.pur_wr[0].status = "WR9";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, Session["Username"].ToString(), "Update WR Status 'Add to WO'");
          }
        }
        else
        {
          wrTableAdapter.FillById(purDataSet.pur_wr, noWR);
          purDataSet.pur_wr[0].status = "WR8";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, Session["Username"].ToString(), "Update WR status 'Add to WO (tertunda)'");
          }
        }
      }
    }

    private void ClearPnl1()
    {
      Pnl1TxtSupplier.Enabled = false;
      Pnl1BtnBrowseSupplier.Visible = false;
      Pnl1TxtNoWR.Text = string.Empty;
      Pnl1LblIdReqJasa.Text = string.Empty;
      Pnl1TxtJasa.Text = string.Empty;
      Pnl1TxtJmlJasa.Text = string.Empty;
      Pnl1TxtSatuan.Text = string.Empty;
      Pnl1TxtCurrency.Text = string.Empty;
      Pnl1RadNmbrcTxtHarga.Text = string.Empty;
      PnlKurs.Visible = false;
      Pnl1RadNmrcTxtKurs.Text = string.Empty;
      Pnl1RadTxtDiskon.Text = string.Empty;
      Pnl1RbItemJasa.Checked = false;
      Pnl1RbSatuan.Checked = false;
      Pnl1RadNmrcTxtTotal.Text = string.Empty;
    }

    private void NotificationSuccess()
    {
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void Pnl1TxtCurrency_TextChanged(object sender, EventArgs e)
    {
      if (Pnl1TxtCurrency.Text != "IDR")
      {
        PnlKurs.Visible = true;
      }
    }

    protected void PgBiayaDListPPN_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void PgBiayaDListDiskon_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (PgBiayaDListDiskon.SelectedValue == "-")
      {
        PgBiayaRadTxtDiskon.Visible = false;
      }
      else
      {
        PgBiayaRadTxtDiskon.Visible = true;
      }
    }

    protected void PgBiayaBtnHitungNota_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      decimal totalitem, totalawal, totaldiskon, totalppn, totalpph, totalakhir;

      decimal diskon = PgBiayaRadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadTxtDiskon.Value);
      decimal pph = PgBiayaRadTxtPPh.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadTxtPPh.Value);
      decimal biayalain = PgBiayaRadNmrcTxtBiayaLain.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadNmrcTxtBiayaLain.Value);
      totalitem = Convert.ToDecimal(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(Pnl1HdIdWO.Value));

      //total awal
      if (PgBiayaDListDiskon.SelectedValue == "-")
      {
        totalawal = totalitem;
        PgBiayaHdDiskonVal.Value = "0";
      }
      else if (PgBiayaDListDiskon.SelectedValue == "%")
      {
        totaldiskon = (diskon * totalitem) / 100;
        totalawal = totalitem - totaldiskon;
        PgBiayaHdDiskonVal.Value = Convert.ToDouble(totaldiskon).ToString();
      }
      else
      {
        totalawal = totalitem - diskon;
        PgBiayaHdDiskonVal.Value = Convert.ToDouble(diskon).ToString();
      }

      //ppn
      if (PgBiayaDListPPN.SelectedValue == "0")
      {
        totalppn = 0;
      }
      else
      {
        totalppn = (10 * totalawal) / 100;
      }
      PgBiayaHdPPNVal.Value = Convert.ToDouble(totalppn).ToString();

      //pph
      totalpph = (pph * totalawal) / 100;
      PgBiayaHdPPhVal.Value = Convert.ToDouble(totalpph).ToString();

      //totalakhir
      totalakhir = totalawal + totalppn - totalpph + biayalain;
      PgBiayaRAdTxtTotal.Value = Convert.ToDouble(totalakhir);
    }

    protected void PgBiayaTxtLain_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Pnl1RbItemJasa_CheckedChanged(object sender, EventArgs e)
    {
      Pnl1RadNmrcTxtTotal.Text = string.Empty;
      decimal harga = Pnl1RadNmbrcTxtHarga.Value == null ? 0 : Convert.ToDecimal(Pnl1RadNmbrcTxtHarga.Value);
      decimal diskon = Pnl1RadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(Pnl1RadTxtDiskon.Value);
      decimal total, besarDiskon;

      besarDiskon = (diskon * harga) / 100;
      total = harga - besarDiskon;

      Pnl1RadNmrcTxtTotal.Value = Convert.ToDouble(total);
    }

    protected void Pnl1RbSatuan_CheckedChanged(object sender, EventArgs e)
    {
      Pnl1RadNmrcTxtTotal.Text = string.Empty;
      int jumlah = Pnl1TxtJmlJasa.Text == string.Empty ? 0 : Convert.ToInt32(Pnl1TxtJmlJasa.Text);
      decimal harga = Pnl1RadNmbrcTxtHarga.Value == null ? 0 : Convert.ToDecimal(Pnl1RadNmbrcTxtHarga.Value);
      decimal diskon = Pnl1RadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(Pnl1RadTxtDiskon.Value);
      decimal total, besarDiskon, totalAwal;

      totalAwal = jumlah * harga;
      besarDiskon = (diskon * totalAwal) / 100;
      total = totalAwal - besarDiskon;

      Pnl1RadNmrcTxtTotal.Value = Convert.ToDouble(total);
    }

    protected void GridSupplier_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var supplierTableAdapter = new master_supplierTableAdapter();

      switch (e.CommandName)
      {
        case "RowClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            supplierTableAdapter.FillById(purDataSet.master_supplier, Convert.ToInt32(gridDataItem["id"].Text));

            try
            {
              Pnl1HdIdSupplier.Value = purDataSet.master_supplier[0].id.ToString();
              Pnl1TxtSupplier.Text = purDataSet.master_supplier[0].nama;

              if (purDataSet.master_supplier[0].IsjenisusahaNull())
              {
                PgBiayaRadTxtPPh.Value = 0;
              }
              else
              {
                if (purDataSet.master_supplier[0].jenisusaha == "Pribadi")
                {
                  //tdk punya npwp
                  if (Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(gridDataItem["id"].Text))) == "-" ||
                      Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(gridDataItem["id"].Text))) == "" ||
                      Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(gridDataItem["id"].Text))) == null)
                  {
                    PgBiayaRadTxtPPh.Value = 3;
                  }
                  else
                  {
                    PgBiayaRadTxtPPh.Value = 2.5;
                  }
                }
                else if (purDataSet.master_supplier[0].jenisusaha == "Badan")
                {
                  if (Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(gridDataItem["id"].Text))) == "-" ||
                      Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(gridDataItem["id"].Text))) == "" ||
                      Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(gridDataItem["id"].Text))) == null)
                  {
                    PgBiayaRadTxtPPh.Value = 2;
                  }
                  else
                  {
                    PgBiayaRadTxtPPh.Value = 4;
                  }
                }
              }
            }
            catch (Exception ex)
            {
              NotificationFailure(ex);
            }
            PnlBrowseSupplierModalPopupExtender.Hide();
          }
          break;
      }
    }

    protected void Pnl1BtnBrowseSupplier_Click(object sender, EventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void PnlSupplierBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + PnlSupplierTxtCariNama.Text + "%");

      ViewState.Remove("SelectMethod");

      SupplierDataSource.SelectMethod = "GetDataLikeNama";
      SupplierDataSource.SelectParameters.Clear();
      SupplierDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataLikeNama");

      PnlBrowseSupplierModalPopupExtender.Show();
      GridSupplier.DataBind();
      GridSupplier.CurrentPageIndex = 0;
    }

    protected void GridJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseJasaModalPopupExtender.Show();
    }

    protected void GridJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseJasaModalPopupExtender.Show();
    }

    protected void GridWorkRequest_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseWorkRequestModalPopupExtender.Show();
    }

    protected void GridWorkRequest_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseWorkRequestModalPopupExtender.Show();
    }

    protected void GridSupplier_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridJasa_ItemDataBound(object sender, GridItemEventArgs e)
    {
    }

    protected void PnlWorkRequestBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + PnlWorkRequestTxtSearch.Text + "%");

      ViewState.Remove("SelectMethod");

      WRDataSource.SelectMethod = "GetDataByTypeSupplierIdUnitKerjaId";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("type", "NP");
      WRDataSource.SelectParameters.Add("supplier_id", Pnl1HdIdSupplier.Value);
      WRDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      WRDataSource.SelectParameters.Add("id", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByTypeSupplierIdUnitKerjaId");

      PnlBrowseWorkRequestModalPopupExtender.Show();
      GridWorkRequest.DataBind();
    }

  }
}
