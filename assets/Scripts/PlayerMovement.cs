using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
	Rigidbody2D rbody;
	Animator anim;
    
    Vector3 pos, dir_up, dir_down, dir_left, dir_right;                                
    float speed = 2.0f;                         // Speed of movement

    // Use this for initialization
    void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
        pos = transform.position;          // Take the initial position

        dir_up = transform.TransformDirection(Vector3.up);
        dir_down = transform.TransformDirection(Vector3.down);
        dir_left = transform.TransformDirection(Vector3.left);
        dir_right = transform.TransformDirection(Vector3.right);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.A) && transform.position == pos && !Physics.Raycast(transform.position, dir_left, 1))
        {
            pos += Vector3.left;
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", -1.0f);
            anim.SetFloat("input_y", 0f);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position == pos && !Physics.Raycast(transform.position, dir_right, 1))
        {
            pos += Vector3.right;
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", 1.0f);
            anim.SetFloat("input_y", 0f);
        }
        else if (Input.GetKey(KeyCode.W) && transform.position == pos && !Physics.Raycast(transform.position, dir_up, 1))
        {
            pos += Vector3.up;
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", 0f);
            anim.SetFloat("input_y", 1.0f);
        }
        else if (Input.GetKey(KeyCode.S) && transform.position == pos && !Physics.Raycast(transform.position, dir_down, 1))
        {
            pos += Vector3.down;
            anim.SetBool("isWalking", true);
            anim.SetFloat("input_x", 0f);
            anim.SetFloat("input_y", -1.0f);
        }
        else if(Physics.Raycast(transform.position, dir_left, 1))
        {
            anim.SetBool("isWalking", false);
            print("Collision detected.");
        } else
        {
            anim.SetBool("isWalking", false);
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
	}
}


/* Source

    [1] http://answers.unity3d.com/questions/611343/movement-2d-in-a-grid.html
 
    */
