<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Laporan.aspx.cs" Inherits="ManagementSystem.Purchasing.Laporan" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Laporan | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="padding: 10px">
  <div style="border: medium solid #CCCCCC; background-color: White; padding: 15px">
    <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; height: 61px; letter-spacing: 0px;">
      <img src="../images/icons/icons8-receipt-48.png" alt="icons8-time-card-48.png" />&nbsp;Laporan<br />
    </div>
    <asp:Panel ID="PnlWarning" runat="server" HorizontalAlign="Center">
      <asp:Label ID="Label3" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="PnlLaporan" runat="server" Visible="false">
      <div style="padding-bottom: 15px;">
        <asp:Panel ID="PnlBarang" runat="server" GroupingText="Cetak Laporan">
          <div style="padding:5px">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
              <ContentTemplate>
                <table style="width: 100%;">
                <tr>
                  <td class="tableFieldHeader">Pilih Laporan</td>
                  <td class="tableFieldSeparator">:</td>
                  <td class="tableFieldValue">
                    <asp:Panel ID="Panel1" runat="server">
                      <asp:RadioButton ID="PnlBarangRbBg" runat="server" Text="Barang" 
                      GroupName="rb0" AutoPostBack="true" 
                      oncheckedchanged="PnlBarangRbBg_CheckedChanged" />&ensp;
                      <asp:RadioButton ID="PnlBarangRbJs" runat="server" Text="Jasa" GroupName="rb0" 
                        AutoPostBack="true" oncheckedchanged="PnlBarangRbJs_CheckedChanged" />
                    </asp:Panel>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader"></td>
                  <td class="tableFieldSeparator"></td>
                  <td class="tableFieldValue">                  
                    <asp:Panel ID="Pnl2" runat="server" Visible="false">
                      <asp:RadioButton ID="PnlBarangRbMR" runat="server" Text="MR" GroupName="rb1" 
                        oncheckedchanged="PnlBarangRbMR_CheckedChanged" AutoPostBack="true" />&ensp;
                      <asp:RadioButton ID="PnlBarangRbPO" runat="server" Text="PO" GroupName="rb1" 
                        oncheckedchanged="PnlBarangRbPO_CheckedChanged" AutoPostBack="true" />
                      <asp:RadioButton ID="PnlBarangRbWR" runat="server"  Text="WR" GroupName="rb1" 
                        oncheckedchanged="PnlBarangRbWR_CheckedChanged" AutoPostBack="true" />&ensp;
                      <asp:RadioButton ID="PnlBarangRbWO" runat="server" Text="WO" GroupName="rb1" 
                        oncheckedchanged="PnlBarangRbWO_CheckedChanged" AutoPostBack="true" />
                    </asp:Panel>
                  </td>
                </tr>                            
                <tr>
                  <td></td>
                  <td></td>
                  <td class="tableFieldValue">
                    <asp:Panel ID="Pnl3" runat="server" Visible="false">
                      <asp:RadioButton ID="PnlBarangRbP" runat="server" Text="Project" 
                        GroupName="rb2" oncheckedchanged="PnlBarangRbP_CheckedChanged" AutoPostBack="true" />&ensp;
                      <asp:RadioButton ID="PnlBarangRbNP" runat="server" Text="Non Project" 
                        GroupName="rb2" oncheckedchanged="PnlBarangRbNP_CheckedChanged" AutoPostBack="true" />
                    </asp:Panel>
                  </td>
                </tr>                            
                <tr>
                  <td></td>
                  <td></td>
                  <td class="tableFieldValue">
                    <asp:DropDownList ID="PnlBarangDlist1" runat="server" Visible="False" 
                      AutoPostBack="True" 
                      onselectedindexchanged="PnlBarangDlist1_SelectedIndexChanged" >
                      <asp:ListItem Value="0">-</asp:ListItem>
                      <asp:ListItem Value="1">Per Tanggal</asp:ListItem>                               
                      <asp:ListItem Value="2">Per Barang</asp:ListItem>                               
                      <asp:ListItem Value="3">Per Project</asp:ListItem>
                      <asp:ListItem Value="4">Per Departement</asp:ListItem>
                      <asp:ListItem Value="5">Per Supplier</asp:ListItem>
                      <asp:ListItem Value="6">Per Jasa</asp:ListItem>
                      <asp:ListItem Value="7">Rekap Supplier</asp:ListItem>
                      <asp:ListItem Value="8">Rekap Barang</asp:ListItem>
                      <%--<asp:ListItem Value="9">Rekap Departement</asp:ListItem>--%>
                      <asp:ListItem Value="10">Rekap Jasa</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>             
                <asp:Panel ID="PnlBarangPnlTgl" runat="server" Visible="false">
                <tr>
                  <td></td>
                  <td></td>
                  <td class="tableFieldValue">
                    <table>
                      <tr>
                        <td>
                          <telerik:RadDatePicker ID="PnlBarangTgl1" Runat="server">
                          </telerik:RadDatePicker>
                        </td>
                        <td>s/d</td>
                        <td>
                          <telerik:RadDatePicker ID="PnlBarangTgl2" Runat="server">
                          </telerik:RadDatePicker>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                </asp:Panel>
                <asp:Panel ID="PnlBarangPnlNama" runat="server" Visible="false">
                <tr>
                  <td></td>
                  <td></td>
                  <td class="tableFieldValue">
                    <table>
                      <tr>
                        <td>
                          <asp:HiddenField ID="PnlBarangKode" runat="server" />
                          <asp:TextBox ID="PnlBarangTxtNama" runat="server" 
                            CssClass="textbox_default"></asp:TextBox>
                        </td>
                        <td>
                          <asp:Button ID="PnlBarangBtnBrowse" runat="server" Text="..." 
                            onclick="PnlBarangBtnBrowse_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                </asp:Panel>
              </table>
              </ContentTemplate>
            </asp:UpdatePanel>
            <div class="tableFieldButton">
              <asp:Button ID="PnlBarangPrint" runat="server" Text="Print" onclick="PnlBarangPrint_Click" />
              <asp:Button ID="PnlBtnCancel" runat="server" Text="Cancel" 
                onclick="PnlBtnCancel_Click" />
            </div>
          </div>
        </asp:Panel>
      </div>
    </asp:Panel>    
  </div>
