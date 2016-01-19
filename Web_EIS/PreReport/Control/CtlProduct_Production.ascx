<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtlProduct_Production.ascx.cs" Inherits="PreReport_Control_CtlProduct_Production" %>
<%@ Register Src="DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
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
                            <td colspan="5" style="height: 20px">
                                &nbsp;<asp:Label ID="lblSubHead" runat="server" Text="Label"></asp:Label></td>
                            <td colspan="1" style="height: 20px">
                            </td>
                        </tr>
		                <tr height="5px">
                            <td style="width: 10px; height: 5px;">
                            </td>
                            <td style="width: 142px; height: 5px;">
                            </td>
		                    <td style="width: 79px; height: 5px;">
		                    </td>
                            <td style="width: 51px; height: 5px">
                            </td>
		                   <td style="width: 199px; height: 5px"></td> 
                            <td style="height: 5px">
                            </td>
		                </tr> 
                        <tr runat="server" height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" colspan="2">
                                ช่วงเวลา :&nbsp;</td>
                            <td style="width: 51px">
                                <asp:RadioButton ID="rbtYear" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="rbtYear_CheckedChanged"
                                    Text="ปี พ.ศ." Width="69px" /></td>
                            <td style="width: 199px">
                                <asp:TextBox ID="txtYearFrom" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                            <td>
                               <asp:Label ID="Label1" runat="server" Text="ถึง"></asp:Label>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:TextBox ID="txtYearTo" runat="server" Width="45px" MaxLength="4"></asp:TextBox></td>
                        </tr>
                        <tr height="25" id="tdDate" runat=server>
                            <td style="width: 10px">
                            </td>
                            <td align="right" style="width: 142px">
                            </td>
                            <td align="left" style="width: 79px">
                                &nbsp;
                                </td>
                            <td style="width: 51px">
                                <asp:RadioButton ID="rbtDate" runat="server" AutoPostBack="True" OnCheckedChanged="rbtDate_CheckedChanged"
                                    Text="วันที่" /></td>
                            <td style="width: 199px">
                                <uc1:DatePickerControl ID="ctlDateFrom" runat="server" />
                                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="ถึง"></asp:Label>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<uc1:DatePickerControl ID="ctlDateTo" runat="server" />
                            </td>
                        </tr>
                        <tr height="25" id="tdDate1" runat=server>
                            <td style="width: 10px"> 
                            </td>
                            <td align="right" style="width: 142px">
                            </td>
                            <td align="left" style="width: 79px">
                                &nbsp;
                                </td>
                            <td style="width: 51px">
                                <asp:RadioButton ID="rbtDate1" runat="server" AutoPostBack="True" OnCheckedChanged="rbtDate1_CheckedChanged"
                                    Text="วันที่" /></td>
                            <td style="width: 199px">
                                </td>
                            <td>
                            </td>
                        </tr>

                       <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right" colspan="2">
                                สินค้า / วัตถุดิบ :&nbsp;</td>
                           <td colspan="3">
                                <asp:RadioButtonList ID="rbtType" runat="server" 
                                    RepeatDirection="Horizontal" Width="249px" AutoPostBack="True" OnSelectedIndexChanged="rbtType_SelectedIndexChanged">
                                    <asp:ListItem Value="FG" Selected="True">สินค้า</asp:ListItem>
                                    <asp:ListItem Value="WH">วัตถุดิบ</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>

                        <tr height="25">
                            <td style="width: 10px; height: 25px;">
                            </td>
                            <td align="right" colspan="2" style="height: 25px">
                                กลุ่มสินค้าในกระบวนการผลิต :&nbsp;
                            </td>
                            <td colspan="3" style="height: 25px">
                                <asp:DropDownList ID="cmbProduceGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduceGroup_SelectedIndexChanged" >
                                </asp:DropDownList>
                                <asp:Label ID="Label2" runat="server" ForeColor="red">*</asp:Label></td>
                        </tr>
                        <tr height="25">
                            <td style="width: 10px; height: 25px">
                            </td>
                            <td align="right" colspan="2" style="height: 25px">
                                บาร์โค้ดสินค้า/ วัตถุดิบ :&nbsp;</td>
                            <td colspan="3" style="height: 25px">
                                <asp:TextBox ID="txtBarcodeFrom" runat="server"></asp:TextBox>
                                ถึง
                                <asp:TextBox ID="txtBarcodeTo" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td align="right" colspan="2" valign="top">
                                สินค้า/วัตถุดิบ :&nbsp;</td>
                            <td colspan="2">
                                <asp:CheckBoxList ID="chklist" runat="server">
                                </asp:CheckBoxList></td>
                            <td>
                            </td>
                        </tr>
		                <tr height="25px">
                            <td style="width: 10px" height="15">
                            </td>
                            <td height="15" style="width: 142px">
                            </td>
		                    <td style="width: 79px" height="15">
		                    </td>
                            <td height="15" style="width: 51px">
                            </td>
		                   <td height="15" style="width: 199px"></td> 
                            <td height="15">
                            </td>
		                </tr> 
		                <tr height="25px">
                            <td style="width: 10px">
                            </td>
                            <td style="width: 142px">
                            </td>
		                    <td style="width: 79px">
		                    </td>
                            <td colspan="2">
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click"  /></td>
                            <td>
                            </td>
		                </tr> 
		                <tr height="25px">
                            <td style="width: 10px" height="5">
                            </td>
                            <td height="5" style="width: 142px">
                            </td>
		                    <td style="width: 79px" height="5">
		                    </td>
                            <td height="5" style="width: 51px">
                            </td>
		                   <td height="5" style="width: 199px"></td> 
                            <td height="5">
                            </td>
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
                                <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox></td>
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