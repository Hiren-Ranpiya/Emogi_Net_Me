using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    /* private void OnMouseDrag()
     {
       //  Debug.Log("Enter");

         transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 50, 0));
     }*/

    Vector2 startPosition;
    Vector2 difference;
    Quaternion RotationA;

    private void Update()
    {
        /*   if (Input.GetMouseButton(0))
           {
               transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 50, 0));
           }*/


        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;

            RotationA = transform.rotation;

            

           // Debug.Log(RotationA);

          //  Debug.Log(startPosition);
        }
        if (Input.GetMouseButton(0))
        {
             difference.x = Input.mousePosition.x - startPosition.x;

            transform.rotation = RotationA *  Quaternion.Euler(new Vector2(0, difference.x));                                    //  new Vector3(0, difference.x, 0);

           // transform.Rotate(new Vector3(0, difference.x, 0));

           // Debug.Log(difference.x);
        }



    }


}
