Dim WshShell
Set WshShell = WScript.CreateObject("WScript.Shell")
Dim ARGS
set ARGS=WScript.Arguments

WshShell.AppActivate(ARGS.Item(0))
WScript.Quit(0)
