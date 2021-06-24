<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterProject.aspx.cs" Inherits="ManagementSystem.Purchasing.Master.MasterProject" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Master Project - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="../../images/icons/icons8-product-48.png" alt="icons8-product-48.png"  /> Master Project
      </div><br />
      <div>
      <asp:Panel ID="PnlData1" runat="server" HorizontalAlign="Center">
        <asp:Label ID="Label5" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
      </asp:Panel>
      <asp:Panel ID="PnlData2" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <div>
              <table>
                <tr>
                  <td class="tableFieldHeader">Kata Kunci</td>
                  <td class="tableFieldHeader">:</td>
                  <td><asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Search By</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:DropDownList ID="DlistSearchBy" runat="server">
                      <asp:ListItem Value="0">No Project</asp:ListItem>
                      <asp:ListItem Value="1">Customer</asp:ListItem>
                    </asp:DropDownList>
                  </td>
                </tr>
                <tr>
                  <td colspan="3" style="padding-top: 10px">
                    <asp:Button ID="BtnSearch" runat="server" Text="Cari" onclick="BtnSearch_Click" />&ensp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Batal" onclick="BtnCancel_Click" />
                  </td>
                </tr>
              </table>
            </div><br /><br />
            <telerik:RadGrid ID="GridMasterProject" runat="server" DataSourceID="MasterProjectDataSource" GridLines="None" AllowPaging="True" onitemcommand="GridMasterProject_ItemCommand" AllowSorting="True">
              <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="MasterProjectDataSource">
                <Columns>
                  <telerik:GridBoundColumn DataField="id" HeaderText="ID" ReadOnly="True" SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" Visible="false" /> 
                  <telerik:GridBoundColumn DataField="nomorproject" HeaderText="Nomor Project" SortExpression="nomorproject" UniqueName="nomorproject" ItemStyle-HorizontalAlign="Center" />
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
              </MasterTableView>
              <HeaderStyle HorizontalAlign="Center" />
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="True" />
              </ClientSettings>
            </telerik:RadGrid><br />
            <div style="text-align: right">
              <asp:Button ID="BtnAddNew" runat="server" Text="Tambah Baru (+)" onclick="BtnAddNew_Click" />
            </div>
          </ContentTemplate>
        </asp:UpdatePanel> 
      </asp:Panel>
    </div>
  </div>
