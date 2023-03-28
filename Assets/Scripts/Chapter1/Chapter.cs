using System;
using UnityEngine;

namespace Chapter1
{
    [Serializable]
    public struct Chapter
    {
        public Texture2D coverPic;
        public string chapterName;
        public string sceneBuildName;
        public string levelDifficulty;
        public bool isLock;
        public bool underDevelopment;
    }
}