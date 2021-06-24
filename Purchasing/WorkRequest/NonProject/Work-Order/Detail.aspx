<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="ManagementSystem.Purchasing.WorkRequest.NonProject.Work_Order.Detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Detail Work Order - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="padding: 10px">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px;">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; height: 36px;">
        <img src="/images/icons/icons8-view-48.png" alt="icons8-view-48.png" />&nbsp;
        <asp:Label ID="LblTitle" runat="server" Text=""></asp:Label><br />
      </div><br />
      <div style="text-align: right;" >
        <asp:Label ID="LblStatus" runat="server" Text="" style="font-size: larger; color: White; background-color: Maroon; padding: 10px;" ></asp:Label>
      </div>
      <table>
        <asp:Panel ID="Panel1" runat="server" Visible="false">
        <tr>
          <td class="tableFieldHeader">Nomer Unit</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblNomer" runat="server"></asp:Label>
          </td>
        </tr>
        </asp:Panel>        
        <tr>
          <td class="tableFieldHeader">No WO</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblNoWO" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Tgl WO</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblTglWO" runat="server"></asp:Label>
          </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Supplier / Vendor</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblSupplier" runat="server"></asp:Label>
          </td>
        </tr>
         <tr>
          <td class="tableFieldHeader">Tgl Pelaksanaan</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblTglPelaksanaan" runat="server"></asp:Label>
           </td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Pembayaran</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblPembayaran" runat="server"></asp:Label>
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
          <td class="tableFieldHeader">Unit Kerja</td>
          <td class="tableFieldHeader">:</td>
          <td>
            <asp:Label ID="LblUnitKerja" runat="server"></asp:Label>
          </td>
        </tr>
      </table><br />
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" DataKeyNames="id" 
        DataSourceID="WODetailDataSource" ForeColor="#333333" GridLines="None" 
        Width="100%">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
          <asp:BoundField DataField="wr_id" HeaderText="No WR" ReadOnly="True" SortExpression="wr_id" ItemStyle-HorizontalAlign="Center" />
          <asp:BoundField DataField="jasa_kode" HeaderText="Kode Jasa" SortExpression="jasa_kode" ItemStyle-HorizontalAlign="Center" />
          <asp:BoundField DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" />
          <asp:TemplateField HeaderText="Jml Jasa" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
              <%#DataBinder.Eval(Container.DataItem, "jmljasa")%> <%#DataBinder.Eval(Container.DataItem, "satuan")%>
            </ItemTemplate>                      
          </asp:TemplateField>
          <asp:BoundField DataField="currency" HeaderText="" SortExpression="currency" ItemStyle-HorizontalAlign="Center" />
          <asp:BoundField DataField="harga" HeaderText="Harga" SortExpression="harga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
          <asp:BoundField DataField="diskon" HeaderText="Diskon (%)" SortExpression="diskon" ItemStyle-HorizontalAlign="Center" />
          <asp:BoundField DataField="totalharga" HeaderText="Total Harga" SortExpression="totalharga" DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
      </asp:GridView>
      <br /><br />
      <table style="width: 100%; text-align: right; padding: 0.1px">
        <tr>
          <td style="width: 15%"></td>
          <td style="width: 15%"></td>
          <td style="width: 15%"></td>
          <td class="tableFieldHeader">Total Jasa</td>
          <td></td>
          <td class="tableFieldHeader"><asp:Label ID="PnlDetailWOLblCurr1" runat="server" /></td>
          <td class="tableFieldHeader"><asp:Label ID="PnlDetailWOTotalJasa" runat="server" /></td> 
        </tr>
        <asp:Panel ID="PnlKurs" runat="server" >                  
        <tr>
          <td style="width: 15%"></td>
          <td style="width: 15%"></td>
          <td style="width: 15%"></td>
          <td class="tableFieldHeader">Kurs</td>
          <td></td>
          <td><asp:Label ID="PnlDetailWOLblKurs" runat="server" /></td>
        </tr>
        </asp:Panel>
        <tr>
          <td style="width: 15%"></td>
          <td style="width: 15%"></td>
          <td style="width: 15%"></td>
          <td class="tableFieldHeader">Diskon</td>
          <td><asp:Label ID="PnlDetailWOLblDiskon" runat="server" /></td>
          <td><asp:Label ID="PnlDetailWoLblCurr2" runat="server" /></td>
          <td><asp:Label ID="PnlDetailWOLblTotalDiskon" runat="server" /></td>
        </tr>
        <tr>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td class="tableFieldHeader">PPn</td>
          <td><asp:Label ID="PnlDetailWOLblPPn" runat="server" /></td>
          <td><asp:Label ID="PnlDetailWOLblCurr3" runat="server" /></td>
          <td><asp:Label ID="PnlDetailWOLblTotalPPn" runat="server" /></td>
        </tr>
        <tr>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td class="tableFieldHeader">PPh</td>
          <td> <asp:Label ID="PnlDetailWOLblPPh" runat="server" /></td>
          <td><asp:Label ID="PnlDetailWOLblCurr4" runat="server" /></td>
          <td> <asp:Label ID="PnlDetailWOLblTotalPPh" runat="server" /></td>
        </tr>
        <tr>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td class="tableFieldHeader">Jasa Lain</td>
          <td> <asp:Label ID="PnlDetailWOLblJasaLain" runat="server" /></td>
          <td><asp:Label ID="PnlDetailWOLblCurr5" runat="server" /></td>
          <td> <asp:Label ID="PnlDetailWOLblBiayaJasaLain" runat="server" /></td>
        </tr>
        <tr>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td style="width: 15%"> </td>
          <td class="tableFieldHeader" style="background-color: Yellow">Total WO</td>
          <td style="background-color: Yellow"></td>
          <td class="tableFieldHeader" style="background-color: Yellow">
            <asp:Label ID="PnlDetailWOLblCurr7" runat="server"></asp:Label>
          </td>
          <td class="tableFieldHeader" style="background-color: Yellow"><asp:Label ID="PnlDetailWOLblTotalWO" runat="server" /></td>
        </tr>
      </table><br /><br />
      <table>
        <tr>
          <td class="tableFieldHeader">Notes</td>
          <td class="tableFieldHeader">:</td>
          <td><asp:Label ID="LblNotes" runat="server"></asp:Label></td>
        </tr>
        <tr>
          <td class="tableFieldHeader">Keterangan</td>
          <td class="tableFieldHeader">:</td>
          <td><asp:Label ID="LblKeterangan" runat="server"></asp:Label></td>
        </tr>
      </table><br /><br />
      <asp:Button ID="BtnBack" runat="server" Text="Kembali" onclick="BtnBack_Click" />
    </div>
  </div>
  
  <asp:ObjectDataSource ID="WODetailDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByWoId" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wodetail01TableAdapter">
    <SelectParameters>
      <asp:QueryStringParameter Name="woId" QueryStringField="pId" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
