﻿Imports DevExpress.Xpo
Imports System

Namespace Northwind

    <Persistent("Customers")> _
    Public Class Customer
        Inherits XPLiteObject

        <Key> _
        Public CustomerID As String
        Public CompanyName As String
        Public ContactTitle As String

        <Association("CustomerOrders", GetType(Order))> _
        Public ReadOnly Property Orders() As XPCollection
            Get
                Return GetCollection("Orders")
            End Get
        End Property
    End Class

    <Persistent("Orders")> _
    Public Class Order
        Inherits XPLiteObject

        <Key> _
        Public OrderID As Integer
        Public ShippedDate As Date

        <Persistent("CustomerID"), Association("CustomerOrders")> _
        Public Customer As Customer

        <Persistent("EmployeeID"), Association("EmployeeOrders")> _
        Public Employee As Employee

        Public ShipName As String
    End Class

    <Persistent("Employees")> _
    Public Class Employee
        Inherits XPLiteObject

        <Key> _
        Public EmployeeID As Integer
        Public FirstName As String
        Public LastName As String

        <Association("EmployeeOrders", GetType(Order))> _
        Public ReadOnly Property Orders() As XPCollection
            Get
                Return GetCollection("Orders")
            End Get
        End Property

        <PersistentAlias("[<Customer>][[<Order>][Customer = ^.This && Employee = ^.^.This]].Count()")> _
        Public ReadOnly Property DistinctCustomerCount() As Integer
            Get
                Return Convert.ToInt32(EvaluateAlias("DistinctCustomerCount"))
            End Get
        End Property
    End Class
End Namespace
