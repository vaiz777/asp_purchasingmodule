using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using Telerik.Web.UI;
using ManagementSystem.Helper;
using System.Web.Security;

namespace ManagementSystem.Purchasing.WorkRequest.NonProject.Price_Request
{
  public partial class Kelola : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        //info reqnotewr
        var purDataSet = new PurDataSet();
        var vwr01TableAdapter = new vpur_wr01TableAdapter();

        ViewState["Id"] = Request.QueryString["pId"];
        String id = ViewState["Id"].ToString();

        vwr01TableAdapter.FillById(purDataSet.vpur_wr01, id);

        LblNoWR.Text = purDataSet.vpur_wr01[0].id;
        LblTglWR.Text = purDataSet.vpur_wr01[0].tglwr.ToShortDateString();
        LblLokasi.Text = purDataSet.vpur_wr01[0].lokasi_nama;
        LblReference.Text = purDataSet.vpur_wr01[0].reference;
        LblStatus.Text = PurchaseUtils.GetStatusJasa(purDataSet.vpur_wr01[0].status);
        LblDibuatOleh.Text = String.Concat(purDataSet.vpur_wr01[0].createdby + " (" + purDataSet.vpur_wr01[0].datecreated + ")");
        LblUnitKerja.Text = purDataSet.vpur_wr01[0].unitkerja;
        string unitkerja = LblUnitKerja.Text;
        //department
        string departmentkode = purDataSet.vpur_wr01[0].departement;
        LblDepartment.Text = departmentkode;

        if (unitkerja == "SHIPYARD")
        {
          LblJenisKategori.Text = purDataSet.vpur_wr01[0].usable_nama;
          LblScope.Text = purDataSet.vpur_wr01[0].scope_nama;
        }
        else
        {
          PnlUsable.Visible = false;
          PnlScope.Visible = false;
        }

