using UnityEngine;
using System.Threading;
using System.Collections;

public class PhoneLogic : MonoBehaviour {

    AudioSource audio;
    bool audioOn;
    Animator anim;

    GameObject player;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("mainchar");

        Idle();
    }

    void Update()
    {
        if (!audioOn && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            )
        {
            StartCoroutine(startRinging(0.5f));
        }

        if(anim.GetBool("isRinging") && player.transform.position == new Vector3(1.501f, -6.054f, 0) && player.GetComponent<SpriteRenderer>().sprite.name == "mainchar_walkcycle_18" && Input.GetKey(KeyCode.E))
        {
            PickUp();
        }


    }

    IEnumerator startRinging(float delayInSeconds)
    {
        audioOn = true;
        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
            {
                audio.Play();
                anim.SetBool("isRinging", true);
                yield return new WaitForSeconds(3f);
            }
            else yield return new WaitForSeconds(delayInSeconds);
        }
    }

    void PickUp()
    {
        anim.SetBool("isRinging", false);
        anim.SetBool("isPickedUp", true);
        audio.Stop();
    }

    void Idle()
    {
        anim.SetBool("isRinging", false);
        anim.SetBool("isPickedUp", false);
    }
}
