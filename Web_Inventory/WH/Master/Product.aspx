<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="WH_Master_Product"   Title="วัตถุดิบ" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;วัตถุดิบ</td> 
        </tr> 
        <tr class="toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true" 
                 BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" 
                 BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="หน่วยย่อย"
                 OnBackClick="BackClick" OnSaveClick="SaveClick" OnCancelClick="CancelClick" OnSubmitClick="SubmitClick"/>
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
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            รหัสวัตถุดิบ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label5" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" />
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ประเภทวัตถุดิบ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="205px" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            กลุ่มวัตถุดิบ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="205px" >
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ชื่อวัตถุดิบ(ภาษาไทย)</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ชื่อวัตถุดิบ(ภาษาอังกฤษ)</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtEName" runat="server" CssClass="zTextbox" MaxLength="200" Width="205px"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ชื่อย่อ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtAbbName" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                  
                     <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            บาร์โค้ด</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr> 
                   
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            หน่วยนับ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zComboBox" Width="205px"></asp:DropDownList>
                            <asp:Label ID="Label6" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ราคาทุน</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCost" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label25" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ราคาขาย</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>                            
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            ราคากลาง</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtStdPrice" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>                            
                        </td> 
                     <tr height="25">
                        <td style="height: 5px; width: 50px;"></td>
                        <td style="height: 5px; width: 150px;">
                            วิธีการนำเข้า</td>
                        <td colspan="3">
			                <table>
			                    <tr>
			                        <td>
                                        <asp:RadioButtonList ID="rbtOrderType" runat="server" RepeatDirection="Horizontal" Width="305px" AutoPostBack="True" OnSelectedIndexChanged="rbtOrderType_SelectedIndexChanged" >
                                            <asp:ListItem Value="PD">ผลิตเอง</asp:ListItem>
                                            <asp:ListItem Value="PO">จ้างผลิต</asp:ListItem>
                                            <asp:ListItem Value="AR">ทั้งสองวิธี</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
			                        <td>
                                        <asp:Label ID="Label9" runat="server" CssClass="zRemark" Text="*"></asp:Label>			    
                                    </td>
                                </tr>
                            </table>			    
                        </td>
                    </tr>
                     <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            ระยะเวลาการสั่งซื้อ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtime" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            วัน&nbsp;<asp:Label ID="Label20" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            จำนวนสั่งซื้อต่อ Lot</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label21" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                     <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            ระยะเวลาการผลิต</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtimePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            วัน&nbsp;<asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr> 

                    <tr height="25px">
                        <td style="height: 25px; width: 50px;"></td>
                        <td style="height: 25px; width: 150px;">
                            จำนวนผลิตต่อ Lot</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSizePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label8" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 
                     <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ขนาดบรรจุ</td> 
                        <td style="width: 150px">
                            <asp:TextBox ID="txtPacksize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp;&nbsp; &nbsp;<asp:Label ID="Label22" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                        <td style="width: 50px">
                            หน่วย</td> 
                        <td style="width: 200px">
                            <asp:DropDownList ID="cmbUnitPack" runat="server" CssClass="zComboBox" Width="105px"></asp:DropDownList>
                            <asp:Label ID="Label23" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td width="8000px"> 
                    </tr>
                    </tr> 
                        <tr height="25px">
                        <td style="width: 50px"></td>
                        <td style="width: 150px">
                            เตือนให้สั่งซื้อ</td> 
                        <td colspan="3">
                            <asp:CheckBoxList ID="chkMonth" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                Width="394px">
                                <asp:ListItem Value="1">มกราคม</asp:ListItem>
                                <asp:ListItem Value="2">กุมภาพันธ์</asp:ListItem>
                                <asp:ListItem Value="3">มีนาคม</asp:ListItem>
                                <asp:ListItem Value="4">เมษายน</asp:ListItem>
                                <asp:ListItem Value="5">พฤษภาคม</asp:ListItem>
                                <asp:ListItem Value="6">มิถุนายน</asp:ListItem>
                                <asp:ListItem Value="7">กรกฎาคม</asp:ListItem>
                                <asp:ListItem Value="8">สิงหาคม</asp:ListItem>
                                <asp:ListItem Value="9">กันยายน</asp:ListItem>
                                <asp:ListItem Value="10">ตุลาคม</asp:ListItem>
                                <asp:ListItem Value="11">พฤศจิกายน</asp:ListItem>
                                <asp:ListItem Value="12">ธันวาคม</asp:ListItem>
                            </asp:CheckBoxList>
                        </td> 
                    </tr>              

                    <tr height="25">
                        <td style="width: 50px"></td>
                        <td style="width: 150px"></td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                            <asp:TextBox ID="txtPBLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                        </td>
                        <td style="width: 50px">&nbsp;</td>
                        <td style="width: 200px">&nbsp;</td>
                    </tr>
                </table>
            </td> 
        </tr> 
    </table>

</asp:Content>