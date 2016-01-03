using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Sprite currentSprite;
	Animator anim;
    AudioSource audio;
    Vector3 pos, dir_up, dir_down, dir_left, dir_right, raycast_pos;                                
    float speed = 4.7f;                         // speed of movement

    bool audioOn;


    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSprite = spriteRenderer.sprite;
		anim = GetComponent<Animator> ();
        audio = GetComponent<AudioSource>();

        pos = transform.position;               //this points to the top-left corner of character

        dir_up = transform.TransformDirection(Vector3.up);
        dir_down = transform.TransformDirection(Vector3.down);
        dir_left = transform.TransformDirection(Vector3.left);
        dir_right = transform.TransformDirection(Vector3.right);
    }
	

	void FixedUpdate () {

        //offset raycast to start from character's feet
        raycast_pos = transform.position;       
        raycast_pos.x += 1f;
        raycast_pos.y -= 1.1f;

        //Debug.DrawRay(raycast_pos, dir_down, Color.green);    //debug: render raycast

        //player movement animation
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)
            )
        {
            anim.SetBool("isWalking", true);
        }

        else
            if (transform.position == pos)
        {
            anim.SetBool("isWalking", false);
        }

        //changes player position
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position == pos)
        {
            if (transform.position == pos && !Physics2D.Raycast(raycast_pos, dir_left, 1))
                pos += Vector3.left; 
            else
                anim.SetBool("isWalking", false);

            anim.SetFloat("input_x", -1.0f);
            anim.SetFloat("input_y", 0f);
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position == pos)
        {
            if (transform.position == pos && !Physics2D.Raycast(raycast_pos, dir_right, 1))
                pos += Vector3.right;
            else
                anim.SetBool("isWalking", false);

            anim.SetFloat("input_x", 1.0f);
            anim.SetFloat("input_y", 0f);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position == pos)
        {
            if (transform.position == pos && !Physics2D.Raycast(raycast_pos, dir_up, 1))
                pos += Vector3.up;
            else
                anim.SetBool("isWalking", false);

            anim.SetFloat("input_x", 0f);
            anim.SetFloat("input_y", 1.0f);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position == pos)
        {
            if(!Physics2D.Raycast(raycast_pos, dir_down, 1))
                pos += Vector3.down;
            else
                anim.SetBool("isWalking", false);
            
            anim.SetFloat("input_x", 0f);
            anim.SetFloat("input_y", -1.0f);
        }

        //update position (commit on code above)
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

        //walking sound
        if (anim.GetBool("isWalking"))
        {
            if (!audioOn)
            {
                audio.Play();
                audioOn = true;
            }
        }
        else {
            if (audioOn)
            {
                audio.Pause();
                audioOn = false;
            }
        }


        /*
            Sprite selection
        */

        //onPhone: handled in PhoneLogic.cs

    }
}


/* Sources

    [1] http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html
 
    */
