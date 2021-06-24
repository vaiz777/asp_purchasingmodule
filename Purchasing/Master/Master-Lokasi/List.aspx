<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.Master.Master_Lokasi.List" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>List Lokasi | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="padding: 10px">
  <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
    <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; ">
      <img src="../../../images/icons/icons8-product-48.png" alt="icons8-time-card-48.png" />&nbsp;Daftar 
      Lokasi</div>
    <asp:Panel ID="PnlData1" runat="server" HorizontalAlign="Center">
      <asp:Label ID="Label5" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="PnlData2" runat="server" Visible="false">
      <div style="padding-bottom: 15px; ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <%--<table>
              <tr>
                <td class="tableFieldHeader">Cari Lokasi</td>
                <td class="tableFieldHeader">:</td>
                <td>
                  <asp:TextBox ID="TxtKataKunci" runat="server"></asp:TextBox>
                </td>
                <td>
                  <asp:Button ID="BtnSearch" runat="server" Text="Search" onclick="BtnSearchClick" />
                </td>
              </tr>
            </table>--%>
            <div class="tableFieldButton">
              <asp:Button ID="BtnTambah" runat="server" Text="Tambah Lokasi (+)" 
                onclick="BtnTambah_Click"  /><br /><br />
            </div>            
            <telerik:RadGrid ID="GridLokasi" runat="server" AllowPaging="True" 
              AllowSorting="True" DataSourceID="LokasiDataSource" GridLines="None">
              <MasterTableView autogeneratecolumns="False" datasourceid="LokasiDataSource" 
                DataKeyNames="id">
                <Columns>
                  <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" 
                    SortExpression="id" UniqueName="id" DataType="System.Int32" Visible="false" />
                  <telerik:GridBoundColumn DataField="inisial" HeaderText="Kode" 
                    SortExpression="inisial" UniqueName="inisial" 
                    ItemStyle-HorizontalAlign="Center" />
                  <telerik:GridBoundColumn DataField="nama" HeaderText="Lokasi" 
                    SortExpression="nama" UniqueName="nama" />
                  <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                    SortExpression="unitkerja" UniqueName="unitkerja" ItemStyle-HorizontalAlign="Center" />
                </Columns>
              </MasterTableView>
              <HeaderStyle HorizontalAlign="Center" />
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </asp:Panel>
  </div>
</div>
  <asp:ObjectDataSource ID="LokasiDataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByUnitKerja" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_lokasiTableAdapter">
    <SelectParameters>
      <asp:SessionParameter Name="unitkerja" SessionField="UnitKerja" Type="String" />
    </SelectParameters>
  </asp:ObjectDataSource>
</asp:Content>
