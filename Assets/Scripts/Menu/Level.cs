using System;
using UnityEngine;

namespace Menu
{
    [Serializable]
    public struct Level
    {
        public Texture nextLvlTexture;
        public string levelName;
        public int lvlNo;
        [Range(0, 4)] public int difficulty;
        public string[] top3Players;
    }
}