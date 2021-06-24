<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.Material_Request.NonProject.Purchase_Order.List" Title="List PO Non Project" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Daftar Purchase Order | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px;">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; ">
        <img src="../../../../images/icons/icons8-product-48.png" alt="icons8-time-card-48.png" />&ensp;<asp:Label ID="LblJudul" runat="server" ></asp:Label>
      </div><br />
      <asp:Panel ID="PnlData1" runat="server" HorizontalAlign="Center">
        <asp:Label ID="Label5" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
      </asp:Panel>
      <asp:Panel ID="PnlData2" Visible="false" runat="server">
        <div style="padding-bottom: 15px;">
          <asp:UpdatePanel ID="Pnl1" runat="server">
            <ContentTemplate>
              <table>
                <asp:Panel ID="PnlKataKunci" runat="server">
                <tr>
                  <td class="tableFieldHeader">Kata Kunci</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader" colspan="3">
                    <asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox>
                  </td>
                </tr>
                </asp:Panel>
                <asp:Panel ID="PnlTanggal" runat="server" Visible="false">
                <tr>
                  <td class="tableFieldHeader">Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtTanggal1" runat="server"></asp:TextBox>
                    <act:CalendarExtender ID="calendar1" runat="server"  Format="MM/dd/yyyy" Enabled="True" TargetControlID="TxtTanggal1" />
                  </td>
                  <td class="tableFieldHeader">&ensp;s/d&ensp;</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtTanggal2" runat="server"></asp:TextBox>
                    <act:CalendarExtender ID="calendar2" runat="server"  Format="MM/dd/yyyy" Enabled="True" TargetControlID="TxtTanggal2" />
                  </td>
                </tr>
                </asp:Panel>
                <asp:Panel ID="PnlStatus" runat="server" Visible="false">
                <tr>
                  <td class="tableFieldHeader">Status</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader" colspan="3">
                    <asp:DropDownList ID="DlistStatus" runat="server">
                      <asp:ListItem Value="PO1">PO Baru</asp:ListItem>
                      <asp:ListItem Value="PO2">PO Pending</asp:ListItem>
                      <asp:ListItem Value="PO3">PO Disetujui</asp:ListItem>
                      <asp:ListItem Value="PO4">PO Lengkap</asp:ListItem>
                      <asp:ListItem Value="PO5">PO Ditutup</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                </asp:Panel>                
                <tr>
                  <td class="tableFieldHeader">Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader" colspan="3">
                    <asp:DropDownList ID="DlistKategoriPencarian" runat="server" onselectedindexchanged="DlistKategoriPencarian_SelectedIndexChanged" AutoPostBack="True">
                      <asp:ListItem Value="0">No PO</asp:ListItem>
                      <asp:ListItem Value="1">Status</asp:ListItem>
                      <asp:ListItem Value="2">Tanggal</asp:ListItem>
                      <asp:ListItem Value="3">Supplier</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldButton" colspan="5">
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" onclick="BtnSearchClick" />
                    <asp:Button ID="BtnCancel" runat="server" Text="Cancel" onclick="BtnCancelClick" />
                  </td>
                </tr>
              </table>
            </ContentTemplate>
          </asp:UpdatePanel>
        </div>
        <div style="text-align: right">
          <asp:Button ID="BtnInput" runat="server" onclick="BtnInput_Click" Text="Tambah PO (+)" />
        </div><br />
        <asp:UpdatePanel ID="PnlGrid" runat="server">
          <ContentTemplate>
            <telerik:RadGrid ID="GridPO" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="PODataSource" GridLines="None" onitemdatabound="GridPO_ItemDataBound" onitemcommand="GridPO_ItemCommand">
              <MasterTableView AutoGenerateColumns="False" DataSourceID="PODataSource">
                <Columns>
                   <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                   <telerik:GridBoundColumn DataField="nomerpo" HeaderText="Nomer" SortExpression="nomerpo" UniqueName="nomerpo" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true"  />
                   <telerik:GridBoundColumn DataField="tglpo" DataType="System.DateTime" HeaderText="Tanggal" SortExpression="tglpo" UniqueName="tglpo" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" />
                   <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                   <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" />
                   <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                   <telerik:GridBoundColumn DataField="totalpo" DataType="System.Decimal" HeaderText="Total" SortExpression="totalpo" UniqueName="totalpo"  ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}" />
                   <%--<telerik:GridBoundColumn DataField="kurs" DataType="System.Decimal" HeaderText="kurs" SortExpression="kurs" UniqueName="kurs" Visible="false" />--%>
                   <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                   <telerik:GridBoundColumn DataField="unitkerja" HeaderText="UnitKerja" SortExpression="unitkerja" UniqueName="unitkerja" />
                   <telerik:GridTemplateColumn HeaderText="Requestor" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "createdby")%><br />
                        ( <%# DataBinder.Eval(Container.DataItem, "datecreated")%> )
                      </ItemTemplate>
                   </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn">
                      <ItemTemplate>
                        <asp:Button ID="BtnPrint" runat="server" Text="Print" CommandName="Print" Visible="false" />
                        <asp:Button ID="BtnUpdate" runat="server" Text="Update" CommandName="Update" Visible="false" />
                        <asp:Button ID="BtnCheck" runat="server" Text="Check" CommandName="Check" Visible="false" />
                        <asp:Button ID="BtnClose" runat="server" Text="Close" CommandName="Close" Visible="false" />
                        <asp:Button ID="BtnBatal" runat="server" Text="Batal" CommandName="Batal" Visible="false" />                                                                        
                      </ItemTemplate>
                   </telerik:GridTemplateColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
              </MasterTableView>
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid>
          </ContentTemplate>
        </asp:UpdatePanel> 
      </asp:Panel>
    </div>
  </div>  
  
  <asp:ObjectDataSource ID="PODataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByUnitKerjaType" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter" 
    oninit="PODataSource_Init">
    <SelectParameters>
      <asp:SessionParameter ConvertEmptyStringToNull="False" Name="unitkerja" SessionField="UnitKerja" Type="String" DefaultValue="0" />
      <asp:Parameter DefaultValue="NP" Name="type" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>

  <%--"Panel Message"--%>
  <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup" Style="display: none">
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
  
  <%--" Panel Close "--%>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlCloseModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlCloseLinkButton" CancelControlID="PnlCloseBtnClose" DropShadow="false" PopupControlID="PnlClose" PopupDragHandleControlID="PnlCloseTitlebar" BackgroundCssClass="modalBackground" />
       <asp:Panel ID="PnlClose" runat="server" Width="480px" CssClass="modalPopup">
          <div>
            <asp:Panel ID="PnlCloseTitlebar" runat="server" CssClass="modalPopupTitle">
              <div style="padding:5px; text-align:left">
                <asp:Label ID="PnlCloseLblTitlebar" runat="server" Text="Message Box" />
              </div>
            </asp:Panel>
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td style="padding: 5px">
                   <img src="../../../../images/icons/icons8-task-completed-48.png" alt="icons8-task-completed-48.png"  />
                  </td>
                  <td style="padding: 5px; font-size:15px" valign="middle">
                    <asp:Label ID="Label3" runat="server" Text="Tutup PO nomer "></asp:Label>
                    <asp:Label ID="PnlCloseLblNoPO" runat="server"></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text=" ?"></asp:Label>
                  </td>
                </tr>
              </table>
              <div style="text-align: center; padding-top: 10px">
                <asp:Button ID="PnlCloseBtnOk" runat="server" Text="Oke" onclick="PnlCloseBtnOk_Click"  />&ensp;&ensp;
                <asp:Button ID="PnlCloseBtnClose" runat="server" Text="Cancel"  />
                <asp:LinkButton ID="PnlCloseLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
              </div>
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <%--"Panel Cek"--%>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlShowConfirmPopupExtender" runat="server" Enabled="True" TargetControlID="PnlShowLinkButton" CancelControlID="PnlShowBtnClose" DropShadow="false" PopupControlID="PnlShow" PopupDragHandleControlID="PnlShowTitlebar" BackgroundCssClass="modalBackground" />
       <asp:Panel ID="PnlShow" runat="server" Width="480px" CssClass="modalPopup">
          <div>
            <asp:Panel ID="PnlShowTitlebar" runat="server" CssClass="modalPopupTitle">
              <div style="padding:5px; text-align:left">
                <asp:Label ID="Label1" runat="server" Text="Konfirmasi" />
              </div>
            </asp:Panel>
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td style="padding: 5px">
                   <img src="../../../../images/icons/icons8-task-completed-48.png" alt="icons8-task-completed-48.png"  />
                  </td>
                  <td style="padding: 5px; font-size:15px" valign="middle">
                    <asp:Label ID="Label2" runat="server" Text="Apakah Anda menyetujui PO nomer "></asp:Label>
                    <asp:Label ID="PnlShowLblNoPO" runat="server"></asp:Label>
                    <asp:Label ID="Label7" runat="server" Text=" ?"></asp:Label>
                  </td>
                </tr>
              </table>
              <div style="text-align: center; padding-top: 10px">
                <asp:Button ID="PnlShowBtnSetuju" runat="server" Text="Setuju" onclick="PnlShowBtnSetuju_Click"  />&ensp;
                <asp:Button ID="PnlShowBtnReject" runat="server" Text="Tolak" onclick="PnlShowBtnReject_Click"  />&ensp;
                <asp:Button ID="PnlShowBtnClose" runat="server" Text="Cancel"  />
                <asp:LinkButton ID="PnlShowLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
              </div>
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <%--Panel Batal--%>
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBatalModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBatalLinkButton" CancelControlID="PnlBatalBtnClose" DropShadow="false" PopupControlID="PnlBatal" PopupDragHandleControlID="PnBatalTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlBatal" runat="server" Width="480px" CssClass="modalPopup" >
          <div>
            <asp:Panel ID="PnBatalTitlebar" runat="server" CssClass="modalPopupTitle">
              <div style="padding:5px; text-align:left">
                <asp:Label ID="PnBatalLblTitlebar" runat="server" Text="Konfirmasi" />
              </div>
            </asp:Panel>
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td style="padding: 5px">
                   <img src="../../../../images/icons/icons8-task-completed-48.png" alt="icons8-task-completed-48.png"  />
                  </td>
                  <td style="padding: 5px; font-size:15px" valign="middle">
                    <asp:Label ID="Label8" runat="server" Text="Batal "></asp:Label>
                    <asp:Label ID="PnlBatalTxtNoPO" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label11" runat="server" Text=" ?"></asp:Label>
                  </td>
                </tr>
              </table>
              <div style="text-align: center; padding-top: 10px">
                <asp:Button ID="PnlBatalBtnOk" runat="server" Text="Oke"  onclick="PnlBatalBtnOk_Click"  />&ensp;&ensp;
                <asp:Button ID="PnlBatalBtnClose" runat="server" Text="Cancel"  />
                <asp:LinkButton ID="PnlBatalLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
              </div>
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  
  <%--"Panel Print"--%>
  <act:ModalPopupExtender ID="PnlKonfirmModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlKonfirmLinkButton" CancelControlID="PnlKonfirmPrintCancel" DropShadow="false" PopupControlID="PnlKonfirm" PopupDragHandleControlID="PnlKonfirmTitlebar"/> 
    <asp:Panel ID="PnlKonfirm" runat="server" Width="480px" CssClass="modalPopup">
      <asp:Panel ID="PnlKonfirmTitleBar" runat="server" CssClass="modalPopupTitle">
        <div style="padding:5px; text-align:left">
          <asp:Label ID="PnlKonfirmLblTitleBar" runat="server" Text="Konfirmasi" />
        </div>
      </asp:Panel>
      <div style="padding:5px; text-align:left">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
          <ContentTemplate>
            <table>
            <tr>
              <td style="padding: 5px">
                <asp:Image ID="PnlKonfirmImgIcon" runat="server" ImageUrl="~/images/icons/icons8-print-48.png" />
              </td>
              <td style="padding: 5px">
                <asp:Label ID="PnlKonfirmLblMessage" runat="server" Text="Print Nomor " />
                <asp:Label ID="LblNoPO" runat="server"></asp:Label>
                <asp:HiddenField ID="HdIdPO" runat="server" />
                <asp:Label ID="Label6" runat="server" Text=" ?"></asp:Label>
              </td>
            </tr>
          </table>
          </ContentTemplate>
        </asp:UpdatePanel>        
        <div style="text-align: center; padding-top: 10px">
          <asp:Button ID="PnlKonfirmPrintOk" runat="server" Text="Print" onclick="PnlKonfirmPrintOk_Click" />&ensp;
          <asp:Button ID="PnlKonfirmPrintCancel" runat="server" Text="Cancel" />
          <asp:LinkButton ID="PnlKonfirmLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
        </div>
      </div>
    </asp:Panel>
</asp:Content>
