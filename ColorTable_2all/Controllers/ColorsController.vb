Imports System.Web.Mvc
Imports System.Data.SqlClient

Public Class ColorsController
    Inherits Controller

    Public Function GetColors() As ActionResult
        Dim colors As New List(Of Color)

        Using conn As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=ColorsDB;Integrated Security=True;")
            conn.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Colors", conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                colors.Add(New Color() With {
                    .Id = reader("Id"),
                    .Name = reader("ColorName"),
                    .Price = reader("Price"),
                    .Order = reader("OrderIndex"),
                    .InStock = reader("InStock"),
                    .Code = reader("ColorCode")
                })
            End While
        End Using

        Return Json(colors, JsonRequestBehavior.AllowGet)
    End Function


    <HttpPost>
    Public Function AddColor(newColor As Color) As ActionResult
        Using conn As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=ColorsDB;Integrated Security=True;")
            conn.Open()
            Dim cmd As New SqlCommand("INSERT INTO Colors (ColorName, Price, [OrderIndex], InStock, ColorCode) VALUES (@Name, @Price, @Order, @InStock, @Code)", conn)
            cmd.Parameters.AddWithValue("@Name", newColor.Name)
            cmd.Parameters.AddWithValue("@Price", newColor.Price)
            cmd.Parameters.AddWithValue("@Order", newColor.Order)
            cmd.Parameters.AddWithValue("@InStock", newColor.InStock)
            cmd.Parameters.AddWithValue("@Code ", newColor.Code)

            cmd.ExecuteNonQuery()
        End Using

        Return New HttpStatusCodeResult(200)
    End Function


    Public Function GetColorById(id As Integer) As JsonResult
        Dim color As New Color()

        Using conn As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=ColorsDB;Integrated Security=True;")
            conn.Open()
            Dim cmd As New SqlCommand("SELECT * FROM Colors WHERE Id = @Id", conn)
            cmd.Parameters.AddWithValue("@Id", id)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                color.Id = reader("Id")
                color.Name = reader("ColorName")
                color.Price = reader("Price")
                color.Order = reader("OrderIndex")
                color.InStock = reader("InStock")
                color.Code = reader("ColorCode")
            End If
        End Using

        Return Json(color, JsonRequestBehavior.AllowGet)
    End Function

    <HttpPost>
    Public Function EditColor(updatedColor As Color) As ActionResult
        Try
            Using conn As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=ColorsDB;Integrated Security=True;")
                conn.Open()
                Dim cmd As New SqlCommand("UPDATE Colors SET ColorName = @Name, Price = @Price, [OrderIndex] = @Order, InStock = @InStock, ColorCode = @Code WHERE Id = @Id", conn)
                cmd.Parameters.Add(New SqlParameter("@Name", SqlDbType.NVarChar)).Value = updatedColor.Name
                cmd.Parameters.Add(New SqlParameter("@Price", SqlDbType.Decimal)).Value = updatedColor.Price
                cmd.Parameters.Add(New SqlParameter("@Order", SqlDbType.Int)).Value = updatedColor.Order
                cmd.Parameters.Add(New SqlParameter("@InStock", SqlDbType.Bit)).Value = updatedColor.InStock
                cmd.Parameters.Add(New SqlParameter("@Code", SqlDbType.NVarChar)).Value = updatedColor.Code
                cmd.Parameters.Add(New SqlParameter("@Id", SqlDbType.Int)).Value = updatedColor.Id
                cmd.ExecuteNonQuery()


            End Using
            Return Json(New With {.success = True, .message = "הצבע עודכן בהצלחה"})
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "אירעה שגיאה: " & ex.Message})
        End Try
    End Function

    <HttpPost>
    Public Function DeleteColor(id As Integer) As ActionResult
        Try
            Using conn As New SqlConnection("Server=(localdb)\MSSQLLocalDB;Database=ColorsDB;Integrated Security=True;")
                conn.Open()
                Dim cmd As New SqlCommand("DELETE FROM Colors WHERE Id = @Id", conn)
                cmd.Parameters.Add(New SqlParameter("@Id", SqlDbType.Int)).Value = id
                cmd.ExecuteNonQuery()
            End Using
            Return Json(New With {.success = True, .message = "הצבע נמחק בהצלחה"})
        Catch ex As Exception
            Return Json(New With {.success = False, .message = "אירעה שגיאה: " & ex.Message})
        End Try
    End Function


End Class
