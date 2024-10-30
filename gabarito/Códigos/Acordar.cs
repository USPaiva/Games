using UnityEngine;
public class Acordar : MonoBehaviour
{
    public GameObject Alvo;
    public float vel = 5f;
    public float distmin = 10f;
    void Start() { 
    
    }
    void Seguir()
    {
        if (Vector3.Distance(transform.position, Alvo.transform.position) < distmin) { 
            transform.LookAt(Alvo.transform.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self); 
            transform.Translate(new Vector3(vel * Time.deltaTime, 0, 0)); 
        }
    }
    void Update() {
        Seguir();
    }
}