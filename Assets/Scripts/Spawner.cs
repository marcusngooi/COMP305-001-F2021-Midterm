using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // "Public variables
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameObject objectSpawner;
    [SerializeField] private float jumpForce = 5000f;

    // Private variables
    private float timer = 0;
    private float spawnRate = 0.05f;
    private bool isChestOpened = false;

    // Update is called once per frame
    void Update()
    {
        if (isChestOpened && timer > spawnRate)
        {
            // Instantiate the object
            GameObject gObj;
            gObj = GameObject.Instantiate(objectToSpawn, objectSpawner.transform.position, objectSpawner.transform.rotation);
            gObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jumpForce));
            Destroy(gObj, 4f);

            // Reset timer
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    public void OpenChest()
    {
        isChestOpened = true;
    }
}
