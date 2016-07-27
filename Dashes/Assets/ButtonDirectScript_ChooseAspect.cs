using UnityEngine;
using System.Collections;

public class ButtonDirectScript_ChooseAspect : MonoBehaviour {

    public void ChooseAspect(int nr)
    {
        References.instance.ChooseAspect(nr);
    }
}
