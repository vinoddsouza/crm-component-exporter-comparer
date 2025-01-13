@echo off

set ILMergePath=..\packages\ILMerge.3.0.41\tools\net452\ILMerge.exe

IF "%1"=="" (
    echo Error: No argument provided.
    echo Usage: %~n0 "[Debug|Release]"
    exit /b 1
)

if "%1"=="Debug" (
    set BuildMode=Debug
)
if "%1"=="Release" (
    set BuildMode=Release
)

if "%BuildMode%"=="" (
    echo Error: Invalid argument "%1".
    echo Usage: %~n0 "[Debug|Release]"
    exit /b 1
)

set BuildDir=bin\%BuildMode%
set OutDir=%BuildDir%\Merged
set OutDll=%OutDir%\RioCanada.Crm.ComponentComparerExporter.dll

IF NOT EXIST %OutDir% (
    mkdir %OutDir%
)

"%ILMergePath%" ^
/targetplatform:4.0,"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2" ^
/out:"%OutDll%" "%BuildDir%\RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.dll" "%BuildDir%\RioCanada.Crm.ComponentExportComparer.Core.dll" "%BuildDir%\Menees.Windows.Forms.dll" "%BuildDir%\Menees.Diffs.dll" "%BuildDir%\Menees.Common.dll" "%BuildDir%\Menees.Diffs.Windows.Forms.dll"

echo Merged DLL: %OutDll%
exit /b 0
