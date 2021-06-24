<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Order.List" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>List Work Order - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="/images/icons/icons8-bill-48.png" alt="icons8-bill-48.png"  />&ensp;<asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
      </div><br /> 
      <asp:Panel ID="PnlContent1" runat="server" HorizontalAlign="Center">
        <asp:Label ID="Label5" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
      </asp:Panel>
      <asp:Panel ID="PnlContent2" runat="server" Visible="false">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                <td class="tableFieldHeader">Tanggal</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <table>
                    <tr>
                      <td>
                        <telerik:RadDatePicker ID="RadPickerTanggal1" runat="server" />
                      </td>
                      <td>&ensp;s/d&ensp;</td>
                      <td>
                        <telerik:RadDatePicker ID="RadPickerTanggal2" runat="server" />
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              </asp:Panel>
              <asp:Panel ID="PnlStatus" runat="server" Visible="false">
              <tr>
                <td class="tableFieldHeader">Status</td>
                <td class="tableFieldHeader">:</td>
                <td class="tableFieldHeader" colspan="3">
                  <asp:DropDownList ID="DlistStatus" runat="server">
                    <asp:ListItem Value="WO1">Baru</asp:ListItem>
                    <asp:ListItem Value="WO2">Pending</asp:ListItem>
                    <asp:ListItem Value="WO3">Disetujui</asp:ListItem>
                    <asp:ListItem Value="WO5">Selesai</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              </asp:Panel> 
              <tr>
                <td class="tableFieldHeader">Kategori</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:DropDownList ID="DlistBerdasarkan" runat="server" AutoPostBack="True" onselectedindexchanged="DlistBerdasarkan_SelectedIndexChanged">
                    <asp:ListItem Value="1">No WO</asp:ListItem>
                    <asp:ListItem Value="2">Supplier</asp:ListItem>
                    <asp:ListItem Value="3">Jasa</asp:ListItem>
                    <asp:ListItem Value="4">Status</asp:ListItem>
                    <asp:ListItem Value="5">Tgl WO</asp:ListItem>
                  </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td colspan="3" style="padding-top: 10px">
                  <asp:Button ID="BtnSearch" runat="server" Text="Cari" onclick="BtnSearch_Click" />&ensp;
                  <asp:Button ID="BtnCancel" runat="server" Text="Batal" onclick="BtnCancel_Click" />
                </td>
              </tr>
            </table><br />
          <div style="text-align: right">
            <asp:Button ID="BtnInputPO" runat="server" Text="WO Baru (+)" onclick="BtnInputPO_Click" />
          </div><br />
          <telerik:RadGrid ID="GridWO" runat="server" AllowPaging="True" 
              AllowSorting="True" GridLines="None" onitemdatabound="GridWO_ItemDataBound" 
              onitemcommand="GridPurchaseOrder_ItemCommand" DataSourceID="WODataSource">
            <MasterTableView AutoGenerateColumns="False" DataSourceID="WODataSource">
              <Columns>
                <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" 
                  UniqueName="id" ItemStyle-HorizontalAlign="Center" Visible="false" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nomerwo" HeaderText="No." 
                  SortExpression="nomerwo" UniqueName="nomerwo" 
                  ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" >
                  <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="tglwo" HeaderText="Tanggal" 
                  SortExpression="tglwo" UniqueName="tglwo" DataType="System.DateTime" 
                  DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama" />
                <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" />
                <telerik:GridBoundColumn DataField="currency" HeaderText="Mata Uang" SortExpression="urrency" UniqueName="currency" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "currency")%> <%# String.Format("{0,20:N2}", DataBinder.Eval(Container.DataItem, "totalbiaya"))%>
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>               
                <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unitkerja" 
                  SortExpression="unitkerja" UniqueName="unitkerja" 
                  ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                  SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="Dibuat Oleh" UniqueName="dibuatoleh" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <asp:Label ID="LblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdby")%>' /><br />
                    (<asp:Label ID="LblTglDibuat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datecreated")%>' />)
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateButtonColumn" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                    <asp:Image ID="ImgIsLocked" runat="server" Width="25px" Height="25px" Visible="false" />
                    <asp:Button ID="BtnPrint" runat="server" Text="Print" CommandName="PrintClick" Visible="false" />
                    <asp:Button ID="BtnUpdate" runat="server" Text="Edit" CommandName="UpdateClick" Visible="false" />
                    <asp:Button ID="BtnApprove" runat="server" Text="Approve" CommandName="ApproveClick" Visible="false" />
                    <asp:Button ID="BtnClose" runat="server" Text="Close" CommandName="CloseClick" Visible="false" />
                    <asp:Button ID="BtnBatal" runat="server" Text="Batal" CommandName="BatalClick" Visible="false" />
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
              </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
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
                  <li>Klik 2x pada baris/row untuk melihat "Detail Work Order".</li>
                  <li>Button Close berfungsi untuk menutup WO ketika WO telah selesai dikerjakan.</li>
                  <li>Untuk melakukan pembatalan Work Order, saat WO telah Disetujui harap hubungi 
                    IT.</li>
                </ul>
              </td>
            </tr>
          </table>          
        </div>
      </asp:Panel>      
    </div>
  </div>
  <asp:ObjectDataSource ID="WODataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByTypeUnitKerja" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wo01TableAdapter" 
    oninit="WODataSource_Init">
    <SelectParameters>
      <asp:Parameter DefaultValue="NP" Name="type" Type="String" />
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
  
  <%--Panel Tips--%>
  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlMessageModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlMessageLinkButton" CancelControlID="PnlMessageBtnOk" PopupControlID="PnlMessage" PopupDragHandleControlID="PnlMessageTitlebar" BackgroundCssClass="modalBackground" />
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
    
    <%--Panel Tips--%>
  <act:ModalPopupExtender ID="PnlPrintModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlPrintLinkButton"  CancelControlID="PnlPrintBtnOk" DropShadow="false" PopupControlID="PnlPrint" PopupDragHandleControlID="PnlPrintTitlebar" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="PnlPrint" runat="server" Width="480px" CssClass="modalPopup">
      <div>
        <asp:Panel ID="PnlPrintTitlebar" runat="server" CssClass="modalPopupTitle">
          <div style="padding:5px; text-align:left">
            <asp:Label ID="PnlPrintLblTitlebar" runat="server" Text="Message Box" />
          </div>
        </asp:Panel>
        <div style="padding:5px; text-align:left">
          <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
              <table>
                <tr>
                  <td style="padding: 5px">
                   <img src="/images/icons/icons8-print-48.png" alt="iicons8-print-48.png"  />
                  </td>
                  <td style="padding: 5px">
                    <asp:Label ID="Label1" runat="server" Text="Cetak Work Order "  />
                    <asp:HiddenField ID="PnlPrintHdIdWO" runat="server" />
                    <asp:Label ID="PnlPrintLblNoWO" runat="server"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="?"></asp:Label>
                  </td>
                </tr>
              </table>
            </ContentTemplate>            
          </asp:UpdatePanel>          
          <div style="text-align: center; padding-top: 10px">
            <asp:Button ID="PnlPrintBtnPrint" runat="server" Text="Oke" onclick="PnlPrintBtnPrint_Click" />&ensp;&ensp;
            <asp:Button ID="PnlPrintBtnOk" runat="server" Text="Close"   />
            <asp:LinkButton ID="PnlPrintLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
          </div>
        </div>
      </div>
    </asp:Panel>

  <%--Panel Message--%>
  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlCloseModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlCloseLinkButton" CancelControlID="PnlCloseBtnClose" DropShadow="false" PopupControlID="PnlClose" PopupDragHandleControlID="PnlCloseTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlClose" runat="server" Width="480px" CssClass="modalPopup">
        <div>
          <asp:Panel ID="PnlCloseTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <asp:Label ID="PnlCloseLblTitlebar" runat="server" Text="Message Box" />
            </div>
          </asp:Panel>
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td style="padding: 5px">
                 <img src="/images/icons/icons8-task-completed-48.png" alt="icons8-task-completed-48.png"  />
                </td>
                <td style="padding: 5px; font-size:15px" valign="middle">
                  <asp:Label ID="Label3" runat="server" Text="Apakah nomer WO "></asp:Label>
                  <asp:HiddenField ID="PnlCloseHdIdWO" runat="server" />
                  <asp:Label ID="PnlCloseLblNoWO" runat="server" />                  
                  <asp:Label ID="Label4" runat="server" Text=" telah selesai ?"></asp:Label>
                </td>
              </tr>
            </table>
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlCloseBtnOk" runat="server" Text="Oke" onclick="PnlCloseBtnOk_Click" />&ensp;&ensp;
              <asp:Button ID="PnlCloseBtnClose" runat="server" Text="Cancel"  />
              <asp:LinkButton ID="PnlCloseLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>  
    
  <%--Panel Print--%>
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlApproveModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlApproveLinkButton" CancelControlID="PnlApproveBtnClose" DropShadow="false" PopupControlID="PnlApprove" PopupDragHandleControlID="PnlApproveTitlebar" BackgroundCssClass="modalBackground" />
      <asp:Panel ID="PnlApprove" runat="server" Width="480px" CssClass="modalPopup">
        <div>
          <asp:Panel ID="PnlApproveTitlebar" runat="server" CssClass="modalPopupTitle">
            <div style="padding:5px; text-align:left">
              <asp:Label ID="PnlApproveLblTitlebar" runat="server" Text="Message Box" />
            </div>
          </asp:Panel>
          <div style="padding:5px; text-align:left">
            <table>
              <tr>
                <td style="padding: 5px">
                 <img src="/images/icons/icons8-approval-48.png" alt="icons8-approval-48.png"  />
                </td>
                <td style="padding: 5px; font-size:15px" valign="middle">
                  <asp:Label ID="Label6" runat="server" Text="Nomor WO "></asp:Label>
                  <asp:HiddenField ID="PnlApproveHdIdWO" runat="server" />
                  <asp:Label ID="PnlApproveLblNoWO" runat="server" />
                  <asp:Label ID="Label8" runat="server" Text=" ingin disetujui?"></asp:Label>
                </td>
              </tr>
            </table>
            <div style="text-align: center; padding-top: 10px">
              <asp:Button ID="PnlApproveBtnOk" runat="server" Text="Ya" onclick="PnlApproveBtnOk_Click"  />&ensp;&ensp;
              <asp:Button ID="PnlApproveBtnClose" runat="server" Text="Batal"  />
              <asp:LinkButton ID="PnlApproveLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
            </div>
          </div>
        </div>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
  
    
  <%--Panel Close--%>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <act:ModalPopupExtender ID="PnlBatalModalPopupExtender" runat="server" Enabled="True" TargetControlID="PnlBatalLinkButton" CancelControlID="PnlBatalBtnClose" DropShadow="false" PopupControlID="PnlBatal" PopupDragHandleControlID="PnlBatalTitlebar" BackgroundCssClass="modalBackground" />
        <asp:Panel ID="PnlBatal" runat="server" Width="480px" CssClass="modalPopup">
          <div>
            <asp:Panel ID="PnlBatalTitlebar" runat="server" CssClass="modalPopupTitle">
              <div style="padding:5px; text-align:left">
                <asp:Label ID="PnlBatalLblTitlebar" runat="server" Text="Message Box" />
              </div>
            </asp:Panel>
            <div style="padding:5px; text-align:left">
              <table>
                <tr>
                  <td style="padding: 5px">
                   <img src="/images/icons/icons8-answers-48.png" alt="icons8-answers-48.png"  />
                  </td>
                  <td style="padding: 5px; font-size:15px" valign="middle">
                    <asp:Label ID="Label7" runat="server" Text="Ingin membatalkan nomer "></asp:Label>
                    <asp:HiddenField ID="PnlBatalHdIdWO" runat="server" />
                    <asp:Label ID="PnlBatalLblNoWO" runat="server" />
                    <asp:Label ID="Label10" runat="server" Text=" ?"></asp:Label>
                  </td>
                </tr>
              </table>
              <div style="text-align: center; padding-top: 10px">
                <asp:Button ID="PnlBatalBtnOk" runat="server" Text="Ya" onclick="PnlBatalBtnOk_Click"  />&ensp;&ensp;
                <asp:Button ID="PnlBatalBtnClose" runat="server" Text="Batal"  />
                <asp:LinkButton ID="PnlBatalLinkButton" runat="server" Style="display: none">LinkButton</asp:LinkButton>
              </div>
            </div>
          </div>
        </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
