using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ScrolSystem : MonoBehaviour
{

    private string _defaultData = "ItemInfo"; // json file name to fetch

    public ItemList MyItems = new ItemList();

    [SerializeField] private GameObject _scrollBar;

    public Transform Parentconect;
    public List<ObjectDetalis> ListObjects;
    public GameObject PrefabObject;

  


    private float scroll_pos = 0;
    private float[] pos;

   
    private void OnEnable()
    {
        WebRequest.myWebEvent += GetdataJSon;
    }

    private void OnDisable()
    {
        WebRequest.myWebEvent -= GetdataJSon;
    }

    private void Update()
    {
        InUpdate();
    }
    //Animation UI/UX
    #region Animation

    private void InUpdate()
    {
        float _distance;
        pos = new float[transform.childCount];
        _distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = _distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = _scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (_distance/2) && scroll_pos > pos[i] - (_distance/2))
                {
                    _scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(_scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);


                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.5f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (_distance/2) && scroll_pos > pos[i] -(_distance/2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f);

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }

    }
    #endregion


    //-------------------
    public void GetdataJSon()
    {
        TextAsset versionDeaultData = (TextAsset)Resources.Load(_defaultData);

        MyItems = JsonConvert.DeserializeObject<ItemList>(versionDeaultData.text);

        Debug.Log(MyItems);

        ShowDataInUI();
    
    }

    //-----------------


    public void ShowDataInUI()
    {
        foreach (Root item in MyItems.items)
        {
            ObjectDetalis elem = Instantiate(PrefabObject).GetComponent<ObjectDetalis>();
            elem.transform.SetParent(Parentconect, false);

            elem.transform.localScale = Vector3.one;

            elem.Type.text = item.type;
            elem.Text.text = item.text;

            ListObjects.Add(elem);
        }
    }

}

