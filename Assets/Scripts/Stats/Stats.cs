using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Stats : MonoBehaviour
{
    [SerializeField] private BaseView currentBaseView;
    [SerializeField] private TextMeshProUGUI healthRegen;
    [SerializeField] private TextMeshProUGUI armor;
    [SerializeField] private TextMeshProUGUI damageRedaction;
    [SerializeField] private TextMeshProUGUI income;

    [SerializeField] private TextMeshProUGUI healthMultiplayer;
    [SerializeField] private TextMeshProUGUI damageMultiplayer;
    [SerializeField] private TextMeshProUGUI attackRiteMultiplayer;
    [SerializeField] private TextMeshProUGUI rewardMultiplayer;
    [SerializeField] private TextMeshProUGUI spawnTimeMultiplayer;

    
    [SerializeField] private TextMeshProUGUI damagePierceMultiplayer;
    [SerializeField] private TextMeshProUGUI attackRitePierceMultiplayer;
    
    [SerializeField] private GameObject statsShow;
    [SerializeField] private GameObject statsBackground;
    private void Start()
    {
        statsShow.SetActive(false);
        statsBackground.SetActive(false);
    }

    public void ShowStats()
    {
        bool isShow = !statsShow.activeSelf;
        statsShow.SetActive(isShow);
        statsBackground.SetActive(isShow);
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
            UpdatePierceWeaponStatMultiplayer();
            yield return new WaitForSeconds(0.1f);
        }

        // ReSharper disable once IteratorNeverReturns
    }


    private void UpdateBaseStats()
    {
        var baseStats = currentBaseView.GetAllBaseStats();
        healthRegen.text = baseStats.HealthRegen.ToString(CultureInfo.InvariantCulture);
        armor.text = baseStats.Armor.ToString(CultureInfo.InvariantCulture);
        damageRedaction.text =
            (Math.Round(currentBaseView.GetCurrentDamageRedaction(), 2) * 100).ToString(CultureInfo.InvariantCulture) +
            " %";
        income.text = (baseStats.Income * 60).ToString("0");
    }

    private void UpdateEnemyMultiplayer()
    {
        var healthMult = FormatStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Health));
        var damageMult = FormatStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Damage));
        var attackRiteMult = FormatStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.AttackRate));
        var rewardMult = FormatStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.Reward));
        var spawnTimeMult = FormatStats(EnemyStatsMultiplayer.GetMultiplayer(MultiplayerType.SpawnTime));

        healthMultiplayer.text = healthMult.ToString(CultureInfo.InvariantCulture) + " %";
        damageMultiplayer.text = damageMult.ToString(CultureInfo.InvariantCulture) + " %";
        attackRiteMultiplayer.text = attackRiteMult.ToString(CultureInfo.InvariantCulture) + " %";
        rewardMultiplayer.text = rewardMult.ToString(CultureInfo.InvariantCulture) + " %";
        spawnTimeMultiplayer.text = spawnTimeMult.ToString(CultureInfo.InvariantCulture) + " %";
    }

    private float FormatStats(float value)
    {
        return (float) Math.Round((value - 1), 2) * 100f;
    }
    
    
    private void UpdatePierceWeaponStatMultiplayer()
    {
        damagePierceMultiplayer.text = FormatStats(PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(MultiplayerType.Damage)) + " %";
        attackRitePierceMultiplayer.text = FormatStats(PierceWeaponStatsMultiplayer.Instance.GetMultiplayer(MultiplayerType.AttackRate)) + " %";
    }
}