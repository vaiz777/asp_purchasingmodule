<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kelola.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.Project.Price_Request.Kelola" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Kelola Work Request (Project) - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
        &nbsp;<img src="/images/icons/icons8-compose-48.png" alt="icons8-compose-48.png"  />&ensp;Work Request (Project)
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <%--<asp:Panel ID="Panel1" runat="server" GroupingText="Work Request">--%>
            <div style="padding: 10px">
              <table>
                <tr>
                  <td class="tableFieldHeader" style="width:150px">No WR</td>
                  <td class="tableFieldHeader" style="width:15px">:</td>
                  <td>
                    <asp:Label ID="LblNoWR" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tanggal WR</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblTglWR" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Lokasi</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblLokasi" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">No Project</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblNoProject" runat="server"></asp:Label>
                  </td>
                  <tr>
                  <td class="tableFieldHeader">Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblKategori" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Usable</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblUsable" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Scope</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblScope" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Kode Reference</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblReference" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Dibuat Oleh</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblDibuatOleh" runat="server"></asp:Label>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Status</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblStatus" runat="server"></asp:Label>
                  </td>
                </tr>
              </table>
            </div>
          <%--</asp:Panel>--%>
          <br />
          <%--<asp:Panel ID="Panel2" runat="server" GroupingText="Item Jasa">--%>
            <div style="padding: 10px">
              <div style="text-align: right">
                <asp:Button ID="BtnAddJasa" runat="server" Text="Tambah Jasa" onclick="BtnAddJasa_Click"/><br /><br />
              </div>
              <telerik:RadGrid ID="GridItemJasa" runat="server" AllowPaging="True" 
                AllowSorting="True" DataSourceID="WRDetailDataSource" GridLines="None"  
                onitemcommand="GridItemJasa_ItemCommand" 
                onitemdatabound="GridItemJasa_ItemDataBound">
                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="WRDetailDataSource">
                  <Columns>
                    <telerik:GridBoundColumn DataField="id" HeaderText="ID " ReadOnly="True" SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" Visible="false" DataType="System.Int64" />
                    <telerik:GridBoundColumn DataField="jasa_kode" HeaderText="Kode Jasa" SortExpression="jasa_kode" UniqueName="jasa_kode" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn HeaderText="Jml Jasa" SortExpression="jmljasa" UniqueName="jmljasa" >
                      <ItemTemplate>
                        <%# string.Concat(DataBinder.Eval(Container.DataItem, "jmljasa"))%> <%# string.Concat(DataBinder.Eval(Container.DataItem, "satuan"))%>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="tanggal" DataType="System.DateTime" HeaderText="Tanggal" SortExpression="tanggal" UniqueName="tanggal" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="status" HeaderText="Status" SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" /> 
                    <telerik:GridBoundColumn DataField="createdby" HeaderText="Dibuat Oleh" SortExpression="createdby" UniqueName="createdby" />
                    <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn">
                      <ItemTemplate>
                        <asp:Button ID="BtnUpdate" runat="server" Text="Edit" CommandName="UpdateClick" Visible="false" />
                        <asp:Button ID="BtnDelete" runat="server" Text="Delete" CommandName="DeleteClick" Visible="false" />
                      </ItemTemplate>
                    </telerik:GridTemplateColumn>
                  </Columns>
                </MasterTableView>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                  <Selecting AllowRowSelect="True" />
                </ClientSettings>
              </telerik:RadGrid>
            </div>
          <%--</asp:Panel>--%>
          <%--Panel Tips--%>
          <br />
          <div style="border: medium solid #FF9933; background-color: #FFFFCC; border-radius: 10px; padding: 1px">
            <table>
              <tr>
                <td style="padding-left: 20px; text-align: right">
                  <img src="/images/icons/icons8-idea-48.png" alt="icons8-idea-48.png" />
                </td>
                <td>
                  <ul>
                    <li>Klik 2x pada baris/row untuk melihat Detail.</li>
                    <li>Button Edit untuk edit Item Jasa (Edit Data, Input Harga & Supplier/Vendor)</li>
                  </ul>
                </td>
              </tr>
            </table>          
          </div>
          <br /><br />
          <asp:Button ID="BtnBack" runat="server" Text="Kembali" OnClick="BtnBack_Click" />
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  
  <asp:ObjectDataSource ID="WRDetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByWrId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wrdetail01TableAdapter">
    <SelectParameters>
      <asp:ControlParameter ControlID="LblNoWR" DefaultValue="0" Name="wrId" 
        PropertyName="Text" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewLinkButton" 
      DropShadow="false" PopupControlID="PanelView" PopupDragHandleControlID="PnlView" BackgroundCssClass="modalBackground" 
      CancelControlID="PnlViewBtnClose" />
        <asp:Panel ID="PanelView" runat="server" Width="55%" CssClass="modalPopup" >
        <div style="padding:5px">
          <asp:Panel ID="PnlView" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td class="">
                    <img src="/images/icons/icons8-paper-48.png" alt="icons8-paper-48.png" />
                  </td>
                  <td class="">
                    &ensp;<asp:Label ID="LblNamaForm" runat="server" Text="Detail Item Jasa" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Tgl Dibutuhkan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblTglDibutuhkan" runat="server"></asp:Label>
                </td>
                <td style="width : 30px"></td>
                <td class="tableFieldHeader">Supplier</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblSupplier" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jasa</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblJasa" runat="server"></asp:Label>
                </td>
                <td style="width: 30px"></td>
                <td class="tableFieldHeader">Harga</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblCurrency" runat="server"></asp:Label>&ensp;
                  <asp:Label ID="LblHarga" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah Jasa</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblJmlJasa" runat="server"></asp:Label>&ensp;
                  <asp:Label ID="LblSatuan" runat="server"></asp:Label>
                </td>
                <td style="width: 30px"></td>
                <td class="tableFieldHeader">Keterangan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblKeterangan" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah Orang</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblJmlOrang" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah Hari</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="LblJmlHari" runat="server"></asp:Label>
                </td>
              </tr>
            </table><br />
            <div style="text-align:center">
              <asp:LinkButton ID="PnlViewLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
              <asp:Button ID="PnlViewBtnClose" runat="server" Text="Tutup"  /><br />
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
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
</asp:Content>
