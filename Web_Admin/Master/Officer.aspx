<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Officer.aspx.cs" Inherits="Master_Officer" Title="ข้อมูลพนักงาน" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;ข้อมูลพนักงาน</td> 
        </tr> 
        <tr class="toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true" 
                 BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" 
                 BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" 
                 OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick"/>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
            </td> 
        </tr> 
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="750px">
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อเข้าระบบ</td> 
                        <td>
                            <asp:TextBox ID="txtUserID" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            รหัสผ่าน</td> 
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="zTextbox" MaxLength="100" Width="205px" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ยืนยันรหัสผ่าน</td> 
                        <td>
                            <asp:TextBox ID="txtPassConfirm" runat="server" CssClass="zTextbox" MaxLength="100" Width="205px" TextMode="Password"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อพนักงาน</td> 
                        <td>
                            <asp:DropDownList ID="cmbTitle" runat="server" Width="80px"></asp:DropDownList>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="100" Width="120px"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            นามสกุล</td> 
                        <td>
                            <asp:TextBox ID="txtLastname" runat="server" CssClass="zTextbox" MaxLength="100" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ชื่อเล่น</td> 
                        <td>
                            <asp:TextBox ID="txtNickname" runat="server" CssClass="zTextbox" MaxLength="100" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label6" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ฝ่าย</td> 
                        <td>
                            <asp:DropDownList ID="cmbDivision" runat="server" Width="212px">
                            </asp:DropDownList>
                            <asp:Label ID="label11" runat="server" CssClass="zRemark" Text="*" Width="11px"></asp:Label></td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            วันเกิด</td> 
                        <td>
                            <uc2:DatePickerControl ID="ctlBirthdate" runat="server" Enabled="true" Visible="true"/>
                        </td>                            
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            โทรศัพท์</td> 
                        <td>
                            <asp:TextBox ID="txtTel" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            E-Mail</td> 
                        <td>
                            <asp:TextBox ID="txtEMail" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px"></td> 
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ที่อยู่</td> 
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ถนน</td> 
                        <td>
                            <asp:TextBox ID="txtRoad" runat="server" CssClass="zTextbox" MaxLength="100" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            จังหวัด</td> 
                        <td>
                            <asp:DropDownList ID="cmbProvince" runat="server" Width="212px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbProvince_SelectedIndexChanged"></asp:DropDownList>   
                            <asp:Label ID="Label10" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            อำเภอ</td> 
                        <td>
                            <asp:DropDownList ID="cmbAmphur" runat="server" Width="212px" CssClass="zComboBox" AutoPostBack="True" OnSelectedIndexChanged="cmbAmphur_SelectedIndexChanged"></asp:DropDownList>  
                            <asp:Label ID="Label9" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            ตำบล</td> 
                        <td>
                            <asp:DropDownList ID="cmbDistrict" runat="server" Width="212px" CssClass="zComboBox"></asp:DropDownList>
                            <asp:Label ID="Label8" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            รหัสไปรษณีย์</td> 
                        <td>
                            <asp:TextBox ID="txtZipcode" runat="server" CssClass="zTextbox" MaxLength="5" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td width="150px">
                            หมายเหตุ</td> 
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="zTextbox" Rows="3" TextMode="MultiLine" MaxLength="200" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50">
                        </td>
                        <td width="150">
                        </td>
                        <td>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="83px"></asp:TextBox>
                            <asp:TextBox ID="txtHidPass" runat="server" CssClass="zHidden" Width="83px"></asp:TextBox>
                            <uc2:DatePickerControl ID="ctlEFDate" runat="server" Enabled="False" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td> 
        </tr> 
    </table>
</asp:Content>