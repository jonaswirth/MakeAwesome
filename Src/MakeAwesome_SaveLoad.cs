using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace Assets.MakeAwesome.Src
{
    class MakeAwesome_SaveLoad
    {
        public const string SavePath = "Assets\\MakeAwesome\\resources\\SavedSettings\\";
        public bool SaveSettings(MakeAwesome_SettingsModel settings, string name)
        {
            string saveFile = SavePath + name + ".json";
            try
            {
                string json = JsonUtility.ToJson(settings);
                using (StreamWriter sw = new StreamWriter(saveFile))
                {
                    sw.Write(json);
                }

                Debug.Log("Saved sucessfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Something went wrong:\n " + ex);
                return false;
            }
            return true;
        }
        public MakeAwesome_SettingsModel LoadSettings(string name)
        {
            string loadFile = SavePath + name + ".json";
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(loadFile))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        json += line;
                    }

                }
                Debug.Log("Settings loaded.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Unable to read file:\n" + ex);
            }
            return JsonUtility.FromJson<MakeAwesome_SettingsModel>(json);
        }
    }
}
