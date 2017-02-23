# Developer Documentation
## Installation
#### Pleace notice: this only runs on Windows
- Clone this repository
- Download edited Earth Texture with borders [here](https://uni-muenster.sciebo.de/index.php/s/GMBXBPg0HfgnuSe) (Source: [NASA Blue Marble](http://visibleearth.nasa.gov/view_cat.php?categoryID=1484))
- Download Earth DEM [here](http://naturalearth.springercarto.com/ne3_data/dem_large.zip) and extract .zip folder
- Download & stars only skybox [here](https://www.assetstore.unity3d.com/en/#!/content/53752) and add it to your project
- Move texture and DEM to projects Assets folder
- Add _stars only skybox_ to your [CameraRig] -> Camera (head) -> Camera (eye)
- Open project folder in Unity
- Add Texture to sphere (drag 'n drop)
    - Set DEM as normal map
- Run

## Add additional features
### Radial menu
#### Location specific
To add additional features to the radial menu, add an additional panel to the right controller radial menu in the Unity3D GUI and link your function there.

#### Overlay
To add an additional overlay to the left controller radial menu, make sure you modified our [Backend](https://github.com/Flugmango/GeoVR_Backend). Then add a case to the `addLegend` fucntion to add a legend to the left controller. Then add a additional panel to the radial menu of the left controller and set the `addOverlay("your Overlay type")` to the new panels onClick function.
