using Rhino;
using Rhino.Commands;
using Rhino.UI;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EDMExporterForRhino
{
    public class EDMExporterForRhinoCommand : Command
    {
        public EDMExporterForRhinoCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static EDMExporterForRhinoCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "EDMExporterForRhino";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {

            string blendeEXEFilePath = "";

            string edmExporterForRhinoPythonFilePath = "";            

            string colladaFilename = "";            

            string baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            string confFile = baseDirectory + "EDMExporterForRhino.conf";

            // *****************************************************
            // Read CONF file
            // *****************************************************

            if (File.Exists(confFile)) {


                string fileContents = File.ReadAllText(confFile);

                string[] lines = fileContents.Split(';');

                edmExporterForRhinoPythonFilePath = lines[0];                
                blendeEXEFilePath = lines[1];


            } else
            {

                // Find EDM Exporter for Rhino Python script                

                Dialogs.ShowMessage("Installation steps. You'll need to do this just once. \r\n\r\n 1. Please locate the EDMExporterForRhino.py file script \r\n\r\n It is at the extracted EDMExporterForRhino ZIP file", "EDM Exporter For Rhino");

                using (System.Windows.Forms.OpenFileDialog FindEDMForRhinoPythonFile = new System.Windows.Forms.OpenFileDialog())
                {
                    FindEDMForRhinoPythonFile.InitialDirectory = "c:\\";
                    FindEDMForRhinoPythonFile.Filter = "EDMExporterForRhino.py | EDMExporterForRhino.py";

                    if (FindEDMForRhinoPythonFile.ShowDialog() == DialogResult.OK)
                    {

                        if (FindEDMForRhinoPythonFile.FileName != "")
                        {
                            edmExporterForRhinoPythonFilePath = FindEDMForRhinoPythonFile.FileName;

                        }

                    }
                }

                // Find blender

                Dialogs.ShowMessage("3. Please locate blender.exe", "EDM Exporter For Rhino");

                using (System.Windows.Forms.OpenFileDialog FindBlenderFile = new System.Windows.Forms.OpenFileDialog())
                {
                    FindBlenderFile.InitialDirectory = "c:\\";
                    FindBlenderFile.Filter = "blender.exe | blender.exe";

                    if (FindBlenderFile.ShowDialog() == DialogResult.OK)
                    {

                        if (FindBlenderFile.FileName != "")
                        {
                            blendeEXEFilePath = FindBlenderFile.FileName;

                        }

                    }
                }

                Dialogs.ShowMessage("Installation finished", "EDM Exporter For Rhino");

                // Write conf file

                using (StreamWriter sw = File.CreateText(confFile))
                {
                    sw.Write(edmExporterForRhinoPythonFilePath + ";");
                    // sw.Write(edmPlguinforBlenderPythonFilePath + ";");
                    sw.Write(blendeEXEFilePath);
                }



            }

            // *****************************************************
            // Select objects for Export
            // *****************************************************


            Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
            go.SetCommandPrompt("Select objects to export");
            go.SubObjectSelect = false;
            go.GroupSelect = true;
            go.GetMultiple(1, 0);

            // *****************************************************
            // Export objects to COLLADA
            // *****************************************************

            int oc = 0;
            foreach (var obj in go.Objects())
            {

                var rhino_object = obj.Object();

                var brep = obj.Brep();
                var mesh = obj.Mesh();

                if (brep!=null)
                {
                    brep.SetUserString("name", "Object" + oc.ToString());
                }

                if (mesh != null)
                {
                    mesh.SetUserString("name", "Object" + oc.ToString());
                }


                rhino_object.Attributes.Name = "Object" + oc.ToString();
                rhino_object.Attributes.Url = "Object" + oc.ToString();

                rhino_object.Attributes.SetUserString("NameObj", "Object" + oc.ToString());
                
                oc++;

            }

            RhinoApp.WriteLine("Number of objects to export = " + oc.ToString());

            System.Windows.Forms.SaveFileDialog saveEDMFile = new System.Windows.Forms.SaveFileDialog();

            saveEDMFile.Filter = "EDM|*.edm";
            saveEDMFile.Title = "EDM File";            

            if (saveEDMFile.ShowDialog() == DialogResult.OK)
            {                

                // Export select object to COLLADA
                colladaFilename = saveEDMFile.FileName.Replace(".edm", ".dae");

                try
                {

                    doc.ExportSelected(colladaFilename);

                    try
                    {
                        // Delete textures folder, we dont need it 
                        Directory.Delete(colladaFilename.Replace(".dae", ""), true);
                    }catch(Exception ex)
                    {

                    }

                }
                catch(Exception ex)
                {
                    RhinoApp.WriteLine("Error exporting file = " + ex.Message);
                }

            }

            // *****************************************************
            // Modify pyhton script, insert filename
            // *****************************************************

            string pythonContents = "";

            if  (!File.Exists(edmExporterForRhinoPythonFilePath + "_bak"))
            {
                // Read original and save backup
                pythonContents = File.ReadAllText(edmExporterForRhinoPythonFilePath);
                File.WriteAllText(edmExporterForRhinoPythonFilePath + "_bak", pythonContents);
            }

            // Get from original backup always
            if (File.Exists(edmExporterForRhinoPythonFilePath + "_bak"))
            {
                pythonContents = File.ReadAllText(edmExporterForRhinoPythonFilePath + "_bak");                
            }            

            // Set filename inside script
            pythonContents = pythonContents.Replace("[#FILENAME#]", colladaFilename.Replace(".dae","").Replace("\\","\\\\"));
            
            File.WriteAllText(edmExporterForRhinoPythonFilePath,pythonContents);


            // *****************************************************
            // Run blender executing "EDMExporterForRhino.py"
            // to export COLLADA to EDM
            // *****************************************************

            // blender --background --python myscript.py

            string output = "";

            RhinoApp.WriteLine("Waiting file to be exported...");

            using (System.Diagnostics.Process pProcess = new System.Diagnostics.Process())
            {
                pProcess.StartInfo.FileName = blendeEXEFilePath;
                pProcess.StartInfo.Arguments = " --background --python \"" + edmExporterForRhinoPythonFilePath; // + "\" \"" + colladaFilename.Replace(@"\",@"\\") + "\"";

                // Dialogs.ShowMessage(blendeEXEFilePath + pProcess.StartInfo.Arguments, "Blender command");

                pProcess.StartInfo.UseShellExecute = false;
                pProcess.StartInfo.RedirectStandardOutput = true;
                pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                pProcess.StartInfo.CreateNoWindow = true; //not diplay a windows
                pProcess.Start();

                output = pProcess.StandardOutput.ReadToEnd(); //The output result

                // Dialogs.ShowMessage(output, "Blender Result");

                pProcess.WaitForExit();
            }

            RhinoApp.WriteLine("File exported.");

            Form1 frmOutput = new Form1();
            frmOutput.Show();
            frmOutput.InsertText(output);

            while(frmOutput.waitClosing)
            {
                Application.DoEvents();
            }

            return Result.Success;
        }
    }
}
