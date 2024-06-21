Imports Microsoft.VisualBasic

Public Class clsMailProperty
    Dim _MailPropertyID, _MailTypeID, _MailSubject, _MailBody, _MailFrom, _MailTo, _MailCC, _MailBCC, _EntryBy, _MailHeader, _MailFooter, _SMTPServer, _SMTPPort As String
    Dim _IsAutomated, _IsActive As Boolean

    Public Property MailPropertyID() As String
        Get
            Return _MailPropertyID
        End Get
        Set(ByVal value As String)
            _MailPropertyID = value
        End Set
    End Property

    Public Property MailTypeID() As String
        Get
            Return _MailTypeID
        End Get
        Set(ByVal value As String)
            _MailTypeID = value
        End Set
    End Property

    Public Property MailSubject() As String
        Get
            Return _MailSubject
        End Get
        Set(ByVal value As String)
            _MailSubject = value
        End Set
    End Property

    Public Property MailBody() As String
        Get
            Return _MailBody
        End Get
        Set(ByVal value As String)
            _MailBody = value
        End Set
    End Property

    Public Property MailFrom() As String
        Get
            Return _MailFrom
        End Get
        Set(ByVal value As String)
            _MailFrom = value
        End Set
    End Property

    Public Property MailTo() As String
        Get
            Return _MailTo
        End Get
        Set(ByVal value As String)
            _MailTo = value
        End Set
    End Property

    Public Property MailCC() As String
        Get
            Return _MailCC
        End Get
        Set(ByVal value As String)
            _MailCC = value
        End Set
    End Property

    Public Property MailBCC() As String
        Get
            Return _MailBCC
        End Get
        Set(ByVal value As String)
            _MailBCC = value
        End Set
    End Property

    Public Property EntryBy() As String
        Get
            Return _EntryBy
        End Get
        Set(ByVal value As String)
            _EntryBy = value
        End Set
    End Property

    Public Property MailHeader() As String
        Get
            Return _MailHeader
        End Get
        Set(ByVal value As String)
            _MailHeader = value
        End Set
    End Property

    Public Property SMTPServer() As String
        Get
            Return _SMTPServer
        End Get
        Set(ByVal value As String)
            _SMTPServer = value
        End Set
    End Property

    Public Property SMTPPort() As String
        Get
            Return _SMTPPort
        End Get
        Set(ByVal value As String)
            _SMTPPort = value
        End Set
    End Property

    Public Property MailFooter() As String
        Get
            Return _MailFooter
        End Get
        Set(ByVal value As String)
            _MailFooter = value
        End Set
    End Property

    Public Property IsAutomated() As Boolean
        Get
            Return _IsAutomated
        End Get
        Set(ByVal value As Boolean)
            _IsAutomated = value
        End Set
    End Property

    Public Property IsActive() As Boolean
        Get
            Return _IsActive
        End Get
        Set(ByVal value As Boolean)
            _IsActive = value
        End Set
    End Property
End Class
