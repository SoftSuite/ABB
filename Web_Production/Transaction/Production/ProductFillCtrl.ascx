<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductFillCtrl.ascx.cs" Inherits="Transaction_Production_ProductFillCtrl" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>

<script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>

    <table border="0" cellspacing="0" cellpadding="0" width="1000px" >

        <tr >
            <td  style="width:50px; height:25px"></td>
            <td style="height:25px; width: 168px">เครื่องบรรจุ</td>
            <td colspan = "3"  style=" width:600px; height:25px">
                <asp:TextBox ID="txtPacking" runat="server" Width="600px" CssClass="zTextBox"></asp:TextBox></td> 
        </tr>
        <tr>
            <td style="width:50px; height:25px"></td>
            <td style="height:25px; width: 168px;">ภาชนะบรรจุ</td>
            <td style=" width:200px; height:25px">
                <asp:TextBox ID="txtPackAge" runat="server" Width="150px" CssClass="zTextBox"></asp:TextBox></td> 
            <td style="width:100px; height:25px">ขนาดบรรจุ</td>
            <td style=" width:500px; height:25px">
                <asp:TextBox ID="txtPackSize" runat="server" Width="80px" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox> 
                <asp:TextBox ID ="lblUname" runat ="server" Width ="158px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td  style="width:50px; height:25px"></td>
            <td style="height:25px; width: 168px">ผลผลิตตามทฤษฎี</td>
            <td style=" height:25px">
                &nbsp;<asp:TextBox ID="txtStdQty" runat="server" Width="80px" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtPDUNAME" runat="server" Width="80px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>&nbsp;</td> 
            
        </tr>
        <tr>
            <td  style="width:50px; height:25px"></td>
            <td style="height:25px; width: 168px">จำนวนที่บรรจุได้จริง</td>
            <td colspan = "3"  style=" width:100px; height:25px">
                <asp:TextBox ID="txtPdQty" runat="server" Width="80px" AutoPostBack="true" CssClass="zTextboxR" OnTextChanged="txtPdQty_TextChanged"></asp:TextBox></td> 
        </tr>  
        <tr>
            <td  style="width:50px; height:25px"></td>
            <td style="height:25px; width: 168px">จำนวนเสีย</td>
            <td colspan = "3"  style=" width:100px; height:25px">
                <asp:TextBox ID="txtLost" runat="server" Width="80px" CssClass="zTextboxR" ></asp:TextBox></td> 
        </tr>     
        <tr>
            <td  style="width:50px; height:25px"></td>
            <td style="height:25px; width: 168px">คิดเป็น % Yield</td>
            <td colspan = "3"  style=" width:100px; height:25px">
                <asp:TextBox ID="txtYield" runat="server" Width="80px" CssClass="zTextboxR-View" ReadOnly="True"></asp:TextBox></td> 
        </tr>  
        <tr style="height:20px">
            <td style="width:50px;"></td>
            <td colspan="4">
                <asp:TextBox ID ="txtPdpLoid" runat ="server" Visible ="false" ></asp:TextBox><asp:TextBox ID="txtPoLoid" runat="server"  Visible ="false" ReadOnly="True"></asp:TextBox><asp:TextBox ID="txtPdLoid" runat="server"  Visible ="false"></asp:TextBox><asp:TextBox ID="txtULoid" runat="server" Visible ="false"></asp:TextBox>
                <asp:TextBox ID="txtPRODSTATUS" runat="server" CssClass="zHidden" Width="28px"></asp:TextBox>
                <asp:TextBox ID="txtPOSTATUS" runat="server" CssClass="zHidden" Width="70px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="5">
                <table width="1000px" border="0" cellspacing="0" cellpadding="0">
                    <tr class="subheadertext">
                        <td colspan = "5">การปิดฉลาก</td> 
                    </tr> 
                    <tr style="height:20px">
                        <td colspan = "5"></td> 
                    </tr> 
                    <tr style="height:25px">
                        <td style="width:50px;"></td>
                        <td style="width:100px">วันที่ผลิต</td>
                        <td style="width:200px">
                            <uc1:DatePickerControl ID="dpMfgDate" runat="server" Enabled="false" />
                        </td> 
                        <td style="width:100px">วันหมดอายุ</td>
                        <td style="width:550px">
                            <uc1:DatePickerControl ID="dpExpDate" runat="server" />
                        </td> 
                    </tr>
                </table>
            </td> 
        </tr> 
    </table>
