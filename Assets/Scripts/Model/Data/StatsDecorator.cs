using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Units
{
    public class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider _statsProvider;

        protected StatsDecorator(IStatsProvider statsProvider)
        {
            _statsProvider = statsProvider;
        }

        public UnitStats GetStats()
        {
            throw new System.NotImplementedException();
        }
    }
}