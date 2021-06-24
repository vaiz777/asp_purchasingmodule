<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditMR.aspx.cs" Inherits="ManagementSystem.Purchasing.Material_Request.Project.Price_Request.EditMR" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit"Assembly="AjaxControlToolkit" %> 
<%@ Register assembly="Telerik.Web.UI"namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Purchasing - Edit Request Barang | PT Tri Ratna Diesel Indonesia</title>
  <script type="text/javascript">
    function pageLoad(sender, args) {
      Telerik.Web.UI.Calendar.Popup.zIndex = 100002;
    }
  </script>
</asp:Content>

<asp:Content  ID="Content2"  ContentPlaceHolderID="ContentPlaceHolder1"  runat="server" >
  <div style="padding: 10px">
    <div id="layar" style="border: medium solid #cccccc; background-color: White; border-radius: 10px; padding: 15px;" runat="server" >
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; ">
        <img src="../../../../images/icons/icons8-product-documents-48.png" alt="icons8-air-conditioner-48.png" />&nbsp;Edit MR
      </div><br />
      <div style="padding-bottom: 15px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <%-- Profil --%>
            <asp:Panel ID="PnlKeteranganBarang" runat="server" GroupingText="Material Request">
              <div style="padding: 5px">
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
                    <td class="tableFieldHeader">No Project</td>
                    <td class="tableFieldSeparator">:</td>
                    <td>
                      <asp:Label ID="LblNomorProject" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Lokasi</td>
                    <td class="tableFieldSeparator">:</td>
                    <td>
                      <asp:Label ID="LblLokasi" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Kategori</td>
                    <td class="tableFieldSeparator">:</td>
                    <td>
                      <asp:Label ID="LblKategori" runat="server"></asp:Label>
                    </td>
                  </tr>
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
                  <tr>
                    <td class="tableFieldHeader">Dibuat Oleh</td>
                    <td class="tableFieldSeparator">:</td>
                    <td>
                      <asp:Label ID="LblCreatedby" runat="server"></asp:Label>
                    </td>
                  </tr>
                </table>
              </div>
            </asp:Panel>
            <div class="tableFieldButton">
              <asp:Button ID="BtnTambah" runat="server" Text="Tambah Barang (+)" align="right" onclick="BtnTambah_Click"/>
            </div><br />
            <telerik:RadGrid ID="GridBarangRequested" runat="server" AllowPaging="True" 
              AllowSorting="True" GridLines="None" DataSourceID="MRDetailDataSource" 
              onitemdatabound="GridBarangRequested_ItemDataBound" OnItemCommand="GridBarangRequested_ItemCommand" >
              <MasterTableView AutoGenerateColumns="False" datakeynames="id" 
                datasourceid="MRDetailDataSource" >
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
                  <telerik:GridBoundColumn DataField="tglpemenuhan" HeaderText="Tgl Dibutuhkan" SortExpression="tglpemenuhan" UniqueName="tglpemenuhan" DataType="System.DateTime" DataFormatString="{0:d}" />
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
                  <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn">
                    <ItemTemplate>
                     <asp:Button ID="RadPushButton1" runat="server" Text="Hapus" CommandName="DeleteClick" Visible="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </telerik:GridTemplateColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
              </MasterTableView>
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid><br />
            <asp:Button ID="BtnClose" runat="server" onclick="BtnClose_Click" Text="Tutup Permintaan" Width="118px" /><br />
          </ContentTemplate>
        </asp:UpdatePanel>
      </div><br />
    </div>
  </div>

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
<asp:ObjectDataSource ID="MasterBarangDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}"  
    SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_barang01TableAdapter">
  <SelectParameters>
    <asp:SessionParameter ConvertEmptyStringToNull="False" Name="unitkerja" SessionField="UnitKerja" Type="String" />
  </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="MRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByMrId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter" >
  <SelectParameters>
    <asp:ControlParameter ControlID="LblNoMR" Name="mrId" 
      PropertyName="Text" Type="String" DefaultValue="0"  />
  </SelectParameters>
</asp:ObjectDataSource>
<asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
  <ContentTemplate>
    <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup" Style="display:none" >
        <asp:Panel ID="PnlMessageTitlebar" runat="server" CssClass="modalPopupTitle" >
          <div style="padding: 5px; text-align: left">
            <asp:Label ID="PnlMessageLblTitlebar" runat="server" Text="MessageBox" />
          </div>
        </asp:Panel>
        <div style="padding: 5px; text-align: left">
          <table>
            <tr>
              <td style="padding: 5px">
                <asp:Image ID="PnlMessageImgIcon" runat="server" />
              </td>
              <td style="padding: 5px">
                <asp:Label ID="PnlMessageLblMessage" runat="server" Text="..." />
              </td>
            </tr>
          </table>
          <div style="text-align: center; padding-top: 10px">
            <asp:Button ID="PnlMessageBtnOk" runat="server" Text="&nbsp;OK&nbsp;"  />&nbsp;
            <asp:LinkButton ID="PnlMessageLinkButton" runat="server" Style="display: none" >LinkButton</asp:LinkButton >
          </div>
        </div>
      </asp:Panel>
  </ContentTemplate>
</asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewLinkButton" DropShadow="false" PopupControlID="PanelView" PopupDragHandleControlID="PnlView" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PanelView" runat="server" Width="50%" CssClass="modalPopup">
          <div style="padding:5px">
          <asp:Panel ID="Panel3" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <img src="/images/icons/icons8-edit-property-48.png" alt="icons8-edit-property-48.png" />
                  </td>
                  <td>
                    &ensp;<asp:Label ID="LblNamaForm" runat="server" />
                    <div style="display:none">
                      <asp:HiddenField ID="HdIdReqBarang" runat="server" />
                    </div>                    
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Batas Pemenuhan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <telerik:RadDatePicker ID="RDateTgl" runat="server" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Nama Barang</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <div style="display: none" visible="false">
                          <asp:Label ID="LblIdJasa" runat="server" Text=""></asp:Label>
                          <asp:Label ID="LblIdReqJasa" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:TextBox ID="TxtBarang" runat="server" CssClass="textbox_default" Width="149px" ReadOnly="True"></asp:TextBox>
                        <asp:HiddenField ID="HdKdBarang" runat="server" />
                      </td>
                      <td><asp:Button ID="BtnBrowseBarang" runat="server" Text="..." onclick="BtnBrowseBarang_Click" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah Barang</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="TxtJumlah" runat="server" CssClass="textbox_default" Text="0" 
                    Width="149px"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jenis Satuan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:DropDownList ID="DlistSatuan" runat="server" DataSourceID="SatuanDataSource" DataTextField="nama" DataValueField="id" Width="149px" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Keterangan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="TxtKeterangan" runat="server" TextMode="MultiLine" CssClass="textbox_default" Width="149px"></asp:TextBox>
                </td>
              </tr>
            </table><br />
            <div style="text-align:right">
              <asp:Button ID="PnlViewBtnInsert" runat="server" Text="Insert" Visible="False" onclick="PnlViewBtnInsert_Click" />
              <asp:Button ID="PnlViewBtnUpdate" runat="server" Text="Update" Visible="False" OnClick="BtnUpdate_Click"  />
              <asp:LinkButton ID="PnlViewLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
              <asp:Button ID="PnlViewBtnClose" runat="server" Text="Batal" onclick="PnlViewBtnClose_Click" /><br />
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlListBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlListBarangLinkButton"  CancelControlID="PnlListBarangBtnClose" DropShadow="false" PopupControlID="PnlListBarang" PopupDragHandleControlID="PnlListBarangTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlListBarang" runat="server" Width="50%" CssClass="modalPopup">
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
          <div style="padding:10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Kata Kunci</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default" Width="150px"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Kategori</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:DropDownList ID="DlistJenisBarang" runat="server" Height="23px" Width="150px">
                    <asp:ListItem Value="0">Kode Barang</asp:ListItem>
                    <asp:ListItem Value="1">Nama Barang</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td colspan="3" class="tableFieldButton">
                  <asp:Button ID="BtnCariDataBarang" runat="server" Text="Search" onclick="BtnCariDataBarang_Click" />
                  <asp:Button ID="BtnClearDataBarang" runat="server" Text="Cancel" onclick="BtnClearDataBarang_Click" />
                </td>
              </tr>
            </table><br />            
            <telerik:RadGrid ID="GridListBarang" runat="server" AllowPaging="True" AllowSorting="True" GridLines="None" onitemcommand="GridListBarang_ItemCommand" DataSourceID="MasterBarangDataSource" onpageindexchanged="GridListBarang_PageIndexChanged" onpagesizechanged="GridListBarang_PageSizeChanged">
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="kode" DataSourceID="MasterBarangDataSource">
                <Columns>
                  <telerik:GridBoundColumn DataField="kode" HeaderText="Kode Barang" ReadOnly="True" SortExpression="kode" UniqueName="kode" ItemStyle-HorizontalAlign="Center" />
                  <telerik:GridBoundColumn DataField="nama" HeaderText="Nama Barang" SortExpression="nama" UniqueName="nama" />
                  <telerik:GridBoundColumn DataField="barangkategori_nama" HeaderText="Kategori" SortExpression="barangkategori_nama" UniqueName="barangkategori_nama" ItemStyle-HorizontalAlign="Center" />
                  <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />                  
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
              </MasterTableView>
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid>
          </div>
          <div style="text-align: center; padding-top: 10px">
            <asp:LinkButton ID="PnlListBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>&nbsp;
            <asp:Button ID="PnlListBarangBtnClose" runat="server" Text="Tutup"  /><br />
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
