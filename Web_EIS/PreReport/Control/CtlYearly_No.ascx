<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtlYearly_No.ascx.cs" Inherits="PreReport_Control_CtlYearly_No" %>
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
                                �� �.�.&nbsp;:&nbsp;
                            </td> 
                            <td><asp:TextBox ID="txtYearFrom" runat="server" Width="45px" MaxLength="4"></asp:TextBox>
                                �֧
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
                                ��ѧ&nbsp;:&nbsp;</td>  
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
                                    <asp:ListItem Value="FG" Selected="True">�Թ���</asp:ListItem>
                                    <asp:ListItem Value="WH">�ѵ�شԺ</asp:ListItem>
                                    <asp:ListItem Value="OT">����</asp:ListItem>
                                </asp:RadioButtonList></td>   
                        </tr>
                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                �������Թ���/�ѵ�شԺ&nbsp;:&nbsp;</td>  
                            <td>
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td>   
                        </tr>
                        <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                ������Թ���/�ѵ�شԺ&nbsp;:&nbsp;</td>  
                            <td >
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged" >
                                </asp:DropDownList></td>  
                        </tr>
                         <tr height="25px">
                            <td style="width:10px" >
                            </td> 
                            <td align="right">
                                �����Թ���/�ѵ�شԺ&nbsp;:&nbsp;</td>  
                            <td>
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td>   
                        </tr>
                         <tr height="25px">
                            <td style="width:10px; height: 25px;" >
                            </td> 
                            <td style="height: 25px" align="right" >
                                �١���&nbsp;:&nbsp;</td>  
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
                                <asp:Button ID="btnReport" runat="server" Text="�ʴ���§ҹ" Width="130px" OnClick="btnReport_Click" /></td> 
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
                </td>
            </tr> 
        </table>
        <br />
        <asp:Panel ID="pnlGraph" runat="server">
            &nbsp;<chart:WebChartViewer ID="vwChart" runat="server"/>
        </asp:Panel>