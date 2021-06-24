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

namespace ManagementSystem.Purchasing.WorkRequest.Project.Work_Order
{
  public partial class Detail : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet();
      var vwo01TableAdapter = new vpur_wo01TableAdapter();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();

      var id = Request.QueryString["pId"];
      vwo01TableAdapter.FillById(purDataSet.vpur_wo01, id);

      string typediskon = purDataSet.vpur_wo01[0].typediskon;
      string currency = purDataSet.vpur_wo01[0].IscurrencyNull() ? "" : purDataSet.vpur_wo01[0].currency;

      LblNomer.Text = purDataSet.vpur_wo01[0].id;
      LblNoWO.Text = purDataSet.vpur_wo01[0].nomerwo;
      LblTglWO.Text = purDataSet.vpur_wo01[0].tglwo.ToShortDateString();
      LblTglPenyelesaian.Text = purDataSet.vpur_wo01[0].tglpenyelesaian.ToShortDateString();
      LblPembayaran.Text = purDataSet.vpur_wo01[0].payment;
      LblSupplier.Text = purDataSet.vpur_wo01[0].supplier_nama;
      LblDibuatOleh.Text = String.Concat(purDataSet.vpur_wo01[0].createdby + " (" + purDataSet.vpur_wo01[0].datecreated + ")");
      LblStatus.Text = PurchaseUtils.GetStatusJasa(purDataSet.vpur_wo01[0].status);
      PnlDetailWOTotalJasa.Text = String.Format("{0,20:N2}", (Convert.ToDouble(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(id))));

      double totalAwal = Convert.ToDouble(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(id));
      PnlDetailWOTotalJasa.Text = String.Format("{0,20:N2}", totalAwal);
      double diskon = Convert.ToDouble(purDataSet.vpur_wo01[0].diskon);
      double total;
      //diskon
      if (typediskon == "%")
      {
        PnlDetailWOLblDiskon.Text = Convert.ToDouble(diskon).ToString() + " %";
        PnlDetailWOLblTotalDiskon.Text = String.Format("{0,20:N2}", ((diskon * totalAwal) / 100));
        total = totalAwal - ((diskon * totalAwal) / 100);
      }
      else if (typediskon == "Nominal")
      {
        PnlDetailWOLblDiskon.Text = "";
        PnlDetailWOLblTotalDiskon.Text = String.Format("{0,20:N2}", diskon);
        total = totalAwal - diskon;
      }
      else
      {
        PnlDetailWOLblTotalDiskon.Text = "0";
        total = totalAwal;
      }
      //ppn
      double ppn = Convert.ToDouble(purDataSet.vpur_wo01[0].ppn);
      if (ppn == 0)
      {
        PnlDetailWOLblTotalPPn.Text = "0";
      }
      else
      {
        PnlDetailWOLblPPn.Text = ppn.ToString() + "%";
        PnlDetailWOLblTotalPPn.Text = String.Format("{0,20:N2}", ((ppn * total) / 100));
      }
      //pph
      double pph = Convert.ToDouble(purDataSet.vpur_wo01[0].pph);
      if (pph == 0)
      {
        PnlDetailWOLblTotalPPh.Text = "0";
      }
      else
      {
        PnlDetailWOLblPPh.Text = purDataSet.vpur_wo01[0].pph.ToString() + " %";
        PnlDetailWOLblTotalPPh.Text = String.Format("{0,20:N2}", ((pph * total) / 100));
      }
      //jasa lain
      if (purDataSet.vpur_wo01[0].jasalain == string.Empty || purDataSet.vpur_wo01[0].jasalain == null)
      {
        PnlDetailWOLblBiayaJasaLain.Text = "";
      }
      else
      {
        PnlDetailWOLblBiayaJasaLain.Text = String.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_wo01[0].hargajasalain));
        PnlDetailWOLblJasaLain.Text = purDataSet.vpur_wo01[0].jasalain;
      }
      //kurs
      if (currency == "IDR")
      {
        PnlKurs.Visible = false;
        PnlDetailWOLblCurr1.Text = "Rp";
        PnlDetailWoLblCurr2.Text = "Rp";
        PnlDetailWOLblCurr3.Text = "Rp";
        PnlDetailWOLblCurr4.Text = "Rp";
        PnlDetailWOLblCurr5.Text = "Rp";
        PnlDetailWOLblCurr7.Text = "Rp";
      }
      else
      {
        PnlDetailWOLblKurs.Text = String.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_wo01[0].kurs));
        PnlDetailWOLblCurr1.Text = "$";
        PnlDetailWoLblCurr2.Text = "$";
        PnlDetailWOLblCurr3.Text = "$";
        PnlDetailWOLblCurr4.Text = "$";
        PnlDetailWOLblCurr5.Text = "$";
        PnlDetailWOLblCurr7.Text = "$";
      }
      PnlDetailWOLblTotalWO.Text = String.Format("{0,20:N2}", purDataSet.vpur_wo01[0].totalbiaya);

      LblNotes.Text = purDataSet.vpur_wo01[0].notes;
      LblKeterangan.Text = purDataSet.vpur_wo01[0].keterangan;

    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");
    }

    protected void GridOrderDetail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      var purDataSet = new PurDataSet();
      var wodetailTableAdapter = new pur_wodetailTableAdapter();

      string nomer = LblNomer.Text;

      if (e.Item is GridFooterItem)
      {
        double total = Convert.ToDouble(wodetailTableAdapter.ScalarGetSumTotalBiayaByWoId(nomer));
        GridFooterItem footerItem = e.Item as GridFooterItem;
        footerItem["totalharga"].Text = String.Concat("TOTAL : " + String.Format("{0,20:N2}", total));
      }
    }
  }
}
