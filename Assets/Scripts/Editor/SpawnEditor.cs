using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BearRun
{
    public class SpawnEditor : Editor
    {
        [MenuItem("Tools/Click Me!")]
        private static void PatternSystem()
        {
            GameObject pmObj = GameObject.Find("PatternManager");
            if (pmObj != null)
            {
                var patternManager = pmObj.GetComponent<PatternManager>();
                if (Selection.gameObjects.Length == 1) // 判断是否选择了一个游戏物体
                {
                    var items = Selection.gameObjects[0].transform.Find("ItemHolder");
                    if (items != null)
                    {
                        PatternProgram patternProgram = new PatternProgram();
                        foreach (var item in items)
                        {
                            Transform itemTrans = item as Transform;
                            if (itemTrans!=null)
                            {
                                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(itemTrans.gameObject);
                                if (prefab!=null)
                                {
                                    PatternItem patternItem = new PatternItem()
                                    {
                                        PrefabName = prefab.name,
                                        Position = itemTrans.localPosition,
                                    };
                                    patternProgram.PatternItems.Add(patternItem);
                                }
                            }
                        }
                        patternManager.PatternPrograms.Add(patternProgram);
                    }
                }
            }
        }
    }
}
