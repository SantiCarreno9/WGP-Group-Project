using Character;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;

public class UIHighlighter : MonoBehaviour
{
    [SerializeField] private Transform highlighter;
    [SerializeField] private Transform movement;
    [SerializeField] private Transform jumpBTN;
    [SerializeField] private Transform shootBTN;
    [SerializeField] private Transform runBTN;
    [SerializeField] private Transform inventoryBTN;
    [SerializeField] private Transform map;
    [SerializeField] private Transform adviceContainer;
    [SerializeField] private TextMeshProUGUI adviceText;
    [SerializeField] private Image thisImage;


    private int constant;

    private void Awake()
    {
        gameObject.GetComponent<Image>().enabled = false;
    }

    public void HighlightTheThing(ElementToHighlight element)
    {
        switch (element)
        {
            case ElementToHighlight.Movement:
                adviceText.text = "TOUCH AND DRAG JOYSTICK TO MOVE";
                MoveMeTo(movement);
                YoyoAnim();
                break;
            case ElementToHighlight.Run:
                adviceText.text = "TOUCH THIS BUTTON TO SPRINT";
                MoveMeTo(runBTN);
                YoyoAnim();
                break;
            case ElementToHighlight.Jump:
                adviceText.text = "TAP THIS BUTTON TO JUMP";
                MoveMeTo(jumpBTN);
                YoyoAnim();
                break;
            case ElementToHighlight.Shoot:
                adviceText.text = "TAP THIS BUTTON TO SHOOT";
                MoveMeTo(shootBTN);
                YoyoAnim();
                break;
            case ElementToHighlight.Inventory:
                adviceText.text = "THIS BUTTON TO OPEN INVENTORY";
                MoveMeTo(inventoryBTN);
                YoyoAnim();
                break;
            case ElementToHighlight.Map:
                adviceText.text = "THIS IS THE MINI MAP";
                MoveMeTo(map);
                YoyoAnim();
                break;
            default:
                break;
        }
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Image>().enabled = false;
        adviceText.text = "";
    }

    private void MoveMeTo(Transform spot) {
        gameObject.GetComponent<Image>().enabled = true;
        highlighter.position = spot.position;
    }
    private void YoyoAnim()
    {
        highlighter.localScale = new Vector3(1,1,1);
        highlighter.DOScale(3, 1).SetLoops(4, LoopType.Yoyo) ;
        adviceContainer.DOScale(2, 2).SetLoops(4, LoopType.Yoyo);
    }


}

public enum ElementToHighlight
{
    Movement,
    Run,
    Jump,
    Shoot,
    Inventory,
    Map,
}
