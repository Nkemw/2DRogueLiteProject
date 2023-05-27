using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float offset = 18.25f;
    [SerializeField] GameObject[] layer1_Img;
    [SerializeField] GameObject[] layer2_Img;
    [SerializeField] GameObject[] layer3_Img;
    [SerializeField] GameObject[] layer4_Img;
    [SerializeField] GameObject[] layer5_Img;
    [SerializeField] GameObject[] layer6_Img;

    private void Update()
    {
        ImageMove(layer1_Img, 1f);
        ImageMove(layer2_Img, 1f);
        ImageMove(layer3_Img, 0.5f);
        ImageMove(layer4_Img, 0.5f);
        ImageMove(layer5_Img, 0.1f);
        ImageMove(layer6_Img, 0.05f);
    }

    public void ImageMove(GameObject[] img, float speed)
    {
        img[0].transform.position = new Vector3(img[0].transform.position.x + -2f * Time.deltaTime * speed, 0f, 0f);
        img[1].transform.position = new Vector3(img[1].transform.position.x + -2f * Time.deltaTime * speed, 0f, 0f);

        if (img[0].transform.position.x <= -offset)
        {
            img[0].transform.position = new Vector3(img[1].transform.position.x + offset, 0f, 0f);
        }

        if (img[1].transform.position.x <= -offset)
        {
            img[1].transform.position = new Vector3(img[0].transform.position.x + offset, 0f, 0f);
        }
    }
}

