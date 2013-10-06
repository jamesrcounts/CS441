@ECHO OFF
set TargetPath=%1
if exist "%ProgramFiles%\Microsoft Fxcop 10.0\FxCopCmd.exe" "%ProgramFiles%\Microsoft Fxcop 10.0\FxCopCmd.exe" /file:%TargetPath% /ignoregeneratedcode /console
REM if exist "%ProgramFiles%\Microsoft Fxcop 10.0\FxCopCmd.exe" "%ProgramFiles%\Microsoft Fxcop 10.0\FxCopCmd.exe" /project:%TargetPath% /ignoregeneratedcode /console