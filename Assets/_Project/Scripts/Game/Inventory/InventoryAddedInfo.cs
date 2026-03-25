using DG.Tweening;
using Gaskellgames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ryadevn
{
    public class InventoryAddedInfo : MonoBehaviour
    {
        public static System.Action<HarvestableSaveData> OnShow;

        [Title("Animation")]
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _waitToClose = 1f;
        [SerializeField] private Ease _showEase = Ease.OutBack;
        [SerializeField] private Ease _closeEase = Ease.InBack;

        [Title("Reference")]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;
        [SerializeField] private CanvasGroup _canvasGroup;

        private ItemKeyData _keyData;
        private Tween _currentSequence;

        private void Awake()
        {
            _keyData = Resources.Load<ItemKeyData>("Data/" + nameof(ItemKeyData));

            if (_canvasGroup != null)
                _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            OnShow += Show;
        }

        private void OnDisable()
        {
            OnShow -= Show;
        }

        private void Show(HarvestableSaveData data)
        {
            if (data == null)
                return;

            string itemName = YTools.LocalizationProvider.Get(_keyData.HarvestableKey[data.ResourceType]);
            int currentAmount = Inventory.GetResourceAmount(data.ResourceType);
            _text.text = $"+{data.Amount} {itemName} | {currentAmount}";
            _icon.sprite = InventoryIconProvider.Get(data);

            if (_currentSequence != null && _currentSequence.IsActive())
            {
                _currentSequence.Kill();
                _currentSequence = null;
            }

            float currentAlpha = _canvasGroup.alpha;
            float targetAlpha = 1;
            float animationDuration;

            if (currentAlpha >= 0.99f)
                animationDuration = 0f;
            else if (currentAlpha > 0f)
            {
                float remainingPath = 1f - currentAlpha;
                animationDuration = _duration * remainingPath;
            }
            else
                animationDuration = _duration;

            _currentSequence = CreateAnimationSequence(targetAlpha, animationDuration);
        }

        private Tween CreateAnimationSequence(float targetAlpha, float showDuration)
        {
            Sequence sequence = DOTween.Sequence();

            if (targetAlpha > 0 && _canvasGroup.alpha < targetAlpha)
            {
                if (showDuration > 0)
                    sequence.Append(_canvasGroup.DOFade(targetAlpha, showDuration).SetEase(_showEase));
                else
                    _canvasGroup.alpha = targetAlpha;
            }

            sequence.AppendInterval(_waitToClose);
            sequence.Append(_canvasGroup.DOFade(0, _duration).SetEase(_closeEase));
            sequence.SetLink(gameObject);
            sequence.OnComplete(() =>
            {
                _canvasGroup.alpha = 0;
                _currentSequence = null;
            });

            sequence.Play();

            return sequence;
        }

        public void HideImmediately()
        {
            if (_currentSequence != null && _currentSequence.IsActive())
            {
                _currentSequence.Kill();
                _currentSequence = null;
            }

            if (_canvasGroup != null)
                _canvasGroup.alpha = 0;
        }
    }
}