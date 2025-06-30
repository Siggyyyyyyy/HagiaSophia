using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DotNavigation : MonoBehaviour
{
    public GameObject dotPrefab; // Dein Punkt-Prefab
    public Transform dotContainer; // Der UI-Container, z. B. ein Horizontal Layout
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private List<GameObject> dots = new List<GameObject>();

    public void CreateDots(int count)
    {
        // Alte Punkte löschen
        foreach (Transform child in dotContainer)
        {
            Destroy(child.gameObject);
        }
        dots.Clear();

      
        for (int i = 0; i < count; i++)
        {
            GameObject dot = Instantiate(dotPrefab, dotContainer);
            dots.Add(dot);
        }
    }

    public void SetActiveDot(int index)
    {
        for (int i = 0; i < dots.Count; i++)
        {
            Image img = dots[i].GetComponent<Image>();
            img.sprite = (i == index) ? activeSprite : inactiveSprite;
        }
    }
}
