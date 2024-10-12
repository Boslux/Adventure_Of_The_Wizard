using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    [Tooltip("Oyunda bu eşya ile ilişkili olan tile.")]
    public float dropChance; // Eşyanın düşme şansı. Varsayılan olarak 50% şans verelim.


    [Tooltip("Eşyanın türü.")]
    public Itemtype type; // Eşyanın türü.

    [Tooltip("Eşyanın gerçekleştireceği aksiyon.")]
    public ActionType actionType; // Eşyanın gerçekleştirdiği aksiyon.

    [Tooltip("Eşyanın menzili.")]
    public Vector2Int range = new Vector2Int(5, 4); // Eşyanın etki alanı.

    [Header("Only UI")]
    [Tooltip("Eşyanın stacklenebilir olup olmadığını belirler.")]
    public bool stackable = false; // Eşyanın stacklenebilirliği.

    [Header("Both")]
    [Tooltip("Eşyaya ait görsel.")]
    public Sprite image; // Eşyaya ait UI görseli.

     

}

public enum ActionType
{
    Attack, // Saldırı aksiyonu.
    Ready,  // Hazırlık aksiyonu.
    Use     // Kullanma aksiyonu.
}

public enum Itemtype
{
    Weapon,   // Silah türü.
    Wearable, // Giyilebilir tür.
    Useable   // Kullanılabilir tür.
}
