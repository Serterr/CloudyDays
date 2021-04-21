using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorer : MonoBehaviour
{
    public Text Score;
    public GameObject Player;
    CharacterCloudController CloudController;
    // Start is called before the first frame update
    void Start()
    {
        CloudController = Player.GetComponent<CharacterCloudController>();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text =("Score: " + CloudController.Score);
    }
}
