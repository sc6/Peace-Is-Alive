using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueLogic : MonoBehaviour {

    RectTransform dialogueWrapper;
    Text dialogueText;
    Vector3 dialoguePosition;
    GameObject player;

    Sprite dir_down;
    

    bool d0, d1, d2, d3; //did the dialogue run?

    void Start () {
        dialogueWrapper = GetComponent<RectTransform>();
        dialogueText = GetComponentInChildren<Text>();
        dialoguePosition = dialogueWrapper.position;
        player = GameObject.Find("mainchar");

    }
	
	void Update () {

        /*
        //d0: Init
        if (!d0)
        {
            d0 = true;
            dialogueText.text = "Use WASD or the Arrow Keys to move.";
            StartCoroutine("SlideIntoView");
        }

        //d1: Display movement directions
        if (!d1 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
             Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
             Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
             Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
             )
        {
            d1 = true;
            StartCoroutine("SlideOutOfView");
        }
        
        //d2: Interact with telephone
        if (!d2 && player.transform.position == new Vector3(1.501f, -6.054f, 0) && player.GetComponent<SpriteRenderer>().sprite.name == "mainchar_walkcycle_18")
        {
            if(player.GetComponent<SpriteRenderer>().sprite.name == "mainchar_walkcycle_18")
            {
                d2 = true;
                dialogueText.text = "Press E to interact with objects.";
                StartCoroutine("SlideIntoView");
            }
        }

        if(!d3 && d2 && player.transform.position != new Vector3(1.501f, -6.054f, 0) && player.GetComponent<SpriteRenderer>().sprite.name != "mainchar_walkcycle_18")
        {
            d2 = false;
            StartCoroutine("SlideOutOfView");
        }
        */

    }
 


    IEnumerator SlideOutOfView()
    {
        for (int i = 1; i <= 21; i++)
        {
            dialoguePosition.x = -i * (350 / 20);       //[?] calculation doesn't exactly equal -350, even with i <= 20
            dialogueWrapper.position = dialoguePosition;
            yield return null;
        }
    }

    IEnumerator SlideIntoView()
    {
        for (int i = 1; i <= 20; i++)
        {
            dialoguePosition.x = (i * (350 / 20)) - 350;
            dialogueWrapper.position = dialoguePosition;
            yield return null;
        }
    }


}
