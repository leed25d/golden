<%@ Page language="c#" Codebehind="productForm.aspx.cs" AutoEventWireup="false" Inherits="DistributedTransaction.productForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Products Form</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:DataGrid id="hqDataGrid" style="Z-INDEX: 102; LEFT: 50px; POSITION: absolute; TOP: 81px" runat="server" Width="443px" Height="169px" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" BorderColor="SlateGray">
				<HeaderStyle Font-Bold="True"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="product_id" ReadOnly="True" HeaderText="Product ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="Product_Name" ReadOnly="True" HeaderText="Product Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Price" HeaderText="Price ($)"></asp:BoundColumn>
					<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Update Price" CancelText="Cancel" EditText="Edit Row"></asp:EditCommandColumn>
				</Columns>
			</asp:DataGrid>
			<asp:Label id="hqLabel" style="Z-INDEX: 103; LEFT: 56px; POSITION: absolute; TOP: 46px" runat="server" Height="23px" Width="195px" Font-Bold="True" ForeColor="Black">Head Quarters Product Data </asp:Label>
			<asp:Label id="regionalLabel" style="Z-INDEX: 104; LEFT: 55px; POSITION: absolute; TOP: 287px" runat="server" Height="16px" Width="180px" Font-Bold="True" ForeColor="Black">Regional Product Data</asp:Label>
			<asp:DataGrid id="regionDataGrid" style="Z-INDEX: 105; LEFT: 50px; POSITION: absolute; TOP: 319px" runat="server" Height="171px" Width="441px" BorderColor="SlateGray" AutoGenerateColumns="False">
				<HeaderStyle Font-Bold="True"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="Product_id" HeaderText="Product ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="Product_Name" HeaderText="Product  Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Price" HeaderText="Price ($)"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
			<asp:Button id="closeBtn" style="Z-INDEX: 106; LEFT: 411px; POSITION: absolute; TOP: 510px" runat="server" Height="26px" Width="75px" Text="Close" Font-Bold="True"></asp:Button></form>
	</body>
</HTML>
