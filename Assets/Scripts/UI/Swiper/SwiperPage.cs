using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Mathematics;

enum ChangePageType
{
    None,
    Left,
    Right
}

[RequireComponent(typeof(RectTransform))]
public class SwiperPage : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] List<RectTransform> children = new List<RectTransform>();
    [SerializeField] float fullLengthEaseDuration = 0.5f;
    private int currentPageIndex = 0;
    private const float X_DIFF_PERCENTAGE_THRESHOLD = 0.2f;
    private bool isEasing = false;

    private void Start()
    {
        SetPageIndex(currentPageIndex);
    }

    private void SetPageIndex(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= children.Count)
        {
            return;
        }

        currentPageIndex = pageIndex;

        float pageWidth = GetComponent<RectTransform>().rect.width;
        float firstX = pageWidth * (-pageIndex);
        for (int index = 0; index < children.Count; index++)
        {
            Vector2 oldPosition = children[index].anchoredPosition;
            float newX = firstX + (pageWidth * index);
            children[index].anchoredPosition = new Vector2(newX, oldPosition.y);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isEasing)
        {
            return;
        }

        float xDiff = eventData.position.x - eventData.pressPosition.x;
        SetDraggingX(xDiff);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isEasing)
        {
            return;
        }

        float xDiff = eventData.position.x - eventData.pressPosition.x;
        ChangePageType changePageType = GetChangePageType(xDiff);
        int targetIndex = GetTargetIndex(changePageType);
        StartCoroutine(EaseChangePage(changePageType, xDiff, () =>
        {
            SetPageIndex(targetIndex);
        }));
    }

    private IEnumerator EaseChangePage(ChangePageType changePageType, float startXDiff, Action onDone)
    {
        isEasing = true;

        float pageWidth = GetComponent<RectTransform>().rect.width;
        float targetXDiff = changePageType == ChangePageType.None ? 0 : changePageType == ChangePageType.Left ? pageWidth : -pageWidth;

        float neededDuration = fullLengthEaseDuration * math.abs(targetXDiff - startXDiff) / pageWidth;
        Debug.Log(neededDuration);
        float progress = 0f;
        while (progress <= 1f)
        {
            progress += Time.deltaTime / neededDuration;
            float easingXDiff = Mathf.SmoothStep(startXDiff, targetXDiff, progress);
            SetDraggingX(easingXDiff);
            yield return null;
        }

        isEasing = false;
        onDone();
    }

    private void SetDraggingX(float xDiff)
    {
        float pageWidth = GetComponent<RectTransform>().rect.width;
        float firstX = pageWidth * (-currentPageIndex);
        for (int index = 0; index < children.Count; index++)
        {
            Vector2 oldPosition = children[index].anchoredPosition;
            float newX = firstX + (pageWidth * index) + xDiff;
            children[index].anchoredPosition = new Vector2(newX, oldPosition.y);
        }
    }

    private ChangePageType GetChangePageType(float differenceX)
    {
        float pageWidth = GetComponent<RectTransform>().rect.width;
        bool isBreakThreshold = Math.Abs(differenceX) / pageWidth > X_DIFF_PERCENTAGE_THRESHOLD;
        ChangePageType changePageType = ChangePageType.None;

        if (isBreakThreshold)
        {
            changePageType = differenceX < 0 ? ChangePageType.Right : ChangePageType.Left;
        }
        else
        {
            changePageType = ChangePageType.None;
        }

        if (IsAcceptableChangePageType(changePageType))
        {
            return changePageType;
        }
        else
        {
            return ChangePageType.None;
        }
    }

    private bool IsAcceptableChangePageType(ChangePageType changePageType)
    {
        int targetPageIndex = GetTargetIndex(changePageType);
        return targetPageIndex >= 0 && targetPageIndex < children.Count;
    }

    private int GetTargetIndex(ChangePageType changePageType)
    {
        switch (changePageType)
        {
            case ChangePageType.Left:
                return currentPageIndex - 1;
            case ChangePageType.Right:
                return currentPageIndex + 1;
            default:
                return currentPageIndex;
        }
    }
}