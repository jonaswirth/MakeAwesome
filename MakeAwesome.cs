/**************************
Author: Jonas Wirth
Date: 06.08.2016
Title: MakeAwesome(); 
Version: 0.2
**************************/
using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEditor;
using System;

[ExecuteInEditMode]
public class MakeAwesome : MonoBehaviour
{
    //References
    public GameObject Sun;
    //
    public bool _Bloom = true;
    public bool _CreaseShading = true;
    public bool _AntiAlising = true;
    public bool _Vignette = true;
    public bool _SunShafts = true;

    public float globalIntensity = 1;
    public float bloomIntensity = 0.15f;
    public float creaseShadingIntensity = 0.2f;
    public float vignetting = 0.08f;
    public float sunShaftIntensity = 0.5f;


    public Bloom bloom;
    public CreaseShading creaseShading;
    public Antialiasing antialising;
    public VignetteAndChromaticAberration vignette;
    public SunShafts sunShafts;

    private bool isSetup = false;

    public void Apply()
    {
        if (isSetup)
        {
            UpdateValues();
        }
        else
        {
            Setup();
        }
    }

    public void Setup()
    {
        //Get the Camera of the Parent object
        Camera camera = gameObject.GetComponent<Camera>();
        //Instantiates the Image Effects, only executed once 
        if (!camera.gameObject.GetComponent<Bloom>())
        {
            bloom = Undo.AddComponent<Bloom>(this.gameObject);
        }
        if (!camera.gameObject.GetComponent<CreaseShading>())
        {
            creaseShading = Undo.AddComponent<CreaseShading>(this.gameObject);
        }
        if (!camera.gameObject.GetComponent<VignetteAndChromaticAberration>())
        {
            vignette = Undo.AddComponent<VignetteAndChromaticAberration>(this.gameObject);
        }
        if (!camera.gameObject.GetComponent<SunShafts>())
        {
            sunShafts = Undo.AddComponent<SunShafts>(gameObject);
        }
        if (!camera.gameObject.GetComponent<Antialiasing>())
        {
            antialising = Undo.AddComponent<Antialiasing>(gameObject);
        }
        //Sets isSetup to true, so it wont be called again by the function Apply
        isSetup = true;
        UpdateValues();
    }
    public void UpdateValues()
    {
        if (Sun == null)
            Debug.Log("Please assign your Directional Light to MakeAwesome as 'Sun' for best results.");
        //Parameters
        if (bloom != null)
        {
            bloom.bloomIntensity = bloomIntensity * globalIntensity;
            bloom.enabled = _Bloom;
        }
        else
        {
            Setup();
        }
        if (creaseShading != null)
        {
            creaseShading.intensity = creaseShadingIntensity * globalIntensity;
            creaseShading.enabled = _CreaseShading;
        }
        else
        {
            Setup();
        }
        if (vignette != null)
        {
            vignette.intensity = vignetting;
            vignette.enabled = _Vignette;
        }
        else
        {
            Setup();
        }
        if (sunShafts != null)
        {
            sunShafts.sunShaftIntensity = sunShaftIntensity;
            sunShafts.enabled = _SunShafts;
            if (Sun != null)
            {
                sunShafts.sunColor = Sun.GetComponent<Light>().color;
                sunShafts.sunTransform = Sun.transform;
            }

        }
        else
        {
            Setup();
        }
        if (antialising != null)
        {
            antialising.enabled = _AntiAlising;
        }
        else
        {
            Setup();
        }
    }

    public void DisableAll()
    {
        bloom.enabled = false;
        creaseShading.enabled = false;
        antialising.enabled = false;
        vignette.enabled = false;
        sunShafts.enabled = false;
    }

    public void RestoreDefault()
    {
        globalIntensity = 1;
        bloomIntensity = 0.15f;
        creaseShadingIntensity = 0.2f;
        vignetting = 0.08f;
        sunShaftIntensity = 0.5f;

        _Bloom = true;
        _CreaseShading = true;
        _AntiAlising = true;
        _Vignette = true;
        _SunShafts = true;
        Apply();
    }
}
