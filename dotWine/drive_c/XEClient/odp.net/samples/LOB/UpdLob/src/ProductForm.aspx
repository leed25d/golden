<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProductForm.aspx.vb" Inherits="UpdLob11.ProductForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ODP.NET LOB Type Sample - Product Form </title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="productsList" style="Z-INDEX: 101; LEFT: 176px; POSITION: absolute; TOP: 154px" runat="server" Height="47px" Width="219px"></asp:dropdownlist><asp:button id="ViewAdBtn" style="Z-INDEX: 102; LEFT: 174px; POSITION: absolute; TOP: 241px" runat="server" Height="24px" Width="127px" Text="View/Update Image"></asp:button>
			<asp:Button id="CloseBtn" style="Z-INDEX: 104; LEFT: 313px; POSITION: absolute; TOP: 242px" runat="server" Width="80px" Height="24px" Text="Close Form"></asp:Button>
			<asp:Panel id="Panel1" style="Z-INDEX: 100; LEFT: 88px; POSITION: absolute; TOP: 74px" runat="server" Height="268px" Width="478px" BackColor="Gainsboro" ForeColor="Gainsboro">Panel 
<asp:label id="Label1" runat="server" Width="427px" Height="45px" ForeColor="Gray" Font-Bold="True">Select a  product for which you wish to view/update image</asp:label></asp:Panel></form>
	</body>
</HTML>
