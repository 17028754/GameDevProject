using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawnBar : MonoBehaviour
{

	public Slider slider;

	public void SetMaxBar(int maxbar)
	{
		slider.maxValue = maxbar;
	}

	public void SetSpawnBar(int bar)
	{
		slider.value = bar;
	}
}