</div>
  
  <asp:ObjectDataSource ID="MasterProjectDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vmaster_project01TableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="SalesCustDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="TmsBackDataController.PurDataSetTableAdapters.sales_customerTableAdapter" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update" />
    
  <asp:UpdatePanel ID="UpdatePanelMessage" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" DropShadow="false" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlMessage" runat="server" Width="480px" CssClass="modalPopup" Style="display: none">
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
                  <asp:Label ID="PnlMessageLblMessage" runat="server" Text="..." />
                </td>
              </tr>
            </table>
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlMessageBtnOk" runat="server" Text="OK" />&nbsp;
              <asp:LinkButton ID="PnlMessageLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel12" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewAddNewModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewAddNewLinkButton" CancelControlID="PnlViewAddNewBtnClose" DropShadow="false" PopupControlID="PanelViewAddNew" PopupDragHandleControlID="PnlViewAddNew" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PanelViewAddNew" runat="server" Width="520px" CssClass="modalPopup" >
        <div style="padding:10px">
          <asp:Panel ID="Panel8" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td class="">
                    <img src="../../images/icons/icons8-create-document-48.png" alt="icons8-create-document-48.png" />
                  </td>
                  <td class="">
                    &ensp;<asp:Label ID="PnlViewLblTitlebar" runat="server" Text="Tambah Data Baru" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Tanggal</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlAddNewTxtTanggal" runat="server" class="textbox_default" />
                  <act:CalendarExtender ID="CalPnlAddNewTxtTanggal" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlAddNewTxtTanggal" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Nomor Project</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlAddNewTxtNomorProject" runat="server" class="textbox_default" Width="255px"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Customer</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="PnlAddNewTxtCustomer" runat="server" Width="255px" class="textbox_default" ReadOnly="true"></asp:TextBox>
                        <div style="display: none"><asp:Label ID="PnlAddNewLblIdCustomer" runat="server" Text="" Visible="false" ></asp:Label></div>
                      </td>
                      <td><asp:Button ID="PnlAddNewBtnBrowseCust" runat="server" Text="..." onclick="PnlAddNewBtnBrowseCust_Click" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jenis Kapal</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlAddNewTxtJenisKapal" runat="server" CssClass="textbox_default" Width="255px" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlAddNewTxtJumlah" runat="server" CssClass="textbox_default" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Tgl Project</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="PnlAddNewProjectStart" runat="server" class="textbox_default" 
                          AutoPostBack="true" ontextchanged="PnlAddNewProjectStart_TextChanged"></asp:TextBox>
                        <act:CalendarExtender ID="PnlAddNewProjectStartCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlAddNewProjectStart" />
                      </td>
                      <td> - </td>
                      <td>
                        <asp:TextBox ID="PnlAddNewProjectEnd" runat="server" class="textbox_default" 
                          AutoPostBack="true" ontextchanged="PnlAddNewProjectEnd_TextChanged" ></asp:TextBox>
                        <act:CalendarExtender ID="PnlAddNewProjectEndCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlAddNewProjectEnd" />
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Lama Project</td>
                <td></td>
                <td>
                  <asp:Label ID="PnlAddnewLblLamaProject" runat="server" Text="0"></asp:Label>&ensp;hari
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Tgl Jaminan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="PnlAddNewWarrantyStart" runat="server" class="textbox_default"></asp:TextBox>
                        <act:CalendarExtender ID="PnlAddNewWarrantyStartCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlAddNewWarrantyStart" />
                      </td>
                      <td> - </td>
                      <td>
                        <asp:TextBox ID="PnlAddNewWarrantyEnd" runat="server" class="textbox_default"></asp:TextBox>
                        <act:CalendarExtender ID="CalendarPnlAddNewWarrantyEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlAddNewWarrantyEnd" />
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Keterangan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlAddNewTxtKeterangan" runat="server" CssClass="textbox_default" Width="255px" TextMode="MultiLine" />
                </td>
              </tr>
            </table><br />
            <div style="text-align:right">
              <asp:Button ID="PnlAddNewBtnAdd" runat="server" Text="Simpan" onclick="PnlAddNewBtnAdd_Click" />&ensp;&ensp;
              <asp:LinkButton ID="PnlViewAddNewLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
              <asp:Button ID="PnlViewAddNewBtnClose" runat="server" Text="Batal" /><br />
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlDetailModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlDetailLinkButton" CancelControlID="PnlDetailBtnClose" DropShadow="false" PopupControlID="PanelDetail" PopupDragHandleControlID="PnlDetail" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PanelDetail" runat="server" Width="520px" CssClass="modalPopup" >
        <div style="padding:10px">
          <asp:Panel ID="Panel3" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td class="">
                    <img src="../../images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
                  </td>
                  <td class="">
                    &ensp;<asp:Label ID="PnlDetailLblTitleBar" runat="server" Text="Detail Data" />
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:10px"> 
            <div style="padding:5px">
              <table>
                <tr>
                  <td class="tableFieldHeader">No Project</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblNoProject" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tanggal</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblTanggal" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Customer</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblCustomer" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jenis Kapal</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblJenisKapal" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Jumlah</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblJmlKapal" runat="server" />
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tgl Project</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:Label ID="LblTglProjectStart" runat="server" />
                        </td>
                        <td>  -  </td>
                        <td>
                          <asp:Label ID="LblTglProjectEnd" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Lama Project</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="PnlDetailLblLamaProject" runat="server"  />&ensp;hari
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Tgl Jaminan</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <table>
                      <tr>
                        <td>
                          <asp:Label ID="LblTglJaminanStart" runat="server" />
                        </td>
                        <td> - </td>
                        <td>
                          <asp:Label ID="LblTglJaminanEnd" runat="server" />
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td class="tableFieldHeader">Keterangan</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:Label ID="LblKeterangan" runat="server" />
                  </td>
                </tr>
              </table>
            </div>           
            <div style="text-align: center">
              <asp:Button ID="PnlDetailBtnClose" runat="server" Text="Close" />
              <asp:LinkButton ID="PnlDetailLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlViewUpdateModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlViewUpdateLinkButton" CancelControlID="PnlViewUpdateBtnClose" DropShadow="false" PopupControlID="PanelViewUpdate" PopupDragHandleControlID="PnlViewUpdate" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PanelViewUpdate" runat="server" Width="520px" CssClass="modalPopup" >
        <div style="padding:10px">
          <asp:Panel ID="Panel2" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td>
                    <img src="../../images/icons/icons8-edit-property-48 (3).png" alt="icons8-edit-property-48 (3).png" />
                  </td>
                  <td class="">
                    &ensp;<asp:Label ID="Label1" runat="server" Text="Update Data" />
                    <asp:Label ID="LblHdId" runat="server" CssClass="" Visible="False" Style="display: none;"></asp:Label>
                  </td>
                </tr>
              </table>
            </div>
          </asp:Panel>
          <div style="padding:10px">
            <table>
              <tr>
                <td class="tableFieldHeader">Tanggal</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlUpdateDataTxtTanggal" runat="server" class="textbox_default" />
                  <act:CalendarExtender ID="CalPnlUpdateDataTxtTanggal" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlUpdateDataTxtTanggal" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Nomor Project</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlUpdateDataTxtNomorProject" runat="server" class="textbox_default" Width="255px"></asp:TextBox>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Customer</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="PnlUpdateDataTxtCustomer" runat="server" Width="255px" class="textbox_default" ReadOnly="true"></asp:TextBox>
                        <div style="display: none"><asp:Label ID="PnlUpdateLblId" runat="server" Text="" Visible="false" ></asp:Label></div>
                      </td>
                      <td><asp:Button ID="PnlUpdateBtnBrowse" runat="server" Text="..." onclick="PnlUpdateBtnBrowse_Click" /></td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jenis Kapal</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlUpdateDataTxtJenisKapal" runat="server" CssClass="textbox_default" Width="255px" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Jumlah Kapal</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlUpdateDataTxtJmlKapal" runat="server" CssClass="textbox_default" />
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Tgl Project</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="PnlUpdateDataTxtProjectStart" runat="server" 
                          class="textbox_default" AutoPostBack="true" 
                          ontextchanged="PnlUpdateDataTxtProjectStart_TextChanged" />
                        <act:CalendarExtender ID="CalPnlUpdateDataTxtProjectStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlUpdateDataTxtProjectStart" />
                      </td>
                      <td>&ensp;-&ensp;</td>
                      <td>
                        <asp:TextBox ID="PnlUpdateDataTxtProjectEnd" runat="server" 
                          class="textbox_default" AutoPostBack="true" 
                          ontextchanged="PnlUpdateDataTxtProjectEnd_TextChanged" />
                        <act:CalendarExtender ID="CalPnlUpdateDataTxtProjectEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlUpdateDataTxtProjectEnd" />
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Lama Project</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:Label ID="PnlUpdateLblLamaProject" runat="server" />&ensp;hari
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Tgl Jaminan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <asp:TextBox ID="PnlUpdateDataTxtWarrantyStart" runat="server" class="textbox_default"></asp:TextBox>
                        <act:CalendarExtender ID="CalPnlUpdateDataTxtWarrantyStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlUpdateDataTxtWarrantyStart" />
                      </td>
                      <td>&ensp;-&ensp;</td>
                      <td>
                        <asp:TextBox ID="PnlUpdateDataTxtWarrantyEnd" runat="server" class="textbox_default"></asp:TextBox>
                        <act:CalendarExtender ID="CalPnlUpdateDataTxtWarrantyEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="Right" TargetControlID="PnlUpdateDataTxtWarrantyEnd" />
                      </td>
                    </tr>
                  </table>                  
                </td>
              </tr>
              <tr>
                <td class="tableFieldHeader">Keterangan</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="PnlUpdateDataTxtKeterangan" runat="server" CssClass="textbox_default" TextMode="MultiLine" Width="255px" />
                </td>
              </tr>
            </table><br />
            <div style="text-align:right">
              <asp:Button ID="PnlUpdateBtnUpdate" runat="server" Text="Simpan" onclick="PnlUpdateBtnUpdate_Click"  />&ensp;&ensp;
              <asp:LinkButton ID="PnlViewUpdateLinkButton" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
              <asp:Button ID="PnlViewUpdateBtnClose" runat="server" Text="Batal" /><br />
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
  <%--Panel Browse Barang--%>
  <act:ModalPopupExtender ID="PnlBrowseCustModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseCustLinkButton" PopupControlID="PnlBrowseCust" PopupDragHandleControlID="PnlBrowseCustTitlebar" BackgroundCssClass="modalBackground" Drag="true" CancelControlID="PnlBrowseCustBtnClose"  />
  <asp:Panel ID="PnlBrowseCust" runat="server" Width="70%" CssClass="modalPopup" >
  <div style="padding: 5px">
    <asp:Panel ID="PnlBrowseCustTitlebar" runat="server" CssClass="modalPopupTitle">
      <div style="padding:5px; text-align:left">
        <table>
            <tr>
              <td class="">
                <img src="../../images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
              </td>
              <td class="">
                <asp:Label ID="PnlBrowseCustLblTitlebar" runat="server" Text="Browse Customer" />
              </td>
            </tr>
          </table>            
      </div>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
      <ContentTemplate>
        <div style="padding:10px; text-align:left">
          <div>
            <table>
              <tr>
                <td class="tableFieldHeader">Nama Customer</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtSearchCust1" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                <td style="width:20px"></td>
                <td>
                  <asp:Button ID="BtnSearchCust1" runat="server" Text="Cari" onclick="BtnSearchCust1_Click" />
                </td>
              </tr>
            </table>
          </div><br />
          <telerik:RadGrid ID="GridCust" runat="server" DataSourceID="SalesCustDataSource" GridLines="None" AllowPaging="True" onitemcommand="GridCust_ItemCommand" AllowSorting="True" PageSize="7">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SalesCustDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" DataType="System.UInt64" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama" SortExpression="nama" UniqueName="nama" />
                <telerik:GridBoundColumn DataField="npwp" HeaderText="NPWP" SortExpression="npwp" UniqueName="npwp" />
                <telerik:GridBoundColumn DataField="alamat" HeaderText="Alamat" SortExpression="alamat" UniqueName="alamat" />
                <telerik:GridBoundColumn DataField="telp" HeaderText="Telp" SortExpression="telp" UniqueName="telp" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="email" HeaderText="Email" SortExpression="email" UniqueName="email" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>        
    <div style="padding-top:10px; text-align:center">
      <asp:LinkButton ID="PnlBrowseCustLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
      <asp:Button ID="PnlBrowseCustBtnClose" runat="server" Text="Close" onclick="PnlBrowseCustBtnClose_Click" /><br />
    </div>
  </div>
  </asp:Panel>
  
  
  <%--Panel Browse Barang--%>
  <act:ModalPopupExtender ID="PnlBrowseCust2ModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBrowseCust2LinkButton" DropShadow="false" PopupControlID="PnlBrowseCust2" PopupDragHandleControlID="PnlBrowseCust2Titlebar" BackgroundCssClass="modalBackground" />
  <asp:Panel ID="PnlBrowseCust2" runat="server" Width="90%" CssClass="modalPopup" >
  <div style="padding: 5px">
    <asp:Panel ID="PnlBrowseCust2Titlebar" runat="server" CssClass="modalPopupTitle">
      <div style="padding:5px; text-align:left">
        <table>
            <tr>
              <td class="">
                <img src="../../images/icons/icons8-search-property-48.png" alt="icons8-search-property-48.png" />
              </td>
              <td class="">
                <asp:Label ID="PnlBrowseCust2LblTitlebar" runat="server" Text="Browse Customer" />
              </td>
            </tr>
          </table>            
      </div>
    </asp:Panel>
    <div style="padding:10px; text-align:left">
      <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
          <div>
            <table>
              <tr>
                <td class="tableFieldHeader">&nbsp;Nama Customer</td>
                <td class="tableFieldHeader">:</td>
                <td><asp:TextBox ID="TxtSearchCust2" runat="server" CssClass="textbox_default"></asp:TextBox></td>
                <td style="width:20px"></td>
                <td><asp:Button ID="BtnSearchCust2" runat="server" Text="Cari" onclick="BtnSearchCust2_Click" /></td>
              </tr>
            </table>
          </div><br />
          <telerik:RadGrid ID="GridCust2" runat="server" AllowPaging="True" DataSourceID="SalesCustDataSource" GridLines="None" onitemcommand="GridCust2_ItemCommand" AllowSorting="True" PageSize="7">
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="SalesCustDataSource">
              <RowIndicatorColumn>
                <HeaderStyle Width="20px" />
              </RowIndicatorColumn>
              <ExpandCollapseColumn>
                <HeaderStyle Width="20px" />
              </ExpandCollapseColumn>
              <Columns>
                <telerik:GridBoundColumn DataField="id" DataType="System.UInt64" HeaderText="id" ReadOnly="True" SortExpression="id" UniqueName="id" Visible="false" />
                <telerik:GridBoundColumn DataField="nama" HeaderText="Nama" SortExpression="nama" UniqueName="nama" />
                <telerik:GridBoundColumn DataField="npwp" HeaderText="NPWP" SortExpression="npwp" UniqueName="npwp" />
                <telerik:GridBoundColumn DataField="alamat" HeaderText="Alamat" SortExpression="alamat" UniqueName="alamat" />
                <telerik:GridBoundColumn DataField="telp" HeaderText="Telp" SortExpression="telp" UniqueName="telp" ItemStyle-HorizontalAlign="Center" />
                <telerik:GridBoundColumn DataField="email" HeaderText="Email" SortExpression="email" UniqueName="email" ItemStyle-HorizontalAlign="Center" />
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
              <Selecting AllowRowSelect="True" />
            </ClientSettings>
          </telerik:RadGrid>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <div style="padding-top:10px; text-align:center">
      <asp:LinkButton ID="PnlBrowseCust2LinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
      <asp:Button ID="PnlBrowseCust2BtnClose" runat="server" Text="Close" onclick="PnlBrowseCust2BtnClose_Click"  /><br />
    </div>
  </div>
  </asp:Panel>
</asp:Content>
