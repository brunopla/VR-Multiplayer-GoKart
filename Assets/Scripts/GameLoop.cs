using Unity.Netcode;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class GameLoop : NetworkBehaviour
{
    [SerializeField] List<Transform> posiciones;
    public Dictionary<int, int> posicionesPartida = new Dictionary<int, int>(); 
    public NetworkVariable<bool> partidaTerminada = new NetworkVariable<bool>(false,readPerm: NetworkVariableReadPermission.Everyone,writePerm: NetworkVariableWritePermission.Server);
    public bool comenzar;
    /// <summary>
    /// Flujo desde que parte el host, se espera hasta comenzar
    /// </summary>
    /// <returns></returns>
   IEnumerator Start()
   {
        if(!IsHost ) yield break;
        yield return new WaitUntil(() => comenzar);
        PosicionarJugadoresServerRpc();
        print("Cantidad de players : " + NetworkManager.Singleton.ConnectedClientsIds);


        for( int i = 0;i<3;i++ )
        {
            yield return new WaitForSeconds(1);
            UpdateContadoresClientRPC(i);
        }
        for (int i = 0; i < NetworkManager.ConnectedClientsIds.Count; i++) posicionesPartida.Add(i, 0);
        while(partidaTerminada.Value == false) 
        {
            yield return new WaitForSeconds(1);
            print("Game update loop");
        }
        print("partida terminada");
        yield break;
   }

    /// <summary>
    /// Posiciona a los jugadores al comenzar la partida en la lista posiciones.
    /// </summary>
   [ServerRpc(RequireOwnership = false)]
   public void PosicionarJugadoresServerRpc()
   {
        var players = GameObject.FindGameObjectsWithTag("Player").ToList();
        players.ForEach(p =>
        {
            var pos = posiciones.Where(pos => pos.transform.childCount== 0).First(); 
            p.transform.position = pos.position;
            p.transform.rotation = UnityEngine.Quaternion.Euler(0, 90, 0);
        } );
 
   }
    /// <summary>
    /// Actualiza la posicion de los jugadores tras una cantidad de segundos, buscando el game object "feedText" y haciendo get component a su tmp_text
    /// </summary>
    /// <param name="segundos"></param>
    [ClientRpc] void UpdateContadoresClientRPC(int segundos)
    {
        //poner ui del auto 
        TMP_Text tMP_Text = GameObject.FindGameObjectsWithTag("feedText").First().GetComponent<TMP_Text>();
        tMP_Text.text = $" Player n:{NetworkManager.Singleton.LocalClientId} posicion: {posicionesPartida[(int)NetworkManager.LocalClientId]}";
        //
    }
}