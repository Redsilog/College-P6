using UnityEngine;
using TMPro;

public class Canvas : MonoBehaviour
{
    [SerializeField] private TextMeshPro bballff, sballff, bballbounce, sballbounce;
    private Formula bballFormula, sballFormula;

    void Start()
    {
        bballFormula = GameObject.Find("Basket Ball").GetComponent<Formula>();
        sballFormula = GameObject.Find("Soccer Ball").GetComponent<Formula>();
    }

    void Update()
    {
        if (bballFormula != null && bballFormula.hasLanded)
        {
            bballff.text = bballFormula.fallTime.ToString("F2") + "s";
            bballbounce.text =  bballFormula.firstBounce.ToString("F2") + "m";
        }

        if (sballFormula != null && sballFormula.hasLanded)
        {
            sballff.text = sballFormula.fallTime.ToString("F2") + "s";
            sballbounce.text = sballFormula.firstBounce.ToString("F2") + "m";
        }
    }
}
