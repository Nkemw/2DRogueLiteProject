using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleImagesChange : MonoBehaviour, ImageChanger
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Image[] imgs;

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
                sprite.color = new Color(1f, 1f, 1f);
                break;
            case 1:
                sprite.color = new Color(210 / 255f, 90 / 255f, 90 / 255f);
                break;
            default:
                sprite.color = new Color(60 / 255f, 60 / 255f, 210 / 255f);
                break;

        }
    }

    void Update()
    {
        ImageChange();
    }
}
