<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.Project.Work_Request.List" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Daftar Work Request (Project) - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="/images/icons/icons8-list-48.png" alt="icons8-list-48.png"  />&ensp;Work Request (Project)
      </div><br />
      <div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
          <ContentTemplate>
            <table>
              <asp:Panel ID="PnlKataKunci" runat="server">
                <tr>
                  <td class="tableFieldHeader">Kata Kunci</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox>
                  </td>
                </tr>
              </asp:Panel>
              <asp:Panel ID="PnlTanggal" runat="server" Visible="false">
                <tr>
                  <td class="tableFieldHeader">Periode Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
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
                    <asp:ListItem Value="1">Kode Reference</asp:ListItem>
                    <asp:ListItem Value="2">Status</asp:ListItem>
                    <asp:ListItem Value="3">Tanggal</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              <tr  >
                <td colspan="3" style="padding-top: 15px">
                  <asp:Button ID="BtnSearch" runat="server" Text="Cari" onclick="BtnSearch_Click" />&ensp;
                  <asp:Button ID="BtnCancel" runat="server" Text="Batal" onclick="BtnCancel_Click" />
                </td>
              </tr>
            </table>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
      <div style="text-align:right">
        <asp:Button ID="BtnAddWR" runat="server" Text="Tambah WR (+)" onclick="BtnAddWR_Click" />
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <telerik:RadGrid ID="GridWR" runat="server" AllowPaging="True" 
            DataSourceID="WRDataSource" GridLines="None" 
            onitemcommand="GridWR_ItemCommand" 
            onitemdatabound="GridWR_ItemDataBound" AllowSorting="True">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
              DataSourceID="WRDataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="No WR" ReadOnly="True" SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center"  />
                <telerik:GridBoundColumn DataField="reference" HeaderText="Reference" SortExpression="reference" UniqueName="reference" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="tglwr" HeaderText="Tanggal" SortExpression="tglwr" UniqueName="tglwr" DataType="System.DateTime" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="project_nomor" HeaderText="No Project" SortExpression="project_nomor" UniqueName="project_nomor" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="createdby" HeaderText="createdby" SortExpression="createdby" UniqueName="createdby" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Dibuat Oleh" UniqueName="dibuatoleh" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <asp:Label ID="LblRequestor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdby")%>' /><br />
                    (<asp:Label ID="LblDateCreated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datecreated")%>' />)
                  </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" SortExpression="unitkerja" UniqueName="unitkerja" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </ContentTemplate>
      </asp:UpdatePanel>
      <br /><br />
      <div style="border: medium solid #FF9933; background-color: #FFFFCC; border-radius: 10px; padding: 1px">
        <table>
          <tr>
            <td style="padding-left: 20px; text-align: right">
              <img src="/images/icons/icons8-idea-48.png" alt="icons8-idea-48.png" />
            </td>
            <td>
              <ul>
                <li>Klik 2x pada baris/row untuk melihat "Detail Work Request".</li>
              </ul>
            </td>
          </tr>
        </table>          
      </div>
    </div>
  </div>

  <asp:ObjectDataSource ID="WRDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetDataByTypeUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wr01TableAdapter" 
    oninit="WRDataSource_Init" >
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
      <asp:ControlParameter ControlID="LblDetailNoWR" DefaultValue="0" Name="wrId" 
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
                    <asp:Label ID="PnlDetailLblTitlebar" runat="server" Text="Work Request" />
                  </td>
                </tr>
              </table>            
          </div>
        </asp:Panel>
        <div style="padding:10px; text-align:left">
          <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" MultiPageID="RadMultiPage1" >
            <Tabs>
              <telerik:RadTab runat="server" Text="Work Request" Selected="True" />
              <telerik:RadTab runat="server" Text="Item Jasa" />
            </Tabs>
          </telerik:RadTabStrip>
          <telerik:RadMultiPage ID="RadMultiPage1" runat="server" Width="100%" SelectedIndex="0" BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" BackColor="White">
            <telerik:RadPageView ID="RadPageView1" runat="server"  >
              <div style="padding:15px">
                <table>
                  <tr>
                    <td class="tableFieldHeader">No WR</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailNoWR" runat="server" Text=""></asp:Label>
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
                      <asp:Label ID="LblDetailUsability" runat="server"></asp:Label>
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
                    <td class="tableFieldHeader">Status</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailStatus" runat="server"></asp:Label>
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
                    <td class="tableFieldHeader">Unit Kerja</td>
                    <td class="tableFieldHeader">:</td>
                    <td>
                      <asp:Label ID="LblDetailUnitKerja" runat="server"></asp:Label>
                    </td>
                  </tr>
                </table>
              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView2" runat="server" >
              <div style="padding:15px">
                <telerik:RadGrid ID="GridWRDetail" runat="server" AllowPaging="True" 
                  DataSourceID="WRDetailDataSource" GridLines="None" 
                  onitemdatabound="GridWRDetail_ItemDataBound" AllowSorting="True" 
                  onpageindexchanged="GridWRDetail_PageIndexChanged" 
                  onpagesizechanged="GridWRDetail_PageSizeChanged">
                  <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
                    DataSourceID="WRDetailDataSource">
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
                          <%# DataBinder.Eval(Container.DataItem, "jmljasa")%><%# string.Concat(DataBinder.Eval(Container.DataItem, "satuan"))%>
                        </ItemTemplate>
                      </telerik:GridTemplateColumn>
                      <telerik:GridBoundColumn DataField="jmlorang" HeaderText="Jml Orang" SortExpression="jmlorang" UniqueName="jmlorang" ItemStyle-HorizontalAlign="Center" DataType="System.Int32" />
                      <telerik:GridBoundColumn DataField="jmlhari" HeaderText="Jml Hari" SortExpression="jmlhari" UniqueName="jmlhari" ItemStyle-HorizontalAlign="Center" DataType="System.Int32" />
                      <telerik:GridBoundColumn DataField="tanggal" HeaderText="Tanggal" SortExpression="tanggal" UniqueName="tanggal" ItemStyle-HorizontalAlign="Center" DataType="System.DateTime" DataFormatString="{0:d}" />
                      <telerik:GridBoundColumn DataField="keterangan" HeaderText="Keterangan" SortExpression="keterangan" UniqueName="keterangan" />                      
                      <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" />
                      <telerik:GridBoundColumn DataField="createdby" HeaderText="Requestor" SortExpression="createdby" UniqueName="createdby" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                  </MasterTableView>      
                </telerik:RadGrid>
              </div>
            </telerik:RadPageView>
          </telerik:RadMultiPage>
        </div>
        <div style="padding-top:10px; text-align:center">
          <asp:LinkButton ID="PnlDetailLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
          <asp:Button ID="PnlDetailBtnClose" runat="server" Text="Tutup" /><br />
        </div>
      </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
