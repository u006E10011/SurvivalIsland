using TMPro;
using UnityEngine;

namespace YTools
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private string _ru;
        [SerializeField] private string _en;

        [SerializeField] private bool _isAutoUpdate = true;
        [SerializeField] private bool _updateOfTick = true;

        [SerializeField] private bool _isTranslateByKey;
        [SerializeField] private string _key;

        [SerializeField] private TMP_Text _text;

        private LocalizationText _localizationText;

        private void OnValidate()
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();

            SetFont();
        }

        private void Awake() => Init(_key);

        private void OnEnable()
        {
            if (_isAutoUpdate)
                UpdateText();
        }

        private void Update()
        {
            if (_updateOfTick)
                UpdateText();
        }

        public Localization Init(string key)
        {
            if (_isTranslateByKey)
            {
                if (string.IsNullOrEmpty(LocalizationProvider.Data.Data.Find(data => data.Key == key).Key) == false)
                    Message.Send($"Not found key: {key}", context: this);

                _localizationText = LocalizationProvider.GetLocalizationText(key);
                _key = key;
            }

            return this;
        }

        public Localization UpdateText(string end = "")
        {
            if (_isTranslateByKey)
                _text.text = (LocalizationProvider.Type == LanguageType.RU ? _localizationText.RU : _localizationText.EN) + end;
            else
                _text.text = (LocalizationProvider.Type == LanguageType.RU ? _ru : _en) + end;

            return this;
        }

        public Localization Add(string text)
        {
            _text.text += text;
            return this;
        }

        public Localization Set(string text)
        {
            _text.text = text;
            return this;
        }

        private void SetFont()
        {
            if (_text && LocalizationProvider.Data && LocalizationProvider.Data.Font)
                _text.font = LocalizationProvider.Data.Font;
        }
    }
}