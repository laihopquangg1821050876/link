using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCommon
{
    public enum KeyCode : ushort
    {
        // Token: 0x0400010B RID: 267
        MEDIA_NEXT_TRACK = 176,
        // Token: 0x0400010C RID: 268
        MEDIA_PLAY_PAUSE = 179,
        // Token: 0x0400010D RID: 269
        MEDIA_PREV_TRACK = 177,
        // Token: 0x0400010E RID: 270
        MEDIA_STOP,
        // Token: 0x0400010F RID: 271
        ADD = 107,
        // Token: 0x04000110 RID: 272
        MULTIPLY = 106,
        // Token: 0x04000111 RID: 273
        DIVIDE = 111,
        // Token: 0x04000112 RID: 274
        SUBTRACT = 109,
        // Token: 0x04000113 RID: 275
        BROWSER_BACK = 166,
        // Token: 0x04000114 RID: 276
        BROWSER_FAVORITES = 171,
        // Token: 0x04000115 RID: 277
        BROWSER_FORWARD = 167,
        // Token: 0x04000116 RID: 278
        BROWSER_HOME = 172,
        // Token: 0x04000117 RID: 279
        BROWSER_REFRESH = 168,
        // Token: 0x04000118 RID: 280
        BROWSER_SEARCH = 170,
        // Token: 0x04000119 RID: 281
        BROWSER_STOP = 169,
        // Token: 0x0400011A RID: 282
        NUMPAD0 = 96,
        // Token: 0x0400011B RID: 283
        NUMPAD1,
        // Token: 0x0400011C RID: 284
        NUMPAD2,
        // Token: 0x0400011D RID: 285
        NUMPAD3,
        // Token: 0x0400011E RID: 286
        NUMPAD4,
        // Token: 0x0400011F RID: 287
        NUMPAD5,
        // Token: 0x04000120 RID: 288
        NUMPAD6,
        // Token: 0x04000121 RID: 289
        NUMPAD7,
        // Token: 0x04000122 RID: 290
        NUMPAD8,
        // Token: 0x04000123 RID: 291
        NUMPAD9,
        // Token: 0x04000124 RID: 292
        F1 = 112,
        // Token: 0x04000125 RID: 293
        F10 = 121,
        // Token: 0x04000126 RID: 294
        F11,
        // Token: 0x04000127 RID: 295
        F12,
        // Token: 0x04000128 RID: 296
        F13,
        // Token: 0x04000129 RID: 297
        F14,
        // Token: 0x0400012A RID: 298
        F15,
        // Token: 0x0400012B RID: 299
        F16,
        // Token: 0x0400012C RID: 300
        F17,
        // Token: 0x0400012D RID: 301
        F18,
        // Token: 0x0400012E RID: 302
        F19,
        // Token: 0x0400012F RID: 303
        F2 = 113,
        // Token: 0x04000130 RID: 304
        F20 = 131,
        // Token: 0x04000131 RID: 305
        F21,
        // Token: 0x04000132 RID: 306
        F22,
        // Token: 0x04000133 RID: 307
        F23,
        // Token: 0x04000134 RID: 308
        F24,
        // Token: 0x04000135 RID: 309
        F3 = 114,
        // Token: 0x04000136 RID: 310
        F4,
        // Token: 0x04000137 RID: 311
        F5,
        // Token: 0x04000138 RID: 312
        F6,
        // Token: 0x04000139 RID: 313
        F7,
        // Token: 0x0400013A RID: 314
        F8,
        // Token: 0x0400013B RID: 315
        F9,
        // Token: 0x0400013C RID: 316
        OEM_1 = 186,
        // Token: 0x0400013D RID: 317
        OEM_102 = 226,
        // Token: 0x0400013E RID: 318
        OEM_2 = 191,
        // Token: 0x0400013F RID: 319
        OEM_3,
        // Token: 0x04000140 RID: 320
        OEM_4 = 219,
        // Token: 0x04000141 RID: 321
        OEM_5,
        // Token: 0x04000142 RID: 322
        OEM_6,
        // Token: 0x04000143 RID: 323
        OEM_7,
        // Token: 0x04000144 RID: 324
        OEM_8,
        // Token: 0x04000145 RID: 325
        OEM_CLEAR = 254,
        // Token: 0x04000146 RID: 326
        OEM_COMMA = 188,
        // Token: 0x04000147 RID: 327
        OEM_MINUS,
        // Token: 0x04000148 RID: 328
        OEM_PERIOD,
        // Token: 0x04000149 RID: 329
        OEM_PLUS = 187,
        // Token: 0x0400014A RID: 330
        KEY_0 = 48,
        // Token: 0x0400014B RID: 331
        KEY_1,
        // Token: 0x0400014C RID: 332
        KEY_2,
        // Token: 0x0400014D RID: 333
        KEY_3,
        // Token: 0x0400014E RID: 334
        KEY_4,
        // Token: 0x0400014F RID: 335
        KEY_5,
        // Token: 0x04000150 RID: 336
        KEY_6,
        // Token: 0x04000151 RID: 337
        KEY_7,
        // Token: 0x04000152 RID: 338
        KEY_8,
        // Token: 0x04000153 RID: 339
        KEY_9,
        // Token: 0x04000154 RID: 340
        KEY_A = 65,
        // Token: 0x04000155 RID: 341
        KEY_B,
        // Token: 0x04000156 RID: 342
        KEY_C,
        // Token: 0x04000157 RID: 343
        KEY_D,
        // Token: 0x04000158 RID: 344
        KEY_E,
        // Token: 0x04000159 RID: 345
        KEY_F,
        // Token: 0x0400015A RID: 346
        KEY_G,
        // Token: 0x0400015B RID: 347
        KEY_H,
        // Token: 0x0400015C RID: 348
        KEY_I,
        // Token: 0x0400015D RID: 349
        KEY_J,
        // Token: 0x0400015E RID: 350
        KEY_K,
        // Token: 0x0400015F RID: 351
        KEY_L,
        // Token: 0x04000160 RID: 352
        KEY_M,
        // Token: 0x04000161 RID: 353
        KEY_N,
        // Token: 0x04000162 RID: 354
        KEY_O,
        // Token: 0x04000163 RID: 355
        KEY_P,
        // Token: 0x04000164 RID: 356
        KEY_Q,
        // Token: 0x04000165 RID: 357
        KEY_R,
        // Token: 0x04000166 RID: 358
        KEY_S,
        // Token: 0x04000167 RID: 359
        KEY_T,
        // Token: 0x04000168 RID: 360
        KEY_U,
        // Token: 0x04000169 RID: 361
        KEY_V,
        // Token: 0x0400016A RID: 362
        KEY_W,
        // Token: 0x0400016B RID: 363
        KEY_X,
        // Token: 0x0400016C RID: 364
        KEY_Y,
        // Token: 0x0400016D RID: 365
        KEY_Z,
        // Token: 0x0400016E RID: 366
        VOLUME_DOWN = 174,
        // Token: 0x0400016F RID: 367
        VOLUME_MUTE = 173,
        // Token: 0x04000170 RID: 368
        VOLUME_UP = 175,
        // Token: 0x04000171 RID: 369
        SNAPSHOT = 44,
        // Token: 0x04000172 RID: 370
        RightClick = 93,
        // Token: 0x04000173 RID: 371
        BACKSPACE = 8,
        // Token: 0x04000174 RID: 372
        CANCEL = 3,
        // Token: 0x04000175 RID: 373
        CAPS_LOCK = 20,
        // Token: 0x04000176 RID: 374
        CONTROL = 17,
        // Token: 0x04000177 RID: 375
        ALT,
        // Token: 0x04000178 RID: 376
        DECIMAL = 110,
        // Token: 0x04000179 RID: 377
        DELETE = 46,
        // Token: 0x0400017A RID: 378
        DOWN = 40,
        // Token: 0x0400017B RID: 379
        END = 35,
        // Token: 0x0400017C RID: 380
        ESC = 27,
        // Token: 0x0400017D RID: 381
        HOME = 36,
        // Token: 0x0400017E RID: 382
        INSERT = 45,
        // Token: 0x0400017F RID: 383
        LAUNCH_APP1 = 182,
        // Token: 0x04000180 RID: 384
        LAUNCH_APP2,
        // Token: 0x04000181 RID: 385
        LAUNCH_MAIL = 180,
        // Token: 0x04000182 RID: 386
        LAUNCH_MEDIA_SELECT,
        // Token: 0x04000183 RID: 387
        LCONTROL = 162,
        // Token: 0x04000184 RID: 388
        LEFT = 37,
        // Token: 0x04000185 RID: 389
        LSHIFT = 160,
        // Token: 0x04000186 RID: 390
        LWIN = 91,
        // Token: 0x04000187 RID: 391
        PAGEDOWN = 34,
        // Token: 0x04000188 RID: 392
        NUMLOCK = 144,
        // Token: 0x04000189 RID: 393
        PAGE_UP = 33,
        // Token: 0x0400018A RID: 394
        RCONTROL = 163,
        // Token: 0x0400018B RID: 395
        ENTER = 13,
        // Token: 0x0400018C RID: 396
        RIGHT = 39,
        // Token: 0x0400018D RID: 397
        RSHIFT = 161,
        // Token: 0x0400018E RID: 398
        RWIN = 92,
        // Token: 0x0400018F RID: 399
        SHIFT = 16,
        // Token: 0x04000190 RID: 400
        SPACE_BAR = 32,
        // Token: 0x04000191 RID: 401
        TAB = 9,
        // Token: 0x04000192 RID: 402
        UP = 38
    }

    public enum VKeys
    {
        // Token: 0x0400007B RID: 123
        VK_A_Cong = 64,
        // Token: 0x0400007C RID: 124
        VK_LBUTTON = 1,
        // Token: 0x0400007D RID: 125
        VK_RBUTTON,
        // Token: 0x0400007E RID: 126
        VK_CANCEL,
        // Token: 0x0400007F RID: 127
        VK_MBUTTON,
        // Token: 0x04000080 RID: 128
        VK_BACK = 8,
        // Token: 0x04000081 RID: 129
        VK_TAB,
        // Token: 0x04000082 RID: 130
        VK_CLEAR = 12,
        // Token: 0x04000083 RID: 131
        VK_RETURN,
        // Token: 0x04000084 RID: 132
        VK_SHIFT = 16,
        // Token: 0x04000085 RID: 133
        VK_CONTROL,
        // Token: 0x04000086 RID: 134
        VK_MENU,
        // Token: 0x04000087 RID: 135
        VK_PAUSE,
        // Token: 0x04000088 RID: 136
        VK_CAPITAL,
        // Token: 0x04000089 RID: 137
        VK_ESCAPE = 27,
        // Token: 0x0400008A RID: 138
        VK_SPACE = 32,
        // Token: 0x0400008B RID: 139
        VK_PRIOR,
        // Token: 0x0400008C RID: 140
        VK_NEXT,
        // Token: 0x0400008D RID: 141
        VK_END,
        // Token: 0x0400008E RID: 142
        VK_HOME,
        // Token: 0x0400008F RID: 143
        VK_LEFT,
        // Token: 0x04000090 RID: 144
        VK_UP,
        // Token: 0x04000091 RID: 145
        VK_RIGHT,
        // Token: 0x04000092 RID: 146
        VK_DOWN,
        // Token: 0x04000093 RID: 147
        VK_SELECT,
        // Token: 0x04000094 RID: 148
        VK_PRINT,
        // Token: 0x04000095 RID: 149
        VK_EXECUTE,
        // Token: 0x04000096 RID: 150
        VK_SNAPSHOT,
        // Token: 0x04000097 RID: 151
        VK_INSERT,
        // Token: 0x04000098 RID: 152
        VK_DELETE,
        // Token: 0x04000099 RID: 153
        VK_HELP,
        // Token: 0x0400009A RID: 154
        VK_0,
        // Token: 0x0400009B RID: 155
        VK_1,
        // Token: 0x0400009C RID: 156
        VK_2,
        // Token: 0x0400009D RID: 157
        VK_3,
        // Token: 0x0400009E RID: 158
        VK_4,
        // Token: 0x0400009F RID: 159
        VK_5,
        // Token: 0x040000A0 RID: 160
        VK_6,
        // Token: 0x040000A1 RID: 161
        VK_7,
        // Token: 0x040000A2 RID: 162
        VK_8,
        // Token: 0x040000A3 RID: 163
        VK_9,
        // Token: 0x040000A4 RID: 164
        VK_A = 65,
        // Token: 0x040000A5 RID: 165
        VK_B,
        // Token: 0x040000A6 RID: 166
        VK_C,
        // Token: 0x040000A7 RID: 167
        VK_D,
        // Token: 0x040000A8 RID: 168
        VK_E,
        // Token: 0x040000A9 RID: 169
        VK_F,
        // Token: 0x040000AA RID: 170
        VK_G,
        // Token: 0x040000AB RID: 171
        VK_H,
        // Token: 0x040000AC RID: 172
        VK_I,
        // Token: 0x040000AD RID: 173
        VK_J,
        // Token: 0x040000AE RID: 174
        VK_K,
        // Token: 0x040000AF RID: 175
        VK_L,
        // Token: 0x040000B0 RID: 176
        VK_M,
        // Token: 0x040000B1 RID: 177
        VK_N,
        // Token: 0x040000B2 RID: 178
        VK_O,
        // Token: 0x040000B3 RID: 179
        VK_P,
        // Token: 0x040000B4 RID: 180
        VK_Q,
        // Token: 0x040000B5 RID: 181
        VK_R,
        // Token: 0x040000B6 RID: 182
        VK_S,
        // Token: 0x040000B7 RID: 183
        VK_T,
        // Token: 0x040000B8 RID: 184
        VK_U,
        // Token: 0x040000B9 RID: 185
        VK_V,
        // Token: 0x040000BA RID: 186
        VK_W,
        // Token: 0x040000BB RID: 187
        VK_X,
        // Token: 0x040000BC RID: 188
        VK_Y,
        // Token: 0x040000BD RID: 189
        VK_Z,
        // Token: 0x040000BE RID: 190
        VK_NUMPAD0 = 96,
        // Token: 0x040000BF RID: 191
        VK_NUMPAD1,
        // Token: 0x040000C0 RID: 192
        VK_NUMPAD2,
        // Token: 0x040000C1 RID: 193
        VK_NUMPAD3,
        // Token: 0x040000C2 RID: 194
        VK_NUMPAD4,
        // Token: 0x040000C3 RID: 195
        VK_NUMPAD5,
        // Token: 0x040000C4 RID: 196
        VK_NUMPAD6,
        // Token: 0x040000C5 RID: 197
        VK_NUMPAD7,
        // Token: 0x040000C6 RID: 198
        VK_NUMPAD8,
        // Token: 0x040000C7 RID: 199
        VK_NUMPAD9,
        // Token: 0x040000C8 RID: 200
        VK_SEPARATOR = 108,
        // Token: 0x040000C9 RID: 201
        VK_SUBTRACT,
        // Token: 0x040000CA RID: 202
        VK_DECIMAL,
        // Token: 0x040000CB RID: 203
        VK_DIVIDE,
        // Token: 0x040000CC RID: 204
        VK_F1,
        // Token: 0x040000CD RID: 205
        VK_F2,
        // Token: 0x040000CE RID: 206
        VK_F3,
        // Token: 0x040000CF RID: 207
        VK_F4,
        // Token: 0x040000D0 RID: 208
        VK_F5,
        // Token: 0x040000D1 RID: 209
        VK_F6,
        // Token: 0x040000D2 RID: 210
        VK_F7,
        // Token: 0x040000D3 RID: 211
        VK_F8,
        // Token: 0x040000D4 RID: 212
        VK_F9,
        // Token: 0x040000D5 RID: 213
        VK_F10,
        // Token: 0x040000D6 RID: 214
        VK_F11,
        // Token: 0x040000D7 RID: 215
        VK_F12,
        // Token: 0x040000D8 RID: 216
        VK_SCROLL = 145,
        // Token: 0x040000D9 RID: 217
        VK_LSHIFT = 160,
        // Token: 0x040000DA RID: 218
        VK_RSHIFT,
        // Token: 0x040000DB RID: 219
        VK_LCONTROL,
        // Token: 0x040000DC RID: 220
        VK_RCONTROL,
        // Token: 0x040000DD RID: 221
        VK_LMENU,
        // Token: 0x040000DE RID: 222
        VK_RMENU,
        // Token: 0x040000DF RID: 223
        VK_PLAY = 250,
        // Token: 0x040000E0 RID: 224
        VK_ZOOM,
        // Token: 0x040000E1 RID: 225
        BM_CLICK = 245,
        // Token: 0x040000E2 RID: 226
        VK_OEM_1 = 186,
        // Token: 0x040000E3 RID: 227
        VK_OEM_PLUS,
        // Token: 0x040000E4 RID: 228
        VK_OEM_COMMA,
        // Token: 0x040000E5 RID: 229
        VK_OEM_MINUS,
        // Token: 0x040000E6 RID: 230
        VK_OEM_PERIOD,
        // Token: 0x040000E7 RID: 231
        VK_OEM_2,
        // Token: 0x040000E8 RID: 232
        VK_OEM_3,
        // Token: 0x040000E9 RID: 233
        VK_OEM_4 = 219,
        // Token: 0x040000EA RID: 234
        VK_OEM_5,
        // Token: 0x040000EB RID: 235
        VK_OEM_6,
        // Token: 0x040000EC RID: 236
        VK_OEM_7,
        // Token: 0x040000ED RID: 237
        VK_OEM_8
    }

    // Token: 0x0200000B RID: 11
    public struct RECT
    {
        // Token: 0x040000F3 RID: 243
        public int Left;

        // Token: 0x040000F4 RID: 244
        public int Top;

        // Token: 0x040000F5 RID: 245
        public int Right;

        // Token: 0x040000F6 RID: 246
        public int Bottom;
    }
    public enum EMouseKey
    {
        // Token: 0x040000EF RID: 239
        LEFT,
        // Token: 0x040000F0 RID: 240
        RIGHT,
        // Token: 0x040000F1 RID: 241
        DOUBLE_LEFT,
        // Token: 0x040000F2 RID: 242
        DOUBLE_RIGHT
    }
    public struct INPUT
    {
        // Token: 0x040000F7 RID: 247
        public uint Type;

        // Token: 0x040000F8 RID: 248
        public MOUSEKEYBDHARDWAREINPUT Data;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSEKEYBDHARDWAREINPUT
    {
        // Token: 0x040000F9 RID: 249
        [FieldOffset(0)]
        public HARDWAREINPUT Hardware;

        // Token: 0x040000FA RID: 250
        [FieldOffset(0)]
        public KEYBDINPUT Keyboard;

        // Token: 0x040000FB RID: 251
        [FieldOffset(0)]
        public MOUSEINPUT Mouse;
    }
    // Token: 0x02000010 RID: 16
    public struct MOUSEINPUT
    {
        // Token: 0x04000104 RID: 260
        public int X;

        // Token: 0x04000105 RID: 261
        public int Y;

        // Token: 0x04000106 RID: 262
        public uint MouseData;

        // Token: 0x04000107 RID: 263
        public uint Flags;

        // Token: 0x04000108 RID: 264
        public uint Time;

        // Token: 0x04000109 RID: 265
        public IntPtr ExtraInfo;
    }
    public struct HARDWAREINPUT
    {
        // Token: 0x040000FC RID: 252
        public uint Msg;

        // Token: 0x040000FD RID: 253
        public ushort ParamL;

        // Token: 0x040000FE RID: 254
        public ushort ParamH;
    }
    public struct KEYBDINPUT
    {
        // Token: 0x040000FF RID: 255
        public ushort Vk;

        // Token: 0x04000100 RID: 256
        public ushort Scan;

        // Token: 0x04000101 RID: 257
        public uint Flags;

        // Token: 0x04000102 RID: 258
        public uint Time;

        // Token: 0x04000103 RID: 259
        public IntPtr ExtraInfo;
    }
    public class AutoControl
    {
        // Token: 0x06000020 RID: 32
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Token: 0x06000021 RID: 33
        [DllImport("User32.dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, AutoControl.CallBack lpEnumFunc, IntPtr lParam);

        // Token: 0x06000022 RID: 34
        [DllImport("User32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder s, int nMaxCount);

        // Token: 0x06000023 RID: 35
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        // Token: 0x06000024 RID: 36
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        // Token: 0x06000025 RID: 37
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // Token: 0x06000026 RID: 38
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        // Token: 0x06000027 RID: 39
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        // Token: 0x06000028 RID: 40
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        // Token: 0x06000029 RID: 41
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        // Token: 0x0600002A RID: 42
        [DllImport("user32.dll")]
        private static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);

        // Token: 0x0600002B RID: 43
        [DllImport("user32.dll")]
        private static extern bool SetDlgItemTextA(IntPtr hWnd, int nIDDlgItem, string gchar);

        // Token: 0x0600002C RID: 44
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        // Token: 0x0600002D RID: 45
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        // Token: 0x0600002E RID: 46
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        // Token: 0x0600002F RID: 47
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // Token: 0x06000030 RID: 48
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        // Token: 0x06000031 RID: 49
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, AutoControl.EnumWindowProc callback, IntPtr lParam);

        // Token: 0x06000032 RID: 50
        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        // Token: 0x06000033 RID: 51
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        // Token: 0x06000034 RID: 52
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

        // Token: 0x06000035 RID: 53
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        // Token: 0x06000036 RID: 54 RVA: 0x00002EA8 File Offset: 0x000010A8
        public static IntPtr BringToFront(string className, string windowName = null)
        {
            IntPtr intPtr = AutoControl.FindWindow(className, windowName);
            AutoControl.SetForegroundWindow(intPtr);
            return intPtr;
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00002ECC File Offset: 0x000010CC
        public static IntPtr BringToFront(IntPtr hWnd)
        {
            AutoControl.SetForegroundWindow(hWnd);
            return hWnd;
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00002EE8 File Offset: 0x000010E8
        public static bool IsWindowVisible_(IntPtr handle)
        {
            return AutoControl.IsWindowVisible(handle);
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00002F00 File Offset: 0x00001100
        public static IntPtr FindWindowHandle(string className, string windowName)
        {
            IntPtr zero = IntPtr.Zero;
            return AutoControl.FindWindow(className, windowName);
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00002F24 File Offset: 0x00001124
        public static List<IntPtr> FindWindowHandlesFromProcesses(string className, string windowName, int maxCount = 1)
        {
            Process[] processes = Process.GetProcesses();
            List<IntPtr> list = new List<IntPtr>();
            int num = 0;
            foreach (Process process in from p in processes
                                        where p.MainWindowHandle != IntPtr.Zero
                                        select p)
            {
                IntPtr mainWindowHandle = process.MainWindowHandle;
                string className2 = AutoControl.GetClassName(mainWindowHandle);
                string text = AutoControl.GetText(mainWindowHandle);
                bool flag = className2 == className || text == windowName;
                if (flag)
                {
                    list.Add(mainWindowHandle);
                    bool flag2 = num >= maxCount;
                    if (flag2)
                    {
                        break;
                    }
                    num++;
                }
            }
            return list;
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00002FF8 File Offset: 0x000011F8
        public static IntPtr FindWindowHandleFromProcesses(string className, string windowName)
        {
            Process[] processes = Process.GetProcesses();
            IntPtr result = IntPtr.Zero;
            foreach (Process process in from p in processes
                                        where p.MainWindowHandle != IntPtr.Zero
                                        select p)
            {
                IntPtr mainWindowHandle = process.MainWindowHandle;
                string className2 = AutoControl.GetClassName(mainWindowHandle);
                string text = AutoControl.GetText(mainWindowHandle);
                bool flag = className2 == className || text == windowName;
                if (flag)
                {
                    result = mainWindowHandle;
                    break;
                }
            }
            return result;
        }

        // Token: 0x0600003C RID: 60 RVA: 0x000030B0 File Offset: 0x000012B0
        public static IntPtr FindWindowExFromParent(IntPtr parentHandle, string text, string className)
        {
            return AutoControl.FindWindowEx(parentHandle, IntPtr.Zero, className, text);
        }

        // Token: 0x0600003D RID: 61 RVA: 0x000030D0 File Offset: 0x000012D0
        private static IntPtr FindWindowByIndex(IntPtr hWndParent, int index)
        {
            bool flag = index == 0;
            IntPtr result;
            if (flag)
            {
                result = hWndParent;
            }
            else
            {
                int num = 0;
                IntPtr intPtr = IntPtr.Zero;
                do
                {
                    intPtr = AutoControl.FindWindowEx(hWndParent, intPtr, "Button", null);
                    bool flag2 = intPtr != IntPtr.Zero;
                    if (flag2)
                    {
                        num++;
                    }
                }
                while (num < index && intPtr != IntPtr.Zero);
                result = intPtr;
            }
            return result;
        }

        // Token: 0x0600003E RID: 62 RVA: 0x00003138 File Offset: 0x00001338
        public static IntPtr GetControlHandleFromControlID(IntPtr parentHandle, int controlId)
        {
            return AutoControl.GetDlgItem(parentHandle, controlId);
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00003154 File Offset: 0x00001354
        public static List<IntPtr> GetChildHandle(IntPtr parentHandle)
        {
            List<IntPtr> list = new List<IntPtr>();
            GCHandle value = GCHandle.Alloc(list);
            IntPtr lParam2 = GCHandle.ToIntPtr(value);
            try
            {
                AutoControl.EnumWindowProc callback = delegate (IntPtr hWnd, IntPtr lParam)
                {
                    GCHandle gchandle = GCHandle.FromIntPtr(lParam);
                    bool flag = gchandle.Target == null;
                    bool result;
                    if (flag)
                    {
                        result = false;
                    }
                    else
                    {
                        List<IntPtr> list2 = gchandle.Target as List<IntPtr>;
                        list2.Add(hWnd);
                        result = true;
                    }
                    return result;
                };
                AutoControl.EnumChildWindows(parentHandle, callback, lParam2);
            }
            finally
            {
                value.Free();
            }
            return list;
        }

        // Token: 0x06000040 RID: 64 RVA: 0x000031C8 File Offset: 0x000013C8
        public static IntPtr FindHandleWithText(List<IntPtr> handles, string className, string text)
        {
            return handles.Find(delegate (IntPtr ptr)
            {
                string className2 = AutoControl.GetClassName(ptr);
                string text2 = AutoControl.GetText(ptr);
                return className2 == className || text2 == text;
            });
        }

        // Token: 0x06000041 RID: 65 RVA: 0x00003204 File Offset: 0x00001404
        public static List<IntPtr> FindHandlesWithText(List<IntPtr> handles, string className, string text)
        {
            List<IntPtr> list = new List<IntPtr>();
            IEnumerable<IntPtr> source = handles.Where(delegate (IntPtr ptr)
            {
                string className2 = AutoControl.GetClassName(ptr);
                string text2 = AutoControl.GetText(ptr);
                return className2 == className || text2 == text;
            });
            return source.ToList<IntPtr>();
        }

        // Token: 0x06000042 RID: 66 RVA: 0x0000324C File Offset: 0x0000144C
        public static IntPtr FindHandle(IntPtr parentHandle, string className, string text)
        {
            return AutoControl.FindHandleWithText(AutoControl.GetChildHandle(parentHandle), className, text);
        }

        // Token: 0x06000043 RID: 67 RVA: 0x0000326C File Offset: 0x0000146C
        public static List<IntPtr> FindHandles(IntPtr parentHandle, string className, string text)
        {
            return AutoControl.FindHandlesWithText(AutoControl.GetChildHandle(parentHandle), className, text);
        }

        // Token: 0x06000044 RID: 68 RVA: 0x0000328C File Offset: 0x0000148C
        public static bool CallbackChild(IntPtr hWnd, IntPtr lParam)
        {
            string text = AutoControl.GetText(hWnd);
            string className = AutoControl.GetClassName(hWnd);
            bool flag = text == "&Options >>" && className.StartsWith("ToolbarWindow32");
            bool result;
            if (flag)
            {
                AutoControl.SendMessage(hWnd, 0, IntPtr.Zero, IntPtr.Zero);
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        // Token: 0x06000045 RID: 69 RVA: 0x000032E4 File Offset: 0x000014E4
        public static void SendClickOnControlById(IntPtr parentHWND, int controlID)
        {
            IntPtr dlgItem = AutoControl.GetDlgItem(parentHWND, controlID);
            int wParam = 0 | (controlID & 65535);
            AutoControl.SendMessage(parentHWND, 273, wParam, dlgItem);
        }

        // Token: 0x06000046 RID: 70 RVA: 0x00003312 File Offset: 0x00001512
        public static void SendClickOnControlByHandle(IntPtr hWndButton)
        {
            AutoControl.SendMessage(hWndButton, 513, IntPtr.Zero, IntPtr.Zero);
            AutoControl.SendMessage(hWndButton, 514, IntPtr.Zero, IntPtr.Zero);
        }

        // Token: 0x06000047 RID: 71 RVA: 0x00003344 File Offset: 0x00001544
        public static void SendClickOnPosition(IntPtr controlHandle, int x, int y, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
        {
            int msg = 0;
            int msg2 = 0;
            bool flag = mouseButton == EMouseKey.LEFT;
            if (flag)
            {
                msg = 513;
                msg2 = 514;
            }
            bool flag2 = mouseButton == EMouseKey.RIGHT;
            if (flag2)
            {
                msg = 516;
                msg2 = 517;
            }
            IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
            bool flag3 = mouseButton == EMouseKey.LEFT || mouseButton == EMouseKey.RIGHT;
            if (flag3)
            {
                for (int i = 0; i < clickTimes; i++)
                {
                    AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
                    AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
                    AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam);
                }
            }
            else
            {
                bool flag4 = mouseButton == EMouseKey.DOUBLE_LEFT;
                if (flag4)
                {
                    msg = 515;
                    msg2 = 514;
                }
                bool flag5 = mouseButton == EMouseKey.DOUBLE_RIGHT;
                if (flag5)
                {
                    msg = 518;
                    msg2 = 517;
                }
                AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
                AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam);
            }
        }

        // Token: 0x06000048 RID: 72 RVA: 0x00003438 File Offset: 0x00001638
        public static void SendDragAndDropOnPosition(IntPtr controlHandle, int x, int y, int x2, int y2, int stepx = 10, int stepy = 10, double delay = 0.05)
        {
            int msg = 513;
            int msg2 = 514;
            IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
            IntPtr lParam2 = AutoControl.MakeLParamFromXY(x2, y2);
            bool flag = x2 < x;
            if (flag)
            {
                stepx *= -1;
            }
            bool flag2 = y2 < y;
            if (flag2)
            {
                stepy *= -1;
            }
            AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
            AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
            bool flag3 = false;
            bool flag4 = false;
            for (; ; )
            {
                AutoControl.PostMessage(controlHandle, 512, new IntPtr(1), AutoControl.MakeLParamFromXY(x, y));
                bool flag5 = stepx > 0;
                if (flag5)
                {
                    bool flag6 = x < x2;
                    if (flag6)
                    {
                        x += stepx;
                    }
                    else
                    {
                        flag3 = true;
                    }
                }
                else
                {
                    bool flag7 = x > x2;
                    if (flag7)
                    {
                        x += stepx;
                    }
                    else
                    {
                        flag3 = true;
                    }
                }
                bool flag8 = stepy > 0;
                if (flag8)
                {
                    bool flag9 = y < y2;
                    if (flag9)
                    {
                        y += stepy;
                    }
                    else
                    {
                        flag4 = true;
                    }
                }
                else
                {
                    bool flag10 = y > y2;
                    if (flag10)
                    {
                        y += stepy;
                    }
                    else
                    {
                        flag4 = true;
                    }
                }
                bool flag11 = flag3 && flag4;
                if (flag11)
                {
                    break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(delay));
            }
            AutoControl.PostMessage(controlHandle, msg2, new IntPtr(0), lParam2);
        }

        // Token: 0x06000049 RID: 73 RVA: 0x0000358C File Offset: 0x0000178C
        public static void SendClickDownOnPosition(IntPtr controlHandle, int x, int y, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
        {
            int msg = 0;
            bool flag = mouseButton == EMouseKey.LEFT;
            if (flag)
            {
                msg = 513;
            }
            bool flag2 = mouseButton == EMouseKey.RIGHT;
            if (flag2)
            {
                msg = 516;
            }
            IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
            for (int i = 0; i < clickTimes; i++)
            {
                AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
                AutoControl.PostMessage(controlHandle, msg, new IntPtr(1), lParam);
            }
        }

        // Token: 0x0600004A RID: 74 RVA: 0x000035FC File Offset: 0x000017FC
        public static void SendClickUpOnPosition(IntPtr controlHandle, int x, int y, EMouseKey mouseButton = EMouseKey.LEFT, int clickTimes = 1)
        {
            int msg = 0;
            bool flag = mouseButton == EMouseKey.LEFT;
            if (flag)
            {
                msg = 514;
            }
            bool flag2 = mouseButton == EMouseKey.RIGHT;
            if (flag2)
            {
                msg = 517;
            }
            IntPtr lParam = AutoControl.MakeLParamFromXY(x, y);
            for (int i = 0; i < clickTimes; i++)
            {
                AutoControl.PostMessage(controlHandle, 6, new IntPtr(1), lParam);
                AutoControl.SendMessage(controlHandle, msg, new IntPtr(0), lParam);
            }
        }

        // Token: 0x0600004B RID: 75 RVA: 0x0000366B File Offset: 0x0000186B
        public static void SendText(IntPtr handle, string text)
        {
            AutoControl.SendMessage(handle, 12, 0, text);
        }

        // Token: 0x0600004C RID: 76 RVA: 0x0000367C File Offset: 0x0000187C
        public static void SendKeyBoardPress(IntPtr handle, VKeys key)
        {
            AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
            AutoControl.PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(1));
            AutoControl.PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
        }

        // Token: 0x0600004D RID: 77 RVA: 0x000036D0 File Offset: 0x000018D0
        public static void SendKeyBoardPressStepByStep(IntPtr handle, string message, float delay = 0.1f)
        {
            foreach (char c in message.ToLower())
            {
                VKeys key = VKeys.VK_0;
                char c2 = c;
                switch (c2)
                {
                    case '0':
                        key = VKeys.VK_0;
                        break;
                    case '1':
                        key = VKeys.VK_1;
                        break;
                    case '2':
                        key = VKeys.VK_2;
                        break;
                    case '3':
                        key = VKeys.VK_3;
                        break;
                    case '4':
                        key = VKeys.VK_4;
                        break;
                    case '5':
                        key = VKeys.VK_5;
                        break;
                    case '6':
                        key = VKeys.VK_6;
                        break;
                    case '7':
                        key = VKeys.VK_7;
                        break;
                    case '8':
                        key = VKeys.VK_8;
                        break;
                    case '9':
                        key = VKeys.VK_9;
                        break;
                    default:
                        switch (c2)
                        {
                            case 'a':
                                key = VKeys.VK_A;
                                break;
                            case 'b':
                                key = VKeys.VK_B;
                                break;
                            case 'c':
                                key = VKeys.VK_V;
                                break;
                            case 'd':
                                key = VKeys.VK_D;
                                break;
                            case 'e':
                                key = VKeys.VK_E;
                                break;
                            case 'f':
                                key = VKeys.VK_F;
                                break;
                            case 'g':
                                key = VKeys.VK_G;
                                break;
                            case 'h':
                                key = VKeys.VK_H;
                                break;
                            case 'i':
                                key = VKeys.VK_I;
                                break;
                            case 'j':
                                key = VKeys.VK_J;
                                break;
                            case 'k':
                                key = VKeys.VK_K;
                                break;
                            case 'l':
                                key = VKeys.VK_L;
                                break;
                            case 'm':
                                key = VKeys.VK_M;
                                break;
                            case 'n':
                                key = VKeys.VK_N;
                                break;
                            case 'o':
                                key = VKeys.VK_O;
                                break;
                            case 'p':
                                key = VKeys.VK_P;
                                break;
                            case 'q':
                                key = VKeys.VK_Q;
                                break;
                            case 'r':
                                key = VKeys.VK_R;
                                break;
                            case 's':
                                key = VKeys.VK_S;
                                break;
                            case 't':
                                key = VKeys.VK_T;
                                break;
                            case 'u':
                                key = VKeys.VK_U;
                                break;
                            case 'v':
                                key = VKeys.VK_V;
                                break;
                            case 'w':
                                key = VKeys.VK_W;
                                break;
                            case 'x':
                                key = VKeys.VK_X;
                                break;
                            case 'y':
                                key = VKeys.VK_Y;
                                break;
                            case 'z':
                                key = VKeys.VK_Z;
                                break;
                        }
                        break;
                }
                AutoControl.SendKeyBoardPress(handle, key);
                Thread.Sleep(TimeSpan.FromSeconds((double)delay));
            }
        }

        // Token: 0x0600004E RID: 78 RVA: 0x000038A1 File Offset: 0x00001AA1
        public static void SendKeyBoardUp(IntPtr handle, VKeys key)
        {
            AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
            AutoControl.PostMessage(handle, 257, new IntPtr((int)key), new IntPtr(0));
        }

        // Token: 0x0600004F RID: 79 RVA: 0x000038D0 File Offset: 0x00001AD0
        public static void SendKeyChar(IntPtr handle, VKeys key)
        {
            AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
            AutoControl.PostMessage(handle, 258, new IntPtr((int)key), new IntPtr(0));
        }

        // Token: 0x06000050 RID: 80 RVA: 0x000038D0 File Offset: 0x00001AD0
        public static void SendKeyChar(IntPtr handle, int key)
        {
            AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
            AutoControl.PostMessage(handle, 258, new IntPtr(key), new IntPtr(0));
        }

        // Token: 0x06000051 RID: 81 RVA: 0x000038FF File Offset: 0x00001AFF
        public static void SendKeyBoardDown(IntPtr handle, VKeys key)
        {
            AutoControl.PostMessage(handle, 6, new IntPtr(1), new IntPtr(0));
            AutoControl.PostMessage(handle, 256, new IntPtr((int)key), new IntPtr(0));
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00003930 File Offset: 0x00001B30
        public static void SendTextKeyBoard(IntPtr handle, string text, float delay = 0.1f)
        {
            foreach (char key in text.ToLower())
            {
                AutoControl.SendKeyChar(handle, (int)key);
            }
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00003969 File Offset: 0x00001B69
        public static void SendKeyFocus(KeyCode key)
        {
            AutoControl.SendKeyPress(key);
        }

        // Token: 0x06000054 RID: 84 RVA: 0x00003974 File Offset: 0x00001B74
        public static void SendMultiKeysFocus(KeyCode[] keys)
        {
            foreach (KeyCode keyCode in keys)
            {
                AutoControl.SendKeyDown(keyCode);
            }
            foreach (KeyCode keyCode2 in keys)
            {
                AutoControl.SendKeyUp(keyCode2);
            }
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000039C5 File Offset: 0x00001BC5
        public static void SendStringFocus(string message)
        {
            Clipboard.SetText(message);
            AutoControl.SendMultiKeysFocus(new KeyCode[]
            {
                KeyCode.CONTROL,
                KeyCode.KEY_V
            });
        }

        // Token: 0x06000056 RID: 86 RVA: 0x000039E8 File Offset: 0x00001BE8
        public static void SendKeyPress(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1U
            };
            input.Data.Keyboard = new KEYBDINPUT
            {
                Vk = (ushort)keyCode,
                Scan = 0,
                Flags = 0U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            INPUT input2 = new INPUT
            {
                Type = 1U
            };
            input2.Data.Keyboard = new KEYBDINPUT
            {
                Vk = (ushort)keyCode,
                Scan = 0,
                Flags = 2U,
                Time = 0U,
                ExtraInfo = IntPtr.Zero
            };
            INPUT[] inputs = new INPUT[]
            {
                input,
                input2
            };
            bool flag = AutoControl.SendInput(2U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
            if (flag)
            {
                throw new Exception();
            }
        }

        // Token: 0x06000057 RID: 87 RVA: 0x00003ADC File Offset: 0x00001CDC
        public static void SendKeyPressStepByStep(string message, double delay = 0.5)
        {
            for (int i = 0; i < message.Length; i++)
            {
                switch (message[i])
                {
                    case '0':
                        AutoControl.SendKeyPress(KeyCode.KEY_0);
                        break;
                    case '1':
                        AutoControl.SendKeyPress(KeyCode.KEY_1);
                        break;
                    case '2':
                        AutoControl.SendKeyPress(KeyCode.KEY_2);
                        break;
                    case '3':
                        AutoControl.SendKeyPress(KeyCode.KEY_3);
                        break;
                    case '4':
                        AutoControl.SendKeyPress(KeyCode.KEY_4);
                        break;
                    case '5':
                        AutoControl.SendKeyPress(KeyCode.KEY_5);
                        break;
                    case '6':
                        AutoControl.SendKeyPress(KeyCode.KEY_6);
                        break;
                    case '7':
                        AutoControl.SendKeyPress(KeyCode.KEY_7);
                        break;
                    case '8':
                        AutoControl.SendKeyPress(KeyCode.KEY_8);
                        break;
                    case '9':
                        AutoControl.SendKeyPress(KeyCode.KEY_9);
                        break;
                }
                Thread.Sleep(TimeSpan.FromSeconds(delay));
            }
        }

        // Token: 0x06000058 RID: 88 RVA: 0x00003BB8 File Offset: 0x00001DB8
        public static void SendKeyDown(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1U
            };
            input.Data.Keyboard = default(KEYBDINPUT);
            input.Data.Keyboard.Vk = (ushort)keyCode;
            input.Data.Keyboard.Scan = 0;
            input.Data.Keyboard.Flags = 0U;
            input.Data.Keyboard.Time = 0U;
            input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
            INPUT[] inputs = new INPUT[]
            {
                input
            };
            bool flag = AutoControl.SendInput(1U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
            if (flag)
            {
                throw new Exception();
            }
        }

        // Token: 0x06000059 RID: 89 RVA: 0x00003C7C File Offset: 0x00001E7C
        public static void SendKeyUp(KeyCode keyCode)
        {
            INPUT input = new INPUT
            {
                Type = 1U
            };
            input.Data.Keyboard = default(KEYBDINPUT);
            input.Data.Keyboard.Vk = (ushort)keyCode;
            input.Data.Keyboard.Scan = 0;
            input.Data.Keyboard.Flags = 2U;
            input.Data.Keyboard.Time = 0U;
            input.Data.Keyboard.ExtraInfo = IntPtr.Zero;
            INPUT[] inputs = new INPUT[]
            {
                input
            };
            bool flag = AutoControl.SendInput(1U, inputs, Marshal.SizeOf(typeof(INPUT))) == 0U;
            if (flag)
            {
                throw new Exception();
            }
        }

        // Token: 0x0600005A RID: 90 RVA: 0x00003D3E File Offset: 0x00001F3E
        public static void MouseClick(int x, int y, EMouseKey mouseKey = EMouseKey.LEFT)
        {
            Cursor.Position = new Point(x, y);
            AutoControl.Click(mouseKey);
        }

        // Token: 0x0600005B RID: 91 RVA: 0x00003D58 File Offset: 0x00001F58
        public static void MouseDragX(Point startPoint, int deltaX, bool isNegative = false)
        {
            Cursor.Position = startPoint;
            AutoControl.mouse_event(2U, 0, 0, 0, UIntPtr.Zero);
            for (int i = 0; i < deltaX; i++)
            {
                bool flag = !isNegative;
                if (flag)
                {
                    AutoControl.mouse_event(1U, 1, 0, 0, UIntPtr.Zero);
                }
                else
                {
                    AutoControl.mouse_event(1U, -1, 0, 0, UIntPtr.Zero);
                }
            }
            AutoControl.mouse_event(32772U, 0, 0, 0, UIntPtr.Zero);
        }

        // Token: 0x0600005C RID: 92 RVA: 0x00003DD0 File Offset: 0x00001FD0
        public static void MouseDragY(Point startPoint, int deltaY, bool isNegative = false)
        {
            Cursor.Position = startPoint;
            AutoControl.mouse_event(2U, 0, 0, 0, UIntPtr.Zero);
            for (int i = 0; i < deltaY; i++)
            {
                bool flag = !isNegative;
                if (flag)
                {
                    AutoControl.mouse_event(1U, 0, 1, 0, UIntPtr.Zero);
                }
                else
                {
                    AutoControl.mouse_event(1U, 0, -1, 0, UIntPtr.Zero);
                }
            }
            AutoControl.mouse_event(32772U, 0, 0, 0, UIntPtr.Zero);
        }

        // Token: 0x0600005D RID: 93 RVA: 0x00003E45 File Offset: 0x00002045
        public static void MouseScroll(Point startPoint, int deltaY, bool isNegative = false)
        {
            Cursor.Position = startPoint;
            AutoControl.mouse_event(2048U, 0, 0, deltaY, UIntPtr.Zero);
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00003E62 File Offset: 0x00002062
        public static void MouseClick(Point point, EMouseKey mouseKey = EMouseKey.LEFT)
        {
            Cursor.Position = point;
            AutoControl.Click(mouseKey);
        }

        // Token: 0x0600005F RID: 95 RVA: 0x00003E74 File Offset: 0x00002074
        public static void Click(EMouseKey mouseKey = EMouseKey.LEFT)
        {
            switch (mouseKey)
            {
                case EMouseKey.LEFT:
                    AutoControl.mouse_event(32774U, 0, 0, 0, UIntPtr.Zero);
                    break;
                case EMouseKey.RIGHT:
                    AutoControl.mouse_event(32792U, 0, 0, 0, UIntPtr.Zero);
                    break;
                case EMouseKey.DOUBLE_LEFT:
                    AutoControl.mouse_event(32774U, 0, 0, 0, UIntPtr.Zero);
                    AutoControl.mouse_event(32774U, 0, 0, 0, UIntPtr.Zero);
                    break;
                case EMouseKey.DOUBLE_RIGHT:
                    AutoControl.mouse_event(32792U, 0, 0, 0, UIntPtr.Zero);
                    AutoControl.mouse_event(32792U, 0, 0, 0, UIntPtr.Zero);
                    break;
            }
        }

        // Token: 0x06000060 RID: 96 RVA: 0x00003F18 File Offset: 0x00002118
        public static RECT GetWindowRect(IntPtr hWnd)
        {
            RECT result = default(RECT);
            AutoControl.GetWindowRect(hWnd, ref result);
            return result;
        }

        // Token: 0x06000061 RID: 97 RVA: 0x00003F3C File Offset: 0x0000213C
        public static Point GetGlobalPoint(IntPtr hWnd, Point? point = null)
        {
            Point result = default(Point);
            RECT windowRect = AutoControl.GetWindowRect(hWnd);
            bool flag = point == null;
            if (flag)
            {
                point = new Point?(default(Point));
            }
            result.X = point.Value.X + windowRect.Left;
            result.Y = point.Value.Y + windowRect.Top;
            return result;
        }

        // Token: 0x06000062 RID: 98 RVA: 0x00003FBC File Offset: 0x000021BC
        public static Point GetGlobalPoint(IntPtr hWnd, int x = 0, int y = 0)
        {
            Point result = default(Point);
            RECT windowRect = AutoControl.GetWindowRect(hWnd);
            result.X = x + windowRect.Left;
            result.Y = y + windowRect.Top;
            return result;
        }

        // Token: 0x06000063 RID: 99 RVA: 0x00004000 File Offset: 0x00002200
        public static string GetText(IntPtr hWnd)
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            AutoControl.GetWindowText(hWnd, stringBuilder, 256);
            return stringBuilder.ToString().Trim();
        }

        // Token: 0x06000064 RID: 100 RVA: 0x00004038 File Offset: 0x00002238
        public static string GetClassName(IntPtr hWnd)
        {
            StringBuilder stringBuilder = new StringBuilder(256);
            AutoControl.GetClassName(hWnd, stringBuilder, 256);
            return stringBuilder.ToString().Trim();
        }

        // Token: 0x06000065 RID: 101 RVA: 0x00004070 File Offset: 0x00002270
        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)(HiWord << 16 | (LoWord & 65535));
        }

        // Token: 0x06000066 RID: 102 RVA: 0x00004094 File Offset: 0x00002294
        public static IntPtr MakeLParamFromXY(int x, int y)
        {
            return (IntPtr)(y << 16 | x);
        }

        // Token: 0x02000019 RID: 25
        // (Invoke) Token: 0x060000AE RID: 174
        public delegate bool CallBack(IntPtr hwnd, IntPtr lParam);

        // Token: 0x0200001A RID: 26
        // (Invoke) Token: 0x060000B2 RID: 178
        private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

        // Token: 0x0200001B RID: 27
        [Flags]
        public enum MouseEventFlags : uint
        {
            // Token: 0x040001A8 RID: 424
            LEFTDOWN = 2U,
            // Token: 0x040001A9 RID: 425
            LEFTUP = 4U,
            // Token: 0x040001AA RID: 426
            MIDDLEDOWN = 32U,
            // Token: 0x040001AB RID: 427
            MIDDLEUP = 64U,
            // Token: 0x040001AC RID: 428
            MOVE = 1U,
            // Token: 0x040001AD RID: 429
            ABSOLUTE = 32768U,
            // Token: 0x040001AE RID: 430
            RIGHTDOWN = 8U,
            // Token: 0x040001AF RID: 431
            RIGHTUP = 16U,
            // Token: 0x040001B0 RID: 432
            WHEEL = 2048U,
            // Token: 0x040001B1 RID: 433
            XDOWN = 128U,
            // Token: 0x040001B2 RID: 434
            XUP = 256U,
            // Token: 0x040001B3 RID: 435
            XBUTTON1 = 1U,
            // Token: 0x040001B4 RID: 436
            XBUTTON2 = 2U
        }
    }
}
