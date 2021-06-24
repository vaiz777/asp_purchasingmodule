<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="ManagementSystem.Purchasing.MaterialRequest.NonProject.Purchase_Order.Detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Detail Purchase Order (Project) - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px;">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large;">
        <img src="../../../../images/icons/icons8-gears-48.png" alt="icons8-vieww-48.png" />&nbsp;<asp:Label ID="LblJudul" runat="server" Text="Detail Purchase Order (PO)"></asp:Label><br />
      </div><br />
      <div style="text-align: right;" >
        <asp:Label ID="LblStatus" runat="server" Text="" style="font-size: larger; color: White; background-color: Maroon; padding: 4px;" ></asp:Label>
      </div>
      <table style=" font-size: ">
       <%-- <tr>
          <td class="tableFieldHeader">&nbsp;</td>
          <td class="tableFieldHeader">&nbsp;</td>
          <td>
            <asp:Label ID="LblNomer" runat="server" Visible="false"></asp:Label>
          </td>
        </tr>--%>
        <tr>
          <td class="tableFieldHeader">No PO</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblNoPO" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Tgl PO</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblTglPO" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Pembelian</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblPembelian" runat="server"></asp:Label>
          </td>
        </tr>    
        <tr>
          <td class="tableFieldHeader">Dibuat Oleh</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblCreatedBy" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Supplier</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblSupplier" runat="server"></asp:Label>
          </td>
        </tr>
         <tr>
          <td class="tableFieldHeader"></td>
          <td class="tableFieldHeader"></td>
          <td>
            <asp:Label ID="LblAlamatSupplier" runat="server"></asp:Label>                  
          </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">&nbsp;</td>
          <td class="tableFieldHeader">&nbsp;</td>
          <td>
            <asp:Label ID="LblKota" runat="server"></asp:Label>
          </td>
        </tr>
      </table><br />
      <telerik:RadGrid ID="GridOrderDetail" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="PODetailDataSource" GridLines="None" ShowFooter="True" onitemdatabound="GridOrderDetail_ItemDataBound">
        <MasterTableView AutoGenerateColumns="False" DataSourceID="PODetailDataSource" DataKeyNames="id">
          <Columns>
            <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" DataType="System.Int64" ReadOnly="True" Visible="false" />
            <telerik:GridBoundColumn DataField="mr_id" HeaderText="No MR" SortExpression="mr_id" UniqueName="mr_id" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" UniqueName="barang_kode" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="jumlah" DataType="System.Int32" HeaderText="Jumlah" SortExpression="jumlah" UniqueName="jumlah" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" SortExpression="satuan_nama" UniqueName="satuan_nama" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="tglpemenuhan" DataType="System.DateTime" HeaderText="Tgl Dibutuhkan" SortExpression="tglpemenuhan" UniqueName="tglpemenuhan" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
            <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" />
            <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" HeaderText="Harga" SortExpression="harga" UniqueName="harga" DataFormatString="{0,20:N2}" />
            <telerik:GridBoundColumn DataField="diskon" DataType="System.Single" HeaderText="Diskon (%)" SortExpression="diskon" UniqueName="diskon" ItemStyle-HorizontalAlign="Center" />
            <telerik:GridBoundColumn DataField="total" DataType="System.Decimal" HeaderText="Total" SortExpression="total" UniqueName="total" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
          </Columns>
          <HeaderStyle HorizontalAlign="Center" />
        </MasterTableView>
      </telerik:RadGrid><br /><br />
      <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="100%" SelectedIndex="0" MultiPageID="RadMultiPage1">
        <Tabs>
          <telerik:RadTab runat="server" Text="Biaya" Selected="True"></telerik:RadTab>
          <telerik:RadTab runat="server" Text="Pembayaran & Pelaksanaan"></telerik:RadTab>
          <telerik:RadTab runat="server" Text="Keterangan"></telerik:RadTab>
        </Tabs>
      </telerik:RadTabStrip>
      <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" BackColor="White" SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
          <div style="padding: 10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Diskon</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblCurrency3" runat="server" Visible="False"></asp:Label>&ensp;
                  <asp:Label ID="LblDiskon" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">PPN</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblPPn" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Biaya Lain</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblJasaLain" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Harga Biaya Lain</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblCurrency" runat="server"></asp:Label>
                     
                    <asp:Label ID="LblBiayaJasaLain" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Total PO</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblCurrency2" runat="server"></asp:Label>&ensp;
                  <asp:Label ID="LblTotalPO" runat="server"></asp:Label>
                </td>
              </tr>
            </table><br />
            <asp:Panel ID="PnlKurs" runat="server">
              <table>
                <tr>
                  <td style="width: 350%"></td>
                  <td style="width: 35%"></td>
                  <td style="width: 35%"></td>
                  <td class="tableFieldHeader">Kurs</td>
                  <td class="tableFieldHeader">:</td>
                  <td><asp:Label ID="LblKurs" runat="server" Text=""></asp:Label></td>
                </tr>
              </table>
            </asp:Panel>
          </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%">
          <div style="padding:10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Tgl Penyerahan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblTglPelaksanaan" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Pembayaran</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblPembayaran" runat="server" Text=""></asp:Label></td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Notes</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblNotes" runat="server" Text=""></asp:Label></td>
              </tr>
            </table>
          </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server" Width="100%">
          <div style="padding: 10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Keterangan</td>
                <td></td>
                <td></td>
              </tr>
            </table>
            <asp:Label ID="LblKeterangan" runat="server" Text=""></asp:Label>
          </div><br />
        </telerik:RadPageView>
      </telerik:RadMultiPage><br />
      <asp:Button ID="BtnBack" runat="server" Text="Kembali" onclick="BtnBack_Click" />
    </div>
  </div>
  <asp:ObjectDataSource ID="PODetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPoId"  
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_podetail01TableAdapter">
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="poid" QueryStringField="pId" 
        Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
