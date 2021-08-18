﻿Imports System.Data.OleDb

Public Class CurrentCheckIn
#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub




#End Region


    Private Sub cmdCLose_Click(sender As Object, e As EventArgs) Handles cmdCLose.Click
        Me.Close()
    End Sub

    Private Sub CurrentCheckIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        checkOpen()
        dt = New DataTable("CheckedIN")

        sql = "SELECT G.guestID as ID, guestName as Guest, R.roomNum as RoomNum, roomType as Type, roomRate as Rate, NumOfOccupants as GuestsInRoom,checkIN as InDate, checkOUT as OutDate FROM Guest G, Reservation RV, Rooms R
              WHERE G.guestID = RV.guestID AND RV.roomNum = R.roomNum AND Remarks = 'Checkin'"

        cmd = New OleDbCommand(sql, con)
        dr = cmd.ExecuteReader

        dt.Load(dr)
        dgCheckedIn.DataSource = dt
        dr.Dispose()

        dgCheckedIn.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        con.Close()
    End Sub

    Private Sub dgCheckedIn_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgCheckedIn.MouseDoubleClick
        CheckOut.lblGuestID.Text = Me.dgCheckedIn.CurrentRow.Cells("ID").Value
        CheckOut.txtGuest.Text = Me.dgCheckedIn.CurrentRow.Cells("Guest").Value
        CheckOut.txtRoomNum.Text = Me.dgCheckedIn.CurrentRow.Cells("RoomNum").Value
        CheckOut.txtRoomType.Text = Me.dgCheckedIn.CurrentRow.Cells("Type").Value
        CheckOut.txtRate.Text = Me.dgCheckedIn.CurrentRow.Cells("Rate").Value
        CheckOut.dtpCheckIn.Text = Me.dgCheckedIn.CurrentRow.Cells("InDate").Value
        CheckOut.dtpCheckOut.Text = Me.dgCheckedIn.CurrentRow.Cells("OutDate").Value
        CheckOut.txtNumGuest.Text = Me.dgCheckedIn.CurrentRow.Cells("GuestsInRoom").Value

        Me.Close()
    End Sub
End Class