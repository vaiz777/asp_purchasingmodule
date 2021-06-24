using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;

namespace ManagementSystem.Purchasing
{
  public partial class CekHarga : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        string[] rolesArray = Roles.GetRolesForUser();
        foreach (string roles in rolesArray)
        {
          if (roles == "Staff Purchasing")
          {
            PnlCekHarga.Visible = true;
            PnlWarning.Visible = false;
          }
        }
      }
      else if (IsPostBack)
      {
        if (ViewState.Count > 0)
        {
          MasterBarangDataSource.SelectMethod = ViewState["SelectMethod"].ToString();
        }
      }
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vpo01TableAdapter = new vpur_po01TableAdapter();
      var txtKataKunci = String.Concat("%" + TxtKataKunci.Text + "%");
      var unitkerja = Session["UnitKerja"].ToString();

      if (DListKategori.SelectedValue == "0")
      {
        CekHargaDataSource.SelectMethod = "GetDataCekHargaByUnitKerjaBarangKode";
        CekHargaDataSource.SelectParameters.Clear();
        CekHargaDataSource.SelectParameters.Add("unitkerja", unitkerja);
        CekHargaDataSource.SelectParameters.Add("barangKode", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataCekHargaByUnitKerjaBarangKode");
      }
      else if (DListKategori.SelectedValue == "1")
      {
        CekHargaDataSource.SelectMethod = "GetDataCekHargaByUnitKerjaBarangNama";
        CekHargaDataSource.SelectParameters.Clear();
        CekHargaDataSource.SelectParameters.Add("unitkerja", unitkerja);
        CekHargaDataSource.SelectParameters.Add("barangNama", txtKataKunci);

        ViewState.Add("SelectMethod", "GetDataCekHargaByUnitKerjaBarangNama");
      }
      GridCekHarga.Visible = true;
      GridCekHarga.DataBind();
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      TxtKataKunci.Text = string.Empty;
      DListKategori.SelectedIndex = 0;
      GridCekHarga.Visible = false;

      ViewState.Remove("SelectMethod");
      ViewState.Add("SelectMethod", "GetDataCekHargaByStatusUnitKerja");
      CekHargaDataSource.SelectMethod = "GetDataCekHargaByStatusUnitKerja";
      CekHargaDataSource.SelectParameters.Clear();
      CekHargaDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      GridCekHarga.CurrentPageIndex = 0;
      GridCekHarga.DataBind();
    }

    protected void BtnBrowseBarang_Click(object sender, EventArgs e)
    {
      PnlBrowseMasterBarangModalPopupExtender.Show();
    }

    protected void GridMasterBarang_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = e.Item as GridDataItem;

        if (DListKategori.SelectedValue == "0")
        {
          TxtKataKunci.Text = gridDataItem["kode"].Text;
        }
        else if (DListKategori.SelectedValue == "1")
        {
          TxtKataKunci.Text = gridDataItem["nama"].Text;
        }
        PnlBrowseMasterBarangModalPopupExtender.Hide();
      }
      else
      {
        PnlBrowseMasterBarangModalPopupExtender.Show();
      }
    }

    protected void GridMasterBarang_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
      PnlBrowseMasterBarangModalPopupExtender.Show();
    }

    protected void GridMasterBarang_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
      PnlBrowseMasterBarangModalPopupExtender.Show();
    }

    protected void PnlBarangBtnSearch_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vmasterbarang01TableAdapter = new vmaster_barang01TableAdapter();

      var txtKataKunci = string.Concat("%" + PnlBarangTxtNmBarang.Text + "%");

      MasterBarangDataSource.SelectMethod = "GetDataByNamaUnitKerja";
      MasterBarangDataSource.SelectParameters.Clear();
      MasterBarangDataSource.SelectParameters.Add("unitkerja", Session["UnitKerja"].ToString());
      MasterBarangDataSource.SelectParameters.Add("nama", txtKataKunci);

      ViewState.Add("SelectMethod", "GetDataByNamaUnitKerja");

      PnlBrowseMasterBarangModalPopupExtender.Show();
    }
  }
}
