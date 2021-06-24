using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using ManagementSystem.Helper;
using Telerik.Web.UI;

namespace ManagementSystem.Purchasing.MaterialRequest.NonProject.Purchase_Order
{
  public partial class Detail : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vpo01TableAdapter = new vpur_po01TableAdapter();
      var supplierTableAdapter = new master_supplierTableAdapter();

      var id = Request.QueryString["pId"];
      vpo01TableAdapter.FillById(purDataSet.vpur_po01, id);

      string supplierID = purDataSet.vpur_po01[0].supplier_id.ToString();
      int supplier = Convert.ToInt32(supplierID);
      string idSupplier = purDataSet.vpur_po01[0].Issupplier_idNull() ? "0" : supplierID;

      supplierTableAdapter.FillById(purDataSet.master_supplier, supplier);

      if (idSupplier != "0")
      {
        if (purDataSet.master_supplier[0].alamat == "")
        {
          LblAlamatSupplier.Text = "";
        }
        else
        {
          LblAlamatSupplier.Text = purDataSet.master_supplier[0].alamat;
        }

        if (purDataSet.master_supplier[0].kota == "")
        {
          LblKota.Text = "0";
        }
        else
        {
          LblKota.Text = purDataSet.master_supplier[0].kota;
        }
      }

      string typediskon = purDataSet.vpur_po01[0].typediskon;
      string currency = purDataSet.vpur_po01[0].IscurrencyNull() ? "" : purDataSet.vpur_po01[0].currency;

      LblNoPO.Text = purDataSet.vpur_po01[0].id;
      LblTglPO.Text = purDataSet.vpur_po01[0].tglpo.ToShortDateString();
      LblPembelian.Text = purDataSet.vpur_po01[0].payment;
      LblCreatedBy.Text = purDataSet.vpur_po01[0].createdby + " (" + purDataSet.vpur_po01[0].datecreated + " )";
      LblSupplier.Text = purDataSet.vpur_po01[0].Issupplier_namaNull() ? "" : purDataSet.vpur_po01[0].supplier_nama;
      LblStatus.Text = PurchaseUtils.GetStatusBarang(purDataSet.vpur_po01[0].status);
      LblTglPelaksanaan.Text = purDataSet.vpur_po01[0].tglpenyelesaian.ToShortDateString();
      if (typediskon == "-")
      {
        LblDiskon.Text = "-";
      }
      else if (typediskon.Contains("%"))
      {
        LblDiskon.Text = Convert.ToDouble(purDataSet.vpur_po01[0].diskon) + " %";
      }
      else
      {
        LblCurrency3.Visible = true;
        LblCurrency3.Text = purDataSet.vpur_po01[0].currency + ". ";
        LblDiskon.Text = String.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].diskon));
      }

      LblPPn.Text = purDataSet.vpur_po01[0].ppn.ToString() + "%";
      LblJasaLain.Text = purDataSet.vpur_po01[0].jasalain;
      LblBiayaJasaLain.Text = String.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].biayajasalain));
      LblCurrency.Text = purDataSet.vpur_po01[0].IscurrencyNull() ? "" : purDataSet.vpur_po01[0].currency;
      LblTotalPO.Text = String.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].totalpo));
      LblCurrency2.Text = purDataSet.vpur_po01[0].IscurrencyNull() ? "" : purDataSet.vpur_po01[0].currency;
      LblPembayaran.Text = purDataSet.vpur_po01[0].payment;
      LblNotes.Text = purDataSet.vpur_po01[0].catatan;
      LblKeterangan.Text = purDataSet.vpur_po01[0].keterangan;

      if (currency == "IDR")
      {
        PnlKurs.Visible = false;
      }
      else if (currency == "")
      {
        PnlKurs.Visible = false;
        LblKurs.Text = purDataSet.vpur_po01[0].IskursNull() ? "" : String.Format("{0,20:N2}", purDataSet.vpur_po01[0].kurs);
      }
      else
      {
        LblKurs.Text = String.Format("{0,20:N2}", purDataSet.vpur_po01[0].keterangan);
      }
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void GridOrderDetail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var podetailTableAdapter = new pur_podetailTableAdapter();

      string nomer = LblNoPO.Text;

      if (e.Item is GridFooterItem)
      {
        double total = Convert.ToDouble(podetailTableAdapter.ScalarGetSumTotalByPoId(nomer));
        GridFooterItem footerItem = e.Item as GridFooterItem;
        footerItem["total"].Text = String.Concat("TOTAL : " + String.Format("{0,20:N2}", total));
      }
    }

  }
}
