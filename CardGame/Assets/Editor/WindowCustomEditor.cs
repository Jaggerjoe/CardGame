using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Collections.Generic;
using NetWork;

public class WindowCustomEditor : EditorWindow
{
    UnityWebRequest request;
    public const string SHEET_ID = "12uPt3HF8pTSMS2GVi8SOQMEk4NZ2pA7aUhHwqx-xmx8";
    public const string SHEET_NAME = "DataCardAnglais";
    public const string SHEET_RANGE = "A2:G23";

    int Number;
    SO_Board m_DeckCards = null;
    List<string> m_SetAssetPath = new List<string>();
    public void CreateAssetCard()
    {
        string[] asset = request.downloadHandler.text.Split(new char[] { '\n', ',' });
        CreatePathAssetExist();
        string[] l_GetAssetPath = m_SetAssetPath.ToArray();
        ClearList();
        //loadasset si il existe, sinon create, attention GUID pas un chemin normalement a traduire.
        for (int i = 0; i < asset.Length; i++)
        {
            string l_NameCard = NacifyText(asset[i]);
            SO_CardData l_Asset = null;

            for (int k = 0; k < l_GetAssetPath.Length; k++)
            {
                if (l_GetAssetPath[k].Contains(l_NameCard))
                {
                    l_Asset = (SO_CardData)AssetDatabase.LoadAssetAtPath(l_GetAssetPath[k], typeof(SO_CardData));
                    break;
                }               
            }

            if(l_Asset == null)
            {
                l_Asset = CreateAssetCard(l_NameCard);
            }

            l_Asset.m_CardNames = l_NameCard;
            i++;

            string l_AffectCard = NacifyText(asset[i]);
            l_Asset.m_EffectCard = l_AffectCard;
            i++;

            string l_PointCard = NacifyText(asset[i]);
            if (int.TryParse(l_PointCard, out Number))
            {
                l_Asset.m_PointsCard = Number;
            }
            i++;

            string l_SignCombo = NacifyText(asset[i]);
            l_Asset.m_Sign = l_SignCombo;
            i++;

            string l_PointCombo = NacifyText(asset[i]);
            if (int.TryParse(l_PointCombo, out Number))
            {
                l_Asset.m_PointCombo = Number;
            }
            i++;

            //switch, Split, flag
            string[] zonesString = asset[i].Split(new char[] { '-' });
            for (int j = 0; j < zonesString.Length; j++)
            {
                //convert
                string l_ZonesCard = NacifyText(zonesString[j]);
                int l_Zone = 0;
                if (int.TryParse(l_ZonesCard, out Number))
                {
                    l_Zone = Number;
                }

                switch (l_Zone)
                {
                    //On veut que l'enum de la zone correspondent à la zone sur la carte sur le excel
                    //pype |
                    case 1:
                        l_Asset.m_CardZone |= EZoneCard.CardZones.Zone1;
                        break;
                    case 2:
                        l_Asset.m_CardZone |= EZoneCard.CardZones.Zone2;
                        break;
                    case 3:
                        l_Asset.m_CardZone |= EZoneCard.CardZones.Zone3;
                        break;
                    case 4:
                        l_Asset.m_CardZone |= EZoneCard.CardZones.Zone4;
                        break;
                    case 5:
                        l_Asset.m_CardZone |= EZoneCard.CardZones.Zone5;
                        break;
                    case 0:
                        l_Asset.m_CardZone = EZoneCard.CardZones.Zone0;
                        break;
                    default:
                        break;
                }
            }

            i++;
            string l_IndexCard = NacifyText(asset[i]);
            if (int.TryParse(l_IndexCard, out Number))
            {
                l_Asset.m_Index = Number;
            }
            AssetDatabase.SaveAssets();
            //AddToLosit(l_Asset);
        }
    }

    private SO_CardData CreateAssetCard( string p_Name)
    {
        SO_CardData p_Asset = null;
        p_Asset = ScriptableObject.CreateInstance<SO_CardData>();

        string l_Name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Gregoire/Card/{p_Name}.asset");
        AssetDatabase.CreateAsset(p_Asset, l_Name);

        return p_Asset;
    }

    private void AddToLosit(SO_CardData p_Asset)
    {
        if (m_DeckCards == null)
        {
            string[] unusedFolder = { "Assets/Gregoire/Scripts/GameFeel/3D" };
            string[] AssetsArray = AssetDatabase.FindAssets("t:SO_Board", unusedFolder);
            foreach (var item in AssetsArray)
            {
                Debug.Log(item);
                if (m_DeckCards != null)
                {
                    m_DeckCards.Side.m_Deck.Add(p_Asset);
                    Debug.Log("ma liste existe");
                }
            }
        }
        else
        {
            m_DeckCards.Side.m_Deck.Add(p_Asset);
            Debug.Log("ma liste existe");
        }
    }

    private void CreatePathAssetExist()
    {
        string[] unusedFolder = { "Assets/Gregoire/Card" };
        string[] AssetsArray = AssetDatabase.FindAssets("t:SO_CardData", unusedFolder);
        m_SetAssetPath.Clear();

        foreach (var item in AssetsArray)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            m_SetAssetPath.Add(path);
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
        //ClearList();
        m_SetAssetPath.Clear();
    }

    private void ClearList()
    {
        if (m_DeckCards == null)
        {
            m_DeckCards = FindObjectOfType<SO_Board>();
            if (m_DeckCards != null)
            {
                m_DeckCards.Side.m_Deck.Clear();
            }
        }
        else
        {
            m_DeckCards.Side.m_Deck.Clear();
        }
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Request"))
        {
            Request();
        }

        if (GUILayout.Button("Clear"))
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
