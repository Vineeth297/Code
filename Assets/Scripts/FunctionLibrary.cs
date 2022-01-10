
using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
	public delegate float Function(float x, float t);

	public enum FunctionEnum
	{
		Wave,
		MultiWave,
		RippleWave
	}

	private static Function[] _functions = {Wave, MultiWave, RippleWave};
	public static Function GetFunction(FunctionEnum name)
	{
		return _functions[(int)name];
	}
	
	public static float Wave(float x, float t)
	{
		return Mathf.Sin(Mathf.PI * (x + t));
	}

	public static float MultiWave(float x, float t)
	{
		float y = Sin(PI * (x + 0.5f * t));
		y += 0.5f * Sin(2f * PI * (x + t));
		return y * (2f/3f);
	}

	public static float RippleWave(float x, float t)
	{
		float d = Abs(x);
		float y = Sin(PI * (4 * d - t));
		return y / (1 + 10 * d);
	}
}
