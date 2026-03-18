using UnityEngine;

namespace YTools
{
    public static class CursorController
    {
        public static bool Visible { get; private set; }

        public static void IsVisible(bool isVisible)
        {
            if (PlatformController.Instance.Type == PlatformType.Mobile)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
            else
            {
                Visible = isVisible;

                Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = isVisible;
            }
        }

        public static void Switch()
        {
            Visible = !Visible;

            Cursor.lockState = Visible ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = Visible;
        }
    }
}