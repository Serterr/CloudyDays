using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class RainCatcher : MonoBehaviour
{
    public int RainTime;   //Time rained on for triggering next stage
    public GameObject Player;   //Player object to make sure player is aligned

    //Set scores in this list as required by each object
    // 5 values in each list, index 0 is for approaching stage -2, index 1 for -1, index 2 for 0, index 3 for 1, and index 4 for 
    public List<int> Scorelist = new List<int>(); //Elements 0 and 1 should usually be negative, and elements 2, 3, and 4 should usually be positive
    public int CurrentProgressStage = 2; //To know how many points to award
    public int PreviousProgressStage = 2; //to track if we are coming to 0 from the positives or negatives
    private int waiting;
    public Animator MyAnimator;
    public Slider slider;
    private float FillSpeed = 50;
    private float targetProgress;
    public int leftoff = 2;         //Left border for detecting rain; change in inspector based on object size
    public int rightoff = 2;        //Right border for detecting rain; change in inspector based on object size
    public int bottomborder = 0; //bottom edge to detect rain (only for high objects)
    private CharacterCloudController CloudController;
    private DayReset dayReset;
    private bool RainLock = false;
    private Image fill;
    private Color originalcolor;
    // Start is called before the first frame update
    void Start()
    {
        CloudController = Player.GetComponent<CharacterCloudController>();
        dayReset = Player.GetComponent<DayReset>();
        waiting=0;
        targetProgress=0;
        fill = slider.GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t=> t.name == "Fill");
        originalcolor = fill.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(waiting==0){
            waiting=1;
            StartCoroutine(OneSecWait());
        }
        if(slider.value != targetProgress){
            slider.value = targetProgress;
        }
        if(dayReset.EndofDay == true){
            //if(RainTime >=5){
                //RainTime=5;
            //}
            //else if(RainTime<=-5){
                //RainTime=-5;
            //}
            //else{
            RainTime=0;
            //}
            slider.value = 0;
            targetProgress = 0;
            RainLock=false;
            fill.color=originalcolor;
        }
    }
    
    IEnumerator OneSecWait(){
        //Alter the rightoff and leftoff to change the width of the rain catch zone
        if (Input.GetKey(KeyCode.Space)&&(this.bottomborder <= Player.transform.position.y)&&(this.transform.position.x>(Player.transform.position.x-rightoff)&&(this.transform.position.x<(Player.transform.position.x+leftoff))) &&RainLock==false){
            if(CloudController.crazymode==true){
                RainTime--; //change the local RainTime value
                MyAnimator.SetFloat("RainTime", (MyAnimator.GetFloat("RainTime"))-1); //change the animator RainTime value
                if(RainTime < 1){
                    IncrementProgress(-(RainTime));
                    fill.color = Color.Lerp(Color.red, Color.red, 0.5f);
                }
                else{
                    IncrementProgress((RainTime));
                    fill.color = originalcolor;
                }
            }
            else{
                RainTime++; //change the local RainTime value
                MyAnimator.SetFloat("RainTime", (MyAnimator.GetFloat("RainTime"))+1); //change the animator RainTime value
                IncrementProgress(RainTime);
            }
            //Track current stage for determining scores
            if(RainTime==5 || RainTime==-5){
                PreviousProgressStage = CurrentProgressStage;
                if(CloudController.crazymode==false){
                    CurrentProgressStage++;
                }
                else{
                    CurrentProgressStage--;
                }
                //Edge case where we approach 0 from the pos or neg.
                //stage 0 is usually a better situation than -1, and worse than 1
                //If days expanded to 3, then edge case will be expanded to include approaching 1 and -1 from and unexpected direction
                if(CurrentProgressStage==2 && PreviousProgressStage==3){
                    CloudController.Score -=Scorelist[CurrentProgressStage];
                }
                else{
                    CloudController.Score +=Scorelist[CurrentProgressStage];
                }
                RainLock=true; //Now that we have changed state, lock this object until the next day
                //Increments so we don't do this loop repeatedly. We only want to set an animator to
                // crazy mode if the cloud was in crazy mode when it was filled up
            }
            
        }
        yield return new WaitForSeconds(1);
        waiting=0;
    }

    public void IncrementProgress(float newProgress){
        targetProgress = newProgress;
    }
}
