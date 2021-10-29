using System.Collections.Generic;
using UnityEngine;
public static class TransformExtensions
{
	public static void LookAt2D(this Transform self, Transform _target)
	{
		var current = self.position;
		var direction = _target.position - current;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		self.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}