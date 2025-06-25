using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static List<int> collectedItems = new List<int>();
    public static Dictionary<int, string> itemNames = new Dictionary<int, string>();
    public static Dictionary<int, ItemData> itemMeta = new Dictionary<int, ItemData>();

    public static int wrongClicks = 0;
    public static int savedMinutesPassed = 0;
    public static int currentExpectedItemID = 1;

    public static void RegisterItem(int itemID, string itemName, ItemData data = null) {
        if (!itemNames.ContainsKey(itemID)) {
            itemNames[itemID] = itemName;
        }

        if (data != null && !itemMeta.ContainsKey(itemID)) {
            itemMeta[itemID] = data;
        }
    }

    public static void ResetSceneProgress() {
        collectedItems.Clear();
        itemMeta.Clear();
        currentExpectedItemID = 1;
        Debug.Log("Progresso da cena reiniciado. Esperando pelo item com ordem 1.");
    }
}
