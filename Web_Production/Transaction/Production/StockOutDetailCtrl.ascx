<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockOutDetailCtrl.ascx.cs" Inherits="Transaction_Production_StockOutDetailCtrl" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<table border="0" cellspacing="0" cellpadding="0" width="1000" >

        <tr >
            <td  style="height:40px; width: 50px;"></td>
            <td style="height:40px; width: 150px;">วันที่จ่ายออก</td>
            <td style=" height:40px; width: 800px;">
                <uc1:DatePickerControl ID="PkSendFgDate" runat="server" />
                <asp:Label ID="lable1" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                &nbsp; &nbsp;<asp:Button ID="btnStockOut" runat="server" CssClass="zComboBox"
                    Height="29px" OnClick="btnStockOut_Click" Text="จ่ายออกจากคลังกักกัน" Width="148px" /></td>  
        </tr>
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">ยอดเสีย</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtLost" runat="server" CssClass="zTextBoxR-View" ReadOnly="True" Width="125px"></asp:TextBox>
                <asp:Label ID="Label1" runat ="server" Font-Bold="True" ForeColor="white">*</asp:Label>
                <asp:DropDownList  ID="cmbUnitLost" runat="server" CssClass="zComboBox" Width="141px" Enabled="False"></asp:DropDownList></td> 
        </tr>
        
        <tr>
            <td style="height:25px; width: 50px;"></td>
            <td style="height:25px; width: 150px;">ยอดจ่าย</td>
            <td style=" height:25px; width: 800px;">
                <asp:TextBox ID="txtSendFgQty" runat="server" CssClass="zTextBox" Width="125px"></asp:TextBox>
                <asp:Label ID="Label3" runat ="server" Font-Bold="True" ForeColor="Red">*</asp:Label>
                <asp:DropDownList  ID="cmbUnitSendFg" runat="server" CssClass="zComboBox" Width="141px" Enabled="False"></asp:DropDownList></td> 
        </tr>
        
        <tr>
            <td style=" width: 50px;"></td>
            <td style="width: 150px; vertical-align :top">หมายเหตุ</td>
            <td style=" width: 800px;">
                <asp:TextBox ID="txtSendFgRemark" runat="server"  CssClass="zTextBox"  Width="626px" Height="99px" TextMode="MultiLine"></asp:TextBox></td> 
        </tr>
        
        <tr>
            <td style=" width: 50px;"></td>
            <td style="width: 150px">
                <asp:TextBox ID="txtPdpLoid" runat ="server" Visible ="false"  ></asp:TextBox>
                <asp:TextBox ID="txtPoLoid" runat ="server" Visible ="false"  ></asp:TextBox>
            </td>
            <td style=" width: 800px;">
                <asp:TextBox ID="txtPdLoid" runat ="server" Visible ="false" Width="58px"  ></asp:TextBox>
                <asp:TextBox ID="txtULoid" runat ="server" Visible ="false" Width="61px"  ></asp:TextBox>
                <asp:TextBox ID="txtPRODSTATUS" runat="server" CssClass="zHidden" Width="44px"></asp:TextBox>
                <asp:TextBox ID="txtPOSTATUS" runat="server" CssClass="zHidden" Width="37px"></asp:TextBox>
                <asp:TextBox ID="txtQcResult" runat="server" CssClass="zHidden" Width="37px"></asp:TextBox>
            </td>        
        </tr>
        
    </table>