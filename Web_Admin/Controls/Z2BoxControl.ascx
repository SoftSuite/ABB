<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Z2BoxControl.ascx.cs" Inherits="Controls_Z2BoxControl" %>
<P><FONT face="Tahoma"><INPUT id="<%=lstAuth.ClientID%>_zLstSelect" type="hidden" name="<%=lstAuth.ClientID%>_zLstSelect"><INPUT id="<%=lstNoAuth.ClientID%>_zLstNoSelect" type="hidden" name="<%=lstNoAuth.ClientID%>_zLstNoSelect">
		<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="600" border="0">
			<TR>
				<TD><FONT face="Tahoma">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="600" border="0">
							<TR>
								<TD align="center" height="25">
									<asp:Label id="lblSource" runat="server">กลุ่มที่ไม่ถูกกำหนด</asp:Label></TD>
								<TD align="center" height="25"></TD>
								<TD align="center" height="25">
									<asp:Label id="lblDestination" runat="server">กลุ่มที่กำหนด</asp:Label></TD>
							</TR>
							<TR>
								<TD align="center" height="15"></TD>
								<TD align="center" height="15"></TD>
								<TD align="center" height="15"></TD>
							</TR>
							<TR>
								<TD align="center">&nbsp;
									<asp:ListBox id="lstNoAuth" runat="server" Width="200px" DESIGNTIMEDRAGDROP="187" Height="300px"
										SelectionMode="Multiple" CssClass="zTextBox"></asp:ListBox></TD>
								<TD align="center">
									<TABLE id="Table4" cellSpacing="3" cellPadding="1" width="100%" border="0">
										<TR>
											<TD align="center" height="25">
												<asp:Button id="btnAddAll" runat="server" Width="50px" CssClass="zButton" Text=">>" ToolTip="เพิ่มทั้งหมด"></asp:Button></TD>
										</TR>
										<TR>
											<TD align="center" height="25"></TD>
										</TR>
										<TR>
											<TD align="center" height="25">
												<asp:Button id="btnAddSel" runat="server" Width="50px" CssClass="zButton" Text=">" ToolTip="เพิ่มรายการที่เลือก"></asp:Button></TD>
										</TR>
										<TR>
											<TD align="center" height="25">
												<asp:Button id="btnRemSel" runat="server" Width="50px" CssClass="zButton" Text="<" ToolTip="ลบรายการที่เลือก"></asp:Button></TD>
										</TR>
										<TR>
											<TD align="center" height="25"></TD>
										</TR>
										<TR>
											<TD align="center" height="25">
												<asp:Button id="btnRemAll" runat="server" Width="50px" CssClass="zButton" Text="<<" ToolTip="ลบทั้งหมด"></asp:Button></TD>
										</TR>
									</TABLE>
								</TD>
								<TD align="center">
									<asp:ListBox id="lstAuth" runat="server" Width="200px" Height="300px" SelectionMode="Multiple"
										CssClass="zTextBox"></asp:ListBox></TD>
							</TR>
						</TABLE>
					</FONT>
				</TD>
			</TR>
		</TABLE>
	</FONT>
</P>