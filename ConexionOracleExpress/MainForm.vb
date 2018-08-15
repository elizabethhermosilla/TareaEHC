Imports System.Data.Odbc
Public Class MainForm

        Private Sub RecibirFoco(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBuscar.GotFocus
            Dim obj As TextBox = sender
            If obj.Text = "" Or obj.Text = "<Introduzca un nombre o telefono a buscar...>" Then
                obj.Text = ""
                obj.ForeColor = Color.Black
                obj.Font = New Font("Arial", 12.0, FontStyle.Regular)
            End If
        End Sub

        Public Sub Llenar()
            AbrirConexion()

            Dim da As New OdbcDataAdapter("Select * from datos_personales where UPPER(nombre) like'%" & UCase(txtBuscar.Text) & "%' or telefono like '%" & txtBuscar.Text & "%'", Conexion)
            Dim ds As New DataSet
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                DataGridView1.DataSource = ds.Tables(0)
            Else
                DataGridView1.DataSource = Nothing
            End If
        End Sub

        Private Sub LimpiaDatos()
            txtID.Text = "0"
            txtNombre.Clear()
            txtTelefono.Clear()
            txtFechaNacimiento.Clear()
            txtNombre.Focus()

        End Sub

        Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
            Try
                If txtID.Text = "0" Then
                    If MessageBox.Show("¿Seguro que desea insertar este registro?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                        FECHA = txtFechaNacimiento.Text
                        GuardarDatos("insert into datos_personales(nombre,telefono,fecha_nacimiento) values('" & txtNombre.Text & "','" & txtTelefono.Text & "',?)")

                        MessageBox.Show("Registro exitoso", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Llenar()
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
            Try
                If txtID.Text <> "0" Then
                    If MessageBox.Show("¿Seguro que desea actualizar este registro?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                        FECHA = txtFechaNacimiento.Text
                        GuardarDatos("update datos_personales set nombre='" & txtNombre.Text & "',telefono='" & txtTelefono.Text & "',fecha_nacimiento=? where id='" & txtID.Text & "'")

                        MessageBox.Show("Actualizacion exitosa", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Llenar()
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(Err.Description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
            If MessageBox.Show("¿Seguro que deseas eliminar este registro?", "Atencion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                GuardarDatos("delete from datos_personales where id='" & txtID.Text & "'")

                MessageBox.Show("Operacion realizada exitosamente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Llenar()
            End If
        End Sub

        Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
            LimpiaDatos()
        End Sub

        Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
            If DataGridView1.RowCount > 0 Then
                txtID.Text = DataGridView1.CurrentRow.Cells("id").Value.ToString
                txtNombre.Text = DataGridView1.CurrentRow.Cells("nombre").Value.ToString
                txtTelefono.Text = DataGridView1.CurrentRow.Cells("telefono").Value.ToString
                txtFechaNacimiento.Text = DataGridView1.CurrentRow.Cells("fecha_nacimiento").Value
            Else
                LimpiaDatos()
            End If
        End Sub

        Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
            Llenar()
        End Sub
    End Class
