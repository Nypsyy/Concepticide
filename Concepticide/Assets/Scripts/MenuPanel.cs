using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    public delegate void ValidationDelegate();
    public struct Option {
        public string name;
        public string description;
        public ValidationDelegate onValidation;
        
        public Option(string name_, string description_, ValidationDelegate onValidation_) {
            name = name_;
            description = description_;
            onValidation = onValidation_;
        }
    }

    public delegate void SelectionHandler(string optionName);
    public GameObject menuChoice;
    public Text infoText;

    // Will be called on selection validation.
    public SelectionHandler selectionHandler = (optionName) => {};

    private List<(Option option, GameObject obj, Text text)> _options = new List<(Option option, GameObject obj, Text text)>();
    private int _selectedPos = 0;

    private string _lastInfoText = "";

    private ValidationDelegate _infoValidationHandler;

    void Start()
    {
        menuChoice.SetActive(false);
        menuChoice.transform.SetParent(null, false);
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        // TODO: better input system

        if (Input.GetKeyDown("up"))
            _UpdateSelectedPos(_selectedPos - 1);
        if (Input.GetKeyDown("down"))
            _UpdateSelectedPos(_selectedPos + 1);
        
        if (Input.GetKeyDown(KeyCode.Return)) {
            gameObject.SetActive(false);
            if (_infoValidationHandler != null) {
                _infoValidationHandler();
                _infoValidationHandler = null;
            } else {
                _options[_selectedPos].option.onValidation();
            }
        }

    }

    public void PushInfo(string text) {
        infoText.text = text;
        _lastInfoText = text;
    }

    public void DisplayInfo(string text, ValidationDelegate onValidation) {
        gameObject.SetActive(true);
        
        PushInfo(text);
        _infoValidationHandler = onValidation;
    }

    public void DisplayMenu(Option[] options) {
        gameObject.SetActive(true);

        foreach (var (_, gameObj,_) in _options)
            GameObject.Destroy(gameObj);
        _options.Clear();

        foreach (Option option in options) {
            var gameObj = Instantiate(menuChoice, transform);
            gameObj.SetActive(true);
            var text = gameObj.GetComponent<Text>();
            text.text = option.name;
            _options.Add((option, gameObj, text));
        }
        
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80 * _options.Count);
        _selectedPos = 0;
        _UpdateSelectedPos(_selectedPos);
    }

    private void _UpdateSelectedPos(int newPos) {
        if (newPos < 0)
            newPos += _options.Count;
        else if (newPos >= _options.Count)
            newPos -= _options.Count;

        _options[_selectedPos].text.fontStyle = FontStyle.Normal;
        _selectedPos = newPos;
        _options[_selectedPos].text.fontStyle = FontStyle.Bold;

        string desc = _options[_selectedPos].option.description;
        infoText.text = desc == null ? _lastInfoText : desc;
    }

}
