using UnityEngine;

public enum ScreenType
{
    Tablet,
    Phone,
    LongPhone
};

public class ScreenInfo
{
    private static Vector2 ScreenSizeInPixels() { return new Vector2(Screen.width, Screen.height); }
    private static float ScreenWidthInPixels() { return Screen.width; }
    private static float ScreenHeightInPixels() { return Screen.height; }
    public static Vector2 ScreenSizeUI(float canvasHeight = 1080f) { return ScreenSizeInPixels() * (canvasHeight / ScreenHeightInPixels()); }
    public static float ScreenWidthUI(float multiplier = 1.0f, float canvasHeight = 1080f) { return ScreenSizeUI(canvasHeight).x * multiplier; }
    public static float ScreenHeightUI(float multiplier = 1.0f, float canvasHeight = 1080f) { return ScreenSizeUI(canvasHeight).y * multiplier; }
    public static float ConstantValueUI(float multiplier = 1.0f) { return Mathf.Min(ScreenWidthUI(multiplier), ScreenHeightUI(multiplier)); }
    public static Vector2 ScreenSize() { return Camera.main.ScreenToWorldPoint(ScreenSizeInPixels()) * 2; }
    public static float ScreenWidth(float multiplier = 1.0f) { return ScreenSize().x * multiplier; }
    public static float ScreenHeight(float multiplier = 1.0f) { return ScreenSize().y * multiplier; }
    public static float ConstantValue(float multiplier = 1.0f) { return Mathf.Min(ScreenHeight(multiplier), ScreenWidth(multiplier)); }

    public static bool IsThisPhone()
    {
        return GetDeviceType() == ScreenType.Phone;
    }

    private static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        return diagonalInches;
    }

    public static ScreenType GetDeviceType()
    {
#if UNITY_IOS
        bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
        if (deviceIsIpad)
            return ScreenType.Tablet;

        bool deviceIsIphone = UnityEngine.iOS.Device.generation.ToString().Contains("iPhone");
        if (deviceIsIphone)
            return ScreenType.Phone;
        else
            return ScreenType.Tablet;
#elif UNITY_ANDROID

            float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
            bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

            if (isTablet)
                return ScreenType.Tablet;
            else
                return ScreenType.Phone;
#endif
    }

    public static Vector3 GetScreenInWorldPoint(float x, float y) { return new Vector3(ScreenWidth(x - 0.5f), ScreenHeight(y - 0.5f), 0); }

    public static Vector3 GetUIScreenPosition(float x, float y) { return new Vector3(Screen.width * x, Screen.height * y, 0); }

    public static float PointsToPixels(float points)
    {
        float pixelsPerPoint = ScreenHeightUI() / ScreenHeight();
        return points * pixelsPerPoint;
    }

    public static Vector2 PointsToPixels(Vector2 value) { return new Vector2(PointsToPixels(value.x), PointsToPixels(value.y)); }

    public static Vector3 PointsToPixels(Vector3 value) { return new Vector3(PointsToPixels(value.x), PointsToPixels(value.y), PointsToPixels(value.z)); }

    public static float PixelsToPoints(float pixels)
    {
        float pixelsPerPoint = ScreenHeightUI() / ScreenHeight();
        return pixels / pixelsPerPoint;
    }
    public static Vector2 PixelsToPoint(Vector2 value) { return new Vector2(PixelsToPoints(value.x), PixelsToPoints(value.y)); }

    public static Vector3 PixelsToPoint(Vector3 value) { return new Vector3(PixelsToPoints(value.x), PixelsToPoints(value.y), PixelsToPoints(value.z)); }

    public static ScreenType GetScreenType()
    {
        float width = Mathf.Max(ScreenWidthInPixels(), ScreenHeightInPixels());
        float height = Mathf.Min(ScreenWidthInPixels(), ScreenHeightInPixels());
        float ratio = width / height;
        if (ratio < 1.55)
            return ScreenType.Tablet;
        if (ratio > 1.9)
            return ScreenType.LongPhone;
        return ScreenType.Phone;
    }

    public static bool IsTablet() { return GetScreenType() == ScreenType.Tablet; }

    public static bool IsPhone() { return GetScreenType() == ScreenType.Phone; }

    public static bool IsLongPhone() { return GetScreenType() == ScreenType.LongPhone; }

    public static float GetLongPhonePaddingUI() { return ScreenHeightUI(0.08f); }

    public static float GetLongPhonePadding() { return ScreenHeight(0.08f); }

    public static float GetDistanceToPutOnMove() { return 0.5f; }

    public static float GetDistanceToPutOnRelease() { return 1.4f; }
}
