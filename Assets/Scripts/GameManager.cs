using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool estaSiendoServer;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);
    }
}