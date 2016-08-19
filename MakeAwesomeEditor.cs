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
        //Register undo operations
        Undo.RecordObject(makeAwesome, "Undo");
        //Draws the MakeAwesome logo
        GUILayout.Label(Resources.Load("makeAwesome", typeof(Texture)) as Texture);
        GUILayout.Label("MakeAwesome(); Version: 0.2");
        GUILayout.Space(20);
        
        //Draws the object field for the sun
        makeAwesome.Sun = (GameObject)EditorGUILayout.ObjectField("Sun",makeAwesome.Sun, typeof(GameObject), true);
        GUILayout.Space(18);
        #region Settings
        GUILayout.Label("Global Multiplier");
        makeAwesome.globalIntensity = EditorGUILayout.Slider(makeAwesome.globalIntensity, 0.2f, 5);
        //Bloom
        if (makeAwesome._Bloom = GUILayout.Toggle(makeAwesome._Bloom, "Bloom"))
        {
            makeAwesome.bloomIntensity = EditorGUILayout.Slider(makeAwesome.bloomIntensity, 0.1f, 1.5f);
        }
        //Crease Shading
        if (makeAwesome._CreaseShading = GUILayout.Toggle(makeAwesome._CreaseShading, "Crease Shading"))
        {
            makeAwesome.creaseShadingIntensity = EditorGUILayout.Slider(makeAwesome.creaseShadingIntensity, 0.1f, 0.7f);
        }
        //Vignetting
        if(makeAwesome._Vignette = GUILayout.Toggle(makeAwesome._Vignette, "Vignette"))
        {
            makeAwesome.vignetting = EditorGUILayout.Slider(makeAwesome.vignetting, 0.01f, 0.2f);
        }
        //Sun Shafts
        if(makeAwesome._SunShafts = GUILayout.Toggle(makeAwesome._SunShafts, "Sun Shafts"))
        {
            makeAwesome.sunShaftIntensity = EditorGUILayout.Slider(makeAwesome.sunShaftIntensity, 0.1f, 1f);
        }
        //Antialising
        makeAwesome._AntiAlising = GUILayout.Toggle(makeAwesome._AntiAlising, "Antialising");
        #endregion
        #region functions
        //Applies the settings
        if (GUILayout.Button("MakeAwesome();"))
        {
            makeAwesome.Apply();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
        //Restore default values
        if (GUILayout.Button("Restore Default Settings"))
        {
            makeAwesome.RestoreDefault();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
        //Disables all components
        if (GUILayout.Button("Disable all"))
        {
            makeAwesome.DisableAll();
        }
        #endregion
    }
}
