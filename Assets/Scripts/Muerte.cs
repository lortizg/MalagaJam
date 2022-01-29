using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    private UnityEngine.GameObject mundoMuerte;
    private UnityEngine.GameObject mundo;
    // Start is called before the first frame update
    void Start()
    {
        mundoMuerte = GameObject.Find("Muerte");
        mundo= GameObject.Find("Tilemap");

        //mundoMuerte.transform.position = new Vector3(2*mundo.transform.position.x,mundo.transform.position.y, mundo.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
