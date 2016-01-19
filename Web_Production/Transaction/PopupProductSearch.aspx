<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupProductSearch.aspx.cs" Inherits="Transaction_PopupProductSearch" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาเลขที่การผลิต</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr height="5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
        <tr>
            <td class="headtext">
                &nbsp;ค้นหาเลขที่การผลิต</td> 
        </tr>  <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="600px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 153px"></td>
                        <td width="20px"></td>
                        <td style="width: 158px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่การผลิต</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLotno" runat="server" CssClass="zTextbox" Width="100px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                วันที่ผลิต</td>  
                            <td style="width: 153px">
                                <uc1:DatePickerControl ID="PDDateFrom" runat="server" />
                            </td>
                            <td width="20px">
                                ถึง</td>                              
                            <td style="width: 158px">
                                <uc1:DatePickerControl ID="PDDateTo" runat="server" />
                            </td>  
                        </tr>
                     <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                             ชื่อผลิตภัณฑ์</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPDName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvRequisition" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="600px" OnRowDataBound="grvRequisition_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imbSelect" runat="server" ImageUrl="~/Images/icn_submit.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                        <asp:BoundField DataField="PDLOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                         <asp:BoundField SortExpression="LOTNO" HeaderText = "เลขที่การผลิต" DataField="LOTNO">
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="MFGDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่ผลิต" DataField="MFGDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="PDNAME" HeaderText = "ชื่อผลิตภัณฑ์" DataField="PDNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField> 
                         <asp:BoundField SortExpression="STDQTY" HeaderText = "จำนวนตามทฤษฏี" DataField="STDQTY">
                            <ItemStyle Width="120px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="120px" />  
                        </asp:BoundField> 
                         <asp:BoundField SortExpression="PDUNITNAME" HeaderText = "หน่วย" DataField="PDUNITNAME">
                            <ItemStyle Width="70px" HorizontalAlign="center"/>
                            <HeaderStyle Width="70px" />  
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView></td> 
        </tr> 
       
           
                    </table>
                </td>
            </tr>
        </table>
        
                    <table border="0" cellpadding="0" cellspacing="0" width="600px">
                        <tr>
                            <td align="center">
                                &nbsp;</td> 
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
