<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Input.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.Project.Work_Request.Input" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Work Request (Project) - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="/images/icons/icons8-brochure-48.png" alt="icons8-brochure-48.png"  />&ensp;Input Work Request (Project)
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
          <asp:Panel ID="Panel1" runat="server" GroupingText="Transaksi Permintaan Jasa" Enabled="true">
            <div style="padding: 10px">
              <table>
                <tr>
                  <td class="tableFieldHeader">No WR</td>
                  <td class="tableFieldHeader">:</td>
                  <td colspan="2">
                    <asp:TextBox ID="TxtNoWR" runat="server" CssClass="textbox_default" 
                      Enabled="False" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <telerik:RadDatePicker ID="RDatePickerTxtTgl" runat="server" 
                      AutoPostBack="True" Culture="Indonesian (Indonesia)" 
                      CssClass="textbox_default" >
                      <DateInput AutoPostBack="True">
                      </DateInput>
                      <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                        ViewSelectorText="x">
                      </Calendar>
                      <DatePopupButton HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                  </td>
                  <td>
                    <table>
                      <tr>
                        <td>Buat kode Referensi</td>
                        <td>:</td>
                        <td><asp:RadioButton ID="RbYes" runat="server" GroupName="rb" Text="Ya" AutoPostBack="true" oncheckedchanged="RbYes_CheckedChanged" /></td>
                        <td><asp:RadioButton ID="RbNo" runat="server" GroupName="rb" Text="Tidak" AutoPostBack="true" oncheckedchanged="RbNo_CheckedChanged" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Nomor Project</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:TextBox ID="TxtNomorProject" runat="server"  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                          <asp:HiddenField ID="HdIdMasterProject" runat="server" />
                        </td>
                        <td><asp:Button ID="BtnBrowseProject" runat="server" Text="..." onclick="BtnBrowseProject_Click" /></td>
                      </tr>
                    </table>
                  </td>
                  <td>
                    <asp:TextBox ID="TxtReference" runat="server" Width="388px" Visible="False"  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Lokasi</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:TextBox ID="TxtLokasi" runat="server"  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                          <asp:HiddenField ID="HdIdLokasi" runat="server" />
                        </td>
                        <td><asp:Button ID="BtnBrowseLokasi" runat="server" Text="..." onclick="BtnBrowseLokasi_Click" /></td>
                      </tr>
                    </table>
                  </td>
                  <td style="text-align: right"><asp:Button ID="BtnRefresh" runat="server" Text="Refresh" Visible="False" onclick="BtnRefresh_Click" /></td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:TextBox ID="TxtKategori" runat="server"  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                          <asp:HiddenField ID="HdIdKategori" runat="server" />
                        </td>
                        <td><asp:Button ID="BtnBrowseKategori" runat="server" Text="..." onclick="BtnBrowseKategori_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Scope</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:TextBox ID="TxtKodeScope" runat="server"  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                          <asp:HiddenField ID="HdIdScope" runat="server" />
                        </td>
                        <td><asp:Button ID="BtnBrowseKodeScope" runat="server" Text="..." onclick="BtnBrowseKodeScope_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Usable/Jenis Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:TextBox ID="TxtUsable" runat="server"  CssClass="textbox_default" ReadOnly="True"></asp:TextBox>
                          <asp:HiddenField ID="HdIdUsable" runat="server" />
                        </td>
                        <td><asp:Button ID="BtnBrowseUsable" runat="server" Text="..." onclick="BtnBrowseUsable_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Unit Kerja</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:DropDownList ID="DlistUnitKerja" runat="server">
                      <asp:ListItem>ASSEMBLING</asp:ListItem>
                      <asp:ListItem>SPARE PARTS</asp:ListItem>
                      <asp:ListItem>SHIPYARD</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
              </table><br />
              <div>
                <asp:Button ID="BtnSaveTransaksiWR" runat="server" Text="Simpan" onclick="BtnSaveTransaksiWR_Click" />&ensp;
                <asp:Button ID="BtnCancelTransaksiWR" runat="server" Text="Batal" onclick="BtnCancelTransaksiWR_Click" />&ensp;
                </div>
            </div>
          </asp:Panel>
        </ContentTemplate>
      </asp:UpdatePanel> <br /><br />
      <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
          <asp:Panel ID="PnlInputJasa" runat="server" GroupingText="Tambah Jasa" Enabled="false" >
            <div style="padding: 10px">
              <table>
                <%--<tr>
                  <td class="tableFieldHeader">No WR</td>
                  <td>:</td>
                  <td>
                    <asp:TextBox ID="LblIdWr" runat="server" CssClass="textbox_default" Enabled="false"></asp:TextBox>
                  </td>
                </tr>--%>
                <tr>
                  <td class="tableFieldHeader">Jasa</td>
                  <td>:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:HiddenField ID="HdKdMasterJasa" runat="server" />
                          <asp:TextBox ID="TxtInisialJasa" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                        <td><asp:Button ID="BtnBrowseMasterJasa" runat="server" Text="..." onclick="BtnBrowseMasterJasa_Click" /></td>                        
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jumlah Jasa</td>
                  <td>:</td>
                  <td>
                    <asp:TextBox ID="TxtJmlJasa" runat="server" CssClass="textbox_default"></asp:TextBox>
                    <asp:RangeValidator ID="TxtJmlJasaValidator" runat="server" ControlToValidate="TxtJmlJasa" ErrorMessage="<b>Jumlah</b><br />Inputan berupa angka." Display="None" Type="Integer" MinimumValue="0" MaximumValue="1000000" />
                    <act:ValidatorCalloutExtender ID="TxtJmlJasaExtender" runat="server" TargetControlID="TxtJmlJasaValidator" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Satuan</td>
                  <td>:</td>
                  <td>
                    <asp:TextBox ID="TxtSatuan" runat="server" CssClass="textbox_default"></asp:TextBox>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jumlah Orang&nbsp;</td>
                  <td>:</td>
                  <td>
                    <asp:TextBox ID="TxtJmlOrang" runat="server" CssClass="textbox_default"></asp:TextBox>
                    <asp:RangeValidator ID="TxtJmlOrangValidator" runat="server" ControlToValidate="TxtJmlOrang" ErrorMessage="<b>Jumlah</b><br />Inputan berupa angka." Display="None" Type="Integer" MinimumValue="0" MaximumValue="10000000"  />
                    <act:ValidatorCalloutExtender ID="TxtJmlOrangExtender" runat="server" TargetControlID="TxtJmlOrangValidator" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tanggal Dibutuhkan</td>
                  <td>:</td>
                  <td>
                    <telerik:RadDatePicker ID="RDTxtTglDibutuhkan" Runat="server" AutoPostBack="True" Culture="Indonesian (Indonesia)" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jumlah Hari</td>
                  <td>:</td>
                  <td>
                    <asp:TextBox ID="TxtJumlahHari" runat="server" CssClass="textbox_default"></asp:TextBox>
                    <asp:RangeValidator ID="TxtJumlahHariValidator" runat="server" ControlToValidate="TxtJumlahHari" ErrorMessage="<b>Jumlah</b><br />Inputan berupa angka." Display="None" Type="Integer" MinimumValue="0" MaximumValue="100000000"  />
                    <act:ValidatorCalloutExtender ID="TxtJumlahHariExtender" runat="server" TargetControlID="TxtJumlahHariValidator" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Keterangan</td>
                  <td>:</td>
                  <td>
                    <asp:TextBox ID="TxtKeterangan" runat="server" CssClass="textbox_default" TextMode="MultiLine"></asp:TextBox>
                  </td>
                </tr>
              </table><br />
              <asp:Button ID="BtnAddJasa" runat="server" Text="Tambah" onclick="BtnAddJasa_Click" /><br /><br /><br />
              <telerik:RadGrid ID="GridRequestJasa" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="WRDetailSource" GridLines="None" Visible="False">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="WRDetailSource">
                  <RowIndicatorColumn>
                    <HeaderStyle Width="20px" />
                  </RowIndicatorColumn>
                  <ExpandCollapseColumn>
                    <HeaderStyle Width="20px" />
                  </ExpandCollapseColumn>
                  <Columns>
                    <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" DataType="System.Int64" Visible="false" />
                    <telerik:GridBoundColumn DataField="jasa_kode" HeaderText="Kode Jasa" SortExpression="jasa_kode" UniqueName="jasa_kode" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn HeaderText="Jml Jasa" SortExpression="jmljasa" UniqueName="jmljasa" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "jmljasa")%><%# DataBinder.Eval(Container.DataItem, "satuan")%>
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="jmlorang" HeaderText="Jml Orang" SortExpression="jmlorang" UniqueName="jmlorang" DataType="System.Int32" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="jmlhari" HeaderText="Jml Hari" SortExpression="jmlhari" UniqueName="jmlhari" DataType="System.Int32" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="tanggal" DataType="System.DateTime" HeaderText="Tanggal" SortExpression="tanggal" UniqueName="tanggal" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                    <telerik:GridBoundColumn DataField="createdby" HeaderText="Requestor" SortExpression="createdby" UniqueName="createdby" />
                  </Columns> 
                  <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
              </telerik:RadGrid>
              <br />
            </div>
          </asp:Panel>
          <asp:Button ID="BtnBackTransaksiWR" runat="server" onclick="BtnBackTransaksiWR_Click" Text="Kembali" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  <asp:ObjectDataSource ID="ProjectDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_project01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
   </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="LokasiDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_lokasiTableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
   </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="KategoriDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_kategoriTableAdapter" />
  <asp:ObjectDataSource ID="ScopeDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataProject" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_scopeTableAdapter" />
  <asp:ObjectDataSource ID="UsableDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataJasaProject" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_usableTableAdapter" />    
  <asp:ObjectDataSource ID="MasterJasaDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_jasaTableAdapter">
    <SelectParameters>
     <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="WRDetailSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByWrId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wrdetail01TableAdapter">
    <SelectParameters>
     <asp:ControlParameter ControlID="TxtNoWR" DefaultValue="0" Name="wrId" 
        PropertyName="Text" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
   
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

  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseLokasiProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseLokasiProjectLinkButton" DropShadow="false" PopupControlID="PnlBrowseLokasiProject" PopupDragHandleControlID="PnlBrowseLokasiProjectTitlebar" CancelControlID="PnlBrowseLokasiProjectLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseLokasiProject" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseLokasiProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseLokasiProjectLblTitlebar" runat="server" Text="Data Lokasi Project" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <telerik:RadGrid ID="GridLokasi" runat="server" DataSourceID="LokasiDataSource" 
            GridLines="None" AllowPaging="True" onitemcommand="GridLokasi_ItemCommand" 
            onpageindexchanged="GridLokasi_PageIndexChanged" 
            onpagesizechanged="GridLokasi_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="LokasiDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" DataType="System.Int32" />
                <telerik:GridBoundColumn DataField="inisial" HeaderText="Kode Lokasi" 
                  SortExpression="inisial" UniqueName="inisial" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nama" HeaderText="Lokasi" SortExpression="nama" UniqueName="nama" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseLokasiProjectLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseLokasiProjectBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseKategoriProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseKategoriProjectLinkButton" DropShadow="false" PopupControlID="PnlBrowseKategoriProject" PopupDragHandleControlID="PnlBrowseKategoriProjectTitlebar" CancelControlID="PnlBrowseKategoriProjectLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseKategoriProject" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseKategoriProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseKategoriProjectLblTitlebar" runat="server" Text="Data Kategori" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <table>
            <tr>
              <td class="tableFieldHeader">Kata Kunci</td>
              <td class="tableFieldHeader">:</td>
              <td><asp:TextBox ID="PnlKategoriTxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              <td style="width: 20px"></td>
              <td><asp:Button ID="PnlKategoriBtnSearch" runat="server" Text="Search" onclick="PnlKategoriBtnSearch_Click" />&ensp;</td>
            </tr>
          </table><br />
          <telerik:RadGrid ID="GridCategory" runat="server" 
            DataSourceID="KategoriDataSource" AllowPaging="True" GridLines="None" 
            onitemcommand="GridCategory_ItemCommand" 
            onpageindexchanged="GridCategory_PageIndexChanged" 
            onpagesizechanged="GridCategory_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="KategoriDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" DataType="System.Int32" />
                <telerik:GridBoundColumn DataField="inisial" HeaderText="Kode Kategori" 
                  SortExpression="inisial" UniqueName="inisial" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nama" HeaderText="Kategori" SortExpression="nama" UniqueName="nama" />
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid> 
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseKategoriProjectLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseKategoriProjectBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseScopeProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseScopeProjectLinkButton" DropShadow="false" PopupControlID="PnlBrowseScopeProject" PopupDragHandleControlID="PnlBrowseScopeProjectTitlebar" CancelControlID="PnlBrowseScopeProjectLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseScopeProject" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowsScopeProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseScopeProjectLblTitlebar" runat="server" Text="Data Scope Project" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">    
          <telerik:RadGrid ID="GridScope" runat="server" DataSourceID="ScopeDataSource" GridLines="None" AllowPaging="True" onitemcommand="GridScope_ItemCommand" onpageindexchanged="GridScope_PageIndexChanged" onpagesizechanged="GridScope_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ScopeDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" DataType="System.Int32" />
                <telerik:GridBoundColumn DataField="inisial" HeaderText="Kode Scope" SortExpression="inisial" UniqueName="inisial" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Scope" SortExpression="nama" UniqueName="nama" />
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>                    
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseScopeProjectLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseScopeProjectBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseUsableProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseUsableProjectLinkButton" DropShadow="false" PopupControlID="PnlBrowseUsableProject" PopupDragHandleControlID="PnlBrowseUsableProjectTitlebar" CancelControlID="PnlBrowseUsableProjectLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseUsableProject" runat="server" Width="50%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseUsableProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td class="">
                  <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                </td>
                <td class="">
                  <asp:Label ID="PnlBrowseUsableProjectLblTitlebar" runat="server" Text="Data Usable Project" />
                </td>
              </tr>
            </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">              
          <telerik:RadGrid ID="GridUsable" runat="server" DataSourceID="UsableDataSource" AllowPaging="True" GridLines="None" onitemcommand="GridUsable_ItemCommand" onpageindexchanged="GridUsable_PageIndexChanged" onpagesizechanged="GridUsable_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="UsableDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" DataType="System.Int32" />
                <telerik:GridBoundColumn DataField="inisial" HeaderText="Kode Usable" SortExpression="inisial" UniqueName="inisial" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Jenis Kategori" SortExpression="nama" UniqueName="nama" />
              </Columns>
              <HeaderStyle HorizontalAlign="Center" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>          
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlBrowseUsableProjectLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlBrowseUsableProjectBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
              <td class="tableFieldHeader">Kata Kunci</td>
              <td class="tableFieldHeader">:</td>
              <td>
                <asp:TextBox ID="PnlJasaTxtKataKunci" runat="server" CssClass="textbox_default" />
              </td>
              <td style="width: 20px"></td>
              <td>
                <asp:Button ID="PnlJasaBtnSearch" runat="server" Text="Search" onclick="PnlJasaBtnSearch_Click" />
              </td>
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
</asp:Content>
