<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.Material_Request.Project.Barang_Request.List" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>List MR | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding: 10px">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large;  letter-spacing: 0px;">
        <img src="../../../../images/icons/icons8-product-48.png" alt="icons8-time-card-48.png" />&nbsp;Daftar Material Request (Project)<br />
      </div><br />
      <div style="padding-bottom: 15px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnCancel" EventName="Click" />
          </Triggers>
          <ContentTemplate>
          <div style="padding:5px">
            <table>
              <asp:Panel ID="PnlKataKunci" runat="server">
              <tr>
                <td class="tableFieldHeader">Kata Kunci</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader">
                  <asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox>
                </td>
              </tr>
              </asp:Panel>              
              <asp:Panel ID="PnlTanggal" runat="server" Visible="false">
              <tr>
                <td class="tableFieldHeader">Tanggal</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader">
                 <asp:TextBox ID="TxtTanggalStart" runat="server" CssClass="textbox_default"></asp:TextBox>
                 <act:CalendarExtender ID="TxtTanggalStartCalExtender" runat="server"  Format="MM/dd/yyyy" Enabled="True" TargetControlID="TxtTanggalStart" />
                </td>
                <td class="tableFieldHeader">&ensp;s/d&ensp;</td>
                <td class="tableFieldHeader">
                 <asp:TextBox ID="TxtTanggalEnd" runat="server" CssClass="textbox_default"></asp:TextBox>
                 <act:CalendarExtender ID="TxtTanggalEndCalExtender" runat="server"  Format="MM/dd/yyyy" Enabled="True" TargetControlID="TxtTanggalEnd" />
                </td>
              </tr>
              </asp:Panel>
              <asp:Panel ID="PnlStatus" runat="server" Visible="false">
              <tr>
                <td class="tableFieldHeader">Status</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader" colspan="3">
                  <asp:DropDownList ID="DlistStatus" runat="server">
                   <asp:ListItem Value="MR1">MR Baru</asp:ListItem>
                   <asp:ListItem Value="MR2">MR Diverifikasi</asp:ListItem>
                   <asp:ListItem Value="MR4">Outstanding by PPC</asp:ListItem>
                   <asp:ListItem Value="MR5">MR Disetujui</asp:ListItem>
                   <asp:ListItem Value="MR6">MR Disetujui</asp:ListItem>
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
                <td class="tableFieldHeader">Cari Berdasarkan</td>
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
          </div>
          <div class="tableFieldButton">
            <asp:Button ID="BtnAdd" runat="server" Text="Tambah Data" onclick="BtnAdd_Click"/><br />
          </div><br />
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
              <telerik:RadGrid ID="GridMaterialRequest" runat="server" AllowPaging="True" 
              AllowSorting="True" GridLines="None" DataSourceID="MaterialRequestDataSource" 
              onitemdatabound="GridMaterialRequest_ItemDataBound" 
              onitemcommand="GridMaterialRequest_ItemCommand">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="MaterialRequestDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="No MR" ReadOnly="True" SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="reference" HeaderText="No Reference" SortExpression="reference" UniqueName="reference" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tanggal" DataType="System.DateTime" HeaderText="Tgl MR" SortExpression="tanggal" UniqueName="tanggal" DataFormatString="{0:d}" />
                <telerik:GridBoundColumn DataField="project_nomor" HeaderText="No Project" SortExpression="project_nomor" UniqueName="project_nomor" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" SortExpression="unitkerja" UniqueName="unitkerja" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridTemplateColumn HeaderText="Requestor"  ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "createdby")%><br />
                    ( <%# DataBinder.Eval(Container.DataItem, "datecreated")%> )
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
              </Columns>
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
            </MasterTableView>            
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="true" />
            </ClientSettings>
          </telerik:RadGrid>
            </ContentTemplate>
          </asp:UpdatePanel>           
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>  
  <asp:ObjectDataSource ID="MaterialRequestDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByUnitKerjaType" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mr01TableAdapter" 
    oninit="ListMRDataSource_Init">
    <SelectParameters>
      <asp:SessionParameter DefaultValue="0" Name="unitkerja" SessionField="UnitKerja" Type="String" />
      <asp:Parameter DefaultValue="P" Name="type" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>    
  <asp:ObjectDataSource ID="MRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByMrId" 
    
      TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="LblNoMR" DefaultValue="0" Name="mrId" 
        PropertyName="Text" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  </div>
  
  <asp:UpdatePanel ID="PanelBarangProject" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewBarangModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewBarangLinkButton" CancelControlID="PnlViewBarangBtnClose" DropShadow="false" PopupControlID="PnlViewBarang" PopupDragHandleControlID="PnlViewBarangTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlViewBarang" runat="server" Width="70%" CssClass="modalPopup">
          <asp:Panel ID="PnlViewBarangTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/icons8-bill-48.png" />
                  </td>
                  <td>
                    &ensp;<asp:Label ID="PnlViewBarangLblTitlebar" runat="server" Text="Detail Material Request" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding: 10px">
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
            <div style="padding:5px;">
              <telerik:RadGrid ID="GridDataBarang" runat="server" AllowPaging="True" 
                AllowSorting="True" GridLines="None" DataSourceID="MRDetailDataSource" 
                onitemdatabound="GridDataBarang_ItemDataBound">
                <MasterTableView DataSourceID="MRDetailDataSource" AutoGenerateColumns="False" 
                  DataKeyNames="id">
                  <Columns>
                    <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" DataType="System.Int64" Visible="false"/>
                    <telerik:GridBoundColumn DataField="barang_kode" HeaderText="Kode Barang" SortExpression="barang_kode" UniqueName="barang_kode" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                    <telerik:GridBoundColumn DataField="jumlah" HeaderText="Jumlah" SortExpression="jumlah" UniqueName="jumlah" DataType="System.Int32" />
                    <telerik:GridBoundColumn DataField="satuan_nama" HeaderText="Satuan" SortExpression="satuan_nama" UniqueName="satuan_nama" />
                    <telerik:GridBoundColumn DataField="tglpemenuhan" DataType="System.DateTime" HeaderText="Tgl Dibutuhkan" SortExpression="tglpemenuhan" UniqueName="tglpemenuhan" DataFormatString="{0:d}" />
                    <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                    <telerik:GridTemplateColumn HeaderText="Requestor" SortExpression="requestor" UniqueName="requestor" ItemStyle-HorizontalAlign="Center">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "createdby")%><br />
                        ( <%# DataBinder.Eval(Container.DataItem, "datecreated")%> )
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                  </Columns>
                  <HeaderStyle HorizontalAlign="Center" />
                </MasterTableView>
                <ClientSettings>
                  <Selecting AllowRowSelect="True" />
                </ClientSettings>
              </telerik:RadGrid>
          </div>
          <div style="text-align: center; padding-top: 10px">
            <asp:LinkButton ID="PnlViewBarangLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            <asp:Button ID="PnlViewBarangBtnClose" runat="server" Text="Close"/>
           </div>
           </div>
        </asp:Panel>
     </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
