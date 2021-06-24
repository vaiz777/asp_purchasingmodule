<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ManagementSystem.Purchasing.Master.Master_Kategori.List" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.20315, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Daftar Kategori | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="padding: 10px">
  <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
    <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large; ">
      <img src="../../../images/icons/icons8-product-48.png" alt="icons8-time-card-48.png" />&nbsp;Daftar Kategori
    </div><br />
    <asp:Panel ID="PnlData1" runat="server" HorizontalAlign="Center">
      <asp:Label ID="Label5" runat="server" Text="Maaf, anda tidak memiliki Akses untuk menu ini." Font-Size="Larger"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="PnlData2" runat="server" Visible="false">
      <div style="padding-bottom: 15px; ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <div style="padding:10px">
              <table>
                <tr>
                  <td class="tableFieldHeader">Cari Kategori</td>
                  <td class="tableFieldHeader">:</td>
                  <td>
                    <asp:TextBox ID="TxtKataKunci" runat="server" CssClass="textbox_default"></asp:TextBox>
                  </td>
                  <td>
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" onclick="BtnSearchClick" />
                  </td>
                </tr>
              </table>
            </div><br />
            <telerik:RadGrid ID="GridKategori" runat="server" AllowPaging="True" 
              AllowSorting="True" DataSourceID="KategoriDataSource" GridLines="None">
              <MasterTableView autogeneratecolumns="False" datasourceid="KategoriDataSource" 
                DataKeyNames="id">
                <Columns>
                  <telerik:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" 
                    SortExpression="id" UniqueName="id" DataType="System.Int32" Visible="false" />
                  <telerik:GridBoundColumn DataField="inisial" HeaderText="Kode" 
                    SortExpression="inisial" UniqueName="inisial" ItemStyle-HorizontalAlign="Center" />
                  <telerik:GridBoundColumn DataField="nama" HeaderText="Kategori" 
                    SortExpression="nama" UniqueName="nama" />
                </Columns>
              </MasterTableView>
              <HeaderStyle HorizontalAlign="Center" />
              <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                <Selecting AllowRowSelect="true" />
              </ClientSettings>
            </telerik:RadGrid>
            <div class="tableFieldButton">
              <asp:Button ID="BtnTambah" runat="server" Text="Tambah Kategori (+)" onclick="BtnTambah_Click"  />
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </asp:Panel>
  </div>
</div>
  <asp:ObjectDataSource ID="KategoriDataSource" runat="server" 
    DeleteMethod="Delete" InsertMethod="Insert" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    TypeName="TmsBackDataController.PurDataSetTableAdapters.pur_kategoriTableAdapter" 
    UpdateMethod="Update">
    <DeleteParameters>
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
      <asp:Parameter Name="Original_id" Type="Int32" />
      <asp:Parameter Name="Original_inisial" Type="String" />
      <asp:Parameter Name="Original_nama" Type="String" />
    </UpdateParameters>
    <InsertParameters>
      <asp:Parameter Name="inisial" Type="String" />
      <asp:Parameter Name="nama" Type="String" />
    </InsertParameters>
  </asp:ObjectDataSource>
</asp:Content>
