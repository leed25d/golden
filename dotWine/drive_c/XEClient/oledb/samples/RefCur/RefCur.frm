VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   2556
   ClientLeft      =   48
   ClientTop       =   276
   ClientWidth     =   3744
   LinkTopic       =   "Form1"
   ScaleHeight     =   2556
   ScaleWidth      =   3744
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Get Dept No. for an Employee No."
      Height          =   492
      Left            =   360
      TabIndex        =   1
      Top             =   1440
      Width           =   2892
   End
   Begin VB.CommandButton Command2 
      Caption         =   " Get Employee Records by Dept"
      Height          =   492
      Left            =   360
      TabIndex        =   0
      Top             =   360
      Width           =   2892
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()

Dim Oracon As ADODB.Connection
Dim recset As New ADODB.Recordset
Dim cmd As New ADODB.Command
Dim param1 As New ADODB.Parameter
Dim param2 As New ADODB.Parameter
Dim objErr As ADODB.Error
Dim Message, Title, Default, EmpNoValue

Message = "Enter an employee number (5000 - 9000)"
Title = "Choose an Employee"
Default = "7654"

On Error GoTo err_test

EmpNoValue = InputBox(Message, Title, Default)
If EmpNoValue = "" Then Exit Sub
  
If EmpNoValue < 5000 Or EmpNoValue > 9000 Then EmpNoValue = 7654

Set Oracon = CreateObject("ADODB.Connection")
Oracon.ConnectionString = "Provider=OraOLEDB.Oracle;" & _
                          "Data Source=db816;" & _
                          "User ID=scott;" & _
                          "Password=tiger;" & _
                          "PLSQLRSet=1;"
Oracon.Open

Set cmd = New ADODB.Command
Set cmd.ActiveConnection = Oracon

Set param1 = cmd.CreateParameter("param1", adSmallInt, adParamInput, , EmpNoValue)
cmd.Parameters.Append param1
Set param2 = cmd.CreateParameter("param2", adSmallInt, adParamOutput)
cmd.Parameters.Append param2

cmd.CommandText = "{CALL Employees.GetDept(?, ?)}"
Set recset = cmd.Execute
MsgBox "Number: " & EmpNoValue & "  Dept: " & recset.Fields("deptno").Value

Exit Sub

err_test:
  MsgBox Error$
  For Each objErr In Oracon.Errors
    MsgBox objErr.Description
  Next
  Oracon.Errors.Clear
  Resume Next
  
End Sub

Private Sub Command2_Click()

Dim Oracon As ADODB.Connection
Dim recset As New ADODB.Recordset
Dim cmd As New ADODB.Command
Dim param1 As New ADODB.Parameter
Dim param2 As New ADODB.Parameter
Dim objErr As ADODB.Error
Dim Message, Title, Default, DeptValue

Message = "Enter a department number (10, 20, or 30)"
Title = "Choose a Department"
Default = "30"

On Error GoTo err_test

DeptValue = InputBox(Message, Title, Default)
If DeptValue = "" Then Exit Sub
If DeptValue < 10 Or DeptValue > 30 Then DeptValue = 30
  
Set Oracon = CreateObject("ADODB.Connection")
Oracon.ConnectionString = "Provider=OraOLEDB.Oracle;" & _
                          "Data Source=db816;" & _
                          "User ID=scott;" & _
                          "Password=tiger;" & _
                          "PLSQLRSet=1;"
Oracon.Open

Set cmd = New ADODB.Command
Set cmd.ActiveConnection = Oracon

Set param1 = cmd.CreateParameter("param1", adSmallInt, adParamInput, , DeptValue)
cmd.Parameters.Append param1
Set param2 = cmd.CreateParameter("param2", adSmallInt, adParamOutput)
cmd.Parameters.Append param2

cmd.CommandText = "{CALL Employees.GetEmpRecords(?, ?)}"
Set recset = cmd.Execute
Do While Not recset.EOF
   MsgBox "Number: " & recset.Fields("empno").Value & "  Name: " & _
   recset.Fields("ename").Value & "  Dept: " & recset.Fields("deptno").Value
   recset.MoveNext
Loop
  
Exit Sub
   
err_test:
  MsgBox Error$
  For Each objErr In Oracon.Errors
    MsgBox objErr.Description
  Next
  Oracon.Errors.Clear
  Resume Next
End Sub
