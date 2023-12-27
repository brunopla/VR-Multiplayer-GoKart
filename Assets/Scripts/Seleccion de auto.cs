using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Usar en la escena 0,antes de conectar
/// </summary>
public class Selecciondeauto : MonoBehaviour
{
    [SerializeField] GameObject prefabEnEscena;
    [SerializeField] List<GameObject> prefabsAutosDisponibles;
    public string nombreAutoSeleccionado;
    int i = 0;
    Vector3 startAutoPos;



    private void Awake()
    {
        startAutoPos = prefabEnEscena.transform.position;
        CambiarAuto();
    }
    /// <summary>
    /// cambia el auto 
    /// </summary>
    /// <returns> el nombre al cambiar </returns>
    public void CambiarAuto()
    {
        Destroy(prefabEnEscena);
        if(i >= prefabsAutosDisponibles.Count) i = 0;
        prefabEnEscena = Instantiate(prefabsAutosDisponibles[i]
            , startAutoPos, Quaternion.identity);
        nombreAutoSeleccionado = prefabEnEscena.name;
        i++;
    }
    /// <summary>
    /// tras conectar llamar a esta funcion para cuando se inicie escena 1 instanciar el auto correcto
    /// </summary>
    public void ConfirmarAuto()
    {
        GameManager.instance.nombrePrefabAuto  = nombreAutoSeleccionado;
    }
}
