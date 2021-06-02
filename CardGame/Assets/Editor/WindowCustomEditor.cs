using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System.Collections.Generic;
using NetWork;
using System;
using System.Collections;

public class WindowCustomEditor : EditorWindow
{
    public const string SHEET_ID = "12uPt3HF8pTSMS2GVi8SOQMEk4NZ2pA7aUhHwqx-xmx8";
    public const string SHEET_NAME = "DataCardAnglais";
    public const string SHEET_RANGE = "A2:H23";

    int Number;
    SO_Board m_DeckCards = null;

    List<string> m_SetAssetPath = new List<string>();

    List<SO_EffectCard> m_SetEffetPAth = new List<SO_EffectCard>();

    List<Texture2D> m_GetTextures = new List<Texture2D>();

    string[] asset;
    public void CreateAssetCard()
    {
        CreatePathAssetExist();
        GetFolderEffect();
        GetTexture();
        for (int i = 0; i < asset.Length; i++)
        {
            string l_NameCard = NacifyText(asset[i]);
            SO_CardData l_Asset = null;

            for (int k = 0; k < m_SetAssetPath.Count; k++)
            {
                if (m_SetAssetPath[k].Contains(l_NameCard))
                {
                    l_Asset = (SO_CardData)AssetDatabase.LoadAssetAtPath(m_SetAssetPath[k], typeof(SO_CardData));
                    break;
                }
            }

            if (l_Asset == null)
            {
                l_Asset = CreateAssetCard(l_NameCard);
            }

            l_Asset.m_CardNames = l_NameCard;
            i++;

            string l_AffectCard = NacifyText(asset[i]);
            l_Asset.m_EffectCardText = l_AffectCard;
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

                for (int j = 0; j < m_SetEffetPAth.Count; j++)
                {
                    if (m_SetEffetPAth[j].IDCard == Number)
                    {
                        l_Asset.m_Effect = m_SetEffetPAth[j];
                        break;
                    }
                }
            }

            i++;
            string l_CardTxrName = NacifyText(asset[i]);
            for (int k = 0; k < m_GetTextures.Count; k++)
            {
                if (m_GetTextures[k].name == l_CardTxrName)
                {
                    l_Asset.m_CardTxr = m_GetTextures[k];
                    break;
                }
            }

            AssetDatabase.SaveAssets();
            AddToLosit(l_Asset);
        }
    }

#if UNITY_EDITOR

#endif
    private SO_CardData CreateAssetCard(string p_Name)
    {
        SO_CardData p_Asset = null;
        p_Asset = ScriptableObject.CreateInstance<SO_CardData>();

        string l_Name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Gregoire/Card/{p_Name}.asset");
        AssetDatabase.CreateAsset(p_Asset, l_Name);

        return p_Asset;
    }
    private void CreatePathAssetExist()
    {
        string[] unusedFolder = { "Assets/Gregoire/Card" };
        string[] AssetsArray = AssetDatabase.FindAssets("t:SO_CardData", unusedFolder);

        foreach (var item in AssetsArray)
        {
            var path = AssetDatabase.GUIDToAssetPath(item);
            if(!m_SetAssetPath.Contains(path))
            {
                m_SetAssetPath.Add(path);
            }
        }
    }

    private void GetTexture()
    {
        string[] l_FolderPath = { "Assets/Gregoire/CardTexture" };
        string[] l_TextureCard = AssetDatabase.FindAssets("t:texture2D", l_FolderPath);

        foreach (var item in l_TextureCard)
        {

            var path = AssetDatabase.GUIDToAssetPath(item);
            Texture2D l_Txr = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
            if (!m_GetTextures.Contains(l_Txr))
            {
                m_GetTextures.Add(l_Txr);
            }
        }
    }

    private void GetFolderEffect()
    {
        string[] l_FolderPath = { "Assets/Gregoire/Effect" };
        string[] l_EffectArrayPath = AssetDatabase.FindAssets("t:SO_EffectCard", l_FolderPath);

        foreach (var Asset in l_EffectArrayPath)
        {
            SO_EffectCard l_Effect;
            var path = AssetDatabase.GUIDToAssetPath(Asset);
            l_Effect = (SO_EffectCard)AssetDatabase.LoadAssetAtPath(path, typeof(SO_EffectCard));
            if(!m_SetEffetPAth.Contains(l_Effect))
            {
                m_SetEffetPAth.Add(l_Effect);
            }
        }
    }

    private string NacifyText(string P_Text)
    {
        P_Text = P_Text.Replace("\"", "");
        return P_Text;
    }

    public void Request()
    {
        UnityWebRequest request = UnityWebRequest.Get($"https://docs.google.com/spreadsheets/d/{SHEET_ID}/gviz/tq?tqx=out:csv&sheet={SHEET_NAME}&range={SHEET_RANGE}");
        request.SendWebRequest();
        while (!request.isDone)
        {
            if (request.isDone)
            {
                asset = request.downloadHandler.text.Split(new char[] { '\n', ',' });
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
        m_SetAssetPath.Clear();
    }

    private void AddToLosit(SO_CardData p_Asset)
    {
        if (m_DeckCards == null)
        {
            string[] unusedFolder = { "Assets/Gregoire/Scripts/Networking/So_Board" };
            string[] AssetsArray = AssetDatabase.FindAssets("t:SO_Board", unusedFolder);
            foreach (var item in AssetsArray)
            {
                var path = AssetDatabase.GUIDToAssetPath(item);
                m_DeckCards = (SO_Board)AssetDatabase.LoadAssetAtPath(path, typeof(SO_Board));
                Debug.Log(m_DeckCards);
                m_DeckCards.Side.m_Deck.Add(p_Asset);
                m_DeckCards.Side2.m_Deck.Add(p_Asset);
            }
        }
        else
        {
            if(!m_DeckCards.Side.m_Deck.Contains(p_Asset))
            {
                m_DeckCards.Side.m_Deck.Add(p_Asset);
            } 
            if(!m_DeckCards.Side2.m_Deck.Contains(p_Asset))
            {
                m_DeckCards.Side2.m_Deck.Add(p_Asset);
            }
        }
    }
    private void ClearList()
    {
        if (m_DeckCards == null)
        {
            string[] unusedFolder = { "Assets/Gregoire/Scripts/Networking/So_Board" };
            string[] AssetsArray = AssetDatabase.FindAssets("t:SO_Board", unusedFolder);
            foreach (var item in AssetsArray)
            {
                var path = AssetDatabase.GUIDToAssetPath(item);
                m_DeckCards = (SO_Board)AssetDatabase.LoadAssetAtPath(path, typeof(SO_Board));
                m_DeckCards.Side.m_Deck.Clear();
            }
        }
        else
        {
            m_DeckCards.Side.m_Deck.Clear();
        }
    }

    private void CreateEffect()
    {
        SO_EffectCard p_Asset = null;
        p_Asset = ScriptableObject.CreateInstance<SO_EffectCard>();

        string l_Name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath($"Assets/Gregoire/Effect/Effect.asset");
        AssetDatabase.CreateAsset(p_Asset, l_Name);
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
            ClearList();
        }

        if (GUILayout.Button("CreateEffect"))
        {
            CreateEffect();
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
