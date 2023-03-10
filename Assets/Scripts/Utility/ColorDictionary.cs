using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class ColorDictionary
    {
        static Dictionary<SortingColor,Color> _colorDictionary = new()
        {
            {SortingColor.Red ,Color.red},
            {SortingColor.Green, Color.green},
            {SortingColor.Blue, Color.blue},
            {SortingColor.Yellow, Color.yellow},
            {SortingColor.Purple, new Color(0.333f,0.145f,0.51f,1)},
            {SortingColor.Pink, new Color(1,0.753f,0.796f,1)},
            {SortingColor.Black, Color.black},
            {SortingColor.White, Color.white},
            {SortingColor.Orange, new Color(0.992f,0.725f,0.153f)},
            {SortingColor.Cyan, Color.cyan},
            {SortingColor.Blank,Color.grey}
        };

        public static Color GetColor(SortingColor sortingColor)
        {
            return _colorDictionary[sortingColor];
        }
    }
    public enum SortingColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Purple,
        Pink,
        Black,
        White,
        Orange,
        Cyan,
        Blank = -1
    }
}