using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBorderControl : MonoBehaviour
{
    public Text levelTexts;
    public Text pointText;
    public Text[] statsTexts;

    public PlayerStats playerStats;
    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    private void Update() 
    {
            StatsText();
            LevelText();
            SkillPoint();
    }
    void SkillPoint()
    {
        pointText.text="Skill Point: "+playerStats.skillPoint.ToString();
    }
    void LevelText()
    {
        levelTexts.text = "Level: "+playerStats.level.ToString();
    }
    void StatsText()
    {
        statsTexts[0].text =playerStats.maxHealth.ToString();
        statsTexts[1].text =playerStats.maxMana.ToString();
        statsTexts[2].text =playerStats.attackPower.ToString();
        statsTexts[3].text =playerStats.defensePower.ToString();
    }
}
