using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace EntityGenerator.UI
{
    /// <summary>
    /// 通过本类调用几个win32 api函数.
    /// </summary>
    public class WinAPIMethods
    {
        /// <summary>
        /// 本结构代表一个矩形区域.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        /// <summary>
        /// 代表鼠标动作.
        /// </summary>
        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        /// <summary>
        /// 设置鼠标的位置.
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <returns>函数是否执行成功</returns>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        /// <summary>
        /// 合成鼠标动作与鼠标按键.
        /// </summary>
        /// <param name="flags">鼠标动作种类</param>
        /// <param name="dx">鼠标X坐标(或偏移量)</param>
        /// <param name="dy">鼠标Y坐标(或偏移量)</param>
        /// <param name="data">若动作种类为Wheel,表示鼠标转动量;否则应置为0</param>
        /// <param name="extraInfo"></param>
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags,
                                        int dx,
                                        int dy,
                                        uint data,
                                        UIntPtr extraInfo);

        /// <summary>
        /// 得到特定的窗体引用.
        /// </summary>
        /// <param name="strClass">窗体相关的类名</param>
        /// <param name="strWindow">窗体名(窗体标题栏内容)</param>
        /// <returns>窗体引用</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string strClass, string strWindow);

        /// <summary>
        /// 查找子窗体.
        /// </summary>
        /// <param name="hwndParent">子窗体的父窗体</param>
        /// <param name="hwndChildAfter">从当前子窗体之后查找所需子窗体</param>
        /// <param name="strClass">子窗体类名</param>
        /// <param name="strWindow">子窗体标题栏内容</param>
        /// <returns>子窗体引用</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(HandleRef hwndParent
                                        , HandleRef hwndChildAfter
                                        , string strClass
                                        , string strWindow);


        /// <summary>
        /// 获取窗体的范围.
        /// </summary>
        /// <param name="hwnd">窗体引用</param>
        /// <param name="rect">矩形范围结构(输出参数)</param>
        /// <returns>是否成功获取</returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(HandleRef hwnd, 
                                        out NativeRECT rect);

    }
}
