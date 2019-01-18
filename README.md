# IdleBuster 
![Appveyor Build Status](https://ci.appveyor.com/api/projects/status/github/Macaba/IdleBuster?branch=master&svg=true)

IdleBuster is an application that will execute an action after the user input (mouse/keyboard) has been idle for a set duration.

## Build ##

* Open IdleBuster.sln in Visual Studio 2017
* Build IdleBuster

## Installation ##

After build, the only file required from the Bin directory is `IdleBuster.exe`. 

Optionally, include a `IdleBuster.json` configuration file alongside `IdleBuster.exe`. If one is not present on the first run, then a default file will be generated.

When the application is run, it'll run silently in the background. The entry in Task Manager is `IdleBuster.exe`.

IdleBuster can run at login for any user, the commands to install or uninstall are:

`IdleBuster.exe install`

`IdleBuster.exe uninstall`

Before installing it, place it in a stable location.

## Configuration ##

In IdleBuster.json:
```
{"Timeout":"00:10:00","Action":"Lock"}
```

Timeout parameter is HH:MM:SS. 

Allowable actions are:
* Lock
* Logoff
* Standby
* Hibernate
* Shutdown
* Restart

## Compatibility ##

IdleBuster is built against .NET 4.5.2.
