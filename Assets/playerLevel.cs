using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLevel : MonoBehaviour
{
    public int p_lvl;
    public int p_exp;

    public int p_updateNeed;

    private void Start() {
        p_updateNeed = 100 * 2^(p_lvl-1);
    }

    private void LateUpdate() {
        if(p_exp == p_updateNeed)
        {
            p_lvl += 1;
            p_updateNeed = 100 * 2^(p_lvl-1);
        }
    }
}
