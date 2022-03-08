# EDMExporterForRhino
DCS World EDM File Exporter for Rhino 7

This is a **very experimental** plugin to export **EDM files** for **DCS World**.

The plugin does not export the EDM file itself, in fact all it does is to export the 3D model to a **COLLADA** file and then it launches Blender in the background running a script that will load the **COLLADA** file and export to EDM using the [BLENDER EDM EXPORTER](https://github.com/tobi-be/BlenderEdmExporter) for Blender, all in background.

Its an option for those who like to create models in Rhino and have little experience in Blender.

Since it uses Blender you will need:

1. [Rhino 7](https://www.rhino3d.com/download/)
2. [Blender](https://www.blender.org/download/) for Windows
3. [BLENDER EDM EXPORTER](https://github.com/tobi-be/BlenderEdmExporter) plugin for Blender

The first time the EDMExporterForRhino plugin runs it asks for the blender.exe file location and for a python script included in the plugin ZIP file

Instruction to install the EDMExporterForRhino in Rhino:


