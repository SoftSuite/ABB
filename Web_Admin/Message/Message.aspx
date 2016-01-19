<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message_Message" Title="ข้อความ" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height:100%">
        <tr style="height:25">
            <td class="headtext">
                &nbsp;Message</td> 
        </tr>
        <tr style="height:25">
            <td>&nbsp;</td> 
        </tr> 
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" width="800px" class="searchTable">
                    <tr style="height: 10px" valign="top">
                        <td width="20">
                        </td>
                        <td width="150">
                        </td>
                        <td colspan="4" width="630">
                        </td>
                    </tr>
                    <tr style="height: 25px" valign="top">
                        <td width="20px"></td>
                        <td width="150px">
                            ข้อความ:
                        </td>
                        <td colspan="4" width="630">
                            <asp:TextBox ID="txtMessage" runat="server" CssClass="zTextbox" Width="550px" TextMode="MultiLine" Height="60px"/>
                        </td>
                    </tr>                    
                    <tr style="height: 25px">
                        <td width="20px"></td>
                        <td style="width: 150px">
                            วันที่ให้แสดง:
                        </td>
                        <td colspan="2" style="width: 150px">
                            <uc2:DatePickerControl ID="dtpDateFrom" runat="server" />
                        </td>
                        <td style="width: 30px" align="center">
                            ถึง
                        </td>                        
                        <td style="width: 450px">
                            <uc2:DatePickerControl ID="dtpDateTo" runat="server" />
                        </td>
                    </tr>
                    <tr style="height: 5px" valign="top">
                        <td width="20">
                        </td>
                        <td width="150">
                        </td>
                        <td colspan="4" width="630">
                        </td>
                    </tr>
                    <tr style="height: 25px" class="subheadertext">
                        <td width="20px"></td>
                        <td style="width: 150px">
                        </td>
                        <td  colspan="4" >
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="toolbarbutton" OnClick="btnSave_Click" ToolTip="บันทึก" />
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="toolbarbutton" OnClick="btnCancel_Click" ToolTip="ยกเลิก" />
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="56px" Text="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>       
        <tr style="height:25">
            <td>&nbsp;</td> 
        </tr> 
        <tr>
            <td>
                <asp:Panel ID="pnlMain" runat="server" Width="800" Height="320" ScrollBars="vertical">
                <asp:GridView ID="grvMessage" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowHeader="false"
                EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="783" OnRowDataBound="grvMessage_RowDataBound" OnRowCommand="grvMessage_RowCommand">
                <PagerSettings Visible="False" Position="Top" />
                    <Columns>
                        <asp:TemplateField ShowHeader='false'>
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width = "100%">
                                    <tr valign="top">
                                        <td width="30px">
                                        </td> 
                                        <td width="80px"><b>วันที่สร้าง</b>
                                        </td> 
                                        <td width="10px">:
                                        </td> 
                                        <td>
                                            <asp:Label ID="txtCreateDate" runat="server" Text='<%# Bind("CREATEON") %>' />
                                        </td> 
                                    </tr>
                                    <tr valign="top">
                                        <td width="30px">
                                        </td> 
                                        <td width="80px"><b>วันที่แก้ไข</b>
                                        </td> 
                                        <td width="10px">:
                                        </td> 
                                        <td>
                                            <asp:Label ID="txtUpdateDate" runat="server" Text='<%# Bind("UPDATEON") %>' />
                                        </td> 
                                    </tr>
                                    <tr valign="top">
                                        <td width="30px">
                                        </td> 
                                        <td width="80px"><b>ข้อความ</b>
                                        </td> 
                                        <td width="10px">:
                                        </td> 
                                        <td>
                                            <asp:Label ID="txtMessage" runat="server" Text='<%# Bind("MESSAGE") %>' />
                                        </td> 
                                    </tr>
                                    <tr bgcolor="#D6D6D6">
                                        <td width="30px">
                                        </td> 
                                        <td width="80px"><b>โดย</b>
                                        </td>
                                        <td width="10px">:
                                        </td> 
                                        <td> 
                                            <asp:Label ID="txtName" runat="server" Text='<%# Bind("NAME") %>' />
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="EditMessage" AlternateText="แก้ไข" ImageUrl="~/Images/icn_edit.gif"/>
                                             <asp:ImageButton ID="imbDelete"  runat="server" CausesValidation="False" CommandName="DeleteMessage" AlternateText="ลบ" ImageUrl="~/Images/icn_delete.gif"/>
                                        </td> 
                                    </tr>
                                </table> 
                                <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="0px" Text='<%# Bind("LOID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                       
                    </Columns>
                </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>