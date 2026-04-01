@echo off
set script_dir=%~dp0
set "script_dir=%script_dir:\=/%"
set str=%1
if "%str:~0,1%"=="@" (
  if "%str%"=="@run" (
    bash.exe -c "%script_dir%/.r.inject-1080p.sh %2 %3 %4 %5 %6 %7 %8 %9
  ) else if "%str%"=="@exe" (
    if not exist "%script_dir%/inject-1080p.exe" (
      wingen %script_dir%/inject-1080p.main.cs
      bash.exe -c "%script_dir%/.r.inject-1080p.sh @merge -f"
    )
    "%script_dir%/inject-1080p.exe" %2 %3 %4 %5 %6 %7 %8 %9
  ) else if "%str%"=="@bin" (
    if not exist "%script_dir%/inject-1080p.exe" (
      wingen %script_dir%/inject-1080p.main.cs
      bash.exe -c "%script_dir%/.r.inject-1080p.sh @pack -f"
    )
    "%script_dir%/inject-1080p.exe" %2 %3 %4 %5 %6 %7 %8 %9
  ) else (
    bash.exe -c "%script_dir%/.r.inject-1080p.sh %*"
  )
) else (
  cscs -nuget:restore "%script_dir%/inject-1080p.main.cs">NUL 2>&1
  cscs -l:0 "%script_dir%/inject-1080p.main.cs" %*
)
