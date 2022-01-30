using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    private Vector3 offset;
    public GameObject player;
    private float minPosition = 0.2f;
    private float maxPosition = 65f;
    private float movimientoCamaraZ = 5.3f;

    // Start is called before the first frame update
    void Start() {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (player.transform.position.x > minPosition && player.transform.position.x < maxPosition) {
            transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
        }
        if (player.GetComponent<Player>().mundoFalso) {
            transform.position = new Vector3(transform.position.x, transform.position.y, +movimientoCamaraZ);
        }
        else {
            transform.position = new Vector3(transform.position.x, transform.position.y, -movimientoCamaraZ);
        }

    }
}
