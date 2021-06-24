<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Input.aspx.cs" Inherits="ManagementSystem.Purchase.Material_Request.NonProject.Barang_Request.Input" Title="Input MR Non Project" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Purchasing - Input Request Barang | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
    <div id="layar" style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px;" runat="server">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
        <img src="../../../../images/icons/icons8-product-documents-48.png" alt="icons8-air-conditioner-48.png" /><asp:Label ID="LblJudul" runat="server" Text=""></asp:Label>
      </div><br />
      <div style="padding-bottom: 15px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
          <%-- Profil --%>
          <asp:Panel ID="PnlKeteranganBarang" runat="server" 
              GroupingText="Permintaan Barang">
            <div style="padding: 5px">
              <table>
                <tr>
                  <td class="tableFieldHeader">Nomor MR</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtNoMR" runat="server" Width="150px" CssClass="textbox_default" Enabled="false"></asp:TextBox>
                  </td>
                  <td class="tableFieldHeader"></td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <telerik:RadDatePicker ID="RadTglMR" Runat="server" AutoPostBack="True" Culture="Indonesian (Indonesia)" >
                    </telerik:RadDatePicker>
                  </td>
                  <td class="tableFieldHeader">
                    <table>
                      <tr>
                        <td>Generate Reference</td>
                        <td>
                          <asp:RadioButton ID="RbYa" runat="server" AutoPostBack="True" GroupName="pilihan" oncheckedchanged="BtnYa_CheckedChanged" Text="Ya" />
                          <asp:RadioButton ID="BtnTidak" runat="server" AutoPostBack="True" GroupName="pilihan" oncheckedchanged="BtnTidak_CheckedChanged1" Text="Tidak" /><br />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Department</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlistDepartemen" runat="server" Width="150px" 
                      DataSourceID="DepartmentDataSource" DataTextField="nama" DataValueField="nama">
                    </asp:DropDownList>
                  </td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtRefer" runat="server" Visible="False" Width="330px" CssClass="textbox_default"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Lokasi</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlistLokasi" runat="server" 
                      DataSourceID="LokasiDataSource" DataTextField="nama" DataValueField="id" 
                      Width="150px" />
                  </td>
                  <td class="tableFieldHeader">
                    <asp:Button ID="BtnRefresh" runat="server" onclick="BtnRefresh_Click" Text="Refresh" Visible="False"/>
                  </td>
                </tr>
                <asp:Panel ID="PnlSM" runat="server" Visible="false">
                <tr>
                  <td class="tableFieldHeader">Scope</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlistScope" runat="server" DataSourceID="ScopeDataSource" 
                      DataTextField="nama" DataValueField="id" Width="150px" />
                  </td>
                  <td class="tableFieldHeader"></td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Usable</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlistUsable" runat="server" 
                      DataSourceID="UsabilityDataSource" DataTextField="nama" 
                      DataValueField="id" Width="150px" />
                  </td>
                  <td class="tableFieldHeader"></td>
                </tr>
                </asp:Panel>
                <tr>
                  <td class="tableFieldHeader">Unit Kerja</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlistUnitKerja" runat="server">
                      <asp:ListItem>ASSEMBLING</asp:ListItem>
                      <asp:ListItem>SPARE PARTS</asp:ListItem>
                      <asp:ListItem>SHIPYARD</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                  <td class="tableFieldHeader"></td>
                </tr>
              </table><br />
              <div style="text-align: left">
                <asp:Button ID="BtnSave" runat="server" onclick="BtnSave_Click" Text="Save" />&ensp;
                <asp:Button ID="BtnCancel" runat="server" onclick="BtnCancel_Click" Text="Cancel" />
              </div>
            </div>
          </asp:Panel><br />
          
          <asp:Panel ID="PnlBarang" runat="server" GroupingText="Data Barang" >
            <div style="padding: 5px">
              <table>
                <tr>
                  <td class="tableFieldHeader">Nama Barang</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtNamaBarang" runat="server" Width="150px" CssClass="textbox_default"></asp:TextBox>
                    <asp:HiddenField ID="HdIdBarang" runat="server" />
                  </td>
                  <td class="tableFieldHeader">
                    <asp:Button ID="BtnCariBarang" runat="server" onclick="BtnCariBarang_Click" Text="..." />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jumlah Barang</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtJumlahBarang" runat="server" Width="150px" CssClass="textbox_default" ></asp:TextBox>
                  </td>
                  <td class="tableFieldHeader">&nbsp;</td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jenis Satuan</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:DropDownList ID="DlisJenisSatuan" runat="server" DataSourceID="SatuanDataSource" DataTextField="nama" DataValueField="id" Width="150px" />
                  </td>
                  <td class="tableFieldHeader">&nbsp;</td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Batas Pemenuhan</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <telerik:RadDatePicker ID="RDatePemenuhan" Runat="server" Width="150px" 
                      MinDate="1980-01-02" >
                      <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                        ViewSelectorText="x">
                      </Calendar>
                      <DatePopupButton HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                  </td>
                  <td class="tableFieldHeader">&nbsp;</td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Keterangan</td>
                  <td class="tableFieldHeader">:</td>
                  <td class="tableFieldHeader">
                    <asp:TextBox ID="TxtKeterangan" runat="server" CssClass="textbox_default" TextMode="MultiLine" Width="150px"></asp:TextBox>
                  </td>
                  <td class="tableFieldHeader">&nbsp;</td>
                </tr>
                <tr>
                  <td class="tableFieldButton" colspan="4">
                    <asp:Button ID="BtnAdd" runat="server" onclick="BtnAdd_Click" Text="Tambah" />&ensp;
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel><br />          
          <telerik:RadGrid ID="GridRequestBarang" runat="server" AllowPaging="True" 
              AllowSorting="True" GridLines="None" DataSourceID="MRDetailDataSource">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="MRDetailDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int64" Visible="false" />
                <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" UniqueName="barang_kode" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                <telerik:GridBoundColumn DataField="jumlah" DataType="System.Int32" HeaderText="Jumlah" SortExpression="jumlah" UniqueName="jumlah" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" SortExpression="satuan_nama" UniqueName="satuan_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tglpemenuhan" HeaderText="Tgl Pemenuhan" SortExpression="tglpemenuhan" UniqueName="tglpemenuhan" DataType="System.DateTime" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                <telerik:GridTemplateColumn HeaderText="Requestor" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "createdby")%><br />
                    ( <%# DataBinder.Eval(Container.DataItem, "datecreated")%> )
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
              </Columns>
            </MasterTableView>
            <ClientSettings>
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid><br />
          <asp:Button ID="TxtClose" runat="server" onclick="TxtClose_Click" Text="Tutup Permintaan" Width="118px"  />
            <br />
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>    
  </div>
  
  <asp:ObjectDataSource ID="ScopeDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataNonProject" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_scopeTableAdapter" 
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" >
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_type" Type="String" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="type" Type="String" />
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_type" Type="String" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="type" Type="String" />
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="UsabilityDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataBarangNonProject" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_usableTableAdapter" 
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" >
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_type" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="type" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_type" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="type" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="SatuanDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_satuanTableAdapter" 
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" >
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="nama" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="LokasiDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_lokasiTableAdapter" 
    DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" >
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
      <asp:Parameter Name="Original_unitkerja" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="unitkerja" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
      <asp:Parameter Name="Original_unitkerja" Type="String" />
    </UpdateParameters>
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
    <InsertParameters>
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="unitkerja" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="MasterBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_barang01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter ConvertEmptyStringToNull="False" Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="MRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByMrId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="TxtNoMR" DefaultValue="0" Name="mrId" 
        PropertyName="Text" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="DepartmentDataSource" runat="server" 
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
  <asp:UpdatePanel ID="PnlPesanModal" runat="server">
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="BtnSave" EventName="Click" />
    </Triggers>
    <ContentTemplate>
    <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup" Style="display: none">
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
            AllowSorting="True" GridLines="None" onitemcommand="GridListBarang_ItemCommand" 
            DataSourceID="MasterBarangDataSource" 
            onpageindexchanged="GridListBarang_PageIndexChanged" 
            onpagesizechanged="GridListBarang_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="kode" 
              DataSourceID="MasterBarangDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="kode" HeaderText="Kode Barang" ReadOnly="True" SortExpression="kode" UniqueName="kode" ItemStyle-HorizontalAlign="Center" />                    
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Barang" SortExpression="nama" UniqueName="nama" />                    
                <telerik:GridBoundColumn DataField="barangkategori_nama" HeaderText="Kategori" SortExpression="barangkategori_nama" UniqueName="barangkategori_nama" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" SortExpression="unitkerja" UniqueName="unitkerja" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
              </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="true" />
            </ClientSettings>
          </telerik:RadGrid>
          <div style="text-align: center; padding-top: 10px">
            <asp:LinkButton ID="PnlListBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
            <asp:Button ID="PnlListBarangBtnClose" runat="server" Text="Close" /><br />
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
