Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = "מערכת לניהול צבעים"

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "אביטל עידן - tchyk52@gmail.com"

        Return View()
    End Function
End Class
