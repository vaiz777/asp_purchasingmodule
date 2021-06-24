using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;
using TmsBackDataController.SysDataSetTableAdapters;
using ManagementSystem.Helper;
using System.Diagnostics;
using System.Web.Security;

namespace ManagementSystem.Purchasing.Master
{
  public partial class MasterProject : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Commercial")
        {
          PnlData1.Visible = false;
          PnlData2.Visible = true;
        }
      }

      if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          MasterProjectDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
          SalesCustDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }

    }

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
      PnlViewAddNewModalPopupExtender.Show();
    }

    protected void PnlUpdateBtnUpdate_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var masterprojectTableAdapter = new master_projectTableAdapter();

      masterprojectTableAdapter.FillById(purDataSet.master_project, Convert.ToInt32(LblHdId.Text));

      try {
        purDataSet.master_project[0].tanggal = Convert.ToDateTime(PnlUpdateDataTxtTanggal.Text);
        purDataSet.master_project[0].nomorproject = PnlUpdateDataTxtNomorProject.Text;
        purDataSet.master_project[0].salescustomer_id = PnlUpdateLblId.Text;
        purDataSet.master_project[0].jeniskapal = PnlUpdateDataTxtJenisKapal.Text;
        purDataSet.master_project[0].jmlkapal = Convert.ToInt32(PnlUpdateDataTxtJmlKapal.Text);
        purDataSet.master_project[0].projectstart = PnlUpdateDataTxtProjectStart.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlUpdateDataTxtProjectStart.Text);
        purDataSet.master_project[0].projectend = PnlUpdateDataTxtProjectEnd.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlUpdateDataTxtProjectEnd.Text);
        purDataSet.master_project[0].warrantystart = PnlUpdateDataTxtWarrantyStart.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlUpdateDataTxtWarrantyStart.Text);
        purDataSet.master_project[0].warrantyend = PnlUpdateDataTxtWarrantyEnd.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlUpdateDataTxtWarrantyEnd.Text);
        purDataSet.master_project[0].unitkerja = Session["UnitKerja"].ToString();
        purDataSet.master_project[0].keterangan = PnlUpdateDataTxtKeterangan.Text;

        masterprojectTableAdapter.Update(purDataSet.master_project);

        GridMasterProject.DataBind();

        //Confirmation
        PnlMessageLblTitlebar.Text = "Confirm";
        PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
        PnlMessageLblMessage.Text = "Data berhasil diperbarui.";
        PnlMessageModalPopupExtender.Show();

        PnlUpdateBtnUpdate.Text = "Updated";
        
      }
      catch (Exception ex)
      {
        PnlMessageLblTitlebar.Text = "Oops!";
        PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
        PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>Purchasing.MasterProject.PnlUpdateBtnUpdate_Click(): ", ex.Message);
        PnlMessageModalPopupExtender.Show();

        _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), "Purchasing.MasterProject.PnlUpdateBtnUpdate_Click()", ex.Message);
      }
      
    }

    protected void PnlAddNewBtnAdd_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var masterprojectTableAdapter = new master_projectTableAdapter();
      var masterprojectRow = purDataSet.master_project.Newmaster_projectRow();
      var id = CUtils.GenerateId("master_project", Convert.ToInt16(DateTime.Now.Year), "PRJ");

      try {
        masterprojectRow.tanggal = PnlAddNewTxtTanggal.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewTxtTanggal.Text);
        masterprojectRow.unitkerja = Session["UnitKerja"].ToString();
        masterprojectRow.nomorproject = PnlAddNewTxtNomorProject.Text;
        masterprojectRow.salescustomer_id = PnlAddNewLblIdCustomer.Text;
        masterprojectRow.jeniskapal = PnlAddNewTxtJenisKapal.Text;
        masterprojectRow.jmlkapal = PnlAddNewTxtJumlah.Text == string.Empty ? 0 : Convert.ToInt32(PnlAddNewTxtJumlah.Text);
        masterprojectRow.projectstart = PnlAddNewProjectStart.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewProjectStart.Text);
        masterprojectRow.projectend = PnlAddNewProjectEnd.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewProjectEnd.Text);
        masterprojectRow.warrantystart = PnlAddNewWarrantyStart.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewWarrantyStart.Text);
        masterprojectRow.warrantyend = PnlAddNewWarrantyEnd.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewWarrantyEnd.Text);
        masterprojectRow.keterangan = PnlAddNewTxtKeterangan.Text;

        purDataSet.master_project.Addmaster_projectRow(masterprojectRow);
        if (masterprojectTableAdapter.Update(purDataSet.master_project) > 0)
        {
          int tahun = DateTime.Now.Year;
          CUtils.UpdateSeed("master_project", tahun, DateTime.Now);
          CUtils.UpdateLog("master_project", id, Session["Username"].ToString(), "insert data");
        }

        
        PnlMessageLblTitlebar.Text = "Confirm";
        PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
        PnlMessageLblMessage.Text = "Data berhasil disimpan.";
        PnlMessageModalPopupExtender.Show();

        ClearForm();
        GridMasterProject.DataBind();
      }
      catch (Exception ex)
      {
        PnlMessageLblTitlebar.Text = "Oops!";
        PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
        PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>Purchasing.MasterProject.PnlAddNewBtnAdd_Click(): ", ex.Message);
        PnlMessageModalPopupExtender.Show();

        _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), "Purchasing.MasterProject.PnlAddNewBtnAdd_Click()", ex.Message);
      }

    }

    protected void ClearForm() {
      PnlAddNewTxtNomorProject.Text = string.Empty;
      PnlAddNewTxtCustomer.Text = string.Empty;
      PnlAddNewProjectStart.Text = string.Empty;
      PnlAddNewProjectEnd.Text = string.Empty;
      PnlAddNewWarrantyStart.Text = string.Empty;
      PnlAddNewWarrantyEnd.Text = string.Empty;
    }

    protected void PnlAddNewBtnBrowseCust_Click(object sender, EventArgs e)
    {
      PnlBrowseCustModalPopupExtender.Show();
    }

    protected void PnlBrowseCustBtnClose_Click(object sender, EventArgs e)
    {
      PnlBrowseCustModalPopupExtender.Hide();
      PnlViewAddNewModalPopupExtender.Show();
    }

    protected void GridCust_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var salesCustomerTableAdapter = new sales_customerTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        salesCustomerTableAdapter.FillById(purDataSet.sales_customer, Convert.ToInt64(id));

        PnlAddNewTxtCustomer.Text = purDataSet.sales_customer[0].nama;
        PnlAddNewLblIdCustomer.Text = purDataSet.sales_customer[0].id.ToString();
        PnlBrowseCustModalPopupExtender.Hide();
        PnlViewAddNewModalPopupExtender.Show();

        GridCust.DataBind();
      }
      else
      {
        PnlBrowseCustModalPopupExtender.Show();
      }
    }

    protected void GridMasterProject_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vmasterproject01TableAdapter = new vmaster_project01TableAdapter();

      switch (e.CommandName)
      {
        case "RowClick": 
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            vmasterproject01TableAdapter.FillById(purDataSet.vmaster_project01, Convert.ToInt32(gridDataItem["id"].Text));

            LblTanggal.Text = purDataSet.vmaster_project01[0].tanggal.ToShortDateString();
            LblNoProject.Text = purDataSet.vmaster_project01[0].nomorproject;
            LblCustomer.Text = purDataSet.vmaster_project01[0].salescustomer_nama;
            LblJenisKapal.Text = purDataSet.vmaster_project01[0].jeniskapal;
            LblJmlKapal.Text = purDataSet.vmaster_project01[0].jmlkapal.ToString();
            LblTglProjectStart.Text = purDataSet.vmaster_project01[0].projectstart.ToShortDateString();
            LblTglProjectEnd.Text = purDataSet.vmaster_project01[0].projectend.ToShortDateString();
            LblTglJaminanStart.Text = purDataSet.vmaster_project01[0].warrantystart.ToShortDateString();
            LblTglJaminanEnd.Text = purDataSet.vmaster_project01[0].warrantyend.ToShortDateString();
            LblKeterangan.Text = purDataSet.vmaster_project01[0].keterangan;

            //hitung lama project
            DateTime date1 = Convert.ToDateTime(LblTglProjectStart.Text);
            DateTime date2 = Convert.ToDateTime(LblTglProjectEnd.Text);

            TimeSpan date = date2 - date1;
            double bil = date.TotalDays;
            PnlDetailLblLamaProject.Text = bil.ToString();

            PnlDetailModalPopupExtender.Show();
          } 
          break;
        case "UpdateCommand":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            vmasterproject01TableAdapter.FillById(purDataSet.vmaster_project01, Convert.ToInt32(gridDataItem["id"].Text));

            try
            {
              PnlUpdateDataTxtTanggal.Text = purDataSet.vmaster_project01[0].tanggal.ToShortDateString();
              PnlUpdateDataTxtNomorProject.Text = purDataSet.vmaster_project01[0].nomorproject;
              PnlUpdateDataTxtCustomer.Text = purDataSet.vmaster_project01[0].salescustomer_nama;
              PnlUpdateLblId.Text = purDataSet.vmaster_project01[0].salescustomer_id.ToString();
              PnlUpdateDataTxtJenisKapal.Text = purDataSet.vmaster_project01[0].jeniskapal;
              PnlUpdateDataTxtJmlKapal.Text = purDataSet.vmaster_project01[0].jmlkapal.ToString();
              PnlUpdateDataTxtProjectStart.Text = purDataSet.vmaster_project01[0].projectstart.ToString() == null ? string.Empty : purDataSet.vmaster_project01[0].projectstart.ToShortDateString();
              PnlUpdateDataTxtProjectEnd.Text = purDataSet.vmaster_project01[0].projectend.ToString() == null ? string.Empty : purDataSet.vmaster_project01[0].projectend.ToShortDateString();
              PnlUpdateDataTxtWarrantyStart.Text = purDataSet.vmaster_project01[0].warrantystart.ToString() == null ? string.Empty : purDataSet.vmaster_project01[0].warrantystart.ToShortDateString();
              PnlUpdateDataTxtWarrantyEnd.Text = purDataSet.vmaster_project01[0].warrantyend.ToString() == null ? string.Empty : purDataSet.vmaster_project01[0].warrantyend.ToShortDateString();

              //hitung lama project
              DateTime date1 = Convert.ToDateTime(PnlUpdateDataTxtProjectStart.Text);
              DateTime date2 = Convert.ToDateTime(PnlUpdateDataTxtProjectEnd.Text);

              TimeSpan date = date2 - date1;
              double bil = date.TotalDays;
              PnlUpdateLblLamaProject.Text = bil.ToString();
            }
            catch (Exception ex)
            {
              var stackTrace = new StackTrace(ex, true);
              var frame = stackTrace.GetFrame(0);

              PnlMessageLblTitlebar.Text = "Oops!";
              PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
              PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya..<br>", frame.GetMethod(), "(", frame.GetFileLineNumber(), "):<br>", ex.Message);
              PnlMessageModalPopupExtender.Show();

              _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), string.Concat(frame.GetFileName(), " ", frame.GetMethod(), "(", frame.GetFileLineNumber(), ")"), ex.Message);
            }
            PnlViewUpdateModalPopupExtender.Show();
          }
          break;
      }
    }

    protected void GridCust2_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var salescustomerTableAdapter = new sales_customerTableAdapter();

      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;
        var id = gridDataItem["id"].Text;

        salescustomerTableAdapter.FillById(purDataSet.sales_customer, Convert.ToInt64(id));

        PnlUpdateDataTxtCustomer.Text = purDataSet.sales_customer[0].nama;
        PnlUpdateLblId.Text = purDataSet.sales_customer[0].id.ToString();

        PnlBrowseCust2ModalPopupExtender.Hide();
        PnlViewUpdateModalPopupExtender.Show();
      }
      else
      {
        PnlBrowseCust2ModalPopupExtender.Show();
      }
    }

    protected void PnlBrowseCust2BtnClose_Click(object sender, EventArgs e)
    {
      PnlBrowseCust2ModalPopupExtender.Hide();
      PnlViewUpdateModalPopupExtender.Show();
    }

    protected void PnlUpdateBtnBrowse_Click(object sender, EventArgs e)
    {
      PnlBrowseCust2ModalPopupExtender.Show();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
      var txtkatakunci = string.Concat("%" + TxtKataKunci.Text + "%");
      var unitkerja = Session["UnitKerja"].ToString();

      ViewState.Remove("SelectMethod");

      if (DlistSearchBy.SelectedValue == "0")
      {
        MasterProjectDataSource.SelectMethod = "GetDataByUnitKerjaNoProject";
        MasterProjectDataSource.SelectParameters.Clear();
        MasterProjectDataSource.SelectParameters.Add("unitkerja", unitkerja);
        MasterProjectDataSource.SelectParameters.Add("nomorproject", txtkatakunci);

        ViewState.Add("SelectMethod", "GetDataByUnitKerjaNoProject");
      }
      else {
        MasterProjectDataSource.SelectMethod = "GetDataByUnitKerjaSalesCustNama";
        MasterProjectDataSource.SelectParameters.Clear();
        MasterProjectDataSource.SelectParameters.Add("unitkerja", unitkerja);
        MasterProjectDataSource.SelectParameters.Add("salescustomernama", txtkatakunci);

        ViewState.Add("SelectMethod", "GetDataByUnitKerjaSalesCustNama");
      }

      GridMasterProject.DataBind();
      GridMasterProject.CurrentPageIndex = 0;

    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      DlistSearchBy.SelectedIndex = 0;

      ViewState.Remove("SelectMethod");
      ViewState.Add("SelectMethod", "GetDataByUnitKerja");

      MasterProjectDataSource.SelectMethod = "GetDataByUnitKerja";
      MasterProjectDataSource.SelectParameters.Clear();
      MasterProjectDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());

      GridMasterProject.DataBind();
      GridMasterProject.CurrentPageIndex = 0;
    }

    protected void BtnSearchCust1_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + TxtSearchCust1.Text + "%");

      ViewState.Remove("SelectMethod");
            
      SalesCustDataSource.SelectMethod = "GetDataByNama";
      SalesCustDataSource.SelectParameters.Clear();
      SalesCustDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByNama");

      PnlBrowseCustModalPopupExtender.Show();
      GridCust.CurrentPageIndex = 0;
      GridCust.DataBind();
    }

    protected void BtnSearchCust2_Click(object sender, EventArgs e)
    {
      var txtKataKunci = String.Concat("%" + TxtSearchCust2.Text + "%");

      ViewState.Remove("SelectMethod");

      SalesCustDataSource.SelectMethod = "GetDataByNama";
      SalesCustDataSource.SelectParameters.Clear();
      SalesCustDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByNama");

      PnlBrowseCust2ModalPopupExtender.Show();
      GridCust2.CurrentPageIndex = 0;
      GridCust2.DataBind();
    }

    protected void PnlUpdateDataTxtProjectStart_TextChanged(object sender, EventArgs e)
    {
      DateTime date1 = Convert.ToDateTime(PnlUpdateDataTxtProjectStart.Text);
      DateTime date2 = Convert.ToDateTime(PnlUpdateDataTxtProjectEnd.Text);

      TimeSpan time = date2 - date1;

      double bil = Convert.ToDouble(time.TotalDays);

      PnlUpdateLblLamaProject.Text = bil.ToString();

      PnlViewUpdateModalPopupExtender.Show();
    }

    protected void PnlUpdateDataTxtProjectEnd_TextChanged(object sender, EventArgs e)
    {
      DateTime date1 = Convert.ToDateTime(PnlUpdateDataTxtProjectStart.Text);
      DateTime date2 = Convert.ToDateTime(PnlUpdateDataTxtProjectEnd.Text);

      TimeSpan time = date2 - date1;

      double bil = Convert.ToDouble(time.TotalDays);

      PnlUpdateLblLamaProject.Text = bil.ToString();

      PnlViewUpdateModalPopupExtender.Show();
    }

    protected void PnlAddNewProjectStart_TextChanged(object sender, EventArgs e)
    {
      DateTime date1 = PnlAddNewProjectStart.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewProjectStart.Text);
      DateTime date2 = PnlAddNewProjectEnd.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewProjectEnd.Text);

      TimeSpan date = date2 - date1;

      double bil = Convert.ToDouble(date.TotalDays);

      PnlAddnewLblLamaProject.Text = bil.ToString();

      PnlViewAddNewModalPopupExtender.Show();
    }

    protected void PnlAddNewProjectEnd_TextChanged(object sender, EventArgs e)
    {
      DateTime date1 = PnlAddNewProjectStart.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewProjectStart.Text);
      DateTime date2 = PnlAddNewProjectEnd.Text == string.Empty ? DateTime.MinValue : Convert.ToDateTime(PnlAddNewProjectEnd.Text);

      TimeSpan date = date2 - date1;

      double bil = Convert.ToDouble(date.TotalDays);

      PnlAddnewLblLamaProject.Text = bil.ToString();

      PnlViewAddNewModalPopupExtender.Show();
    }
  }
}
