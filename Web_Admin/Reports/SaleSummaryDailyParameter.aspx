<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SaleSummaryDailyParameter.aspx.cs" Inherits="Reports_SaleSummaryDailyParameter" Title="รายงานสรุปยอดขายรายวัน(ฝ่ายการตลาด)" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
       <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
       <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr height="5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr height="25px">
                <td width="5px">
                </td>
                <td class="headtext">
                    &nbsp;รายงานสรุปยอดขายรายวัน(ฝ่ายการตลาด)</td>
            </tr> 
            <tr height="10px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="500px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" height="20px">
                                &nbsp;รายงานสรุปยอดขายรายวัน(ฝ่ายการตลาด)</td> 
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td width="100px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td width="40px">
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                ปี พ.ศ.</td>  
                            <td width="150px">
                                <asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="59px" CssClass="zTextboxR" AutoPostBack="True"></asp:TextBox>
                                <asp:Label ID="label5" runat="server" ForeColor="red">*</asp:Label></td> 
                            <td width="100px">
                                </td>  
                            <td width="150px">
                                &nbsp;</td> 
                            <td>
                            </td>  
                        </tr>
                       <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                </td>  
                            <td width="150px">
                                </td> 
                            <td width="100px">
                                </td>  
                            <td width="150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                </td>  
                            <td width="150px">
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="102px" OnClick="btnReport_Click" /></td> 
                            <td width="100px">
                                </td>  
                            <td width="150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                      </table>
                </td>
            </tr> 
        </table>
</asp:Content>

