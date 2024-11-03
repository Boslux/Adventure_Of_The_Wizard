using NRPG.Player;
using UnityEngine;

namespace NRPG.Core
{
    public class StatsButtonsController : MonoBehaviour
    {
        public void IncreaseStat(int statIndex)
        {
            PlayerStats playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

            if (playerStats.skillPoint > 0)
            {
                switch (statIndex)
                {
                    case 0:
                        playerStats.maxHealth += 2;
                        break;
                    case 1:
                        playerStats.maxMana += 2;
                        break;
                    case 2:
                        playerStats.attackPower += 1;
                        break;
                    case 3:
                        playerStats.defensePower += 1;
                        break;
                    default:
                        Debug.LogWarning("Invalid stat index");
                        return;
                }

                playerStats.skillPoint--;
            }
        }
    }

}