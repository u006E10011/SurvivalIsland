using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ryadevn
{
    internal class InventoryIconProvider
    {
        private readonly static List<Sprite> _icons = new();

        static InventoryIconProvider()
        {
            _icons = Resources.LoadAll<Texture2D>("InventoryIcons")
                .Select(x => TextureToSprite(x)) .ToList();
        }

        public static Sprite Get(InventorySaveDataBase data)
        {
            return _icons.Find(x => x.name.Equals(data.Type.ToString().ToLower()));
        }

        private static Sprite TextureToSprite(Texture2D texture)
        {
            var sprite =  Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f,
                0,
                SpriteMeshType.FullRect
            );

            sprite.name = texture.name.ToLower();
            return sprite;
        }
    }
}
