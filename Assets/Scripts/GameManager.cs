using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static List<int> collectedItems = new List<int>();
    public static Dictionary<int, string> itemNames = new Dictionary<int, string>();

    public static void RegisterItem(int itemID, string itemName) {
        if (!itemNames.ContainsKey(itemID)) {
            itemNames[itemID] = itemName;
        }
    }
}
