using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ManagementSystem.Helper;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;

namespace ManagementSystem.Purchasing
{
  public partial class CheckPO : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DlistUnitKerja_SelectedIndexChanged(object sender, EventArgs e)
    {
      ListBarang.DataBind();
      ListBarang.Visible = true;
    }

    protected void RadDatePicker1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
      ListBarang.DataBind();
    }

    protected void RadDatePicker2_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
      ListBarang.DataBind();
    }

    protected void ListBarang_SelectedIndexChanged(object sender, EventArgs e)
    {
      GridPO.DataBind();
      GridPO.Visible = true;
    }

    protected void GridPO_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        string status = gridDataItem["status"].Text;
        gridDataItem["status"].Text = PurchaseUtils.GetStatusBarang(status).ToUpper();

        string unitkerja = DlistUnitKerja.SelectedValue;
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

    protected void GridPO_ItemCommand(object source, GridCommandEventArgs e)
    {
      if (e.Item is GridDataItem)
      {
        var gridDataItem = (GridDataItem)e.Item;

        var purDataSet = new PurDataSet();
        var vpo01TableAdapter = new vpur_po01TableAdapter();
        var podetailTableAdapter = new pur_podetailTableAdapter();

        string id = gridDataItem["id"].Text;
        vpo01TableAdapter.FillById(purDataSet.vpur_po01, id);

        PnlDetailHdIdPO.Value = purDataSet.vpur_po01[0].id;
        PnlDetailPOLblNomer.Text = purDataSet.vpur_po01[0].nomerpo;
        PnlDetailPOLblTanggal.Text = purDataSet.vpur_po01[0].tglpo.ToShortDateString();        
        PnlDetailPOLblSupplier.Text = purDataSet.vpur_po01[0].supplier_nama;
        PnlDetailPOLblJenis.Text = purDataSet.vpur_po01[0].jenispo;
        PnlDetailPOLblPembayaran.Text = purDataSet.vpur_po01[0].payment;
        PnlDetailPOLblLokasiGudang.Text = purDataSet.vpur_po01[0].lokasigudang_nama;
        PnlDetailPOLblTglPenyerahan.Text = purDataSet.vpur_po01[0].tglpenyelesaian.ToShortDateString();
        PnlDetailPOLblDibuatOleh.Text = purDataSet.vpur_po01[0].createdby + " (" + purDataSet.vpur_po01[0].datecreated + " )";

        string currency = purDataSet.vpur_po01[0].currency;
        PnlDetailPOLblCurr1.Text = currency;
        PnlDetailPOLblCurr2.Text = currency;
        PnlDetailPOLblCurr3.Text = currency;
        PnlDetailPOLblCurr5.Text = currency;
        PnlDetailPOLblCurr7.Text = currency;

        PnlDetailPOLblTotalBarang.Text = String.Format("{0,20:N2}", podetailTableAdapter.ScalarGetSumTotalByPoId(id));

        string typediskon = purDataSet.vpur_po01[0].typediskon;
        if (typediskon == "%") 
        {
          PnlDetailPOLblDiskon.Text = String.Format("{0,20:N2}", purDataSet.vpur_po01[0].diskon) + "%";
          PnlDetailPOLblTotalDiskon.Text = string.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].diskonval));
        }
        else if (typediskon == "-")
        {
          PnlDetailPOLblDiskon.Text = "0";
          PnlDetailPOLblTotalDiskon.Text = "0";
        }
        else {
          PnlDetailPOLblDiskon.Text = "";
          PnlDetailPOLblTotalDiskon.Text = string.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].diskonval));
        }

        string cur = purDataSet.vpur_po01[0].currency;
        if (cur != "IDR")
        {
          PnlDetailPOKurs.Visible = true;
          PnlDetailPOLblKurs.Text = String.Format("{0,20:N2}", purDataSet.vpur_po01[0].kurs);
        }
        else {
          PnlDetailPOKurs.Visible = false;
        }

        double ppn = Convert.ToDouble(purDataSet.vpur_po01[0].ppn);
        if (ppn == 0)
        {
          PnlDetailPOLblPPn.Text = "";
          PnlDetailPOLblTotalPPn.Text = "0";
        }
        else {
          PnlDetailPOLblPPn.Text = string.Format("{0,20:N2}", purDataSet.vpur_po01[0].ppn) + "%";
          PnlDetailPOLblTotalPPn.Text = string.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].ppnval));
        }

        PnlDetailPOLblJasaLain.Text = purDataSet.vpur_po01[0].jasalain;
        PnlDetailPOLblBiayaJasaLain.Text = string.Format("{0,20:N2}", Convert.ToDouble(purDataSet.vpur_po01[0].biayajasalain));

        PnlDetailPOLblTotalPO.Text = String.Format("{0,20:N2}", purDataSet.vpur_po01[0].totalpo);

        PnlDetailPOLblNotes.Text = purDataSet.vpur_po01[0].catatan;
        PnlDetailPOLblKeterangan.Text = purDataSet.vpur_po01[0].keterangan;

        string unitkerja = purDataSet.vpur_po01[0].unitkerja;
        if (unitkerja == "SHIPYARD")
        {
          PnlPOTipe.Visible = true;
          string type = purDataSet.vpur_po01[0].type;
          if (type == "P") { PnlDetailPOLblTipe.Text = "Project"; }
          else { PnlDetailPOLblTipe.Text = "Non Project"; }
        }
        else { PnlPOTipe.Visible = false; }


        string jenispo = purDataSet.vpur_po01[0].jenispo;
        if (jenispo == "LKL") { PnlDetailPOLblJenis.Text = "Lokal"; }
        else { PnlDetailPOLblJenis.Text = "Import"; }

        PnlDetailPOModalPopupExtender.Show();
      }
    }
  }
}
