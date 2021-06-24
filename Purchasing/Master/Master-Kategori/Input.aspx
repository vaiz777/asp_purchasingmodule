<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Input.aspx.cs" Inherits="ManagementSystem.Purchasing.Master.Master_Kategori.Input" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Input Kategori - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="padding: 10px">
  <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px; width: 80%">
    <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
      <img src="../../../images/icons/icons8-product-documents-48.png" alt="icons8-air-conditioner-48.png" />&nbsp;Input Kategori
    </div><br />
    <div style="padding-bottom: 15px">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td class="tableFieldHeader">Kode Kategori</td>
              <td class="tableFieldHeader">: </td>
              <td>
                <asp:TextBox ID="TxtKodeKategori" runat="server" CssClass="textbox_default"  />
              </td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td class="tableFieldHeader">Nama Kategori</td>
              <td class="tableFieldHeader">: </td>
              <td class="tableFieldHeader">
                <asp:TextBox ID="TxtNamaKategori" runat="server" CssClass="textbox_default"  />
              </td>
              <td>&nbsp;</td>
            </tr>
          </table>
          <br />
          <div style="text-align: left">
            <asp:Button ID="BtnSave" runat="server" Text="Save" onclick="BtnSave_Click"  />&ensp;
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" onclick="BtnCancel_Click"  />
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</div>

<asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
  <ContentTemplate>
    <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup">
      <div>
        <asp:Panel ID="PnlMessageTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <asp:Label ID="PnlMessageLblTitlebar" runat="server" Text="MessageBox" />
          </div>
        </asp:Panel>
        <div style="padding:5px; text-align:left">
          <table>
            <tr>
              <td style="padding: 5px">
                <asp:Image ID="PnlMessageImgIcon" runat="server" />
              </td>
              <td style="padding: 5px">
                <asp:Label ID="PnlMessageLblMessage" runat="server" Text="Hello" />
              </td>
            </tr>
          </table>
          <div style="text-align: center; padding-top: 10px">
            <asp:Button ID="PnlMessageBtnOk" runat="server" Text="OK" />
            <asp:LinkButton ID="PnlMessageLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          </div>
        </div>
      </div>
    </asp:Panel>
  </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
