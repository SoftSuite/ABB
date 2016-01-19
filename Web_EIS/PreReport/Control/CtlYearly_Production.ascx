<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtlYearly_Production.ascx.cs" Inherits="PreReport_Control_CtlYearly_Production" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %> 
<table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr height="5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr height="25px">
                <td width="5px">
                </td>
                <td class="headtext">
                    &nbsp;<asp:Label ID="lblHead" runat="server" Text="Label"></asp:Label></td>
            </tr> 
            <tr height="10px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="650px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="3" height="20px">
                                &nbsp;<asp:Label ID="lblSubHead" runat="server" Text="Label"></asp:Label></td> 
                        </tr>
                        <tr height="5px">
                            <td style="width:10px" >
                            </td> 
                            <td>
                            </td>  
                            <td style="width: 391px">
                            </td> 
                        </tr>
                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                           <td align="right">
                                ปี พ.ศ.&nbsp;:&nbsp;
                            </td> 
                            <td style="width: 391px"><asp:TextBox ID="txtYearFrom" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
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
                            <td style="width: 391px" >
                             </td> 
                        </tr>

                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                &nbsp;</td>  
                            <td style="width: 391px">
                                <asp:RadioButtonList ID="rbtType" runat="server" 
                                    RepeatDirection="Horizontal" Width="249px" AutoPostBack="True" OnSelectedIndexChanged="rbtType_SelectedIndexChanged">
                                    <asp:ListItem Value="FG" Selected="True">สินค้า</asp:ListItem>
                                    <asp:ListItem Value="WH">วัตถุดิบ</asp:ListItem>
                                </asp:RadioButtonList></td>   
                        </tr>

                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                กลุ่มสินค้าในกระบวนการผลิต&nbsp;:&nbsp;</td>  
                            <td style="width: 391px" >
                                <asp:DropDownList ID="cmbProduceGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduceGroup_SelectedIndexChanged" >
                                </asp:DropDownList><asp:Label ID="label3" runat="server" ForeColor="red">*</asp:Label></td>  
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                ชื่อสินค้า/วัตถุดิบ&nbsp;:&nbsp;</td>  
                            <td style="width: 391px">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList><asp:Label ID="label4" runat="server" ForeColor="red">*</asp:Label></td>   
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                             </td>  
                            <td style="width: 391px" >
                             </td> 
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                                </td>  
                            <td style="width: 391px" >
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click" /></td> 
                        </tr>
                        <tr height="15px">
                            <td style="width:10px" >
                            </td> 
                            <td >
                             </td>  
                            <td style="width: 391px" >
                             </td> 
                        </tr>
                    </table>
                </td>
            </tr> 
        </table>
        <br />
        <asp:Panel ID="pnlGraph" runat="server">
            &nbsp;<chart:WebChartViewer ID="vwChart" runat="server"/>
        </asp:Panel>