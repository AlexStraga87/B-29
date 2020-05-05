using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menu.SetActive(!_menu.activeSelf);
            if (_menu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void ContinueGame()
    {
        _menu.SetActive(false);
        Time.timeScale = 1;
    }
}
