<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockInDetailCtrl.ascx.cs" Inherits="Transaction_Production_StockInDetailCtrl" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<table border="0" cellspacing="0" cellpadding="0" width="1000" >

        <tr >
            <td  style="height:40px; width: 50px;"></td>
            <td style="height:40px; width: 150px;">วันที่รับเข้าคลัง</td>
            <td style=" height:40px; width: 800px;">
                <uc1:DatePickerControl ID="PkQuarantineDate" runat="server" />
                <asp:Label ID="lable1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnStockIn" runat="server" CssClass="zComboBox"
                    OnClick="btnStockIn_Click" Text="รับเข้าคลังกักกัน" Width="107px" Height="29px" /></td>  
        </tr>
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">จำนวนที่รับ</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtQuarantineQty" runat="server" CssClass="zTextBox" Width="123px"></asp:TextBox>
                <asp:Label ID="Label1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                <asp:DropDownList  ID="cmbQuarantineUnit" runat="server" CssClass="zComboBox" Width="141px" Enabled="False"></asp:DropDownList> </td> 
        </tr>
        <tr>
            <td style=" height:100px; width: 50px;"></td>
            <td style="height:100px; width: 150px; vertical-align :top">หมายเหตุ</td>
            <td style=" height:100px; width: 800px;">
                <asp:TextBox ID="txtQuarantineRemark" runat="server"  CssClass="zTextBox" Width="626px" Height="99px" TextMode="MultiLine"></asp:TextBox></td> 
        </tr>
        <tr>
            <td style=" width: 50px;"></td>
            <td colspan="2">
                <asp:TextBox ID ="txtPdpLoid" runat ="server" Visible ="false" Width="69px"></asp:TextBox>
                <asp:TextBox ID ="txtPoLoid" runat ="server" Width="59px" Visible="False"></asp:TextBox><asp:TextBox ID ="txtPdLoid" runat ="server" Visible ="false" Width="57px"></asp:TextBox>
                <asp:TextBox ID ="txtULoid" runat ="server" Visible ="false" Width="54px"></asp:TextBox>
                <asp:TextBox ID="txtPOSTATUS" runat="server" Width="50px" CssClass="zHidden"></asp:TextBox>
                <asp:TextBox ID="txtPRODSTATUS" runat="server" Width="58px" CssClass="zHidden"></asp:TextBox>
                <asp:TextBox ID="txtRadiateRetDate" runat="server" CssClass="zHidden" Width="58px"></asp:TextBox></td>
        </tr>
        
    </table>