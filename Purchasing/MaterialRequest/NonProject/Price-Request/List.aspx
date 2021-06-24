<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.Material_Request.NonProject.Price_Request.List" Title="Price List" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>List Request Barang | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large;">
        <img src="../../../../images/icons/icons8-product-48.png" alt="icons8-time-card-48.png" />&nbsp;<asp:Label ID="LblTitle" runat="server" ></asp:Label>
      </div><br />
      <asp:Panel ID="PnlData1" runat="server" HorizontalAlign="Center">
        <asp:Label ID="Label5" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
      </asp:Panel>
      <asp:Panel ID="PnlData2" runat="server" Visible="false">
        <div style="padding-bottom: 15px;">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <table>
                <asp:Panel ID="PnlKataKunci" runat="server">
                <tr>
                  <td class="tableFieldHeader">Kata Kunci</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader" colspan="3">
                    <asp:TextBox ID="TxtKataKunci" runat="server"></asp:TextBox>
                  </td>
                </tr>
                </asp:Panel>
                <asp:Panel ID="PnlTanggal" runat="server" Visible="false">
                <tr>
                  <td class="tableFieldHeader">Input Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtTanggalStart" runat="server"></asp:TextBox>
                    <act:CalendarExtender ID="calendar1" runat="server"  Format="MM/dd/yyyy" Enabled="True" TargetControlID="TxtTanggalStart" />
                  </td>
                  <td class="tableFieldHeader">s/d</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtTanggalEnd" runat="server"></asp:TextBox>
                    <act:CalendarExtender ID="calendar2" runat="server"  Format="MM/dd/yyyy" Enabled="True" TargetControlID="TxtTanggalEnd" />
                  </td>
                </tr>
                </asp:Panel>
                <asp:Panel ID="PnlStatus" runat="server" Visible="false">
                <tr>
                  <td class="tableFieldHeader">Pilih Status</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader" colspan="3">
                    <asp:DropDownList ID="DlistStatus" runat="server">
                      <asp:ListItem Value="MR4">Outstanding by PPC</asp:ListItem>
                      <asp:ListItem Value="MR5">MR Disetujui</asp:ListItem>
                      <asp:ListItem Value="MR6">MR Ditolak</asp:ListItem>
                      <asp:ListItem Value="MR7">Outstanding by Manager/Spv/GM</asp:ListItem>
                      <asp:ListItem Value="HR1">Harga Didaftarkan</asp:ListItem>
                      <asp:ListItem Value="HR2">Harga Diverifikasi</asp:ListItem>
                      <asp:ListItem Value="HR4">Outstanding Harga by PPC</asp:ListItem>
                      <asp:ListItem Value="HR5">Harga Disetujui</asp:ListItem>
                      <asp:ListItem Value="HR6">Harga Ditolak</asp:ListItem>
                      <asp:ListItem Value="HR7">Outstanding Harga by Manager/Spv/GM</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                </asp:Panel>              
                <tr>
                  <td class="tableFieldHeader">Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlistKategoriPencarian" runat="server" AutoPostBack="True" onselectedindexchanged="DlistKategoriPencarian_SelectedIndexChanged">
                      <asp:ListItem Value="0">No MR</asp:ListItem>
                      <asp:ListItem Value="1">No Project</asp:ListItem>
                      <asp:ListItem Value="2">Tanggal</asp:ListItem>
                      <asp:ListItem Value="3">Status</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                  <td colspan="3" class="tableFieldButton">
                    <asp:Button ID="BtnSearch" runat="server" onclick="BtnSearchClick" Text="Search" />&ensp;
                    <asp:Button ID="BtnCancel" runat="server" onclick="BtnCancelClick" Text="Cancel" />
                  </td>
                </tr>
              </table>
            </ContentTemplate>
          </asp:UpdatePanel>
        </div><br />
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
          <ContentTemplate>
            <telerik:RadGrid ID="GridMR" runat="server" AllowPaging="True" 
              AllowSorting="True" GridLines="None" DataSourceID="MaterialRequestDataSource" 
              onitemcommand="GridMR_ItemCommand" onitemdatabound="GridMR_ItemDataBound">
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
                DataSourceID="MaterialRequestDataSource">
                <Columns>
                  <telerik:GridBoundColumn DataField="id" HeaderText="No MR" ReadOnly="True" 
                    SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-Font-Bold="true" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="reference" HeaderText="No Reference" 
                    SortExpression="reference" UniqueName="reference" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="tanggal" DataType="System.DateTime" HeaderText="Tgl MR" SortExpression="tanggal" UniqueName="tanggal" DataFormatString="{0:d}" />
                  <telerik:GridBoundColumn DataField="departement" HeaderText="Departement" SortExpression="departement" UniqueName="departement" ItemStyle-HorizontalAlign="Center" />
                  <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                    SortExpression="unitkerja" UniqueName="unitkerja" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                    SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridTemplateColumn HeaderText="Requestor"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <%#DataBinder.Eval(Container.DataItem, "createdby")%><br />
                      ( <%# DataBinder.Eval(Container.DataItem, "datecreated")%> )
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridTemplateColumn>
                  <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn">
                    <ItemTemplate>
                      <asp:Button ID="BtnInput" runat="server" Text="Input" CommandName="Input" Visible="false" />
                      <asp:Button ID="BtnCheck" runat="server" Text="Check" CommandName="Check" Visible="false" />
                      <asp:Button ID="BtnEdit" runat="server" Text="Edit Harga" CommandName="Edit" Visible="false" />
                      <asp:Button ID="BtnPrint" runat="server" Text="Print" CommandName="Print" Visible="false" />
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

  <asp:UpdatePanel ID="PnlPesan" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar"/>
      <asp:Panel ID="PnlMessage" runat="server" Width="50%" CssClass="modalPopup" Style="display: none">
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

  <%--"Panel Message"--%>
  <asp:UpdatePanel ID="PnlBarang" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewBarangLinkButton" CancelControlID="PnlViewBarangBtnClose" DropShadow="false" PopupControlID="PnlViewBarang" PopupDragHandleControlID="PnlViewBarangTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlViewBarang" runat="server" CssClass="modalPopup" Width="70%" >
          <asp:Panel ID="PnlViewBarangTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                  </td>
                  <td>
                    <asp:Label ID="PnlViewBarangLblTitlebar" runat="server" Text="Data Barang" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:10px;">
            <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1">
              <Tabs>
                <telerik:RadTab runat="server" Selected="True" Text="Material Request"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Barang"></telerik:RadTab>
              </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0" BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" BackColor="White">
              <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
                <div style="padding:10px">
                  <table>    
                    <tr>
                      <td class="tableFieldHeader">No MR</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblNoMR" runat="server" Text=""></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tableFieldHeader">Tgl MR</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblTglMR" runat="server" Text=""></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tableFieldHeader">Reference</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblReference" runat="server" Text=""></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tableFieldHeader">Departement</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblDepartement" runat="server"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tableFieldHeader">Lokasi</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblLokasi" runat="server"></asp:Label>
                      </td>
                    </tr>
                    <asp:Panel ID="PnlSM" runat="server">
                    <tr>
                      <td class="tableFieldHeader">Scope</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblScope" runat="server"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tableFieldHeader">Usable</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblUsable" runat="server"></asp:Label>
                      </td>
                    </tr>
                    </asp:Panel>                    
                    <tr>
                      <td class="tableFieldHeader">Dibuat Oleh</td>
                      <td class="tableFieldSeparator">:</td>
                      <td>
                        <asp:Label ID="LblCreatedby" runat="server"></asp:Label>
                      </td>
                    </tr>
                  </table>
                </div>
                <br />
              </telerik:RadPageView>
              <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%">
                <div style="padding:10px">
                  <telerik:RadGrid ID="GridBarang" runat="server" AllowPaging="True" AllowSorting="True" GridLines="None" DataSourceID="DetailReqBarangDataSource" onitemdatabound="GridBarang_ItemDataBound" onpageindexchanged="GridBarang_PageIndexChanged" onpagesizechanged="GridBarang_PageSizeChanged">
                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="DetailReqBarangDataSource">
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
                        <telerik:GridBoundColumn DataField="tglpemenuhan" DataType="System.DateTime" 
                          HeaderText="Tgl Pemenuhan" SortExpression="tglpemenuhan" 
                          UniqueName="tglpemenuhan" DataFormatString="{0:d}" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                          SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
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
                        <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" 
                          HeaderText="Harga" SortExpression="harga" UniqueName="harga" 
                          DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="createdby_nama" HeaderText="Requestor" 
                          SortExpression="createdby_nama" UniqueName="createdby_nama" 
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
                </div>
              </telerik:RadPageView>
            </telerik:RadMultiPage>
            <div style="text-align: center; padding-top: 10px">
              <asp:LinkButton ID="PnlViewBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;&nbsp;
              <asp:Button ID="PnlViewBarangBtnClose" runat="server" Text="Close" /><br />
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <%--"Panel Detail"--%>
  <asp:UpdatePanel ID="PnlHarga" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewHargaModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewHargaLinkButton" CancelControlID="PnlViewHargaBtnClose" DropShadow="false" PopupControlID="PnlViewHarga" PopupDragHandleControlID="PnlViewHargaTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlViewHarga" runat="server" CssClass="modalPopup" Width="75%">
          <asp:Panel ID="PnlViewHargaTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <img src="../../../../images/icons/icons8-bill-48.png" alt="icons8-brief-48.png" />
                  </td>
                  <td>
                    <asp:Label ID="Label1" runat="server" Text="Data Harga Per Barang" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel><br />
          <table>
            <tr>
              <td class="tableFieldHeader">No MR</td>
              <td class="tableFieldHeader">:</td>
              <td class="tableFieldHeader"><asp:TextBox ID="TxtNoteMR" runat="server" Enabled="true" ReadOnly="True" CssClass="textbox_default"></asp:TextBox></td>
              </tr>
          </table><br />
          <telerik:RadGrid ID="GridHarga" runat="server" AllowPaging="True" onitemdatabound="GridHarga_ItemDataBound" GridLines="None" AllowSorting="True" DataSourceID="CekReqBarangDataSource">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="CekReqBarangDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckTemp">
                  <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2"  AutoPostBack="true" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" /> 
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="id" DataType="System.Int64" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" />
                <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" UniqueName="barang_kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                <telerik:GridBoundColumn DataField="jumlah" DataType="System.Int32" HeaderText="Jumlah" SortExpression="jumlah" UniqueName="jumlah" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" SortExpression="satuan_nama" UniqueName="satuan_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tglpemenuhan" DataType="System.DateTime" HeaderText="Tgl Dibutuhkan" SortExpression="tglpemenuhan" UniqueName="tglpemenuhan" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="currency" HeaderText="" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="harga" DataType="System.Decimal" HeaderText="Harga" SortExpression="harga" UniqueName="harga"  DataFormatString="{0,20:N2}" />
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
          </MasterTableView>
          <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
            <Selecting AllowRowSelect="true" />
          </ClientSettings>
        </telerik:RadGrid>
        <div style="text-align: center; padding-top: 10px">
          <asp:LinkButton ID="PnlViewHargaLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton><asp:Button ID="PnlViewHargaBtnApprove" runat="server" Text="Setujui" onclick="PnlViewHargaBtnApproveClick" Visible="False" />
          <asp:Button ID="PnlViewHargaBtnVerified" runat="server" Text="Verifikasi" Visible="False" onclick="PnlViewHargaBtnVerified_Click" />
          <asp:Button ID="PnlViewHargaBtnClose" runat="server" Text="Tutup"/>
          <asp:Button ID="PnlViewHargaBtnReject" runat="server" Text="Tolak" onclick="PnlViewHargaBtnReject_Click" Visible="False"  /> 
          <asp:Button ID="PnlViewHargaBtnDelayedVerify" runat="server" Text="Tunda Verifikasi" onclick="PnlViewHargaBtnDelayedVerify_Click" Visible="False"  /> <br />
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>  
  
  <%--"Panel Check Harga"--%>
  <act:ModalPopupExtender ID="PnlKonfirmModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlKonfirmLinkButton" CancelControlID="PnlKonfirmPrintCancel" DropShadow="false" PopupControlID="PnlKonfirm" PopupDragHandleControlID="PnlKonfirmTitlebar"/> 
    <asp:Panel ID="PnlKonfirm" runat="server" Width="480px" CssClass="modalPopup" >
      <asp:Panel ID="PnlKonfirmTitleBar" runat="server" CssClass="modalPopupTitle">
        <div style="padding:5px; text-align:left">
          <asp:Label ID="PnlKonfirmLblTitleBar" runat="server" Text="Konfirmasi" />
        </div>
      </asp:Panel>
      <div style="padding:5px; text-align:left">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
          <ContentTemplate>
            <table>
              <tr>
                <td style="padding: 5px">
                  <asp:Image ID="PnlKonfirmImgIcon" runat="server" ImageUrl="~/images/icons/icons8-print-48.png" />
                </td>
                <td style="padding: 5px">
                  <asp:Label ID="PnlKonfirmLblMessage" runat="server" Text="Cetak nomor " />
                  <asp:Label ID="LblNote" runat="server"></asp:Label>
                  <asp:Label ID="Label4" runat="server" Text="?" />
                </td>
              </tr>
            </table>
          </ContentTemplate>
        </asp:UpdatePanel>        
        <div style="text-align: center; padding-top: 10px">
          <asp:Button ID="PnlKonfirmPrintOk" runat="server" Text="Print" onclick="PnlKonfirmPrintOk_Click" />
          <asp:Button ID="PnlKonfirmPrintCancel" runat="server" Text="Cancel"/>          
          <asp:LinkButton ID="PnlKonfirmLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
        </div>
      </div>
    </asp:Panel>

  <%--"Panel Print"--%>    
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBatalModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBatalLinkButton" CancelControlID="PnlBatalBtnClose" DropShadow="false" PopupControlID="PnlBatal" PopupDragHandleControlID="PnBatalTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlBatal" runat="server" Width="480px" CssClass="modalPopup" >
          <asp:Panel ID="PnBatalTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <asp:Label ID="PnBatalLblTitlebar" runat="server" Text="Konfirmasi" />
            </div>
          </asp:Panel>
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td style="padding: 5px">
                 <img src="~/images/icons/icons8-task-completed-48.png" alt="icons8-task-completed-48.png"  />
                </td>
                <td style="padding: 5px" valign="middle">
                  <asp:Label ID="Label2" runat="server" Text="Nomer "  />
                  <asp:Label ID="PnlBatalTxtNoPO" runat="server" />
                  <asp:Label ID="Label8" runat="server" Text=" sudah Disetujui." /><br />
                  <asp:Label ID="Label3" runat="server" Text="Apakah yakin ingin membatalkan?" />
                </td>
              </tr>
            </table>
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlBatalBtnOk" runat="server" Text="Oke"  onclick="PnlBatalBtnOk_Click"  />&ensp;&ensp;
              <asp:Button ID="PnlBatalBtnClose" runat="server" Text="Cancel"  />
              <asp:LinkButton ID="PnlBatalLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>  
  
  <%--"Panel Batal"--%>
  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlInputNoteModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlInputNoteLinkButton" CancelControlID="PnlInputNoteLinkButton" DropShadow="false" PopupControlID="PnlInputNote" PopupDragHandleControlID="PnlInputNoteTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlInputNote" runat="server" Width="480px" CssClass="modalPopup" Style="display: none">
        <div style="padding : 5px">
          <asp:Panel ID="PnlInputNoteTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:8px; text-align:left; ">
              <asp:Label ID="PnlInputNoteLblNomer" runat="server" />
            </div>
          </asp:Panel>
          <div style="padding:5px; text-align:left" >
            <table>
              <tr>                
                <td class="tableFieldHeader" style="padding: 5px">
                  Masukan sebab WR dibatalkan persetujuannya<br /><br />
                  <asp:TextBox ID="PnlInputNoteTxtKeterangan" runat="server" TextMode="MultiLine" 
                    Width="350px" CssClass="tableFieldHeader" placeholder="Input kerangan disini.." ></asp:TextBox>
                </td>
                <td style="width:30px">
                 <img src="~/images/icons/icons8-sign-up-48.png" alt="icons8-sign-up-48.png"  /> </td>
                <td>
                 <img src="~/images/icons/icons8-sign-up-48.png" alt="icons8-sign-up-48.png"  />
                </td>
              </tr>
            </table><br />
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlInputNoteBtnSubmit" runat="server" Text="Submit" onclick="PnlInputNoteBtnSubmit_Click"  />
              <asp:LinkButton ID="PnlInputNoteLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:ObjectDataSource ID="MaterialRequestDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByUnitKerjaType" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mr01TableAdapter" 
    oninit="NoteDataSource_Init">
    <SelectParameters>
      <asp:SessionParameter DefaultValue="0" Name="unitkerja" SessionField="UnitKerja" Type="String" />
      <asp:Parameter DefaultValue="NP" Name="type" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>    
  <asp:ObjectDataSource ID="DetailReqBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByMrId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="LblNoMR" Name="mrId" PropertyName="Text" 
        Type="String" DefaultValue="0" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="CekReqBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByMrId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="TxtNoteMR" Name="mrId" 
        PropertyName="Text" Type="String" DefaultValue="0" />
    </SelectParameters>
  </asp:ObjectDataSource>  
</asp:Content>