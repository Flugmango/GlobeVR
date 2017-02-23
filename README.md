# GlobeVR

#### Study project GeoVR @ Institute for Geoinformatics, WWU Münster, Germany
As part of the study project a GeoVR application namely GlobeVR for displaying the different kinds of data(weather, rainfall, Windspeed) on the Globe. [Unity 3D](https://unity3d.com/) was used in order to develop the application. 
## Demo

## Documentation
- [User Docs](user-documentation.md)
- [Developer Docs](developer-documentation.md)

## Bugs, Limitations and Future Work
__Known Bugs__
- The rotation of the globe has some issues. When rotating the globe by more than 180° on the Y-axis the rotation around the Z and X axis is inverted.
- Cloud image is static. If there is an API that is fast enough (and free-to-use) it would be possible to display near real time data.
- The population graph is not displayed properly

__Future Work__
For the future the following features could be implemented:
- Display a pin to the selected location when using the right controller
- Make the infoscreens draggable (using the VRTK) and make them individually deletable
- Display near real-time cloud imagery.
- Use higher quality overlay images.




## Dependencies
Make sure to install the following free Frameworks from the Unity Asset Store:

- [SteamVR](http://www.steamvr.com)
- VRTK by [theStoneFox](https://github.com/thestonefox/VRTK)

Tested on HTC Vive only.

### Icons
- Info icon designed by Chris Veigt from Flaticon
- Cloud, rain and temperature icon designed by Madebyoliver from Flaticon
- Barometric pressure icon designed by Freepik from Flaticon

## Contributors
- Boris Stöcker
- Felix Erdmann
- Milan Köster
- Nico Steffens
- Sruthi Ketineni