</div>
  <asp:ObjectDataSource ID="MasterBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_barang01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="MasterJasaDataSource" runat="server" 
    DeleteMethod="Delete" InsertMethod="Insert" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_jasaTableAdapter" 
    UpdateMethod="Update">
    <DeleteParameters>
      <asp:Parameter Name="Original_kode" Type="String" />
      <asp:Parameter Name="Original_unitkerja" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="unitkerja" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="Original_kode" Type="String" />
      <asp:Parameter Name="Original_unitkerja" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="kode" Type="String" />
      <asp:Parameter Name="unitkerja" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="SupplierDataSource" runat="server" 
    DeleteMethod="Delete" InsertMethod="Insert" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_supplierTableAdapter" 
    UpdateMethod="Update">
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
  <asp:ObjectDataSource ID="DepartementDataSource" runat="server" 
    DeleteMethod="Delete" InsertMethod="Insert" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.HrdDataSetTableAdapters.hrd_departemenTableAdapter" 
    UpdateMethod="Update">
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="UInt16" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="Original_id" Type="UInt16" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="nama" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  
  <asp:ObjectDataSource ID="ProjectDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_project01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <%--Departement--%>
  <asp:UpdatePanel ID="PnlListBarangModal" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlListBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlListBarangLinkButton" CancelControlID="PnlListBarangBtnClose" DropShadow="false" PopupControlID="PnlListBarang" PopupDragHandleControlID="PnlListBarangTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlListBarang" runat="server" Width="70%" CssClass="modalPopup" >
          <asp:Panel ID="PnlPnlListBarangTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/icons8-bill-48.png" />
                  </td>
                  <td>
                    <asp:Label ID="Label13" runat="server" Text="List Barang" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:5px">
            <table>
              <tr>
                <td class="tableFieldHeader"><b>Kata Kunci</b></td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader">
                  <asp:TextBox ID="TxtKataKunci" runat="server"  CssClass="textbox_default" Width="150px"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Cari Berdasarkan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:DropDownList ID="DlistJenisBarang" runat="server" Height="23px" Width="150px">
                    <asp:ListItem Value="nama">Nama</asp:ListItem>
                    <asp:ListItem Value="kode">Kode Barang</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td colspan="3" style=" padding-top:10px">
                  <asp:Button ID="BtnCariDataBarang" runat="server" Text="Cari" onclick="BtnCariDataBarang_Click" />&ensp;
                  <asp:Button ID="BtnClearDataBarang" runat="server" Text="Batal" onclick="BtnClearDataBarang_Click" />
                </td>
              </tr>
            </table><br />
            <telerik:RadGrid ID="GridListBarang" runat="server" AllowPaging="True" 
              AllowSorting="True" GridLines="None" DataSourceID="MasterBarangDataSource" 
              onitemcommand="GridListBarang_ItemCommand" 
              onpageindexchanged="GridListBarang_PageIndexChanged" 
              onpagesizechanged="GridListBarang_PageSizeChanged">
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="kode" 
                DataSourceID="MasterBarangDataSource">
                <Columns>
                  <telerik:GridBoundColumn DataField="kode" HeaderText="Kode Barang" 
                    ReadOnly="True" SortExpression="kode" UniqueName="kode" 
                    ItemStyle-HorizontalAlign="Center" >                    
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Barang" SortExpression="nama" UniqueName="nama" />                    
                  <telerik:GridBoundColumn DataField="barangkategori_nama" HeaderText="Kategori" 
                    SortExpression="barangkategori_nama" UniqueName="barangkategori_nama" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                    SortExpression="unitkerja" UniqueName="unitkerja" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                </Columns>
              </MasterTableView>
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid>
          </div>          
          <div style="text-align: center; padding-top: 10px">
            <asp:LinkButton ID="PnlListBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
            <asp:Button ID="PnlListBarangBtnClose" runat="server" Text="Close" /><br />
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <%--Departement--%>
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseMasterJasaModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseMasterJasaLinkButton" DropShadow="false" PopupControlID="PnlBrowseMasterJasa" PopupDragHandleControlID="PnlBrowseMasterJasaTitlebar" CancelControlID="PnlBrowseMasterJasaLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseMasterJasa" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseMasterJasaTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseMasterJasaLblTitlebar" runat="server" Text="Data Master Jasa" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <table>
            <tr>
              <td class="tableFieldHeader">Cari Jasa</td>
              <td class="tableFieldHeader">:</td>
              <td><asp:TextBox ID="PnlJasaTxtNmJasa" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 15px"></td>
              <td><asp:Button ID="PnlJasaBtnSearch" runat="server" Text="Cari" onclick="PnlJasaBtnSearch_Click" /></td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridMasterJasa" runat="server" AllowPaging="True" DataSourceID="MasterJasaDataSource" GridLines="None" onitemcommand="GridMasterJasa_ItemCommand" AllowSorting="True" onpageindexchanged="GridMasterJasa_PageIndexChanged" onpagesizechanged="GridMasterJasa_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="kode" DataSourceID="MasterJasaDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="kode" HeaderText="Kode Jasa" SortExpression="kode" UniqueName="kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Jasa" SortExpression="nama" UniqueName="nama" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseMasterJasaLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseMasterJasaBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <%--Master Project--%>
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
          <telerik:RadGrid ID="GridSupplier" runat="server" AllowPaging="True" 
            DataSourceID="SupplierDataSource" GridLines="None" 
            onitemcommand="GridSupplier_ItemCommand" AllowSorting="True" 
            onpageindexchanged="GridSupplier_PageIndexChanged" 
            onpagesizechanged="GridSupplier_PageSizeChanged">
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
  
  
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseDeptModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseDeptLinkButton" DropShadow="false" PopupControlID="PnlBrowseDept" PopupDragHandleControlID="PnlBrowseDeptTitlebar" CancelControlID="PnlBrowseDeptLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseDept" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseDeptTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="Label2" runat="server" Text="Departement" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <telerik:RadGrid ID="GridDepartement" runat="server" AllowPaging="True" 
            DataSourceID="DepartementDataSource" GridLines="None"  AllowSorting="True" 
            onitemcommand="GridDepartement_ItemCommand" 
            onpageindexchanged="GridDepartement_PageIndexChanged" 
            onpagesizechanged="GridDepartement_PageSizeChanged" >
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="DepartementDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="nama" HeaderText="Departement" SortExpression="nama" UniqueName="nama" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseDeptLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseDeptBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel8" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseMasterProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseMasterProjectLinkButton" DropShadow="false" PopupControlID="PnlBrowseMasterProject" PopupDragHandleControlID="PnlBrowseMasterProjectTitlebar" CancelControlID="PnlBrowseMasterProjectLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseMasterProject" runat="server" CssClass="modalPopup" Width="70%" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseMasterProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseMasterProjectLblTitlebar" runat="server" Text="Data Master Project" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <div>
            <table>
              <tr>
                <td class="tableFieldHeader">Kata Kunci</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="PnlMasterProjectTxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Kategori</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:DropDownList ID="PnlMasterProjectDlistSerachBy" runat="server">
                    <asp:ListItem Value="0">No Project</asp:ListItem>
                    <asp:ListItem Value="1">Customer</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td colspan="3" style="padding-top:10px">
                  <asp:Button ID="PnlMasterProjectBtnSearch" runat="server" Text="Search" onclick="PnlMasterProjectBtnSearch_Click" />&ensp;
                  <asp:Button ID="PnlMasterProjectBtnCancel" runat="server" Text="Cancel" onclick="PnlMasterProjectBtnCancel_Click" />
                </td>
              </tr>
            </table>
          </div><br />
          <telerik:RadGrid ID="GridMasterProject" runat="server" AllowPaging="True" 
            DataSourceID="ProjectDataSource" GridLines="None" 
            onitemcommand="GridMasterProject_ItemCommand" AllowSorting="True" 
            onpageindexchanged="GridMasterProject_PageIndexChanged" 
            onpagesizechanged="GridMasterProject_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,salescustomer_id" DataSourceID="ProjectDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" />
                <telerik:GridBoundColumn DataField="nomorproject" HeaderText="No Project" SortExpression="nomorproject" UniqueName="nomorproject"  ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="salescustomer_nama" HeaderText="Customer" SortExpression="salescustomer_nama" UniqueName="salescustomer_nama" />
                <telerik:GridTemplateColumn HeaderText="Tgl Project" ItemStyle-HorizontalAlign="Center" DataType="System.DateTime" >
                  <ItemTemplate>
                    <%# String.Format("{0:d}", DataBinder.Eval(Container.DataItem, "projectstart"))%> - <%# String.Format("{0:d}", DataBinder.Eval(Container.DataItem, "projectend"))%>
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Tgl Jaminan" ItemStyle-HorizontalAlign="Center" DataType="System.DateTime" >
                  <ItemTemplate>
                    <%# String.Format("{0:d}", DataBinder.Eval(Container.DataItem, "warrantystart"))%> - <%# String.Format("{0:d}", DataBinder.Eval(Container.DataItem, "warrantyend"))%>
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>            
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseMasterProjectLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseMasterProjectBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel> 
</asp:Content>
