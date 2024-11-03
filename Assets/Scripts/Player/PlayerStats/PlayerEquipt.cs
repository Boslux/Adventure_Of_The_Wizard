using System;
using NRPG.Core;
using UnityEngine;

namespace NRPG.Player
{
    class PlayerEquipt : MonoBehaviour
{
    private Weapons currentWeapon; // Şu an kuşanılan silah
    private Armor currentArmor; // Şu an kuşanılan zırh

    // Saldırı gücü ekleme
    public void EquipWeapon(PlayerStats stats, Weapons newWeapon)
    {
        if (currentWeapon != null)
        {
            // Eğer eski bir silah varsa, onun gücünü çıkar
            stats.attackPower -= currentWeapon.attackPower;
            Debug.Log($"Eski silahın gücü çıkarıldı: {currentWeapon.attackPower}");
        }

        // Yeni silahı kuşan ve gücünü ekle
        currentWeapon = newWeapon;
        stats.attackPower += newWeapon.attackPower;
        Debug.Log($"Yeni silah kuşanıldı: {newWeapon.name}, Güç: {newWeapon.attackPower}");
    }

    // Zırh giyme
    public void EquipArmor(PlayerStats stats, Armor newArmor)
    {
        if (currentArmor != null)
        {
            // Eğer eski bir zırh varsa, onun savunma gücünü çıkar
            stats.defensePower -= currentArmor.defensePower;
            Debug.Log($"Eski zırhın gücü çıkarıldı: {currentArmor.defensePower}");
        }

        // Yeni zırhı kuşan ve gücünü ekle
        currentArmor = newArmor;
        stats.defensePower += newArmor.defensePower;
        Debug.Log($"Yeni zırh kuşanıldı: {newArmor.name}, Savunma: {newArmor.defensePower}");
    }

    // Silah çıkarma işlemi
    public void UnequipWeapon(PlayerStats stats)
    {
        if (currentWeapon != null)
        {
            // Mevcut silahın saldırı gücünü çıkar
            stats.attackPower -= currentWeapon.attackPower;
            Debug.Log($"Silah çıkarıldı: {currentWeapon.name}, Güç: {currentWeapon.attackPower}");
            currentWeapon = null; // Artık silah kuşanılmamış olacak
        }
    }

    // Zırh çıkarma işlemi
    public void UnequipArmor(PlayerStats stats)
    {
        if (currentArmor != null)
        {
            // Mevcut zırhın savunma gücünü çıkar
            stats.defensePower -= currentArmor.defensePower;
            Debug.Log($"Zırh çıkarıldı: {currentArmor.name}, Savunma: {currentArmor.defensePower}");
            currentArmor = null; // Artık zırh kuşanılmamış olacak
        }
    }
}
}
