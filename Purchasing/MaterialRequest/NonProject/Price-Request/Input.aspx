<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Input.aspx.cs" Inherits="ManagementSystem.Purchasing.Material_Request.NonProject.Price_Request.Input" Title="Input Price" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>Input Harga | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; ">
        <img src="../../../../images/icons/icons8-product-48.png" alt="icons8-time-card-48.png" />&ensp;<asp:Label ID="LblTitle" runat="server" Text="Material Request" ></asp:Label>
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
          <table>
            <tr>
              <td class="tableFieldHeader">Nomor MR</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader">
                <asp:HiddenField ID="HdIdReqBarang" runat="server" />
                <asp:TextBox ID="TxtNomorRequest" runat="server" Width="148px" ReadOnly="True" CssClass="textbox_default" Enabled="False" />
              </td>
              <td class="tableFieldHeader">&nbsp;</td>
              <td class="tableFieldHeader">&nbsp;</td>
              <td class="tableFieldHeader">Supplier</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader">
                <asp:HiddenField ID="HdIdSupplier" runat="server" />
                <asp:TextBox ID="TxtSupplier" runat="server" Width="148px" CssClass="textbox_default"></asp:TextBox>
              </td>
              <td class="tableFieldHeader" colspan="2">
                <asp:Button ID="BtnOption1" runat="server" onclick="BtnOption1_Click" Text="...." />
              </td>
            </tr>
            <tr>
              <td class="tableFieldHeader">Nama Barang</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader">
                <asp:HiddenField ID="HdKodeBarang" runat="server" />             
                <asp:TextBox ID="TxtNamaBarang" runat="server" Width="148px" ReadOnly="True" CssClass="textbox_default" />
              </td>
              <td class="tableFieldHeader">
                <asp:Button ID="BtnChoice" runat="server" Text="...." onclick="BtnChoice_Click"/>
              </td>
              <td class="tableFieldHeader">
                <asp:Button ID="BtnTambah" runat="server" Text="+" onclick="BtnTambah_Click"/>
              </td>
              <td class="tableFieldHeader">Harga</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader">
                <telerik:RadNumericTextBox ID="RadTxtHarga" runat="server" Width="148px" 
                  ontextchanged="RadTxtHarga_TextChanged" AutoPostBack="true"></telerik:RadNumericTextBox>
              </td>
              <td class="tableFieldHeader">
                <asp:DropDownList ID="DlistCurrency" runat="server" >
                  <asp:ListItem Value="IDR">IDR</asp:ListItem>
                  <asp:ListItem Value="USD">USD</asp:ListItem>
                  <asp:ListItem Value="SGD">SGD</asp:ListItem>
                  <asp:ListItem Value="AUD">AUD</asp:ListItem>
                  <asp:ListItem Value="EUR">EUR</asp:ListItem>
                </asp:DropDownList>
              </td>
              <td class="tableFieldHeader">
                <asp:Button ID="BtnSuggest" runat="server" Text="Cek Harga" Width="77px" onclick="BtnSuggest_Click" />
              </td>
            </tr>
            <tr>
              <td class="tableFieldHeader">Jumlah</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader">
                <asp:TextBox ID="TxtJumlah" runat="server" Width="148px" AutoPostBack="True" 
                  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
              </td>
              <td class="tableFieldHeader"></td>
              <td class="tableFieldHeader">&nbsp;</td>
              <td class="tableFieldHeader">Total Harga</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader" colspan="3">
                <telerik:RadNumericTextBox ID="RadTxtTotal" runat="server" Width="148px"></telerik:RadNumericTextBox>
              </td>
            </tr>
            <tr>
              <td class="tableFieldHeader">Satuan</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader" colspan="8">
                <asp:TextBox ID="TxtSatuan" runat="server" Width="148px" ReadOnly="True" 
                  CssClass="textbox_default"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td class="tableFieldHeader">Keterangan</td>
              <td class="tableFieldSeparator">:</td>
              <td class="tableFieldHeader" colspan="8">
                <asp:TextBox ID="TxtKeterangan" runat="server" Width="148px" TextMode="MultiLine" ></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td class="tableFieldButton" colspan="10">
                <asp:Button ID="BtnSave" runat="server" onclick="BtnSave_Click" Text="Simpan" />&ensp;
                <asp:Button ID="PnlViewHargaBtnClose" runat="server" onclick="PnlViewBarangBtnClose_Click" Text="Cancel" />
              </td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridHarga" runat="server" AllowPaging="True" 
            AllowSorting="True" DataSourceID="MRDetailDataSource" GridLines="None" 
            onitemcommand="GridHarga_ItemCommand" 
            onitemdatabound="GridHarga_ItemDataBound">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="MRDetailDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int64" Visible="false" />
                <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" 
                  SortExpression="barang_kode" UniqueName="barang_kode" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                <telerik:GridBoundColumn DataField="jumlah" HeaderText="Jumlah" 
                  SortExpression="jumlah" UniqueName="jumlah" DataType="System.Int32" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" 
                  SortExpression="satuan_nama" UniqueName="satuan_nama" 
                  ItemStyle-HorizontalAlign="Center" >              
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                  SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" />
                <telerik:GridBoundColumn DataField="currency" HeaderText="" 
                  SortExpression="currency" UniqueName="currency" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" 
                  HeaderText="Harga" SortExpression="harga" UniqueName="harga" 
                  ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}"  >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Hapus" CommandName="DeleteClick" Visible="false" />
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" />
                 </telerik:GridTemplateColumn>
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
            <ClientSettings>
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>

  <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar"/>
        <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup" Style="display:none">
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
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlRequestedBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlRequestedBarangLinkButton" CancelControlID="PnlRequestedBarangBtnOk" DropShadow="false" PopupControlID="PnlRequestedBarang" PopupDragHandleControlID="PnlRequestedBarangTitlebar"/>
        <asp:Panel ID="PnlRequestedBarang" runat="server" Width="60%" CssClass="modalPopup">
          <asp:Panel ID="PnlRequestedBarangTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                  </td>
                  <td>
                    <asp:Label ID="Label2" runat="server" Text="Daftar Item Barang" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel><br />
          <div style="padding:5px; text-align:left">
            <telerik:RadGrid ID="GridBarangRequested" runat="server" GridLines="None" 
              onitemcommand="GridBarangRequested_ItemCommand" AllowPaging="True" 
              AllowSorting="True" DataSourceID="MRDetailBaruDataSource" 
              onitemdatabound="GridBarangRequested_ItemDataBound">
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="MRDetailBaruDataSource">
                <RowIndicatorColumn>
                  <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn>
                  <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
                <Columns>
                  <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int64" Visible="false" />
                  <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" 
                    SortExpression="barang_kode" UniqueName="barang_kode" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                  <telerik:GridBoundColumn DataField="jumlah" DataType="System.Int32" 
                    HeaderText="Jumlah" SortExpression="jumlah" UniqueName="jumlah" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" 
                    SortExpression="satuan_nama" UniqueName="satuan_nama" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="tglpemenuhan" DataType="System.DateTime" 
                    HeaderText="Tgl Dibutuhkan" SortExpression="tglpemenuhan" 
                    UniqueName="tglpemenuhan" DataFormatString="{0:d}" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                    SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                  <telerik:GridBoundColumn DataField="createdby" HeaderText="Requestor" 
                    SortExpression="createdby" UniqueName="createdby" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
              </MasterTableView>
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid>
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlRequestedBarangBtnOk" runat="server" Text="Close" />
              <asp:LinkButton ID="PnlRequestedBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </asp:Panel>
      </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
        <act:ModalPopupExtender ID="PnlViewSupplierModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewSupplierLinkButton" CancelControlID="PnlViewSupplierBtnClose" DropShadow="false" PopupControlID="PnlViewSupplier" PopupDragHandleControlID="PnlViewSupplierTitlebar" BackgroundCssClass="modalBackground" />
          <asp:Panel ID="PnlViewSupplier" runat="server" Width="80%" CssClass="modalPopup" >
            <asp:Panel ID="PnlViewSupplierTitlebar" runat="server" CssClass="modalPopupTitle">
              <div style="padding:5px; text-align:left">
                <table>
                  <tr>
                    <td>
                      <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                    </td>
                    <td>
                      <asp:Label ID="Label4" runat="server" Text="Daftar Supplier" />
                    </td>
                  </tr>
                </table>
              </div>
            </asp:Panel>
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td class="tableFieldHeader">Kata Kunci</td>
                  <td>
                    <asp:TextBox ID="TxtKataKunci" runat="server" Width="170px" CssClass="textbox_default"></asp:TextBox>
                  </td>
                  <td>
                    &ensp;<asp:Button ID="BtnCariSupplier" runat="server" onclick="BtnCariSupplier_Click" Text="Cari" />
                  </td>
                </table><br />
              <telerik:RadGrid ID="GridSupplier" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="SupplierDataSource" GridLines="None" onitemcommand="GridSupplier_ItemCommand" onpageindexchanged="GridSupplier_PageIndexChanged" onpagesizechanged="GridSupplier_PageSizeChanged">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SupplierDataSource">
                  <RowIndicatorColumn>
                    <HeaderStyle Width="20px" />
                  </RowIndicatorColumn>
                  <ExpandCollapseColumn>
                    <HeaderStyle Width="20px" />
                  </ExpandCollapseColumn>
                  <Columns>
                    <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int32" Visible="false" />
                    <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Supplier" SortExpression="nama" UniqueName="nama" />
                    <telerik:GridBoundColumn DataField="alamat" HeaderText="Alamat" SortExpression="alamat" UniqueName="alamat" />
                    <telerik:GridBoundColumn DataField="kota" HeaderText="Kota" SortExpression="kota" UniqueName="kota" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="notelpkantor" HeaderText="No. Telp Kantor" SortExpression="notelpkantor" UniqueName="notelpkantor" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="kontakperson" HeaderText="UP" SortExpression="kontakperson" UniqueName="kontakperson" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="email" HeaderText="Email" SortExpression="email" UniqueName="email" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="jenisusaha" HeaderText="Jenis Usaha" SortExpression="jenisusaha" UniqueName="jenisusaha" ItemStyle-HorizontalAlign="Center" />
                  </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                  <Selecting AllowRowSelect="true" />
                </ClientSettings>
              </telerik:RadGrid>
              <div style="text-align: center; padding-top: 10px">
                <asp:LinkButton ID="PnlViewSupplierLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
                <asp:Button ID="PnlViewSupplierBtnClose" runat="server" Text="Close"  /><br />
              </div>
            </div>
          </asp:Panel>
      </ContentTemplate>
    </asp:UpdatePanel>  
        
    <asp:UpdatePanel ID="PnlSugestiModal" runat="server">
      <ContentTemplate>
        <act:ModalPopupExtender ID="PnlSuggestionsPriceModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlSuggestionsPriceLinkButton" DropShadow="false" PopupControlID="PnlSuggestionsPrice" PopupDragHandleControlID="PnlSuggestionsPriceTitlebar" CancelControlID="PnlSuggestionsPriceLinkButton" BackgroundCssClass="modalBackground" />
          <asp:Panel ID="PnlSuggestionsPrice" runat="server"  CssClass="modalPopup" Width="50%" >
            <div style="padding: 5px">
              <asp:Panel ID="PnlSuggestionsPriceTitlebar" runat="server" CssClass="modalPopupTitle">
                <div style="padding:5px; text-align:left">
                  <table>
                    <tr>
                      <td class="">
                        <img src="/images/icons/icons8-cash-48.png" alt="icons8-cash-48.png" />
                      </td>
                      <td class="">&ensp;Cek Harga</td>
                    </tr>
                  </table>
                </div>
              </asp:Panel>
            <div style="padding:10px; text-align:left">
              <telerik:RadGrid ID="GridCekHarga" runat="server" DataSourceID="CekHargaDataSource" 
                GridLines="None" onitemcommand="GridCekHarga_ItemCommand">
                <MasterTableView AutoGenerateColumns="False" DataSourceID="CekHargaDataSource">
                  <RowIndicatorColumn>
                    <HeaderStyle Width="20px" />
                  </RowIndicatorColumn>
                  <ExpandCollapseColumn>
                    <HeaderStyle Width="20px" />
                  </ExpandCollapseColumn>
                  <Columns>
                    <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                    <telerik:GridBoundColumn DataField="nomerpo" HeaderText="No PO" 
                      SortExpression="nomerpo" UniqueName="nomerpo" 
                      ItemStyle-HorizontalAlign="Center" >
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="tglpo" DataType="System.DateTime" 
                      HeaderText="Tgl" SortExpression="tglpo" UniqueName="tglpo" 
                      DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" >
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="currency" HeaderText="" 
                      SortExpression="currency" UniqueName="currency" 
                      ItemStyle-HorizontalAlign="Center" >
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" 
                      HeaderText="Harga" SortExpression="harga" UniqueName="harga" 
                      DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" >                    
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="podetail_diskon" DataType="System.Single" 
                      HeaderText="Diskon (%)" SortExpression="podetail_diskon" 
                      UniqueName="podetail_diskon" ItemStyle-HorizontalAlign="Center" >
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="podetail_total" DataType="System.Decimal" 
                      HeaderText="Total" SortExpression="podetail_total" UniqueName="podetail_total" 
                      DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" >                    
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridBoundColumn>
                  </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                  <Selecting AllowRowSelect="true" />
                </ClientSettings>
              </telerik:RadGrid>
            </div>
            <div style="padding-top:10px; text-align:center">
              <asp:LinkButton ID="PnlSuggestionsPriceLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton> 
              <asp:Button ID="PnlSuggestionsPriceBtnClose" runat="server" Text="Close" /><br />
            </div>
          </div>
        </asp:Panel>
      </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:ObjectDataSource ID="MRDetailDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataStatusSetHargaByMrId" TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
      <SelectParameters>
        <asp:ControlParameter ControlID="TxtNomorRequest" Name="mrId" PropertyName="Text" Type="String" DefaultValue="0" />
      </SelectParameters>
    </asp:ObjectDataSource>     
    <asp:ObjectDataSource ID="SupplierDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TmsBackDataController.PurDataSetTableAdapters.master_supplierTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" />
    <asp:ObjectDataSource ID="MRDetailBaruDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByStatusMrId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
      <SelectParameters>
        <asp:Parameter DefaultValue="B1" Name="status" Type="String" />
        <asp:ControlParameter ControlID="TxtNomorRequest" Name="mrId" 
          PropertyName="Text" Type="String" DefaultValue="0" />
      </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CekHargaDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataCekHargaByTypeUnitKerjaBarangKodeSupplierId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter">
      <SelectParameters>
        <asp:Parameter DefaultValue="NP" Name="type" Type="String" />
        <asp:SessionParameter DefaultValue="" Name="unitkerja" SessionField="UnitKerja" 
          Type="String" />
        <asp:ControlParameter ControlID="HdKodeBarang" DefaultValue="0" 
          Name="barangKode" PropertyName="Value" Type="String" />
        <asp:ControlParameter ControlID="HdIdSupplier" DefaultValue="0" 
          Name="supplierId" PropertyName="Value" Type="Int32" />
      </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
