<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ControlStock.aspx.cs" Inherits="FG_Master_ControlStock" Title="ควบคุมปริมาณสินค้า" %>

<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ควบคุมปริมาณสินค้า/วัตถุดิบ</td> 
        </tr> 
        <tr style="height:25px;">
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" OnBackClick="BackClick"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" NameBtnBack="กลับหน้าค้นหา" OnCancelClick="CancelClick" OnSaveClick="SaveClick" />
            </td> 
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" width="750px" >
        <tr>
            <td style="height:15px" colspan="2"></td>
        </tr>
        <tr>
            <td style="width:50px;">
            </td>
            <td>
                <div>
                <fieldset>
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="height:10px; width: 60px;"></td>
                        </tr>
                        <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                คลัง
                            </td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtWHName" runat="server" CssClass="zTextbox-view" Width="200px" Enabled="False"></asp:TextBox>
                                <asp:TextBox ID="txtWarehouse" runat="server" CssClass="zHidden" ReadOnly="True"
                                    Width="108px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                สินค้า/วัตดุดิบ
                            </td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" Width="120px"></asp:TextBox>
                                <asp:ImageButton ID="imbSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="imbSearch_Click" />
                                &nbsp;
                            </td>
                        </tr>
                                                <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                ชื่อ</td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox-view" Width="300px" Enabled="False" ForeColor="#0000C0" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtUnit" runat="server" Width="108px" CssClass="zTextbox-view" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                         <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                หน่วยเล็กที่สุด</td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtUnitMaster" runat="server" Width="120px" CssClass="zTextbox-view" ReadOnly="True"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
 
                        <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                ปริมาณคงที่
                            </td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtStock" runat="server" CssClass="zTextboxR" Width="120px"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtUnitStock" runat="server" Width="120px" CssClass="zTextbox-view" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                ปริมาณต่ำสุด
                            </td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtMinimum" runat="server" CssClass="zTextboxR" Width="120px"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtUnitMin" runat="server" Width="120px" CssClass="zTextbox-view" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width:60px; height:24px;">&nbsp;
                            </td>
                            <td style="width:100px; height:24px;">
                                ปริมาณสูงสุด
                            </td>
                            <td  style="height:24px; width: 508px;">
                                <asp:TextBox ID="txtMaximum" runat="server" CssClass="zTextboxR" Width="120px"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtUnitMax" runat="server" Width="120px" CssClass="zTextbox-view" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height:10px; width: 60px;"></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            </td>
        </tr>
    </table>
</asp:Content>

