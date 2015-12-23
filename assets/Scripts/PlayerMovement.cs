using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
	Animator anim;
    
    Vector3 pos, dir_up, dir_down, dir_left, dir_right, raycast_pos;                                
    float speed = 2.0f;                         // Speed of movement

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();

        pos = transform.position;               //this points to the top-left corner of character

        dir_up = transform.TransformDirection(Vector3.up);
        dir_down = transform.TransformDirection(Vector3.down);
        dir_left = transform.TransformDirection(Vector3.left);
        dir_right = transform.TransformDirection(Vector3.right);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        raycast_pos = transform.position;       //offset raycast to start from character's feet
        raycast_pos.x += 1f;
        raycast_pos.y -= 1.1f;

        //Debug.DrawRay(raycast_pos, dir_down, Color.green);    //DEBUG: render raycast

   
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position == pos)
        {
            if(transform.position == pos && !Physics2D.Raycast(raycast_pos, dir_left, 1))
            {
                pos += Vector3.left;
                anim.SetBool("isWalking", true);
            }
            else anim.SetBool("isWalking", false);

            anim.SetFloat("input_x", -1.0f);
            anim.SetFloat("input_y", 0f);
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position == pos)
        {
            if (transform.position == pos && !Physics2D.Raycast(raycast_pos, dir_right, 1))
            {
                pos += Vector3.right;
                anim.SetBool("isWalking", true);
            }
            else anim.SetBool("isWalking", false);

            anim.SetFloat("input_x", 1.0f);
            anim.SetFloat("input_y", 0f);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position == pos)
        {
            if (transform.position == pos && !Physics2D.Raycast(raycast_pos, dir_up, 1))
            {
                pos += Vector3.up;
                anim.SetBool("isWalking", true);
            }
            else anim.SetBool("isWalking", false);

            anim.SetFloat("input_x", 0f);
            anim.SetFloat("input_y", 1.0f);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position == pos)
        {
            if(!Physics2D.Raycast(raycast_pos, dir_down, 1))
            {
                pos += Vector3.down;
                anim.SetBool("isWalking", true);

            } else anim.SetBool("isWalking", false);
            
            anim.SetFloat("input_x", 0f);
            anim.SetFloat("input_y", -1.0f);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
	}
}


/* Sources

    [1] http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html
 
    */
