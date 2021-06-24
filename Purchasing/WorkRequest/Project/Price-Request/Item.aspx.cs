using System;
using TmsBackDataController.MasterDataSetTableAdapters;
using TmsBackDataController.SysDataSetTableAdapters;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using ManagementSystem.Helper;
using System.Diagnostics;
using Telerik.Web.UI;

namespace ManagementSystem.Purchasing.WorkRequest.Project.Price_Request
{
  public partial class Item : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      ViewState["Id"] = Request.QueryString["pId"];
      string id = ViewState["Id"].ToString();

      if (!IsPostBack)
      {
        if (id.Substring(0, 1) == "M" || id.Substring(0, 1) == "S")
        {
          TxtNoWR.Text = id;
        }
        else
        {
          var purDataSet = new PurDataSet();
          var vwrdetail01TableAdapter = new vpur_wrdetail01TableAdapter();

          vwrdetail01TableAdapter.FillById(purDataSet.vpur_wrdetail01, Convert.ToInt64(id));
          TxtNoWR.Text = purDataSet.vpur_wrdetail01[0].wr_id;
          TxtJasa.Text = purDataSet.vpur_wrdetail01[0].jasa_nama;
          HdKodeJasa.Value = purDataSet.vpur_wrdetail01[0].jasa_kode;
          TxtJmlJasa.Text = purDataSet.vpur_wrdetail01[0].jmljasa.ToString();
          TxtJmlHari.Text = purDataSet.vpur_wrdetail01[0].jmlhari.ToString();
          TxtJmlOrang.Text = purDataSet.vpur_wrdetail01[0].jmlorang.ToString();
          TxtKeterangan.Text = purDataSet.vpur_wrdetail01[0].keterangan;
          TxtTanggal.Text = purDataSet.vpur_wrdetail01[0].tanggal.ToShortDateString();
          if (purDataSet.vpur_wrdetail01[0].supplier_id == 0)
          {
            HdIdSupplier.Value = purDataSet.vpur_wrdetail01[0].supplier_id.ToString();
            TxtSupplier.Text = "-";
          }
          else
          {
            HdIdSupplier.Value = purDataSet.vpur_wrdetail01[0].supplier_id.ToString();
            TxtSupplier.Text = purDataSet.vpur_wrdetail01[0].supplier_nama;
            RadTxtHarga.Text = Convert.ToDouble(purDataSet.vpur_wrdetail01[0].harga).ToString();
            DListSatuan.SelectedValue = purDataSet.vpur_wrdetail01[0].satuan;
          }

          BtnBrowseReqJasa.Visible = false;
          TxtJmlJasa.ReadOnly = true;
          TxtJmlHari.ReadOnly = true;
          TxtJmlOrang.ReadOnly = true;
          TxtSatuan.ReadOnly = true;
        }
      }
      else if (IsPostBack)
      {
        if (ViewState.Count > 1)
        {
          SupplierDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          MasterJasaDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
      ViewState["Id"] = Request.QueryString["pId"];
      string id = ViewState["Id"].ToString();

      if (TxtSupplier.Text == string.Empty || RadTxtHarga.Value == null || TxtJasa.Text == string.Empty)
      {
        NotificationWarning();
      }
      else
      {
        if (id.Substring(0, 1) == "M" || (id.Substring(0, 1) == "S"))
        {
          InsertData();
        }
        else
        {
          UpdateData();
        }
      }
    }

    private void InsertData()
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrdetailRow = purDataSet.pur_wrdetail.Newpur_wrdetailRow();

      try
      {
        wrdetailRow.wr_id = TxtNoWR.Text;
        wrdetailRow.jasa_kode = HdKodeJasa.Value;
        wrdetailRow.jmljasa = TxtJmlJasa.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlJasa.Text);
        wrdetailRow.satuan = TxtSatuan.Text == string.Empty ? "" : TxtSatuan.Text;
        wrdetailRow.jmlhari = TxtJmlHari.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlHari.Text);
        wrdetailRow.jmlorang = TxtJmlOrang.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlOrang.Text);
        wrdetailRow.tanggal = TxtTanggal.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(TxtTanggal.Text);
        wrdetailRow.keterangan = TxtKeterangan.Text;
        wrdetailRow.supplier_id = Convert.ToInt64(HdIdSupplier.Value);
        wrdetailRow.harga = Convert.ToDecimal(RadTxtHarga.Text);
        wrdetailRow.currency = DListSatuan.SelectedValue;
        wrdetailRow.status = "J2";
        wrdetailRow.type = "P";
        wrdetailRow.createdby = Session["FUllName"].ToString();

        purDataSet.pur_wrdetail.Addpur_wrdetailRow(wrdetailRow);
        if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail",  (Convert.ToInt32(wrdetailTableAdapter.ScalarGetMaxId())+1).ToString(), Session["Username"].ToString(), "Insert WR Detail");
        }

        functToUStatus(TxtNoWR.Text);
        NotificationSuccess();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }

    }

    private void UpdateData()
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();
      string idReqJasa = ViewState["Id"].ToString();

      wrdetailTableAdapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(idReqJasa));

      try
      {
        purDataSet.pur_wrdetail[0].jasa_kode = HdKodeJasa.Value;
        purDataSet.pur_wrdetail[0].jmljasa = TxtJmlJasa.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlJasa.Text);
        purDataSet.pur_wrdetail[0].satuan = TxtSatuan.Text;
        purDataSet.pur_wrdetail[0].jmlorang = TxtJmlOrang.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlOrang.Text);
        purDataSet.pur_wrdetail[0].jmlhari = TxtJmlHari.Text == string.Empty ? 0 : Convert.ToInt32(TxtJmlHari.Text);
        purDataSet.pur_wrdetail[0].tanggal = TxtTanggal.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(TxtTanggal.Text);
        purDataSet.pur_wrdetail[0].keterangan = TxtKeterangan.Text;

        purDataSet.pur_wrdetail[0].supplier_id = Convert.ToInt64(HdIdSupplier.Value);
        purDataSet.pur_wrdetail[0].harga = RadTxtHarga.Text == string.Empty ? 0 : Convert.ToDecimal(RadTxtHarga.Text);
        purDataSet.pur_wrdetail[0].currency = DListSatuan.SelectedValue;
        purDataSet.pur_wrdetail[0].status = UpdateStatusJasa(idReqJasa);

        if (wrdetailTableAdapter.Update(purDataSet.pur_wrdetail) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", ViewState["Id"].ToString(), Session["Username"].ToString(), "Update WR Detail (Input Harga)");
        }

        functToUStatus(TxtNoWR.Text);
        NotificationSuccess();
      }
      catch (Exception ex)
      {
        NotificationFailure(ex);
      }
    }

    private string UpdateStatusJasa(string idReqJasa)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();

      wrdetailTableAdapter.FillById(purDataSet.pur_wrdetail, Convert.ToInt64(idReqJasa));
      string pStatus = purDataSet.pur_wrdetail[0].status;
      string status;

      if (pStatus == "J1")
      {
        status = "J2";
      }
      else
      {
        status = pStatus;
      }

      return status;
    }


    private void functToUStatus(string nowr)
    {
      var purDataSet = new PurDataSet();
      var wrTableAdapter = new pur_wrTableAdapter();

      wrTableAdapter.FillById(purDataSet.pur_wr, nowr);
      string aStatus = purDataSet.pur_wr[0].status;

      if (aStatus == "WR1" || aStatus == "WR3" || aStatus == "WR2")
      {
        UpdateStatusWRBaru(nowr);
      }
      else
      {
        UpdateStatusWRLain(nowr, aStatus);
      }
    }

    private void UpdateStatusWRLain(string nowr, string status)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();

      //update status requestnotewr
      int jmlItemWR = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(nowr));
      int jmlStatusVerified = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J3", nowr));
      int jmlStatusApproved = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J4", nowr));

      wrTableAdapter.FillById(purDataSet.pur_wr, nowr);

      if (status == "WR4" || status == "WR5") // Verifikasi
      {
        if (jmlItemWR == jmlStatusVerified)
        {
          purDataSet.pur_wr[0].status = "WR5";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Diverifikasi'");
          }
        }
        else {
          purDataSet.pur_wr[0].status = "WR4";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Diverifikasi (Pending)'");
          }
        }
      }
      else if (status == "WR6") // Disetujui (Pending)
      {
        if (jmlItemWR == jmlStatusApproved)
        {
          purDataSet.pur_wr[0].status = "WR7";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Disetujui'");
          }
        }
        else
        {
          purDataSet.pur_wr[0].status = "WR6";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Disetujui (Pending)'");
          }
        }
      }
    }

    private void UpdateStatusWRBaru(string nowr)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();

      //update status pur_requestnote
      int countWR = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(nowr));
      int countPRegWR = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J2", nowr));

      wrTableAdapter.FillById(purDataSet.pur_wr, nowr);
       
      if (countWR == countPRegWR)
      {
        purDataSet.pur_wr[0].status = "WR3";
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Didaftarkan'");
        }
      }
      else
      {
        purDataSet.pur_wr[0].status = "WR2";
        if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
        {
          CUtils.UpdateLog("pur_wr", nowr, Session["Username"].ToString(), "Update WR Status 'Didaftarkan (Pending)'");
        }
      }
    }

    private void CrearForm()
    {
      TxtJasa.Text = string.Empty;
      TxtJmlJasa.Text = string.Empty;
      TxtSatuan.Text = string.Empty;
      TxtSupplier.Text = string.Empty;
      HdKodeJasa.Value = null;
      HdIdSupplier.Value = null;
      RadTxtHarga.Text = string.Empty;
      DListSatuan.SelectedIndex = 0;
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

    private void NotificationSuccess()
    {
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    private void NotificationWarning()
    {
      PnlMessageLblTitlebar.Text = "Warning";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-error-48.png";
      PnlMessageLblMessage.Text = "Data Jasa, Supplier, dan Harga tidak boleh kosong !";
      PnlMessageModalPopupExtender.Show();
    }

    protected void BtnBrowseReqJasa_Click(object sender, EventArgs e)
    {
      PnlBrowseReqJasaModalPopupExtender.Show();
    }

    protected void GridReqJasa_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var masterDataSet = new MasterDataSet();
      var masterjasaTableAdapter = new master_jasaTableAdapter();

      switch (e.CommandName)
      {
        case "RowClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            masterjasaTableAdapter.FillByKode(masterDataSet.master_jasa, gridDataItem["kode"].Text);

            try
            {
              HdKodeJasa.Value = masterDataSet.master_jasa[0].kode;
              TxtJasa.Text = masterDataSet.master_jasa[0].nama;
            }
            catch (Exception ex)
            {
              NotificationFailure(ex);
            }
            PnlBrowseReqJasaModalPopupExtender.Hide();
          }
          break;
      }
    }

    protected void GridSupplier_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var masterDataSet = new MasterDataSet();
      var supplierTableAdapter = new TmsBackDataController.MasterDataSetTableAdapters.master_supplierTableAdapter();

      switch (e.CommandName)
      {
        case "RowClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            supplierTableAdapter.FillById(masterDataSet.master_supplier, Convert.ToInt32(gridDataItem["id"].Text));

            try
            {
              HdIdSupplier.Value = Convert.ToString(masterDataSet.master_supplier[0].id);
              TxtSupplier.Text = masterDataSet.master_supplier[0].nama;
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

    protected void BtnBrowseSupplier_Click(object sender, EventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      string nowr = TxtNoWR.Text;
      var url = string.Concat("Kelola.aspx?pId=", nowr);
      Response.Redirect(url);
    }

    protected void PnlSupplierBtnSearch_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + PnlSupplierTxtSerchBy.Text + "%");

      ViewState.Remove("SelectMethod");

      SupplierDataSource.SelectMethod = "GetDataLikeNama";
      SupplierDataSource.SelectParameters.Clear();
      SupplierDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataLikeNama");

      PnlBrowseSupplierModalPopupExtender.Show();
      GridSupplier.DataBind();
      GridSupplier.CurrentPageIndex = 0;
    }


    protected void GridCekHarga_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        RadTxtHarga.Value = Convert.ToDouble(gridDataItem["harga"].Text);

        PnlBrowseCekHargaModalPopupExtender.Hide();
      }
    }

    protected void BtnCekHarga_Click(object sender, EventArgs e)
    {
      PnlBrowseCekHargaModalPopupExtender.Show();
    }

    protected void GridReqJasa_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseReqJasaModalPopupExtender.Show();
    }

    protected void GridReqJasa_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseReqJasaModalPopupExtender.Show();
    }

    protected void GridSupplier_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridSupplier_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseSupplierModalPopupExtender.Show();
    }

    protected void GridCekHarga_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseCekHargaModalPopupExtender.Show();
    }

    protected void GridCekHarga_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseCekHargaModalPopupExtender.Show();
    }

    protected void BtnSearchJasa_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%", PnlBrowseReqJasaTxtJasa.Text, "%");

      ViewState.Remove("SelectMethod");

      MasterJasaDataSource.SelectMethod = "GetDataByUnitKerjaNama";
      MasterJasaDataSource.SelectParameters.Clear();
      MasterJasaDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      MasterJasaDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByUnitKerjaNama");

      PnlBrowseReqJasaModalPopupExtender.Show();
      GridReqJasa.DataBind();
      GridReqJasa.CurrentPageIndex = 0;
    }

  }
}
