<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtlProduct_No.ascx.cs" Inherits="PreReport_Control_CtlProduct_No" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %> 
    <table style="VERTICAL-ALIGN: top" borderColor="#3366cc" cellSpacing="0" cellPadding="0"
		width="100%" bgColor="white" border="0" class="Border_outer">
		<tr>
			<td class="headtext" headers="headerText">
                &nbsp;<asp:Label ID="lblHead" runat="server" Text="Label"></asp:Label></td>
		</tr>
		<tr height="20px">
			<td bgcolor="#99ccff">
                &nbsp;<asp:CheckBox
                ID="chkOption" runat="server" Text="แสดงตัวเลือกมิติ" AutoPostBack="True" OnCheckedChanged="chkOption_CheckedChanged" Checked="True" /></td>
		</tr>
		<tr id="tblOption">
		    <td>
		        <asp:Panel ID="pnlConstraints" runat="server" Width="100%" BackColor="AliceBlue">
		            <table border="0" cellpadding="0" cellspacing="0" width="750px">
                        <tr height="5">
                            <td colspan="3" height="20">
                                &nbsp;<asp:Label ID="lblSubHead" runat="server" Text="Label"></asp:Label></td>
                        </tr>
		                <tr height="5px">
                            <td style="width: 10px">
                            </td>
		                    <td style="width: 160px">
		                    </td>
		                   <td></td> 
		                </tr> 
		                <tr height="25px">
                            <td style="width: 10px">
                            </td>
		                    <td style="width: 160px" align="right">&nbsp;ปี พ.ศ. :&nbsp;
		                    </td>
		                   <td><asp:TextBox ID="txtYearFrom" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                                ถึง
                                <asp:TextBox ID="txtYearTo" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                                &nbsp;<asp:Label ID="label5" runat="server" ForeColor="red">*</asp:Label>
                               </td> 
		                </tr> 
                        <tr height="25">
                            <td height="5" style="width: 10px">
                            </td>
                            <td align="right" height="5" style="width: 160px">
                            </td>
                            <td height="5">
                            </td>
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" style="width: 160px">คลัง&nbsp;:&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" style="width: 160px">ลูกค้า&nbsp;:&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td>
                        </tr>
<tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                &nbsp;</td>  
                            <td>
                                <asp:RadioButtonList ID="rbtType" runat="server" 
                                    RepeatDirection="Horizontal" Width="249px" AutoPostBack="True" OnSelectedIndexChanged="rbtType_SelectedIndexChanged">
                                    <asp:ListItem Value="FG" Selected="True">สินค้า</asp:ListItem>
                                    <asp:ListItem Value="WH">วัตถุดิบ</asp:ListItem>
                                    <asp:ListItem Value="OT">อื่นๆ</asp:ListItem>
                                </asp:RadioButtonList></td>   
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" style="width: 160px">ประเภทสินค้า/วัตถุดิบ&nbsp;:&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" style="width: 160px">กลุ่มสินค้า/วัตถุดิบ&nbsp;:&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged" >
                                </asp:DropDownList></td>
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" style="width: 160px" valign="top">สินค้า/วัตถุดิบ&nbsp;:&nbsp;
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chklist" runat="server">
                                </asp:CheckBoxList></td>
                        </tr>
		                <tr height="25px">
                            <td style="width: 10px" height="15">
                            </td>
		                    <td style="width: 160px" height="15">
		                    </td>
		                   <td height="15"></td> 
		                </tr> 
		                <tr height="25px">
                            <td style="width: 10px">
                            </td>
		                    <td style="width: 160px">
		                    </td>
		                   <td>
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click"  /></td> 
		                </tr> 
		                <tr height="25px">
                            <td style="width: 10px" height="5">
                            </td>
		                    <td style="width: 160px" height="5">
		                    </td>
		                   <td height="5"></td> 
		                </tr> 
		            </table>
		        </asp:Panel> 
		    </td>
		</tr>
	    <tr>
	        <td>
		        <asp:Panel ID="pnlChart" runat="server" Width="100%" Visible="False">
		            <table border="0" cellpadding="0" cellspacing="0" width="750px">
                        <tr>
                            <td style="width: 10px" height="10">
                            </td>
                            <td colspan="2" height="10">
                            </td>
                        </tr>
		                <tr>
                            <td style="width: 10px; height: 32px;">
                            </td>
		                    <td colspan="2" style="height: 32px">
		                        <asp:Panel ID="pnlMonth" runat="server" Visible="false" Width="100%" Height="25px">
                                <asp:LinkButton ID="lnkYear" runat="server" OnClick="lnkYear_Click">ปี</asp:LinkButton>
                                <asp:Label ID="lblMonth" runat="server" Text=">> เดือน"></asp:Label></asp:Panel> </td>
		                </tr> 
                        <tr height="5">
                            <td style="width: 10px" height="10">
                            </td>
                            <td style="width: 140px" height="10">
                            </td>
                            <td height="10">
                            </td>
                        </tr>
		                <tr height="5px">
                            <td colspan="3" align="center">
            &nbsp;<chart:WebChartViewer ID="vwChart" runat="server"/></td>
		                </tr> 
		            </table>
		        </asp:Panel>  
	        </td> 
	    </tr> 
	</table>