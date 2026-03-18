using System.Collections;
using UnityEngine;

namespace YTools
{
    public class ObjectPool<T> : PoolBase<T> where T : Component
    {
        public ObjectPool(T prefab, int preloadCount = 0, bool autoPreload = true, System.Action<T> init = null, Transform parent = null)
            : base(() => PreloadAction(prefab), GetAction, ReturnAction, init, preloadCount, autoPreload)
        {
            SetParent(parent);
        }


        public static T PreloadAction(T prefab) => Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        public static void GetAction(T prefab) => prefab.gameObject.SetActive(true);
        public static void ReturnAction(T prefab) => prefab.gameObject.SetActive(false);

        public ObjectPool<T> ForEach(System.Action<T> action)
        {
            foreach (var item in Pool)
                action?.Invoke(item);

            return this;
        }

        public ObjectPool<T> ForEach(System.Action<T, int> action)
        {
            for (int i = 0; i < Pool.Count; i++)
                action?.Invoke(AllItems[i], i);

            return this;
        }

        public void SetParent(Transform parent)
        {
            ForEach(p => p.transform.parent = parent);
        }
    }
}