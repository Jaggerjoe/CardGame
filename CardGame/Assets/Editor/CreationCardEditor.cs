using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

[CustomEditor(typeof(WindowCustomEditor))]
public class CreationCardEditor : Editor
{
    public const string SHEET_ID = "12uPt3HF8pTSMS2GVi8SOQMEk4NZ2pA7aUhHwqx-xmx8";
    public const string SHEET_NAME = "DataCardAnglais";
    public const string SHEET_RANGE = "A2:G23";
    public static void CreateAssetCard()
    {
        Debug.Log("je suis la ");
        SO_CardData l_Asset = ScriptableObject.CreateInstance<SO_CardData>();

        string l_Name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Gregoire/Card/NewCard.asset");
        AssetDatabase.CreateAsset(l_Asset, l_Name);
        AssetDatabase.SaveAssets();
        //Selection.activeObject = l_Asset;
        Debug.Log(AssetDatabase.GetAssetPath(l_Asset));
    }

    public static void Request()
    {
        UnityWebRequest request = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/{SHEET_ID}/gviz/tq?tqx=out:csv&sheet={SHEET_NAME}&range={SHEET_RANGE}");
        request.SendWebRequest();
        Debug.Log(request.isDone);
        while (!request.isDone)
        {
            Debug.Log(request.isDone);
            Debug.Log("je sais ap");
            if (request.isDone)
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
        Debug.Log(request.isDone);
    }
    public static void ClearFolderCard()
    {
        string[] unusedFolder = { "Assets/Gregoire/Card" };
        foreach (var asset in AssetDatabase.FindAssets("NewCard", unusedFolder))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }
    }
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    using (new EditorGUILayout.HorizontalScope())
    //    {
    //        if (GUILayout.Button("Create"))
    //        {
    //            CreateAssetCard();
    //        }

    //        if (GUILayout.Button("Clear"))
    //        {
    //            Debug.Log("je t'aime Pedro");
    //            ClearFolderCard();
    //        }

    //        if (GUILayout.Button("Request"))
    //        {
    //            Request();
    //            CreateAssetCard();
    //        }
    //    }
    //}


}
