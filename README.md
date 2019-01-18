# IdleBuster

IdleBuster is a Windows Service that will execute an action after the user input (mouse/keyboard) has been idle for a set duration.

## Build ##

* Open IdleBuster.sln in Visual Studio 2017
* Build IdleBuster

## Installation ##

After build, the only file required from the Bin directory is IdleBuster.exe. 

Optionally, include a IdleBuster.json configuration file. If one is not present on the first run, then a default file will be generated.

### Command line reference ###

On the command line:

`IdleBuster.exe run` Runs the service from the command line (default)

`IdleBuster.exe install` Installs the service

`IdleBuster.exe start` Starts the service if it is not already running

`IdleBuster.exe stop` Stops the service if it is running

`IdleBuster.exe uninstall` Uninstalls the service

For further detail, see the [Topshelf Command-Line Reference](https://topshelf.readthedocs.io/en/latest/overview/commandline.html).

## Configuration ##

In IdleBuster.json:
```
{"Timeout":"00:00:10","Action":"Lock"}
```

Timeout parameter is HH:MM:SS. 

Allowable actions are:
* Lock
* Logoff
* Standby
* Hibernate
* Shutdown
* Restart
