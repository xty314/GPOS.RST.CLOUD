wmic process where name="webserver.EXE" get processid
webserver.EXE /port:2180 /path:%~dp0 /vpath:"/"
cmd.exe exit