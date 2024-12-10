using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level04_Controller : MonoBehaviour
{
    [System.Serializable]
    public struct key
    {
        public KeyCode keyCode;
        public Sprite sprite;
    }
    [SerializeField] private Image imagenkey;
    [SerializeField] private List<key> keys;
    GameObject balon;
    BalonPablo balonPablo;
    BalonMrJosh balonMrJosh;

    public Slider sliderFuerza;
    public TextMeshProUGUI procentajePablo;
    public TextMeshProUGUI procentajeMrJosh;


    KeyCode currentKey;
    int index;
    private float fuerzaGlobal = 100f;
    private float fuerzaPablo;
    private float fuerzaMrJosh;

    void Awake()
    {
        balon = GameObject.Find("Balon N4");
    }

    void Start()
    {
        balonPablo = balon.GetComponent<BalonPablo>();
        balonMrJosh = balon.GetComponent<BalonMrJosh>();

        // Inicializar fuerza de cada balón en 50 para que suma total sea 100
        balonPablo.setFuerza(fuerzaGlobal / 2);
        balonMrJosh.setFuerza(fuerzaGlobal / 2);

        // Configurar el rango del slider de fuerza
        sliderFuerza.minValue = -fuerzaGlobal;
        sliderFuerza.maxValue = fuerzaGlobal;
        sliderFuerza.value = 0;  // Comienza en el punto medio

        StartCoroutine(GenerarFuerzaNPC());
        StartCoroutine(CambiarTeclaJugador());
    }

    void Update()
    {
        if (Input.GetKeyDown(currentKey))
        {
            AumentarFuerzaPablo(0.2f); // Aumento en décimas
        }

        fuerzaMrJosh = balonMrJosh.fuerza;
        fuerzaPablo = balonPablo.fuerza;

        procentajeMrJosh.text = System.Math.Round(fuerzaMrJosh, 2).ToString();
        procentajePablo.text = System.Math.Round(fuerzaPablo, 2).ToString();


        // Calcular el valor del slider en función de la diferencia de fuerzas
        sliderFuerza.value = fuerzaPablo - fuerzaMrJosh;
    }

    private IEnumerator GenerarFuerzaNPC()
    {
        while (true)
        {
            float fuerza = Random.Range(0.3f, 0.4f);  // Cambios en décimas
            AumentarFuerzaMrJosh(fuerza);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator CambiarTeclaJugador()
    {
        while (true)
        {
            index = Random.Range(0, keys.Count);
            currentKey = keys[index].keyCode;
            imagenkey.sprite = keys[index].sprite;
            Debug.Log("Presiona la tecla: " + currentKey);

            yield return new WaitForSeconds(2f);
        }
    }

    void AumentarFuerzaPablo(float fuerza)
    {
        if (balonPablo.getfuerza() + fuerza <= fuerzaGlobal)
        {
            balonPablo.aumentarFuerza(fuerza);
            balonMrJosh.disminuirFuerza(fuerza);
        }
    }

    void AumentarFuerzaMrJosh(float fuerza)
    {
        if (balonMrJosh.getfuerza() + fuerza <= fuerzaGlobal)
        {
            balonMrJosh.aumentarFuerza(fuerza);
            balonPablo.disminuirFuerza(fuerza);
        }
    }
}
