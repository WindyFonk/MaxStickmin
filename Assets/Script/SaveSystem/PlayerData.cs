using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int resource;
    public bool launchUnlocked;
    public bool dodgeUnlocked;
    public float energy;

    public PlayerData(PlayerController player)
    {
        health = player.health;
        launchUnlocked= player.launchUnlocked;
        dodgeUnlocked= player.dodgeUnlocked;
        energy= player.energy;
        resource= player.resource;
    }
}
