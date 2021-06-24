<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Input.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Request.Input" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Work Request - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
        <img src="/images/icons/icons8-brochure-48.png" alt="icons8-brochure-48.png"  />&ensp;<asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <asp:Panel ID="PnlTransaksi" runat="server" GroupingText="Transaksi Permintaan Jasa" Enabled="true">
            <div style="padding: 10px">
              <table>
                <tr>
                  <td class="tableFieldHeader">No WR</td>
                  <td class="tableFieldHeader">:</td>
                  <td colspan="2">
                    <asp:TextBox ID="TxtNoWR" runat="server" Enabled="false" CssClass="textbox_default"></asp:TextBox>
                  </td>
                </tr>               
                <tr>
                  <td class="tableFieldHeader">Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <telerik:RadDatePicker ID="RDPickerTxtTgl" runat="server" AutoPostBack="True" Culture="Indonesian (Indonesia)" />
                  </td>
                  <td>
                    <table>
                      <tr>
                        <td>Buat kode Reference</td>
                        <td>:</td>
                        <td>
                          <asp:RadioButton ID="RbYes" runat="server" Text="Ya" GroupName="rb" AutoPostBack="true" oncheckedchanged="RbYes_CheckedChanged" /></td>
                        <td>
                          <asp:RadioButton ID="RbNo" runat="server" Text="Tidak" GroupName="rb" AutoPostBack="true" oncheckedchanged="RbNo_CheckedChanged" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Lokasi</td>
                  <td class="tableFieldHeader">:</td>
                  <td>                    
                    <asp:DropDownList ID="DlistLokasiProject" runat="server" 
                      DataSourceID="LokasiDataSource" DataTextField="nama" DataValueField="id" 
                      AutoPostBack="True" />
                  </td>
                  <td><asp:TextBox ID="TxtReference" runat="server" Width="388px" Visible="False" CssClass="textbox_default"></asp:TextBox></td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Departement</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:DropDownList ID="DListDepartment" runat="server" Width="206px" 
                      DataSourceID="DepartementDataSource" DataTextField="nama" DataValueField="nama">
                    </asp:DropDownList>
                  </td>
                  <td style="text-align: right">
                    <asp:Button ID="BtnRefresh" runat="server" Text="Refresh" onclick="BtnRefresh_Click" Visible="False" />
                  </td>
                </tr>
                <asp:Panel ID="PnlScope" runat="server">
                <tr>
                  <td class="tableFieldHeader">Kode Scope</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:HiddenField ID="HdIdScope" runat="server" />
                          <asp:TextBox ID="TxtKodeScope" runat="server" CssClass="textbox_default"></asp:TextBox>
                        </td>
                        <td><asp:Button ID="BtnBrowseScope" runat="server" Text="..." onclick="BtnBrowseScope_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                </asp:Panel>
                <asp:Panel ID="PnlUsable" runat="server">
                <tr>
                  <td class="tableFieldHeader">Usable/Jenis Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:HiddenField ID="HdIdUsable" runat="server" />
                          <asp:TextBox ID="TxtUsable" runat="server" CssClass="textbox_default"></asp:TextBox>
                        </td>
                        <td><asp:Button ID="BtnBrowseUsable" runat="server" Text="..." onclick="BtnBrowseUsable_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                </asp:Panel>
                <tr>
                  <td class="tableFieldHeader">Unit Kerja</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:DropDownList ID="DlistUnitKerja" runat="server" 
                      onselectedindexchanged="DlistUnitKerja_SelectedIndexChanged" AutoPostBack="true">
                      <asp:ListItem>ASSEMBLING</asp:ListItem>
                      <asp:ListItem>SPARE PARTS</asp:ListItem>
                      <asp:ListItem>SHIPYARD</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
              </table><br />
              <div>
                <asp:Button ID="BtnSave" runat="server" Text="Simpan" onclick="BtnSave_Click" />&ensp;
                <asp:Button ID="BtnCancel" runat="server" Text="Batal" onclick="BtnCancel_Click" />&ensp;                
              </div>
            </div>
          </asp:Panel>
        </ContentTemplate>
      </asp:UpdatePanel><br /><br />
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <asp:Panel GroupingText="Tambah Jasa" ID="PnlInputJasa" runat="server" Enabled="False" >
            <div style="padding:10px">              
              <div style="display: none" visible="false"></div>
              <table>               
                <tr>
                  <td class="tableFieldHeader">Jasa</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:HiddenField ID="HdKdJasa" runat="server" />
                          <asp:TextBox ID="TxtJasa" runat="server" CssClass="textbox_default"></asp:TextBox>
                        </td>
                        <td>
                          <asp:Button ID="BtnBrowseJasa" runat="server" Text="..." onclick="BtnBrowseJasa_Click" /></td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jumlah Jasa</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:TextBox ID="TxtJmlJasa" runat="server" CssClass="textbox_default"></asp:TextBox>
                    <asp:RangeValidator ID="TxtJmlJasaValidator" runat="server" ControlToValidate="TxtJmlJasa" ErrorMessage="<b>Jumlah</b><br />Nilai berupa angka bulat." Display="None" Type="Integer" MaximumValue="100000" MinimumValue="0" />
                    <act:ValidatorCalloutExtender ID="TxtJmlJasaExtender" runat="server" TargetControlID="TxtJmlJasaValidator" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Satuan</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:TextBox ID="TxtSatuan" runat="server" CssClass="textbox_default"></asp:TextBox>
                  </td>
                </tr>
                <asp:Panel ID="PnlJmlOrang" runat="server">
                  <tr>
                    <td class="tableFieldHeader">Jumlah Orang</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:TextBox ID="TxtJmlOrang" runat="server" CssClass="textbox_default"></asp:TextBox>
                      <asp:RangeValidator ID="TxtJmlOrangValidator" runat="server" ControlToValidate="TxtJmlOrang" ErrorMessage="<b>Jumlah</b><br />Nilai berupa angka bulat." Display="None" Type="Integer" MaximumValue="100000" MinimumValue="0" />
                      <act:ValidatorCalloutExtender ID="TxtJmlOrangExtender" runat="server" TargetControlID="TxtJmlOrangValidator" />
                    </td>
                  </tr>
                </asp:Panel>
                <tr>
                  <td class="tableFieldHeader">Tanggal Pelaksanaan</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <telerik:RadDatePicker ID="RDateTxtTglPelaksanaan" runat="server" />
                  </td>
                </tr>
                <asp:Panel ID="PnlHari" runat="server">
                  <tr>
                    <td class="tableFieldHeader">Jumlah Hari</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:TextBox ID="TxtJmlHari" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                      <asp:RangeValidator ID="TxtJmlHariValidator" runat="server" ControlToValidate="TxtJmlHari" ErrorMessage="<b>Jumlah</b><br />Nilai berupa angka bulat." Display="None" Type="Integer" MaximumValue="100000" MinimumValue="0" />
                      <act:ValidatorCalloutExtender ID="TxtJmlHariExtender" runat="server" TargetControlID="TxtJmlHariValidator" />
                  </tr>
                </asp:Panel>
                <tr>
                  <td class="tableFieldHeader">Keterangan</td>
                  <td class="tableFieldHeader">:</td>
                  <td><asp:TextBox ID="TxtKeterangan" runat="server" TextMode="MultiLine" CssClass="textbox_default"></asp:TextBox></td>
                </tr>
              </table><br />
              <div><asp:Button ID="BtnAddInputJasa" runat="server" Text="Tambah" onclick="BtnAddInputJasa_Click" /></div><br />
              <telerik:RadGrid ID="GridRequestJasa" runat="server" AllowPaging="True" DataSourceID="WRDetailDataSource" GridLines="None" Visible="False" onitemdatabound="GridRequestJasa_ItemDataBound">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="WRDetailDataSource">
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
              </telerik:RadGrid><br />    
            </div>
          </asp:Panel>          
        </ContentTemplate>
      </asp:UpdatePanel><br /><br />
      <asp:Button ID="BtnBack" runat="server" Text="Kembali" onclick="BtnBack_Click" />
    </div>
  </div>  
 <asp:ObjectDataSource ID="LokasiDataSource" runat="server" OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByUnitKerja" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_lokasiTableAdapter" >
   <SelectParameters>
     <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
   </SelectParameters>
  </asp:ObjectDataSource>
 <asp:ObjectDataSource ID="ScopeDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataNonProject" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_scopeTableAdapter" />
 <asp:ObjectDataSource ID="UsableDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataJasaNonProject" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_usableTableAdapter" />  
 <asp:ObjectDataSource ID="MasterJasaDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.master_jasaTableAdapter">
   <SelectParameters>
     <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
   </SelectParameters>
 </asp:ObjectDataSource>
 <asp:ObjectDataSource ID="WRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByWrId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wrdetail01TableAdapter">
   <SelectParameters>
     <asp:ControlParameter ControlID="TxtNoWR" DefaultValue="0" Name="wrId" 
       PropertyName="Text" Type="String" />
   </SelectParameters>
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
     
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBrowseScopeProjectModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseScopeProjectLinkButton" DropShadow="false" PopupControlID="PnlBrowseScopeProject" PopupDragHandleControlID="PnlBrowseScopeProjectTitlebar" CancelControlID="PnlBrowseScopeProjectLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBrowseScopeProject" runat="server" Width="40%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowsScopeProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseScopeProjectLblTitlebar" runat="server" Text="Data Scope" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">    
          <telerik:RadGrid ID="GridScope" runat="server" DataSourceID="ScopeDataSource" GridLines="None" AllowPaging="True" onitemcommand="GridScope_ItemCommand" AllowSorting="True">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="ScopeDataSource">
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
      <asp:Panel ID="PnlBrowseUsableProject" runat="server" Width="30%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlBrowseUsableProjectTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlBrowseUsableProjectLblTitlebar" runat="server" Text="Data Usable / Jenis Kategori" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">              
          <telerik:RadGrid ID="GridUsable" runat="server" DataSourceID="UsableDataSource" AllowPaging="True" GridLines="None" onitemcommand="GridUsable_ItemCommand" AllowSorting="True">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="UsableDataSource">
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

</asp:Content>
