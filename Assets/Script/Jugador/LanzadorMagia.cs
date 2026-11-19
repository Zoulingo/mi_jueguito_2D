using UnityEngine;

public class LanzadorMagia : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public float cooldown = 0.5f;
    private float proximoDisparo;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= proximoDisparo)
        {
            proximoDisparo = Time.time + cooldown;
            Disparar();
        }
    }

    void Disparar()
    {
        Vector3 mouseMundo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = ((Vector2)mouseMundo - (Vector2)transform.position).normalized;

        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        GameObject obj = Instantiate(proyectilPrefab, transform.position, Quaternion.Euler(0, 0, angulo));
        obj.GetComponent<Proyectil>().direccion = direccion;

        if (AudioManager.Instance != null)
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoHechizo);
    }
}