        //label title
        if (unitkerja == "SHIPYARD") { LblTitle.Text = "Work Request (Non Project)"; }
        else { LblTitle.Text = "Work Request"; }
      }

    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void BtnAddJasa_Click(object sender, EventArgs e)
    {
      string nowr = LblNoWR.Text;
      var url = string.Concat("Item.aspx?pId=", nowr);
      Response.Redirect(url);
    }


    private void NotificationFailure(Exception ex)
    {
      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya.. ", ex.Message);
      PnlMessageModalPopupExtender.Show();
    }

    protected void GridItemJasa_ItemCommand(object source, GridCommandEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vwrdetail01TableAdapter = new vpur_wrdetail01TableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();

      switch (e.CommandName)
      {
        case "UpdateClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;
            var idReqJasa = gridDataItem["id"].Text;
            ViewState["Id"] = idReqJasa;

            var url = string.Concat("Item.aspx?pId=", ViewState["Id"].ToString());
            Response.Redirect(url);

          }
          break;
        case "DeleteClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;

            string idJasa = gridDataItem["id"].Text;
            string noWR = LblNoWR.Text;
            string status = LblStatus.Text;

            FuncToDeleteItem(status, idJasa, noWR);

          }
          break;
        case "RowClick":
          {
            var gridDataItem = (GridDataItem)e.Item;
            if (gridDataItem == null) return;
            var idReqJasa = gridDataItem["id"].Text;

            vwrdetail01TableAdapter.FillById(purDataSet.vpur_wrdetail01, Convert.ToInt64(idReqJasa));
            LblTglDibutuhkan.Text = purDataSet.vpur_wrdetail01[0].tanggal.ToShortDateString();
            LblJasa.Text = purDataSet.vpur_wrdetail01[0].jasa_nama;
            LblJmlJasa.Text = purDataSet.vpur_wrdetail01[0].jmljasa.ToString();
            LblSatuan.Text = purDataSet.vpur_wrdetail01[0].satuan;
            LblJmlOrang.Text = purDataSet.vpur_wrdetail01[0].jmlorang.ToString();
            LblJmlHari.Text = purDataSet.vpur_wrdetail01[0].jmlhari.ToString();
            LblKeterangan.Text = purDataSet.vpur_wrdetail01[0].keterangan;
            LblCurrency.Text = purDataSet.vpur_wrdetail01[0].currency;
            LblSupplier.Text = purDataSet.vpur_wrdetail01[0].Issupplier_namaNull() ? "" : purDataSet.vpur_wrdetail01[0].supplier_nama;
            LblHarga.Text = purDataSet.vpur_wrdetail01[0].IshargaNull() ? "" : String.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_wrdetail01[0].harga));

            string unitkerja = LblUnitKerja.Text;
            if (unitkerja == "SHIPYARD")
            {
              PnlKelola.Visible = true;
            }
            else {
              PnlKelola.Visible = false;
            }

            PnlViewModalPopupExtender.Show();
          }
          break;

      }
    }

    private void FuncToDeleteItem(string status, string idReqJasa, string noWR)
    {
      var purDataSet = new PurDataSet();
      var wrdetailTableAdapter = new pur_wrdetailTableAdapter();
      var wrTableAdapter = new pur_wrTableAdapter();

      string idJasa = idReqJasa;

      if (status == "WR1" || status == "WR5" || status == "WR3") // Baru, Didaftarkan, Verifikasi PPIC
      {
        if (wrdetailTableAdapter.DeleteQueryById(Convert.ToInt64(idJasa)) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", idJasa, Session["Username"].ToString(), "Delete WR Detail");
        }
        GridItemJasa.DataBind();
        NotificationDeleteSuccess();
      }
      else if (status == "WR4") // Verifikasi PPIC(Pending)
      {
        if (wrdetailTableAdapter.DeleteQueryById(Convert.ToInt64(idJasa)) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", idJasa, Session["Username"].ToString(), "Delete WR Detail");
        }

        int countJasaVerified = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J3", noWR));
        int countJasa = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(noWR));

        if (countJasa == countJasaVerified)
        {
          wrTableAdapter.FillById(purDataSet.pur_wr, noWR);

          purDataSet.pur_wr[0].status = "WR5";

          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, Session["Username"].ToString(), "Update Status WR 'Diverifikasi PPIC'");
          }
        }
        Response.Redirect(Request.RawUrl);
        GridItemJasa.DataBind();
        NotificationDeleteSuccess();
      }
      else if (status == "WR6") // Disetujui (Pending)
      {
        if (wrdetailTableAdapter.DeleteQueryById(Convert.ToInt64(idJasa)) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", idJasa, Session["Username"].ToString(), "Update Status WR 'Disetujui (Pending)'");
        }

        int countJasaApproved = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J4", noWR));
        int countJasa = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(noWR));

        if (countJasa == countJasaApproved)
        {
          wrTableAdapter.FillById(purDataSet.pur_wr, noWR);

          purDataSet.pur_wr[0].status = "WR7";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, Session["Username"].ToString(), "Update Status WR 'Disetujui'");
          }
        }
        Response.Redirect(Request.RawUrl);
        GridItemJasa.DataBind();
        NotificationDeleteSuccess();
      }
      else if (status == "WR2") // Didaftarkan (Pending)
      {
        if (wrdetailTableAdapter.DeleteQueryById(Convert.ToInt64(idJasa)) > 0)
        {
          CUtils.UpdateLog("pur_wrdetail", idJasa, Session["Username"].ToString(), "Delete WR Detail");
        }

        int countJasaRegistered = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByStatusWrId("J2", noWR));
        int countJasa = Convert.ToInt32(wrdetailTableAdapter.ScalarGetCountByWrId(noWR));

        if (countJasa == countJasaRegistered)
        {
          wrTableAdapter.FillById(purDataSet.pur_wr, noWR);
          purDataSet.pur_wr[0].status = "WR3";
          if (wrTableAdapter.Update(purDataSet.pur_wr) > 0)
          {
            CUtils.UpdateLog("pur_wr", noWR, Session["Username"].ToString(), "Update status WR 'Didaftarkan'");
          }
        }
        Response.Redirect(Request.RawUrl);
        GridItemJasa.DataBind();
        NotificationDeleteSuccess();
      }
    }

    private void NotificationDeleteSuccess()
    {
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-trash-48.png";
      PnlMessageLblMessage.Text = "Data berhasil dihapus.";
      PnlMessageModalPopupExtender.Show();
    }


    protected void GridItemJasa_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        GridDataItem item = e.Item as GridDataItem;

        string[] rolesArray = Roles.GetRolesForUser();
        string status = item["status"].Text;

        Button btnDelete = item.FindControl("BtnDelete") as Button;
        Button btnUpdate = item.FindControl("BtnUpdate") as Button;

        if (status == "Baru" || status == "Didaftarkan" || status == "J1" || status == "J2")
        {
          btnDelete.Visible = true;
          btnUpdate.Visible = true;
        }
        else if (status == "Diverifikasi" || status == "J3")
        {
          foreach (string roles in rolesArray)
          {
            if (roles == "Manager Purchasing" || roles == "GM Purchasing")
            {
              btnDelete.Visible = true;
              btnUpdate.Visible = true;
            }
          }
        }

        item["status"].Text = PurchaseUtils.GetStatusJasa(status);
      }
    }

    protected void GridItemJasa_DataBound(object sender, EventArgs e)
    {
    }
  }
}
