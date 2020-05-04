using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Flash());
        
    }

    private IEnumerator Flash()
    {
        Color color = image.color;
        for (int i = 0; i < 50; i++)
        {
            color.a = 0.5f + i * 0.01f;
            image.color = color;

            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 20; i++)
        {
            color.a = 1 - i * 0.05f;
            image.color = color;

            yield return null;
        }
        image.gameObject.SetActive(false);
    }
}
