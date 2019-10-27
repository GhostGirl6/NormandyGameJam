using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class MachineRecette : MonoBehaviour
{
    [SerializeField]
    private GameObject TextUi;
    [SerializeField]
    private Text FinalText;
    [SerializeField]
    private List<Image> caseList;
    [SerializeField]
    private Sprite check;
    [SerializeField]
    private Sprite cross;
    [SerializeField]
    private Sprite cercle;
    private int EtatActuel = 0;
    private bool IsGameOver = false;
    private enum m_ObjectType
    {
        Livre, 
        Fiole,
        Trousse,
        Eau
   
    }
    private List<bool> finalList;

    

    // Start is called before the first frame update
    void Start()
    {
        finalList = new List<bool>();
        finalList.Add(false);
        finalList.Add(false);
        finalList.Add(false);
        finalList.Add(false);
        FinalText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.tag)
        {
            case "Fiole":
                NouvelObjetEntrant(m_ObjectType.Fiole);
                removeObjectFromHands(other.gameObject);
                Destroy(other.gameObject);
                break;
            case "Eau":
                NouvelObjetEntrant(m_ObjectType.Eau);
                removeObjectFromHands(other.gameObject);
                Destroy(other.gameObject);
                break;
            case "Trousse":
                NouvelObjetEntrant(m_ObjectType.Trousse);
                removeObjectFromHands(other.gameObject);
                Destroy(other.gameObject);
                break;
            case "Livre":
                NouvelObjetEntrant(m_ObjectType.Livre);
                removeObjectFromHands(other.gameObject);
                Destroy(other.gameObject);
                break;
            case "Inutile":
                IsGameOver = true;
                finalList[EtatActuel] = false;
                removeObjectFromHands(other.gameObject);
                Destroy(other.gameObject);
                EtatActuel++;
                CheckVictory();
                break;
            default:
                break;
        }
        Debug.Log(EtatActuel);
        
    }

    private void removeObjectFromHands(GameObject go)
    {
        if (Player.instance.hands[0].ObjectIsAttached(go))
        {
            Player.instance.hands[0].DetachObject(go);
        }
        if (Player.instance.hands[1].ObjectIsAttached(go))
        {
            Player.instance.hands[1].DetachObject(go);
        }
    }

    /// <summary>
    /// Cette fonction utilise un objet entrant et va détecter si c'est un des objets attendus.
    /// </summary>
    /// <param name="NouvelObjet"> Un objet de la pièce </param>
    private void NouvelObjetEntrant( m_ObjectType NouvelObjet)
    {
        caseList[EtatActuel].sprite = cercle;
        Debug.Log(EtatActuel);
        switch ( EtatActuel)
        {
            case 0: 
                if(NouvelObjet != m_ObjectType.Livre )
                {
                    IsGameOver = true;
                    finalList[EtatActuel] = false;
                }
                else
                {
                    finalList[EtatActuel] = true;
                }
            break;
            case 1:
                if (NouvelObjet != m_ObjectType.Fiole )
                {
                    IsGameOver = true;
                    finalList[EtatActuel] = false; ;
                }
                else
                {
                    finalList[EtatActuel] = true;
                }
                break;
            case 2:
                if (NouvelObjet != m_ObjectType.Trousse)
                {
                    IsGameOver = true;
                    finalList[EtatActuel] = false;
                }
                else
                {
                    finalList[EtatActuel] = true;
                }
                break;
            case 3:
                if (NouvelObjet != m_ObjectType.Eau)
                {
                    IsGameOver = true;
                    finalList[EtatActuel] = false;
                }
                else
                {
                    finalList[EtatActuel] = true;
                }
                break;
        }
        EtatActuel++;
        CheckVictory();
    }
    private void CheckVictory()
    {
        int cpt = 0;
        if (EtatActuel < 4) { return; }
        if (IsGameOver)
        {
            print("Game Over");
            TextUi.SetActive(false);
            FinalText.text = "Game Over!";
            FinalText.color = Color.red;
        }
        else
        {
            print("Victory!");
            TextUi.SetActive(false);
            FinalText.text = "Victory!";
            FinalText.color = Color.green;
        }
        foreach (Image img in caseList)
        {
            if (finalList[cpt])
            {
                img.sprite = check;
            }
            else
            {
                img.sprite = cross;
            }
            cpt++;
        }

    }
}
