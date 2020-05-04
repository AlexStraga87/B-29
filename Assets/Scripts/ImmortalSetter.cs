using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalSetter : MonoBehaviour
{
    [SerializeField] Destroyable _target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _target.SetImmortal();
        }
    }
}
