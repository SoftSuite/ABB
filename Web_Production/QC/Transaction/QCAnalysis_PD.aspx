<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="QCAnalysis_PD.aspx.cs" Inherits="Transaction_QCAnalysis_PD"  Title="การวิเคราะห์คุณภาพ" %>

<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;การวิเคราะห์คุณภาพ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true" NameBtnPrint="พิมพ์ใบแจ้ง QC"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="ยืนยัน"
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
                &nbsp;
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr height="25px" width="1500px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="750px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" width="600px" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px"></td>
                                    <td style="width: 75px">
                                        <asp:TextBox ID="txtSender" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox></td>
                                    <td style="width: 75px"></td>
                                    <td style="width: 146px"></td>
                                    <td style="width: 146px">
                                    </td>
                                    <td style="width: 146px">
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px">
                                        เลขที่ส่งตรวจ</td>
                                    <td style="width: 75px">
                                        <asp:TextBox ID="txtQCCode" runat="server" Width="110px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox></td>
                                    <td style="width: 75px">
                                        วันที่ส่งตรวจ</td>
                                    <td style="width: 146px"><uc2:DatePickerControl ID="ctlQCDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 146px">
                                        เลขที่วิเคราะห์</td>
                                    <td style="width: 146px"><asp:TextBox ID="txtNo" runat="server" Width="110px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:Label id="lblRemark" runat="server" CssClass="zRemark" Text="*"></asp:Label></td>
                                </tr>
                                
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px">
                                        ผู้ส่งตรวจ</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDivision" runat="server" Width="110px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCreateby" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="200px"></asp:TextBox>
                                </td>
                                    <td colspan="1">
                                        วันที่วิเคราะห์
                                    </td>
                                    <td colspan="1"><uc2:DatePickerControl ID="ctlAnaDate" runat="server" Enabled="true" />
                                        <asp:Label id="Label1" runat="server" CssClass="zRemark" Text="*"></asp:Label>
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px">
                                        เลขที่อ้างอิง</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtInvNo" runat="server" Width="110px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>&nbsp;
                                </td>
                                    <td colspan="1">
                                    </td>
                                    <td colspan="1">
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 120px">
                                        </td>
                                    <td colspan="3">
                                        &nbsp;
                                </td>
                                    <td colspan="1">
                                    </td>
                                    <td colspan="1">
                                    </td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">

                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                                Width="792px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" >
                                <PagerSettings Visible="False" />
                                <Columns>
                                 <asp:TemplateField HeaderText="ผ่าน">
				     <ItemTemplate>
                                        <asp:RadioButton ID="rbtPass" runat="server" GroupName='<%# DataBinder.Eval(Container.DataItem, "NO").ToString()%>'
					 Checked='<%# DataBinder.Eval(Container.DataItem, "QCRESULT").ToString().ToUpper()=="Y"?true:false%>' />
						</ItemTemplate>
						<ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
				      <asp:TemplateField HeaderText="ไม่ผ่าน">
				     <ItemTemplate>
                                       <asp:RadioButton ID="rbtNotPass" runat="server" GroupName='<%# DataBinder.Eval(Container.DataItem, "NO").ToString()%>'
				        Checked='<%# DataBinder.Eval(Container.DataItem, "QCRESULT").ToString().ToUpper()=="N"?true:false%>' />
						</ItemTemplate>
						<ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    
												<asp:TemplateField HeaderText="รหัสสินค้า">
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="90px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="95px" />
                                    </asp:TemplateField>

                                   

                                    <asp:TemplateField HeaderText="ชื่อสินค้า">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPDName" runat="server" Width="200px" Text='<%# Bind("PDNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="200px" />
                                    </asp:TemplateField>
          
                                    <asp:TemplateField HeaderText="จำนวนที่ผลิตได้">
                                        <ItemTemplate>
                                            <asp:Label ID="txtQCQtyView" runat="server" Width="100px" Text='<%# Bind("PDQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="1st Sampling">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtS1" runat="server" CssClass="zTextboxR" Width="50px" Text='<%# Bind("QCQTY1") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle  Width="55px" HorizontalAlign="Right" />
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="2nd Sampling">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtS2" runat="server" CssClass="zTextboxR" Width="50px" Text='<%# Bind("QCQTY2") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle  Width="55px" HorizontalAlign="Right" />
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="3rd Sampling">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtS3" runat="server" CssClass="zTextboxR" Width="50px" Text='<%# Bind("QCQTY3") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle  Width="55px" HorizontalAlign="Right" />
                                        <HeaderStyle Width="55px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <ItemTemplate>
                                            <asp:Label ID="txtUNameView" runat="server" Width="70px" Text='<%# Bind("UNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="70px" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="หมายเหตุ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQCRemarkView" runat="server" CssClass="zTextbox" Width="175px" Text='<%# Bind("QCREMARK") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle  Width="180px" HorizontalAlign="Right" />
                                        <HeaderStyle Width="180px" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="กำหนดวันที่ตรวจ QC แล้ว">
                                        <ItemTemplate>
                                            <uc2:DatePickerControl ID="txtDate" runat="server" DateValue='<%# Convert.IsDBNull(Eval("DUEDATE")) ? new DateTime(1,1,1) : Convert.ToDateTime(Eval("DUEDATE")) %>'></uc2:DatePickerControl>  
                                        </ItemTemplate>

                                        <HeaderStyle Width="150px" />
                                    </asp:TemplateField>
                                    
                                 <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>

                                     
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">

                        </td>
                    </tr>
                </table>
            </td> 
            <td width="2px">
                &nbsp;
            </td> 
            
        </tr>
    </table>
</asp:Content>




