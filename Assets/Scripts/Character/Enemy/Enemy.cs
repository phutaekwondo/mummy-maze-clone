using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class Enemy : Character
{
    DoctorAnimStateController animStateController;

    private void Awake() 
    {
        this.animStateController = this.GetComponent<DoctorAnimStateController>();
    }
}
