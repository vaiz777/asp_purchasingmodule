using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TmsBackDataController;
using TmsBackDataController.PurDataSetTableAdapters;
using ManagementSystem.Helper;

namespace ManagementSystem.Purchasing.Master.Master_Kategori
{
  public partial class Input : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void ClearForm()
    {
      TxtKodeKategori.Text = string.Empty;
      TxtNamaKategori.Text = string.Empty;
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
      var purDataSet = new PurDataSet();;
      var kategoriTabelAdapter = new pur_kategoriTableAdapter();
      var kategoriRow = purDataSet.pur_kategori.Newpur_kategoriRow();

      try
      {
        kategoriRow.inisial = TxtKodeKategori.Text;
        kategoriRow.nama = TxtNamaKategori.Text;

        purDataSet.pur_kategori.Addpur_kategoriRow(kategoriRow);
        if (kategoriTabelAdapter.Update(purDataSet.pur_kategori) > 0) {
          CUtils.UpdateLog("pur_kategori", (Convert.ToInt32(kategoriTabelAdapter.ScalarGetMaxId()) + 1).ToString(), Session["Username"].ToString(), "Input kategori");
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
