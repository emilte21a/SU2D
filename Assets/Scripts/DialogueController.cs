using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip beepboop;
    public TMP_Text text;
    public string lines;
    public float textspeed;

    public float initialTextSpeed;


    public bool dialogueIsDone = false;
    // Start is called before the first frame update
    void Start()
    {
        textspeed = initialTextSpeed;
        StartDialogue();
    }

    public void ChangeScene(string sceneName)
    {
        Scenemanager.LoadScene(sceneName);
    }

    public void StartDialogue()
    {
        dialogueIsDone = false;
        text.text = "";
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(4);
        foreach (char c in lines.ToCharArray())
        {
            text.text += c;
            audioSource.PlayOneShot(beepboop);
            float randomSpeed = textspeed + Random.Range(-0.05f, 0.05f);
            yield return new WaitForSeconds(randomSpeed);
        }
        dialogueIsDone = true;
    }

}
