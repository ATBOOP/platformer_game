using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour{
    public static LevelManager instance;


    public Transform Respawnpoint;
    public GameObject playerPrefab;
    
    private void Awake() {
        instance = this;
    }

     public void Respawn () {
        Instantiate(playerPrefab, Respawnpoint.position, Quaternion.identity);
    }
}
