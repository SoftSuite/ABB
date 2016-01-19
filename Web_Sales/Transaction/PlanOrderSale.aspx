<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanOrderSale.aspx.cs" Inherits="Transaction_PlanOrderSale" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ประมาณการจำหน่ายสินค้า</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:5px; height: 5px;">
                            </td> 
                            <td style="height: 5px">
                                </td> 
                        </tr>
                        <tr style="height:25px">
                            <td style="width:5px">&nbsp;
                            </td> 
                            <td class="toolbarplace"><asp:LinkButton ID="btnSave" CssClass="toolbarbutton" runat="server" OnClick="btnSave_Click">LinkButton</asp:LinkButton>
                                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="true"
                                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"/></td> 
                        </tr>
                        <tr style="height:5px">
                            <td style="width:5px">
                            </td> 
                            <td></td> 
                        </tr>
                        <tr>
                            <td style="width:5px">
                            </td> 
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" width="500" class="searchTable" bgcolor="#ffffcc">
                                    <tr style="height:5px">
                                        <td style="width: 50px"></td> 
                                        <td style="width: 200px"></td> 
                                        <td style="width: 65px"></td> 
                                        <td></td> 
                                    </tr>
                                    <tr style="height:25px">
                                        <td align="right" style="width: 50px">สินค้า&nbsp;:&nbsp;
                                        </td> 
                                        <td style="width: 200px">
                                            <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label></td> 
                                        <td style="width: 65px">
                                            &nbsp;หน่วยนับ&nbsp;:&nbsp;</td> 
                                        <td style="width: 185px">
                                            <asp:Label ID="lblUnitName" runat="server" Text=""></asp:Label></td> 
                                    </tr>
                                    <tr style="height:25px">
                                        <td align="right" style="width: 50px">เดือน&nbsp;:&nbsp;
                                        </td> 
                                        <td style="width: 200px">
                                            <asp:Label ID="lblMonthName" runat="server" Text=""></asp:Label></td> 
                                        <td colspan="2">
                                            <asp:CheckBox ID="chkCopyAll" runat="server" Text="คัดลอกไปทุกเดือน" />
                                            <asp:Label ID="lblStatus" runat="server" CssClass="zHidden"></asp:Label></td> 
                                    </tr>
                                    <tr style="height:5px">
                                        <td style="width: 50px"></td> 
                                        <td style="width: 200px"></td> 
                                        <td style="width: 65px"></td> 
                                        <td></td> 
                                    </tr>
                                </table> 
                            </td> 
                        </tr>
                        <tr style="height:5px">
                            <td style="width:5px">
                            </td> 
                            <td></td> 
                        </tr>
                        <tr>
                            <td style="width:5px">
                            </td> 
                            <td>
                                <asp:GridView ID="grvPlanOrderSale" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="500px" ShowFooter="True" DataKeyNames="RANK" DataSourceID="PlanOrderSaleDataSource" OnRowCommand="grvPlanOrderSale_RowCommand" OnRowDataBound="grvPlanOrderSale_RowDataBound" OnRowDeleted="grvPlanOrderSale_RowDeleted">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbDelete" AlternateText="ลบ" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imbInsert" AlternateText="บันทึก" runat="server" CausesValidation="False" CommandName="Insert" ImageUrl="~/Images/icn_save.gif"/>
                                            </FooterTemplate> 
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="20px" /> 
                                        </asp:TemplateField>
                                       <asp:BoundField DataField="RANK" >
                                            <ControlStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sale">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSaleName" runat="server" Text='<%# Bind("SALENAME") %>'></asp:Label> 
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="cmbSaleman" runat="server" CssClass="zCombobox" Width="360px"></asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle Width="365px" /> 
                                            <HeaderStyle Width="365px" /> 
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่สั่ง">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQtyEdit" runat="server" CssClass="zTextboxR" Width="95px" Text='<%# Convert.ToDouble(Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="95px" Text="1"></asp:TextBox>
                                            </FooterTemplate> 
                                            <ItemStyle HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                       <asp:BoundField DataField="SALEMAN" >
                                            <ControlStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="t_headtext" />
                                    <AlternatingRowStyle CssClass="t_alt_bg" />
                                    <PagerSettings Visible="False" />            
                                </asp:GridView>
                                <asp:ObjectDataSource ID="PlanOrderSaleDataSource" runat="server" DeleteMethod="DeletePlanOrderSale"
                                    OldValuesParameterFormatString="{0}" SelectMethod="GetPlanOrderSale"
                                    TypeName="PlanOrderSale">
                                    <DeleteParameters>
                                        <asp:Parameter Name="RANK" Type="Double" />
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:QueryStringParameter DefaultValue="0" Name="plan" QueryStringField="plan" Type="Double" />
                                        <asp:QueryStringParameter DefaultValue="0" Name="month" QueryStringField="month"
                                            Type="Int32" />
                                        <asp:QueryStringParameter DefaultValue="0" Name="product" QueryStringField="product" Type="Double" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:GridView ID="grvPlanOrderSaleNew" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="500px" DataSourceID="PlanOrderSaleNewDataSource" OnRowCommand="grvPlanOrderSaleNew_RowCommand" OnRowDataBound="grvPlanOrderSaleNew_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbInsert" AlternateText="บันทึก" runat="server" CausesValidation="False" CommandName="Insert" ImageUrl="~/Images/icn_save.gif"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="20px" /> 
                                        </asp:TemplateField>
                                       <asp:BoundField DataField="RANK" >
                                            <ControlStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sale">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cmbSaleman" runat="server" CssClass="zCombobox" Width="360px"></asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <HeaderStyle Width="365px" /> 
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนที่สั่ง">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="95px" Text="1"></asp:TextBox>
                                            </ItemTemplate> 
                                            <ItemStyle HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                       <asp:BoundField DataField="SALEMAN" >
                                            <ControlStyle CssClass="zHidden" />
                                            <ItemStyle CssClass="zHidden" />
                                            <HeaderStyle CssClass="zHidden" />
                                            <FooterStyle CssClass="zHidden" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="t_headtext" />
                                    <AlternatingRowStyle CssClass="t_alt_bg" />
                                    <PagerSettings Visible="False" />            
                                </asp:GridView>
                                <asp:ObjectDataSource ID="PlanOrderSaleNewDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetPlanOrderSaleBlank" TypeName="PlanOrderSale"></asp:ObjectDataSource>
                            </td> 
                        </tr>
                        <tr style="height:5px">
                            <td style="width:5px">
                            </td> 
                            <td></td> 
                        </tr>
                    </table> &nbsp;
                </ContentTemplate>
            </asp:UpdatePanel>
       </div>
    </form>
</body>
</html>
