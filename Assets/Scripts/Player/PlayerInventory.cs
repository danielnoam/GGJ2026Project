using System.Collections.Generic;
using DNExtensions.Utilities;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    
    [Header("Inventory Settings")]
    [SerializeField, ReadOnly] private List<SOItem> allItems = new List<SOItem>();
    
    public List<SOItem> AllItems => allItems;
    public int Count => allItems.Count;
    public bool IsEmpty => allItems.Count == 0;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    public bool TryAddItem(SOItem item)
    {
        if (!item)
        {
            Debug.LogWarning("Attempted to add null item to inventory.");
            return false;
        }
        
        allItems.Add(item);
        GameEvents.InventoryChanged(this);
        return true;
    }
    
    public bool TryRemoveItem(SOItem item)
    {
        if (!item)
        {
            Debug.LogWarning("Attempted to remove null item from inventory.");
            return false;
        }
        
        if (!allItems.Remove(item))
        {
            Debug.LogWarning($"Item {item.name} not found in inventory.");
            return false;
        }
        
        GameEvents.InventoryChanged(this);
        return true;
    }
    
    public bool HasItem(SOItem item)
    {
        return allItems.Contains(item);
    }
    
    public void Clear()
    {
        allItems.Clear();
        GameEvents.InventoryChanged(this);
    }
    
    public SOItem GetItemAtIndex(int index)
    {
        if (index < 0 || index >= allItems.Count)
        {
            Debug.LogWarning($"Index {index} out of range.");
            return null;
        }
        
        return allItems[index];
    }

    public int GetItemIndex(SOItem item)
    {
        if (!item) return -1;
        
        return allItems.IndexOf(item);
    }
}