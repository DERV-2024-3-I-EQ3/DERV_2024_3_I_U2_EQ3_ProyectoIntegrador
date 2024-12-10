using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BalonMrJosh : MonoBehaviour
{
    [SerializeField] private GameObject rayo;
    [SerializeField] public Transform inicioRayo;
    [SerializeField] private float tiempoRayo = 1f;
    [SerializeField] public float fuerza;
    private GameObject Instance;
    public float MaxLength;
    private new Rigidbody rigidbody;

    [SerializeField] GameObject escudo1;
    [SerializeField] GameObject escudo2;
    [SerializeField] GameObject escudo3;
    [SerializeField] Image escudo1IMG;
    [SerializeField] Image escudo2IMG;
    [SerializeField] Image escudo3IMG;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        instanciarRayo();
        if (Instance != null)
        {
            Instance.transform.position = inicioRayo.transform.position;
            Instance.transform.LookAt(transform);
        }

        RaycastHit hit;
        Vector3 direccion = (transform.position - inicioRayo.transform.position).normalized;
        if (Physics.Raycast(inicioRayo.transform.position, direccion, out hit, MaxLength))
        {
            direccion = (hit.point - inicioRayo.transform.position).normalized;
            Instance.transform.forward = direccion;
        }
        else
        {
            Instance.transform.forward = direccion;
        }

        if (0f < fuerza && fuerza < 100f)
        {
            rigidbody.AddForce(transform.right * -1 * fuerza / 100, ForceMode.Impulse);
        }
        else if (fuerza < 0)
        {
            fuerza = 0;
        }
        else if (fuerza > 100)
        {
            fuerza = 100;
        }

        float procentajeFuerza = (fuerza * 100) / 50;
        if (procentajeFuerza < 2)
        {
            escudo3.SetActive(false);
            Destroy(escudo3IMG);
        }
        else if (procentajeFuerza < 50)
        {
            escudo2.SetActive(false);
            Destroy(escudo2IMG);
        }
        else if (procentajeFuerza < 75)
        {
            escudo1.SetActive(false);
            Destroy(escudo1IMG);
        }
    }

    private void instanciarRayo()
    {
        if (Instance == null && inicioRayo != null)
        {
            Destroy(Instance);
            Instance = Instantiate(rayo, inicioRayo.transform.position, inicioRayo.transform.rotation);
            Instance.transform.parent = transform;
        }
    }

    public void aumentarFuerza(float fuerza)
    {
        this.fuerza += fuerza;
    }

    public void disminuirFuerza(float fuerza)
    {
        this.fuerza = Mathf.Max(this.fuerza - fuerza, 0); // Asegura que no sea negativa
    }

    public void setFuerza(float fuerza)
    {
        this.fuerza = fuerza;
    }

    public float getfuerza()
    {
        return this.fuerza;
    }
}
