using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Conection : MonoBehaviour
{
    private void Start()
    {        
        if (GameManager.instance.estaSiendoServer) NetworkManager.Singleton.StartHost();
        else NetworkManager.Singleton.StartClient();
    }
}
