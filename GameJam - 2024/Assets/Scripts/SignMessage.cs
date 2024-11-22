using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SignMessaage : MonoBehaviour
{
    [SerializeField] GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        text.SetActive(true);       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        text.SetActive(false);
    }
}
