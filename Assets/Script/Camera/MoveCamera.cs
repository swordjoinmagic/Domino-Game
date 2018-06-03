using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于移动摄像机的类
/// 当鼠标移动到屏幕的四个区域的时候，
/// 就会移动摄像机照射的区域
/// </summary>

public class MoveCamera : MonoBehaviour {

    public Texture2D texture2D;
    public Camera camera;

    private int screenWidth;
    private int screenHeight;

    private Vector3 mousePosition;
    public int range = 20;
    private int leftLimit;      // 左边界
    private int rightLimit;     // 右边界
    private int upLimit;        // 上边界
    private int downLimit;      // 下边界

    public Vector3 leftStep = new Vector3(-1,0,0);
    public Vector3 rightStep = new Vector3(1,0,0);
    public Vector3 upStep = new Vector3(0,0,1);
    public Vector3 downStep = new Vector3(0,0,-1);
    public Vector3 upZStep = new Vector3(0,1,0);
    public Vector3 downZStep = new Vector3(0, -1, 0);

    public Texture2D Rotate(Texture2D texture) {
        int width = texture.width;  //图片原本的宽度  
        int height = texture.height;  //图片原本的高度  
        Texture2D newTexture = new Texture2D(height, width); //实例化一个新的texture，高度是原来的宽度，宽度是原来的高度 
        
        for (int i = 0; i <= width - 1; i++) {
            for (int j = 0; j <= height - 1; j++) {
                Color color = texture.GetPixel(i, j);
                newTexture.SetPixel(j, width - 1 - i, color);
            }
        }
        newTexture.Apply();
        return newTexture;
    }

    public Texture2D DownArrow(Texture2D texture) {
        return Rotate(texture);
    }

    public Texture2D UpArrow(Texture2D texture) {
        for (int i=1;i<=3;i++) {
            texture = Rotate(texture);
        }
        return texture;
    }

    public Texture2D LeftArrow(Texture2D texture) {
        for (int i=1;i<=2;i++) {
            texture = Rotate(texture);
        }
        return texture;
    }

    public Texture2D RightArrow(Texture2D texture) {
        for (int i=1;i<=4;i++) {
            texture = Rotate(texture);
        }
        return texture;
    }

    // Use this for initialization
    void Start () {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        leftLimit = range;
        rightLimit = screenWidth - range;
        upLimit = screenHeight - range;
        downLimit = range;
	}

    private void Update() {
        Vector3 mousePosition = Input.mousePosition;

        float x = mousePosition.x;
        float y = mousePosition.y;

        Texture2D nowTexture2D = null;

        //===================================
        // 移动摄像头的x坐标和y坐标
        //===================================
        if (x < leftLimit) {
            camera.transform.Translate(leftStep * Time.deltaTime,Space.World);
            nowTexture2D = LeftArrow(texture2D);
        }
        if (x > rightLimit) {
            camera.transform.Translate(rightStep * Time.deltaTime, Space.World);
            nowTexture2D = RightArrow(texture2D);
        }
        if (y < downLimit) {
            camera.transform.Translate(downStep * Time.deltaTime, Space.World);
            nowTexture2D = DownArrow(texture2D);
        }
        if (y > upLimit) {
            camera.transform.Translate(upStep * Time.deltaTime, Space.World);
            nowTexture2D = UpArrow(texture2D);
        }

        try {
            Cursor.SetCursor(nowTexture2D, Vector2.zero, CursorMode.Auto);
        } catch (Exception e) { }


        //=======================================
        // 使用鼠标滚轮移动摄像头的y坐标
        //========================================
        if (Input.mouseScrollDelta.y < 0) {
            // 向前滚轮，视野变小
            camera.transform.Translate(upZStep * Time.deltaTime,Space.World);
        } else if (Input.mouseScrollDelta.y > 0) {
            // 向后滚轮，视野变大
            camera.transform.Translate(downZStep * Time.deltaTime,Space.World);
        }
    }

}
