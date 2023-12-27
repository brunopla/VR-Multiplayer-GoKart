using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Conection : MonoBehaviour
{
    private IEnumerator Start()
    {
		try
		{
			if (GameManager.instance.estaSiendoServer) NetworkManager.Singleton.StartHost();
			else NetworkManager.Singleton.StartClient();

		}
		catch (System.Exception)
		{
			yield break;
		}
		yield return new WaitUntil(() => GameManager.instance.partidaComenzada);
    }
}
