<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PlanPurchase.aspx.cs" Inherits="Transaction_PlanPurchase" Title="แผนการสั่งซื้อวัตถุดิบ/สินค้า" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;แผนการสั่งซื้อวัตถุดิบ/สินค้า</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnBackClick="BackClick"/>
                <asp:LinkButton ID="btnCalculate" runat="server" CssClass="toolbarbutton" OnClick="btnCalculate_Click" Visible="False">LinkButton</asp:LinkButton></td> 
        </tr>
        <tr height="3">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="1550" class="searchTable">
                    <tr height="10px">
                        <td style="width: 140px;"></td>
                        <td style="width: 170px;"></td>
                        <td style="width: 120px;"></td>
                        <td style="width: 170px;"></td>
                        <td style="width: 100px"></td>
                        <td style="width: 195px"></td>
                        <td style="width: 730px"></td>
                    </tr>
                    <tr height="25">
                        <td align="right" style="width: 140px">
                            เดือน :&nbsp;
                        </td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbMonth" runat="server" CssClass="zComboBox" Width="160px">
                            </asp:DropDownList></td>
                        <td align="right" style="width: 120px">
                            ปี พ.ศ. :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:TextBox ID="txtYear" runat="server" CssClass="zTextbox-View" Width="50px" ReadOnly="True"></asp:TextBox></td>
                        <td align="right" style="width: 100px">
                        </td>
                        <td style="width: 195px">
                        </td>
                        <td style="width: 730px">
                        </td>
                    </tr>
                    <tr height="25">
                        <td style="width: 140px" align="right">
                            ประเภทสินค้า/วัตถุดิบ :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zComboBox" Width="160px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="width: 120px" align="right">
                            กลุ่มสินค้า/วัตถุดิบ :&nbsp;</td>
                        <td style="width: 170px">
                            <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zComboBox" Width="160px">
                            </asp:DropDownList></td>
                        <td style="width: 100px" align="right">
                            สินค้า/วัตถุดิบ :&nbsp;</td>
                        <td style="width: 195px">
                            <asp:TextBox ID="txtProductKey" runat="server" CssClass="zTextbox" Width="170px"></asp:TextBox></td>
                        <td style="width: 730px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /> 
                        </td>
                    </tr>
                    <tr height="10">
                        <td style="width: 140px">
                        </td>
                        <td style="width: 170px">
                        </td>
                        <td style="width: 120px">
                        </td>
                        <td style="width: 170px">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 195px">
                        </td>
                        <td style="width: 730px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="10px">
            <td style="height: 5px"></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPlanItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="1550px" OnRowDataBound="grvPlanItem_RowDataBound" OnRowCommand="grvPlanItem_RowCommand" >
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField HeaderText="สินค้า">
                            <ItemTemplate>
                                <asp:Label ID="lblProductName" runat="server" Text = '<%# Bind("PRODUCTNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="top" /> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "1" SortExpression="DAY1" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn1" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY1")) ==0 ? space : Convert.ToDouble(Eval("DAY1")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "2" SortExpression="DAY2" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn2" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY2")) ==0 ? space : Convert.ToDouble(Eval("DAY2")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "3" SortExpression="DAY3" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn3" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY3")) ==0 ? space : Convert.ToDouble(Eval("DAY3")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "4" SortExpression="DAY4" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn4" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY4")) ==0 ? space : Convert.ToDouble(Eval("DAY4")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "5" SortExpression="DAY5" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn5" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY5")) ==0 ? space : Convert.ToDouble(Eval("DAY5")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "6" SortExpression="DAY6" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn6" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY6")) ==0 ? space : Convert.ToDouble(Eval("DAY6")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "7" SortExpression="DAY7" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn7" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY7")) ==0 ? space : Convert.ToDouble(Eval("DAY7")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "8" SortExpression="DAY8" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn8" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY8")) ==0 ? space : Convert.ToDouble(Eval("DAY8")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "9" SortExpression="DAY9" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn9" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY9")) ==0 ? space : Convert.ToDouble(Eval("DAY9")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "10" SortExpression="DAY10" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn10" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY10")) ==0 ? space : Convert.ToDouble(Eval("DAY10")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "11" SortExpression="DAY11" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn11" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY11")) ==0 ? space : Convert.ToDouble(Eval("DAY11")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "12" SortExpression="DAY12" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn12" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY12")) ==0 ? space : Convert.ToDouble(Eval("DAY12")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "13" SortExpression="DAY13" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn13" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY13")) ==0 ? space : Convert.ToDouble(Eval("DAY13")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "14" SortExpression="DAY14" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn14" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY14")) ==0 ? space : Convert.ToDouble(Eval("DAY14")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "15" SortExpression="DAY15" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn15" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY15")) ==0 ? space : Convert.ToDouble(Eval("DAY15")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "16" SortExpression="DAY16" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn16" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY16")) ==0 ? space : Convert.ToDouble(Eval("DAY16")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "17" SortExpression="DAY17" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn17" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY17")) ==0 ? space : Convert.ToDouble(Eval("DAY17")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "18" SortExpression="DAY18" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn18" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY18")) ==0 ? space : Convert.ToDouble(Eval("DAY18")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "19" SortExpression="DAY19" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn19" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY19")) ==0 ? space : Convert.ToDouble(Eval("DAY19")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "20" SortExpression="DAY20" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn20" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY20")) ==0 ? space : Convert.ToDouble(Eval("DAY20")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "21" SortExpression="DAY21" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn21" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY21")) ==0 ? space : Convert.ToDouble(Eval("DAY21")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "22" SortExpression="DAY22" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn22" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY22")) ==0 ? space : Convert.ToDouble(Eval("DAY22")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "23" SortExpression="DAY23" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn23" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY23")) ==0 ? space : Convert.ToDouble(Eval("DAY23")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "24" SortExpression="DAY24" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn24" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY24")) ==0 ? space : Convert.ToDouble(Eval("DAY24")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "25" SortExpression="DAY25" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn25" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY25")) ==0 ? space : Convert.ToDouble(Eval("DAY25")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "26" SortExpression="DAY26" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn26" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY26")) ==0 ? space : Convert.ToDouble(Eval("DAY26")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "27" SortExpression="DAY27" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn27" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY27")) ==0 ? space : Convert.ToDouble(Eval("DAY27")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "28" SortExpression="DAY28" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn28" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY28")) ==0 ? space : Convert.ToDouble(Eval("DAY28")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "29" SortExpression="DAY29" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn29" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY29")) ==0 ? space : Convert.ToDouble(Eval("DAY29")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "30" SortExpression="DAY30" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn30" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY30")) ==0 ? space : Convert.ToDouble(Eval("DAY30")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText = "31" SortExpression="DAY31" >
                            <ItemStyle Width="40px" HorizontalAlign="right"/>
                            <HeaderStyle Width="40px" />  
                            <ItemTemplate>
                                <asp:Label ID="btn31" runat="server" CssClass="planbutton" Text='<%# Convert.ToDouble(Eval("DAY31")) ==0 ? space : Convert.ToDouble(Eval("DAY31")).ToString(ABB.Data.Constz.IntFormat) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>