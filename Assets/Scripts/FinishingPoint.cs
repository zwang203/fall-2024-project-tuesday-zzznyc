using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishingPoint : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI winText;
    // Start is called before the first frame update
    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && winText != null)
        {
            winText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
