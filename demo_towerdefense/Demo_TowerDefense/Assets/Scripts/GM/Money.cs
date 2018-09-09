using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money {

    private int value;

    public int Value {
        get {
            return this.value;
        }
        set {
            if (value < 1000)
                this.value = value;
        }
    }


    public Money() { }


    public Money(int value) {
        this.value = value;
    }


    public void SpendMoney(int value) {
        this.value -= value;
    }


    public void GetMoney(int value) {
        this.value += value;
    }
}
