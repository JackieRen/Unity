using UnityEngine;
using UnityEngine.UI;

public class Ctrl : MonoBehaviour
{

    public RectTransform _panel;
    public RectTransform _center;
    public Button[] _btns;

    private float[] distance;
    private float[] distReposition;
    private bool dragging = false;
    private int btnDistance;
    private int minBtnNum;

    void Start()
    {
        int btnLenght = _btns.Length;
        distance = new float[btnLenght];
        distReposition = new float[btnLenght];
        btnDistance = (int)Mathf.Abs(_btns[1].GetComponent<RectTransform>().anchoredPosition.x
                                    - _btns[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    void Update()
    {
        for (int i = 0; i < _btns.Length; ++i)
        {
            distReposition[i] = _center.GetComponent<RectTransform>().position.x - _btns[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            if (distReposition[i] > 500)
            {
                float curX = _btns[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = _btns[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (_btns.Length * btnDistance), curY);
                _btns[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;

            }

            if (distReposition[i] < -500)
            {
                float curX = _btns[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = _btns[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (_btns.Length * btnDistance), curY);
                _btns[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;

            }

        }

        float minDistance = Mathf.Min(distance);

        for (int i = 0; i < _btns.Length; ++i)
        {
            if (minDistance == distance[i])
            {
                minBtnNum = i;
            }
        }

        if (!dragging)
        {
            // LerpToBtn(minBtnNum * -btnDistance);
            LerpToBtn((int)(-_btns[minBtnNum].GetComponent<RectTransform>().anchoredPosition.x));
        }


    }

    private void LerpToBtn(int position)
    {
        float newX = Mathf.Lerp(_panel.anchoredPosition.x, position, Time.deltaTime * 20f);
        Vector2 newPosition = new Vector2(newX, _panel.anchoredPosition.y);
        _panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

}

