﻿using UnityEngine;

namespace Common.Attributes
{
    public class PrefabDropdownAttribute : PropertyAttribute
    {

        public string addtionalPath;
        public string propertyType;

        public PrefabDropdownAttribute()
        {
            addtionalPath = "";
        }

        public PrefabDropdownAttribute(string path)
        {
            if (path.Length > 0)
                addtionalPath = System.IO.Path.DirectorySeparatorChar + path;
        }

    }
}
