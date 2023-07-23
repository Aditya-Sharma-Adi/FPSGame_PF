using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpectatePlayer : NetworkBehaviour
{


    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] PlayerHealth playerHealth;

    int currentCamIndex;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerHealth.isDead && Object.HasInputAuthority)
        {
            currentCamIndex++;
            if (currentCamIndex >= SpectateManager.instance.spectatePlayers.Count)
                currentCamIndex = 0;

            if (currentCamIndex == 0)
            {
                CurrentSpectatePlayer(currentCamIndex, SpectateManager.instance.spectatePlayers.Count - 1);
            }
            else
            {
                CurrentSpectatePlayer(currentCamIndex, currentCamIndex - 1);
            }
        }

    }

    public void CurrentSpectatePlayer(int enableIndex, int disableIndex)
    {
        SpectateManager.instance.spectatePlayers[disableIndex].DisableThisPlayerSpectate();
        SpectateManager.instance.spectatePlayers[enableIndex].EnableThisPlayerSpectate();
    }

    public void EnableThisPlayerSpectate()
    {
        playerCamera.SetActive(true);
        healthCanvas.SetActive(true);
        playerHealth.FetchLocalHealth();
    }

    public void DisableThisPlayerSpectate()
    {
        playerCamera.SetActive(false);
        healthCanvas.SetActive(false);
    }

}


