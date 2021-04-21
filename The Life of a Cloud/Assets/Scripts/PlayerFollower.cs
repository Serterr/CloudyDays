using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.
    public Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private bool m_FacingRight = false;  // For determining which way the player is currently facing.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        //Quaternion x = this.transform.rotation; //z rotation is set to 0 to prevent spinning
        //x.z = 0;
        //x[0] = 0;
        //x[1] = 0;
        //this.transform.rotation = x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis ("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our follower.
        rb2d.AddForce (movement * speed);
        if (moveHorizontal > 0 && !m_FacingRight)
			{
				// ... flip the follower
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (moveHorizontal < 0 && m_FacingRight)
			{
				// ... flip the follower.
				Flip();
			}
    }
    private void Flip()
	{
		// Switch the way the follower is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the follower's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
