using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    [SerializeField] Transform playerModel;
    [SerializeField] GameObject[] enableObject;
    public SpectatePlayer spectatePlayer;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            // Utils.SetRenderLayerinChildren(playerModel, LayerMask.NameToLayer("Ignore_Layer"));
            foreach (GameObject item in enableObject)
            {
                item.SetActive(true);
            }
            Debug.Log("Spawned Local Player..");
        }
        else
        {
            Debug.Log("Spawned remote Playerrrr...." + gameObject.name);
            SpectateManager.instance.SetSpectatePlayer(gameObject.GetComponent<NetworkPlayer>().spectatePlayer);
        }
        transform.name = $"Player_{Object.Id}";
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }

}
