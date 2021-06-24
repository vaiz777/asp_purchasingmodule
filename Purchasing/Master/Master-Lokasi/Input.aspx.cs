using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using ManagementSystem.Helper;

namespace ManagementSystem.Purchasing.Master.Master_Lokasi
{
  public partial class Input : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void ClearForm()
    {
      TxtKodeLokasi.Text = string.Empty;
      TxtNamaLokasi.Text = string.Empty;
    }

    protected void notificationSuccess()
    {
      ClearForm();
      // Pnl Confirmation
      PnlMessageLblTitlebar.Text = "Confirm";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-approval-48.png";
      PnlMessageLblMessage.Text = "Data berhasil disimpan.";
      PnlMessageModalPopupExtender.Show();
    }

    protected void notificationFailure(Exception ex)
    {
      PnlMessageLblTitlebar.Text = "Oops!";
      PnlMessageImgIcon.ImageUrl = "~/images/icons/icons8-fragile-48.png";
      PnlMessageLblMessage.Text = string.Concat("Sesuatu tidak berjalan semestinya.. ", ex.Message);
      PnlMessageModalPopupExtender.Show();
      // _errorLogTableAdapter.Insert(DateTime.Now, Session["Username"].ToString(), "Gs.Ac.InputAkta.BtnSaveClick()", ex.Message);    
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
      var purDataSet = new PurDataSet(); ;
      var lokasiTabelAdapter = new pur_lokasiTableAdapter();
      var lokasiRow = purDataSet.pur_lokasi.Newpur_lokasiRow();

      try
      {
        lokasiRow.inisial = TxtKodeLokasi.Text;
        lokasiRow.nama = TxtNamaLokasi.Text;
        lokasiRow.unitkerja = Session["UnitKerja"].ToString();

        purDataSet.pur_lokasi.Addpur_lokasiRow(lokasiRow);
        if (lokasiTabelAdapter.Update(purDataSet.pur_lokasi) > 0)
        {
          CUtils.UpdateLog("pur_lokasi", (Convert.ToInt32(lokasiTabelAdapter.ScalarGetMaxId()) + 1).ToString(), Session["Username"].ToString(), "Input Lokasi");
        }
        notificationSuccess();
      }
      catch (Exception ex)
      {
        notificationFailure(ex);
      }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("List.aspx");

    }
  }
}
