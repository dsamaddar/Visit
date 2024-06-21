Imports Microsoft.VisualBasic

Public Class clsContact

    Dim _VisitingOfficialID, _ContactPerson, _Relation, _ContactNo, _VisitingOfficial, _VisitingDate As String

    Public Property VisitingOfficialID() As String
        Get
            Return _VisitingOfficialID
        End Get
        Set(ByVal value As String)
            _VisitingOfficialID = value
        End Set
    End Property

    Public Property ContactPerson() As String
        Get
            Return _ContactPerson
        End Get
        Set(ByVal value As String)
            _ContactPerson = value
        End Set
    End Property

    Public Property Relation() As String
        Get
            Return _Relation
        End Get
        Set(ByVal value As String)
            _Relation = value
        End Set
    End Property

    Public Property ContactNo() As String
        Get
            Return _ContactNo
        End Get
        Set(ByVal value As String)
            _ContactNo = value
        End Set
    End Property

    Public Property VisitingOfficial() As String
        Get
            Return _VisitingOfficial
        End Get
        Set(ByVal value As String)
            _VisitingOfficial = value
        End Set
    End Property

    Public Property VisitingDate() As String
        Get
            Return _VisitingDate
        End Get
        Set(ByVal value As String)
            _VisitingDate = value
        End Set
    End Property

End Class
