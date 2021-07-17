using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject GameOverScreen;

    public static GameManager Instance;

    private void Awake()
    {
        

        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != null)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
