using UnityEngine;
using System.Threading;
using System.Collections;

public class PhoneLogic : MonoBehaviour {

    public ChatLogic chatLogic;

    new AudioSource audio;
    bool audioOn, phonePickedUp;
    Animator anim, playerAnim;
    ChatLogic ChatScript;

    GameObject player, ChatUI;


    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("mainchar");
        ChatUI = GameObject.Find("ChatUI");
        ChatScript = (ChatLogic) ChatUI.GetComponent(typeof(ChatLogic));
        playerAnim = player.GetComponent<Animator>();

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

        if(phonePickedUp && player.transform.position != new Vector3(1.501f, -6.054f, 0))
        {
            Idle();
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
        phonePickedUp = true;
        
        playerAnim.SetBool("onPhone", true);
        anim.SetBool("isRinging", false);
        anim.SetBool("isPickedUp", true);
        audio.Stop();

        string[] narr = { "Come into my office." };
        ChatScript.updateText(narr);
    }


    void Idle()
    {
        phonePickedUp = false;

        playerAnim.SetBool("onPhone", false);
        anim.SetBool("isRinging", false);
        anim.SetBool("isPickedUp", false);

        ChatScript.Idle();
    }
}
