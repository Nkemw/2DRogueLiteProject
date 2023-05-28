using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class VillageImagesChange : MonoBehaviour, ImageChanger
{
    [SerializeField] Image[] imgs;
    [SerializeField] Tilemap tilemap;

    public void ImageChange()
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            switch ((int)GameManager.Instance.time / 60 % 3)
            {
                case 0:
                    imgs[i].color = new Color(1f, 1f, 1f);
                    break;
                case 1:
                    imgs[i].color = new Color(210 / 255f, 90 / 255f, 90 / 255f);
                    break;
                default:
                    imgs[i].color = new Color(60 / 255f, 60 / 255f, 210 / 255f);
                    break;

            }
        }

        switch ((int)GameManager.Instance.time / 60 % 3)
        {
            case 0:
                tilemap.color = new Color(1f, 1f, 1f);
                break;
            case 1:
                tilemap.color = new Color(210 / 255f, 90 / 255f, 90 / 255f);
                break;
            default:
                tilemap.color = new Color(60 / 255f, 60 / 255f, 210 / 255f);
                break;

        }
    }

    void Update()
    {
        ImageChange();
    }
}
