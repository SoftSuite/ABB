<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Master_Product"  Title="สินค้า" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;สินค้า</td> 
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
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ประเภทสินค้า</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="205px" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                            <asp:CheckBox ID="chkActive" runat="server" Text="ใช้งาน" /></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            กลุ่มสินค้า</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="205px" >
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            รหัสสินค้า</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Labe24" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ชื่อสินค้า ภาษาไทย</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label3" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ชื่อสินค้า ภาษาอังกฤษ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtEName" runat="server" CssClass="zTextbox" MaxLength="200" Width="205px"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 150px">
                            ชื่อย่อสินค้า</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtAbbName" runat="server" CssClass="zTextbox" MaxLength="20" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                            </td> 
                    </tr>
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            บาร์โค้ด</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" CssClass="zRemark" Text="*"></asp:Label></td> 
                    </tr> 
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            เลขทะเบียนตำรับยา</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtRegisNo" runat="server" CssClass="zTextbox" MaxLength="50" Width="205px"></asp:TextBox>
                            </td> 
                    </tr>   
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            หน่วยนับ</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zComboBox" Width="105px"></asp:DropDownList>
                            <asp:Label ID="Label6" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ราคาทุน</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtCost" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            <asp:Label ID="Label19" runat="server" CssClass="zRemark" Text="*"></asp:Label> </td> 
                    </tr> 
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            ราคาขาย</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            <asp:Label ID="Label18" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            การคิดภาษี</td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsVat" runat="server" RepeatDirection="Horizontal" Width="305px">
                                <asp:ListItem Value="1">ราคาขายรวม VAT</asp:ListItem>
                                <asp:ListItem Value="0">ราคาขายไม่รวม VAT</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            การคิดส่วนลด</td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsDiscount" runat="server" RepeatDirection="Horizontal" Width="315px">
                                <asp:ListItem Value="1">คิดส่วนลด</asp:ListItem>
                                <asp:ListItem Value="0">ไม่คิดส่วนลด</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            การแก้ไขราคาขาย</td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsEdit" runat="server" RepeatDirection="Horizontal" Width="347px">
                                <asp:ListItem Value="Y">ได้</asp:ListItem>
                                <asp:ListItem Value="N">ไม่ได้</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr height="25">
                        <td width="50" style="height: 5px">
                        </td>
                        <td style="height: 5px; width: 150px;">
                            การคืนเงินสมาชิก
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbtIsRefund" runat="server" RepeatDirection="Horizontal" Width="348px">
                                <asp:ListItem Value="Y">คืน</asp:ListItem>
                                <asp:ListItem Value="N">ไม่คืน</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                     <tr height="25">
                        <td width="50" style="height: 5px"></td>
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
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            ระยะเวลาการสั่งซื้อ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtime" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            วัน&nbsp;<asp:Label ID="Label20" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            จำนวนสั่งซื้อต่อ Lot</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label21" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr>
                     <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            ระยะเวลาการผลิต</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLeadtimePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            วัน&nbsp;<asp:Label ID="Label2" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td>
                    </tr> 

                    <tr height="25px">
                        <td width="50" style="height: 25px"></td>
                        <td style="height: 25px; width: 150px;">
                            จำนวนผลิตต่อ Lot</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLotSizePD" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="Label8" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                    </tr> 

                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            อายุ</td> 
                        <td colspan="3">
                            <asp:TextBox ID="txtAge" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px">1</asp:TextBox>
                            ปี</td> 
                    </tr> 
                     <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            <span lang="TH" style="font-size: 14pt; line-height: 115%; font-family: 'Angsana New';
                                mso-ansi-font-size: 11.0pt; mso-ascii-font-family: Calibri; mso-fareast-font-family: Calibri;
                                mso-hansi-font-family: Calibri; mso-ansi-language: EN-US; mso-fareast-language: EN-US;
                                mso-bidi-language: TH">ขนาดบรรจุ(ใน </span><span style="font-size: 11pt; line-height: 115%;
                                    font-family: Times New Roman; mso-fareast-font-family: Calibri; mso-ansi-language: EN-US;
                                    mso-fareast-language: EN-US; mso-bidi-language: TH; mso-bidi-font-size: 14.0pt;
                                    mso-bidi-font-family: 'Angsana New'">1 </span><span lang="TH" style="font-size: 14pt;
                                        line-height: 115%; font-family: 'Angsana New'; mso-ansi-font-size: 11.0pt; mso-ascii-font-family: Calibri;
                                        mso-fareast-font-family: Calibri; mso-hansi-font-family: Calibri; mso-ansi-language: EN-US;
                                        mso-fareast-language: EN-US; mso-bidi-language: TH">หน่วยผลิต)</span></td> 
                        <td width="150px">
                            <asp:TextBox ID="txtPacksize" runat="server" CssClass="zTextboxR" MaxLength="50" Width="100px"></asp:TextBox>
                            &nbsp;&nbsp; &nbsp;<asp:Label ID="Label22" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                        <td width="50px">
                            หน่วย</td> 
                        <td width="200px">
                            <asp:DropDownList ID="cmbUnitPack" runat="server" CssClass="zComboBox" Width="105px"></asp:DropDownList>
                            <asp:Label ID="Label23" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                        </td> 
                    </tr>
                    <tr height="25px">
                        <td width="50px">
                        </td>
                        <td style="width: 150px">
                            กลุ่มสินค้าในกระบวนการผลิต</td> 
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProduceGroup" runat="server" CssClass="zComboBox" Width="205px">
                            </asp:DropDownList>
                        </td>
                    </tr>                     
                    <tr height="25">
                        <td width="50">
                        </td>
                        <td style="width: 150px">
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                            <asp:TextBox ID="txtPBLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
            </td> 
        </tr> 
    </table>

</asp:Content>
