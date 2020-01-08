using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget; // La variable pour la position de teleportation
    public GameObject thePlayer; // La variable pour la teleportation

    void OnTriggerEnter(Collider other) //Setting le trigger de teleportation
    {
        thePlayer.transform.position = teleportTarget.transform.position;
    }
}
