using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System;

[Serializable]
public class MakeAwesome_SettingsModel {

    public bool _Bloom;
    public bool _CreaseShading;
    public bool _AntiAlising;
    public bool _Vignette;
    public bool _SunShafts;

    public float globalIntensity;
    public float bloomIntensity;
    public float creaseShadingIntensity;
    public float vignetting;
    public float sunShaftIntensity;

}
