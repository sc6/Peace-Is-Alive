using UnityEngine;
using System.Collections;

public class HintLogic : MonoBehaviour {

    Transform[] TextRects;  //0: shadow, 1: foreground
    Transform PlayerRect;
    Vector3 PlayerPos, TextPos, ShadowPos;
    MeshRenderer[] Renderers;
    TextMesh[] TextMeshes;
    GameObject player;
    AudioSource audio;


    bool d0, d1, d2;            //has the hint already completed?
    

    void Start () {
        player = GameObject.Find("mainchar");
        TextRects = GetComponentsInChildren<Transform>();
        PlayerRect = player.GetComponent<Transform>();

        TextPos = PlayerPos;
        ShadowPos = new Vector3(++PlayerPos.x, ++PlayerPos.y, PlayerPos.z);

        audio = GetComponent<AudioSource>();

        Renderers = GetComponentsInChildren<MeshRenderer>();

        Renderers[0].sortingOrder = 6;  //assign layer sort (shadow behind foreground)
        Renderers[1].sortingOrder = 7;

        TextMeshes = GetComponentsInChildren<TextMesh>();

        StartCoroutine("FollowPlayer");
    }
	
	void Update () {
       
        //d0: init, Press WASD or Arrow Keys to move.
        if(!d0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
             Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
             Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
             Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)
             ))
        {
            d0 = true;
            StartCoroutine("FadeOut");
        }

        //d1: Press E to interact with objects.
        if(!d1 && player.transform.position == new Vector3(1.501f, -6.054f, 0) && player.GetComponent<SpriteRenderer>().sprite.name == "mainchar_walkcycle_18")
        {
            d1 = true;
            ChangeText("Press E to interact with objects.");
            StartCoroutine("FadeIn");
        }

        if(!d2 && d1 && player.transform.position != new Vector3(1.501f, -6.054f, 0) && player.GetComponent<SpriteRenderer>().sprite.name == "mainchar_walkcycle_18")
        {
            d1 = false;
            StartCoroutine("FadeOut");
        }
        
        
    }


    IEnumerator FollowPlayer()
    {
        while (true)
        {
            PlayerPos = PlayerRect.position;

            TextPos.x = PlayerPos.x + 1.3f;
            TextPos.y = PlayerPos.y - 0.2f;

            ShadowPos.x = TextPos.x - 0.03f;
            ShadowPos.y = TextPos.y + 0.03f;

            TextRects[0].position = ShadowPos;
            TextRects[1].position = TextPos;


            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        /*
        for(float i = 0f; i <= 5f; i++)
        {
            Color c = TextMeshes[0].color;
            c.a = i * (1f/5f);
            TextMeshes[0].color = c;

            Color c2 = TextMeshes[1].color;
            c2.a = i * (1.0f / 5f);
            TextMeshes[1].color = c2;

            yield return null;
        }
        */

        audio.Play();

        Color c = TextMeshes[0].color;
        c.a = 1f;
        TextMeshes[0].color = c;

        Color c2 = TextMeshes[1].color;
        c2.a = 1f;
        TextMeshes[1].color = c2;

        yield return null;
    }

    IEnumerator FadeOut()
    {
        for (float i = 0f; i <= 15f; i++)
        {
            Color c = TextMeshes[0].color;
            c.a = 1.0f - (i*(1.0f/15f));
            TextMeshes[0].color = c;

            Color c2 = TextMeshes[1].color;
            c2.a = 1.0f - (i * (1.0f / 15f));
            TextMeshes[1].color = c2;

            yield return null;
        }
        
    }

    void ChangeText(string str)
    {
        TextMeshes[0].text = str;
        TextMeshes[1].text = str;
    }
}
