// Abir Mahmood
// CSC-350H
// Dr. Hao Tang
// ScreenUtils.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public static class ScreenUtils
{
    static int screenWidth;
	static int screenHeight;

    // this is for boundary checking of the ship
    static float screenLeft;
	static float screenRight;
	static float screenTop;
	static float screenBottom;

	public static float ScreenLeft { get { return screenLeft; } }
	public static float ScreenRight { get { return screenRight; } }
	public static float ScreenTop { get { return screenTop; } }
	public static float ScreenBottom { get { return screenBottom; } }

	public static void Initialize()
	{
		float screen2 = -Camera.main.transform.position.z;
		Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screen2);
		Vector3 lowerLeftCornerWorld = Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
		Vector3 topRightCornerScreen = new Vector3(UnityEngine.Device.Screen.width, UnityEngine.Device.Screen.height, screen2);
		Vector3 topRightCornerWorld = Camera.main.ScreenToWorldPoint(topRightCornerScreen);

		screenLeft = lowerLeftCornerWorld.x;

		screenRight = topRightCornerWorld.x;

		screenTop = topRightCornerWorld.y;

		screenBottom = lowerLeftCornerWorld.y;

	}
}