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
- Code refactoring
    - Create a few generic scripts for console logging, config, UI that can be shared between this mod and Map Extras (this might be able to be it's own library mod if the dependency system is working)
    - Add logging wherever possible to help with finding errors
    - Make some classes (like HudLine) derive from MonoBehaviour
    - Find container objects using name instead of child id and fix the debug system for this process
    - Rework HudComponent class to cache values when possible
    - Change the way that the hud is disabled to be based on `GameObject.SetActive(false)` instead of `PugText.Render("")`
    - Move config logic from HudManager to its own class
    - Make component loading more lenient to `... ;; ...`s (blank lines)
    - Update to work with CoreLib
- Add spawning cell position hud component
- Add second hud location
- Split hud between togglable and non-toggable hud
- Replace compact mode with customizable text for each component
- In-game config editing
- Custom text color for each component
- Config for some of the components
- An outline and/or background for the text
- Display player stats like attack speed, crit chance, etc.
- APS (Attacks Per Second) meter
