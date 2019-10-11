# IevaScript
IevaScript is a script with C# syntax to write small scripts and combine them.

10.10.2019 - First implementation pushed

Calls of other scripts will look like this: <br> #call_NameOfCalledScript : ParameterToPassToScript = VariableToStoreResultFromScript ;

See IevaScript_Example.csproj for examples.

The test project includes 3 test scripts: Script_1.isc, Script_2.isc and Script_3.isc

Script_1 calls Script_2, Script_2 returns a string. At last Script_1 calls Script_3 with the string, returned from Script_2 and Writes it on the console.

Documentation is coming soon...
