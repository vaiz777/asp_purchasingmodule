using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController.SysDataSetTableAdapters;
using ManagementSystem.Helper;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;
using System.Diagnostics;

namespace ManagementSystem.Purchasing.WorkRequest.Project.Work_Order
{
  public partial class Input : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    #region Get Session

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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //NO WO
        HdIdWO.Value = GetIdWO();
        string id = HdIdWO.Value;

        //TextBox Supplier & No Project
        var purDataSet = new PurDataSet();
        var wodetail02TableAdapter = new vpur_wodetail02TableAdapter();
        var supplierTableAdapter = new master_supplierTableAdapter();

        if (wodetail02TableAdapter.FillByWoId(purDataSet.vpur_wodetail02, id) > 0)
        {
          HdIdProject.Value = purDataSet.vpur_wodetail02[0].project_id.ToString();
          Pnl1TxtNoProject.Text = purDataSet.vpur_wodetail02[0].project_nomor;
          Pnl1TxtNoProject.Enabled = false;
          Pnl1BtnBrowseMasterProject.Visible = false;
          Pnl1TxtSupplier.Text = purDataSet.vpur_wodetail02[0].supplier_nama;
          Pnl1TxtSupplier.Enabled = false;
          Pnl1BtnBrowseSupplier.Visible = false;
          HdIdSupplier.Value = purDataSet.vpur_wodetail02[0].supplier_id.ToString();

          //TextBox PPh
          string idSupplier = HdIdSupplier.Value;
          if (idSupplier != "")
          {
            supplierTableAdapter.FillById(purDataSet.master_supplier, Convert.ToInt32(idSupplier));

            if (purDataSet.master_supplier[0].IsjenisusahaNull())
            {
              PgBiayaRadTxtPPh.Text = "";
            }
            else
            {
              if (purDataSet.master_supplier[0].jenisusaha == "Pribadi")
              {
                //tdk punya npwp
                if (Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "-" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == null)
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
                if (Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "-" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == null)
                {
                  PgBiayaRadTxtPPh.Value = 2;
                }
                else
                {
                  PgBiayaRadTxtPPh.Value = 4;
                }
              }
              else
              {
                PgBiayaRadTxtPPh.Value = null;
              }
            }
          }
          else
          {
            PgBiayaRadTxtPPh.Value = null;
          }
        }
      }
      else if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          SupplierDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          MasterProjectDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    private string GetIdWO()
    {
      string id = "";
      DateTime date = DateTime.Now;

      if (getUnitKerja == "ASSEMBLING")
      {
        id = CUtils.GenerateId("pur_wo_assembling", date.Year, "D");
      }
      else if (getUnitKerja == "SPARE PARTS")
      {
        id = CUtils.GenerateId("pur_wo_spareparts", date.Year, "C");
      }
      else if (getUnitKerja == "SHIPYARD")
      {
        id = CUtils.GenerateId("pur_wo_shipyard", date.Year, "S");
      }

      return id;
    }

    protected void GridMasterProject_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var purDataset = new PurDataSet();
      var masterprojectTableAdapter = new master_projectTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        masterprojectTableAdapter.FillById(purDataset.master_project, Convert.ToInt32(id));

        HdIdProject.Value = purDataset.master_project[0].id.ToString();
        Pnl1TxtNoProject.Text = purDataset.master_project[0].nomorproject;

        PnlBrowseMasterProjectModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterProjectModalPopupExtender.Show();
      }
    }

    protected void Pnl1BtnBrowseMasterProject_Click(object sender, EventArgs e)
    {
      PnlBrowseMasterProjectModalPopupExtender.Show();
    }

    protected void GridSupplier_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var supplierTableAdapter = new master_supplierTableAdapter();

      switch (e.CommandName)
      {
        case "RowClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            string idSupplier = gridDataItem["id"].Text;

            supplierTableAdapter.FillById(purDataSet.master_supplier, Convert.ToInt32(idSupplier));

            try
            {
              HdIdSupplier.Value = purDataSet.master_supplier[0].id.ToString();
              Pnl1TxtSupplier.Text = purDataSet.master_supplier[0].nama;

              if (purDataSet.master_supplier[0].IsjenisusahaNull())
              {
                PgBiayaRadTxtPPh.Value = null;
              }
              else
              {
                if (purDataSet.master_supplier[0].jenisusaha == "Pribadi")
                {
                  //tdk punya npwp
                  if (Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "-" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == null)
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
                  if (Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "-" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == "" ||
                    Convert.ToString(supplierTableAdapter.ScalarGetNpwpById(Convert.ToInt32(idSupplier))) == null)
                  {
                    PgBiayaRadTxtPPh.Value = 2;
                  }
                  else
                  {
                    PgBiayaRadTxtPPh.Value = 4;
                  }
                }
                else
                {
                  PgBiayaRadTxtPPh.Value = null;
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

    private void NotificationFailure(Exception ex)
    {
      var stackTrace = new StackTrace(ex, true);
      var frame = stackTrace.GetFrame(0);

      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>", frame.GetMethod(), "(", frame.GetFileLineNumber(), "):<br>", ex.Message);
      PnlMessageModalPopupExtender.Show();

      _errorLogTableAdapter.Insert(DateTime.Now, getUsername, string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
    }

    protected void Pnl1BtnBrowseSupplier_Click(object sender, EventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
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

    protected void Pnl1BtnBrowseWorkRequest_Click(object sender, EventArgs e)
    {
      PnlBrowseWorkRequestModalPopupExtender.Show();
    }

    protected void Pnl1BtnBrowseJasa_Click(object sender, EventArgs e)
    {
      PnlBrowseJasaModalPopupExtender.Show();
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

        HdIdWRDetail.Value = purDataset.vpur_wrdetail01[0].id.ToString();
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

    protected void Pnl1TxtCurrency_TextChanged(object sender, EventArgs e)
    {
      if (Pnl1TxtCurrency.Text != "IDR")
      {
        PnlKurs.Visible = true;
      }
    }

    protected void Pnl1BtnAdd_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      var wodetailRow = purDataSet.pur_wodetail.Newpur_wodetailRow();

      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      wrdetailTableAdapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(HdIdWRDetail.Value));

      try
      {
        //add data in orderdetailwr
        wodetailRow.wo_id = HdIdWO.Value;
        wodetailRow.wrdetail_id = Convert.ToInt64(HdIdWRDetail.Value);
        wodetailRow.diskon = Pnl1RadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(Pnl1RadTxtDiskon.Value);
        wodetailRow.kurs = Pnl1RadNmrcTxtKurs.Value == null ? 0 : Convert.ToDouble(Pnl1RadNmrcTxtKurs.Value);
        wodetailRow.totalharga = Pnl1RadNmrcTxtTotal.Value == null ? 0 : Convert.ToDecimal(Pnl1RadNmrcTxtTotal.Value);        
        if (Pnl1RbSatuan.Checked == true) { wodetailRow.typetotal = true; } // Satuan, Item
        else { wodetailRow.typetotal = false; } // Gelondongan

        purDataSet.pur_wodetail.Addpur_wodetailRow(wodetailRow);
        if (wodetailTableAdapter.Update(purDataSet.pur_wodetail) > 0)
        {
          int tahun = DateTime.Now.Year;
          CUtils.UpdateLog("pur_wodetail", (Convert.ToInt32(wodetailTableAdapter.ScalarGetMaxById())+1).ToString(), getUsername, "Insert WO Detail");
        }

        //update data in wr detail
        purDataSet.pur_wrdetail[0].status = "J5";
        if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", HdIdWRDetail.Value, getUsername, "Update WR Detail Status 'Ditambahlan ke WO'");
        }

        //update data in req note
        string noWR = Pnl1TxtNoWR.Text;
        UpdateStatusWorkReq(noWR);

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
          purDataSet.pur_wr[0].status = "WR9"; // Add to WO
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, getUsername, "Update WR Status 'Add to WO'");
          }
        }
        else
        {
          wrTableAdapter.FillById(purDataSet.pur_wr, noWR);
          purDataSet.pur_wr[0].status = "WR8"; // Add to WO (Pending)
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, getUsername, "Update WR status 'Add to WO (Pending)'");
          }
        }
      }
    }

    protected void ClearPnl1()
    {
      Pnl1TxtNoProject.Enabled = false;
      Pnl1BtnBrowseMasterProject.Visible = false;
      Pnl1TxtSupplier.Enabled = false;
      Pnl1BtnBrowseSupplier.Visible = false;
      Pnl1TxtNoWR.Text = string.Empty;
      HdIdWRDetail.Value = string.Empty;
      Pnl1TxtJasa.Text = string.Empty;
      Pnl1TxtJmlJasa.Text = string.Empty;
      Pnl1TxtSatuan.Text = string.Empty;
      Pnl1TxtCurrency.Text = string.Empty;
      Pnl1RadNmbrcTxtHarga.Text = string.Empty;
      PnlKurs.Visible = false;
      Pnl1RadNmrcTxtKurs.Text = string.Empty;
      Pnl1RadTxtDiskon.Text = string.Empty;
      Pnl1RbPerItemJasa.Checked = false;
      Pnl1RbSatuan.Checked = false;
      Pnl1RadNmrcTxtTotal.Text = string.Empty;
    }

    protected void NotificationSuccess()
    {
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
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

    protected void GridOrderDetail_ItemDataBound(object sender, GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();

      string nomer = HdIdWO.Value;

      if (e.Item is GridFooterItem)
      {
        double total = Convert.ToDouble(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(nomer));
        GridFooterItem footerItem = e.Item as GridFooterItem;
        footerItem["totalharga"].Text = String.Concat("TOTAL : " + String.Format("{0,20:N2}", total));
        PgBiayaRAdTxtTotal.Text = Convert.ToString(total);
      }
    }

    protected void PgBiayaDListPPN_SelectedIndexChanged(object sender, EventArgs e)
    {
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
      string idOrdernoteWr = HdIdWO.Value;

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
              string nowr = item["wr_id"].Text;
              purDataSet.pur_wrdetail[0].status = "J4";
              if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
              {
                CUtils.UpdateLog("pur_wrdetail", idReqJasa, getUsername, "Update WR Detail Status 'Disetujui'");
              }
              UpdateRequestNoteWr(nowr);
            }            

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

    //private void UpdateStatusOrdernoteWr(string idOrdernoteWr) {
    //  var purDataSet = new PurDataSet();
    //  var ordernotewrTableAdapter = new pur_ordernotewrTableAdapter();
    //  var orderdetailwrTableAdapter = new pur_orderdetailwrTableAdapter();

    //  int count = Convert.ToInt32(orderdetailwrTableAdapter.ScalarCountDataByOrdernoteWrId(idOrdernoteWr));
    //  if (count == 0) {
    //    if (ordernotewrTableAdapter.FillById(purDataSet.pur_ordernotewr, idOrdernoteWr) > 0) {
    //      purDataSet.pur_ordernotewr[0].status = "WO Dibuat (Tertunda)";

    //    }

    //  }
    //}

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
        purDataSet.pur_wr[0].status = "WR8"; // Add to WO (Pending)
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", nowr, getUsername, "Update WR Status 'Add to WO(Pending)'");
        }
      }
      else if (countItem == countItemPriceApprove)
      {
        wrTableAdapter.FillById(purDataSet.pur_wr, nowr);
        purDataSet.pur_wr[0].status = "WR7";
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", nowr, getUsername, "Update WR Status 'Disetujui'");
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

    protected void PgBiayaTxtPPh_TextChanged(object sender, EventArgs e)
    {

    }

    protected void PgBiayaTxtDiskon_TextChanged(object sender, EventArgs e)
    {

    }


    protected void PgBiayaBtnHitungNota_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      decimal totalitem, totalawal, totaldiskon, totalppn, totalpph, totalakhir;

      decimal diskon = PgBiayaRadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadTxtDiskon.Value);
      decimal pph = PgBiayaRadTxtPPh.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadTxtPPh.Value);
      decimal biayalain = PgBiayaRadNmrcTxtBiayaLain.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadNmrcTxtBiayaLain.Value);
      totalitem = Convert.ToDecimal(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(HdIdWO.Value));

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

    private void GeUpdateSeedWO(DateTime tanggal)
    {
      if (getUnitKerja == "ASSEMBLING")
      {
        CUtils.UpdateSeed("pur_wo_assembling", tanggal.Year, tanggal);
      }
      else if (getUnitKerja == "SPARE PARTS")
      {
        CUtils.UpdateSeed("pur_wo_spareparts", tanggal.Year, tanggal);
      }
      else if (getUnitKerja == "SHIPYARD")
      {
        CUtils.UpdateSeed("pur_wo_shipyard", tanggal.Year, tanggal);
      }
    }

    private string GetNomerWO(DateTime tanggal)
    {
      string nomerwo = "";
      if (getUnitKerja == "ASSEMBLING")
      {
        nomerwo = CUtils.GenerateId("pur_nowo", tanggal.Year, tanggal.Month, "MAWO-");
      }
      if (getUnitKerja == "SPARE PARTS")
      {
        nomerwo = CUtils.GenerateId("pur_nowo", tanggal.Year, tanggal.Month, "MSWO-");
      }
      else if (getUnitKerja == "SHIPYARD")
      {
        nomerwo = CUtils.GenerateId("pur_nowo", tanggal.Year, tanggal.Month, "SWO-");
      }
      return nomerwo;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var woTableAdapter = new pur_woTableAdapter();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();
      var woRow = purDataSet.pur_wo.Newpur_woRow();
      string nomer = HdIdWO.Value;
      DateTime date = RDateTglWO.SelectedDate.Value;

      try
      {
        woRow.id = nomer;
        woRow.nomerwo = GetNomerWO(date);
        woRow.tglwo = date == null ? RDateTglWO.MinDate : date;
        woRow.ppn = Convert.ToSingle(PgBiayaDListPPN.SelectedValue);
        woRow.ppnval = Convert.ToDecimal(PgBiayaHdPPNVal.Value);
        woRow.pph = PgBiayaRadTxtPPh.Value == null ? 0 : Convert.ToSingle(PgBiayaRadTxtPPh.Value);
        woRow.pphval = Convert.ToDecimal(PgBiayaHdPPhVal.Value);
        woRow.datecreated = DateTime.Now;
        woRow.tglpenyelesaian = PgLainRadTxtTglPenyelesaian.SelectedDate == null ? DateTime.MinValue : Convert.ToDateTime(PgLainRadTxtTglPenyelesaian.SelectedDate);
        woRow.diskon = PgBiayaRadTxtDiskon.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadTxtDiskon.Value);
        woRow.diskonval = Convert.ToDecimal(PgBiayaHdDiskonVal.Value);
        if (PgBiayaDListDiskon.SelectedValue == "-") { woRow.typediskon = "-"; }
        else if (PgBiayaDListDiskon.SelectedValue == "%") { woRow.typediskon = "%"; }
        else { woRow.typediskon = "Nominal"; }
        woRow.jasalain = PgBiayaTxtLain.Text;
        woRow.hargajasalain = PgBiayaRadNmrcTxtBiayaLain.Value == null ? 0 : Convert.ToDecimal(PgBiayaRadNmrcTxtBiayaLain.Value);
        woRow.type = "P";
        woRow.keterangan = PgLainTxtKet.Text;
        woRow.notes = PgLainTxtNotes.Text;
        woRow.payment = PgLainTxtPembayaran.Text;
        if (Convert.ToInt32(wodetailTableAdapter.ScalarGetCountByWoId(nomer)) == 0)
        {
          woRow.status = "WO2"; // Pending
        }
        else
        {
          woRow.status = "WO1"; // Dibuat
        }
        woRow.createdby = getFullName;
        woRow.totalbiaya = PgBiayaRAdTxtTotal.Value == null ? 0 : Convert.ToDecimal(PgBiayaRAdTxtTotal.Value);
        woRow.approvedby = "-";
        woRow.dateapproved = DateTime.MinValue;
        woRow.unitkerja = getUnitKerja;

        purDataSet.pur_wo.Addpur_woRow(woRow);
        if (woTableAdapter.Update(purDataSet.pur_wo) > 0)
        {
          if (date == null)
          {
            GeUpdateSeedWO(DateTime.Now);
            CUtils.UpdateSeed("pur_nowo", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now);
          }
          else
          {
            GeUpdateSeedWO(date);
            CUtils.UpdateSeed("pur_nowo", date.Year, date.Month, date);
          }
          CUtils.UpdateLog("pur_wo", nomer, getUsername, "Insert WO");
        }

        Response.Redirect("List.aspx");
        NotificationSuccess();
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

    protected void PnlMasterProjectBtnSearch_Click(object sender, EventArgs e)
    {
      var txtkatakunci = string.Concat("%" + PnlMasterProjectTxtKataKunci.Text + "%");
      var unitkerja = Session["UnitKerja"].ToString();

      ViewState.Remove("SelectMethod");

      if (PnlMasterProjectDlistSearchBy.SelectedValue == "0") // NO Project
      {
        MasterProjectDataSource.SelectMethod = "GetDataByUnitKerjaNoProject";
        MasterProjectDataSource.SelectParameters.Clear();
        MasterProjectDataSource.SelectParameters.Add("unitkerja", unitkerja);
        MasterProjectDataSource.SelectParameters.Add("nomorproject", txtkatakunci);

        ViewState.Add("SelectMethod", "GetDataByUnitKerjaNoProject");
      }
      else
      {
        MasterProjectDataSource.SelectMethod = "GetDataByUnitKerjaSalesCustNama";
        MasterProjectDataSource.SelectParameters.Clear();
        MasterProjectDataSource.SelectParameters.Add("unitkerja", unitkerja);
        MasterProjectDataSource.SelectParameters.Add("salescustomernama", txtkatakunci);

        ViewState.Add("SelectMethod", "GetDataByUnitKerjaSalesCustNama");
      }

      PnlBrowseMasterProjectModalPopupExtender.Show();
      GridMasterProject.DataBind();
      GridMasterProject.CurrentPageIndex = 0;
    }

    protected void PnlMasterProjectBtnCancel_Click(object sender, EventArgs e)
    {
      PnlMasterProjectTxtKataKunci.Text = string.Empty;
      PnlMasterProjectDlistSearchBy.SelectedIndex = 0;

      ViewState.Remove("SelectMethod");
      ViewState.Add("SelectMethod", "GetDataByUnitKerja");

      MasterProjectDataSource.SelectMethod = "GetDataByUnitKerja";
      MasterProjectDataSource.SelectParameters.Clear();
      MasterProjectDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());

      PnlBrowseMasterProjectModalPopupExtender.Show();
      GridMasterProject.DataBind();
      GridMasterProject.CurrentPageIndex = 0;
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

    protected void PnlWorkRequestBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + PnlWorkRequestTxtSearch.Text + "%");

      ViewState.Remove("SelectMethod");

      WRDataSource.SelectMethod = "GetDataByProjectidSupplieridId";
      WRDataSource.SelectParameters.Clear();
      WRDataSource.SelectParameters.Add("supplier_id", HdIdSupplier.Value);
      WRDataSource.SelectParameters.Add("project_id", HdIdProject.Value);
      WRDataSource.SelectParameters.Add("id", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByProjectidSupplieridId");

      PnlBrowseWorkRequestModalPopupExtender.Show();
      GridWorkRequest.DataBind();
      GridWorkRequest.CurrentPageIndex = 0;
    }

    protected void Pnl1RbPerItemJasa_CheckedChanged(object sender, EventArgs e)
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

    protected void GridSupplier_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridWorkRequest_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseWorkRequestModalPopupExtender.Show();
    }

    protected void GridWorkRequest_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseWorkRequestModalPopupExtender.Show();
    }

    protected void GridJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseJasaModalPopupExtender.Show();
    }

    protected void GridJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseJasaModalPopupExtender.Show();
    }

    protected void GridWorkRequest_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusJasa(status);
      }
    }
  }
}
