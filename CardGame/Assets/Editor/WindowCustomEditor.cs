using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class WindowCustomEditor : EditorWindow
{
    UnityWebRequest request;
    public const string SHEET_ID = "12uPt3HF8pTSMS2GVi8SOQMEk4NZ2pA7aUhHwqx-xmx8";
    public const string SHEET_NAME = "DataCardAnglais";
    public const string SHEET_RANGE = "A2:F23";
    string m_Name = "patrick";
    int Number;
    DeckCard l_Deck = null;

    public void CreateAssetCard()
    {
        string[] asset = request.downloadHandler.text.Split(new char[] { '\n', ',' });
        for (int i = 0; i < asset.Length; i++)
        {
            SO_CardData l_Asset = ScriptableObject.CreateInstance<SO_CardData>();

            string p_Name = NacifyText(asset[i]);
            m_Name = p_Name;
            l_Asset.m_CardNames = p_Name;
            i++;

            string p_Effect = NacifyText(asset[i]);
            l_Asset.m_EffectCard = p_Effect;
            i++;

            string p_PointName = NacifyText(asset[i]);
            if(int.TryParse(p_PointName, out Number))
            {
                l_Asset.m_PointsCard = Number;
            }
            i++;

            string p_Sign = NacifyText(asset[i]);
            l_Asset.m_Sign = p_Sign;
            i++;

            string p_PointCombo = NacifyText(asset[i]);
            if (int.TryParse(p_PointCombo, out Number))
            {
                l_Asset.m_PointCombo = Number;
            }
            i++;
          
            //switch, Split, flag
            string[] zonesString = asset[i].Split(new char[] { '-' });
            for (int j = 0; j < zonesString.Length; j++)
            {
                //convert
                string p_ManyZones = NacifyText(zonesString[j]);
                int p_Zones = 0;
                if (int.TryParse(p_ManyZones, out Number))
                {
                    p_Zones = Number;
                }
               
                switch(p_Zones)
                {
                    //On veut que l'enum de la zone correspondent � la zone sur la carte sur le excel
                    //pype |
                    case 1:
                        l_Asset.m_CardZone |= CardZones.Zone1;
                        break;
                    case 2:
                        l_Asset.m_CardZone |= CardZones.Zone2;
                        break;
                    case 3:
                        l_Asset.m_CardZone |= CardZones.Zone3;
                        break;
                    case 4:
                        l_Asset.m_CardZone |= CardZones.Zone4;
                        break;
                    case 5:
                        l_Asset.m_CardZone |= CardZones.Zone5;
                        break;
                    case 0:
                        l_Asset.m_CardZone = CardZones.Zone0;
                        break;
                    default:
                        break;
                }
            }

          

            string[] unusedFolder = { "Assets/Gregoire/Card" };
            string[] Asset = AssetDatabase.FindAssets(p_Name, unusedFolder);
            foreach (var item in Asset)
            {
                Debug.Log("je suis une petite patate");
            }
            if(Asset == null)
            {
                string l_Name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Gregoire/Card/{m_Name}.asset");
                AssetDatabase.CreateAsset(l_Asset, l_Name);
                AssetDatabase.SaveAssets();
            }
            
            
            //if (l_Deck == null)
            //{
            //    l_Deck = FindObjectOfType<DeckCard>();
            //    l_Deck.CardList.Add(l_Asset);
            //}
            //else
            //{
            //    l_Deck.CardList.Add(l_Asset);
            //}
        }
    }

    private string NacifyText(string P_Text)
    {
        P_Text = P_Text.Replace("\"", "");
        return P_Text;
    }

    public void Request()
    {
        request = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/{SHEET_ID}/gviz/tq?tqx=out:csv&sheet={SHEET_NAME}&range={SHEET_RANGE}");
        request.SendWebRequest();
        while (!request.isDone)
        {
            if (request.isDone)
            {
                CreateAssetCard();
            }
        }
    }
  
    public void ClearFolderCard()
    {
        string[] unusedFolder = { "Assets/Gregoire/Card" };
        foreach (var asset in AssetDatabase.FindAssets("", unusedFolder))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            AssetDatabase.DeleteAsset(path);
        }

        if(l_Deck == null)
        {
            l_Deck = FindObjectOfType<DeckCard>();
            l_Deck.CardList.Clear();
        }
        else
        {
            l_Deck.CardList.Clear();
        }
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Request"))
        {
            Request();
        }

        //if(GUILayout.Button("Create"))
        //{
        //    CreateAssetCard();
        //}

        if(GUILayout.Button("Clear"))
        {
            ClearFolderCard();
        }
    }

    [MenuItem("Tools/MyWindow")]
    public static WindowCustomEditor OpenWindow()
    {
        WindowCustomEditor w = GetWindow<WindowCustomEditor>(false, "MyWindow", true);
        w.Show();
        return w;
    }
}
