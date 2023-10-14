# CK Hud
This is a mod for the game Core Keeper that displays various stats in game. You can find the mod.io page [here](https://mod.io/g/corekeeper/m/ckhud)

# Pull Requests
If you would like to make a pull request here are some things that you'll want to know about how the code is set up.

- Each text "piece" is called a hud component
    - These components are classes that derive from the base `HudComponent` class and implement the `GetString()` function
    - To have your component be recognized by the mod you will also need to add it to the `HudComponent`'s `Parse` method
- To add to the config system you will want to modify the `HudManager`'s LoadConfig function
    - The `ConfigSystem` class already contains sethods for loading certain data types but if one does is not currently supported feel free to add it
 
# Roadmap/TODO
- In-game config editing
- Better system for instantiating the text
- Fix debug system for finding game objects
- Config for if you want to count mining and explosive damage in DPS
- An outline and/or background for the text
- Display player stats like attack speed, crit chance, etc.
- APS (Attacks Per Second) meter
