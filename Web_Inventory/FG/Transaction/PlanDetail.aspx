<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanDetail.aspx.cs" Inherits="FG_Transaction_PlanDetail" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>��������´�������Թ���</title>
    <link href="../../Template/BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:5px; height: 5px;">
                            </td> 
                            <td style="height: 5px"></td> 
                        </tr>
                        <tr style="height:25px">
                            <td style="width:5px">&nbsp;
                            </td> 
                            <td class="toolbarplace">
                                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="true"
                                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" OnSaveClick="SaveClick"/>
                            </td> 
                        </tr>
                        <tr style="height:5px">
                            <td style="width:5px">
                            </td> 
                            <td></td> 
                        </tr>
                        <tr>
                            <td style="width:5px">
                            </td> 
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" width="500" class="searchTable" bgcolor="#ffffcc">
                                    <tr style="height:5px">
                                        <td style="width: 120px"></td> 
                                        <td style="width: 130px"></td> 
                                        <td style="width: 65px"></td> 
                                        <td>
                                            </td> 
                                    </tr>
                                    <tr style="height:25px">
                                        <td align="right" style="width: 120px">�Թ���&nbsp;:&nbsp;
                                        </td> 
                                        <td colspan="3">
                                            <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                                            (<asp:Label ID="lblUnitName" runat="server" Text=""></asp:Label>)</td> 
                                    </tr>
                                    <tr style="height: 25px">
                                        <td align="right" style="width: 120px">
                                            Min&nbsp;:&nbsp;</td>
                                        <td style="width: 130px">
                                            <asp:Label ID="lblMin" runat="server"></asp:Label></td>
                                        <td style="width: 65px" align="right">
                                            Max&nbsp;:&nbsp;</td>
                                        <td style="width: 185px">
                                            <asp:Label ID="lblMax" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="height:25px">
                                        <td align="right" style="width: 120px">
                                            �ѹ����Թ��Ҩ����&nbsp;:&nbsp;
                                        </td> 
                                        <td colspan="3">
                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                            <asp:Label ID="lblStatus" runat="server" CssClass="zHidden"></asp:Label>
                                            <asp:Label ID="lblProduct" runat="server" CssClass="zHidden"></asp:Label></td> 
                                    </tr>
                                    <tr style="height:5px">
                                        <td style="width: 120px"></td> 
                                        <td style="width: 130px"></td> 
                                        <td style="width: 65px"></td> 
                                        <td></td> 
                                    </tr>
                                </table> 
                            </td> 
                        </tr>
                        <tr style="height:10px">
                            <td style="width:5px">
                            </td> 
                            <td>
                                <asp:Label ID="lblReceiveLOID" runat="server" CssClass="zHidden"></asp:Label></td> 
                        </tr>
                        <tr>
                            <td style="width: 5px">
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                    <tr style="height:25">
                                        <td style="width: 50px">
                                            &nbsp;<b>��觫���</b></td>
                                        <td style="width: 120px" align="right">
                                            ����ҳ�����觫���&nbsp;:&nbsp;</td> 
                                        <td style="width: 100px">
                                            <asp:Label ID="lblPOLotSize" runat="server"></asp:Label></td> 
                                        <td style="width: 140px" align="right">
                                            �������ҡ����觫���&nbsp;:&nbsp;</td> 
                                        <td style="width: 90px">
                                            <asp:Label ID="lblPOLeadTime" runat="server"></asp:Label>
                                            �ѹ</td> 
                                    </tr>
                                    <tr style="height:25">
                                        <td align="right" style="width: 50px">
                                            <asp:Label ID="lblPOLOID" runat="server" CssClass="zHidden"></asp:Label></td>
                                        <td style="width: 120px" align="right">
                                            �ӹǹ�����觫���&nbsp;:&nbsp;</td> 
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtPOQty" runat="server" CssClass="zTextboxR" Width="80px"></asp:TextBox></td> 
                                        <td style="width: 140px" align="right">
                                            ��ͧ��觫��������ѹ���&nbsp;:&nbsp;</td> 
                                        <td style="width: 90px">
                                            <asp:Label ID="lblPODate" runat="server"></asp:Label></td> 
                                    </tr>
                                    <tr style="height: 10px">
                                        <td align="right" style="width: 50px">
                                        </td>
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td style="width: 100px">
                                        </td>
                                        <td align="right" style="width: 140px">
                                        </td>
                                        <td style="width: 90px">
                                        </td>
                                    </tr>
                                    <tr style="height:25">
                                        <td style="width: 50px">
                                            &nbsp;<b>��觼�Ե</b></td>
                                        <td style="width: 120px" align="right">
                                            ����ҳ�����觼�Ե&nbsp;:&nbsp;</td> 
                                        <td style="width: 100px">
                                            &nbsp;<asp:Label ID="lblPDLotSize" runat="server"></asp:Label></td> 
                                        <td style="width: 140px" align="right">
                                            �������ҡ�ü�Ե&nbsp;:&nbsp;</td> 
                                        <td style="width: 90px">
                                            <asp:Label ID="lblPDLeadTime" runat="server"></asp:Label>
                                            �ѹ</td> 
                                    </tr>
                                    <tr style="height:25">
                                        <td align="right" style="width: 50px">
                                            <asp:Label ID="lblPDLOID" runat="server" CssClass="zHidden"></asp:Label></td>
                                        <td style="width: 120px" align="right">
                                            �ӹǹ�����觼�Ե&nbsp;:&nbsp;</td> 
                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtPDQty" runat="server" CssClass="zTextboxR" Width="80px"></asp:TextBox></td> 
                                        <td style="width: 140px" align="right">
                                            ��ͧ��觼�Ե�����ѹ���&nbsp;:&nbsp;</td> 
                                        <td style="width: 90px">
                                            <asp:Label ID="lblPDDate" runat="server"></asp:Label></td> 
                                    </tr>
                                </table> 
                            </td>
                        </tr>
                        <tr style="height:10px">
                            <td style="width:5px">
                            </td> 
                            <td></td> 
                        </tr>
                        <tr style="height: 5px">
                            <td style="width: 5px">
                            </td>
                            <td class="toolbarplace">
                                <asp:Button ID="btnCalculate" runat="server" CssClass="zButton" OnClick="btnCalculate_Click"
                                    Text="�ӹǳ������ѵ�شԺ" /></td>
                        </tr>
                        <tr style="height:5px">
                            <td style="width:5px">
                            </td> 
                            <td>
                                <asp:GridView ID="grvMaterial" runat="server" CssClass="t_tablestyle" EmptyDataText=""  AutoGenerateColumns="False" Width="500px" OnRowDataBound="grvMaterial_RowDataBound">
                                    <Columns>
                                       <asp:BoundField DataField="RANK" HeaderText="�ӴѺ" >
                                            <ItemStyle Width="50px" HorizontalAlign="center" />
                                            <HeaderStyle Width="50px"/>
                                        </asp:BoundField>
                                       <asp:BoundField DataField="RWNAME" HeaderText="�ѵ�شԺ" >
                                        </asp:BoundField>
                                       <asp:BoundField DataField="REMAIN" HeaderText="�ӹǹ����ѧ" HtmlEncode="false" DataFormatString="{0:#,##0}" >
                                            <ItemStyle Width="80px" HorizontalAlign="right"/>
                                            <HeaderStyle Width="80px"/>
                                        </asp:BoundField>
                                       <asp:BoundField DataField="QTY" HeaderText="�ӹǹ�����" HtmlEncode="false" DataFormatString="{0:#,##0}" >
                                            <ItemStyle Width="80px" HorizontalAlign="right"/>
                                            <HeaderStyle Width="80px"/>
                                        </asp:BoundField>
                                       <asp:BoundField DataField="RWUNITNAME" HeaderText="˹���" >
                                            <ItemStyle Width="100px"/>
                                            <HeaderStyle Width="100px"/>
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="t_headtext" />
                                    <AlternatingRowStyle CssClass="t_alt_bg" />
                                    <PagerSettings Visible="False" />            
                                </asp:GridView>
                            </td> 
                        </tr>
                        <tr>
                            <td style="width:5px">
                            </td> 
                            <td>
                                &nbsp;</td> 
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
