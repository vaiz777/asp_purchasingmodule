using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ManagementSystem.Purchasing.Master.Master_Lokasi
{
  public partial class List : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string[] rolesArray = Roles.GetRolesForUser();
      foreach (string roles in rolesArray)
      {
        if (roles == "Staff Purchasing")
        {
          PnlData1.Visible = false;
          PnlData2.Visible = true;
        }
      }
    }

    protected void BtnTambah_Click(object sender, EventArgs e)
    {
      Response.Redirect("Input.aspx");
    }

  }
}
