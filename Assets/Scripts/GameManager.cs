using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static List<int> collectedItems = new List<int>();
    public static Dictionary<int, string> itemNames = new Dictionary<int, string>();
    public static Dictionary<int, ItemData> itemMeta = new Dictionary<int, ItemData>();

    public static int wrongClicks = 0;
    public static int currentExpectedItemID = 1; // Começa esperando o item 1

    public static void RegisterItem(int itemID, string itemName, ItemData data = null) {
        if (!itemNames.ContainsKey(itemID)) {
            itemNames[itemID] = itemName;
        }

        if (data != null && !itemMeta.ContainsKey(itemID)) {
            itemMeta[itemID] = data;
        }
    }
}
