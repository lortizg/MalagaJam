using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private float horizontal;
    private bool alreadyJumping=false;
    private float muertePosition;
    private bool mundoFalso = false;
    private GameObject[] objetosAIgnorarFalso;
    private GameObject[] objetosAIgnorarTrue;
    public GameObject mundo;
    public GameObject area;

    public float JumpForce=150;
    public float Speed=1;
    private bool noMoverse=false;
    private Vector3 posicionNoMoverse;
    private bool OnArea=false;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        muertePosition = GameObject.Find("Muerte").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f)) { alreadyJumping = false; }

        //giramos si vamos a la izquierda
        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        // Saltamos si pulsamos w
        if (Input.GetKeyDown(KeyCode.W) && !alreadyJumping)
        {
            Jump();
        }

        //Si pulsamos e atacamos
        if (Input.GetKeyDown(KeyCode.E)) {
            
            area.gameObject.SetActive(true);
            OnArea = true;
        }
        if (OnArea) {
            if (Input.GetMouseButtonDown(0)) {
                AreaSeleccion component = area.GetComponent<AreaSeleccion>();
                component.seleccionar();
                GameObject.Find("Area").gameObject.SetActive(false);
                noMoverse = true;
                posicionNoMoverse = transform.position;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Animator>().SetTrigger("activarAtaque");
                OnArea = false;
            }
        }
        if (noMoverse) {
            //transform.position = posicionNoMoverse;
        }
        // cambiar mapa
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (!mundoFalso)
            {
                cambiarMapa(GameObject.Find("Muerte").transform);
                mundoFalso = true;
                // enableamos colision con muerte
                foreach (GameObject obj in objetosAIgnorarTrue) {
                    Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);
                }
                // disableamos con mundo
                foreach (GameObject obj in objetosAIgnorarFalso) {
                    Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                
            }
            else
            {
                cambiarMapa(GameObject.Find("Tilemap").transform);
                mundoFalso = false;
                // enableamos colision con mundo
                foreach (GameObject obj in objetosAIgnorarFalso) {
                    Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }
                // disableamos con muerte
                foreach (GameObject obj in objetosAIgnorarTrue) {
                    Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
            }

        }
    }
    private void OnEnable() {
        objetosAIgnorarFalso = GameObject.FindGameObjectsWithTag("Mundo");
        objetosAIgnorarTrue = GameObject.FindGameObjectsWithTag("Muerte");
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log(mundoFalso);
        //Debug.Log(collision.gameObject.tag);
        if((collision.gameObject.tag=="Muerte" && !mundoFalso) || (collision.gameObject.tag == "Mundo" && mundoFalso)) {
            //Debug.Log("AAAAAAA");
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), collision.collider.GetComponent<Collider2D>());
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up*JumpForce);
        alreadyJumping = true;
    }
    private void cambiarMapa(Transform mundo) {
        //Debug.Log(mundo.transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, mundo.transform.position.z);
        
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity =new Vector2(horizontal*Speed,Rigidbody2D.velocity.y);
    }

    public void EndAnimation() 
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
