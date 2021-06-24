using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Telerik.Web.UI;
using ManagementSystem.Helper;

namespace ManagementSystem.Purchasing
{
  public partial class Dashboard : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string unitkerja = Session["UnitKerja"].ToString();
      string[] rolesArray = Roles.GetRolesForUser();

      if (!IsPostBack) {
        foreach (string roles in rolesArray)
        {
          if (roles == "Direktur Purchasing") 
          {
            PnlMR.Visible = false;
            PnlWR.Visible = false;
            PnlPO.Visible = true;
            PnlWO.Visible = true;

            LblInfoPO.Text = "Data PO yang perlu persetujuan.";
            LblInfoWO.Text = "Data WO yang perlu persetujuan";
          }
          else if (roles == "Manager Purchasing" || roles == "GM Purchasing") 
          {
            PnlPO.Visible = true;
            PnlWO.Visible = true;

            LblInfoPO.Text = "Data PO yang perlu persetujuan.";
            LblInfoWO.Text = "Data WO yang perlu persetujuan";
            LblInfoWR.Text = "Data WR yang perlu persetujuan";
            LblInfoMR.Text = "Data MR yang perlu persetujuan";
          }
          else if (roles == "PPIC Purchasing") 
          {
            PnlMR.Visible = true;
            PnlWR.Visible = true;
            PnlPO.Visible = false;
            PnlWO.Visible = false;

            LblInfoWR.Text = "Data WR yang perlu diverifikasi";
            LblInfoMR.Text = "Data MR yang perlu diverifikasi";
          }
          else if (roles == "Staff Purchasing") 
          {
            PnlPO.Visible = true;
            PnlWO.Visible = true;

            LblInfoPO.Text = "Data PO yang sudah lengkap";
            LblInfoWO.Text = "Data WO yang belum ditutup";
            LblInfoWR.Text = "Data WR berhasil disetujui";
            LblInfoMR.Text = "Data MR berhasil disetujui";
          }
          else if (roles == "Supervisor Purchasing") 
          {
            PnlMR.Visible = true;
            PnlWR.Visible = true;
            PnlPO.Visible = false;
            PnlWO.Visible = false;

            LblInfoWR.Text = "Data WR yang perlu persetujuan";
            LblInfoMR.Text = "Data MR yang perlu persetujuan";
          }
        }
      }
    }

    protected void WRDataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      string unitkerja = Session["UnitKerja"].ToString();

      foreach (string roles in rolesArray)
      {
        if (roles == "GM Purchasing" || roles == "Manager Purchasing") 
        {
          WRDataSource.SelectMethod = "GetDataApprovedByUnitKerja";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("unitkerja", unitkerja);

          if (roles == "Purchasing Manufacturing") 
          {
            WRDataSource.SelectMethod = "GetDataApprovedByUnitManufacture";
            WRDataSource.SelectParameters.Clear();
          }
        }
        else if (roles == "PPIC Purchasing") 
        {
          WRDataSource.SelectMethod = "GetDataByStatusUnitKerja";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("status", "WR3");
          WRDataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
        else if (roles == "Staff Purchasing") 
        {
          WRDataSource.SelectMethod = "GetDataByStatusUnitKerja";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("status", "WR7");
          WRDataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
        else if (roles == "Supervisor Purchasing") 
        {
          WRDataSource.SelectMethod = "GetDataByStatusUnitKerja";
          WRDataSource.SelectParameters.Clear();
          WRDataSource.SelectParameters.Add("status", "WR5");
          WRDataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
      }
    }

    protected void WODataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      string unitkerja = Session["UnitKerja"].ToString();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          WODataSource.SelectMethod = "GetDataByStatus";
          WODataSource.SelectParameters.Clear();
          WODataSource.SelectParameters.Add("status", "WO1");
        }
        else if (roles == "GM Purchasing" || roles == "Manager Purchasing")
        {
          WODataSource.SelectMethod = "GetDataByStatusUnitKerja";
          WODataSource.SelectParameters.Clear();
          WODataSource.SelectParameters.Add("status", "WO1");
          WODataSource.SelectParameters.Add("unitkerja", unitkerja);

          if (roles == "Purchasing Manufacturing")
          {
            WODataSource.SelectMethod = "GetDataByStatusUnitManufacture";
            WODataSource.SelectParameters.Clear();
            WODataSource.SelectParameters.Add("status", "WO1");
          }
        }
        else if (roles == "Staff Purchasing")
        {
          WODataSource.SelectMethod = "GetDataByStatusUnitKerja";
          WODataSource.SelectParameters.Clear();
          WODataSource.SelectParameters.Add("status", "WO4");
          WODataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
      }
    }

    protected void MRDataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      string unitkerja = Session["UnitKerja"].ToString();

      foreach (string roles in rolesArray)
      {
        if (roles == "GM Purchasing" || roles == "Manager Purchasing")
        {
          MRDataSource.SelectMethod = "GetDataApprovedByUnitKerja";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("unitkerja", unitkerja);

          if (roles == "Purchasing Manufacturing")
          {
            MRDataSource.SelectMethod = "GetDataByUnitManufactureByType";
            MRDataSource.SelectParameters.Clear();
          }
        }
        else if (roles == "PPIC Purchasing")
        {
          MRDataSource.SelectMethod = "GetDataByStatusUnitKerja";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("status", "HR1");
          MRDataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
        else if (roles == "Staff Purchasing")
        {
          MRDataSource.SelectMethod = "GetDataByStatusUnitKerja";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("status", "HR4");
          MRDataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
        else if (roles == "Supervisor Purchasing")
        {
          MRDataSource.SelectMethod = "GetDataByStatusUnitKerja";
          MRDataSource.SelectParameters.Clear();
          MRDataSource.SelectParameters.Add("status", "HR2");
          MRDataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
      }
    }

    protected void PODataSource_Init(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      string unitkerja = Session["UnitKerja"].ToString();

      foreach (string roles in rolesArray)
      {
        if (roles == "Direktur Purchasing")
        {
          PODataSource.SelectMethod = "GetDataByStatus";
          PODataSource.SelectParameters.Clear();
          PODataSource.SelectParameters.Add("status", "PO1");
        }
        else if (roles == "GM Purchasing" || roles == "Manager Purchasing")
        {
          PODataSource.SelectMethod = "GetDataByStatusUnitKerja";
          PODataSource.SelectParameters.Clear();
          PODataSource.SelectParameters.Add("status", "PO1");
          PODataSource.SelectParameters.Add("unitkerja", unitkerja);

          if (roles == "Purchasing Manufacturing")
          {
            PODataSource.SelectMethod = "GetDataByStatusUnitManufacture";
            PODataSource.SelectParameters.Clear();
            PODataSource.SelectParameters.Add("status", "PO1");
          }
        }
        else if (roles == "Staff Purchasing")
        {
          PODataSource.SelectMethod = "GetDataByStatusUnitKerja";
          PODataSource.SelectParameters.Clear();
          PODataSource.SelectParameters.Add("status", "PO4");
          PODataSource.SelectParameters.Add("unitkerja", unitkerja);
        }
      }
    }

    protected void GridMR_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status).ToUpper();

        string unitkerja = gridDataItem["unitkerja"].Text;
        if (unitkerja != "SHIPYARD")
        {
          (GridMR.MasterTableView.GetColumn("type") as GridBoundColumn).Display = false;
        }
        else 
        {
          string type = gridDataItem["type"].Text;
          if (type == "P") { gridDataItem["type"].Text = "Project"; }
          else { gridDataItem["type"].Text = "Non Project"; }
        }
      }
    }

    protected void GridPO_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status).ToUpper();

        string unitkerja = gridDataItem["unitkerja"].Text;
        if (unitkerja != "SHIPYARD")
        {
          (GridPO.MasterTableView.GetColumn("type") as GridBoundColumn).Display = false;
        }
        else
        {
          string type = gridDataItem["type"].Text;
          if (type == "P") { gridDataItem["type"].Text = "Project"; }
          else { gridDataItem["type"].Text = "Non Project"; }
        }
      }
    }

    protected void GridWR_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusJasa(status).ToUpper();

        string unitkerja = gridDataItem["unitkerja"].Text;
        if (unitkerja != "SHIPYARD")
        {
          (GridWR.MasterTableView.GetColumn("type") as GridBoundColumn).Display = false;
        }
        else
        {
          string type = gridDataItem["type"].Text;
          if (type == "P") { gridDataItem["type"].Text = "Project"; }
          else { gridDataItem["type"].Text = "Non Project"; }
        }
      }
    }

    protected void GridWO_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusJasa(status).ToUpper();

        string unitkerja = gridDataItem["unitkerja"].Text;
        if (unitkerja != "SHIPYARD")
        {
          (GridWO.MasterTableView.GetColumn("type") as GridBoundColumn).Display = false;
        }
        else
        {
          string type = gridDataItem["type"].Text;
          if (type == "P") { gridDataItem["type"].Text = "Project"; }
          else { gridDataItem["type"].Text = "Non Project"; }
        }
      }
    }

    protected void GridMR_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string type = gridDataItem["type"].Text;
        if (type == "P" || type == "Project") 
        {
          Response.Redirect("~/Purchasing/MaterialRequest/Project/Price-Request/List.aspx");
        }
        else {
          Response.Redirect("~/Purchasing/MaterialRequest/NonProject/Price-Request/List.aspx");
        }
      }
    }

    protected void GridPO_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string type = gridDataItem["type"].Text;
        if (type == "P" || type == "Project")
        {
          Response.Redirect("~/Purchasing/MaterialRequest/Project/Purchase-Order/List.aspx");
        }
        else
        {
          Response.Redirect("~/Purchasing/MaterialRequest/NonProject/Purchase-Order/List.aspx");
        }
      }
    }

    protected void GridWR_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string type = gridDataItem["type"].Text;
        if (type == "P" || type == "Project")
        {
          Response.Redirect("~/Purchasing/WorkRequest/Project/Price-Request/List.aspx");
        }
        else
        {
          Response.Redirect("~/Purchasing/WorkRequest/NonProject/Price-Request/List.aspx");
        }
      }
    }

    protected void GridWO_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string type = gridDataItem["type"].Text;
        if (type == "P" || type == "Project")
        {
          Response.Redirect("~/Purchasing/WorkRequest/Project/Work-Order/List.aspx");
        }
        else
        {
          Response.Redirect("~/Purchasing/WorkRequest/NonProject/Work-Order/List.aspx");
        }
      }
    }

 
  }
}
