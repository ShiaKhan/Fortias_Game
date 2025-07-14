using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    public string[] dialogLines;
    public int currentLine;
    void Start()
    {
        dialogBox.SetActive(false);
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogBox.SetActive(true);
            dialogText.text = dialogLines[currentLine];
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
        }
    }
    

}
