@echo off
set script_dir=%~dp0
set "script_dir=%script_dir:\=/%"
set str=%1
if "%str:~0,1%"=="@" (
  if "%str%"=="@run" (
    bash.exe -c "%script_dir%/.r.inject-m4a.sh %2 %3 %4 %5 %6 %7 %8 %9
  ) else if "%str%"=="@exe" (
    if not exist "%script_dir%/inject-m4a.exe" (
      wingen %script_dir%/inject-m4a.main.cs
      bash.exe -c "%script_dir%/.r.inject-m4a.sh @merge -f"
    )
    "%script_dir%/inject-m4a.exe" %2 %3 %4 %5 %6 %7 %8 %9
  ) else if "%str%"=="@bin" (
    if not exist "%script_dir%/inject-m4a.exe" (
      wingen %script_dir%/inject-m4a.main.cs
      bash.exe -c "%script_dir%/.r.inject-m4a.sh @pack -f"
    )
    "%script_dir%/inject-m4a.exe" %2 %3 %4 %5 %6 %7 %8 %9
  ) else (
    bash.exe -c "%script_dir%/.r.inject-m4a.sh %*"
  )
) else (
  cscs -nuget:restore "%script_dir%/inject-m4a.main.cs">NUL 2>&1
  cscs -l:0 "%script_dir%/inject-m4a.main.cs" %*
)
