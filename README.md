# EDMExporterForRhino

## DCS World EDM File Exporter for Rhino 7

Author: **Toni Novak** - toninovak@gmail.com
Site: [https://tnnvk.com/](https://tnnvk.com/)

This is a **very experimental** plugin to export **EDM files** for **DCS World** from Rhino.

The plugin does not export the EDM file itself, in fact all it does is to export the 3D model to a **COLLADA** file and then it launches Blender in the background running a script that will load the **COLLADA** file and export to EDM using the [BLENDER EDM EXPORTER](https://github.com/tobi-be/BlenderEdmExporter) for Blender, all in background.

Its an option for those who want to create simpler DCS 3D models in Rhino and have little experience with Blender.

Since it uses Blender you will need pre-installed:

1. [Rhino 7](https://www.rhino3d.com/download/)
2. [Blender](https://www.blender.org/download/) for Windows
3. [BLENDER EDM EXPORTER](https://github.com/tobi-be/BlenderEdmExporter) plugin installed in Blender (instruction on how to install it can be found in the plugin page)

The first time the **EDMExporterForRhino** plugin runs it asks for the blender.exe file location and for a python script included in the plugin ZIP file

## Instruction to install the EDMExporterForRhino in Rhino:

1. Download ZIP from the last release [here]() and unzip
2. Open Rhino
3. Go to menu --> Tools then Options
4. On the left pane choose Plugins 
5. Then on the Right pane click the "Install..." button
6. Go to the unziped file folder select the **EDMExporterForRhino.rhp** and load it

Now the pluign is installed in Rhino. You can call it in the command line by typing:

**EDMExporterForRhino**

## Instructions for using the plugin:

The first time you run the plugin it will ask for:

1. The **blender.exe** file location (usually at **C:\Program Files\Blender Foundation\Blender XXX**) or at any other folder you had installed blender
2. Then it will ask for the **EDMExporterForRhino.py** file. It is inside the EDMExporterForRhino unziped folder you had downloaded

After the above is complete it will not asked for those anymore. Then you can use the plugin like this:

1. Load or create the model you want to export
2. In the Rhino Layers pane change the name of the layer to the texture file name you want to export in the EDM file for that part of the model
3. In the "material" item for the layer click, select "Custom" material and in the Color option click the button to load the texture file (the same texture file of the step 1)
4. Repeat this for all the textures you want to have in the model, assigning each texture file for each layer 
5. Assign to the each different part and objects of the model the layer correspondent to the texture for that part/object
6. After all the parts/objects have a texture assigned type **EDMExporterForRhino** in the command line
7. Select all the model you want to export
8. After the selection is done a Save Dialog window will appear. Select location and EDM filename to export
9. The EDM file will be generated. Blender will run hidden in the background, export the file and then quit. A popup window will display the Blender output so you can check if the export process was successful.

### UV maps are not working in this version. Only simple texturing.













