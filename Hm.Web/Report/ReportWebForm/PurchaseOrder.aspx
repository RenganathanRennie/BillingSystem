<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="Hm.Web.Report.ReportWebForm.PurchaseOrder" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    
          
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="614px" Width="1238px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportEmbeddedResource="Hm.Web.Report.RDLC.PurchaseOrder.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="PurchaseOrderDataset" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        
    
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataSet1TableAdapters."></asp:ObjectDataSource>
    
       
        </form>
       
</body>
</html>
