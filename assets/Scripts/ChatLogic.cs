using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatLogic : MonoBehaviour {

    GameObject ChatText, ChatBackground, ChatInstructionText;
    Text ChatText_t, ChatInstructionText_t;
    bool onScreen;  //is the ChatBackground opaque right now?
    

	// Use this for initialization
	void Start () {

        ChatText = GameObject.Find("ChatText");
        ChatBackground = GameObject.Find("ChatBackground");
        ChatInstructionText = GameObject.Find("ChatInstructionText");
        onScreen = false;
        Image i = ChatBackground.GetComponent<Image>();
        ChatText_t = ChatText.GetComponent<Text>();
        ChatInstructionText_t = ChatInstructionText.GetComponent<Text>();
        Color cb_c = i.color;
        Color ct_c = ChatText_t.color;
        Color cit_c = ChatInstructionText_t.color;
     
        cb_c.a = ct_c.a = cit_c.a = 0f;

        i.color = cb_c;
        ChatText_t.color = ct_c;
        ChatInstructionText_t.color = cit_c;
        
    }


    public void updateText(string[] text)
    {
        if (!onScreen) StartCoroutine("fadeIn");
        
        foreach(string str in text) StartCoroutine(talk(str));
    }

    public void Idle()
    {
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeIn()
    {
        onScreen = true;

        Image i = ChatBackground.GetComponent<Image>();
        ChatText_t = ChatText.GetComponent<Text>();
        ChatInstructionText_t = ChatInstructionText.GetComponent<Text>();

        Color cb_c = i.color;
        Color ct_c = ChatText_t.color;
        Color cit_c = ChatInstructionText_t.color;

        cb_c.a = ct_c.a = cit_c.a = 1f;

        i.color = cb_c;
        ChatText_t.color = ct_c;
        ChatInstructionText_t.color = cit_c;

        yield return null;
    }

    IEnumerator fadeOut()
    {
        onScreen = false;

        Image im = ChatBackground.GetComponent<Image>();
        ChatText_t = ChatText.GetComponent<Text>();
        ChatInstructionText_t = ChatInstructionText.GetComponent<Text>();

        Color cb_c = im.color;
        Color ct_c = ChatText_t.color;
        Color cit_c = ChatInstructionText_t.color;

        for (float i = 10; i >= 0; i--)
        {
            cb_c.a = ct_c.a = cit_c.a = 1f - (i/1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator talk(string str)
    {
        ChatText_t.text = "";

        for (int i = 0; i < str.Length; i++)
        {
            ChatText_t.text = str.Substring(0, (i+1));
            yield return new WaitForSeconds(0.05f);
        }
    }
}
