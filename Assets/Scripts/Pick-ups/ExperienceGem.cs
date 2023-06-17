using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup, ICollectible
{
    public int experienceGranted;
    //public GameObject Sound;
    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        //Debug.Log(GetComponent<AudioSource>() == null);
        //GetComponent<AudioSource>().Play();
        player.IncreaseExperience(experienceGranted);
    }
}
