using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    // public GameObject PlayerObj;

    GameObject PlayerObj;


    public float Difference;

    private void Start()
    {
        PlayerObj = GameObject.Find("Player");

      //  Debug.Log(PlayerObj.transform.position);

    }

    private void Update()
    {
       

        if (PlayerObj.transform.position.y + Difference < this.transform.position.y)
        {
            Debug.Log("A" + (PlayerObj.transform.position.y + Difference) + "  ::  " + this.transform.position.y);
            transform.position = PlayerObj.transform.position + new Vector3(0, 1f, -10f);
        }
       
    }

}
