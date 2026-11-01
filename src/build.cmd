@echo off
rem Сборка XYZ Запись встроенным компилятором .NET Framework 4.x (Visual Studio не нужна).
chcp 65001 >nul
cd /d "%~dp0"
set CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
set RES=
for %%f in (res\*.png) do call set RES=%%RES%% "/resource:res\%%~nxf,%%~nxf"
"%CSC%" /nologo /target:winexe /optimize+ /codepage:65001 /win32icon:xyz_rec_button.ico "/out:XYZ Запись.exe" /r:System.Windows.Forms.dll /r:System.Drawing.dll %RES% /resource:xyz_rec_button.ico,app.ico RecorderDevice.cs
if errorlevel 1 (echo Ошибка сборки & pause & exit /b 1)
echo Готово: XYZ Запись.exe
