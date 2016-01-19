<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Admin_UserList" Title="สิทธิ์การใช้งานในระบบ" %>

<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarCtl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td style="height: 27px" class="headtext">
            &nbsp;สิทธิ์การใช้งานในระบบ</td>
    </tr>
    <tr>
        <td style="height: 16px" class="toolbarplace">
            <uc1:ToolbarCtl ID="ToolbarCtl1" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="false" 
                BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnDelete="ยกเลิกสิทธิ์การใช้งาน" OnDeleteClick="DeleteClick"/>
        </td>
    </tr>
    <tr>
        <td style="height: 16px">
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0" width="600" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                <tr>
                    <td class="subheadertext" colspan="4" style="height: 25px">
                        &nbsp;ค้นหา</td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 15px">
                    </td>
                    <td style="width: 160px; height: 15px">
                    </td>
                    <td style="width: 200px; height: 15px">
                    </td>
                    <td style="height: 15px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 25px">
                    </td>
                    <td style="width: 160px; height: 25px">
                        ชื่อเข้าระบบ</td>
                    <td style="width: 200px; height: 25px">
                        <asp:TextBox ID="txtSUserID" runat="server" CssClass="zTextbox" Width="180px"></asp:TextBox></td>
                    <td style="height: 25px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 25px">
                    </td>
                    <td style="width: 160px; height: 25px">
                        ชื่อพนักงาน</td>
                    <td style="width: 200px; height: 25px">
                        <asp:TextBox ID="txtSName" runat="server" CssClass="zTextbox" Width="180px"></asp:TextBox></td>
                    <td style="height: 25px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 25px">
                    </td>
                    <td style="width: 160px; height: 25px">
                        ระดับการใช้งาน</td>
                    <td style="width: 200px; height: 25px">
                        <asp:DropDownList ID="cmbSLevel" runat="server" CssClass="zComboBox" Width="180px">
                            <asp:ListItem Value="">ทั้งหมด</asp:ListItem>
                            <asp:ListItem Value="U">ผู้ใช้งานระบบ</asp:ListItem>
                            <asp:ListItem Value="M">หัวหน้างาน</asp:ListItem>
                            <asp:ListItem Value="A">ผู้ดูแลระบบ</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="height: 25px">
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click"
                            ToolTip="ค้นหา" /></td>
                </tr>
                <tr>
                    <td style="width: 50px; height: 15px">
                    </td>
                    <td style="width: 160px; height: 15px">
                    </td>
                    <td style="width: 200px; height: 15px">
                    </td>
                    <td style="height: 15px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 25px">
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowDataBound="gvMain_RowDataBound">
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItem" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle  Width="30px" HorizontalAlign="Center"/>
                        <HeaderStyle Width="30px"  HorizontalAlign="Center"/> 
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="ชื่อเข้าระบบ" DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="UserDetail.aspx?id={0}" DataTextField="USERID">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="ชื่อพนักงาน" DataField="OFFNAME" />
                    <asp:BoundField HeaderText="ระดับการใช้งาน" DataField="ZLEVELNAME">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="ใช้งานล่าสุด" DataField="LASTLOGIN" DataFormatString="{0:dd MMM yyyy [HH:mm]}" HtmlEncode="False">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </asp:BoundField>
                   <asp:BoundField DataField="ROLE" >
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Content>

