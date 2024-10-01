using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "Loot", order = 0)]
public class Loot : ScriptableObject 
{
    public Sprite lootSprite;
    public string lootName;
    public int damage;

    public int dropChance;

    public Loot(string lootName, int dropChance)
    {
        this.lootName=lootName;
        this.dropChance=dropChance;
    }
}