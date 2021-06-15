using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootCount : MonoBehaviour
{

    public int count = 10;

    public Text display;
    public GameObject iconModel;

    private bool _inCombat = false;
    private int _animating = 0;
    private int _incomingCount = 0;

    // Start is called before the first frame update
    void Start() {
        Refresh();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void Refresh() {
        string s = $"{count}";
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
            int remaining = 20 - _incomingCount - _animating;
            Add(remaining);
        } else {
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
                StartCoroutine("IconAnimation");
            }
            --i;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator IconAnimation() {

        GameObject icon = Instantiate(iconModel, transform);
        icon.SetActive(true);
        var rectTransform = icon.GetComponent<RectTransform>();
        var aimpos = new Vector2(-Screen.width/2 + rectTransform.rect.width/2, Screen.height/2 - rectTransform.rect.height/2);

        float smoothTime = 0.1f;
        Vector2 velocity = new Vector2(0,0);

        while ((rectTransform.anchoredPosition - aimpos).sqrMagnitude > 9) {
            yield return new WaitForSeconds(0.01f);
            rectTransform.anchoredPosition = Vector2.SmoothDamp(rectTransform.anchoredPosition, aimpos* 1.02f, ref velocity, smoothTime);
        }
        rectTransform.anchoredPosition = aimpos;
        --_animating;
        if (_inCombat) ++_incomingCount;
        else ++count;
        Object.Destroy(icon);
        Refresh();
    }
}
