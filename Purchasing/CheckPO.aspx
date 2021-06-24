<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckPO.aspx.cs" Inherits="ManagementSystem.Purchasing.CheckPO" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Cek PO - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="../images/icons/icons8-agreement-48.png" alt="icons8-agreement-48.png"  />&ensp;Cek Purchase Order
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <%--<asp:Panel ID="PnlWarning" runat="server" HorizontalAlign="Center" >
            <asp:Label ID="Label1" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
          </asp:Panel>  --%>  
          <asp:Panel ID="PnlCheck" runat="server">
            <div style="padding:5px">
              <table style="width: 100%;">
                <tr>
                  <td class="tableFieldHeader">Pilih Tanggal</td>
                  <td class="tableFieldSeparator">:</td>
                  <td class="tableFieldValue">
                    <table>
                      <tr>
                        <td>
                          <telerik:RadDatePicker ID="RadDatePicker1" Runat="server" 
                            onselecteddatechanged="RadDatePicker1_SelectedDateChanged" 
                            AutoPostBack="True" Culture="Indonesian (Indonesia)">
                            <DateInput AutoPostBack="True">
                            </DateInput>
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                              ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                          </telerik:RadDatePicker>
                        </td>
                        <td>s/d</td>
                        <td>
                          <telerik:RadDatePicker ID="RadDatePicker2" Runat="server" 
                            onselecteddatechanged="RadDatePicker2_SelectedDateChanged" 
                            AutoPostBack="True" Culture="Indonesian (Indonesia)">
                            <DateInput AutoPostBack="True">
                            </DateInput>
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                              ViewSelectorText="x">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                          </telerik:RadDatePicker>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Unit Kerja</td>
                  <td class="tableFieldSeparator">:</td>
                  <td class="tableFieldValue">
                    <asp:DropDownList ID="DlistUnitKerja" runat="server" 
                      onselectedindexchanged="DlistUnitKerja_SelectedIndexChanged" AutoPostBack="true">
                      <asp:ListItem>SHIPYARD</asp:ListItem>
                      <asp:ListItem>ASSEMBLING</asp:ListItem>
                      <asp:ListItem>SPARE PARTS</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                
                <br />
                <tr>
                  <td></td>
                  <td></td>
                  <td>
                    <table>
                      <tr>
                        <td>                    
                          <asp:ListBox ID="ListBarang" runat="server" DataSourceID="ListBarangDataSource" 
                            DataTextField="barang_nama" DataValueField="barang_kode" AutoPostBack="True" 
                            Height="120px" onselectedindexchanged="ListBarang_SelectedIndexChanged" 
                            Width="175px" Visible="False"></asp:ListBox>
                        </td>
                        <td></td>
                        <td>
                          <telerik:RadGrid ID="GridPO" runat="server" AllowPaging="True" 
                            AllowSorting="True" DataSourceID="GridPODataSource" GridLines="None" 
                            Visible="False" onitemcommand="GridPO_ItemCommand" 
                            onitemdatabound="GridPO_ItemDataBound">
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="GridPODataSource">
                              <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                              </RowIndicatorColumn>
                              <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                              </ExpandCollapseColumn>
                              <Columns>
                                <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                                <telerik:GridBoundColumn DataField="nomerpo" HeaderText="Nomer" 
                                  SortExpression="nomerpo" UniqueName="nomerpo" 
                                  ItemStyle-HorizontalAlign="Center" >
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="tglpo" DataType="System.DateTime" 
                                  HeaderText="Tgl" SortExpression="tglpo" UniqueName="tglpo" 
                                  DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center">
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="type" HeaderText="Type" 
                                  SortExpression="type" UniqueName="type" ItemStyle-HorizontalAlign="Center" >
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Jumlah" ItemStyle-HorizontalAlign="Center">
                                  <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "jumlah")%><%# DataBinder.Eval(Container.DataItem, "satuan_nama")%>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" 
                                  SortExpression="supplier_nama" UniqueName="supplier_nama" 
                                  ItemStyle-HorizontalAlign="Center" >
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="currency" HeaderText="" 
                                  SortExpression="currency" UniqueName="currency" 
                                  ItemStyle-HorizontalAlign="Center" >
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="totalpo" DataType="System.Decimal" 
                                  HeaderText="Total" SortExpression="totalpo" UniqueName="totalpo" 
                                  DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" >
                                  <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" />
                              </Columns>
                              <HeaderStyle HorizontalAlign="Center" />
                            </MasterTableView>
                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                              <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                          </telerik:RadGrid>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
              <br />
            </div>
          </asp:Panel>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  <asp:ObjectDataSource ID="ListBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByUnitKerjaTglPo" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="DlistUnitKerja" DefaultValue="-" 
        Name="unitkerja" PropertyName="SelectedValue" Type="String" />
      <asp:ControlParameter ControlID="RadDatePicker1" DefaultValue="" Name="tglpo1" 
        PropertyName="SelectedDate" Type="DateTime" />
      <asp:ControlParameter ControlID="RadDatePicker2" Name="tglpo2" 
        PropertyName="SelectedDate" Type="DateTime" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="GridPODataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByUnitKerjaTglPoBarangKode" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="DlistUnitKerja" Name="unitkerja" 
        PropertyName="SelectedValue" Type="String" />
      <asp:ControlParameter ControlID="RadDatePicker1" DefaultValue="" Name="tglpo1" 
        PropertyName="SelectedDate" Type="DateTime" />
      <asp:ControlParameter ControlID="RadDatePicker2" DefaultValue="" Name="tglpo2" 
        PropertyName="SelectedDate" Type="DateTime" />
      <asp:ControlParameter ControlID="ListBarang" DefaultValue="" Name="barangKode" 
        PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>  
  
  <asp:ObjectDataSource ID="PODetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByPoId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_podetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="PnlDetailHdIdPO" DefaultValue="0" Name="poid" 
        PropertyName="Value" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlDetailPOModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlDetailPOLinkButton" DropShadow="false" PopupControlID="PnlDetailPO" PopupDragHandleControlID="PnlDetailPOTitlebar" CancelControlID="PnlDetailPOLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlDetailPO" runat="server" Width="70%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlDetailPOTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="../images/icons/icons8-view-48.png" alt="icons8-view-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlDetailPOLblTitlebar" runat="server" Text="Detail Purchase Order" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <telerik:RadTabStrip ID="RadTabStrip4" runat="server" SelectedIndex="0" 
            MultiPageID="RadMultiPage4" >
            <Tabs>
              <telerik:RadTab runat="server" Text="Detail PO" Selected="True" />
              <telerik:RadTab runat="server" Text="Biaya" />
              <telerik:RadTab runat="server" Text="Lain - lain">
              </telerik:RadTab>
            </Tabs>
          </telerik:RadTabStrip>
          <telerik:RadMultiPage ID="RadMultiPage4" runat="server" Width="100%" SelectedIndex="0" BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" BackColor="White">
            <telerik:RadPageView ID="RadPageView8" runat="server"  >
              <div style="padding:15px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">Nomer</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:HiddenField ID="PnlDetailHdIdPO" runat="server" />
                      <asp:Label ID="PnlDetailPOLblNomer" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Tanggal</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="PnlDetailPOLblTanggal" runat="server" /></td>
                  </tr>
                  <asp:Panel ID="PnlPOTipe" runat="server">
                  <tr>
                    <td class="tableFieldHeader">Tipe</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblTipe" runat="server" /></td>
                  </tr>
                  </asp:Panel>                  
                  <tr>
                    <td class="tableFieldHeader">Supplier</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblSupplier" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Jenis PO</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblJenis" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Pembayaran</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblPembayaran" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Tgl Penyerahan</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblTglPenyerahan" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Lokasi Penyerahan / Gudang</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblLokasiGudang" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Dibuat Oleh</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblDibuatOleh" runat="server" /></td>
                  </tr>
                </table>
              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView9" runat="server" >
              <div style="padding:15px">
                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                  AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                  GridLines="None" PageSize="5" Width="100%" DataSourceID="PODetailDataSource">
                  <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                  <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" Visible="false" />
                    <asp:BoundField DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" />
                    <asp:TemplateField HeaderText="Jumlah" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "jumlah")%> <%#DataBinder.Eval(Container.DataItem, "satuan_nama")%>
                      </ItemTemplate>                      
                    </asp:TemplateField>
                    <asp:BoundField DataField="harga" HeaderText="Harga" SortExpression="harga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="diskon" HeaderText="Diskon (%)" SortExpression="diskon" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="total" HeaderText="Subtotal" SortExpression="total" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
                  </Columns>
                  <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                  <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                  <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                  <EditRowStyle BackColor="#999999" />
                  <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <br />
                <table style="width: 100%; text-align: right; padding: 0.1px">
                  <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 15%"></td>
                    <td style="width: 15%"></td>
                    <td class="tableFieldHeader">Total Barang</td>
                    <td></td>
                    <td class="tableFieldHeader"><asp:Label ID="PnlDetailPOLblCurr1" runat="server" /></td>
                    <td class="tableFieldHeader"><asp:Label ID="PnlDetailPOLblTotalBarang" runat="server" /></td> 
                  </tr>
                  <asp:Panel ID="PnlDetailPOKurs" runat="server" >                  
                  <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 15%"></td>
                    <td style="width: 15%"></td>
                    <td class="tableFieldHeader">Kurs</td>
                    <td></td>
                    <td></td>
                    <td><asp:Label ID="PnlDetailPOLblKurs" runat="server" /></td>
                  </tr>
                  </asp:Panel>
                  <tr>
                    <td style="width: 15%"></td>
                    <td style="width: 15%"></td>
                    <td style="width: 15%"></td>
                    <td class="tableFieldHeader">Diskon</td>
                    <td><asp:Label ID="PnlDetailPOLblDiskon" runat="server" /></td>
                    <td><asp:Label ID="PnlDetailPOLblCurr2" runat="server" /></td>
                    <td><asp:Label ID="PnlDetailPOLblTotalDiskon" runat="server" /></td>
                  </tr>
                  <tr>
                    <td style="width: 15%"> </td>
                    <td style="width: 15%"> </td>
                    <td style="width: 15%"> </td>
                    <td class="tableFieldHeader">PPn</td>
                    <td><asp:Label ID="PnlDetailPOLblPPn" runat="server" /></td>
                    <td><asp:Label ID="PnlDetailPOLblCurr3" runat="server" /></td>
                    <td><asp:Label ID="PnlDetailPOLblTotalPPn" runat="server" /></td>
                  </tr>                 
                  <tr>
                    <td style="width: 15%"> </td>
                    <td style="width: 15%"> </td>
                    <td style="width: 15%"> </td>
                    <td class="tableFieldHeader">Jasa Lain</td>
                    <td> <asp:Label ID="PnlDetailPOLblJasaLain" runat="server" /></td>
                    <td><asp:Label ID="PnlDetailPOLblCurr5" runat="server" /></td>
                    <td> <asp:Label ID="PnlDetailPOLblBiayaJasaLain" runat="server" /></td>
                  </tr>
                  <tr>
                    <td style="width: 15%"> </td>
                    <td style="width: 15%"> </td>
                    <td style="width: 15%"> </td>
                    <td class="tableFieldHeader" style="background-color: Yellow">Total WO</td>
                    <td style="background-color: Yellow"></td>
                    <td class="tableFieldHeader" style="background-color: Yellow">
                      <asp:Label ID="PnlDetailPOLblCurr7" runat="server"></asp:Label>
                    </td>
                    <td class="tableFieldHeader" style="background-color: Yellow">
                      <asp:Label ID="PnlDetailPOLblTotalPO" runat="server" /></td>
                  </tr>
                </table>
              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView10" runat="server" >
              <div style="padding:15px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">Notes</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblNotes" runat="server" /></td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Keterangan</td>
                    <td class="tableFieldHeader">:</td>
                    <td><asp:Label ID="PnlDetailPOLblKeterangan" runat="server" /></td>
                  </tr>
                </table>
              </div>
            </telerik:RadPageView>
          </telerik:RadMultiPage>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlDetailPOLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlDetailPOBtnClose" runat="server" Text="Tutup" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
