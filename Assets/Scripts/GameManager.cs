using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool estaSiendoServer,partidaComenzada;
    public string nombrePrefabAuto;
    public GameObject autoInstaciadoEnEscena;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);
    }

}