using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TMP_Text text;
    public string[] lines;
    public float textspeed;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    public void ChangeScene()
    {
        Scenemanager.LoadScene("Director");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(4);
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textspeed);
            textspeed += Random.Range(-0.1f, 0.1f);
        }
    }

}
