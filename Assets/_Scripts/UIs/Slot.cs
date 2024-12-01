using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Slot : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    private bool isMatch = false;
    public Image muralImg;
    public Rune curRune;
    public int slotID;
    public bool isEmpty;
    public int matchRuneID;
    public List<Slot> adjacentSlots;

    private void OnEnable()
    {
        curRune = GetComponentInChildren<Rune>(true);
        isEmpty=curRune.runeID == 0;
        CheckIsMatch();
        Debug.Log("<color=green>curSlotID: </color>" + slotID + "<color=green>curRuneID: </color>" + curRune.runeID);
        Debug.Log("<color=green>isMatch: </color>" + isEmpty);
    }

    private void Update()
    {
        if (isEmpty)
        {
            curRune.gameObject.SetActive(false);
        }
        else
        {
            curRune.gameObject.SetActive(true);
        }

        if (isMatch)
        {
            Color currentColor = muralImg.color;
            currentColor.a = 0.0f;
            muralImg.color = currentColor;
        }
        else
        {
            Color currentColor = muralImg.color;
            currentColor.a = 0.5f;
            muralImg.color = currentColor;
        }
    }
    public void CheckIsMatch()
    {
        if (curRune.runeID == matchRuneID)
        {
            isMatch = true;
            Debug.Log("<color=green>Is match!</color> matchRuneID: " + matchRuneID + " curRuneID: " + curRune.runeID);
        }
        else
        {
            isMatch = false;
            Debug.Log("<color=red>Is not match!</color>matchRuneID: " + matchRuneID + " curRuneID: " + curRune.runeID);
        }
    }
    public void UpdateCurRune()
    {
        curRune = GetComponentInChildren<Rune>(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.3f);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.3f, 0.3f);
    }
    public bool getIsMatch()
    {
        return isMatch;
    }
}