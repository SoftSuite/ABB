<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Request.aspx.cs" Inherits="Search_Request" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาเลขที่บันทึกสั่งผลิต</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height :5px">
                <td style ="width :5px">
                </td>

            </tr> 
            <tr style ="height :25px">
                <td style ="width :5px">
                </td>
                <td class="headtext">
                    &nbsp;ค้นหาเลขที่บันทึกสั่งผลิต</td>
            </tr> 
            <tr style=" height :10px">
                <td style ="width :5px">
                </td>

            </tr> 

        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="540px" class="searchTable" >

            <tr>
                <td style="width: 120px; height: 24px;"  >
                    <asp:Label ID="label4" runat ="server" Width="107px" >เลขที่บันทึกสั่งผลิต</asp:Label>
                </td>
                <td style="width: 150px; height: 24px;"  >
                    <asp:TextBox ID ="txtCFrom" runat ="server" ></asp:TextBox>
                </td>
                <td style="width :40px; height: 24px;" align ="center"  >
                    <asp:Label ID="label1" runat ="server" Width="20px"  >ถึง</asp:Label>
                </td>
                <td style="width: 200px; height: 24px;"  >
                    <asp:TextBox ID ="txtCTo" runat ="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 120px; height: 25px;" >
                    <asp:Label ID="label6" runat ="server" Width="107px" >วันที่สั่งผลิต</asp:Label>
                </td>
                <td style="width: 150px; height: 25px;" >
                    <uc1:DatePickerControl ID="dpReqDateFrom" runat="server"  />
                </td>
                <td style="width :40px; height: 25px;" align ="center"  >
                    <asp:Label ID="label2" runat ="server" Width="20px"  >ถึง</asp:Label>
                </td>
                <td style="width: 200px; height: 25px;"  >
                    <uc1:DatePickerControl ID="dpReqDateTo" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 94px" >
                    <asp:Label ID="label3" runat ="server" Width="107px">ชื่อผลิตภัณฑ์</asp:Label>
                </td>
                <td colspan ="4" style ="width:500px">
                    <asp:TextBox ID ="txtPName" runat ="server" Width="369px" ></asp:TextBox>
                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click"  />
                    </td>
            </tr>
            <tr>
                <td colspan ="4" style ="height :10px"></td>
            </tr>
        
            <tr>
                <td colspan ="4">
                    <asp:GridView ID="grvRequest" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="RQILOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvRequest_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                              <asp:BoundField DataField="RQILOID" HeaderText="RQILOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="ORDERNO" HeaderText="ลำดับ">
                                <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                <HeaderStyle Width="50px"/>
                            </asp:BoundField> 
                            <asp:TemplateField HeaderText="เลขที่บันทึกสั่งผลิต">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplCode" runat="server" Text='<%# Bind("CODE") %>' ></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Font-Underline="True" ForeColor="Blue" Width="120px" />
                                <HeaderStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="REQDATE" HeaderText="วันที่สั่งผลิต">
                                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                <HeaderStyle Width="100px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="NAME" HeaderText="ชื่อผลิตภัณฑ์">
                                <ItemStyle Width="150px" HorizontalAlign="Left"/>
                                <HeaderStyle Width="150px"/>
                            </asp:BoundField> 
                            <asp:BoundField DataField="QTY" HeaderText="จำนวนสั่งผลิต">
                                <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                <HeaderStyle Width="80px"/>
                            </asp:BoundField> 
                            <asp:BoundField DataField="UNAME" HeaderText="หน่วยนับ">
                                <ItemStyle Width="80px" HorizontalAlign="Center"/>
                                <HeaderStyle Width="80px"/>
                            </asp:BoundField> 
                            <asp:BoundField DataField="CODE" >
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="PDLOID" >
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField> 
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" /> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                    </asp:GridView>
                </td>
            </tr>
    </table>  
    </ContentTemplate>
</asp:UpdatePanel>
</form>
</body>
</html>