using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private BaseView CurentBaseView;
    [SerializeField] private TextMeshProUGUI HealthRegen;
    [SerializeField] private TextMeshProUGUI Armor;
    [SerializeField] private TextMeshProUGUI DamageRedaction;
    [SerializeField] private TextMeshProUGUI Income;
    
    [SerializeField] private TextMeshProUGUI HealthMultiplayer;
    [SerializeField] private TextMeshProUGUI DamageMultiplayer;
    [SerializeField] private TextMeshProUGUI AttackRiteMultiplayer;
    [SerializeField] private TextMeshProUGUI RewardMultiplayer;
    [SerializeField] private TextMeshProUGUI SpawnTimeMultiplayer;

    [SerializeField] private GameObject StatsShow;
    [SerializeField] private GameObject StatsBackground;
    private void Start()
    {
        StatsShow.SetActive(false);
        StatsBackground.SetActive(false);
    }

    public void ShowStats()
    {
        bool isShow = !StatsShow.activeSelf;
        StatsShow.SetActive(isShow);
        StatsBackground.SetActive(isShow);
        if (isShow)
        {
            StartCoroutine(UpdateStats());
        }
        else
        {
            StopAllCoroutines();
        }

    }
    private IEnumerator UpdateStats()
    {
        while (true)
        {
            UpdateBaseStats();
            UpdateEnemyMultiplayer();
            yield return new WaitForSeconds(0.1f);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void UpdateBaseStats()
    {
        var BaseStats = CurentBaseView.GetAllBaseStats();
        HealthRegen.text = BaseStats.HealthRegen.ToString(CultureInfo.InvariantCulture);
        Armor.text = BaseStats.Armor.ToString(CultureInfo.InvariantCulture);
        DamageRedaction.text = (Math.Round(CurentBaseView.GetCurrentDamageRedaction(), 2) * 100).ToString(CultureInfo.InvariantCulture) + " %";
        Income.text = (BaseStats.Income * 60).ToString("0");
    }

    private void UpdateEnemyMultiplayer()
    {
        var healthMult = FormatEnemyStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Health));
        var damageMult = FormatEnemyStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Damage));
        var attackRiteMult = FormatEnemyStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.AttackRate));
        var rewardMult = FormatEnemyStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Reward));
        var spawnTimeMult = FormatEnemyStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.SpawnTime));
        
        HealthMultiplayer.text = healthMult.ToString(CultureInfo.InvariantCulture) + " %";
        DamageMultiplayer.text = damageMult.ToString(CultureInfo.InvariantCulture) + " %";
        AttackRiteMultiplayer.text = attackRiteMult.ToString(CultureInfo.InvariantCulture) + " %";
        RewardMultiplayer.text = rewardMult.ToString(CultureInfo.InvariantCulture) + " %";
        SpawnTimeMultiplayer.text = spawnTimeMult.ToString(CultureInfo.InvariantCulture) + " %";
    }

    private float FormatEnemyStats(float value)
    {
        return (float)Math.Round((value - 1), 2) * 100f;
    }
}
