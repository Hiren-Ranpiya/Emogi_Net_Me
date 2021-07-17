using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] HelixPrefab;

   // public GameObject ParentObj;

    private void Start()
    {
        

        for (int i = 0; i <= HelixPrefab.Length; i++)
        {
            int RandomValue = Random.Range(0, (HelixPrefab.Length -1 ));

            if (i == 0) 
            {
                RandomValue = 0;
            }

            else if (i == HelixPrefab.Length)
            {
                RandomValue = (HelixPrefab.Length - 1);
            }

           GameObject Ring = Instantiate(HelixPrefab[RandomValue]);

            // Ring.transform.SetParent(ParentObj.transform);

            Ring.transform.SetParent(GameObject.Find("HelixManager").transform);

            Ring.transform.position = new Vector3(0,-2f * i,0);

        }
       
    }

}
