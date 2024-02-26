using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BearRun
{
    [Serializable]
    public class PatternItem
    {
        public string PrefabName;
        public Vector3 Position;
    }

    // Ò»Ì×·½°¸
    [Serializable]
    public class PatternProgram
    {
        public List<PatternItem> PatternItems = new List<PatternItem>();
    }

    public class PatternManager : MonoBehaviour
    {
        public List<PatternProgram> PatternPrograms = new List<PatternProgram>();
    }
}
