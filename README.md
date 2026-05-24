# sackMAN

sackMAN is a program that interfaces with the LittleBigPlanet PS3 games, primarily targetted towards speedrunners.

For more information about game patches, Lua automations, and other details, please refer to the [RaCMAN Wiki](https://github.com/MichaelRelaxen/racman/wiki).

## Supported Games
- LittleBigPlanet - (Game Versions v1.21 and v1.30/Latest)
- LittleBigPlanet 2 - (Game Versions v1.00 and v1.33/Latest)

## Supported Features
- Autosplitter/Load Remover with LiveSplit
- Memory Viewer
- Patch Loader

## Setup
To use sackMAN, follow these steps:
### PS3 setup
First, make sure you have `webMAN MOD` installed on a jailbroken PS3 using either a console with custom firmware or a HEN-enabled console.
It's important that you have the full version of webMAN MOD, which can be done by holding L1 while running the installer.

Make sure your PC and PS3 are connected to the same network.
Then, to connect sackMAN to your PS3, you need to get the IP address from your PS3. This can be done by going into Settings > System Settings > System Information on your console, or by pressing the SELECT+START button combination with webMAN MOD installed.

Input the IP address and insert it into the sackMAN.exe application on your PC.
Then all you need to do is run a game, and click Attach!

If it prompts you to allow it through your firewall, select both Private and Public options and it should be fine.

### RPCS3 setup
Just run `sackMAN.exe`, then while your game is open in RPCS3, click the `RPCS3` button. 

Some features such as patch loading, or other features that may edit the game's code are unfortunately not supported on RPCS3.

## Acknowledgements
Special thanks to all those who have worked on the original [RaCMAN project](https://github.com/MichaelRelaxen/racman) and the [Ratchetron API](https://github.com/bordplate/Ratchetron). This would not be a thing without the work of all that.
