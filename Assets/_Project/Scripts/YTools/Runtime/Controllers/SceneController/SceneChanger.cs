using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YTools
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private SceneChangerType _type;
        [SerializeField] private int _index;
        [SerializeField] private string _name;

        [SerializeField] private Button _button;

        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SceneChange);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SceneChange);
        }

        private void SceneChange()
        {
            if (Validate() == false)
            {
                Error();
                return;
            }

            (_type switch
            {
                SceneChangerType.Index => (System.Action)(() => SceneManager.LoadScene(_index)),
                SceneChangerType.Name => () => SceneManager.LoadScene(_name),
                SceneChangerType.Back => () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1),
                SceneChangerType.Next => () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1),
                SceneChangerType.Restart => () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex),
                _ => default
            })();
        }

        private bool Validate()
        {
            var current = SceneManager.GetActiveScene().buildIndex;

            return _type switch
            {
                SceneChangerType.Index => _index >= 0 && _index < SceneManager.sceneCountInBuildSettings,
                SceneChangerType.Name => DoesSceneExistInBuild(),
                SceneChangerType.Back => current > 0,
                SceneChangerType.Next => current < SceneManager.sceneCountInBuildSettings - 1,
                _ => default
            };
        }

        private bool DoesSceneExistInBuild()
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneUtility.GetScenePathByBuildIndex(i).EndsWith($"/{_name}.unity"))
                    return true;
            }

            return false;
        }

        private void Error()
        {
            var message = _type switch
            {
                SceneChangerType.Index => $"index: {_index}",
                SceneChangerType.Name => $"name : {_name}",
                SceneChangerType.Back => $"index: {SceneManager.GetActiveScene().buildIndex - 1}",
                SceneChangerType.Next => $"index: {SceneManager.GetActiveScene().buildIndex + 1}",
                _ => default
            };

            Message.Send($"{"[ErrorLoadScene]".Color(ColorType.Red)} SceneCount: {SceneManager.sceneCountInBuildSettings.Color(ColorType.Cyan)} | target scene {message.Color(ColorType.Cyan)}");
        }
    }
}
