using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private GameObject chestClosedObj;
    [SerializeField] private GameObject chestOpenedObj;
    [SerializeField] private GameObject spawner;

    // Private variables
    private bool inRange = false;

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            chestClosedObj.SetActive(false);
            chestOpenedObj.SetActive(true);
            spawner.GetComponent<Spawner>().OpenChest();
        }
    }

    // Detect player collision with chest
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }
}
