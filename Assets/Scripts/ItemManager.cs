using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject[] items;

    void Start()
    {
        items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            Item itemScript = item.GetComponent<Item>();
            if (itemScript != null)
            {
                itemScript.testInt = Random.Range(1, 101); // Assign random value between 1 and 100
                Debug.Log($"Assigned random value {itemScript.testInt} to item: {item.name}");
            }
        }
    }

    void Update()
    {
        
    }
}
