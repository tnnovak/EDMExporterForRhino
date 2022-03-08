# EDMExporterForRhino

## DCS World EDM File Exporter for Rhino 7

This is a **very experimental** plugin to export **EDM files** for **DCS World** from Rhino.

The plugin does not export the EDM file itself, in fact all it does is to export the 3D model to a **COLLADA** file and then it launches Blender in the background running a script that will load the **COLLADA** file and export to EDM using the [BLENDER EDM EXPORTER](https://github.com/tobi-be/BlenderEdmExporter) for Blender, all in background.

Its an option for those who like to create models in Rhino and have little experience in Blender.

Since it uses Blender you will need:

1. [Rhino 7](https://www.rhino3d.com/download/)
2. [Blender](https://www.blender.org/download/) for Windows
3. [BLENDER EDM EXPORTER](https://github.com/tobi-be/BlenderEdmExporter) plugin for Blender

The first time the EDMExporterForRhino plugin runs it asks for the blender.exe file location and for a python script included in the plugin ZIP file

## Instruction to install the EDMExporterForRhino in Rhino:

1. Download ZIP from the last release [here]() and unzip
2. Open Rhino
3. Go to menu --> Tools then Options
4. On the left pane choose Plugins 
5. Then on the Right pane click the "Install..." button
6. Go to the unziped file folder and select the EDMExporterForRhino.rhp and open

Now the pluign is installed in Rhino. You can call it in the command line by typing:

**EDMExporterForRhino**

## Instructions for using the plugin:

1. Load or create the model you want to export
2. In the Rhino Layers pane change the name of the layer to the texture file you want to export in the EDM file for that part of the model
3. In the "material" item for the layer click, select "Custom" material and in the Color option click the button to load the texture file (with same name of the step 1)
4. Repeat this for all the textures you want to have in the model, assigning each texture file for each layer 
5. Assign to the different parts and object od the model the layer correspondent to the texture for tat part/object
6. After all the parts / objects have a texture assigned type **EDMExporterForRhino** in the command line
7. Select all the parts and object you want to export
8. After the selection a Save Dialog window will appear. Select location and EDM filename to export
9. After that the EDM file will exported. A popup window will display the Blender EDM output so you can check if the export process was successful.

### UV maps are not working in this version. Only simple texturing.













