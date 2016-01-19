<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ABB Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="ctlReportViewer" runat="server" AutoDataBind="false" Width="100%" Height="100%"
            DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" EnableDrillDown="False" HasToggleGroupTreeButton="False" PrintMode="ActiveX" HasCrystalLogo="False" HasSearchButton="False" 
            HasViewList="False"/>
    </div>
    </form>
</body>
</html>
