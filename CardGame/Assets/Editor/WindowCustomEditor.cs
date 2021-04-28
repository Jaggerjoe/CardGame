using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

public class WindowCustomEditor : EditorWindow
{
    public const string SHEET_ID = "12uPt3HF8pTSMS2GVi8SOQMEk4NZ2pA7aUhHwqx-xmx8";
    public const string SHEET_NAME = "DataCardAnglais";
    public const string SHEET_RANGE = "A2:G23";

    public void CreateAssetCard()
    {
        SO_CardData l_Asset = ScriptableObject.CreateInstance<SO_CardData>();

        //mettre toutes les valeurs, et changer le noms de la carte.
        string l_Name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Gregoire/Card/NewCard.asset");
        AssetDatabase.CreateAsset(l_Asset, l_Name);
        AssetDatabase.SaveAssets();
        //Selection.activeObject = l_Asset;
        Debug.Log(AssetDatabase.GetAssetPath(l_Asset));
    }

    public void Request()
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
                CreateAssetCard();
            }
        }
        Debug.Log(request.isDone);
    }
    public void ClearFolderCard()
    {
        string[] unusedFolder = { "Assets/Gregoire/Card" };
        foreach (var asset in AssetDatabase.FindAssets("", unusedFolder))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }
    }
    public void OnGUI()
    {
        if (GUILayout.Button("Request"))
        {
            Request();
        }

        if(GUILayout.Button("Create"))
        {
            CreateAssetCard();
        }

        if(GUILayout.Button("Clear"))
        {
            ClearFolderCard();
        }
    }

    [MenuItem("Tools/UndoShortcuts")]
    public static WindowCustomEditor OpenWindow()
    {
        WindowCustomEditor w = GetWindow<WindowCustomEditor>(false, "UndoShortcut", true);
        w.Show();
        return w;
    }
}
