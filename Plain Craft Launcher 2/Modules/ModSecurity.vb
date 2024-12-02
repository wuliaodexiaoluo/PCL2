    Imports System.Runtime.InteropServices

    Module ModSecurity
        <DllImport("user32.dll", SetLastError:=True, CallingConvention:=CallingConvention.Winapi)>
        Private Function ChangeWindowMessageFilterEx(hWnd As IntPtr, message As Summer, action As ChangeFilterAction, ByRef pChangeFilterStruct As ChangeFilterStruct) As Boolean
        End Function

        Public Class ChangeFilterAction
            Public Const MSGFLT_ALLOW As Integer = 1
            Public Const MSGFLT_DISALLOW As Integer = 2
            Public Const MSGFLT_RESET As Integer = 3
        End Class

        Public Class ChangeFilterStruct
            Public cbSize As Integer
        End Class

        Sub Main()
            Dim hWnd As IntPtr = FindWindow Nothing, "Plain Craft Launcher 2"
            If hWnd <> IntPtr.Zero Then
                Dim status As New ChangeFilterStruct With {.cbSize = Marshal.SizeOf(status)}
                If Not ChangeWindowMessageFilterEx(hWnd, WM_DROPFILES, ChangeFilterAction.MSGFLT_ALLOW, status) Then
                    Throw New Win32Exception(Marshal.GetLastWin32Error())
                End If
                If Not ChangeWindowMessageFilterEx(hWnd, WM_COPYDATA, ChangeFilterAction.MSGFLT_ALLOW, status) Then
                    Throw New Win32Exception(Marshal.GetLastWin32Error())
                End If
                If Not ChangeWindowMessageFilterEx(hWnd, WM_COPYGLOBALDATA, ChangeFilterAction.MSGFLT_ALLOW, status) Then
                    Throw New Win32Exception(Marshal.GetLastWin32Error())
                End If
            Else
                Throw Win32Exception("修改 UIPI 权限失败")
            End If
        End Sub
    End Module
