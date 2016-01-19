<%@ Page Language="C#" MasterPageFile="~/Template/Page1.Master" AutoEventWireup="true" CodeFile="UnitSearch.aspx.cs" Inherits="Master_UnitSearch" Title="หน่วยนับ" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;หน่วยนับ</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr> 
        <tr>
            <td height="10">
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="700" class="searchTable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;ค้นหา</td>
                    </tr>
                    <tr>
                        <td colspan="3" height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px" height="25">
                        </td>
                        <td style="width: 150px" height="25">
                            ชื่อหน่วยนับภาษาไทย</td> 
                        <td height="25">
                            <asp:TextBox ID="txtUNName" runat="server" CssClass="zTextbox" MaxLength="20" Width="200px"></asp:TextBox></td> 
                    </tr> 
                   
                    <tr height="25px">
                        <td style="width: 50px" height="25">
                        </td>
                        <td style="width: 150px" height="25">
                            ชื่อหน่วยนับภาษาอังกฤษ</td> 
                        <td height="25">
                            <asp:TextBox ID="txtUNEname" runat="server" CssClass="zTextbox" MaxLength="50" Width="200px"></asp:TextBox>
                            <asp:ImageButton id="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" ImageAlign="AbsMiddle">
                            </asp:ImageButton></td> 
                    </tr>
                    <tr>
                        <td height="10" style="width: 50px">
                        </td>
                        <td height="10" style="width: 150px">
                        </td>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="10">
            </td>
        </tr>
       
        <tr height="25px">
            <td style="height: 25px">
                <asp:GridView ID="grvUnitSearch" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="700px" OnRowDataBound="grvUnitSearch_RowDataBound">
                <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="30px" /> 
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                       <asp:BoundField DataField="LOID" >
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                    <asp:BoundField DataField="CODE" HeaderText="รหัสหน่วยนับ">
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                     <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Unit.aspx?loid={0}"
                            DataTextField="NAME" HeaderText="ชื่อหน่วยนับภาษาไทย" DataTextFormatString="{0}" >
                        </asp:HyperLinkField>
                    <asp:BoundField DataField="ENAME" HeaderText="ชื่อหน่วยนับภาษาอังกฤษ">
                        <ItemStyle Width="150px"/>
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                       <asp:TemplateField HeaderText="ใช้สำหรับ">
                            <ItemTemplate>
                                <asp:Label ID="lblType" runat="server" Text='<%# (Eval("TYPE").ToString() == ABB.Data.Constz.UnitType.FG.Code ?  ABB.Data.Constz.UnitType.FG.Name : (Eval("TYPE").ToString() == ABB.Data.Constz.UnitType.WH.Code ?  ABB.Data.Constz.UnitType.WH.Name : ABB.Data.Constz.UnitType.ALL.Name)) %>'></asp:Label> 
                            </ItemTemplate> 
                            <HeaderStyle Width="150px" /> 
                       </asp:TemplateField> 
                    </Columns> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                        <PagerSettings Visible="False" />            
                </asp:GridView>
            </td> 
        </tr> 
        <tr>
            <td >
                &nbsp;</td> 
        </tr>
    </table>
</asp:Content>

