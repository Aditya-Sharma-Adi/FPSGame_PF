using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class PlayerHealth : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnHealthChange))]

    byte currentHealt { get; set; }

    [Networked(OnChanged = nameof(OnStateChange))]

    public bool isDead { get; set; }

    bool isInitialized;

    const byte maxHealth = 5;

    float lastHealth = 5;

    [SerializeField] Image healthBar;
    [SerializeField] HitboxRoot hitBoxRoot;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] CharacterController characterController;
    [SerializeField] NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustoms;
    [SerializeField] SpectatePlayer spectatePlayer;

    private void Start()
    {
        isDead = false;
        currentHealt = maxHealth;
        healthBar.fillAmount = 1;
        isInitialized = true;
    }


    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealt -= (byte)damage;

        Debug.Log("Enemy Left Health..." + currentHealt);

        if (currentHealt <= 0)
        {
            Debug.Log("Player Dead ..." + currentHealt);
            isDead = true;
        }

    }

    static void OnHealthChange(Changed<PlayerHealth> changed)
    {
        Debug.Log($"On HealthChanged value {changed.Behaviour.currentHealt}");

        byte newHealth = changed.Behaviour.currentHealt;

        changed.LoadOld();

        byte oldHealth = changed.Behaviour.currentHealt;

        if (newHealth < oldHealth)
            changed.Behaviour.OnHealthReduced(newHealth);

    }

    void OnHealthReduced(float _health)
    {
        if (!isInitialized)
            return;

        Debug.Log("player Get health reduceeeee.. .. ... . ." + _health);

        if (Object.HasInputAuthority)
        {
            healthBar.fillAmount = _health / maxHealth;
        }

        lastHealth = _health;
    }

    static void OnStateChange(Changed<PlayerHealth> changed)
    {
        Debug.Log($"On State Changed {changed.Behaviour.isDead}");

        bool isPlayerDead = changed.Behaviour.isDead;

        changed.LoadOld();

        bool isOldPlayerDead = changed.Behaviour.isDead;

        if (isPlayerDead)
            changed.Behaviour.OnDeath();
        else if (!isPlayerDead && isOldPlayerDead)
            changed.Behaviour.OnSpectate();

    }


    void OnDeath()
    {
        Debug.Log("Player Dead");
        hitBoxRoot.HitboxRootActive = false;
        playerModel.SetActive(false);
        characterController.enabled = false;
        healthCanvas.SetActive(false);
        networkCharacterControllerPrototypeCustoms.enabled = false;
        if (Object.HasInputAuthority)
            spectatePlayer.CurrentSpectatePlayer(0, 1);
    }

    void OnSpectate()
    {
        Debug.Log("Player spectate starttttttttttttttttttt ...");
        //  if (Object.HasInputAuthority)
        //   spectatePlayer.CurrentSpectatePlayer(0, 1);
    }

    public void FetchLocalHealth()
    {
        healthBar.fillAmount = lastHealth / maxHealth;
    }
}
