using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MakeAwesome))]
public class MakeAwesomeEditor : Editor
{
    public MakeAwesome makeAwesome;

    public override void OnInspectorGUI()
    {
        //Get the target
        makeAwesome = (MakeAwesome)target;
        //Params
        //Register undo operations
        Undo.RecordObject(makeAwesome, "Undo");
        //Draws the MakeAwesome logo
        GUILayout.Label(Resources.Load("makeAwesome", typeof(Texture)) as Texture);
        GUILayout.Label("MakeAwesome(); Version: 0.3");
        GUILayout.Space(20);
        //Draws the object field for the sun
        makeAwesome.Sun = (GameObject)EditorGUILayout.ObjectField("Sun",makeAwesome.Sun, typeof(GameObject), true);
        GUILayout.Space(18);
        #region Settings
        GUILayout.Label("Global Multiplier");
        makeAwesome.settings.globalIntensity = EditorGUILayout.Slider(makeAwesome.settings.globalIntensity, 0.2f, 5);
        //Bloom
        if (makeAwesome.settings._Bloom = GUILayout.Toggle(makeAwesome.settings._Bloom, "Bloom"))
            makeAwesome.settings.bloomIntensity = EditorGUILayout.Slider(makeAwesome.settings.bloomIntensity, 0.1f, 1.5f);
        //Crease Shading
        if (makeAwesome.settings._CreaseShading = GUILayout.Toggle(makeAwesome.settings._CreaseShading, "Crease Shading"))
            makeAwesome.settings.creaseShadingIntensity = EditorGUILayout.Slider(makeAwesome.settings.creaseShadingIntensity, 0.1f, 0.7f);
        //Vignetting
        if(makeAwesome.settings._Vignette = GUILayout.Toggle(makeAwesome.settings._Vignette, "Vignette"))
            makeAwesome.settings.vignetting = EditorGUILayout.Slider(makeAwesome.settings.vignetting, 0.01f, 0.2f);
        //Sun Shafts
        if(makeAwesome.settings._SunShafts = GUILayout.Toggle(makeAwesome.settings._SunShafts, "Sun Shafts"))
            makeAwesome.settings.sunShaftIntensity = EditorGUILayout.Slider(makeAwesome.settings.sunShaftIntensity, 0.1f, 1f);
        //Antialising
        makeAwesome.settings._AntiAlising = GUILayout.Toggle(makeAwesome.settings._AntiAlising, "Antialising");
        #endregion
        #region functions
        //Applies the settings
        if (GUILayout.Button("MakeAwesome();")) 
            makeAwesome.Apply();
        //Restore default values
        if (GUILayout.Button("Restore Default Settings"))
            makeAwesome.RestoreDefault();
        //Disables all components
        if (GUILayout.Button("Disable all"))
            makeAwesome.DisableAll();
        makeAwesome.fileName_Save = EditorGUILayout.TextField("Name: ", makeAwesome.fileName_Save);
        if(GUILayout.Button("Save Settings"))
        {
            Debug.Log(makeAwesome.fileName_Save);
            makeAwesome.SaveSettings();
        }
        makeAwesome.selectedFile = EditorGUILayout.Popup("Open file: ", makeAwesome.selectedFile, makeAwesome.loadFiles);
        if(GUILayout.Button("Load Settings"))
        {
            makeAwesome.LoadSettings();
        }
        #endregion
    }
}
