using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class DefineTool : EditorWindow {
    [MenuItem("DefineTool/Tool")]
    public static void Open() {
        GetWindow<DefineTool>(true, "DefineToolEditor");
    }
    
    private DefineData[] m_defineList;
    private Vector2 m_scrollView = new Vector2();

    private const string TEMPLETE_PATH = "Assets/DirectiveList";
    
    public void OnGUI() {
        //Init
        if (null == m_defineList) {
            Init();
        }
        //toggle
        m_scrollView = EditorGUILayout.BeginScrollView(m_scrollView);

        string category = null;
        string tmpdefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        string[] settingdefine = tmpdefine.Split(';');
        foreach (DefineData tDefineData in m_defineList) {
            //category
            if (category != tDefineData.category) {
                if (null != tDefineData.category) {
                    GUILayout.Label("--" + tDefineData.category + "--");
                } else {
                    GUILayout.Label("---------------");
                }
                category = tDefineData.category;
            }

            //flg
            tDefineData.flg = false;
            foreach (string itemdefine in settingdefine) {
                if (itemdefine == tDefineData.define) {
                    tDefineData.flg = true;
                    break;
                }
            }

            EditorGUILayout.BeginHorizontal();
            {
                //select
                bool nowFlg = EditorGUILayout.Toggle(tDefineData.flg, GUILayout.MaxWidth(10));
                if (nowFlg != tDefineData.flg) {
                    tDefineData.flg = nowFlg;

                    string stdefine = "";
                    foreach (DefineData itemdefine in m_defineList) {
                        if (itemdefine.flg)
                            stdefine += itemdefine.define + ";";
                    }
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                                                                     stdefine);
                    //save();
                    AssetDatabase.Refresh();
                }

                //define
                EditorGUILayout.SelectableLabel(tDefineData.define, GUILayout.MaxWidth(300));

                //comment
                if (null != tDefineData.comment && "" != tDefineData.comment) {
                    EditorGUILayout.TextField(tDefineData.comment);
                }
            }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("edit template")) {
            System.Diagnostics.Process.Start("open", TEMPLETE_PATH);
        }

        if (GUILayout.Button("reload")) {
            m_defineList = null;
        }
    }



    private void Init() {
        if (!File.Exists(TEMPLETE_PATH)) {
            FileStream fm = File.Create(TEMPLETE_PATH);
            fm.Close();
            AssetDatabase.Refresh();
        }
        string[] tAllLines = File.ReadAllLines(TEMPLETE_PATH);

        m_defineList = new DefineData[tAllLines.Length];

        int tIndex = 0;
        foreach (string tLine in tAllLines) {
            string[] tSplitLine = tLine.Split(new string[] { "//" }, StringSplitOptions.None);
            m_defineList[tIndex] = new DefineData();
            m_defineList[tIndex].define = tSplitLine[0];
            if (1 < tSplitLine.Length) {
                m_defineList[tIndex].comment = tSplitLine[1];
            }
            if (2 < tSplitLine.Length) {
                m_defineList[tIndex].category = tSplitLine[2];
            }
            tIndex++;
        }

        Array.Sort<DefineData>(m_defineList, (x, y) => {
            int tRet;
            if (null != x.category && null != y.category) {
                tRet = x.category.CompareTo(y.category);
                if (0 != tRet)
                    return tRet;
            } else if (null != x.category && null == y.category) {
                return -1;
            } else if (null == x.category && null != y.category) {
                return 1;
            }

            return -x.define.CompareTo(y.define);
        });
    }


    class DefineData {
        public string define;
        public string comment;
        public string category;
        public bool flg;
    }
}
