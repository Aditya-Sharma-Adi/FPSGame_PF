using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateManager : MonoBehaviour
{
    public static SpectateManager instance;

    public List<SpectatePlayer> spectatePlayers;

    private void Awake()
    {
        instance = this;
    }

    public void SetSpectatePlayer(SpectatePlayer splayer)
    {
        spectatePlayers.Add(splayer);
    }

}
