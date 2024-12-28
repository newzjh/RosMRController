/**
 * @file qrgenerator.cs
 * @author zoequ
 * @brief Generate QR code for input info in 2D canvas. Copy script from internet.
 * @version 1.0
 * @date 2024
 * 
 * @copyright Flair 2024
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class qrgenerator : MonoBehaviour
{
    //Button to activate the feature.
    public Button sendBtn;
    //Input area.
    public TMP_InputField info;
    //Show the generated QRCode.
    public RawImage QRCode;

    void Start()
    {
        sendBtn.onClick.AddListener(DrawQRCode);
    }

    void DrawQRCode()
    {
        //Notice: the size (256, 256) is fixed, or the script will be broken.
        Texture2D t = ShowQRCode(info.text, 256, 256);

        //show the QR on RawImage:QRCode.
        QRCode.texture = t;

        //save the generated QRcode to loacl;
        byte[] pngData = t.EncodeToPNG();
        Debug.Log(Application.dataPath);
        string filePath = Application.dataPath + "/QRCode.png"; //just under "/Asset".
        File.WriteAllBytes(filePath, pngData);
    }
    /// <summary>
    /// 根据二维码图片信息绘制指定字符串信息
    /// </summary>
    /// <param name="str">字符串信息</param>
    /// <param name="width">二维码的宽度</param>
    /// <param name="height">二维码的高度</param>
    /// <returns>返回绘制好的图片</returns>
    Texture2D ShowQRCode(string str, int width, int height)
    {
        //实例化一个图片类
        Texture2D t = new Texture2D(width, height);
        //获取二维码图片颜色数组信息
        Color32[] col32 = GeneQRCode(str, width, height);
        //为图片设置绘制像素颜色信息
        t.SetPixels32(col32);
        //设置信息更新应用下
        t.Apply();
        return t;
    }
    /// <summary>
    /// 将制定字符串信息转换成二维码图片信息
    /// </summary>
    /// <param name="formatStr">字符串信息</param>
    /// <param name="width">二维码的宽度</param>
    /// <param name="height">二维码的高度</param>
    /// <returns>返回二维码图片的颜色数组信息</returns>
    Color32[] GeneQRCode(string formatStr, int width, int height)
    {
        //绘制二维码前进行一些设置
        ZXing.QrCode.QrCodeEncodingOptions options = new ZXing.QrCode.QrCodeEncodingOptions();
        //设置字符串转换格式 
        options.CharacterSet = "UTF-8";
        // 宽度和高度 
        options.Width = width;
        options.Height = height;
        //设置二维码边缘留白宽度 
        options.Margin = 1;
        //实例化字符串绘制二维码工具
        BarcodeWriter barcodeWriter = new BarcodeWriter { Format = BarcodeFormat.QR_CODE, Options = options };
        //进行二维码绘制并进行返回图片的颜色数组信息
        return barcodeWriter.Write(formatStr);
    }
}


