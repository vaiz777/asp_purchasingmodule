<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.Project.Price_Request.List" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Permintaan Harga (Project) - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="/images/icons/icons8-list-48.png" alt="icons8-list-48.png"  />&ensp;Daftar Permintaan Harga (Project)
      </div><br />
      <asp:Panel ID="PnlContent1" runat="server" HorizontalAlign="Center">
        <asp:Label ID="Label1" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
      </asp:Panel>
      <asp:Panel ID="PnlContent2" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <table>
              <asp:Panel ID="PnlKataKunci" runat="server">
              <tr>
                <td class="tableFieldHeader">Kata Kunci</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader"><asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox></td>
              </tr>
              </asp:Panel>
              <asp:Panel ID="PnlTanggal" runat="server" Visible="false">
              <tr>
                <td class="tableFieldHeader">Periode Tanggal</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader">
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="TxtRangeTgl1" runat="server" CssClass="textbox_default"></asp:TextBox>
                        <act:CalendarExtender ID="CalTxtRangeTgl1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="TxtRangeTgl1" />
                      </td>
                      <td>&ensp;s/d&ensp;</td>
                      <td>
                        <asp:TextBox ID="TxtRangeTgl2" runat="server" CssClass="textbox_default"></asp:TextBox>
                        <act:CalendarExtender ID="CalTxtRangeTgl2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="TxtRangeTgl2" />
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              </asp:Panel>
              <tr>
                <td class="tableFieldHeader">Cari Berdasarkan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:DropDownList ID="DListBerdasarkan" runat="server" AutoPostBack="true" onselectedindexchanged="DListBerdasarkan_SelectedIndexChanged">
                    <asp:ListItem Value="0">No WR</asp:ListItem>
                    <asp:ListItem Value="1">Status</asp:ListItem>
                    <asp:ListItem Value="2">Tanggal</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td colspan="3" style="padding-top: 15px">
                  <asp:Button ID="BtnSearch" runat="server" Text="Cari" onclick="BtnSearch_Click" />&ensp;
                  <asp:Button ID="BtnCancel" runat="server" Text="Batal" onclick="BtnCancel_Click" />
                </td>
              </tr>
            </table>
            <br />
            <telerik:RadGrid ID="GridWR" runat="server" DataSourceID="WRDataSource" 
              GridLines="None" AllowPaging="True" onitemdatabound="GridReqNote_ItemDataBound" 
              onitemcommand="GridReqNote_ItemCommand" AllowSorting="True" >
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="WRDataSource">
                <Columns>
                  <telerik:GridBoundColumn DataField="id" HeaderText="No WR" ReadOnly="True" 
                    SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" 
                    ItemStyle-Font-Bold="true" >
                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="reference" HeaderText="Reference" 
                    SortExpression="reference" UniqueName="reference" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="tglwr" HeaderText="Tgl" 
                    SortExpression="tglwr" UniqueName="tglwr" DataType="System.DateTime" 
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                    ItemStyle-HorizontalAlign="Center" SortExpression="status" UniqueName="status" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridTemplateColumn HeaderText="Dibuat Oleh" UniqueName="dibuatoleh" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <asp:Label ID="LblRequestor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdby")%>' /><br />
                      (<asp:Label ID="LblDateCreated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datecreated")%>' />)
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridTemplateColumn>
                  <telerik:GridBoundColumn DataField="createdby" HeaderText="createdby" SortExpression="createdby" UniqueName="createdby" Visible="false" />
                  <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                    SortExpression="unitkerja" UniqueName="unitkerja" 
                    ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridBoundColumn>
                  <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <asp:Button ID="BtnKelola" runat="server" Text="Kelola" CommandName="KelolaClick" Visible="false" />                     
                      <asp:Button ID="BtnCheck" runat="server" Text="Cek" CommandName="CheckClick" Visible="false" />
                      <asp:Button ID="BtnBatal" runat="server" Text="Batal" CommandName="BatalClick" Visible="false" />
                      <asp:Button ID="BtnPrint" runat="server" Text="Print" CommandName="PrintClick" Visible="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                  </telerik:GridTemplateColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
              </MasterTableView>
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="True" />
              </ClientSettings>
            </telerik:RadGrid>
          </ContentTemplate>
        </asp:UpdatePanel>
        <%--Panel Tips--%>
        <br /><br />
        <div style="border: medium solid #FF9933; background-color: #FFFFCC; border-radius: 10px; padding: 1px">
          <table>
            <tr>
              <td style="padding-left: 20px; text-align: right">
                <img src="/images/icons/icons8-idea-48.png" alt="icons8-idea-48.png" />
              </td>
              <td>
                <ul>
                  <li>Klik 2x pada baris/row untuk melihat Detail.</li>
                  <li>Button Check : Untuk approved/verified Item Jasa (hanya u/ Manager, GM, dan PPIC)</li>
                  <li>Button Kelola : Untuk mengola Item Jasa (Create, Read, Update, Delete Item Jasa)</li>
                </ul>
              </td>
            </tr>
          </table>          
        </div>
      </asp:Panel>
    </div>
  </div>
  
  <asp:ObjectDataSource ID="WRDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByTypeUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wr01TableAdapter" 
    oninit="ReqNoteDataSource_Init" >
    <SelectParameters>
      <asp:Parameter DefaultValue="P" Name="type" Type="String" />
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="WRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByWrId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="LblDetailNoWR" Name="wrId" PropertyName="Text" 
        Type="String" DefaultValue="0" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="CheckWRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByWrId" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="PnlCheckTxtNoWR" DefaultValue="0" Name="wrId" 
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
 
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlDetailModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlDetailLinkButton" DropShadow="false" PopupControlID="PnlDetail" PopupDragHandleControlID="PnlDetailTitlebar" CancelControlID="PnlDetailLinkButton" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlDetail" runat="server" Width="80%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlDetailTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-view-48.png" alt="icons8-view-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlDetailLblTitlebar" runat="server" Text="Detail" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1">
            <Tabs>
              <telerik:RadTab runat="server" Text="Work Request" Selected="True" />
              <telerik:RadTab runat="server" Text="Item Jasa" />
            </Tabs>
          </telerik:RadTabStrip>
          <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0" BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" BackColor="White">
            <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
              <div style="padding:10px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">No WR</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailNoWR" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Nomor Project</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailNoProject" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Lokasi</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailLokasi" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Tanggal WR</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailTglDibuat" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Kategori</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailKategori" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <asp:Panel ID="PnlUsable" runat="server">
                  <tr>
                    <td class="tableFieldHeader">Usable</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailUsable" runat="server"></asp:Label>
                    </td>
                  </tr>
                  </asp:Panel>
                  <asp:Panel ID="PnlScope" runat="server">
                  <tr>
                    <td class="tableFieldHeader">Scope</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailScope" runat="server"></asp:Label>
                    </td>
                  </tr>
                  </asp:Panel>
                  <tr>
                    <td class="tableFieldHeader">Kode Referensi</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailNoReferensi" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Dibuat Oleh</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailDibuatOleh" runat="server"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td class="tableFieldHeader">Status</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailStatus" runat="server"></asp:Label>
                    </td>
                  </tr>
                </table>
              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server" Width="100%">
              <div style="padding:10px">
                <telerik:RadGrid ID="GridRequestJasa" runat="server" AllowPaging="True" DataSourceID="WRDetailDataSource" GridLines="None" AllowSorting="True" onpageindexchanged="GridRequestJasa_PageIndexChanged" onpagesizechanged="GridRequestJasa_PageSizeChanged" onitemdatabound="GridRequestJasa_ItemDataBound" >
                  <MasterTableView AutoGenerateColumns="False" DataSourceID="WRDetailDataSource" DataKeyNames="id">
                    <Columns>
                      <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false"  DataType="System.Int64" />
                      <telerik:GridBoundColumn DataField="jasa_kode" HeaderText="Kode Jasa" SortExpression="jasa_kode" UniqueName="jasa_kode" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama" />
                      <telerik:GridTemplateColumn HeaderText="Jml Jasa" SortExpression="jmljasa" UniqueName="jmljasa" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                          <%# string.Concat(DataBinder.Eval(Container.DataItem, "jmljasa"))%><%# string.Concat(DataBinder.Eval(Container.DataItem, "satuan"))%>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn>
                      <telerik:GridBoundColumn DataField="jmlorang" HeaderText="Jml Orang" SortExpression="jmlorang" UniqueName="jmlorang" DataType="System.Int32" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="jmlhari" HeaderText="Jml Hari" SortExpression="jmlhari" UniqueName="jmlhari" DataType="System.Int32" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                      <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="currency" HeaderText="Mata Uang" SortExpression="currency" UniqueName="currency" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="harga" HeaderText="Harga" SortExpression="harga" UniqueName="harga" DataType="System.Decimal" ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}" />
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                  </MasterTableView>
                  <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                  </ClientSettings>            
                </telerik:RadGrid>
              </div>
            </telerik:RadPageView>
          </telerik:RadMultiPage>          
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlDetailLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlDetailBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlCheckModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlCheckLinkButton" DropShadow="false" PopupControlID="PnlCheck" PopupDragHandleControlID="PnlCheckTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlCheck" runat="server" Width="80%" CssClass="modalPopup" >
      <div style="padding: 5px">
        <asp:Panel ID="PnlCheckTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    <asp:Label ID="PnlCheckLblTitlebar" runat="server" Text="" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <div>
            <table>
              <tr>
                <td class="tableFieldHeader">No WR</td>
                <td class="tableFieldSeparator">:</td>
                <td>
                  <asp:TextBox ID="PnlCheckTxtNoWR" runat="server" CssClass="textbox_default" Enabled="false"></asp:TextBox>
                </td>
              </tr>
            </table>
          </div><br />
          <telerik:RadGrid ID="GridApproveReqJasa" runat="server" AllowPaging="True" DataSourceID="CheckWRDetailDataSource" GridLines="None" onitemcreated="GridApproveReqJasa_ItemCreated" onitemdatabound="GridApproveReqJasa_ItemDataBound" ondatabound="GridApproveReqJasa_DataBound" onpageindexchanged="GridApproveReqJasa_PageIndexChanged" onpagesizechanged="GridApproveReqJasa_PageSizeChanged">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="CheckWRDetailDataSource">
              <Columns>
                <telerik:GridTemplateColumn UniqueName="CheckTemp">
                  <HeaderTemplate> 
                    <asp:CheckBox ID="CheckBoxHeader"  AutoPostBack="true" runat="server" OnCheckedChanged="CheckBoxHeader_CheckedChanged" /> 
                  </HeaderTemplate> 
                  <ItemTemplate>
                      <asp:CheckBox ID="CheckBoxRow" runat="server" />
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" DataType="System.Int64" />
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
                <telerik:GridBoundColumn DataField="jmlorang" HeaderText="Jml Orang" 
                  SortExpression="jmlorang" UniqueName="jmlorang" DataType="System.Int32" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="jmlhari" HeaderText="Jml Hari" 
                  SortExpression="jmlhari" UniqueName="jmlhari" DataType="System.Int32" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                  SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" 
                  SortExpression="supplier_nama" UniqueName="supplier_nama" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="currency" HeaderText="Mata Uang" 
                  SortExpression="currency" UniqueName="currency" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="harga" HeaderText="Harga" 
                  SortExpression="harga" UniqueName="harga" DataType="System.Decimal" 
                  ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>          
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:Button ID="PnlCheckBtnApproved" runat="server" Text="Disetujui" OnClick="BtnApprove_Click" />
          <asp:Button ID="PnlCheckBtnVerified" runat="server" Text="Diverifikasi" onclick="PnlCheckBtnVerified_Click" />&ensp;
          <asp:LinkButton ID="PnlCheckLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlCheckBtnClose" runat="server" Text="Close" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <act:ModalPopupExtender ID="PnlPrintModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlPrintLinkButton" CancelControlID="PnlPrintBtnOk" DropShadow="false" PopupControlID="PnlPrint" PopupDragHandleControlID="PnlPrintTitlebar" BackgroundCssClass="modalBackground" />
  <asp:Panel ID="PnlPrint" runat="server" Width="480px" CssClass="modalPopup">
    <div>
      <asp:Panel ID="PnlPrintTitlebar" runat="server" CssClass="modalPopupTitle">
        <div style="padding:5px; text-align:left">
          <asp:Label ID="PnlPrintLblTitlebar" runat="server" Text="Message Box" />
        </div>
      </asp:Panel>
      <div style="padding:5px; text-align:left">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
          <ContentTemplate>
            <table>
              <tr>
                <td style="padding: 5px">
                 <img src="/images/icons/icons8-print-48.png" alt="iicons8-print-48.png"  />
                </td>
                <td style="padding: 5px">
                  <asp:Label ID="Label2" runat="server" Text="Cetak Work Request"  />
                  &nbsp;<asp:Label ID="PnlPrintLblNoWR" runat="server"></asp:Label>
                  <asp:Label ID="Label3" runat="server" Text="?"></asp:Label>
                </td>
              </tr>
            </table>
          </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align: center; padding-top: 10px">
          <asp:Button ID="PnlPrintBtnPrint" runat="server" Text="Ya" onclick="PnlPrintBtnPrint_Click" />&ensp;&ensp;
          <asp:Button ID="PnlPrintBtnOk" runat="server" Text="Tidak"  />
          <asp:LinkButton ID="PnlPrintLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
        </div>
      </div>
    </div>
  </asp:Panel>
  
  <asp:UpdatePanel ID="UpdatePanel6" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBatalSetujuModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBatalSetujuLinkButton" CancelControlID="PnlBatalSetujuBtnOk" DropShadow="false" PopupControlID="PnlBatalSetuju" PopupDragHandleControlID="PnlBatalSetujuTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlBatalSetuju" runat="server" Width="480px" CssClass="modalPopup">
        <div>
          <asp:Panel ID="PnlBatalSetujuTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <asp:Label ID="PnlBatalSetujuLblTitlebar" runat="server" Text="Message Box" />
            </div>
          </asp:Panel>
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td style="padding: 5px">
                 <img src="/images/icons/icons8-error-48.png" alt="icons8-error-48.png"  />
                </td>
                <td style="padding: 10px; text-align: center">
                  <asp:Label ID="Label5" runat="server" Text="Nomer "  />
                  <asp:Label ID="PnlBatalSetujuLblNoWR" runat="server" />
                  <asp:Label ID="Label8" runat="server" Text=" sudah Disetujui." /><br />
                  <asp:Label ID="Label4" runat="server" Text="Apakah yakin ingin membatalkan?" />
                </td>
              </tr>
            </table>
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlBatalSetujuBtnYa" runat="server" Text="Ya" onclick="PnlBatalSetujuBtnYa_Click" />&ensp;&ensp;
              <asp:Button ID="PnlBatalSetujuBtnOk" runat="server" Text="Tidak"  />
              <asp:LinkButton ID="PnlBatalSetujuLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>

  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlInputNoteModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlInputNoteLinkButton" CancelControlID="PnlInputNoteLinkButton" DropShadow="false" PopupControlID="PnlInputNote" PopupDragHandleControlID="PnlInputNoteTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlInputNote" runat="server" Width="480px" CssClass="modalPopup">
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
                <td style="width:30px"></td>
                <td>
                 <img src="/images/icons/icons8-sign-up-48.png" alt="icons8-sign-up-48.png"  />
                </td>
              </tr>
            </table><br />
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlInputNoteBtnSubmit" runat="server" Text="Submit" 
                onclick="PnlInputNoteBtnSubmit_Click"  />
              <asp:LinkButton ID="PnlInputNoteLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
