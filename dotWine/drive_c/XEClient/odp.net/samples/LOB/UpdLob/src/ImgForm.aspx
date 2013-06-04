<%@ Import Namespace="System.IO" %>
<%@ Page AutoEventWireup="false" Codebehind="ImgForm.aspx.vb" Inherits="UpdLob11.ImgForm" Language="vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ODP.NET LOB Type Sample - ImgForm </title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form method="post" encType="multipart/form-data" runat="server">
			<asp:image id="existingImagePicBx" style="Z-INDEX: 101; LEFT: 179px; POSITION: absolute; TOP: 146px" runat="server" Width="297px" Height="187px"></asp:image><asp:image id="newImagePicBx" style="Z-INDEX: 102; LEFT: 179px; POSITION: absolute; TOP: 346px" runat="server" Width="299px" Height="190px"></asp:image><asp:label id="Label3" style="Z-INDEX: 103; LEFT: 48px; POSITION: absolute; TOP: 153px" runat="server" Width="131px" Height="20px"> Existing Image</asp:label><asp:label id="Label4" style="Z-INDEX: 104; LEFT: 63px; POSITION: absolute; TOP: 345px" runat="server" Width="105px" Height="23px"> New Image</asp:label><asp:button id="CloseButton" style="Z-INDEX: 105; LEFT: 315px; POSITION: absolute; TOP: 598px" runat="server" Width="55px" Height="24px" Text="Back"></asp:button><INPUT id="BrowseFile" style="Z-INDEX: 107; LEFT: 180px; WIDTH: 298px; POSITION: absolute; TOP: 552px; HEIGHT: 24px" type="file" size="30" name="Browse" runat="server" title="Browse">
			<INPUT id="LoadButton" style="Z-INDEX: 106; LEFT: 494px; WIDTH: 81px; POSITION: absolute; TOP: 552px; HEIGHT: 24px" type="button" value="Load Image" runat="server">
			<asp:button id="SaveBtn" style="Z-INDEX: 108; LEFT: 209px; POSITION: absolute; TOP: 598px" runat="server" Width="58px" Height="24px" Text="Save"></asp:button><asp:label id="HeaderLbl" style="Z-INDEX: 110; LEFT: 71px; POSITION: absolute; TOP: 62px" runat="server" Width="515px" Height="49px" Font-Size="Medium" ForeColor="Black"></asp:label><span id="span1" style="FONT-FAMILY: Arial" runat="server"></span></form>
	</body>
</HTML>
