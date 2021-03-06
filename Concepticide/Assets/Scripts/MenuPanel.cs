using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MenuPanel : MonoBehaviour
{
    public delegate void ValidationDelegate();

    public struct Option
    {
        public string name;
        public string description;
        public ValidationDelegate onValidation;

        public Option(string name, string description, ValidationDelegate onValidation) {
            this.name = name;
            this.description = description;
            this.onValidation = onValidation;
        }

        public static readonly Option Null = new Option(null, null, null);
    }

    public delegate void SelectionHandler(string optionName);

    public GameObject menuChoice;
    public Text infoText;

    // Will be called on selection validation.
    public SelectionHandler selectionHandler = optionName => { };

    private List<(Option option, GameObject obj, Text text)> _options = new List<(Option option, GameObject obj, Text text)>();
    private int _selectedPos;

    private string _lastInfoText = "";

    private ValidationDelegate _infoValidationHandler;

    private void Start() {
        menuChoice.SetActive(false);
        menuChoice.transform.SetParent(null, false);
        //gameObject.SetActive(false);
    }


    private void Update() {
        // TODO: better input system

        if (Input.GetKeyDown("up"))
            _UpdateSelectedPos(_selectedPos - 1);
        if (Input.GetKeyDown("down"))
            _UpdateSelectedPos(_selectedPos + 1);

        if (Input.GetKeyDown(KeyCode.Return)) {
            gameObject.SetActive(false);

            if (_infoValidationHandler != null) {
                var validationHandler = _infoValidationHandler;
                _infoValidationHandler = null;
                validationHandler();
            }
            else {
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
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        PushInfo(text);
        _infoValidationHandler = onValidation;
    }

    public void DisplayMenu(Option[] options) {
        gameObject.SetActive(true);

        foreach (var (_, gameObj, _) in _options)
            Destroy(gameObj);
        _options.Clear();

        // ignoring null choices
        options = options.Where(option => option.name != null).ToArray();

        foreach (var option in options) {
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

        var desc = _options[_selectedPos].option.description;
        infoText.text = desc ?? _lastInfoText;
    }
}