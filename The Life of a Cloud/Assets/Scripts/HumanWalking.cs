using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWalking : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    private bool countingsecond;
    private float randnum;
    private float turnprobability = 5; //As a percentile (5 = 5% chance/sec)
    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if(groundInfo.collider ==false){
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        //Check for a chance to randomly turn around every second
        if(countingsecond ==false){
            countingsecond =true;
            randnum =Random.Range(1, 100);
            if(randnum <= turnprobability){
                if(movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                }
                else{
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
                speed = Random.Range(1.0f, 3.0f);
            }
            StartCoroutine(OneSecWait());
        }
    }

    IEnumerator OneSecWait(){
        yield return new WaitForSeconds(1);
        countingsecond = false;
    }
}
