<%@ Page Language="vb" AutoEventWireup="false" EnableViewState="true" Codebehind="ProductForm.aspx.vb" Inherits="UpdRefCursor.ProductForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ODP.NET REF Cursor Sample - Product Form</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:datagrid id="OrderableDataGrid" style="Z-INDEX: 101; LEFT: 28px; POSITION: absolute; TOP: 138px" runat="server" Width="259px" Height="136px" AutoGenerateColumns="False">
				<HeaderStyle Font-Size="4mm" Font-Bold="True"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="Product_ID" ReadOnly="True" HeaderText="Product ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="Product_Name" ReadOnly="True" HeaderText="Product Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Price" HeaderText="Price($)"></asp:BoundColumn>
				</Columns>
			</asp:datagrid><asp:datagrid id="UDevelopmentDataGrid" style="Z-INDEX: 102; LEFT: 313px; POSITION: absolute; TOP: 138px" runat="server" Width="300px" Height="138px" AutoGenerateColumns="False">
				<HeaderStyle Font-Size="4mm" Font-Bold="True"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="Product_ID" ReadOnly="True" HeaderText="Product ID"></asp:BoundColumn>
					<asp:BoundColumn DataField="Product_Name" HeaderText="Product Name"></asp:BoundColumn>
					<asp:BoundColumn DataField="Price" ReadOnly="True" HeaderText="Price($)"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Update">
						<ItemTemplate>
							<asp:CheckBox id="cbx" runat="server"></asp:CheckBox>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid><asp:label id="HeadingLbl" style="Z-INDEX: 103; LEFT: 207px; POSITION: absolute; TOP: 62px" runat="server" Width="323px" Height="17px" ForeColor="Blue" Font-Size="Large">Update Product Status Form</asp:label><asp:button id="UpdateButton" style="Z-INDEX: 104; LEFT: 620px; POSITION: absolute; TOP: 138px" runat="server" Width="95px" Height="29px" Text="Update Status"></asp:button><asp:label id="Label1" style="Z-INDEX: 105; LEFT: 317px; POSITION: absolute; TOP: 112px" runat="server" Width="275px" ForeColor="#404040" Font-Bold="True">List of Products Under Development</asp:label><asp:label id="Label2" style="Z-INDEX: 106; LEFT: 31px; POSITION: absolute; TOP: 114px" runat="server" Width="239px" Height="17px" ForeColor="#404040" Font-Bold="True">List of Orderable Products</asp:label>
			<asp:Button id="CloseButton" style="Z-INDEX: 107; LEFT: 621px; POSITION: absolute; TOP: 184px" runat="server" Width="95px" Height="29px" Text="Close Form"></asp:Button></form>
	</body>
</HTML>
