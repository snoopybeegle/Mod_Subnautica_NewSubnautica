﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Straitjacket.Harmony;

namespace NewSubnautica
{
    class AggressiveWhenSeePlayer : MonoBehaviour
    {
        Creature creature;

        public float maxRangeScalar = 20f;

        public float aggressionPerSecond = 0.3f;

        void Start()
        {
            creature = GetComponent<Creature>();

            InvokeRepeating("ScanForPlayer", UnityEngine.Random.Range(0f, 1f), 1f);
        }

        void ScanForPlayer()
        {
            var p = Player.main;

            if (p != null && p.CanBeAttacked())
            {
                var po = p.gameObject;

                float num = Vector3.Distance(po.transform.position, transform.position);

                if (num < maxRangeScalar && creature.GetCanSeeObject(po))
                {
                    float num2 = (maxRangeScalar - num) / maxRangeScalar;

                    creature.Aggression.Add(aggressionPerSecond * num2);

                    Debugger.Log("Aggr : " + creature.Aggression.Value);
                }
            }

            //Debugger.Log("Action : " + creature.GetBestAction());
        }
    }
}
