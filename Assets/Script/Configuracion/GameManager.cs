/*
CREDITOS
- Musica de fondo: "Night Runner (Chiptune Edit)" - Lightyeartraxx
- Sprites de enemigos y mazmorra: DungeonTilesetII - 0x72
(Los demas recursos del juego se pueden agregar en la lista "creditosExtra" desde el Inspector.)
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject jugador;
    public AudioSource musicaFondo;   // si lo dejas vacío, busca el objeto "Musica" solo

    [Header("Pantalla de inicio")]
    public string tituloJuego = "JUEGO 2D";
    public string[] creditosExtra;    // lineas de credito adicionales (opcional)

    // true despues de la primera partida: al reiniciar no vuelve a salir la pantalla de inicio
    private static bool inicioMostrado = false;
    private bool iniciado = false;

    private int puntaje = 0;
    private int record;

    private bool juegoTerminado = false;
    private float tiempoFin;
    private GUIStyle estiloTitulo;
    private GUIStyle estiloTexto;
    private GUIStyle estiloPuntaje;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        iniciado = inicioMostrado;
        Time.timeScale = iniciado ? 1f : 0f;
        record = PlayerPrefs.GetInt("Record", 0);
        if (jugador == null) jugador = GameObject.FindWithTag("Player");

        if (musicaFondo == null)
        {
            GameObject m = GameObject.Find("Musica");
            if (m != null) musicaFondo = m.GetComponent<AudioSource>();
        }
    }

    public void SumarPuntos(int cantidad)
    {
        if (juegoTerminado) return;
        puntaje += cantidad;

        if (AudioManager.Instance != null)
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoPuntaje);
    }

    void Update()
    {
        // Pantalla de inicio: espera una tecla para comenzar
        if (!iniciado)
        {
            // solo teclado (el clic se reserva para lanzar hechizos)
            if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && Time.unscaledTime > 0.3f)
            {
                iniciado = true;
                inicioMostrado = true;
                Time.timeScale = 1f;
            }
            return;
        }

        if (!juegoTerminado)
        {
            if (jugador == null)
            {
                TerminarJuego();
                return;
            }

            Camera cam = Camera.main;
            float bordeInferior = cam.transform.position.y - cam.orthographicSize - 1f;
            if (jugador.transform.position.y < bordeInferior)
            {
                TerminarJuego();
            }
        }
        else
        {
            if (Time.unscaledTime > tiempoFin + 0.7f && Input.anyKeyDown)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void TerminarJuego()
    {
        juegoTerminado = true;
        tiempoFin = Time.unscaledTime;
        Time.timeScale = 0f;

        if (musicaFondo != null) musicaFondo.Pause();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoMuerteJugador);
            AudioManager.Instance.Reproducir(AudioManager.Instance.sonidoGameOver);
        }

        if (puntaje > record)
        {
            record = puntaje;
            PlayerPrefs.SetInt("Record", record);
            PlayerPrefs.Save();
        }
    }

    void CrearEstilos()
    {
        estiloTitulo = new GUIStyle(GUI.skin.label);
        estiloTitulo.alignment = TextAnchor.MiddleCenter;
        estiloTitulo.fontSize = 64;
        estiloTitulo.fontStyle = FontStyle.Bold;
        estiloTitulo.normal.textColor = new Color(1f, 0.3f, 0.3f);

        estiloTexto = new GUIStyle(estiloTitulo);
        estiloTexto.fontSize = 28;
        estiloTexto.fontStyle = FontStyle.Normal;
        estiloTexto.normal.textColor = Color.white;

        estiloPuntaje = new GUIStyle(estiloTexto);
        estiloPuntaje.alignment = TextAnchor.UpperLeft;
        estiloPuntaje.fontSize = 32;
        estiloPuntaje.fontStyle = FontStyle.Bold;
        estiloPuntaje.normal.textColor = Color.yellow;
    }

    void DibujarInicio()
    {
        Color anterior = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.8f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = anterior;

        GUIStyle titulo = new GUIStyle(estiloTitulo);
        titulo.normal.textColor = new Color(1f, 0.85f, 0.3f);

        GUIStyle pequeno = new GUIStyle(estiloTexto);
        pequeno.fontSize = 20;
        pequeno.normal.textColor = new Color(0.8f, 0.8f, 0.8f);

        float y = Screen.height * 0.12f;
        GUI.Label(new Rect(0, y, Screen.width, 100), tituloJuego, titulo);

        GUI.Label(new Rect(0, y + 120, Screen.width, 40), "A / D o Flechas: moverse     Shift: correr", estiloTexto);
        GUI.Label(new Rect(0, y + 160, Screen.width, 40), "Espacio: saltar     Clic: lanzar hechizo", estiloTexto);
        GUI.Label(new Rect(0, y + 215, Screen.width, 40), "Evita a los enemigos y la lava: un toque y pierdes", estiloTexto);

        // parpadea para llamar la atencion
        if (Mathf.Repeat(Time.unscaledTime, 1f) < 0.7f)
        {
            GUIStyle empezar = new GUIStyle(estiloTexto);
            empezar.fontSize = 34;
            empezar.fontStyle = FontStyle.Bold;
            empezar.normal.textColor = Color.white;
            GUI.Label(new Rect(0, y + 285, Screen.width, 50), "Presiona una tecla para comenzar", empezar);
        }

        // creditos
        float yc = Screen.height - 120f;
        GUI.Label(new Rect(0, yc, Screen.width, 28), "Música: \"Night Runner (Chiptune Edit)\" - Lightyeartraxx", pequeno);
        GUI.Label(new Rect(0, yc + 26, Screen.width, 28), "Sprites: DungeonTilesetII - 0x72", pequeno);
        if (creditosExtra != null)
        {
            for (int i = 0; i < creditosExtra.Length; i++)
                GUI.Label(new Rect(0, yc + 52 + i * 26, Screen.width, 28), creditosExtra[i], pequeno);
        }
    }

    void OnGUI()
    {
        if (estiloTitulo == null) CrearEstilos();

        // Pantalla de inicio
        if (!iniciado)
        {
            DibujarInicio();
            return;
        }

        // Puntaje durante el juego
        if (!juegoTerminado)
        {
            GUI.Label(new Rect(20, 15, 400, 50), "Puntos: " + puntaje, estiloPuntaje);
            return;
        }

        // Pantalla de fin
        Color anterior = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.75f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = anterior;

        float y = Screen.height * 0.25f;
        GUI.Label(new Rect(0, y, Screen.width, 100), "FIN DEL JUEGO", estiloTitulo);
        GUI.Label(new Rect(0, y + 110, Screen.width, 50), "Puntos: " + puntaje, estiloTexto);
        GUI.Label(new Rect(0, y + 155, Screen.width, 50), "Récord: " + record, estiloTexto);
        GUI.Label(new Rect(0, y + 220, Screen.width, 50), "Presiona cualquier tecla para volver a jugar", estiloTexto);
    }
}