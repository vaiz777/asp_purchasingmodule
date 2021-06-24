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
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchasing.Material_Request.Project.Barang_Request
{
  public partial class List : System.Web.UI.Page
  {
    private readonly sys_errorlogTableAdapter _errorLogTableAdapter = new sys_errorlogTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();

      foreach (string roles in rolesArray) {
        if (roles == "Direktur Purchasing") {
          BtnAdd.Visible = false;
        }
      }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
      var url = string.Concat("Input.aspx");
      Response.Redirect(url);
    }

    protected void BtnSearchClick(object sender, EventArgs e)
    {
      var txtKataKunci = string.Concat("%", TxtKataKunci.Text, "%");

      string status = DlistStatus.SelectedValue;
      string kategori = DlistKategoriPencarian.SelectedValue;

      ViewState.Remove("SelectMethod");

      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray) {
        if (roles == "Direktur Purchasing") {
          SearchGeneral(kategori, txtKataKunci);
        }
        else if (roles == "Purchasing S.M" || roles == "Purchasing M.C" || roles == "Purchasing D.E.M") {
          SearchByUnitKerja(kategori, txtKataKunci);
        }
      }

      GridMaterialRequest.CurrentPageIndex = 0;
      GridMaterialRequest.DataBind();
    }

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

    
    protected void BtnCancelClick(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      DlistKategoriPencarian.SelectedIndex = 0;
      TxtTanggalStart.Text = string.Empty;
      TxtTanggalEnd.Text = string.Empty;
      DlistStatus.SelectedIndex = 0;

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
    }

    protected void GridMaterialRequest_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.CommandName == "RowClick")
      {
        var purDataSet = new PurDataSet();
        var vmr01TableAdapter = new  vpur_mr01TableAdapter();

        var gridDataItem = (GridDataItem)e.Item;
        if (gridDataItem == null) return;

        vmr01TableAdapter.FillById(purDataSet.vpur_mr01, gridDataItem["id"].Text);

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
    }


    protected void GridMaterialRequest_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;
        string status = item["status"].Text;
        
        ////Variabel Color
        if (status == "MR1")
        {
          item["status"].ForeColor = Color.White;
          item["status"].BackColor = Color.Blue;
          item["status"].Text = "MR BARU";
        }
        item["status"].Text = PurchaseUtils.GetStatusBarang(status);

      }
    }

    protected void GridDataBarang_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string kdStatus = item["status"].Text;

        string status = PurchaseUtils.GetStatusBarang(kdStatus);

        item["status"].Text = status;       
      }
    }

    protected void ListMRDataSource_Init(object sender, EventArgs e)
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

    protected void DlistKategoriPencarian_SelectedIndexChanged(object sender, EventArgs e)
    {
      string kategori = DlistKategoriPencarian.SelectedValue;
      if (kategori == "0" || kategori == "1") {
        PnlKataKunci.Visible = true;
        PnlStatus.Visible = false;
        PnlTanggal.Visible = false;
      }
      else if (kategori == "2") {
        PnlKataKunci.Visible = false;
        PnlStatus.Visible = false;
        PnlTanggal.Visible = true;
      }
      else if (kategori == "3") {
        PnlKataKunci.Visible = false;
        PnlStatus.Visible = true;
        PnlTanggal.Visible = false;
      }
    }
  }
}
