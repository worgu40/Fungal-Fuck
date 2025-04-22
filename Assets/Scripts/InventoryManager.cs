using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] inventorySlots;
    private int inventoryMaxSize = 10;
    private int inventorySize;

    void Start()
    {
        inventorySlots = new GameObject[inventoryMaxSize];
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (inventorySize < inventoryMaxSize)
            {
                Debug.Log("Attempting to add " + other.gameObject.name + " to inventory.");
                AddItemToInventory(other.gameObject);
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }
    private void AddItemToInventory(GameObject item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null)
            {
                inventorySlots[i] = item;
                inventorySize++;
                item.SetActive(false); // Hide the item in the world
                break;
            }
            else {
                Debug.Log("Item already in slot " + i + ", Skipping slot..." );
            }
        }
    }
}
