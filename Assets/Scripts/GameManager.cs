using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int[] scores = { 1, 4, 3, 2 };

    [SerializeField] private AudioClip[] sounds;

    [SerializeField] private AudioClip[] destroySounds;

    public static GameManager instance { get; private set; }

    private PrimitiveType primitiveToPlace;

    Vector3 _nextShapePreviewPos = new Vector3(0, 1, 10);
    GameObject _previewObject;

    [SerializeField] private GameObject scoreTextGameObject;
    [SerializeField] private GameObject timerTextGameObject;

    private float _timer = 15;
    private TextMeshPro _scoreText;
    private TextMeshPro _timerText;

    private int shapeScore;
    private AudioClip shapeClip;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sounds = new AudioClip[7];
        sounds[0] = Resources.Load<AudioClip>("Sounds/Hit_Hurt2");
        sounds[1] = Resources.Load<AudioClip>("Sounds/Hit_Hurt3");
        sounds[2] = Resources.Load<AudioClip>("Sounds/Hit_Hurt4");
        sounds[3] = Resources.Load<AudioClip>("Sounds/Hit_Hurt5");
        sounds[4] = Resources.Load<AudioClip>("Sounds/Hit_Hurt6");
        sounds[5] = Resources.Load<AudioClip>("Sounds/Hit_Hurt7");
        sounds[6] = Resources.Load<AudioClip>("Sounds/Hit_Hurt8");

        destroySounds = new AudioClip[3];
        destroySounds[0] = Resources.Load<AudioClip>("Sounds/Explosion");
        destroySounds[1] = Resources.Load<AudioClip>("Sounds/Explosion2");
        destroySounds[2] = Resources.Load<AudioClip>("Sounds/Explosion3");
    }

    private void Start()
    {
        _scoreText = scoreTextGameObject.GetComponent<TextMeshPro>();
        _timerText = timerTextGameObject.GetComponent<TextMeshPro>();
        GenerateNextShape();
    }

    private void GenerateNextShape()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                primitiveToPlace = PrimitiveType.Cube;
                shapeScore = scores[0];
                break;
            case 1:
                primitiveToPlace = PrimitiveType.Sphere;
                shapeScore = scores[1];
                break;
            case 2:
                primitiveToPlace = PrimitiveType.Capsule;
                shapeScore = scores[2];
                break;
            case 3:
                primitiveToPlace = PrimitiveType.Cylinder;
                shapeScore = scores[3];
                break;
            default:
                primitiveToPlace = PrimitiveType.Cube;
                shapeScore = scores[0];
                break;
        }

        if (_previewObject) Destroy(_previewObject);

        _previewObject = GameObject.CreatePrimitive(primitiveToPlace);
        _previewObject.name = "Preview Shape";
        _previewObject.transform.position = _nextShapePreviewPos;
        _previewObject.transform.rotation = Random.rotation;

        shapeClip = sounds[Random.Range(0, sounds.Length)];

        Texture2D texture = Resources.Load<Texture2D>("Textures/wood_texture2");
        Color randomColor = Random.ColorHSV();
        float H, S, V;
        Color.RGBToHSV(randomColor, out H, out S, out V);

        S = 1f;
        V = 1f;

        MeshRenderer meshRenderer = _previewObject.GetComponent<MeshRenderer>();

        meshRenderer.material.color = Color.HSVToRGB(H, S, V);
        meshRenderer.material.mainTexture = texture;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        setTimer();
        if (_timer <= 0)
        {
            enabled = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject go = Instantiate(_previewObject);
                go.transform.localScale = Vector3.one * 0.3f;
                go.transform.position = hit.point + Vector3.up;
                go.transform.rotation = Random.rotation;

                AudioSource audioSource = go.AddComponent<AudioSource>();

                go.AddComponent<Rigidbody>();
                DestroyOnFall de = go.AddComponent<DestroyOnFall>();
                de.DestroySound = destroySounds[Random.Range(0, destroySounds.Length)];
                go.AddComponent<DragWithMouse>();

                Block block = go.AddComponent<Block>();

                block.Score = shapeScore;
                audioSource.playOnAwake = false;
                audioSource.clip = shapeClip;

                GenerateNextShape();
            }
        }
    }

    private void setTimer()
    {
        _timerText.SetText($"Remaining Time: {(int)Mathf.Ceil(_timer)}");
    }

    public void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
