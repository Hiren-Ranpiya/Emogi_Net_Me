using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerA : MonoBehaviour
{
    // Start is called before the first frame update

    public static SceneManagerA Instance;
    void Start()
    {
      //  DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("SceneB");
        }
        if (Input.GetKey(KeyCode.A))
        {
            SceneManager.LoadScene("SceneA");
        }
    }
}
