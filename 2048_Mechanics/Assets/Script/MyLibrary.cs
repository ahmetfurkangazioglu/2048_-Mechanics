using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
namespace MyLibrary
{
    [Serializable]
    public class Missions
    {
        public int MissionValue;
        public string MissionType;
        public Sprite MissonSprite;
    }
    [Serializable]
    public class MissionTool
    {
        public Image MissionImage;
        public TextMeshProUGUI MissionTxt;
        public GameObject CompletedImage;
        public GameObject MissionObject;
    }
}