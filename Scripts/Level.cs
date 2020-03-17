using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level :MonoBehaviour
{

    public int level;
    private void Update()
    {
        if (level == 0)
            level = PlayerPrefs.GetInt("Level");
    }



}
