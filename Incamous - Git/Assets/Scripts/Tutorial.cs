using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text tutorialText;
    [SerializeField] private string[] tutorialStages;
    [SerializeField] private GameObject checkPoint;

    private GameObject player;
    private PlayerMovement playerMovement;
    private PickUp pickUp;
    private Inventory inventory;
    private int currentIndx = 0;
    private int newIndx = 0;
    private int dummiesShot = 0;
    private bool[] isStageDone = new bool[15];

    public int DummiesShot
    {
        get { return dummiesShot; }
        set { dummiesShot = value; }
    }

    void Awake()
    {
        for(int i = 0; i < isStageDone.Length; i++)
            isStageDone[i] = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = tutorialStages[0];
        
        player = GameManager.Instance.Player;

        playerMovement = player.GetComponent<PlayerMovement>();
        pickUp = player.GetComponent<PickUp>();
        inventory = player.GetComponent<Inventory>();

        playerMovement.enabled = false;
        pickUp.enabled = false;
        inventory.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialText.text == tutorialStages[0] || tutorialText.text == tutorialStages[1] || tutorialText.text == tutorialStages[2] || tutorialText.text == tutorialStages[4])
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                for(int i = 0; i < tutorialStages.Length; i++)
                {
                    if (tutorialText.text == tutorialStages[i])
                    {
                        currentIndx = i;
                        Debug.Log(i);

                        newIndx = currentIndx + 1;
                        Debug.Log(newIndx);

                        currentIndx = newIndx;
                    }
                }

                tutorialText.text = tutorialStages[currentIndx];
            }
        }

        if (tutorialText.text == tutorialStages[3] && !playerMovement.enabled)
            playerMovement.enabled = true;

        if (player.transform.position.z >= checkPoint.transform.position.z && !isStageDone[0])
        {   
            newIndx++;
            currentIndx = newIndx;

            playerMovement.enabled = false;
            tutorialText.text = tutorialStages[currentIndx];

            isStageDone[0] = true;
        }

        if (tutorialText.text == tutorialStages[5] && !playerMovement.enabled)
        {
            playerMovement.enabled = true;
            pickUp.enabled = true;
            inventory.enabled = true;
        }

        if (pickUp.ActiveWeapon != null && !isStageDone[1])
        {
            newIndx++;
            currentIndx = newIndx;

            tutorialText.text = tutorialStages[currentIndx];

            isStageDone[1] = true;
        }

        if (tutorialText.text == tutorialStages[6] && !isStageDone[2])
        {
            if (DummiesShot > 0 && pickUp.ActiveWeapon.GetComponent<Weapon>().MagAmmo == 0)
            {
                newIndx++;
                currentIndx = newIndx;

                tutorialText.text = tutorialStages[currentIndx];
                isStageDone[2] = true;
            }
        }
    }
}
