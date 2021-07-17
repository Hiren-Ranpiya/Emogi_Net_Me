using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class btnlog : MonoBehaviour
//*************** prefab script*****************//
{
    public int Xvalue;
    public int Yvalue;

    public static btnlog Game;
    private void Awake()
    {
        Game = this;
    }
    public void btnclick()
    {
        Debug.Log(this.gameObject.GetComponentInChildren<Text>().text);
       
        connect.fairy.livelist.Add(this.gameObject);
        for (int i = 0; i < connect.fairy.btnlist.Count; i++)
        {
            connect.fairy.btnlist[i].GetComponent<Image>().color = Color.white;

            this.gameObject.GetComponent<Image>().color = new Color32(255, 23, 54, 255);
        }
    }
}
