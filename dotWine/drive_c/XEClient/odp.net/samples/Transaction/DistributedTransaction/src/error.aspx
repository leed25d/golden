<%@ Page language="c#" Codebehind="error.aspx.cs" AutoEventWireup="false" Inherits="DistributedTransaction.error" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>error</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="error" method="post" runat="server">
			<%= Request.QueryString["error"] %>
		</form>
	</body>
</HTML>
