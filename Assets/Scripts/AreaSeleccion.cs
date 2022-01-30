using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AreaSeleccion : MonoBehaviour
{
    public GameObject player;
    private GameObject[] objetosAIgnorar;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //desactivar selector
        this.gameObject.SetActive(false);
        transform.position = player.transform.position;
    }

    private void OnEnable() {
        GameObject player= GameObject.FindGameObjectWithTag("Player");
        objetosAIgnorar = GameObject.FindGameObjectsWithTag("Mundo");
        foreach (GameObject obj in objetosAIgnorar) {
            //if (obj.tag != "enemy") {
                Debug.Log("Hola" + obj.name+"aaaa");
                Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            //}

        }
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        /*
        if (Input.GetKeyDown(KeyCode.E)) {
            this.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0)) {
                seleccionar();
                this.gameObject.SetActive(false);
            }
        }
        */
    }

    
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.name);
    }
    private void OnMouseUp() {
        //if (Input.mousePosition. < transform.localScale.x + transform.position.x && Input.mousePosition.x > transform.localScale.x - transform.position.x) {
        //RaycastHit ray = cam.
        //seleccionar();

    }
    public GameObject seleccionar() {
        Collider2D[] area=Physics2D.OverlapCircleAll(transform.position, transform.GetComponent<CircleCollider2D>().radius*transform.localScale.x);
        bool encontrado = false;
        GameObject enemigo=this.gameObject;
        for(int i=0; i < area.Length && !encontrado; i++) {
            if (area[i].tag == "enemy") {
                //Debug.Log("enemigo");
                enemigo=area[i].gameObject;
                encontrado = true;
            }
        }
        return enemigo;
    }
    public void finAnimacionArea() {

    }
}
