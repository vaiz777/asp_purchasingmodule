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
using System.Web.Security;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using ManagementSystem.ReportsDataSetTableAdapters;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchasing.Material_Request.Project.Price_Request
{
  public partial class List : System.Web.UI.Page
  {
		private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();
    private readonly ReportParameter[] _myReportParameters = new ReportParameter[6];
    protected IEnumerable<ReportParameter> MyParamEnum;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {   
        string[] rolesArray = Roles.GetRolesForUser();

        foreach (string roles in rolesArray)
        {
          if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing" || roles == "Direktur Purchasing" || roles == "Staff Purchasing" || roles == "PPIC Purchasing")
          {
            PnlData2.Visible = true;
            PnlData1.Visible = false;
          }            
        }
      }
      //else if (IsPostBack) {
      //  if (ViewState.Count > 0) {
      //    BarangDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
      //  }
      //}
    }

    protected void GridMR_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vmr01TableAdapter = new vpur_mr01TableAdapter();

      

      if (e.CommandName == "RowClick")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string id = gridDataItem["id"].Text;
        vmr01TableAdapter.FillById(purDataSet.vpur_mr01, id);

        LblNoMR.Text = purDataSet.vpur_mr01[0].id;
        LblTglMR.Text = purDataSet.vpur_mr01[0].tanggal.ToShortDateString();
        LblReference.Text = purDataSet.vpur_mr01[0].reference;
        LblNomorProject.Text = purDataSet.vpur_mr01[0].project_nomor;
        LblLokasi.Text = purDataSet.vpur_mr01[0].lokasi_nama;
        LblKategori.Text = purDataSet.vpur_mr01[0].kategori_nama;
        LblScope.Text = purDataSet.vpur_mr01[0].scope_nama;
        LblUsable.Text = purDataSet.vpur_mr01[0].usable_nama;
        LblCreatedby.Text = String.Concat(purDataSet.vpur_mr01[0].createdby + " (" + purDataSet.vpur_mr01[0].datecreated + " ) ");

        PnlViewBarangModalPopupExtender.Show();
      }
      else if (e.CommandName == "Check") 
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string id = gridDataItem["id"].Text;
        TxtNoteMR.Text = id;

        PnlViewHargaModalPopupExtender.Show();
      }
      else if (e.CommandName == "Edit")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string id = gridDataItem["id"].Text;
        ViewState["Id"] = id;

        var url = string.Concat("Input.aspx?pId=", ViewState["Id"].ToString());
        Response.Redirect(url);
      }
      else if (e.CommandName == "Input")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string id = gridDataItem["id"].Text;
        ViewState["Id"] = id;

        var url = string.Concat("Input.aspx?pId=", ViewState["Id"].ToString());
        Response.Redirect(url);
      }

      else if (e.CommandName == "Print")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string id = gridDataItem["id"].Text;
        LblNote.Text = id;

        PnlKonfirmModalPopupExtender.Show();      
      }
      else if (e.CommandName == "Batal")
      {
        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        string id = gridDataItem["id"].Text;
        PnlBatalTxtNoPO.Text = id;

        PnlBatalModalPopupExtender.Show();
      }        
    }
   
    private void printMR(String note)
    {
      var reportDataSet = new ReportsDataSet();
      var purDataSet = new PurDataSet();
      var vmrdetail01TableAdapter = new ManagementSystem.ReportsDataSetTableAdapters.vpur_mrdetail01TableAdapter();
      var vmr01TableAdapter = new vpur_mr01TableAdapter();

      var reportViewer = new ReportViewer();
      reportViewer.ProcessingMode = ProcessingMode.Local;
      reportViewer.LocalReport.ReportPath = Server.MapPath("~/reports/RptMaterialRequest.rdlc");
      reportViewer.ZoomMode = ZoomMode.Percent;
      reportViewer.ZoomPercent = 100;
      reportViewer.ExportContentDisposition = ContentDisposition.AlwaysInline; // Look here..

      vmr01TableAdapter.FillById(purDataSet.vpur_mr01, note);
      string pReference = purDataSet.vpur_mr01[0].reference;
      string pTglMR = purDataSet.vpur_mr01[0].tanggal.ToShortDateString();
      string pRequestor = purDataSet.vpur_mr01[0].createdby;
      string pNoMR = note;
      string pOperator = Session["FullName"].ToString();
      string pNoProject = purDataSet.vpur_mr01[0].project_nomor;

      _myReportParameters[0] = new ReportParameter("pNoMR", pNoMR);
      _myReportParameters[1] = new ReportParameter("pReference", pReference);
      _myReportParameters[2] = new ReportParameter("pTglMR", pTglMR);
      _myReportParameters[3] = new ReportParameter("pOperator", pOperator);
      _myReportParameters[4] = new ReportParameter("pRequestor", pRequestor);
      _myReportParameters[5] = new ReportParameter("pNoProject", pNoProject);


      vmrdetail01TableAdapter.FillByMrId(reportDataSet.vpur_mrdetail01, note);
      MyParamEnum = _myReportParameters;
      var reportDataSource = new ReportDataSource();
      reportDataSource.Name = "vpur_mrdetail01";
      reportDataSource.Value = reportDataSet.vpur_mrdetail01;
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
      Response.AddHeader("content-disposition", string.Concat("inline; filename=MR", getUnitKerja + "_", note + "_", DateTime.Now.Date.ToShortDateString(), ".pdf")); // .. and here!
      Response.BinaryWrite(bytes); // create the file    
      Response.Flush();

    }

    protected void BtnSearchClick(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");

      string status = DlistStatus.SelectedValue;
      string kategori = DlistKategoriPencarian.SelectedValue;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          SearchGeneral(kategori, txtKataKunci);
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M")
        {
          SearchByUnitKerja(kategori, txtKataKunci);
        }
      }

      GridMR.CurrentPageIndex = 0;
      GridMR.DataBind();
    }

    #region Function Search

    private void SearchGeneral(string kategori, string txtKataKunci)
    {
      if (kategori == "0")
      {
        MaterialRequestDataSource.SelectMethod = "GetDataByTypeId";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("type", "P");
        MaterialRequestDataSource.SelectParameters.Add("id", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeId");
      }
      else if (kategori == "1")
      {
        MaterialRequestDataSource.SelectMethod = "GetDataByTypeNomorProject";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("type", "P");
        MaterialRequestDataSource.SelectParameters.Add("noproject", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataByTypeNomorProject");
      }
      else if (kategori == "2")
      {
        MaterialRequestDataSource.SelectMethod = "GetDataByTypeStatus";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("type", "P");
        MaterialRequestDataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);

        ViewState.Add("SelectMethod", "GetDataByTypeStatus");
      }
      else if (kategori == "3")
      {
        DateTime datereq1 = Convert.ToDateTime(TxtTanggalStart.Text);
        DateTime datereq2 = Convert.ToDateTime(TxtTanggalEnd.Text);

        MaterialRequestDataSource.SelectMethod = "GetDataByTypeTanggal";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("type", "P");
        MaterialRequestDataSource.SelectParameters.Add("tanggal", datereq1.ToString("yyyy-MM-dd"));
        MaterialRequestDataSource.SelectParameters.Add("tanggal", datereq2.ToString("yyyy-MM-dd"));

        ViewState.Add("SelectMethod", "GetDataByTypeTanggal");
      }
    }

    private void SearchByUnitKerja(string kategori, string txtKataKunci)
    {
      if (kategori == "0")
      {
        MaterialRequestDataSource.SelectMethod = "GetDataByTypeUnitKerjaId";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MaterialRequestDataSource.SelectParameters.Add("id", txtKataKunci);
        MaterialRequestDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaId");
      }
      else if (kategori == "1")
      {
        MaterialRequestDataSource.SelectMethod = "GetDataByTypeUnitKerjaProjectNomor";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MaterialRequestDataSource.SelectParameters.Add("noproject", txtKataKunci);
        MaterialRequestDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaProjectNomor");
      }
      else if (kategori == "2")
      {
        MaterialRequestDataSource.SelectMethod = "GetDataByTypeUnitKerjaStatus";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MaterialRequestDataSource.SelectParameters.Add("status", DlistStatus.SelectedValue);
        MaterialRequestDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaStatus");
      }
      else if (kategori == "3")
      {
        DateTime datereq1 = Convert.ToDateTime(TxtTanggalStart.Text);
        DateTime datereq2 = Convert.ToDateTime(TxtTanggalEnd.Text);

        MaterialRequestDataSource.SelectMethod = "GetDataByTypeUnitKerjaTanggal";
        MaterialRequestDataSource.SelectParameters.Clear();
        MaterialRequestDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
        MaterialRequestDataSource.SelectParameters.Add("tanggal1", datereq1.ToString("yyyy-MM-dd"));
        MaterialRequestDataSource.SelectParameters.Add("tanggal2", datereq2.ToString("yyyy-MM-dd"));
        MaterialRequestDataSource.SelectParameters.Add("type", "P");

        ViewState.Add("SelectMethod", "GetDataByTypeUnitKerjaTanggal");
      }

    }

    #endregion 

    protected void BtnCancelClick(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      DlistKategoriPencarian.SelectedIndex = 0;
      DlistStatus.SelectedIndex = 0;
      TxtTanggalStart.Text = string.Empty;
      TxtTanggalEnd.Text = string.Empty;

      PnlKataKunci.Visible = true;
      PnlTanggal.Visible = false;
      PnlStatus.Visible = false;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {

        if (roles == "Direktur Purchasing")
        {
          MaterialRequestDataSource.SelectMethod = "GetDataByType";
          MaterialRequestDataSource.SelectParameters.Clear();
          MaterialRequestDataSource.SelectParameters.Add("type", "P");

          ViewState.Add("SelectMethod", "GetDataProject");
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M")
        {
          MaterialRequestDataSource.SelectMethod = "GetDataByUnitKerjaType";
          MaterialRequestDataSource.SelectParameters.Clear();
          MaterialRequestDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
          MaterialRequestDataSource.SelectParameters.Add("type", "P");

          ViewState.Add("SelectMethod", "GetDataByUnitKerjaType");
        }
      }

      GridMR.DataBind();
    }
    
    protected void GridHarga_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;
        CheckBox CheckBox1 = gridDataItem.FindControl("CheckBox1") as CheckBox;

        string kdstatus = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(kdstatus);
        string[] rolesArray = Roles.GetRolesForUser();

        foreach (string roles in rolesArray)
        {
          if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Supervisor Purchasing")
          {
            if (kdstatus == "B5" || kdstatus == "B6" || kdstatus == "Disetujui" || kdstatus == "Ditolak" || kdstatus == "B1" || kdstatus == "Baru")
            {
              CheckBox1.Visible = false;
            }
          }
          else if (roles == "PPIC Purchasing")
          {
            if (kdstatus == "B3" || kdstatus == "B4" || kdstatus == "Diverifikasi" || kdstatus == "Verifikasi ditunda" || kdstatus == "B1" || kdstatus == "Baru")
            {
              CheckBox1.Visible = false;
            }
          }
        }
      }    
    }

    protected void GridMR_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        Button btnInput = gridDataItem.FindControl("BtnInput") as Button;
        Button btnCheck = gridDataItem.FindControl("BtnCheck") as Button;
        Button btnEdit = gridDataItem.FindControl("BtnEdit") as Button;
        Button btnPrint = gridDataItem.FindControl("BtnPrint") as Button;
        Button btnBatal = gridDataItem.FindControl("BtnBatal") as Button;

        string status = gridDataItem["status"].Text;
        string[] rolesArray = Roles.GetRolesForUser();

        foreach (string roles in rolesArray)
        {
          if (status == "MR1") // Status MR Baru
          {
            if (roles == "PPIC Purchasing")
            {
              btnInput.Visible = false;
            }
            else if (roles == "Staff Purchasing" || roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur Purchasing")
            {
              btnInput.Visible = true;
            }
          }
          else if (status == "HR1") // Status Set Harga
          {
            if (roles == "PPIC Purchasing")
            {
              btnCheck.Visible = true;

              PnlViewHargaBtnVerified.Visible = true;
              PnlViewHargaBtnDelayedVerify.Visible = true;
            }
            else if (roles == "Staff Purchasing")
            {
              btnEdit.Visible = true;
            }
            else if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur")
            {
              btnCheck.Visible = true;
              btnEdit.Visible = true;

              PnlViewHargaBtnApprove.Visible = true;
              PnlViewHargaBtnReject.Visible = true;
            }
          }
          else if (status == "HR2") // Status Diverifikasi
          {
            if (roles != "PPIC Purchasing")
            {
              if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur")
              {
                btnEdit.Visible = true;
                btnCheck.Visible = true;

                PnlViewHargaBtnApprove.Visible = true;
                PnlViewHargaBtnReject.Visible = true;
              }
            }
          }
          else if (status == "HR3") // Outstanding By PPIC
          {
            if (roles == "PPIC Purchasing")
            {
              btnCheck.Visible = true;

              PnlViewHargaBtnVerified.Visible = true;
              PnlViewHargaBtnDelayedVerify.Visible = true;
            }
            else if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur")
            {
              btnCheck.Visible = true;
              btnEdit.Visible = true;

              PnlViewHargaBtnApprove.Visible = true;
              PnlViewHargaBtnReject.Visible = true;
            }
            else if (roles == "Staff Purchasing")
            {
              btnEdit.Visible = true;
            }
          }
          else if (status == "HR4") // Approved
          {
            btnPrint.Visible = true;
            if (roles == "Manager Purchasing" || roles == "GM Purchasing")
            {
              btnBatal.Visible = true;
            }
          }
          else if (status == "HR5") // Outstanding by Manager
          {  
            if (roles == "Manager Purchasing" || roles == "GM Purchasing" || roles == "Direktur")
            {
              btnCheck.Visible = true;
              btnEdit.Visible = true;

              PnlViewHargaBtnApprove.Visible = true;
              PnlViewHargaBtnReject.Visible = true;
            }
          }
        }
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status);
      }
    }

    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
      foreach (GridDataItem item in GridHarga.MasterTableView.Items)
      {
        CheckBox chkbx = (CheckBox)item["CheckTemp"].FindControl("CheckBox1");
        chkbx.Checked = !chkbx.Checked;
        PnlViewHargaModalPopupExtender.Show();
      }
    }

    //Function Notifications
    protected void NotificationSuccess()
    {
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Berhasil.";
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

    protected void PnlViewHargaBtnApproveClick(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();

      string requestnoteId = TxtNoteMR.Text;

      foreach (GridDataItem item in GridHarga.MasterTableView.Items)
      {
        GridDataItem griDataItem = item as GridDataItem;

        if (item is GridDataItem)
        {
          CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
          if (CheckBox1.Checked && CheckBox1 != null)
          {
            string requestbarangId = griDataItem["id"].Text;

            mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(requestbarangId));
            purDataSet.pur_mrdetail[0].status = "B5";
            if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
            {
              CUtils.UpdateLog("pur_mrdetail", requestbarangId.ToString(), getUsername, "Update status MR Detail 'Disetujui'");
            }
          }
        }
      }
      PurchaseUtils.UpdateStatusRequestNote('a', requestnoteId, getUsername);
      GridHarga.DataBind();
      GridMR.DataBind();
    }

    protected void PnlViewHargaBtnVerified_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();

      string requestnoteId = TxtNoteMR.Text;

      foreach (GridDataItem item in GridHarga.MasterTableView.Items)
      {
        GridDataItem griDataItem = item as GridDataItem;

        if (item is GridDataItem)
        {
          CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
          if (CheckBox1.Checked && CheckBox1 != null)
          {
            string requestbarangId = griDataItem["id"].Text;

            mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(requestbarangId));
            purDataSet.pur_mrdetail[0].status = "B3";
            if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
            {
              CUtils.UpdateLog("pur_mrdetail", requestbarangId.ToString(), getUsername, "Update status MR Detail 'Diverifikasi'");
            }
          }
        }
      }
      PurchaseUtils.UpdateStatusRequestNote('v', requestnoteId, getUsername);
      GridHarga.DataBind();
      GridMR.DataBind();
    }

    protected void PnlViewHargaBtnReject_Click(object sender, EventArgs e) 
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();

      string requestnoteId = TxtNoteMR.Text;

      foreach (GridDataItem item in GridHarga.MasterTableView.Items)
      {
        GridDataItem griDataItem = item as GridDataItem;

        if (item is GridDataItem)
        {
          CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
          if (CheckBox1.Checked && CheckBox1 != null)
          {
            string requestbarangId = griDataItem["id"].Text;

            mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(requestbarangId));
            purDataSet.pur_mrdetail[0].status = "B6";
            if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
            {
              CUtils.UpdateLog("pur_mrdetail", requestbarangId.ToString(), getUsername, "Update status MR Detail 'Ditolak'");
            }
          }
        }
      }
      PurchaseUtils.UpdateStatusRequestNote('r', requestnoteId, getUsername);
      GridHarga.DataBind();
      GridMR.DataBind();
    }

    protected void PnlViewHargaBtnDelayedVerify_Click(object sender, EventArgs e) 
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();

      string requestnoteId = TxtNoteMR.Text;

      foreach (GridDataItem item in GridHarga.MasterTableView.Items)
      {
        GridDataItem griDataItem = item as GridDataItem;

        if (item is GridDataItem)
        {
          CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
          if (CheckBox1.Checked && CheckBox1 != null)
          {
            string requestbarangId = griDataItem["id"].Text;

            mrdetailTableAdapter.FillById(purDataSet.pur_mrdetail, Convert.ToInt32(requestbarangId));
            purDataSet.pur_mrdetail[0].status = "B4";
            if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
            {
              CUtils.UpdateLog("pur_mrdetail", requestbarangId.ToString(), getUsername, "Update status MR Detail 'Verifikasi Ditunda'");
            }
          }
        }
      }
      PurchaseUtils.UpdateStatusRequestNote('v', requestnoteId, getUsername);
      GridHarga.DataBind();
      GridMR.DataBind();
    }    

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

    public string getUsername
    {
      get
      {
        string result = Session["Username"].ToString();
        return result;
      }
    }


    protected void DlistKategoriPencarian_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (DlistKategoriPencarian.SelectedValue == "0")
      {
        PnlKataKunci.Visible = true;
        PnlTanggal.Visible = false;
        PnlStatus.Visible = false;
      }
      else if (DlistKategoriPencarian.SelectedValue == "1")
      {
        PnlKataKunci.Visible = true;
        PnlTanggal.Visible = false;
        PnlStatus.Visible = false;
      }
      else if (DlistKategoriPencarian.SelectedValue == "2")
      {
        PnlKataKunci.Visible = false;
        PnlTanggal.Visible = true;
        PnlStatus.Visible = false;
      }
      else if (DlistKategoriPencarian.SelectedValue == "3")
      {
        PnlKataKunci.Visible = false;
        PnlTanggal.Visible = false;
        PnlStatus.Visible = true;
      }
    }

    protected void PnlKonfirmPrintOk_Click(object sender, EventArgs e) 
    {
      printMR(LblNote.Text);
    }

    protected void PnlBatalBtnOk_Click(object sender, EventArgs e) 
    {
      PnlInputNoteLblNomer.Text = PnlBatalTxtNoPO.Text;

      PnlInputNoteModalPopupExtender.Show();
    }

    protected void GridBarang_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status);
      }    
    }

    protected void NoteDataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          MaterialRequestDataSource.SelectMethod = "GetDataByType";
          MaterialRequestDataSource.SelectParameters.Clear();
          MaterialRequestDataSource.SelectParameters.Add("type", "P");
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing D.E.M" || roles == "Purchasing M.C")
        {
          MaterialRequestDataSource.SelectMethod = "GetDataByUnitKerjaType";
          MaterialRequestDataSource.SelectParameters.Clear();
          MaterialRequestDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
          MaterialRequestDataSource.SelectParameters.Add("type", "P");
        }
      }
    }

    protected void GridBarang_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlViewBarangModalPopupExtender.Show();
    }

    protected void GridBarang_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlViewBarangModalPopupExtender.Show();
    }

    protected void GridHarga_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

    }

    protected void PnlInputNoteBtnSubmit_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var mrTableAdapter = new pur_mrTableAdapter();
      var mrdetailTableAdapter = new pur_mrdetailTableAdapter();
      var rejectTableAdapter = new pur_rejectTableAdapter();
      var rejectRow = purDataSet.pur_reject.Newpur_rejectRow();

      string idReqNote = PnlInputNoteLblNomer.Text;
      
      // Update Requestnote ...
      if (mrTableAdapter.FillById(purDataSet.pur_mr, idReqNote) > 0)
      {
        int x = Convert.ToInt32(mrdetailTableAdapter.ScalarGetAmountByMrId(idReqNote));
        for (int i = 0; i < x; i++)
        {
          mrdetailTableAdapter.FillByMrId(purDataSet.pur_mrdetail, idReqNote);

          string idRequestBarang = purDataSet.pur_mrdetail[i].id.ToString();
          purDataSet.pur_mrdetail[i].id = Convert.ToInt32(idRequestBarang);
          purDataSet.pur_mrdetail[i].status = "B7";

          if (mrdetailTableAdapter.Update(purDataSet.pur_mrdetail) > 0)
          {
            CUtils.UpdateLog("pur_mrdetail", idRequestBarang, getUsername, "Update status MR Detail 'Dibatalkan'");
          }
        }

        purDataSet.pur_mr[0].status = "HR6";
        if (mrTableAdapter.Update(purDataSet.pur_mr) > 0) {
          CUtils.UpdateLog("pur_mr", idReqNote, getUsername, "Update status MR 'Dibatalkan'");
        }
      }

      // Insert RequestReject
      rejectRow.id = idReqNote;
      rejectRow.jenisrequest = "MR";
      rejectRow.datecreated = DateTime.Now;
      rejectRow.ket = PnlInputNoteTxtKeterangan.Text;
      rejectRow.createdby = getFullName;

      purDataSet.pur_reject.Addpur_rejectRow(rejectRow);
      if (rejectTableAdapter.Update(purDataSet.pur_reject) > 0) {
        CUtils.UpdateLog("pur_mrreject", idReqNote, getUsername, "Insert MR Reject");
      }

      NotificationSuccess();
      GridMR.DataBind();
    }
  }
}
