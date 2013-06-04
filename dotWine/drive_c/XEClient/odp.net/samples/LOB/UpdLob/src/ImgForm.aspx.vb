'************************************************************************************
'@author                        :  Jagriti
'@version                       :  1.0
'Development Environment        :  MS Visual Studio.NET
'Name of the File               :  ImgForm.aspx.vb
'Creation/Modification History  :
'                                  17-Feb-2003     Created

'Overview:
'This file contains code that demonstrates how LOBs can be updated
'using ODP.NET LOB objects. This webform is called from "Products Form". When this 
'form gets control then if the corresponding image for the selected product is
'existing it appears in the "existingPicImageBx". To update the product's image
'the user can select a new image by clicking the "Browse" button. To load this image
'into "newImagePicBx" the user can click "Load Image" button. To update this image
'to database the user can click "Save" button. Prior to updating the image,
'a transaction is started. OracleDataReader is used to get the resultset for the 
'Product selected."GetOracleBlobForUpdate" is used to lock the row, it also returns
'an updateable LOB object of specified column for update. This BLOB object is
'assigned the byte array for the new image chosen and the transaction is commited.
'***********************************************************************************

' Standard Namespaces referenced in this sample
Imports System.Data
Imports System.IO
Imports System.Drawing.Imaging

' ODP.NET specific Namespaces referenced in this sample
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Public Class ImgForm
    Inherits System.Web.UI.Page

    'UI components
    Protected WithEvents existingImagePicBx As System.Web.UI.WebControls.Image
    Protected WithEvents newImagePicBx As System.Web.UI.WebControls.Image
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents CloseButton As System.Web.UI.WebControls.Button
    Protected WithEvents BrowseFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents SaveBtn As System.Web.UI.WebControls.Button
    Protected WithEvents HeaderLbl As System.Web.UI.WebControls.Label
    Protected WithEvents LoadButton As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents span1 As System.Web.UI.HtmlControls.HtmlGenericControl

    Dim imgURL As String
    Dim ProdID, prodName As String


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

    '************************************************************************************
    ' This method is executed when the page is loaded. If image for the selected product 
    ' is exists then it is displayed.
    '************************************************************************************
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ' Get value for "prodID" parameter
            ProdID = Request.Params("prodID")

            prodName = Request.Params("ProdName")

            HeaderLbl.Text = "View/Update Image for Product : " + prodName + " (" + ProdID + ")"

            ' Display existing image
            If (ConnectionManager.conn.ToString <> "") Then
                LoadImage()
            End If
        Catch ex As Exception
            Response.Redirect("Error.aspx?error=" + ex.Message)
        End Try
    End Sub

    '*************************************************************************************
    ' This code fires on the click event of the "Save" button. The purpose of this code is 
    ' to update LOB object using ODP.NET LOB object. 
    '*************************************************************************************
    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        Dim newImgUrl As String = Session("imgurl")
        Dim txn As OracleTransaction
        Try

            ' Check if the user has chosen an image using "Browse" button
            If (newImgUrl <> "") Then

                ' Start an Oracle Transaction
                txn = ConnectionManager.conn.BeginTransaction()

                ' Get datarow for selected Product ID
                Dim printmediaCmd As OracleCommand = New OracleCommand("SELECT product_id, ad_image " & _
                                                                       " FROM PrintMedia WHERE " & _
                                                                       " product_id =" + ProdID, ConnectionManager.conn)

                Dim printmediaDataReader As OracleDataReader = printmediaCmd.ExecuteReader()
                Dim recExist As Boolean = printmediaDataReader.Read()


                If recExist Then
                    ' Use 'GetOracleBlobForUpdate' to lock the row for updatation, 'orablob' is
                    ' opened in an updateable mode
                    Dim orablob As OracleBlob = printmediaDataReader.GetOracleBlobForUpdate(1)

                    ' providing read access to the file chosen using the 'Browse' button
                    Dim fs As FileStream = New FileStream(newImgUrl, FileMode.Open, FileAccess.Read)

                    ' Create a byte array of file stream length
                    Dim adImageData(fs.Length) As Byte

                    ' Read block of bytes from stream into the byte array
                    fs.Read(adImageData, 0, System.Convert.ToInt32(fs.Length))

                    ' Writing byte array to OracleBlob object
                    orablob.Write(adImageData, 0, fs.Length)

                    fs.Close()

                    ' Commit transaction
                    txn.Commit()

                End If

                'On successful updation, display the image in "exisitingImagePicBx"
                'and clear the "newImagePicBx"
                If (newImgUrl <> "") Then

                    'Show the image in picture box
                    existingImagePicBx.ImageUrl = Session("imgurl")

                    'Clear contents
                    newImagePicBx.ImageUrl = ""
                End If

                'Reset variables
                newImgUrl = ""

                'Display message on successful data updatation
                Response.Write("<b>Data saved successfully</b>")

            Else

                Response.Write("<b>Select an image for the advertisement!</b>")

            End If

        Catch ex As Exception
            ' Rollback Transaction in case of failure
            txn.Rollback()
            Response.Redirect("Error.aspx?error=" + ex.Message)
        End Try
    End Sub


    '***************************************************************************
    ' The purpose of this method is to display existing image for the product id
    ' selected.
    '***************************************************************************
    Private Sub LoadImage()

        Try
            'Clear contents
            existingImagePicBx.ImageUrl = Nothing
            newImagePicBx.ImageUrl = Nothing

            If (ProdID <> "") Then

                ' Use GetImage.aspx file to return image 
                imgURL = "GetImage.aspx?productid=" + ProdID

                existingImagePicBx.ImageUrl = imgURL
                
            End If

        Catch ex As Exception

            ' Error Handling
            Response.Redirect("Error.aspx?error=" + ex.Message)
        End Try
    End Sub

    '************************************************************************
    ' The purpose of this code is to load image into "newImagePicBx" from the 
    ' URL that is selected using "Browse" button"
    '************************************************************************
    Private Sub LoadButton_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.ServerClick

        ' If an image file is chosen
        If Not IsNothing(BrowseFile.PostedFile) Then

            ' Set the URL for "newImagePicBx" as the new chosen image
            newImagePicBx.ImageUrl = BrowseFile.PostedFile.FileName.ToString()

            ' Maintaining session state
            Session("imgurl") = BrowseFile.PostedFile.FileName.ToString()

        End If
    End Sub

    '**********************************************************************
    ' On click event of close button, navigate back to "ProductForm.aspx"
    '**********************************************************************
    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Response.Redirect("Productform.aspx")
    End Sub


End Class
