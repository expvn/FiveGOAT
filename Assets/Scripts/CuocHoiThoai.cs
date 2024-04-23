using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuocHoiThoai : MonoBehaviour, IPointerClickHandler
{
    public GameObject parent;
    public Image playerAvatar;
    public Image npcAvatar;
    public Sprite playerSprite;
    public Sprite npcSprite;
    public TextMeshProUGUI textMeshProUGUI;

    public int current = 0;

    public List<HoiThoai> HoiThoaiList = new List<HoiThoai>();

    // Start is called before the first frame update
    private void Start()
    {
        LoadTextAsset();
        NoiChuyen();
    }

    public void LoadTextAsset()
    {
        TextAsset loadText = Resources.Load<TextAsset>("Map1/frog1");
        string[] lines = loadText.text.Split("\n");

        for (int i = 1; i < lines.Length; i++)
        {
            HoiThoai hoiThoai = new HoiThoai();
            string[] fields = lines[i].Split("\t");

            hoiThoai.id = int.Parse(fields[0]);
            hoiThoai.name = fields[1];
            hoiThoai.content = fields[2];

            HoiThoaiList.Add(hoiThoai);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        current++;
        NoiChuyen();
    }

    public void NoiChuyen()
    {
        if (current < HoiThoaiList.Count)
        {
            if (HoiThoaiList[current].name == "Player")
            {
                playerAvatar.sprite = playerSprite;
                playerAvatar.gameObject.SetActive(true);
                npcAvatar.gameObject.SetActive(false);
            }
            else
            {
                npcAvatar.sprite = npcSprite;
                playerAvatar.gameObject.SetActive(false);
                npcAvatar.gameObject.SetActive(true);
            }

            textMeshProUGUI.text = HoiThoaiList[current].content;
        }
        else
        {
            parent.SetActive(false);
        }
    }
}

[Serializable]
public class HoiThoai
{
    public int id;
    public string name;
    public string content;
}