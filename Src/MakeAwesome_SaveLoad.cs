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

                Debug.Log("Saved sucessfully at: " + Path.GetFullPath(saveFile));
            }
            catch (Exception ex)
            {
                Debug.Log("Something went wrong: " + ex);
                return false;
            }
            return true;
        }
    }
}
