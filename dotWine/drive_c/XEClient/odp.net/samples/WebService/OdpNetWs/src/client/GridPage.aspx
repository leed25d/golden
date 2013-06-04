<%@ Page Language="vb" AutoEventWireup="false" Codebehind="GridPage.aspx.vb" Inherits="client.GridPage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>.NET Web Service using ODP.NET</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:table id="Table1" style="Z-INDEX: 100; LEFT: 18px; POSITION: absolute; TOP: 15px" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" Width="581px" Height="52px">
				<asp:TableRow>
					<asp:TableCell VerticalAlign="Middle" Font-Size="X-Large" Font-Names="Arial" HorizontalAlign="Center" Text=".NET Web Service using ODP.NET"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:button id="InsertCancelButton" style="Z-INDEX: 118; LEFT: 312px; POSITION: absolute; TOP: 502px" tabIndex="5" runat="server" Height="23px" Width="121px" Visible="False" Font-Names="Arial" Text="Hide Insert Form"></asp:button><asp:textbox id="TBCurrency" style="Z-INDEX: 113; LEFT: 457px; POSITION: absolute; TOP: 437px" tabIndex="4" runat="server" Width="139px" Height="22px" Visible="False"></asp:textbox><asp:textbox id="TBLanguage" style="Z-INDEX: 112; LEFT: 308px; POSITION: absolute; TOP: 438px" tabIndex="3" runat="server" Width="134px" Height="22px" Visible="False"></asp:textbox><asp:label id="LabelCurrency" style="Z-INDEX: 109; LEFT: 459px; POSITION: absolute; TOP: 410px" runat="server" Width="122px" Height="23px" Visible="False" Font-Names="Arial">Currency</asp:label><asp:label id="LabelLanguage" style="Z-INDEX: 108; LEFT: 309px; POSITION: absolute; TOP: 412px" runat="server" Width="122px" Height="23px" Visible="False" Font-Names="Arial">Language</asp:label><asp:label id="LabelPopulation" style="Z-INDEX: 107; LEFT: 166px; POSITION: absolute; TOP: 413px" runat="server" Width="122px" Height="23px" Visible="False" Font-Names="Arial">Population</asp:label><asp:label id="Label1" style="Z-INDEX: 101; LEFT: 23px; POSITION: absolute; TOP: 80px" runat="server" Width="563px" Height="13px" Font-Names="Arial">Country table received and updated through a Web Service that uses ODP.NET</asp:label><asp:datagrid id="countryGrid" style="Z-INDEX: 102; LEFT: 19px; POSITION: absolute; TOP: 126px" tabIndex="8" runat="server" BorderStyle="None" BorderColor="Black" Width="577px" Height="189px" Font-Names="Arial" PageSize="5" AllowPaging="True" AutoGenerateColumns="False" ToolTip="Country table data" GridLines="Horizontal" ForeColor="Black" BackColor="#E0E0E0">
				<HeaderStyle Font-Bold="True"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="countryname" ReadOnly="True" HeaderText="Country Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="population" HeaderText="Population"></asp:BoundColumn>
					<asp:BoundColumn DataField="language" ReadOnly="True" HeaderText="Language"></asp:BoundColumn>
					<asp:BoundColumn DataField="currency" ReadOnly="True" HeaderText="Currency"></asp:BoundColumn>
					<asp:ButtonColumn Text="Delete" HeaderText="Delete" CommandName="Delete"></asp:ButtonColumn>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Update" CancelText="Cancel" EditText="Edit"></asp:EditCommandColumn>
				</Columns>
				<PagerStyle NextPageText="Next Page " Font-Size="X-Small" Font-Names="Arial" PrevPageText="Previous Page"></PagerStyle>
			</asp:datagrid><asp:label id="Label2" style="Z-INDEX: 103; LEFT: 17px; POSITION: absolute; TOP: 343px" runat="server" Width="64px" Height="18px" Font-Names="Arial" Font-Size="Small" Font-Bold="True">Status : </asp:label><asp:label id="StatusText" style="Z-INDEX: 104; LEFT: 79px; POSITION: absolute; TOP: 344px" runat="server" Width="513px" Height="18px" Font-Names="Arial" Font-Size="Small" Font-Bold="True">Data loaded with the help of DataSet retrieved through  Web Service </asp:label><asp:linkbutton id="InsertButton" style="Z-INDEX: 105; LEFT: 17px; POSITION: absolute; TOP: 377px" tabIndex="6" runat="server" Width="255px" Height="20px" Font-Names="Arial" Font-Size="Small">Click here to insert a row in the table</asp:linkbutton><asp:label id="LabelCountryName" style="Z-INDEX: 106; LEFT: 17px; POSITION: absolute; TOP: 412px" runat="server" Width="122px" Height="23px" Visible="False" Font-Names="Arial">Country Name</asp:label><asp:textbox id="TBCountryName" style="Z-INDEX: 110; LEFT: 18px; POSITION: absolute; TOP: 438px" tabIndex="1" runat="server" Width="133px" Visible="False"></asp:textbox><asp:textbox id="TBPopulation" style="Z-INDEX: 111; LEFT: 165px; POSITION: absolute; TOP: 438px" tabIndex="2" runat="server" Width="130px" Height="22px" Visible="False"></asp:textbox><asp:button id="BInsert" style="Z-INDEX: 115; LEFT: 167px; POSITION: absolute; TOP: 502px" tabIndex="5" runat="server" Width="121px" Height="23px" Visible="False" Font-Names="Arial" Text="Insert Data"></asp:button><asp:label id="InsertStatus" style="Z-INDEX: 116; LEFT: 18px; POSITION: absolute; TOP: 472px" runat="server" Width="298px" Height="21px" Font-Names="Arial" ForeColor="Gray"></asp:label><asp:hyperlink id="HyperLink1" style="Z-INDEX: 117; LEFT: 326px; POSITION: absolute; TOP: 377px" tabIndex="7" runat="server" Width="267px" Height="17px" Font-Names="Arial" NavigateUrl="Main.aspx">Click here to go back to the Main page</asp:hyperlink></form>
	</body>
</HTML>
