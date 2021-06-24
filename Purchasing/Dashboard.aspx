<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ManagementSystem.Purchasing.Dashboard" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register TagPrefix="act" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Dashboard - Purchasing | PT Tri Ratna Diesel Indonesia</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="padding:10px;">
    <div style="border: medium solid #CCCCCC; background-color: White; border-radius: 10px; padding: 15px">
      <div style="font-weight: normal; font-family: 'Trebuchet MS'; font-size: large">
          <img src="../images/icons/icons8-speedometer-48.png" alt="icons8-speedometer-48.png"  />&ensp;Dashboard
      </div><br />
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <telerik:RadTabStrip ID="RadTabStrip3" runat="server" Width="100%" SelectedIndex="0" MultiPageID="RadMultiPage3">
            <Tabs>
              <telerik:RadTab runat="server" Text="Barang" Selected="True"  />
              <telerik:RadTab runat="server" Text="Jasa" />
            </Tabs>
          </telerik:RadTabStrip>
          <telerik:RadMultiPage ID="RadMultiPage3" runat="server" Width="100%" SelectedIndex="0" BorderStyle="Solid" BorderColor="#999999" BorderWidth="1px" BackColor="White" >
            <telerik:RadPageView ID="RadPageView6" runat="server">
              <div style="padding: 10px">                  
                <%--Panel MR--%>
                <asp:Panel ID="PnlMR" runat="server">
                  <span style="font-family: Trebuchet MS; font-size:large">Material Request</span><br />
                  <asp:Label ID="LblInfoMR" runat="server"></asp:Label><br /><br />
                  <telerik:RadGrid ID="GridMR" runat="server" AllowPaging="True" 
                    AllowSorting="True" DataSourceID="MRDataSource" GridLines="None" 
                    onitemcommand="GridMR_ItemCommand" onitemdatabound="GridMR_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="MRDataSource">
                      <Columns>
                        <telerik:GridBoundColumn DataField="id" HeaderText="No MR" ReadOnly="True" 
                          SortExpression="id" UniqueName="id" ItemStyle-HorizontalAlign="Center" 
                          ItemStyle-Font-Bold="true" >
                          <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="reference" HeaderText="Reference" 
                          SortExpression="reference" UniqueName="reference" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="tanggal" DataType="System.DateTime" 
                          HeaderText="Tgl WR" SortExpression="tanggal" UniqueName="tanggal" 
                          ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="type" HeaderText="Type" 
                          SortExpression="type" UniqueName="type" ItemStyle-HorizontalAlign="Center" >                        
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                          SortExpression="unitkerja" UniqueName="unitkerja" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                          ItemStyle-HorizontalAlign="Center" SortExpression="status" UniqueName="status" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Requestor" DataField="requestor" UniqueName="requestor" ItemStyle-HorizontalAlign="Center">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "createdby")%><br />(<%# DataBinder.Eval(Container.DataItem, "datecreated")%>)
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                      </Columns>
                      <HeaderStyle HorizontalAlign="Center" />
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                      <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                  </telerik:RadGrid><br /><br />
                </asp:Panel>
                
                <%--Panel PO--%>
                <asp:Panel ID="PnlPO" runat="server" Visible="false">
                  <span style="font-family: Trebuchet MS; font-size:large">Purchase Order</span><br />
                  <asp:Label ID="LblInfoPO" runat="server"></asp:Label><br /><br />
                  <telerik:RadGrid ID="GridPO" runat="server" AllowPaging="True" 
                    AllowSorting="True" DataSourceID="PODataSource" GridLines="None" 
                    onitemcommand="GridPO_ItemCommand" onitemdatabound="GridPO_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="False" DataSourceID="PODataSource">
                      <RowIndicatorColumn>
                        <HeaderStyle Width="20px" />
                      </RowIndicatorColumn>
                      <ExpandCollapseColumn>
                        <HeaderStyle Width="20px" />
                      </ExpandCollapseColumn>
                      <Columns>
                        <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                        <telerik:GridBoundColumn DataField="nomerpo" HeaderText="No PO" 
                          SortExpression="nomerpo" UniqueName="nomerpo" 
                          ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" >
                          <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="tglpo" DataType="System.DateTime" 
                          HeaderText="Tgl" SortExpression="tglpo" UniqueName="tglpo" 
                          DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="barang_nama" HeaderText="Nama Barang" SortExpression="barang_nama" UniqueName="barang_nama" />
                        <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" />
                        <telerik:GridBoundColumn DataField="type" HeaderText="Type" 
                          SortExpression="type" UniqueName="type" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="currency" HeaderText="" 
                          SortExpression="currency" UniqueName="currency" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="totalpo" DataType="System.Decimal" 
                          HeaderText="Total" SortExpression="totalpo" UniqueName="totalpo" 
                          DataFormatString="{0,20:N2}" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                          SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                          SortExpression="unitkerja" UniqueName="unitkerja" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Requestor" DataField="requestor" UniqueName="requestor" ItemStyle-HorizontalAlign="Center">
                          <ItemTemplate>
                            <asp:Label ID="LblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdby")%>' /><br />
                            (<asp:Label ID="LblDateCreated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datecreated")%>' />)
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                      </Columns>
                      <HeaderStyle HorizontalAlign="Center" />
                    </MasterTableView>
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                      <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                  </telerik:RadGrid>
                </asp:Panel>
              </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView7" runat="server">
              <div style="padding: 10px">
                <%--Panel WR--%>
                <asp:Panel ID="PnlWR" runat="server">
                  <span style="font-family: Trebuchet MS; font-size:large">Work Request</span><br />
                  <asp:Label ID="LblInfoWR" runat="server"></asp:Label><br /><br />
                  <telerik:RadGrid ID="GridWR" runat="server" AllowPaging="True" 
                    AllowSorting="True" DataSourceID="WRDataSource" GridLines="None" 
                    onitemcommand="GridWR_ItemCommand" onitemdatabound="GridWR_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="id" 
                      DataSourceID="WRDataSource">
                      <RowIndicatorColumn>
                        <HeaderStyle Width="20px" />
                      </RowIndicatorColumn>
                      <ExpandCollapseColumn>
                        <HeaderStyle Width="20px" />
                      </ExpandCollapseColumn>
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
                        <telerik:GridBoundColumn DataField="tglwr" DataType="System.DateTime" 
                          HeaderText="Tgl WR" SortExpression="tglwr" UniqueName="tglwr" 
                          ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="type" HeaderText="Type" 
                          SortExpression="type" UniqueName="type" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                          SortExpression="unitkerja" UniqueName="unitkerja" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                          ItemStyle-HorizontalAlign="Center" SortExpression="status" UniqueName="status" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Requestor" DataField="requestor" UniqueName="requestor" ItemStyle-HorizontalAlign="Center">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "createdby")%><br />(<%# DataBinder.Eval(Container.DataItem, "datecreated")%>)
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                      </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" >
                      <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                  </telerik:RadGrid>
                  <br /><br />
                </asp:Panel>
                
                <%--Panel WO--%>
                <asp:Panel ID="PnlWO" runat="server" Visible="false">
                  <span style="font-family: Trebuchet MS; font-size:large">Work Order</span><br />
                  <asp:Label ID="LblInfoWO" runat="server"></asp:Label><br /><br />
                  <telerik:RadGrid ID="GridWO" runat="server" AllowPaging="True" 
                    AllowSorting="True" DataSourceID="WODataSource" GridLines="None" 
                    onitemcommand="GridWO_ItemCommand" onitemdatabound="GridWO_ItemDataBound">
                    <MasterTableView AutoGenerateColumns="False" DataSourceID="WODataSource">
                      <Columns>
                        <telerik:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" UniqueName="id" Visible="false" />
                        <telerik:GridBoundColumn DataField="nomerwo" HeaderText="No WO" 
                          SortExpression="nomerwo" UniqueName="nomerwo" 
                          ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" >
                          <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="tglwo" HeaderText="Tgl" 
                          SortExpression="tglwo" UniqueName="tglwo" ItemStyle-HorizontalAlign="Center" 
                          DataType="System.DateTime" DataFormatString="{0:d}" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="jasa_nama" HeaderText="Nama Jasa" SortExpression="jasa_nama" UniqueName="jasa_nama"  />                        
                        <telerik:GridBoundColumn DataField="supplier_nama" HeaderText="Supplier" SortExpression="supplier_nama" UniqueName="supplier_nama" />
                        <telerik:GridBoundColumn DataField="type" HeaderText="Type" 
                          SortExpression="type" UniqueName="type" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="currency" HeaderText="" 
                          SortExpression="currency" UniqueName="currency" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="totalbiaya" DataType="System.Decimal" 
                          HeaderText="Total" SortExpression="totalbiaya" UniqueName="totalbiaya" 
                          ItemStyle-HorizontalAlign="Center" DataFormatString="{0,20:N2}" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="status" HeaderText="Status" 
                          SortExpression="status" UniqueName="status" ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="unitkerja" HeaderText="Unit Kerja" 
                          SortExpression="unitkerja" UniqueName="unitkerja" 
                          ItemStyle-HorizontalAlign="Center" >
                          <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Requestor" DataField="requestor" UniqueName="requestor" ItemStyle-HorizontalAlign="Center">
                          <ItemTemplate>
                            <asp:Label ID="LblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdby")%>' /><br />
                            (<asp:Label ID="LblDateCreated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "datecreated")%>' />)
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
                </asp:Panel>
              </div>
            </telerik:RadPageView>
          </telerik:RadMultiPage>
        </ContentTemplate>
      </asp:UpdatePanel>      
    </div>
  </div>
  <asp:ObjectDataSource ID="WRDataSource" runat="server" 
     OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
     TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wr01TableAdapter" 
      oninit="WRDataSource_Init">
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="WODataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_wo01TableAdapter" 
    oninit="WODataSource_Init">
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="MRDataSource" runat="server" 
     OldValuesParameterFormatString="original_{0}" 
     SelectMethod="GetData"      
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_mr01TableAdapter" 
    oninit="MRDataSource_Init">
  </asp:ObjectDataSource>
  <asp:ObjectDataSource ID="PODataSource" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
    
    TypeName="TmsBackDataController.PurDataSetTableAdapters.vpur_po01TableAdapter" 
    oninit="PODataSource_Init">
  </asp:ObjectDataSource>
   
  
</asp:Content>
