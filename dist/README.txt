# Valheim No Wear And Tear

A simple mod that allows you to change following behaviours:

* Make structural stability always 100% (this might be helpful if you have issues with performance or disappearing structures)
* Disable structure wear-down (take damage from being exposed to the elements or in water)
* Alternative: Disable only visualization of structure wear-down (the health still goes down, but it is not ugly)

## Installation

1. Install [Valheim ](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
2. Copy `valheim_no_wear_and_tear.dll` (in the ZIP file) into `Valheim/BepInEx/plugins`
3. Copy `de.mrnotsoevil.valheim.no-wear-and-tear.cfg` (in the ZIP file and also down below) into `Valheim/BepInEx/config` (Optional; this file is created on starting the game)

By default only the wear-down visualization is turned off (no alteration of the gameplay). 
Open the config file in Notepad or any other editor and set entries you want to remove from the game to `false`. 

## Example config

```
## Settings file was created by plugin Valheim No Wear And Tear v1.0.0.0
## Plugin GUID: de.mrnotsoevil.valheim.no-wear-and-tear

[General]

## If enabled, exposed structures are damaged over time.
# Setting type: Boolean
# Default value: true
EnableWear = true

## If enabled, exposed structures will be displayed as 'worn' over time.
# Setting type: Boolean
# Default value: false
EnableWearVisualization = false

## If enabled, structures are checked for their integrity.
# Setting type: Boolean
# Default value: true
EnableStructuralIntegrity = true
```

## Source code

The plugin source is available here: https://github.com/rumangerst/valheim-no-wear-and-tear