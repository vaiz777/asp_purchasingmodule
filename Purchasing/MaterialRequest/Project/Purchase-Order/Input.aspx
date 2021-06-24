<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Input.aspx.cs" Inherits="ManagementSystem.Purchasing.Material_Request.Project.Purchase_Order.Input" Title="Input PO" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Purchasing - Input Purchase Order | PT Tri Ratna Diesel Indonesia</title>
  </asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
  <div id="layar" style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px;" runat="server">
    <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
      <img src="../../../../images/icons/icons8-product-documents-48.png" alt="icons8-air-conditioner-48.png" />&nbsp;Input Purchase Order 
      (Project)
    </div><br />
    <div style="padding-bottom: 15px">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <%-- Profil --%>
          <asp:Panel ID="PnlKeteranganRequest" runat="server" GroupingText="Keterangan Purchase Order">
            <div style="padding: 5px">
             <table>
              <tr>
                <td class="tableFieldHeader">Tanggal PO</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:HiddenField ID="HdIdPO" runat="server" />
                  <telerik:RadDatePicker ID="TglPO" Runat="server" AutoPostBack="True" Culture="Indonesian (Indonesia)" Width="191px" MinDate="1980-08-29" >
                    <DateInput AutoPostBack="True" ></DateInput>
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                  </telerik:RadDatePicker>
                  <asp:RequiredFieldValidator ID="TglPOValidator" runat="server" ControlToValidate="TglPO" Display="None" ErrorMessage="<b>Tgl Purchase Order<b><br/>Wajib diisi." ValidationGroup="PO" />
                  <act:ValidatorCalloutExtender ID="TglPOCalloutExtender" runat="server" TargetControlID="TglPOValidator" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="tableFieldHeader">Harga</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <telerik:RadNumericTextBox ID="RadTxtHarga" Runat="server" Enabled="false">
                  </telerik:RadNumericTextBox>
                </td>
                <td class="tableFieldHeader">&nbsp;</td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Nomor Project</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:HiddenField ID="HdIdProject" runat="server" />
                  <asp:TextBox ID="TxtNomorProject" runat="server" Width="191px" CssClass="textbox_default" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                  <asp:Button ID="BtnBrowseProject" runat="server" onclick="BtnBrowseProject_Click" 
                    Text="...." />
                </td>
                <td>&nbsp;</td>
                <td class="tableFieldHeader">Currency</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:TextBox ID="TxtCurrency" runat="server" CssClass="textbox_default" Enabled="false"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Supplier</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:TextBox ID="TxtSupplier" runat="server" CssClass="textbox_default" Width="191px" ReadOnly="true"></asp:TextBox>
                  <asp:HiddenField ID="HdIdSupplier" runat="server" />
                </td>
                <td>
                  <asp:Button ID="BtnBrowseSupplier" runat="server" onclick="BtnBrowseSupplier_Click" 
                    Text="...." />
                </td>
                <td></td>
                <td class="tableFieldHeader">Diskon (%)</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <telerik:RadNumericTextBox ID="RadTxtDiskon" Runat="server">
                  </telerik:RadNumericTextBox>
                </td>
                <td></td>
              </tr>
              <tr>
                <td class="tableFieldHeader">No MR</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:TextBox ID="TxtNoMR" runat="server" Width="191px" CssClass="textbox_default"></asp:TextBox>
                </td>
                <td>
                  <asp:Button ID="BtnBrowseMR" runat="server" onclick="BtnBrowseMR_Click" 
                    Text="...." />
                </td>
                <td>&nbsp;</td>
                <td class="tableFieldHeader">SubTotal</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader">
                  <telerik:RadNumericTextBox ID="RadTxtSubTotal" Runat="server">
                  </telerik:RadNumericTextBox>
                </td>
                <td>
                  <asp:Button ID="BtnHitung" runat="server" onclick="BtnHitung_Click" Text="Hitung" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Nama Barang</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:TextBox ID="TxtNamaBarang" runat="server" Width="191px" Enabled="False" CssClass="textbox_default"></asp:TextBox>
                  <asp:HiddenField ID="HdIdMRDetail" runat="server" />
                </td>
                <td>
                  <asp:Button ID="BtnBrowseMRDetail" runat="server" Text="...." 
                    onclick="BtnBrowseMRDetail_Click" />
                </td>
                <td></td>
                <asp:Panel ID="PnlKurs" runat="server" Visible="false">
                  <td class="tableFieldHeader">Kurs</td>
                  <td class="tableFieldSeparator">:</td>
                  <td>
                    <telerik:RadNumericTextBox ID="RadTxtKurs" Runat="server">
                    </telerik:RadNumericTextBox>
                  </td>
                  <td class="tableFieldHeader">IDR</td>
                </asp:Panel>                
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah</td>
                <td class="tableFieldSeparator">:</td>
                <td class="tableFieldHeader" colspan="7">
                  <asp:TextBox ID="TxtJumlah" runat="server" Width="191px" Enabled="False" CssClass="textbox_default"></asp:TextBox>
                </td>
              </tr>                 
              <tr>
                <td class="tableFieldHeader">Satuan</td>
                <td class="tableFieldSeparator">:</td>
                <td class="tableFieldHeader" colspan="7">
                  <asp:TextBox ID="TxtSatuan" runat="server" Width="191px" Enabled="False" CssClass="textbox_default"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td colspan="9" class="tableFieldButton">
                  <asp:Button ID="BtnAdd" runat="server" Text="Add" onclick="BtnAdd_Click" />
                </td>
              </tr>
            </table>
          </div><br />
          <div>
            <asp:LinkButton ID="LinkBtnDelete" runat="server" Text="Delete" 
              onclick="Delete_Click"></asp:LinkButton>
          </div>          
          <telerik:RadGrid ID="GridPODetail" runat="server" GridLines="None" 
              DataSourceID="PODetailDataSource" AllowPaging="True" 
              onitemdatabound="GridPODetail_ItemDataBound">
            <MasterTableView AutoGenerateColumns="False" ShowFooter="true" DataKeyNames="id" DataSourceID="PODetailDataSource">
              <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckTemp">
                  <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2"  AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" /> 
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int64" Visible="false" />
                <telerik:GridBoundColumn DataField="mrdetail_id" HeaderText="mrdetail_id" SortExpression="mrdetail_id" UniqueName="mrdetail_id" DataType="System.Int64" Visible="false" />
                <telerik:GridBoundColumn DataField="mr_id" HeaderText="mr_id" SortExpression="mr_id" UniqueName="mr_id" Visible="false" />
                <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" UniqueName="barang_kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="jumlah" DataType="System.Int32" HeaderText="Jumlah" SortExpression="jumlah" UniqueName="jumlah" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" SortExpression="satuan_nama" UniqueName="satuan_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="supplier_nama" DataType="System.Int32" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" HeaderText="Harga" SortExpression="harga" UniqueName="harga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />              
                <telerik:GridBoundColumn DataField="diskon" DataType="System.Single" HeaderText="Diskon (%)" SortExpression="diskon" UniqueName="diskon" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="total" DataType="System.Decimal" HeaderText="Total" SortExpression="total" UniqueName="total" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
          </telerik:RadGrid>
        </asp:Panel><br />
        <asp:Panel ID="PnlNote" runat="server">
          <telerik:RadTabStrip ID="TabStripPO" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1" Width="100%" >
            <Tabs>
              <telerik:RadTab runat="server" Text="Biaya" Selected="True"></telerik:RadTab>
              <telerik:RadTab runat="server" Text="Pembayaran"></telerik:RadTab>
              <telerik:RadTab runat="server" Text="Penyerahan"></telerik:RadTab>
              <telerik:RadTab runat="server" Text="Lain" ></telerik:RadTab>
            </Tabs>
          </telerik:RadTabStrip>
          <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Width="100%" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
            <telerik:RadPageView ID="PgBiaya" runat="server">
              <div style="padding: 15px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">Diskon</td>
                    <td class="tableFieldSeparator">:</td>
                    <td>
                      <asp:DropDownList ID="PgPODlistDiskon" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="PgPODlistDiskon_SelectedIndexChanged" Width="150px">
                        <asp:ListItem>-</asp:ListItem>
                        <asp:ListItem Value="%">%</asp:ListItem>
                        <asp:ListItem Value="Manual">Manual</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                    <td>
                      <asp:HiddenField ID="PgPOHdDiskonVal" runat="server" />
                      <telerik:RadNumericTextBox ID="PgPOTxtDiskon" Runat="server" Visible="false">
                      </telerik:RadNumericTextBox>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Lain-lain</td>
                    <td class="tableFieldSeparator">:</td>
                    <td colspan="2"><asp:TextBox ID="PgPOTxtLain" CssClass="textbox_default" 
                        runat="server" Width="150px"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Biaya Lain</td>
                    <td class="tableFieldSeparator">:</td>
                    <td colspan="2">
                      <telerik:RadNumericTextBox ID="PgPOTxtBiayaLain" Runat="server">
                      </telerik:RadNumericTextBox>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Jenis Pembelian</td>
                    <td class="tableFieldSeparator">:</td>
                    <td colspan="2">
                      <asp:DropDownList ID="PgPODlistJenisBeli" runat="server" Width="150px">
                        <asp:ListItem Value="-"></asp:ListItem>
                        <asp:ListItem Value="LKL">Lokal</asp:ListItem>
                        <asp:ListItem Value="IMP">Import</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">PPN (%)</td>
                    <td class="tableFieldSeparator">:</td>
                    <td colspan="2">
                      <asp:HiddenField ID="PgPOHdPPNVal" runat="server" />
                      <telerik:RadNumericTextBox ID="PgPOTxtPPN" Runat="server">
                      </telerik:RadNumericTextBox>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Total</td>
                    <td class="tableFieldSeparator">:</td>
                    <td>
                      <telerik:RadNumericTextBox ID="PgPOTxtTotal" Runat="server">
                      </telerik:RadNumericTextBox>
                    </td>
                    <td>
                      <asp:Button ID="PgPOBtnCalculate" runat="server" onclick="BtnCalculate_Click" Text="Hitung" />&ensp;
                      </td>
                  </tr>
                </table><br />
                <div class="tableFieldButton">&#160;&nbsp;</div><br />
              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PgInfoPembayaran" runat="server" >
              <table>
                <tr>
                  <td class="tableFieldHeader">Pembayaran</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:DropDownList ID="PgPODlistPembayaran" runat="server" AutoPostBack="True" 
                      onselectedindexchanged="PgPODlistPembayaran_SelectedIndexChanged">
                      <asp:ListItem Value="Cash">Cash</asp:ListItem>
                      <asp:ListItem Value="Termin">Termin</asp:ListItem>
                      <asp:ListItem Value="Custom">Custom</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <asp:TextBox ID="PgPOTxtPembayaran" runat="server" CssClass="textbox_default" Visible="False" />
                    <asp:Label ID="LblHari" runat="server" Text="hari" Visible="False"></asp:Label>
                  </td>
                </tr>
              </table><br />
              <div class="tableFieldButton">&#160;&nbsp;</div><br />
            </telerik:RadPageView>
            <telerik:RadPageView ID="PgInfoPenyerahan" runat="server">
              <table>
                <tr>
                  <td class="tableFieldHeader">Lokasi Penerimaan</td>
                  <td class="tableFieldSeparator">:</td>
                  <td>
                    <asp:DropDownList ID="PgPODlistPenempatan" runat="server" Width="150px" 
                      AutoPostBack="True" DataSourceID="WarehouseDataSource" DataTextField="nama" 
                      DataValueField="id" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tanggal Penyerahan</td>
                  <td class="tableFieldSeparator">:</td>
                  <td >
                    <telerik:RadDatePicker ID="PgPOTglPenyelesaian" Runat="server" MinDate="1980-01-02">
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="PgPOTglPenyelesaianValidator" runat="server" ControlToValidate="PgPOTglPenyelesaian" Display="None" ErrorMessage="<b>Tgl Penyaerahan<b><br/>Wajib diisi." ValidationGroup="PO" />
                    <act:ValidatorCalloutExtender ID="PgPOTglPenyelesaianCalloutExtender" runat="server" TargetControlID="PgPOTglPenyelesaianValidator" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Catatan Tambahan (NOTE)</td>
                  <td class="tableFieldSeparator">:</td>
                  <td>
                    <asp:TextBox ID="PgPOTxtCatatan" runat="server" CssClass="textbox_default"  Width="300px" TextMode="MultiLine"></asp:TextBox>
                  </td>
                </tr>
              </table><br />
              <div class="tableFieldButton">&#160;&nbsp;</div><br />
            </telerik:RadPageView>
            <telerik:RadPageView ID="PgPONotes" runat="server">
              <table>
                <tr>
                  <td class="tableFieldHeader">Keterangan</td>
                  <td>&#160;</td>
                </tr>
                <tr>
                  <td colspan="2">
                    <asp:TextBox ID="PgPOTxtKeterangan" runat="server" CssClass="textbox_default" Height="85px" Width="202px" TextMode="MultiLine"></asp:TextBox>
                  </td>
                </tr>
              </table>
            </telerik:RadPageView>
          </telerik:RadMultiPage>
        </asp:Panel><br />
        <div style="padding-bottom:10px">
          <asp:Button ID="BtnSaveAll" runat="server" onclick="BtnSaveAll_Click" Text="Save All" />
          <asp:Button ID="BtnCancel" runat="server" onclick="BtnCancel_Click" Text="Cancel" />
          <asp:Button ID="BtnBack" runat="server" onclick="BtnBack_Click" Text="Back" />
        </div><br />
      </ContentTemplate>
    </asp:UpdatePanel>
    </div>
  </div>
