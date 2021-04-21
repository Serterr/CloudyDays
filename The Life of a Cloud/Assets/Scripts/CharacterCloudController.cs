using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterCloudController : MonoBehaviour {

    public float speed;                //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Camera myCamera;
    private float TimeSinceRained;
    private bool countingsecond;
    public bool crazymode;
    public Sprite happySprite;
    public Sprite sadSprite;
    public Sprite crazySprite;
    public Sprite neutralSprite;
    public SpriteRenderer spriteRenderer;
    public ParticleSystem particleSystem;
    public Color32 crazyColor1;
    public Color32 crazyColor2;
    public Slider RainSlider;
    public bool m_FacingRight = false;  // For determining which way the player is currently facing.
    private bool paused = false;

    public int Score;
    public float orthographicSizeMin;
    public float orthographicSizeMax;

    public float zoomSpeed;
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        TimeSinceRained = 0;
        countingsecond = false;
        crazymode = false;
        crazyColor1 = new Color32(230, 33, 33, 254);
        crazyColor2 = new Color32(135, 34, 34, 254);
        spriteRenderer.sprite = neutralSprite;
    }

    void Update(){
        //Pause button: Needs menu (Resume, Options, Titlescreen(with a "are you sure?"), Exit to Desktop(with a "are you sure?")
         if(Input.GetKeyDown(KeyCode.P)){
            if(paused==true){
                paused=false;
                Time.timeScale = 1;
            }
            else{
                paused=true;
                Time.timeScale = 0;
            }
        }
        //Only check for player keyboard input if not paused
        if(paused==false){
            //For Testing sprite only; remove from gameplay
            if (Input.GetKeyDown(KeyCode.I))
                spriteRenderer.sprite = happySprite;
            if (Input.GetKeyDown(KeyCode.O))
                spriteRenderer.sprite = sadSprite;
            if (Input.GetKeyDown(KeyCode.Y))
                spriteRenderer.sprite = crazySprite;
            if (Input.GetKeyDown(KeyCode.U))
                spriteRenderer.sprite = neutralSprite;
            //End above
            if (Input.GetKey(KeyCode.Space)){
                particleSystem.Play();
                spriteRenderer.sprite = happySprite;
                if(crazymode)
                    spriteRenderer.sprite = crazySprite;
                TimeSinceRained = 0;
            }
            if (Input.GetKeyUp(KeyCode.Space)){
                particleSystem.Stop();
                spriteRenderer.sprite = neutralSprite;
                if(crazymode)
                    spriteRenderer.sprite = crazySprite;
                TimeSinceRained = 0;
            }
            //Performs mouse wheel zoom
            if (myCamera.orthographic){
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    myCamera.orthographicSize += zoomSpeed;
                }
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    myCamera.orthographicSize -= zoomSpeed;
                }
                myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
            }
        }

        StartCoroutine(CrazyCheck());
    }
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce (movement * speed);
        if (moveHorizontal > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (moveHorizontal < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
    }
    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    IEnumerator CrazyCheck(){
        if(!countingsecond){
            countingsecond = true;
            StartCoroutine(OneSecWait());

        }
        //After some amount of time without raining, the cloud becomes sad
        if (TimeSinceRained > 30 && crazymode==false){
            spriteRenderer.sprite = sadSprite;
        }
        //After longer without raining, the cloud enters "crazy mode", often the best mode for scoring negative points
        //Crazy mode will cause objects to decay rather than to advance
        if (TimeSinceRained > 50){
            spriteRenderer.sprite = crazySprite;
            crazymode = true;
            var main = particleSystem.main;
            main.startColor = new ParticleSystem.MinMaxGradient(crazyColor1, crazyColor2);
        }
        
        yield return null;
    }

    IEnumerator OneSecWait(){
        TimeSinceRained++;
        if(Input.GetKey(KeyCode.Space)){
            RainSlider.value--; //increments the UI rain tracker
            //scale with every rain-second, reducing player size by 1% of current size
                Vector3 x = this.transform.localScale;
                x.x *= (float)0.99;
                x.y *= (float)0.99;
                this.transform.localScale = x;
        }
        yield return new WaitForSeconds(1);
        countingsecond = false;
    }

}