'**************************************************************************
'@author                        :  Jagriti
'@version                       :  1.0
'Development Environment        :  MS Visual Studio.NET
'Name of the File               :  GetImage.aspx.vb
'Creation/Modification History  :
'                                  17-Feb-2003     Created

'Overview:
'The purpose of this file is to return image as a byte stream for the 
'selected product.
'**************************************************************************

' Importing Standard namespace 
Imports System.Data
Imports System.IO
Imports System.Drawing.Imaging

' ODP.NET specific Namespaces referenced in this file
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class GetImage
    Inherits System.Web.UI.Page

    Dim conn As OracleConnection
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    '***************************************************************
    'On page load, this page returns the product image.
    '***************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim len As Integer

        ' Get value for Product ID
        Dim param As String = Request.Params("productid")

        Try

            ' Command for fetching product image from database
            Dim viewPicCmd As New OracleCommand("SELECT Product_id, Ad_Image FROM PrintMedia " & _
                                                " WHERE product_id = " + param, ConnectionManager.conn)

            ' Sends the CommandText to the Connection and builds OracleDataReader
            Dim viewPicReader As OracleDataReader = viewPicCmd.ExecuteReader()

            Dim recordExist As Boolean = viewPicReader.Read()

            If (recordExist) Then

                ' If image exists
                If (viewPicReader.GetValue(1).ToString() <> "") Then
                    Response.Clear()

                    ' Read Blob data into a byte array
                    Dim byteBLOBData() As Byte = viewPicReader.GetValue(1)

                    ' Set content type as image, data type as jpeg
                    Response.ContentType = "image/jpeg"

                    ' Write byte array to response 
                    Response.BinaryWrite(byteBLOBData)

                    Response.Flush()
                    Response.Close()
                End If
            End If
        Catch ex As Exception
            Response.Redirect("Error.aspx?error=" + ex.Message)
        End Try

    End Sub
End Class
