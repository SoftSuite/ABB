<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtlMonth_No.ascx.cs" Inherits="PreReport_Control_CtlMonth_No" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %> 
<table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr height="5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr height="25px">

                <td class="headtext">
                    &nbsp;<asp:Label ID="lblHead" runat="server" Text="Label"></asp:Label></td>
            </tr> 
            		<tr height="20px">
			<td>
                &nbsp;<asp:CheckBox
                ID="chkOption" runat="server" Text="แสดงตัวเลือกมิติ" AutoPostBack="True" OnCheckedChanged="chkOption_CheckedChanged" Checked="True" /></td>
		</tr>
<tr id="tblOption">
<td>
<asp:Panel ID="pnlConstraints" runat="server" Width="100%">
                    <table border="0" cellpadding="0" cellspacing="0" width="550px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="3" height="20px">
                                &nbsp;<asp:Label ID="lblSubHead" runat="server" Text="Label"></asp:Label></td> 
                        </tr>
                        <tr height="5px">
                            <td style="width:10px" >
                            </td> 
                            <td>
                            </td>  
                            <td>
                            </td> 
                        </tr>
                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                           <td align="right">
                                ปี พ.ศ.&nbsp;:&nbsp;
                            </td> 
                            <td><asp:TextBox ID="txtYearFrom" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                                ถึง
                                <asp:TextBox ID="txtYearTo" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                                &nbsp;<asp:Label ID="label5" runat="server" ForeColor="red">*</asp:Label>
                            </td>
                        </tr>
                        <tr height="15px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                             </td>  
                            <td >
                             </td> 
                        </tr>
                        <tr height="25px">
                            <td style="width:10px; height: 25px;" >
                            </td> 
                            <td style="height: 25px" align="right" >
                                คลัง&nbsp;:&nbsp;</td>  
                            <td style="height: 25px">
                                <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zCombobox" Width="356px">
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
                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                ประเภทสินค้า/วัตถุดิบ&nbsp;:&nbsp;</td>  
                            <td>
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td>   
                        </tr>
                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                กลุ่มสินค้า/วัตถุดิบ&nbsp;:&nbsp;</td>  
                            <td >
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged" >
                                </asp:DropDownList></td>  
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                ชื่อสินค้า/วัตถุดิบ&nbsp;:&nbsp;</td>  
                            <td>
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td>   
                        </tr>
                         <tr height="25px">
                            <td style="width:10px; height: 25px;" >
                            </td> 
                            <td style="height: 25px" align="right" >
                                ลูกค้า&nbsp;:&nbsp;</td>  
                            <td style="height: 25px">
                                <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td>  
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                             </td>  
                            <td >
                             </td> 
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                                </td>  
                            <td >
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click" /></td> 
                        </tr>
                        <tr height="15px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                             </td>  
                            <td >
                             </td> 
                        </tr>
                    </table>
</asp:Panel>
</td>
</tr>
            
        </table>
        <br />
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