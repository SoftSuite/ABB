<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="Transaction_Production_TabControl" %>
<asp:Panel ID="pnlFirst" runat="server" Width="100%">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr style="height:23px">
            <td width="4px">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnQCV" runat="server" Text="ส่งตรวจวิเคราะห์คุณภาพ" Width="210px" CssClass="tabtext" OnClick="btnQCV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnRawMaterialLossV" runat="server" Text="ความสูญเสียวัตถุดิบ" Width="180px" CssClass="tabtext" OnClick="btnRawMaterialLossV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnPackLossV" runat="server" Text="ความสูญเสียบรรจุภัณฑ์" Width="200px" CssClass="tabtext" OnClick="btnPackLossV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnExportV" runat="server" Text="จ่ายออกจากคลังกักกัน" Width="195px" CssClass="tabtext" OnClick="btnExportV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td width="100%"></td> 
        </tr> 
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="2px" style="background-image: url(../Images/tB.png); background-repeat: repeat-x; background-color: transparent; height: 25px;">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.RawMaterialUsing.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.RawMaterialUsing.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:Label ID="lblRawMaterialUsing" runat="server" Text="การใช้วัตถุดิบ" Width="150px"></asp:Label>
                <asp:LinkButton ID="btnRawMaterialUsing" runat="server" Text="การใช้วัตถุดิบ" Width="150px" Visible="False" CssClass="tabtext" OnClick="btnRawMaterialUsing_Click"></asp:LinkButton>
            </td>
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.RawMaterialUsing.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Pack.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Pack.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:Label ID="lblPack" runat="server" Text="การบรรจุ" Width="130px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnPack" runat="server" Text="การบรรจุ" Width="130px" CssClass="tabtext" OnClick="btnPack_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Pack.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.X_RaySending.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.X_RaySending.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:Label ID="lblX_RaySending" runat="server" Text="ส่งไปฉายรังสี" Width="150px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnX_RaySending" runat="server" Text="ส่งไปฉายรังสี" Width="150px" CssClass="tabtext" OnClick="btnX_RaySending_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.X_RaySending.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.X_RayReceiving.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.X_RayReceiving.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:Label ID="lblX_RayReceiving" runat="server" Text="รับคืนจากฉายรังสี" Width="180px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnX_RayReceiving" runat="server" Text="รับคืนจากฉายรังสี" Width="180px" CssClass="tabtext" OnClick="btnX_RayReceiving_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.X_RayReceiving.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Import.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Import.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent; height: 25px;" valign="middle" align="center">
                <asp:Label ID="lblImport" runat="server" Text="รับเข้าคลังกักกัน" Width="170px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnImport" runat="server" Text="รับเข้าคลังกักกัน" Width="170px" CssClass="tabtext" OnClick="btnImport_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Import.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right;">&nbsp;</td>
            
            <td width="100%" style="background-image: url(../Images/tB.png); background-repeat: repeat-x; background-color: transparent;"></td> 
        </tr> 
    </table>
</asp:Panel>
<asp:Panel ID="pnlSecond" runat="server" Width="100%" Visible="False">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr style="height:23px">
            <td width="4px">&nbsp;</td>
            
            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnRawMaterialUsingV" runat="server" Text="การใช้วัตถุดิบ" Width="150px" CssClass="tabtext" OnClick="btnRawMaterialUsingV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnPackV" runat="server" Text="การบรรจุ" Width="130px" CssClass="tabtext" OnClick="btnPackV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnX_RaySendingV" runat="server" Text="ส่งไปฉายรังสี" Width="150px" CssClass="tabtext" OnClick="btnX_RaySendingV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnX_RayReceivingV" runat="server" Text="รับคืนจากฉายรังสี" Width="180px" CssClass="tabtext" OnClick="btnX_RayReceivingV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td valign="top" width="6px" style="background-image: url(../Images/tNL.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/tN.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:LinkButton ID="btnImportV" runat="server" Text="รับเข้าคลังกักกัน" Width="170px" CssClass="tabtext" OnClick="btnImportV_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/tNR.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>

            <td width="100%"></td> 
        </tr> 
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="2px" style="background-image: url(../Images/tB.png); background-repeat: repeat-x; background-color: transparent; height: 25px;">&nbsp;</td>
                
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.QC.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.QC.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                <asp:Label ID="lblQC" runat="server" Text="ส่งตรวจวิเคราะห์คุณภาพ" Width="210px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnQC" runat="server" Text="ส่งตรวจวิเคราะห์คุณภาพ" Width="210px" CssClass="tabtext" OnClick="btnQC_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.QC.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.RawMaterialLoss.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent; ">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.RawMaterialLoss.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent; height: 25px;" valign="middle" align="center">
                <asp:Label ID="lblRawMaterialLoss" runat="server" Text="ความสูญเสียวัตถุดิบ" Width="180px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnRawMaterialLoss" runat="server" Text="ความสูญเสียวัตถุดิบ" Width="180px" CssClass="tabtext" OnClick="btnRawMaterialLoss_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.RawMaterialLoss.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right;">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.PackLoss.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.PackLoss.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent; height: 25px;" valign="middle" align="center">
                <asp:Label ID="lblPackLoss" runat="server" Text="ความสูญเสียบรรจุภัณฑ์" Width="205px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnPackLoss" runat="server" Text="ความสูญเสียบรรจุภัณฑ์" Width="205px" CssClass="tabtext" OnClick="btnPackLoss_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.PackLoss.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right;">&nbsp;</td>
            
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Export.Index ? "tSL" : "tBNL") %>.png); background-repeat: repeat-x; background-color: transparent;">&nbsp;</td>
            <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Export.Index ? "tS" : "tBN") %>.png); background-repeat: repeat-x; background-color: transparent; height: 25px;" valign="middle" align="center">
                <asp:Label ID="lblExport" runat="server" Text="จ่ายออกจากคลังกักกัน" Width="195px" Visible="False"></asp:Label>
                <asp:LinkButton ID="btnExport" runat="server" Text="จ่ายออกจากคลังกักกัน" Width="195px" CssClass="tabtext" OnClick="btnExport_Click"></asp:LinkButton>
            </td> 
            <td valign="top" align="center" width="6px" style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ProductionTab.Export.Index ? "tSR" : "tBNR") %>.png); background-repeat: repeat-x; background-color: transparent; background-position-x:right;">&nbsp;</td>
        
            <td width="100%" style="background-image: url(../Images/tB.png); background-repeat: repeat-x; background-color: transparent;"></td> 
        </tr> 
    </table>
</asp:Panel>
<asp:Label ID="lblTab" runat="server" CssClass="zHidden" Text="1"></asp:Label><asp:Label ID="lblRow" runat="server" CssClass="zHidden" Text="1"></asp:Label>