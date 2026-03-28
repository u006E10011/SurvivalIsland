using UnityEngine;

namespace Ryadevn
{
    [System.Flags]
    public enum HarvestableObjectType
    {
        [InspectorName("Дерево [Tree]")] Tree = 1 << 0,
        [InspectorName("Камень [Stone]")] Stone = 1 << 1,
        [InspectorName("Песок [Sand]")] Sand = 1 << 2,
        [InspectorName("Трава [Grass]")] Grass = 1 << 3,
        [InspectorName("Сахарный тростник [Sugar Cane]")] SugarCane = 1 << 4,
        [InspectorName("Железная руда [Iron Ore]")] IronOre = 1 << 5,
        [InspectorName("Золотая руда [Gold Ore]")] GoldOre = 1 << 6,
        [InspectorName("Медная руда [Copper Ore]")] CopperOre = 1 << 7,
        [InspectorName("Алмазная руда [Diamond Ore]")] DiamondOre = 1 << 8,
        [InspectorName("Уголь [Coal]")] Coal = 1 << 9,
        [InspectorName("Бамбук [Bamboo]")] Bamboo = 1 << 10,
        [InspectorName("Глина [Clay]")] Clay = 1 << 11,
        [InspectorName("Кость [Dice]")] Dice = 1 << 12,
        [InspectorName("Пшеница [Wheat]")] Wheat = 1 << 13,
        [InspectorName("Хлопок [Cotton]")] Cotton = 1 << 14,
    }
}
