/**************************
Author: Jonas Wirth
Date: 06.08.2016
Title: MakeAwesome(); 
Version: 0.25
**************************/
using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEditor;
using System;
using Assets.MakeAwesome.Src;

[ExecuteInEditMode]
public class MakeAwesome : MonoBehaviour
{
    //Settings:
    public MakeAwesome_SettingsModel settings = new MakeAwesome_SettingsModel();
    //References
    public GameObject Sun;
    public Bloom bloom;
    public CreaseShading creaseShading;
    public Antialiasing antialising;
    public VignetteAndChromaticAberration vignette;
    public SunShafts sunShafts;

    //Editor variables
    public string fileName_Save = "fgd";
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
            bloom.bloomIntensity = settings.bloomIntensity * settings.globalIntensity;
            bloom.enabled = settings._Bloom;
        }
        else
        {
            Setup();
        }
        if (creaseShading != null)
        {
            creaseShading.intensity = settings.creaseShadingIntensity * settings.globalIntensity;
            creaseShading.enabled = settings._CreaseShading;
        }
        else
        {
            Setup();
        }
        if (vignette != null)
        {
            vignette.intensity = settings.vignetting;
            vignette.enabled = settings._Vignette;
        }
        else
        {
            Setup();
        }
        if (sunShafts != null)
        {
            sunShafts.sunShaftIntensity = settings.sunShaftIntensity;
            sunShafts.enabled = settings._SunShafts;
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
            antialising.enabled = settings._AntiAlising;
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
    public void SaveSettings()
    {
        MakeAwesome_SaveLoad save = new MakeAwesome_SaveLoad();
        save.SaveSettings(settings, fileName_Save);
    }
    public void LoadSettings()
    {
        MakeAwesome_SaveLoad load = new MakeAwesome_SaveLoad();
        settings = load.LoadSettings("test");
    }

    public void RestoreDefault()
    {
        settings.globalIntensity = 1;
        settings.bloomIntensity = 0.15f;
        settings.creaseShadingIntensity = 0.2f;
        settings.vignetting = 0.08f;
        settings.sunShaftIntensity = 0.5f;

        settings._Bloom = true;
        settings._CreaseShading = true;
        settings._AntiAlising = true;
        settings._Vignette = true;
        settings._SunShafts = true;
        Apply();
    }
}
