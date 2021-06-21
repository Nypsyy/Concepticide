using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootCount : MonoBehaviour
{
    public int count = 10;

    public TextMeshProUGUI display;
    public GameObject iconModel;
    public GameObject dest;

    private bool _inCombat;
    private int _animating;
    private int _incomingCount;

    private void Start() {
        Refresh();
    }

    private void Refresh() {
        var s = $"{count}";
        if (_incomingCount > 0)
            s = $"{s} (+{_incomingCount})";
        display.text = s;
    }

    public void StartCombat() {
        _inCombat = true;
    }

    public void EndCombat(bool hasPlayerWon) {
        _inCombat = false;

        if (hasPlayerWon) {
            count += _incomingCount;
            var remaining = 20 - _incomingCount - _animating;
            Add(remaining);
        }
        else {
            StopAllCoroutines();
            _animating = 0;
        }

        _incomingCount = 0;
        Refresh();
    }

    public void RunawayCombat() {
        _inCombat = false;
        count += _incomingCount;
        _incomingCount = 0;

        Refresh();
    }

    public void Add(int i) {
        StartCoroutine(AddImpl(i));
    }

    private IEnumerator AddImpl(int i) {
        while (i > 0) {
            // maximum 10 scraps per combat
            if (!_inCombat || _incomingCount + _animating < 10) {
                ++_animating;
                StartCoroutine(nameof(IconAnimation));
            }

            --i;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IconAnimation() {
        var icon = Instantiate(iconModel, transform);
        icon.SetActive(true);
        var rectTransform = icon.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(Screen.width / 2, -Screen.height / 2);
        Vector2 aimPos = dest.GetComponent<RectTransform>().anchoredPosition;

        const float smoothTime = 0.1f;
        var velocity = new Vector2(0, 0);

        while ((rectTransform.anchoredPosition - aimPos).sqrMagnitude > 9) {
            yield return new WaitForSeconds(0.01f);
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, aimPos, ref velocity, smoothTime);
        }

        rectTransform.anchoredPosition = aimPos;
        --_animating;
        
        if (_inCombat)
            ++_incomingCount;
        else
            ++count;

        Destroy(icon);
        Refresh();
    }
}