</div>
<asp:ObjectDataSource ID="MRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByStatusMrId" 
    
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
  <SelectParameters>
    <asp:Parameter DefaultValue="B5" Name="status" Type="String" />
    <asp:ControlParameter ControlID="TxtNoMR" Name="mrId" 
      PropertyName="Text" Type="String" DefaultValue="0" />
  </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="MRDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByProjectIdSupplierId" 
    
        
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mr02TableAdapter">
  <SelectParameters>
    <asp:ControlParameter ControlID="HdIdProject" Name="projectid" 
          PropertyName="Value" Type="Int32" DefaultValue="0" />
    <asp:ControlParameter ControlID="HdIdSupplier" Name="supplierid" 
          PropertyName="Value" Type="Int32" DefaultValue="0" />
  </SelectParameters>
</asp:ObjectDataSource>
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
<asp:ObjectDataSource ID="MasterProjectDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_project01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
    
  <asp:ObjectDataSource ID="WarehouseDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_lokasigudangTableAdapter" 
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_kode" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
      <asp:Parameter Name="Original_unitkerja" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="kode" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="unitkerja" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_kode" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
      <asp:Parameter Name="Original_unitkerja" Type="String" />
    </UpdateParameters>
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
    <InsertParameters>
      <asp:Parameter Name="kode" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="unitkerja" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  
  <asp:ObjectDataSource ID="PODetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByPoId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_podetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="HdIdPO" 
          Name="poId" PropertyName="Value" Type="String" DefaultValue="0" />
    </SelectParameters>
  </asp:ObjectDataSource>

  <%--"Panel Barang"--%>
  <asp:UpdatePanel ID="PnlModalProject" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewProjectLinkButton" CancelControlID="PnlViewProjectBtnClose" DropShadow="false" PopupControlID="PnlViewProject" PopupDragHandleControlID="PnlViewProjectTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlViewProject" runat="server" Width="50%" CssClass="modalPopup" >
         <asp:Panel ID="PnlViewProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td>
                  <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                </td>
                <td>
                  <asp:Label ID="PnlViewProjectLblTitlebar" runat="server" Text="Daftar Project" />
                </td>
              </tr>
            </table>
          </div>
        </asp:Panel>
        <div style="padding:5px; text-align:left">
          <telerik:RadGrid ID="GridProject" runat="server" GridLines="None" onitemcommand="GridProject_ItemCommand" AllowPaging="True" AllowSorting="True" DataSourceID="MasterProjectDataSource">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="MasterProjectDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>          
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int32" Visible="false" />
                <telerik:GridBoundColumn DataField="nomorproject" HeaderText="No Project" SortExpression="nomorproject" UniqueName="nomorproject" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tanggal" HeaderText="Tgl Project" SortExpression="tanggal" UniqueName="tanggal" DataType="System.DateTime" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />            
                <telerik:GridBoundColumn DataField="salescustomer_nama" HeaderText="Customer" SortExpression="salescustomer_nama" UniqueName="salescustomer_nama" />            
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>        
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="true" />
            </ClientSettings>
          </telerik:RadGrid>
          <div style="text-align: center; padding-top: 10px">
            <asp:LinkButton ID="PnlViewProjectLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
            <asp:Button ID="PnlViewProjectBtnClose" runat="server" Text="Close" /><br />
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
   
  <%--"Panel Barang"--%>
  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewSupplierModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewSupplierLinkButton" CancelControlID="PnlViewSupplierBtnClose" DropShadow="false" PopupControlID="PnlViewSupplier" PopupDragHandleControlID="PnlViewSupplierTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlViewSupplier" runat="server" Width="80%" CssClass="modalPopup">
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
          <div style="padding:5px;">
            <table>
             <tr>
              <td class="tableFieldHeader">Kata Kunci</td>
              <td class="tableFieldSeparator">:</td>
              <td>
                <asp:TextBox ID="TxtCariSupplier" runat="server" CssClass="textbox_default" Width="110px"></asp:TextBox>
              </td>
              <td>              
                &ensp;<asp:Button ID="BtnCariSupplier" runat="server" onclick="BtnCariSupplier_Click" Text="Cari" />
              </td>
             </tr>
            </table><br />
            <telerik:RadGrid ID="GridSupplier" runat="server" AllowPaging="True" AllowSorting="True" GridLines="None" onitemcommand="GridSupplier_ItemCommand" DataSourceID="SupplierDataSource">
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
              <asp:Button ID="PnlViewSupplierBtnClose" runat="server" Text="Close" /><br />
            </div>
          </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <%--"Panel Barang"--%>
  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
   <ContentTemplate>
    <act:ModalPopupExtender ID="PnlRequestNoteModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlRequestNoteLinkButton" CancelControlID="PnlRequestNoteBtnClose" DropShadow="false" PopupControlID="PnlRequestNote" PopupDragHandleControlID="PnlRequestNoteTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlRequestNote" runat="server" Width="70%" CssClass="modalPopup" >
        <asp:Panel ID="PnlRequestNoteTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td>
                  <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                </td>
                <td>
                  &ensp;<asp:Label ID="Label6" runat="server" Text="Daftar Material Request" />
                </td>
              </tr>
            </table>
          </div>
        </asp:Panel>
        <div style="padding:5px; ">
          <telerik:RadGrid ID="GridMR" runat="server" GridLines="None" 
            onitemcommand="GridMR_ItemCommand" DataSourceID="MRDataSource" 
            onitemdatabound="GridMR_ItemDataBound" 
            AllowPaging="True">
            <MasterTableView AutoGenerateColumns="False" DataSourceID="MRDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="No MR" SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="reference" HeaderText="Reference" SortExpression="reference" UniqueName="reference" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tanggal" HeaderText="Tanggal MR" SortExpression="tanggal" UniqueName="tanggal" DataType="System.DateTime" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="project_nomor" HeaderText="No Project" SortExpression="project_nomor" UniqueName="project_nomor" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="kategori_nama" HeaderText="Kategori" SortExpression="kategori_nama" UniqueName="kategori_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="lokasi_nama" HeaderText="Lokasi" SortExpression="lokasi_nama" UniqueName="lokasi_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="mr_status" HeaderText="Status" SortExpression="mr_status" UniqueName="mr_status" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="createdby" HeaderText="Requestor" SortExpression="createdby" UniqueName="createdby" ItemStyle-HorizontalAlign="Center" />
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="true" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="text-align: center; padding-top: 10px">
          <asp:LinkButton ID="PnlRequestNoteLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
          <asp:Button ID="PnlRequestNoteBtnClose" runat="server" Text="Close"  /><br />
        </div>        
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>  

  <%--"Panel Barang"--%>
  <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup" >
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
        </asp:Panel>
   </ContentTemplate>
 </asp:UpdatePanel>
 
 
 <%--"Panel Barang"--%>
 <asp:UpdatePanel ID="UpdatePanel10" runat="server">
  <ContentTemplate>
    <act:ModalPopupExtender ID="PnlViewRequestBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewRequestBarangLinkButton" CancelControlID="PnlViewRequestBarangBtnClose" DropShadow="false" PopupControlID="PnlViewRequestBarang" PopupDragHandleControlID="PnlViewRequestBarangTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlViewRequestBarang" runat="server" Width="70%" CssClass="modalPopup" >
        <div>
          <asp:Panel ID="PnlViewRequestBarangTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                  </td>
                  <td>
                    &ensp;<asp:Label ID="Label15" runat="server" Text="Daftar Barang Per MR" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding: 5px">
            <telerik:RadGrid ID="GridDetailMR" runat="server" GridLines="None" 
              AllowPaging="True" AllowSorting="True" 
              onitemcommand="GridDetailMR_ItemCommand" 
              DataSourceID="MRDetailDataSource" >
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="MRDetailDataSource">
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
                  <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" 
                    SortExpression="barang_nama" UniqueName="barang_nama" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
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
                  <telerik:GridBoundColumn DataField="tglpemenuhan" HeaderText="Tgl Pemenuhan" 
                    SortExpression="tglpemenuhan" UniqueName="tglpemenuhan" 
                    DataType="System.DateTime" DataFormatString="{0:d}" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
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
                  <telerik:GridBoundColumn DataField="createdby" HeaderText="Requestor" SortExpression="createdby" UniqueName="createdby" />
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                  <Selecting AllowRowSelect="true" />
                </ClientSettings>
              </telerik:RadGrid>
            </div>
            <div style="text-align: center; padding-top: 10px">
              <asp:LinkButton ID="PnlViewRequestBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
              <asp:Button ID="PnlViewRequestBarangBtnClose" runat="server" Text="Close"  /><br />
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
     
</asp:Content>
