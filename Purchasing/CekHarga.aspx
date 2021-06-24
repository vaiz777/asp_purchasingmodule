<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CekHarga.aspx.cs" Inherits="ManagementSystem.Purchasing.CekHarga" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Cek Harga - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="../images/icons/icons8-payment-history-48.png" alt="icons8-payment-history-48.png"  />&ensp;Cek Harga
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="PnlWarning" runat="server" HorizontalAlign="Center">
          <asp:Label ID="Label1" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PnlCekHarga" runat="server" Visible="false">
          <table>
            <tr>
              <td class="tableFieldHeader">Kata Kunci</td>
              <td class="tableFieldHeader">:</td>
              <td>
                <table>
                  <tr>
                    <td><asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                    <td><asp:Button ID="BtnBrowseBarang" runat="server" Text="..." onclick="BtnBrowseBarang_Click" /></td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td class="tableFieldHeader">Kategori</td>
              <td class="tableFieldHeader">:</td>
              <td>
                <asp:DropDownList ID="DListKategori" runat="server">
                  <asp:ListItem Value="0">Kode Barang</asp:ListItem>
                  <asp:ListItem Value="1">Barang</asp:ListItem>
                </asp:DropDownList>
              </td>
            </tr>
          </table><br />
          <asp:Button ID="BtnShow" runat="server" Text="Browse" onclick="BtnShow_Click" />&ensp;
          <asp:Button ID="BtnCancel" runat="server" Text="Cancel" onclick="BtnCancel_Click" />
          <br /><br />
          <telerik:RadGrid ID="GridCekHarga" runat="server" AllowPaging="True" 
            AllowSorting="True" DataSourceID="CekHargaDataSource" GridLines="None" 
            ShowGroupPanel="True" Visible="False">
            <MasterTableView AutoGenerateColumns="False" DataSourceID="CekHargaDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                <telerik:GridBoundColumn DataField="nomerpo" HeaderText="No PO" SortExpression="nomerpo" UniqueName="nomerpo" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" UniqueName="barang_kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                <telerik:GridTemplateColumn HeaderText="Jumlah" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "jumlah")%><%# DataBinder.Eval(Container.DataItem, "satuan_nama")%>
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" HeaderText="Harga" SortExpression="harga" UniqueName="harga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="diskon" DataType="System.Single" HeaderText="Diskon (%)" SortExpression="diskon" UniqueName="diskon" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="podetail_total" DataType="System.Decimal" HeaderText="Total" SortExpression="podetail_total" UniqueName="podetail_total" ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}" />
                <telerik:GridBoundColumn DataField="datecreated" HeaderText="Tgl Dibuat" SortExpression="datecreated" UniqueName="datecreated" ItemStyle-HorizontalAlign="Center" />
              </Columns>
              <GroupByExpressions>
              <telerik:GridGroupByExpression>
                <GroupByFields>
                  <telerik:GridGroupByField FieldName="supplier_nama" />
                </GroupByFields>
                <SelectFields>
                  <telerik:GridGroupByField FieldName="supplier_nama" HeaderText="Supplier  " />
                </SelectFields>
              </telerik:GridGroupByExpression>
            </GroupByExpressions>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />            
            <ClientSettings AllowDragToGroup="True">
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <GroupingSettings ShowUnGroupButton="True"  />
          </telerik:RadGrid>
        </asp:Panel>       
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  <asp:ObjectDataSource ID="CekHargaDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataCekHargaByStatusUnitKerja"     
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="MasterBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_barang01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseMasterBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseMasterBarangLinkButton" DropShadow="false" PopupControlID="PnlBrowseMasterBarang" PopupDragHandleControlID="PnlBrowseMasterBarangTitlebar" CancelControlID="PnlBrowseMasterBarangLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseMasterBarang" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseMasterBarangTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="../images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseMasterBarangLblTitlebar" runat="server" Text="Data Master Barang" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <table>
            <tr>
              <td class="tableFieldHeader">Cari Barang</td>
              <td class="tableFieldHeader">:</td>
              <td><asp:TextBox ID="PnlBarangTxtNmBarang" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 15px"></td>
              <td><asp:Button ID="PnlBarangBtnSearch" runat="server" Text="Cari" 
                  onclick="PnlBarangBtnSearch_Click" /></td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridMasterBarang" runat="server" AllowPaging="True" DataSourceID="MasterBarangDataSource" GridLines="None" AllowSorting="True" onitemcommand="GridMasterBarang_ItemCommand" onpageindexchanged="GridMasterBarang_PageIndexChanged" onpagesizechanged="GridMasterBarang_PageSizeChanged" >
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="kode" 
              DataSourceID="MasterBarangDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="kode" HeaderText="Kode" ReadOnly="True" SortExpression="kode" UniqueName="kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Barang" SortExpression="nama" UniqueName="nama" />
                <telerik:GridBoundColumn DataField="barangkategori_nama" HeaderText="Kategori" SortExpression="barangkategori_nama" UniqueName="barangkategori_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="keterangan" HeaderText="Ket" SortExpression="keterangan" UniqueName="keterangan" />                
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseMasterBarangLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseMasterBarangBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
