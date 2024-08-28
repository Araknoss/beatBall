using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public void ActiveButton()
    {
        transform.LeanScale(Vector2.one, 1).setEaseOutBack();
    }

    public void DesactiveButton()
    {
        transform.LeanScale(Vector2.zero, 0);
        gameObject.SetActive(false);
    }


}
