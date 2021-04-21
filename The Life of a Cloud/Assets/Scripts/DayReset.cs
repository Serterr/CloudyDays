using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script resets time, player location, player size,the remaining-rain meter, and crazy mode
//Rainable objects reset their own bar and rain time in the RainCatcher
//Still need to added less jarring transitions
public class DayReset : MonoBehaviour
{
    public GameObject Player;
    public GameObject RainUI;
    public GameObject RainSlider;
    public ParticleSystem particleSystem;
    private Transform PlayerTransform;
    private Timer timer;
    private float InitialTime;
    private float InitialSliderValue;
    private CharacterCloudController CloudController;
    private Vector3 StartingPosition;
    private Vector3 StartingScale;
    private Slider slider;
    private Color32 normalColor1;
    private Color32 normalColor2;
    public bool EndofDay = false;
    private bool EndofLastDay = false; //Use this to trigger exit to post game screen at the end
    private int daysPast = 0;
    //Possibility of free panning on the map after score screen?
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = Player.GetComponent<Transform>();
        StartingPosition = PlayerTransform.position; //Store starting position
        StartingScale = PlayerTransform.localScale;
        timer = RainUI.GetComponent<Timer>();
        InitialTime = timer.timeRemaining; //Store starting time
        CloudController = Player.GetComponent<CharacterCloudController>();
        normalColor1 = new Color32(136, 208, 238, 255);
        normalColor2 = new Color32(64, 149, 217, 255);
        slider = RainSlider.GetComponent<Slider>();
        InitialSliderValue = slider.value;  //Store starting slider (rain) amount

    }

    // Update is called once per frame
    void Update()
    {
        //Check for end of day conditions
        if(timer.timeRemaining == 0 || slider.value == 0f){
            if(daysPast>=1){ //change to 2 if days expanded to 3
                EndofLastDay = true;
            }
            daysPast++;
            EndofDay = true;
            StartCoroutine(OneSecWait());
            //Play exploding animation and start of day animation before resetting values (here)
            timer.timeRemaining = InitialTime;
            timer.timerIsRunning = true;
            PlayerTransform.position = StartingPosition;
            PlayerTransform.localScale = StartingScale;
            timer.timeRemaining = InitialTime;
            slider.value = InitialSliderValue;
            CloudController.crazymode=false;
            var main = particleSystem.main;
            main.startColor = new ParticleSystem.MinMaxGradient(normalColor1, normalColor2);
            if(CloudController.m_FacingRight==true){
                CloudController.m_FacingRight=false;
            }

        }
    }

    IEnumerator OneSecWait(){
        yield return new WaitForSeconds(1);
        EndofDay = false;
    }
}
