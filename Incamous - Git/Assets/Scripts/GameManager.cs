using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private Text reloadingText;
    [SerializeField] private int bulletsToLoadInPool;
    [SerializeField] private int bulletImpactsToLoadInPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletImpactPrefab;
    [SerializeField] private GameObject bulletsPoolParent;
    [SerializeField] private GameObject bulletImpactsPoolParent;

    private List<GameObject> bulletsPool = new List<GameObject>();
    private List<GameObject> bulletImpactsPool = new List<GameObject>();

    private GameObject player;

    public GameObject Player
    {
        get { return player; }
    }

    public Text ReloadingText
    {
        get { return reloadingText; }
        set { reloadingText = value; }
    }

    public List<GameObject> BulletsPool
    {
        get { return bulletsPool; }
    }

    public List<GameObject> BulletImpactsPool
    {
        get { return bulletImpactsPool; }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < bulletsToLoadInPool; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletsPoolParent.transform) as GameObject;
            bullet.SetActive(false);
            bulletsPool.Add(bullet);
        }

        for (int i = 0; i < bulletImpactsToLoadInPool; i++)
        {
            GameObject bulletImpact = Instantiate(bulletImpactPrefab, bulletImpactsPoolParent.transform) as GameObject;
            bulletImpact.SetActive(false);
            bulletImpactsPool.Add(bulletImpact);
        }

        ReloadingText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
