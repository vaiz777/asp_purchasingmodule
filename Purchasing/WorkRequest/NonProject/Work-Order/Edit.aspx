<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Order.Edit" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Edit Work Order - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="/images/icons/icons8-compose-48.png" alt="icons8-compose-48.png"  />&ensp;<asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <asp:Panel ID="PnlInputPO1" runat="server" GroupingText="Input Item PO">
            <table>
              <tr>
                <td class="tableFieldHeader">No WO</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:HiddenField ID="Pnl1HdIdWO" runat="server" />
                  <asp:TextBox ID="Pnl1TxtNoWO" runat="server" CssClass="textbox_default" Enabled="False"></asp:TextBox>
                </td>
                <td style="width:20px"></td>
                <td class="tableFieldHeader">Jasa</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:HiddenField ID="Pnl1HdIdWRDetail" runat="server" />
                  <table>
                    <tr>
                      <td>
                        <div style="display: none" visible="false">
                          <asp:Label ID="Pnl1LblIdReqHarga" runat="server" Text=""></asp:Label>
                          <asp:Label ID="Pnl1LblIdReqJasa" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:TextBox ID="Pnl1TxtJasa" runat="server" CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                      </td>
                      <td><asp:Button ID="Pnl1BtnBrowseJasa" runat="server" Text="..." onclick="Pnl1BtnBrowseJasa_Click" /></td>
                    </tr>
                  </table>
                </td>
                <td style="width:20px"></td>
                <td class="tableFieldHeader">Diskon (%)</td>
                <td class="tableFieldHeader">:</td>
                <td><telerik:RadNumericTextBox ID="Pnl1RadTxtDiskon" runat="server" NumberFormat-DecimalDigits="2" /></td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Supplier / Vendor</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:HiddenField ID="Pnl1HdIdSupplier" runat="server" />
                        <asp:TextBox ID="Pnl1TxtSupplier" runat="server" CssClass="textbox_default" ReadOnly="True"></asp:TextBox></td>
                      <td><asp:Button ID="Pnl1BtnBrowseSupplier" runat="server" Text="..." onclick="Pnl1BtnBrowseSupplier_Click" /></td>
                    </tr>
                  </table>
                </td>
                <td style="width:20px"></td>
                <td class="tableFieldHeader">Jml Jasa</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="Pnl1TxtJmlJasa" runat="server" CssClass="textbox_default" Enabled="False" Width="50px"></asp:TextBox>
                  &ensp;<asp:TextBox ID="Pnl1TxtSatuan" runat="server" CssClass="textbox_default" Enabled="false" Width="50px"></asp:TextBox>
                </td>
                <td style="width:20px"></td>
                <td class="tableFieldHeader">Total</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:RadioButton ID="Pnl1RbItemJasa" runat="server" Text="Per Jasa" oncheckedchanged="Pnl1RbItemJasa_CheckedChanged" AutoPostBack="true" GroupName="rb" />&ensp;
                  <asp:RadioButton ID="Pnl1RbSatuan" runat="server" Text="Satuan" AutoPostBack="true" oncheckedchanged="Pnl1RbSatuan_CheckedChanged" GroupName="rb" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">No WR</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td><asp:TextBox ID="Pnl1TxtNoWR" runat="server" CssClass="textbox_default" ReadOnly="True"></asp:TextBox></td>
                      <td><asp:Button ID="Pnl1BtnBrowseWorkRequest" runat="server" Text="..." onclick="Pnl1BtnBrowseWorkRequest_Click" /></td>
                    </tr>
                  </table>
                </td>
                <td style="width:20px"></td>
                <td class="tableFieldHeader">Harga</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="Pnl1TxtCurrency" runat="server" CssClass="textbox_default" Enabled="false" Width="30px"></asp:TextBox>&ensp;
                  <telerik:RadNumericTextBox ID="Pnl1RadNmbrcTxtHarga" Runat="server" ReadOnly="True" Enabled="False" NumberFormat-DecimalDigits="2" />
                </td>
                <td style="width:20px"></td>
                <td class="tableFieldHeader"></td>
                <td class="tableFieldHeader"></td>
                <td><telerik:RadNumericTextBox ID="Pnl1RadNmrcTxtTotal" Runat="server" /></td>            
              </tr>
              <tr>
                <td></td>
                <td></td>
                <td></td>
                <td style="width:20px"></td>
                <asp:Panel ID="PnlKurs" runat="server" Visible="false">
                  <td class="tableFieldHeader">Kurs</td>
                  <td class="tableFieldHeader">:</td>
                  <td><telerik:RadNumericTextBox ID="Pnl1RadNmrcTxtKurs" Runat="server" /></td>
                </asp:Panel>
                <td style="width:20px"></td>
                <td></td>
                <td></td>
              </tr>              
            </table><br />
            <asp:Button ID="Pnl1BtnAdd" runat="server" Text="Tambah" onclick="Pnl1BtnAdd_Click" />&ensp;<br />
            <telerik:RadGrid ID="GridOrderDetail" runat="server" AllowPaging="True" 
              AllowSorting="True" DataSourceID="WODetailDataSource" GridLines="None" 
              onitemdatabound="GridOrderDetail_ItemDataBound" ShowFooter="True" 
              onitemcreated="GridOrderDetail_ItemCreated">
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="WODetailDataSource">
                <Columns>
                  <telerik:GridTemplateColumn UniqueName="CheckTemp">
                    <HeaderTemplate> 
                      <asp:CheckBox ID="CheckHeader"  AutoPostBack="true" runat="server" OnCheckedChanged="CheckHeaderCheckedChanged" /> 
                    </HeaderTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckRow" runat="server" />
                    </ItemTemplate>
                  </telerik:GridTemplateColumn>
                  <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" 
                    SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" 
                    DataType="System.Int64" Visible="false" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="wrdetail_id" HeaderText="wrdetail_id" SortExpression="wrdetail_id" UniqueName="wrdetail_id" Visible="false" DataType="System.Int64" />
                  <telerik:GridBoundColumn DataField="wr_id" HeaderText="No WR" 
                    SortExpression="wr_id" UniqueName="wr_id" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="jasa_kode" HeaderText="Kode Jasa" 
                    SortExpression="jasa_kode" UniqueName="jasa_kode" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama" />
                  <telerik:GridTemplateColumn HeaderText="Jml Jasa" SortExpression="jmljasa" UniqueName="jmljasa" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <%# string.Concat(DataBinder.Eval(Container.DataItem, "jmljasa"))%><%# string.Concat(DataBinder.Eval(Container.DataItem, "satuan"))%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridTemplateColumn>
                  <telerik:GridBoundColumn DataField="currency" HeaderText="" 
                    SortExpression="currency" UniqueName="currency" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" 
                    HeaderText="Harga" ItemStyle-HorizontalAlign="Center" SortExpression="harga" 
                    UniqueName="harga" DataFormatString="{0,20:N2}" >                  
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="diskon" DataType="System.Decimal" 
                    HeaderText="Diskon (%)" ItemStyle-HorizontalAlign="Center" 
                    SortExpression="diskon" UniqueName="diskon" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="totalharga" DataType="System.Decimal" 
                    HeaderText="Total" SortExpression="totalharga" UniqueName="totalharga" 
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                </Columns>
              </MasterTableView>
              <HeaderStyle HorizontalAlign="Center" />
              <ClientSettings >
                <Selecting AllowRowSelect="True" />
              </ClientSettings>
            </telerik:RadGrid>
          </asp:Panel>
        </ContentTemplate>
      </asp:UpdatePanel><br />
      <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
          <asp:Panel ID="PnlOrderNote" runat="server" GroupingText="Input Work Order">
            <div style="padding: 10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Tanggal WO</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <telerik:RadDatePicker ID="RDateTglWO" runat="server" />
                </td>
              </tr>
            </table><br />
            <asp:Panel ID="PnlBiaya" runat="server" GroupingText="Input Biaya">
              <div style="padding: 10px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">Diskon</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <table>
                        <tr>
                          <td>
                            <asp:DropDownList ID="PgBiayaDListDiskon" runat="server" AutoPostBack="True" onselectedindexchanged="PgBiayaDListDiskon_SelectedIndexChanged" >                              
                              <asp:ListItem>-</asp:ListItem>
                              <asp:ListItem>%</asp:ListItem>
                              <asp:ListItem>Nominal</asp:ListItem>                              
                            </asp:DropDownList>
                          </td>
                          <td>
                            <asp:HiddenField ID="PgBiayaHdDiskonVal" runat="server" />
                            <telerik:RadNumericTextBox ID="PgBiayaRadTxtDiskon" runat="server" Visible="False" NumberFormat-DecimalDigits="2" />
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">PPN</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:DropDownList ID="PgBiayaDListPPN" runat="server" >
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem Value="10">10%</asp:ListItem>
                      </asp:DropDownList>
                      <asp:HiddenField ID="PgBiayaHdPPNVal" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">PPh</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <telerik:RadNumericTextBox ID="PgBiayaRadTxtPPh" runat="server" Width="30px" NumberFormat-DecimalDigits="2" />
                      <asp:Label ID="PgBiayaLblPrsen" runat="server" Text="%" ></asp:Label>
                      <asp:HiddenField ID="PgBiayaHdPPhVal" runat="server" />
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Biaya Lain</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:TextBox ID="PgBiayaTxtLain" runat="server" CssClass="textbox_default" ></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Harga Biaya Lain</td>
                    <td class="tableFieldHeader">:</td>
                    <td><telerik:RadNumericTextBox ID="PgBiayaRadNmrcTxtBiayaLain" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Total Harga</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <table>
                        <tr>
                          <td>
                            <telerik:RadNumericTextBox ID="PgBiayaRAdTxtTotal" runat="server" NumberFormat-DecimalDigits="2" />
                          </td>
                          <td><asp:Button ID="PgBiayaBtnHitungNota" runat="server" Text="Hitung" onclick="PgBiayaBtnHitungNota_Click" /></td>
                        </tr>
                      </table>
                     </td>
                  </tr>
                </table><br />
            </asp:Panel><br />
            <asp:Panel ID="PnlInfoLain" runat="server" GroupingText="Info Lain">
              <div style="padding: 10px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">Jenis Pembayaran</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:TextBox ID="PgLainTxtPembayaran" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Tanggal Penyelesaian</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:TextBox ID="PgLainTxtTglPenyelesaian" runat="server" CssClass="textbox_default"></asp:TextBox>
                      <act:calendarextender ID="PgLainTxtTglPenyelesaianCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PgLainTxtTglPenyelesaian" />
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Notes</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:TextBox ID="PgLainTxtNotes" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                  </tr>
                  <asp:Panel ID="PnlKeterangan" runat="server" Visible="false">
                    <tr>
                      <td class="tableFieldHeader">Keterangan</td>
                      <td></td>
                      <td></td>
                    </tr>
                    <tr>
                      <td colspan="3"><asp:TextBox ID="PgLainTxtKet" runat="server" Height="150px" TextMode="MultiLine" Width="300px" CssClass="textbox_default"></asp:TextBox></td>
                    </tr>
                  </asp:Panel>
                </table><br />
              </div>
            </asp:Panel>
            </div>
          </asp:Panel>
          <div>
            <asp:Button ID="BtnSave" runat="server" Text="Edit Data" onclick="BtnSave_Click" />&ensp;
            <asp:Button ID="BtnCancel" runat="server" Text="Batal" onclick="BtnCancel_Click" />
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  
  <asp:ObjectDataSource ID="SupplierDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_supplierTableAdapter" />
  <asp:ObjectDataSource ID="WRDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByTypeSupplierIdUnitKerja" 
    
    
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wr02TableAdapter">
    <SelectParameters>
      <asp:Parameter DefaultValue="NP" Name="type" Type="String" />
      <asp:ControlParameter ControlID="Pnl1HdIdSupplier" Name="supplierId" 
        PropertyName="Value" Type="Int64" DefaultValue="0" />
      <asp:SessionParameter DefaultValue="" Name="unitkerja" SessionField="UnitKerja" 
        Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="WRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByWrIdStatus" 
    
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="Pnl1TxtNoWR" DefaultValue="0" Name="wrId" 
        PropertyName="Text" Type="String" />
      <asp:Parameter DefaultValue="J4" Name="status" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="WODetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByWoId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wodetail01TableAdapter">
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="0" Name="woId" QueryStringField="pId" 
        Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
    
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                    <asp:Label ID="Label1" runat="server" Text="Supplier" />
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
              <td><asp:TextBox ID="PnlSupplierTxtCariNama" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 15px"></td>
              <td><asp:Button ID="PnlSupplierBtnSearch" runat="server" Text="Cari" onclick="PnlSupplierBtnSearch_Click" /></td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridSupplier" runat="server" AllowPaging="True" DataSourceID="SupplierDataSource" GridLines="None" onitemcommand="GridSupplier_ItemCommand" onpageindexchanged="GridSupplier_PageIndexChanged" onpagesizechanged="GridSupplier_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SupplierDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ReadOnly="True" SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" DataType="System.Int32" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Supplier" SortExpression="nama" UniqueName="nama" />
                <telerik:GridBoundColumn DataField="alamat" HeaderText="Alamat" SortExpression="alamat" UniqueName="alamat" />
                <telerik:GridBoundColumn DataField="kota" HeaderText="Kota" SortExpression="kota" UniqueName="kota" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="notelpkantor" HeaderText="Telp. Kantor" SortExpression="notelpkantor" UniqueName="notelpkantor" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="kontakperson" HeaderText="CP" SortExpression="kontakperson" UniqueName="kontakperson" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="npwp" HeaderText="NPWP" SortExpression="npwp" UniqueName="npwp" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="jenisusaha" HeaderText="Jenis Usaha" SortExpression="jenisusaha" UniqueName="jenisusaha" ItemStyle-HorizontalAlign="Center" Visible="false" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings   EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseSupplierLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseSupplierBtnClose" runat="server" Text="Tutup"/><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>  
  
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
 
  
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseWorkRequestModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseWorkRequestLinkButton" DropShadow="false" PopupControlID="PnlBrowseWorkRequest" PopupDragHandleControlID="PnlBrowseWorkRequestTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseWorkRequest" runat="server" Width="60%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseWorkRequestTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="Label2" runat="server" Text="Work Request" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px;"> 
          <table>
            <tr>
              <td class="tableFieldHeader">Cari No WR</td>
              <td class="tableFieldHeader">:</td>
              <td class="tableFieldHeader"><asp:TextBox ID="PnlWorkRequestTxtSearch" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 15px"></td>
              <td><asp:Button ID="PnlWorkRequestBtnSearch" runat="server" Text="Search" onclick="PnlWorkRequestBtnSearch_Click" /></td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridWorkRequest" runat="server" AllowPaging="True" 
            AllowSorting="True" DataSourceID="WRDataSource" GridLines="None" 
            onitemcommand="GridWorkRequest_ItemCommand" 
            onpageindexchanged="GridWorkRequest_PageIndexChanged" 
            onpagesizechanged="GridWorkRequest_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataSourceID="WRDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="No WR" SortExpression="id" UniqueName="id" ReadOnly="True" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="reference" HeaderText="Reference" SortExpression="reference" UniqueName="reference" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tglwr" HeaderText="Tgl WR" SortExpression="tglwr" UniqueName="tglwr" DataType="System.DateTime" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="departement" HeaderText="Departement" SortExpression="departement" UniqueName="departement" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="lokasi_nama" HeaderText="Lokasi" SortExpression="lokasi_nama" UniqueName="lokasi_nama" ItemStyle-HorizontalAlign="Center" />                
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>   
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseWorkRequestLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseWorkRequestBtnClose" runat="server" Text="Close"/><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>  
  
  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseJasaModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseJasaLinkButton" DropShadow="false" PopupControlID="PnlBrowseJasa" PopupDragHandleControlID="PnlBrowseJasaTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseJasa" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseJasaTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="Label3" runat="server" Text="Browse Jasa" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px;">
          <telerik:RadGrid ID="GridJasa" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="WRDetailDataSource" GridLines="None" onitemcommand="GridJasa_ItemCommand" onpageindexchanged="GridJasa_PageIndexChanged" onpagesizechanged="GridJasa_PageSizeChanged" onitemdatabound="GridJasa_ItemDataBound">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="WRDetailDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id"  DataType="System.Int64" Visible="false" />               
                <telerik:GridBoundColumn DataField="jasa_kode" HeaderText="Kode Jasa" SortExpression="jasa_kode" UniqueName="jasa_kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama" />
                <telerik:GridTemplateColumn HeaderText="Jml Jasa" SortExpression="jmljasa" UniqueName="jmljasa" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <%# string.Concat(DataBinder.Eval(Container.DataItem, "jmljasa"))%><%# string.Concat(DataBinder.Eval(Container.DataItem, "satuan"))%>
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="tanggal" HeaderText="Tgl" SortExpression="tanggal" UniqueName="tanggal" DataType="System.DateTime" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="keterangan" HeaderText="Ket" SortExpression="keterangan" UniqueName="keterangan" />
                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Nama Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" HeaderText="Harga" SortExpression="harga" UniqueName="harga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />                
                <telerik:GridBoundColumn DataField="createdby" HeaderText="Requestor" SortExpression="createdby" UniqueName="createdby" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings  EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid> 
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseJasaLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseJasaBtnClose" runat="server" Text="Close"/><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
