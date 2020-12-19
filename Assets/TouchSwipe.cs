using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSwipe : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchStartPos, touchEndPos;
    private Vector2 currentCardStartPos, currentCardEndPos; 
    private Vector2 nextCardStartPos, nextCardEndPos;
    public RectTransform[] m_Cards;
   // private int cardPosition;
    private int totalCards;    
    private int currentCardIndex;
    private int nextCardIndex;

    private bool cardSwipedRight = false;
    private bool cardSwipedLeft = false;

    private Vector2 vec = new Vector2(10000, 0);// Vector2.zero;
//private Vector2 vec = new Vector2(10000, 0);
    private Vector2 cardOffsetX = new Vector2(900,0);
    // Update is called once per frame

    void Start()
    {
        Debug.Log("Start");
        totalCards = m_Cards.Length;
        currentCardIndex = 0;
    }
    void Update()
    {
        Debug.Log("Update");
        Debug.Log(currentCardIndex);
        if (Input.touchCount > 0)
        {
            if (!isLast(currentCardIndex))
            {
                currentCardStartPos = m_Cards[currentCardIndex].anchoredPosition;
                currentCardEndPos = new Vector2(cardOffsetX.x + currentCardStartPos.x, 0);
            }
            if (!isFirst(currentCardIndex))
            {
                nextCardIndex = currentCardIndex + 1;
                nextCardStartPos = m_Cards[nextCardIndex].anchoredPosition;
                nextCardEndPos = m_Cards[nextCardIndex].anchoredPosition- cardOffsetX;
            }

            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                if (cardSwipedRight) currentCardIndex++;
                if (cardSwipedLeft) currentCardIndex--;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                float x = touchEndPos.x - touchStartPos.x;
                float y = touchEndPos.y - touchStartPos.y;

                if (Mathf.Abs(x) == Mathf.Abs(y))
                {
                    //tapped

                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    string direction = (x > y) ? "Right" : "Left";
                    if (direction == "Right" && !isLast(currentCardIndex))
                    {
                        Debug.Log("It goes right");
                        m_Cards[currentCardIndex].anchoredPosition = Vector2.SmoothDamp(currentCardStartPos, currentCardEndPos, ref vec, 0.1F);
                        cardSwipedRight = true;
                    }
                    else if (direction == "Left" && isFirst(currentCardIndex))
                    {
                        Debug.Log("It goes left");
                        m_Cards[currentCardIndex].anchoredPosition = Vector2.SmoothDamp(nextCardStartPos, nextCardEndPos, ref vec, 0.1F);
                        cardSwipedLeft = true;
                    }
                    else { cardSwipedRight = false; cardSwipedLeft = false; }
                }
            }
        }

        bool isLast(int currentCardIndex)
        {
            //Debug.Log((currentCardIndex < m_Cards.Length) ? true : false);
            Debug.Log("Right");
            return (currentCardIndex >=totalCards - 1) ? true : false;
        }

        bool isFirst(int currentCardIndex)
        {
            //Debug.Log((currentCardIndex != 0) ? true : false);
            Debug.Log("Left");

            return (currentCardIndex != 0) ? true : false;
        }
    }
}

