using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   // [SerializeField]
    Rigidbody rigidBody;

    public float Speed;

     float Difference = 0.99f;



    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        gameObject.GetComponent<MeshRenderer>().material.color = new Color32(120, 134, 45, 255);

        Debug.Log(gameObject.GetComponent<MeshRenderer>().material.color);
    }

   
    void Update()
    {
      //  Debug.Log(this.transform.position + " :: " + Camera.main.transform.position);


        if (this.transform.position.y + Difference < Camera.main.transform.position.y)
        {
            Camera.main.transform.position = this.transform.position + new Vector3(0,1,-10f);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Safe" )
        {
            // Debug.Log("Collide");

              // rigidBody.velocity = new Vector3(0, 5, 0);

         //  rigidBody.AddForce(new Vector3(0, Speed * Time.deltaTime, 0), ForceMode.VelocityChange);

            //   transform.position = new Vector3(0.019f, 2, -0.736f);

            // transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 2, 0), 12f);

          //  Vector3 Temp = new Vector3(0, 20, 0);

          //  transform.position = Vector3.SmoothDamp(transform.position, transform.position + new Vector3(0, 2, 0), ref Temp, 10f);

        }

        if (other.gameObject.GetComponent<MeshRenderer>().material.name == "Safe (Instance)")
        {
            rigidBody.velocity = new Vector3(0, 5, 0);
        }

        if (other.gameObject.GetComponent<MeshRenderer>().material.name == "Finish (Instance)")
        {
            Debug.Log("Win");
            GameManager.Instance.WinScreen.SetActive(true);
        }


    }
}
