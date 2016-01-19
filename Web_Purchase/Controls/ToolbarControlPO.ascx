<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ToolbarControlPO.ascx.cs" Inherits="Controls_ToolbarControlPO" %>
<asp:LinkButton ID="lnkNew" runat="server" CssClass="toolbarbutton" OnClick="lnkNew_Click">เพิ่ม</asp:LinkButton>
<asp:LinkButton ID="lnkDelete" runat="server" CssClass="toolbarbutton" OnClick="lnkDelete_Click">ลบข้อมูล</asp:LinkButton>
<asp:LinkButton ID="lnkEdit" runat="server" CssClass="toolbarbutton" OnClick="lnkEdit_Click">แก้ไข</asp:LinkButton>
<asp:LinkButton ID="lnkSave" runat="server" CssClass="toolbarbutton" OnClick="lnkSave_Click">บันทึก</asp:LinkButton>
<asp:LinkButton ID="lnkCancel" runat="server" CssClass="toolbarbutton" OnClick="lnkCancel_Click">ยกเลิก</asp:LinkButton>
<asp:LinkButton ID="lnkBack" runat="server" CssClass="toolbarbutton" OnClick="lnkBack_Click">กลับหน้ารายการ</asp:LinkButton>
<asp:LinkButton ID="lnkSubmit" runat="server" CssClass="toolbarbutton" OnClick="lnkSubmit_Click">ส่งข้อมูล</asp:LinkButton>
<asp:LinkButton ID="lnkSent" runat="server" CssClass="toolbarbutton" OnClick="lnkSent_Click">ส่งข้อมูลให้ผู้จำหน่าย</asp:LinkButton>
<asp:LinkButton ID="lnkReturn" runat="server" CssClass="toolbarbutton" OnClick="lnkReturn_Click">ส่งเรื่องคืน</asp:LinkButton>
<asp:LinkButton ID="lnkPrint" runat="server" CssClass="toolbarbutton" OnClick="lnkPrint_Click" Visible="False">พิมพ์รายงาน</asp:LinkButton>