# GlobeVR

#### Study project GeoVR @ Institute for Geoinformatics, WWU Münster, Germany

## Installation
- Clone this repository
- Download edited Earth Texture with borders [here](https://uni-muenster.sciebo.de/index.php/s/GMBXBPg0HfgnuSe) (Source: [NASA Blue Marble](http://visibleearth.nasa.gov/view_cat.php?categoryID=1484))
- Download Earth DEM [here](http://naturalearth.springercarto.com/ne3_data/dem_large.zip) and extract .zip folder
- Download & stars only skybox [here](https://www.assetstore.unity3d.com/en/#!/content/53752) and add it to your project
- Move texture and DEM to projects Assets folder
- Add stars only skybox to your [CameraRig] -> Camera (head) -> Camera (eye)
- Open project folder in Unity
- Add Texture to sphere (drag 'n drop)
    - Set DEM as normal map
- Run

## Dependencies
Make sure to install the following free Frameworks from the Unity Asset Store:

- SteamVR
- VRTK by [theStoneFox](https://github.com/thestonefox/VRTK)

Tested on HTC Vive only.
