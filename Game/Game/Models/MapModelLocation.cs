﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Represent the Map
    /// 
    /// The Cordinates
    /// What is at that location
    /// 
    /// </summary>
    public class MapModelLocation
    {
        // The X, which is R in Grid
        public int Row;

        // They Y, which is Column in Grid
        public int Column;

        // The Player, Character or Unknown for blank
        public EntityInfoModel Player = new EntityInfoModel();

        // If IsSelected, used for targeting
        public bool IsSelectedTarget = false;

        // If IsSelectable, used for target selection
        public bool IsSelectable = false;
    }
}
