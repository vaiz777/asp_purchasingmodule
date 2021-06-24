<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.NonProject.Price_Request.Item" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Input Request Harga (Non Project) - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="/images/icons/icons8-compose-48.png" alt="icons8-compose-48.png"  />&ensp;<asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
      </div><br />
      <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <table>
              <tr>
                <td class="tableFieldHeader">No WR</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="TxtNoWR" runat="server" CssClass="textbox_default" Enabled="false"></asp:TextBox>
                </td>
                <td style="width: 30px;"></td>
                <td class="tableFieldHeader">Tgl Dibutuhkan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="TxtTanggal" runat="server" CssClass="textbox_default"></asp:TextBox>
                  <act:CalendarExtender ID="CalTxtTanggal" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="TxtTanggal" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jasa</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:HiddenField ID="HdKdJasa" runat="server" />
                        <asp:TextBox ID="TxtJasa" runat="server" CssClass="textbox_default" ReadOnly="True" ></asp:TextBox>
                      </td>
                      <td><asp:Button ID="BtnBrowseReqJasa" runat="server" Text="..." onclick="BtnBrowseReqJasa_Click" style="width: 26px" /></td>
                    </tr>
                  </table>
                </td>
                <td style="width: 30px;"></td>
                <td class="tableFieldHeader">Supplier / Vendor</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:HiddenField ID="HdIdSupplier" runat="server" />
                        <asp:TextBox ID="TxtSupplier" runat="server" CssClass="textbox_default" ReadOnly="True" ></asp:TextBox>
                      </td>
                      <td><asp:Button ID="BtnBrowseSupplier" runat="server" Text="..." onclick="BtnBrowseSupplier_Click" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jml Jasa</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtJmlJasa" runat="server" CssClass="textbox_default" ></asp:TextBox></td>
                <td style="width: 30px;"></td>
                <td class="tableFieldHeader">Harga</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <telerik:RadNumericTextBox ID="RadTxtHarga" runat="server" />
                      </td>
                      <td>
                        <asp:DropDownList ID="DListSatuan" runat="server" AutoPostBack="true">
                          <asp:ListItem Value="IDR">IDR</asp:ListItem>
                          <asp:ListItem Value="SGD">SGD</asp:ListItem>
                          <asp:ListItem Value="USD">USD</asp:ListItem>
                          <asp:ListItem Value="AUD">AUD</asp:ListItem>
                          <asp:ListItem Value="EUR">EUR</asp:ListItem>
                        </asp:DropDownList>
                      </td>
                      <td>
                        <asp:Button ID="BtnCekHistoryHarga" runat="server" Text="Cek Harga" onclick="BtnCekHistoryHarga_Click" />
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Satuan</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtSatuan" runat="server" CssClass="textbox_default" ></asp:TextBox></td>
                <td style="width: 30px;"></td>
                <td></td>
                <td></td>
                <td></td>
              </tr>
              <asp:Panel ID="PnlSM" runat="server">
              <tr>
                <td class="tableFieldHeader">Jml Orang</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtJmlOrang" runat="server" CssClass="textbox_default" ></asp:TextBox></td>
                <td style="width: 30px;"></td>
                <td></td>
                <td></td>
                <td></td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jml Hari</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtJmlHari" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                <td style="width: 30px;"></td>
                <td></td>
                <td></td>
                <td></td>
              </tr>
              </asp:Panel>              
              <tr>
                <td class="tableFieldHeader">Keterangan</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtKeterangan" runat="server" CssClass="textbox_default"  TextMode="MultiLine"></asp:TextBox></td>
                <td style="width: 30px;"></td>
                <td></td>
                <td></td>
                <td></td>
              </tr>
            </table><br />
            <div>
              <asp:Button ID="BtnSave" runat="server" Text="Simpan" onclick="BtnSave_Click"  />&ensp;
              <asp:Button ID="BtnCancel" runat="server" Text="Kembali" onclick="BtnCancel_Click" />
            </div><br />
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  
  <asp:ObjectDataSource ID="SupplierDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_supplierTableAdapter" 
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" >
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_nama" Type="String" />
      <asp:Parameter Name="Original_alamat" Type="String" />
      <asp:Parameter Name="Original_kota" Type="String" />
      <asp:Parameter Name="Original_notelpkantor" Type="String" />
      <asp:Parameter Name="Original_notelppribadi" Type="String" />
      <asp:Parameter Name="Original_nofax" Type="String" />
      <asp:Parameter Name="Original_jenisbayar" Type="String" />
      <asp:Parameter Name="Original_kontakperson" Type="String" />
      <asp:Parameter Name="Original_email" Type="String" />
      <asp:Parameter Name="Original_npwp" Type="String" />
      <asp:Parameter Name="Original_jenisusaha" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="alamat" Type="String" />
      <asp:Parameter Name="kota" Type="String" />
      <asp:Parameter Name="notelpkantor" Type="String" />
      <asp:Parameter Name="notelppribadi" Type="String" />
      <asp:Parameter Name="nofax" Type="String" />
      <asp:Parameter Name="jenisbayar" Type="String" />
      <asp:Parameter Name="kontakperson" Type="String" />
      <asp:Parameter Name="email" Type="String" />
      <asp:Parameter Name="npwp" Type="String" />
      <asp:Parameter Name="jenisusaha" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_nama" Type="String" />
      <asp:Parameter Name="Original_alamat" Type="String" />
      <asp:Parameter Name="Original_kota" Type="String" />
      <asp:Parameter Name="Original_notelpkantor" Type="String" />
      <asp:Parameter Name="Original_notelppribadi" Type="String" />
      <asp:Parameter Name="Original_nofax" Type="String" />
      <asp:Parameter Name="Original_jenisbayar" Type="String" />
      <asp:Parameter Name="Original_kontakperson" Type="String" />
      <asp:Parameter Name="Original_email" Type="String" />
      <asp:Parameter Name="Original_npwp" Type="String" />
      <asp:Parameter Name="Original_jenisusaha" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="alamat" Type="String" />
      <asp:Parameter Name="kota" Type="String" />
      <asp:Parameter Name="notelpkantor" Type="String" />
      <asp:Parameter Name="notelppribadi" Type="String" />
      <asp:Parameter Name="nofax" Type="String" />
      <asp:Parameter Name="jenisbayar" Type="String" />
      <asp:Parameter Name="kontakperson" Type="String" />
      <asp:Parameter Name="email" Type="String" />
      <asp:Parameter Name="npwp" Type="String" />
      <asp:Parameter Name="jenisusaha" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="MasterJasaDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_jasaTableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
   </asp:ObjectDataSource>
   <asp:ObjectDataSource ID="CekHargaDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataCekHargaByTypeUnitKerjaJasaKodeSupplierId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wo01TableAdapter">
     <SelectParameters>
       <asp:Parameter DefaultValue="NP" Name="type" Type="String" />
       <asp:SessionParameter DefaultValue="" Name="unitkerja" SessionField="UnitKerja" 
         Type="String" />
       <asp:ControlParameter ControlID="HdKdJasa" DefaultValue="0" Name="jasaKode" 
         PropertyName="Value" Type="String" />
       <asp:ControlParameter ControlID="HdIdSupplier" DefaultValue="0" 
         Name="supplierId" PropertyName="Value" Type="Int64" />
     </SelectParameters>
   </asp:ObjectDataSource>
  
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
              <asp:Button ID="PnlMessageBtnOk" runat="server" Text="OK"  />
              <asp:LinkButton ID="PnlMessageLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseReqJasaModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseReqJasaLinkButton" DropShadow="false" PopupControlID="PnlBrowseReqJasa" PopupDragHandleControlID="PnlBrowseReqJasaTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseReqJasa" runat="server" Width="40%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseReqJasaTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseReqJasaLblTitlebar" runat="server" Text="Daftar Jasa" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px;">
          <table>
            <tr>
              <td class="tableFieldHeader">Cari Jasa</td>
              <td class="tableFieldHeader">:</td>
              <td><asp:TextBox ID="PnlBrowseReqJasaTxtJasa" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 10px"></td>
              <td><asp:Button ID="BtnSearchJasa" runat="server" Text="Cari" onclick="BtnSearchJasa_Click" /></td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridReqJasa" runat="server" AllowPaging="True" DataSourceID="MasterJasaDataSource" GridLines="None" onitemcommand="GridReqJasa_ItemCommand" onitemdatabound="GridReqJasa_ItemDataBound" onpageindexchanged="GridReqJasa_PageIndexChanged" onpagesizechanged="GridReqJasa_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="kode" 
              DataSourceID="MasterJasaDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="kode" HeaderText="Kode Jasa" SortExpression="kode" UniqueName="kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Jasa" SortExpression="nama" UniqueName="nama" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings  EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseReqJasaLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseReqJasaBtnClose" runat="server" Text="Close"/><br />
        </div>
      </div>
      </asp:Panel> 
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseSupplierModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseSupplierLinkButton" DropShadow="false" PopupControlID="PnlBrowseSupplier" PopupDragHandleControlID="PnlBrowseSupplierTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseSupplier" runat="server" Width="80%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseSupplierTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="Label1" runat="server" Text="Daftar Supplier" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px;">
          <table>
            <tr>
              <td class="tableFieldHeader">Cari Nama</td>
              <td class="tableFieldHeader">:</td>
              <td><asp:TextBox ID="PnlSupplierTxtSerchBy" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 10px"></td>
              <td><asp:Button ID="PnlSupplierBtnSearch" runat="server" Text="Cari" onclick="PnlSupplierBtnSearch_Click" /></td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridSupplier" runat="server" AllowPaging="True" DataSourceID="SupplierDataSource" GridLines="None" onitemcommand="GridSupplier_ItemCommand" onneeddatasource="GridSupplier_NeedDataSource" AllowSorting="True" onpageindexchanged="GridSupplier_PageIndexChanged" onpagesizechanged="GridSupplier_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SupplierDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int64"  />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Supplier" SortExpression="nama" UniqueName="nama" />
                <telerik:GridBoundColumn DataField="alamat" HeaderText="Alamat" SortExpression="alamat" UniqueName="alamat" />
                <telerik:GridBoundColumn DataField="kota" HeaderText="Kota" SortExpression="kota" UniqueName="kota" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="notelpkantor" HeaderText="Telp Kantor" SortExpression="notelpkantor" UniqueName="notelpkantor" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="kontakperson" HeaderText="Kontak" SortExpression="kontakperson" UniqueName="kontakperson" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="npwp" HeaderText="NPWP" SortExpression="npwp" UniqueName="npwp" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
              <Scrolling UseStaticHeaders="true" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseSupplierLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseSupplierBtnClose" runat="server" Text="Close"/><br />
        </div>
      </div>
      </asp:Panel>  
    </ContentTemplate>
  </asp:UpdatePanel>
  

  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseHargaPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseHargaLinkButton" DropShadow="false" PopupControlID="PnlBrowseHarga" PopupDragHandleControlID="PnlBrowseHargaTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseHarga" runat="server" Width="70%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseHargaTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-cash-48.png" alt="icons8-cash-48.png" />
                  </td>
                  <td class="">
                    &ensp;<asp:Label ID="Label2" runat="server" Text="Cek Harga" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px;">
          <telerik:RadGrid ID="GridCekHarga" runat="server" AllowPaging="True" DataSourceID="CekHargaDataSource" GridLines="None" onitemcommand="GridCekHarga_ItemCommand" >
            <MasterTableView AutoGenerateColumns="False" DataSourceID="CekHargaDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                <telerik:GridBoundColumn DataField="nomerwo" HeaderText="No WO" SortExpression="nomerwo" UniqueName="nomerwo" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tglwo" DataType="System.DateTime" HeaderText="Tgl" SortExpression="tglwo" UniqueName="tglwo" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" HeaderText="Harga" SortExpression="harga" UniqueName="harga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="wodetail_diskon" DataType="System.Single" HeaderText="Diskon (%)" SortExpression="wodetail_diskon" UniqueName="wodetail_diskon" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="wodetail_totalharga" DataType="System.Decimal" HeaderText="Total" SortExpression="wodetail_totalharga" UniqueName="wodetail_totalharga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings  EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseHargaLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseHargaBtnClose" runat="server" Text="Close"/><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
