// XYZ Запись v19.15 (09.11.2026): сдвиг первого кадра до 20 с принимается (у db ffmpeg стартовал 4 с — события уезжали на 4 с). v19.14 (09.11.2026): ddagrab — метка кадра по часам в момент захвата (setpts=RTCTIME внутри lavfi вместо
// -use_wallclock_as_timestamps), строка привязки нажатий/курсора (источник, ключи входа, сдвиг X, «секундомер−файл» Y) — в recorder.log;
// запасная поправка для ddagrab — ini mk_dda_shift (с, + = подсветку позже; по умолчанию 0.1 — замер 09.11: без поправки −2 кадра, с 0.1 — +1).
// XYZ Запись v19.13 (09.11.2026): подсветка курсора и щелчки без отставания на 5 кадров (сдвиг по часам первого кадра). v19.12 (08.11.2026): показ мыши и клавиш как у Bandicam — подсветка курсора мягким градиентом без отставания
// (время событий привязано к кадрам, интерполяция на каждый кадр), щелчки красный/синий, клавиши крупным белым текстом
// с обводкой (9 позиций, 3 размера, «все нажатия»), окно «Мышь и клавиатура…» (ini mk_*), «Показывать курсор» (draw_mouse).
// Проверка: --keystest <mp4> [all] → test\keystest.mp4 / keystest_all.mp4; --snapshot-mousekeys → test\mk19\mousekeys.png.
// XYZ Запись v19.11 (07.11.2026): новогодний облик «Новогодний» — шапка Деда Мороза с помпоном на эмблеме (res/hat@1x|2x.png).
// XYZ Запись v19 (05.11.2026): облики-палитры «как в Winamp» — меню «Цвет облика ▸»: Мятный, Нуар, Чёрно-красный,
// Чёрно-бело-красный, Зелёно-красный. Эмблема не меняется; перекрашиваются корпус (ColorMatrix), подсветка и меню. ini: palette=<id>.
// XYZ Запись v18 (01.11.2026): новый значок приложения — виниловая пластинка (xyz_rec_vinyl.ico: /win32icon и ресурс app.ico — окно, трей). Код как в v17.
// XYZ Запись v17 (01.11.2026): компакт в облике «Рамка» — без плашки и обводки: на прозрачном фоне только эмблема (каска, «db»),
// мини-осциллограф и таймер; вокруг — мягкая тёмная тень (~35%) для читаемости на светлом фоне; полости «d»/«b» залиты тёмным (это кнопки).
// Хит-тест: кнопки по маске, перетаскивание — за непрозрачные пиксели эмблемы; тень и прозрачные места пропускают щелчки
// (WS_EX_TRANSPARENT по положению курсора). Кнопки «свернуть»/«закрыть» — при наведении слева и справа от каски. Рамку записи
// без плашки не рисуем (таймер и пульс точки). Облик «Корпус» — как в v16. Нажатия мягкие: затемнение полости — под эмблемой
// (край закрыт буквами, без ободка) с радиальным градиентом, знак при нажатии темнеет и уменьшается на 8%; маска палочек —
// со сглаженными краями (вес по альфе букв и растушёвка по X). --snapshot-compact: test\snap17\c_*.png и b_*.png (без плашки).
// XYZ Запись v16 (01.11.2026): компактный режим — в верхних углах плашки (по бокам от каски) невидимые зоны: курсор в левом углу —
// плавно (~150 мс) появляется круглая тёмная кнопка «Свернуть» (–), в правом — «Закрыть» (×), размер как у кнопок полного режима (R 10).
// Наведение на кнопку: «Свернуть» загорается янтарём (как пауза), «Закрыть» — красным (как запись). Действия те же, что у «–»/«×» полного
// режима (PowerOff / ExitApp). Хит-тест кнопок раньше перетаскивания; каска и буквы не перекрываются. --snapshot-compact: c_corner_*.png.
// XYZ Запись v15 (01.11.2026): мини-осциллограф компактного режима работает от звука, как большой осциллограф полного режима:
// каждый кадр — тот же текущий буфер сэмплов (sbuf) и тот же Scope.Points (триггер по переходу через ноль, tanh-усиление),
// без истории и прокрутки. Тишина — сразу ровная тонкая линия, голос — волна вибрирует на месте. Геометрия, цвета, таймер — как в v14.
// XYZ Запись v14 (01.11.2026): компактный режим — в полости «d» всегда видна красная точка (как на кнопке «Запись» прибора, ~43% полости:
// покой — приглушённая ~55%, наведение — ярче, запись — пульсирует, пауза — тёмная), в полости «b» — скруглённый квадратик (как «Стоп», ~38%:
// покой — приглушённый, наведение — светлее, нажатие — темнее). Знаки рисуются под эмблемой — буквы их не перекрывают и не меняются.
// XYZ Запись v13 (01.11.2026): захват экрана — Desktop Duplication (ffmpeg lavfi ddagrab + hwdownload), окна с WDA_EXCLUDEFROMCAPTURE
// (прибор, компакт, плашка, рамка области) в ролик не попадают; gdigrab (BitBlt) их не исключал. ddagrab не запустился — откат на gdigrab
// и запись в журнал %APPDATA%\XYZ\recorder.log (туда же — результат SetWindowDisplayAffinity / GetWindowDisplayAffinity каждого окна).
// Мини-осциллограф: геометрия v11 (таймер y=181, полоса уровня 80 px под ним), внутри — бегущая плавная волна справа налево
// (несущая ≈4,5 горба на ширину под огибающей громкости), тишина — ровная линия; «BIM DESIGN» стирается по пикселям надписи
// (свечение низа букв не срезается). Ключ проверки: --capturetest full|compact|plate|region <out.mp4> (показ 3 с + запись + сверка кадра).
// XYZ Запись v12 (01.11.2026): компактный режим — под буквами «db» мини-осциллограф (тот же источник сэмплов и свечение, что у большого;
// в покое при включённом питании и контроле входа — вместо «BIM DESIGN», в записи/паузе — под таймером вместо полоски уровня; пауза — янтарный).
// XYZ Запись v11 (01.11.2026): компактный режим (плашка с эмблемой db, Запись — полость «d», Пауза — палочки «d»+«b», Стоп — полость «b»,
// хит-тест по маске из PNG эмблемы; ini compact=1; двойной щелчок по эмблеме / меню «Компактный режим»), «Выбрать область…» (затемнение
// на весь виртуальный экран, чётные размеры, ini region=x,y,w,h; во время записи — рамка 2 px снаружи области, исключена из захвата),
// «О программе…», исходники до «Улучшить голос» — в %LOCALAPPDATA%\XYZ\raw (старше 7 дней удаляются при запуске, меню «Открыть исходники»).
// Ключи проверки v11: --snapshot-compact, --snapshot-about, --snapshot-region (test\snap11\*.png), --regiontest x,y,w,h (запись области 2 с).
// XYZ Запись v10 (01.11.2026): портабельная версия — ffmpeg ищется рядом с exe, в подпапке ffmpeg\, затем Shotcut, затем PATH.
// XYZ Запись v8 — «прибор»: форма «Шасси» (башня с эмблемой db + корпус со скошенным углом), современный стиль.
// Окно — layered (WS_EX_LAYERED + UpdateLayeredWindow, 32bpp PARGB): статичные слои — PNG из ресурсов (@1x/@2x),
// динамика (осциллограф, таймер, подсветка пэдов, тумблер) рисуется GDI+ поверх, ~30 кадров/с.
// Логика записи прежняя (Recorder.cs): ffmpeg gdigrab + dshow, пауза сегментами, склейка concat, F9, ini.
// v8 (01.11.2026): второй тумблер «Облик» рядом с «Питанием» (тот же щелчок и анимация): вверх — «Корпус», вниз — «Рамка».
// «Рамка»: корпус растворяется за 0,4 с, остаётся светящийся контур формы «Шасси» (слой outline, тонируется как кромка),
// эмблема db без подложки (слой emblem: сплошные мятные буквы #DEF7F0 с фаской, каска и рулон бирюзой #3FB6A8 со свечением,
// дырки d/b прозрачные — общий для обоих обликов), пэды/экран/таймер/списки/тумблеры висят в воздухе; под ними дымка
// (слой frame: чёрная alpha 0.35–0.55 с мягким краем + тонкие обводки и надписи). Прозрачные пиксели layered-окна
// пропускают мышь — перетаскивание только за контур и элементы. Облик хранится в ini: skin=body|frame.
// Чёткость в 100%: гайка — отдельный спрайт nut@1x, нарисованный под пиксели (24 зуба насечки + резкость), рычаги
// тумблеров при s<1.5 рисуются в 2× и уменьшаются HighQualityBicubic. Ресурсы — res_v8 (генератор gen8.py).
// v7 (01.11.2026): при «Запись»/F9 прибор НЕ сворачивается в плашку — остаётся на месте и переходит в состояние записи
// (красный пэд загорается и пульсирует, таймер и кромка краснеют с плавным переходом, волна бежит, списки приглушены;
// пауза — янтарная). Прибор и все его меню на время записи исключены из захвата: SetWindowDisplayAffinity(0x11)
// на главном layered-окне и на каждом ContextMenuStrip (Opening + Opened); после «Стоп» — 0 (скриншоты прибора снова видны).
// Меню корпуса (ini): «Показывать прибор в записи» (showInRec, по умолчанию 0) и «Сворачивать в плашку при записи» (foldRec, 0).
// --selftest: окно на экране 3–5 с, запись, проверка affinity и кадра mp4 (test\selftest_v7.log, test\v7_*.png);
// прежняя звуковая самопроверка — --selftest-audio.
// v6 (01.11.2026): основание тумблера — металлическая гайка с насечкой, фаской и втулкой (слой body),
// шток выходит из отверстия втулки со скруглённым основанием и тенью на гайке.
// v5 (01.11.2026): «Улучшить голос» — второй проход ffmpeg после склейки (цепочка «вариант 4», одобрена db),
// видео копируется, звук моно AAC 96k; оригинал — в <папка>\_raw\<имя>_raw.mp4. Ключ ini: voice=1/0.
// Сборка: csc /target:winexe /win32icon:xyz_rec_button.ico /out:"XYZ Запись_v7.exe" /r:System.Windows.Forms.dll /r:System.Drawing.dll
//         /resource:res_v6\body@1x.png,body@1x.png ... (все PNG из res) /resource:xyz_rec_button.ico,app.ico RecorderDevice.cs
// Ключи: --snapshot (кадры в test\v4_*.png без показа окна), --selftest (запись 2×1,5 с + ffprobe + проверка waveIn
// параллельно с ffmpeg, test\selftest_v5.log, плюс второй проход «Улучшить голос»), --mictest (waveIn, test\mictest_wavein.log).
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace XyzRecorder
{
    static class Native
    {
        [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
        [DllImport("user32.dll")] public static extern bool RegisterHotKey(IntPtr h, int id, uint mod, uint vk);
        [DllImport("user32.dll")] public static extern bool UnregisterHotKey(IntPtr h, int id);
        [DllImport("user32.dll", SetLastError = true)] public static extern bool SetWindowDisplayAffinity(IntPtr h, uint a);
        [DllImport("user32.dll")] public static extern bool GetWindowDisplayAffinity(IntPtr h, out uint a);
        public delegate bool EnumProc(IntPtr h, IntPtr l);
        [DllImport("user32.dll")] public static extern bool EnumWindows(EnumProc p, IntPtr l);
        [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
        [DllImport("user32.dll")] public static extern bool IsIconic(IntPtr h);
        [DllImport("user32.dll")] public static extern bool IsWindow(IntPtr h);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetWindowText(IntPtr h, StringBuilder s, int n);
        [DllImport("user32.dll")] public static extern int GetWindowTextLength(IntPtr h);
        [DllImport("user32.dll")] public static extern int GetWindowLong(IntPtr h, int i);
        [DllImport("user32.dll")] public static extern int SetWindowLong(IntPtr h, int i, int v);
        [DllImport("user32.dll")] public static extern IntPtr GetWindow(IntPtr h, uint cmd);
        [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr h, out uint pid);
        [DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr h, int c);
        [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
        [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
        [DllImport("dwmapi.dll")] public static extern int DwmGetWindowAttribute(IntPtr h, int a, out RECT r, int size);
        [DllImport("dwmapi.dll")] public static extern int DwmGetWindowAttribute(IntPtr h, int a, out int v, int size);
        [DllImport("user32.dll")] public static extern bool ReleaseCapture();
        [DllImport("user32.dll")] public static extern IntPtr SendMessage(IntPtr h, int m, IntPtr w, IntPtr l);
        [DllImport("user32.dll")] public static extern bool PostMessage(IntPtr h, int m, IntPtr w, IntPtr l);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int RegisterWindowMessage(string s);
        [StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left, Top, Right, Bottom; }
        public const uint WDA_EXCLUDEFROMCAPTURE = 0x11;

        // layered
        [StructLayout(LayoutKind.Sequential)] public struct POINT { public int X, Y; public POINT(int x, int y) { X = x; Y = y; } }
        [StructLayout(LayoutKind.Sequential)] public struct SIZE { public int CX, CY; public SIZE(int x, int y) { CX = x; CY = y; } }
        [StructLayout(LayoutKind.Sequential, Pack = 1)] public struct BLENDFUNCTION { public byte BlendOp, BlendFlags, SourceConstantAlpha, AlphaFormat; }
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER { public int biSize, biWidth, biHeight; public short biPlanes, biBitCount; public int biCompression, biSizeImage, biXPelsPerMeter, biYPelsPerMeter, biClrUsed, biClrImportant; }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);
        [DllImport("user32.dll")] public static extern IntPtr GetDC(IntPtr h);
        [DllImport("user32.dll")] public static extern int ReleaseDC(IntPtr h, IntPtr dc);
        [DllImport("gdi32.dll")] public static extern IntPtr CreateCompatibleDC(IntPtr dc);
        [DllImport("gdi32.dll")] public static extern bool DeleteDC(IntPtr dc);
        [DllImport("shell32.dll")] public static extern void SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
        [DllImport("gdi32.dll")] public static extern IntPtr SelectObject(IntPtr dc, IntPtr o);
        [DllImport("gdi32.dll")] public static extern bool DeleteObject(IntPtr o);
        [DllImport("gdi32.dll")] public static extern bool GdiFlush();
        [DllImport("gdi32.dll")] public static extern IntPtr CreateDIBSection(IntPtr dc, ref BITMAPINFOHEADER bmi, uint usage, out IntPtr bits, IntPtr sec, uint off);

        public static void DragWindow(Form f)
        {
            ReleaseCapture();
            SendMessage(f.Handle, 0xA1, (IntPtr)2, IntPtr.Zero);   // WM_NCLBUTTONDOWN, HTCAPTION
        }
    }

    // ---------- v13: журнал %APPDATA%\XYZ\recorder.log ----------
    static class AppLog
    {
        public static string PathLog { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XYZ", "recorder.log"); } }
        static readonly object lk = new object();
        public static string Tag = "";
        public static void W(string s)
        {
            try
            {
                lock (lk)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PathLog));
                    var fi = new FileInfo(PathLog);
                    if (fi.Exists && fi.Length > 2000000) { try { File.Delete(PathLog + ".old"); } catch { } File.Move(PathLog, PathLog + ".old"); }
                    File.AppendAllText(PathLog, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " [" + Process.GetCurrentProcess().Id + Tag + "] " + s + "\r\n", Encoding.UTF8);
                }
            }
            catch { }
        }
        // SetWindowDisplayAffinity + проверка GetWindowDisplayAffinity; true — значение стоит.
        public static bool Affinity(IntPtr h, uint want, string who)
        {
            if (h == IntPtr.Zero) { W("affinity " + who + ": нет окна"); return false; }
            bool ok = Native.SetWindowDisplayAffinity(h, want);
            int err = ok ? 0 : Marshal.GetLastWin32Error();
            uint now = 0xFFFF; Native.GetWindowDisplayAffinity(h, out now);
            bool good = ok && now == want;
            W("affinity " + who + " hwnd=0x" + h.ToInt64().ToString("X") + ": Set(0x" + want.ToString("X") + ")=" + ok + (ok ? "" : " err=" + err) + ", Get=0x" + now.ToString("X") + (good ? "" : "  <-- НЕ СТОИТ"));
            return good;
        }
        // Для layered-окна: если сразу не встало — повтор после показа/первого UpdateLayeredWindow (через очередь сообщений).
        public static bool AffinityRetry(Form f, uint want, string who)
        {
            if (f == null || !f.IsHandleCreated) return false;
            if (Affinity(f.Handle, want, who)) return true;
            try { f.BeginInvoke(new Action(() => { try { var lf = f as LayeredForm; if (lf != null) lf.Present(); Affinity(f.Handle, want, who + " (повтор после показа)"); } catch { } })); } catch { }
            return false;
        }
    }

    // ---------- v13: выходы DXGI основного адаптера (ddagrab output_idx = номер выхода на адаптере 0) ----------
    static class Dxgi
    {
        [ComImport, Guid("770aae78-f26f-4dba-a829-253c83d1b387"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IDXGIFactory1
        {
            void SetPrivateData(); void SetPrivateDataInterface(); void GetPrivateData(); void GetParent();
            void EnumAdapters(); void MakeWindowAssociation(); void GetWindowAssociation(); void CreateSwapChain(); void CreateSoftwareAdapter();
            [PreserveSig] int EnumAdapters1(uint idx, out IDXGIAdapter1 adapter);
            [PreserveSig] int IsCurrent();
        }
        [ComImport, Guid("29038f61-3839-4626-91fd-086879011a05"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IDXGIAdapter1
        {
            void SetPrivateData(); void SetPrivateDataInterface(); void GetPrivateData(); void GetParent();
            [PreserveSig] int EnumOutputs(uint idx, out IDXGIOutput output);
            void GetDesc(); void CheckInterfaceSupport(); void GetDesc1();
        }
        [ComImport, Guid("ae02eedb-c735-4690-8d52-5a8dc20213aa"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface IDXGIOutput
        {
            void SetPrivateData(); void SetPrivateDataInterface(); void GetPrivateData(); void GetParent();
            [PreserveSig] int GetDesc(out OUTPUT_DESC desc);
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct OUTPUT_DESC
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string DeviceName;
            public Native.RECT Desktop; public int Attached; public int Rotation; public IntPtr Monitor;
        }
        [DllImport("dxgi.dll")] static extern int CreateDXGIFactory1(ref Guid riid, out IntPtr factory);

        public class Output { public int Idx; public string Name; public Rectangle Bounds; public override string ToString() { return Idx + ":" + Name + " " + Bounds; } }
        static List<Output> cached; static DateTime cachedT;
        public static List<Output> Outputs(out string err)
        {
            err = null;
            if (cached != null && (DateTime.Now - cachedT).TotalSeconds < 5) return cached;
            var res = new List<Output>();
            object fo = null, ao = null;
            try
            {
                var iid = typeof(IDXGIFactory1).GUID; IntPtr pf;
                int hr = CreateDXGIFactory1(ref iid, out pf);
                if (hr != 0 || pf == IntPtr.Zero) { err = "CreateDXGIFactory1 hr=0x" + hr.ToString("X"); return res; }
                fo = Marshal.GetObjectForIUnknown(pf); Marshal.Release(pf);
                var f = (IDXGIFactory1)fo;
                IDXGIAdapter1 a;
                hr = f.EnumAdapters1(0, out a);
                if (hr != 0 || a == null) { err = "EnumAdapters1(0) hr=0x" + hr.ToString("X"); return res; }
                ao = a;
                for (uint i = 0; i < 16; i++)
                {
                    IDXGIOutput o;
                    if (a.EnumOutputs(i, out o) != 0 || o == null) break;
                    OUTPUT_DESC d;
                    if (o.GetDesc(out d) == 0)
                        res.Add(new Output { Idx = (int)i, Name = d.DeviceName, Bounds = Rectangle.FromLTRB(d.Desktop.Left, d.Desktop.Top, d.Desktop.Right, d.Desktop.Bottom) });
                    Marshal.ReleaseComObject(o);
                }
            }
            catch (Exception ex) { err = "DXGI: " + ex.Message; }
            finally
            {
                try { if (ao != null) Marshal.ReleaseComObject(ao); } catch { }
                try { if (fo != null) Marshal.ReleaseComObject(fo); } catch { }
            }
            cached = res; cachedT = DateTime.Now;
            return res;
        }
    }

    static class Theme
    {
        public static float K = 1f;
        public static Color Teal = Color.FromArgb(95, 211, 192);
        public static Color TealHi = Color.FromArgb(178, 255, 238);
        public static Color Red = Color.FromArgb(255, 72, 64);
        public static Color Amber = Color.FromArgb(255, 176, 56);
        public static Color Ink = Color.FromArgb(228, 233, 238);
        public static Color Mute = Color.FromArgb(128, 137, 147);
        public static Color Dim = Color.FromArgb(84, 91, 99);
        // v19: облики-палитры. Корпус: sat — насыщенность (0 — ч/б), br — яркость, tint/ta — оттенок и его доля.
        public class Pal { public string Id, Name; public Color Acc, AccHi, Rec, Pause; public float Sat = 1f, Br = 1f, Ta = 0f; public Color Tint = Color.Gray; }
        public static readonly Pal[] Pals = {
            new Pal { Id = "mint",  Name = "Мятный",             Acc = Color.FromArgb(95, 211, 192),  AccHi = Color.FromArgb(178, 255, 238), Rec = Color.FromArgb(255, 72, 64), Pause = Color.FromArgb(255, 176, 56) },
            new Pal { Id = "noir",  Name = "Нуар",               Acc = Color.FromArgb(214, 218, 222), AccHi = Color.FromArgb(255, 255, 255), Rec = Color.FromArgb(232, 60, 52), Pause = Color.FromArgb(200, 200, 200), Sat = 0f, Br = 0.62f },
            new Pal { Id = "blackred", Name = "Чёрно-красный",   Acc = Color.FromArgb(232, 58, 50),   AccHi = Color.FromArgb(255, 160, 150), Rec = Color.FromArgb(255, 40, 34), Pause = Color.FromArgb(255, 150, 60), Sat = 0f, Br = 0.55f, Ta = 0.18f, Tint = Color.FromArgb(150, 40, 36) },
            new Pal { Id = "bwred", Name = "Чёрно-бело-красный", Acc = Color.FromArgb(236, 238, 242), AccHi = Color.FromArgb(255, 255, 255), Rec = Color.FromArgb(240, 36, 36), Pause = Color.FromArgb(240, 36, 36), Sat = 0f, Br = 0.7f },
            new Pal { Id = "greenred", Name = "Зелёно-красный",  Acc = Color.FromArgb(118, 214, 92),  AccHi = Color.FromArgb(205, 255, 185), Rec = Color.FromArgb(255, 64, 52), Pause = Color.FromArgb(255, 176, 56), Sat = 0.25f, Br = 0.78f, Ta = 0.3f, Tint = Color.FromArgb(70, 120, 60) },
            new Pal { Id = "newyear", Name = "Новогодний (шапка Деда Мороза)", Acc = Color.FromArgb(214, 236, 248), AccHi = Color.FromArgb(255, 255, 255), Rec = Color.FromArgb(232, 40, 44), Pause = Color.FromArgb(255, 196, 72), Sat = 0.55f, Br = 0.9f, Ta = 0.12f, Tint = Color.FromArgb(170, 196, 230) },
        };
        public static Pal Cur = Pals[0];
        public static void ApplyPal(string id)
        {
            Pal p = Pals[0];
            foreach (var q in Pals) if (q.Id == id) p = q;
            Cur = p; Teal = p.Acc; TealHi = p.AccHi; Red = p.Rec; Amber = p.Pause;
        }
        public static bool Plain { get { return Cur.Sat >= 0.999f && Math.Abs(Cur.Br - 1f) < 0.001f && Cur.Ta <= 0.001f; } }
        static float Ch(float v, float lum, int ti) { float x = (lum + (v - lum) * Cur.Sat) * Cur.Br; return x * (1f - Cur.Ta) + Cur.Ta * x * (ti / 128f); }
        // тот же пересчёт для цветов, нарисованных кодом (плашка, кнопки, меню)
        public static Color Shade(Color c)
        {
            if (Plain) return c;
            float l = 0.299f * c.R + 0.587f * c.G + 0.114f * c.B;
            Func<float, int> cl = v => Math.Max(0, Math.Min(255, (int)Math.Round(v)));
            return Color.FromArgb(c.A, cl(Ch(c.R, l, Cur.Tint.R)), cl(Ch(c.G, l, Cur.Tint.G)), cl(Ch(c.B, l, Cur.Tint.B)));
        }
        public static ColorMatrix BodyMatrix(float alpha)
        {
            float s = Cur.Sat, ir = 0.299f * (1 - s), ig = 0.587f * (1 - s), ib = 0.114f * (1 - s);
            float kr = Cur.Br * (1f - Cur.Ta + Cur.Ta * Cur.Tint.R / 128f), kg = Cur.Br * (1f - Cur.Ta + Cur.Ta * Cur.Tint.G / 128f), kb = Cur.Br * (1f - Cur.Ta + Cur.Ta * Cur.Tint.B / 128f);
            return new ColorMatrix(new float[][] {
                new float[] { (ir + s) * kr, ir * kg, ir * kb, 0, 0 },
                new float[] { ig * kr, (ig + s) * kg, ig * kb, 0, 0 },
                new float[] { ib * kr, ib * kg, (ib + s) * kb, 0, 0 },
                new float[] { 0, 0, 0, alpha, 0 }, new float[] { 0, 0, 0, 0, 1 } });
        }
        public static Color A(Color c, float a) { return Color.FromArgb(Math.Max(0, Math.Min(255, (int)(a * 255))), c.R, c.G, c.B); }
        public static Color Mix(Color a, Color b, float t)
        {
            t = Math.Max(0, Math.Min(1, t));
            return Color.FromArgb((int)(a.A + (b.A - a.A) * t), (int)(a.R + (b.R - a.R) * t), (int)(a.G + (b.G - a.G) * t), (int)(a.B + (b.B - a.B) * t));
        }
        static Dictionary<string, FontFamily> fams = new Dictionary<string, FontFamily>();
        public static FontFamily Fam(params string[] names)
        {
            string key = string.Join("|", names);
            FontFamily f;
            if (fams.TryGetValue(key, out f)) return f;
            foreach (var n in names) { try { f = new FontFamily(n); break; } catch { } }
            if (f == null) f = FontFamily.GenericSansSerif;
            fams[key] = f; return f;
        }
        public static FontFamily UiFam { get { return Fam("Segoe UI"); } }
        public static FontFamily UiSemi { get { return Fam("Segoe UI Semibold", "Segoe UI"); } }
        public static FontFamily DigitFam { get { return Fam("Bahnschrift Light", "Bahnschrift", "Segoe UI Light", "Segoe UI"); } }
        static Dictionary<string, Font> fonts = new Dictionary<string, Font>();
        public static Font Px(FontFamily fam, float px, FontStyle st)
        {
            string k = fam.Name + "|" + px.ToString("0.00") + "|" + (int)st;
            Font f;
            if (!fonts.TryGetValue(k, out f)) { f = new Font(fam, Math.Max(1f, px), st, GraphicsUnit.Pixel); fonts[k] = f; }
            return f;
        }
        public static GraphicsPath Round(RectangleF r, float rad)
        {
            var p = new GraphicsPath();
            float d = Math.Min(rad * 2, Math.Min(r.Width, r.Height));
            if (d <= 0.5f) { p.AddRectangle(r); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure(); return p;
        }
    }

    // ---------- Ресурсы: PNG @1x/@2x, масштаб под DPI один раз ----------
    static class Res
    {
        static Dictionary<string, Bitmap> cache = new Dictionary<string, Bitmap>();
        static Bitmap Load(string name)
        {
            try
            {
                var s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
                if (s == null) return null;
                using (var im = Image.FromStream(s)) return new Bitmap(im);
            }
            catch { return null; }
        }
        // Спрайт нужного масштаба: логический размер = @1x; при s>1 — из @2x.
        // v19: эмблема в облике — та же форма, перекрашены только цветные части (каска) в цвет облика, буквы — нейтрально-белые
        public static Bitmap Get(string name, float s)
        {
            if (name == "emblem" && Theme.Cur.Id != "mint")
            {
                string pk = "emblem#" + Theme.Cur.Id + "|" + s.ToString("0.000");
                Bitmap pb;
                if (cache.TryGetValue(pk, out pb)) return pb;
                var orig = GetRaw(name, s);
                pb = orig == null ? null : Recolor(orig, Theme.Cur.Acc);
                if (pb != null && Theme.Cur.Id == "newyear")
                {   // v19.11: новогодний облик — красная шапка Деда Мороза с помпоном поверх каски
                    var hat = GetRaw("hat", s);
                    if (hat != null) using (var g = Graphics.FromImage(pb)) { g.CompositingMode = CompositingMode.SourceOver; g.DrawImage(hat, new Rectangle(0, 0, pb.Width, pb.Height), 0, 0, hat.Width, hat.Height, GraphicsUnit.Pixel); }
                }
                cache[pk] = pb; return pb;
            }
            return GetRaw(name, s);
        }
        static Bitmap Recolor(Bitmap src, Color acc)
        {
            var b = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
            float la = Math.Max(1f, 0.299f * acc.R + 0.587f * acc.G + 0.114f * acc.B);
            for (int y = 0; y < src.Height; y++)
                for (int x = 0; x < src.Width; x++)
                {
                    var c = src.GetPixel(x, y);
                    if (c.A == 0) { b.SetPixel(x, y, c); continue; }
                    int mx = Math.Max(c.R, Math.Max(c.G, c.B)), mn = Math.Min(c.R, Math.Min(c.G, c.B));
                    float sat = mx == 0 ? 0 : (mx - mn) / (float)mx;
                    float l = 0.299f * c.R + 0.587f * c.G + 0.114f * c.B;
                    float k = Math.Min(1f, sat * 2.2f);             // насколько пиксель «цветной»
                    float m = l / la;
                    Func<float, float, int> f = (g0, ac) => Math.Max(0, Math.Min(255, (int)Math.Round(g0 * (1 - k) + Math.Min(255f, ac * m) * k)));
                    b.SetPixel(x, y, Color.FromArgb(c.A, f(l, acc.R), f(l, acc.G), f(l, acc.B)));
                }
            return b;
        }
        static Bitmap GetRaw(string name, float s)
        {
            string key = name + "|" + s.ToString("0.000");
            Bitmap b;
            if (cache.TryGetValue(key, out b)) return b;
            var one = Load(name + "@1x.png");
            if (one == null) { cache[key] = null; return null; }
            int w = (int)Math.Round(one.Width * s), h = (int)Math.Round(one.Height * s);
            Bitmap src = one;
            if (Math.Abs(s - 1f) > 0.01f) { var two = Load(name + "@2x.png"); if (two != null) src = two; }
            b = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(b))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                if (src.Width == w && src.Height == h) g.DrawImage(src, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel);
                else
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    using (var ia = new ImageAttributes())
                    {
                        ia.SetWrapMode(WrapMode.TileFlipXY);
                        g.DrawImage(src, new Rectangle(0, 0, w, h), 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, ia);
                    }
                }
            }
            if (src != one) src.Dispose();
            one.Dispose();
            cache[key] = b; return b;
        }
        public static Stream Raw(string name)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }
        // Нарисовать спрайт 1:1 в целых пикселях; tint/alpha — через ColorMatrix.
        public static void Draw(Graphics g, Bitmap b, float x, float y, float alpha)
        {
            if (b == null || alpha <= 0.003f) return;
            var r = new Rectangle((int)Math.Round(x), (int)Math.Round(y), b.Width, b.Height);
            var im = g.InterpolationMode; var po = g.PixelOffsetMode;
            g.InterpolationMode = InterpolationMode.NearestNeighbor; g.PixelOffsetMode = PixelOffsetMode.Half;
            if (alpha >= 0.997f) g.DrawImage(b, r, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel);
            else using (var ia = Attr(1, 1, 1, alpha)) g.DrawImage(b, r, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, ia);
            g.InterpolationMode = im; g.PixelOffsetMode = po;
        }
        // v19: корпус и плашка — через палитру облика (эмблема рисуется обычным Draw и не меняется)
        public static void DrawBody(Graphics g, Bitmap b, float x, float y, float alpha)
        {
            if (Theme.Plain) { Draw(g, b, x, y, alpha); return; }
            if (b == null || alpha <= 0.003f) return;
            var r = new Rectangle((int)Math.Round(x), (int)Math.Round(y), b.Width, b.Height);
            var im = g.InterpolationMode; var po = g.PixelOffsetMode;
            g.InterpolationMode = InterpolationMode.NearestNeighbor; g.PixelOffsetMode = PixelOffsetMode.Half;
            using (var ia = new ImageAttributes()) { ia.SetColorMatrix(Theme.BodyMatrix(Math.Min(1f, alpha))); g.DrawImage(b, r, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, ia); }
            g.InterpolationMode = im; g.PixelOffsetMode = po;
        }
        public static void DrawTint(Graphics g, Bitmap b, float x, float y, Color c, float alpha)
        {
            if (b == null || alpha <= 0.003f) return;
            var r = new Rectangle((int)Math.Round(x), (int)Math.Round(y), b.Width, b.Height);
            var im = g.InterpolationMode; var po = g.PixelOffsetMode;
            g.InterpolationMode = InterpolationMode.NearestNeighbor; g.PixelOffsetMode = PixelOffsetMode.Half;
            using (var ia = Attr(c.R / 255f, c.G / 255f, c.B / 255f, Math.Min(1f, alpha)))
                g.DrawImage(b, r, 0, 0, b.Width, b.Height, GraphicsUnit.Pixel, ia);
            g.InterpolationMode = im; g.PixelOffsetMode = po;
        }
        public static ImageAttributes Attr(float r, float gg, float b, float a)
        {
            var cm = new ColorMatrix(new float[][] {
                new float[] { r, 0, 0, 0, 0 }, new float[] { 0, gg, 0, 0, 0 }, new float[] { 0, 0, b, 0, 0 },
                new float[] { 0, 0, 0, a, 0 }, new float[] { 0, 0, 0, 0, 1 } });
            var ia = new ImageAttributes(); ia.SetColorMatrix(cm); return ia;
        }
    }
    // ---------- Настройки ----------
    class Settings
    {
        static string PathIni { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XYZ", "recorder.ini"); } }
        public Dictionary<string, string> D = new Dictionary<string, string>();
        public string Get(string k, string def) { string v; return D.TryGetValue(k, out v) ? v : def; }
        public void Set(string k, string v) { D[k] = v ?? ""; }
        public static Settings Load()
        {
            var s = new Settings();
            try
            {
                if (File.Exists(PathIni))
                    foreach (var line in File.ReadAllLines(PathIni, Encoding.UTF8))
                    {
                        int i = line.IndexOf('=');
                        if (i > 0) s.D[line.Substring(0, i)] = line.Substring(i + 1);
                    }
            }
            catch { }
            return s;
        }
        public static bool ReadOnly;   // --selftest: ini пользователя не трогаем
        public void Save()
        {
            if (ReadOnly) return;
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(PathIni));
                var sb = new StringBuilder();
                foreach (var kv in D) sb.Append(kv.Key).Append('=').Append(kv.Value).Append("\r\n");
                File.WriteAllText(PathIni, sb.ToString(), Encoding.UTF8);
            }
            catch { }
        }
    }

    class MicDev { public string Name; public string Alt; public override string ToString() { return Name; } public string Arg { get { return string.IsNullOrEmpty(Alt) ? Name : Alt; } } }

    // ---------- ffmpeg ----------
    class Engine
    {
        public string Ffmpeg, WorkDir;
        public Rectangle? Area;          // null = весь рабочий стол
        public string Mic;               // аргумент dshow или null
        public string MicName;           // обычное имя устройства (запасной вариант, если Alt не открылся)
        public List<string> Segments = new List<string>();
        // v19.13: время (Unix, с) первого кадра каждого сегмента — из строки ffmpeg «Duration: …, start: X» первого входа
        // (часы Windows, как DateTime.UtcNow). NaN — не узнали. Нужно, чтобы привязать подсветку/щелчки/клавиши к кадрам.
        public List<double> SegStarts = new List<double>();
        public List<string> SegModes = new List<string>();   // v19.14: чем писался каждый сегмент (ddagrab / gdigrab) и ключи входа — для recorder.log
        public List<string> SegInputs = new List<string>();
        double curStart = double.NaN; string curInput = "";
        static readonly System.Text.RegularExpressions.Regex StartRx = new System.Text.RegularExpressions.Regex(@"^\s*Duration:.*?,\s*start:\s*(-?\d+\.\d+)");
        public string LastError = "";
        // v13: захват — Desktop Duplication (ddagrab), исключает окна с WDA_EXCLUDEFROMCAPTURE; gdigrab — запасной.
        public bool PreferDda = true;
        public static bool DrawMouse = true;   // v19.12: «Показывать курсор в записи» (окно «Мышь и клавиатура»)
        public static bool DdaBroken;      // ddagrab не запустился в этом сеансе — дальше сразу gdigrab
        public string CapMode = "";        // "ddagrab" / "gdigrab" — чем пишется текущий сегмент
        public Rectangle CapRect;          // что реально попадает в кадр (экранные физические координаты)
        Process proc; string curSeg;
        readonly object lk = new object();
        StringBuilder tail = new StringBuilder();
        ManualResetEvent started = new ManualResetEvent(false);
        public bool Running { get { return proc != null; } }

        public static string FindFfmpeg()
        {
            // v10: портабельный порядок — рядом с exe, подпапка ffmpeg\, Shotcut, PATH.
            var c = new List<string>();
            string bd = AppDomain.CurrentDomain.BaseDirectory;
            c.Add(Path.Combine(bd, "ffmpeg.exe"));
            c.Add(Path.Combine(Path.Combine(bd, "ffmpeg"), "ffmpeg.exe"));
            c.Add(@"C:\Program Files\Shotcut\ffmpeg.exe");
            c.Add(@"C:\Program Files (x86)\Shotcut\ffmpeg.exe");
            foreach (var p in (Environment.GetEnvironmentVariable("PATH") ?? "").Split(';'))
                if (p.Trim().Length > 0) c.Add(Path.Combine(p.Trim(), "ffmpeg.exe"));
            // v19: кандидат должен реально запускаться (на Яндекс.Диске файлы бывают «только в облаке» — exe есть, а DLL не скачаны)
            if (ffCache != null && File.Exists(ffCache)) return ffCache;
            foreach (var f in c) { try { if (File.Exists(f) && Works(f)) { ffCache = f; return f; } } catch { } }
            foreach (var f in c) { try { if (File.Exists(f)) return f; } catch { } }
            return null;
        }
        static string ffCache;
        static bool Works(string exe)
        {
            try
            {
                var psi = new ProcessStartInfo(exe, "-hide_banner -version") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true };
                using (var p = Process.Start(psi))
                {
                    p.StandardOutput.ReadToEndAsync(); p.StandardError.ReadToEndAsync();
                    if (!p.WaitForExit(8000)) { try { p.Kill(); } catch { } return false; }
                    return p.ExitCode == 0;
                }
            }
            catch { return false; }
        }

        static ProcessStartInfo Psi(string exe, string args, bool stdin)
        {
            var psi = new ProcessStartInfo(exe, args);
            psi.UseShellExecute = false; psi.CreateNoWindow = true;
            psi.RedirectStandardError = true; psi.RedirectStandardOutput = true; psi.RedirectStandardInput = stdin;
            psi.StandardErrorEncoding = Encoding.UTF8; psi.StandardOutputEncoding = Encoding.UTF8;
            return psi;
        }

        // Короткий запуск с полным выводом (stdout+stderr).
        public static int Run(string exe, string args, out string output, int timeoutMs)
        {
            var sb = new StringBuilder(); object l = new object();
            using (var p = new Process())
            {
                p.StartInfo = Psi(exe, args, false);
                p.OutputDataReceived += (s, e) => { if (e.Data != null) lock (l) sb.AppendLine(e.Data); };
                p.ErrorDataReceived += (s, e) => { if (e.Data != null) lock (l) sb.AppendLine(e.Data); };
                p.Start(); p.BeginOutputReadLine(); p.BeginErrorReadLine();
                if (!p.WaitForExit(timeoutMs)) { try { p.Kill(); } catch { } output = sb.ToString() + "\n[таймаут]"; return -1; }
                p.WaitForExit();
                lock (l) output = sb.ToString();
                return p.ExitCode;
            }
        }

        // Windows: Параметры → Конфиденциальность → Микрофон. Deny = захват звука заблокирован системой.
        public static string MicBlockReason()
        {
            string[] keys = { @"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\microphone" };
            try
            {
                using (var k = Registry.LocalMachine.OpenSubKey(keys[0]))
                    if (k != null && "Deny".Equals(k.GetValue("Value"))) return "выключен «Доступ к микрофону» для этого устройства";
                using (var k = Registry.CurrentUser.OpenSubKey(keys[0]))
                    if (k != null && "Deny".Equals(k.GetValue("Value"))) return "выключен доступ приложений к микрофону";
                using (var k = Registry.CurrentUser.OpenSubKey(keys[0] + @"\NonPackaged"))
                    if (k != null && "Deny".Equals(k.GetValue("Value"))) return "выключен доступ классических приложений к микрофону";
            }
            catch { }
            return null;
        }
        public const string MicHint = "Параметры Windows → Конфиденциальность и защита → Микрофон";
        public const string MicNoAccess = "Нет доступа к микрофону — включите: Параметры → Конфиденциальность и защита → Микрофон";

        public static List<MicDev> ListMics(string ffmpeg, out string raw)
        {
            var res = new List<MicDev>(); raw = "";
            if (ffmpeg == null) return res;
            Run(ffmpeg, "-hide_banner -list_devices true -f dshow -i dummy", out raw, 15000);
            bool audioSection = false; MicDev last = null;
            foreach (var l0 in raw.Split('\n'))
            {
                string l = l0.TrimEnd('\r');
                if (l.Contains("DirectShow audio devices")) { audioSection = true; continue; }
                if (l.Contains("DirectShow video devices")) { audioSection = false; continue; }
                int q1 = l.IndexOf('"'); if (q1 < 0) continue;
                int q2 = l.IndexOf('"', q1 + 1); if (q2 < 0) continue;
                string val = l.Substring(q1 + 1, q2 - q1 - 1);
                if (l.Contains("Alternative name")) { if (last != null) last.Alt = val; continue; }
                string after = l.Substring(q2 + 1).Trim();
                bool isAudio = after.StartsWith("(audio") || (after.Length == 0 && audioSection) || after.Contains("audio");
                if (isAudio && !after.Contains("video")) { last = new MicDev { Name = val }; res.Add(last); }
                else last = null;
            }
            return res;
        }

        public void Log(string s)
        {
            try { File.AppendAllText(Path.Combine(WorkDir, "ffmpeg.log"), s + "\r\n", Encoding.UTF8); } catch { }
        }

        // ---- «Улучшить голос»: вариант 4, одобрен db 01.11.2026. Частоты режекторов — под микрофон ноутбука (Realtek).
        // НЕ менять на вариант 5 (dynamic EQ / линейный loudnorm / limiter) — db: «отвратительно ужасный звук».
        public const string VoiceChain = "highpass=f=90,lowpass=f=9500,bandreject=f=235:width_type=q:w=12,bandreject=f=424:width_type=q:w=12,bandreject=f=440:width_type=q:w=12,bandreject=f=630:width_type=q:w=12,bandreject=f=689:width_type=q:w=12,bandreject=f=907:width_type=q:w=12,bandreject=f=2782:width_type=q:w=12,bandreject=f=5521:width_type=q:w=12,equalizer=f=180:t=q:w=1.2:g=-3,afftdn=nf=-38:nr=10:tn=1,agate=threshold=0.01:ratio=2:attack=10:release=350:range=0.15,equalizer=f=6500:t=h:w=0.7:g=-3,deesser=i=0.4:m=0.5:f=0.5,acompressor=threshold=-22dB:ratio=2.2:attack=15:release=250:makeup=3,loudnorm=I=-17:TP=-2:LRA=11";

        // Второй проход: inFile → outFile, видео копируется, звук моно 48 кГц AAC 160k (loudnorm сам поднимает до 96 кГц — возвращаем 48). Прогресс 0…1 по -progress pipe:1 (out_time_us).
        public static bool Enhance(string ff, string inFile, string outFile, double durSec, Action<double> progress, Action<string> log, out string err)
        {
            err = null;
            string args = "-hide_banner -nostats -y -i \"" + inFile + "\" -map 0:v? -map 0:a -c:v copy -af \"" + VoiceChain +
                "\" -ac 1 -ar 48000 -c:a aac -b:a 96k -movflags +faststart -progress pipe:1 \"" + outFile + "\"";
            if (log != null) log("=== " + DateTime.Now.ToString("HH:mm:ss") + " улучшение голоса: ffmpeg " + args);
            var tl = new StringBuilder(); object l = new object();
            int rc = -1;
            try
            {
                using (var p = new Process())
                {
                    p.StartInfo = Psi(ff, args, false);
                    p.OutputDataReceived += (s, e) =>
                    {
                        if (e.Data == null || progress == null) return;
                        string d = e.Data.Trim();
                        if (d.StartsWith("out_time_us=") || d.StartsWith("out_time_ms="))
                        {
                            long us;
                            if (durSec > 0.1 && long.TryParse(d.Substring(12), out us)) progress(Math.Max(0.0, Math.Min(1.0, us / 1e6 / durSec)));
                        }
                        else if (d == "progress=end") progress(1.0);
                    };
                    p.ErrorDataReceived += (s, e) => { if (e.Data != null) lock (l) { tl.AppendLine(e.Data); if (tl.Length > 40000) tl.Remove(0, 20000); } };
                    p.Start(); p.BeginOutputReadLine(); p.BeginErrorReadLine();
                    int tmo = (int)Math.Min(int.MaxValue, Math.Max(120000.0, durSec * 3000.0 + 60000.0));
                    if (!p.WaitForExit(tmo)) { try { p.Kill(); } catch { } err = "ffmpeg не уложился в " + (tmo / 1000) + " с"; }
                    p.WaitForExit();
                    rc = p.ExitCode;
                }
            }
            catch (Exception ex) { err = "Не удалось запустить ffmpeg: " + ex.Message; }
            string t; lock (l) t = tl.ToString();
            if (log != null) log(t.TrimEnd() + "\r\n=== улучшение rc=" + rc);
            bool ok = err == null && rc == 0 && File.Exists(outFile) && new FileInfo(outFile).Length > 1000;
            if (!ok && err == null) err = "ffmpeg rc=" + rc + ":\n" + (t.Length > 1500 ? t.Substring(t.Length - 1500) : t);
            return ok;
        }

        // v11: исходники до «Улучшить голос» — %LOCALAPPDATA%\XYZ\raw; файлы старше 7 дней удаляются при запуске (CleanRaw).
        public static string RawDir { get { return Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XYZ"), "raw"); } }
        public static void CleanRaw(int days)
        {
            try
            {
                if (!Directory.Exists(RawDir)) return;
                var lim = DateTime.Now.AddDays(-days);
                foreach (var f in Directory.GetFiles(RawDir))
                    try { if (File.GetLastWriteTime(f) < lim) File.Delete(f); } catch { }
            }
            catch { }
        }
        // Обработанный tmp готов: оригинал → RawDir\<имя>_raw.mp4, tmp → на место итогового файла.
        public static bool SwapEnhanced(string outFile, string tmp, out string rawFile, out string err)
        {
            rawFile = null; err = null;
            try
            {
                string rawDir = RawDir;   // v11: не рядом с записями, а в %LOCALAPPDATA%\XYZ\raw
                Directory.CreateDirectory(rawDir);
                string b = Path.GetFileNameWithoutExtension(outFile);
                string rf = Path.Combine(rawDir, b + "_raw.mp4");
                for (int i = 2; File.Exists(rf); i++) rf = Path.Combine(rawDir, b + "_raw_" + i + ".mp4");
                File.Move(outFile, rf);
                try { File.Move(tmp, outFile); }
                catch (Exception ex) { try { File.Move(rf, outFile); } catch { } err = ex.Message; return false; }
                rawFile = rf; return true;
            }
            catch (Exception ex) { err = ex.Message; return false; }
        }

        public bool StartSegment()
        {
            bool ok = StartSegment1();
            if (!ok && !string.IsNullOrEmpty(Mic) && !string.IsNullOrEmpty(MicName) && Mic != MicName)
            {
                Log("Alt-имя микрофона не открылось — пробую имя «" + MicName + "»");
                string keep = Mic; Mic = MicName;
                ok = StartSegment1();
                if (!ok) Mic = keep;
            }
            return ok;
        }

        bool StartSegment1()
        {
            if (PreferDda && !DdaBroken)
            {
                string why;
                string vin = DdaInput(out why);
                if (vin != null)
                {
                    if (StartSegment2(vin, "ddagrab")) return true;
                    string e1 = LastError;
                    Log("ddagrab не запустился — пробую gdigrab");
                    if (StartSegment2(GdiInput(), "gdigrab"))
                    {
                        DdaBroken = true;
                        AppLog.W("ddagrab не запустился, запись идёт через gdigrab (прибор МОЖЕТ попасть в ролик): " + Short(e1));
                        return true;
                    }
                    return false;
                }
                AppLog.W("ddagrab недоступен для области " + (Area.HasValue ? Area.Value.ToString() : "весь экран") + ": " + why + " — gdigrab");
            }
            return StartSegment2(GdiInput(), "gdigrab");
        }
        static string Short(string t) { t = (t ?? "").Replace("\r", " ").Replace("\n", " "); return t.Length > 400 ? t.Substring(t.Length - 400) : t; }

        string GdiInput()
        {
            var a = new StringBuilder("-thread_queue_size 1024 -f gdigrab -framerate 15 -draw_mouse " + (DrawMouse ? 1 : 0) + " ");
            if (Area.HasValue)
            {
                var r = Area.Value;
                a.AppendFormat("-offset_x {0} -offset_y {1} -video_size {2}x{3} ", r.X, r.Y, r.Width, r.Height);
                CapRect = r;
            }
            else CapRect = SystemInformation.VirtualScreen;
            a.Append("-i desktop ");
            return a.ToString();
        }

        // v13: Desktop Duplication. Область/окно — offset и video_size относительно монитора (физические px, процесс DPI-aware);
        // «Весь экран» — основной монитор. Монитор — выход DXGI адаптера 0 с наибольшим пересечением.
        string DdaInput(out string why)
        {
            why = null;
            Rectangle want = Area.HasValue ? Area.Value : Screen.PrimaryScreen.Bounds;
            string err; var outs = Dxgi.Outputs(out err);
            if (outs.Count == 0) { why = "выходы DXGI не найдены" + (err != null ? " (" + err + ")" : ""); return null; }
            Dxgi.Output best = null; long bestA = 0;
            foreach (var o in outs)
            {
                var i = Rectangle.Intersect(o.Bounds, want);
                long ar = (long)Math.Max(0, i.Width) * Math.Max(0, i.Height);
                if (ar > bestA) { bestA = ar; best = o; }
            }
            if (best == null) { why = "область вне мониторов основного адаптера (" + string.Join("; ", outs.ConvertAll(o => o.ToString()).ToArray()) + ")"; return null; }
            var r = Rectangle.Intersect(best.Bounds, want);
            r = new Rectangle(r.X, r.Y, r.Width & ~1, r.Height & ~1);
            if (r.Width < 16 || r.Height < 16) { why = "область слишком мала"; return null; }
            var sb = new StringBuilder("ddagrab=output_idx=" + best.Idx + ":framerate=15:draw_mouse=" + (DrawMouse ? 1 : 0));
            if (r != best.Bounds)
                sb.AppendFormat(":video_size={0}x{1}:offset_x={2}:offset_y={3}", r.Width, r.Height, r.X - best.Bounds.X, r.Y - best.Bounds.Y);
            // v19.14: метка времени кадра — часы Windows (мкс) сразу после ddagrab, т. е. в момент захвата (а не когда ffmpeg забрал кадр);
            // ffmpeg печатает время первого кадра («start: …») — по нему подсветка курсора привязывается к кадрам
            sb.Append(",settb=1/1000000,setpts=RTCTIME/(1000000*TB)");
            sb.Append(",hwdownload,format=bgra");
            CapRect = r;
            string info = "ddagrab: монитор " + best + ", кадр " + r + (r != want ? " (запрошено " + want + ", обрезано по монитору)" : "");
            Log(info);
            return "-thread_queue_size 1024 -f lavfi -i \"" + sb + "\" ";
        }

        bool StartSegment2(string videoIn, string mode)
        {
            Directory.CreateDirectory(WorkDir);
            CapMode = mode;
            curSeg = Path.Combine(WorkDir, string.Format("seg_{0:000}.mkv", Segments.Count));
            var a = new StringBuilder("-hide_banner -y ");
            a.Append(videoIn);
            bool mic = !string.IsNullOrEmpty(Mic);
            if (mic) a.Append("-thread_queue_size 1024 -f dshow -audio_buffer_size 50 -i audio=\"" + Mic + "\" ");
            a.Append("-map 0:v ");
            if (mic) a.Append("-map 1:a ");
            a.Append("-vf \"scale=trunc(iw/2)*2:trunc(ih/2)*2\" -c:v libx264 -preset veryfast -crf 28 -pix_fmt yuv420p ");
            if (mic) a.Append("-af aresample=async=1 -c:a aac -b:a 96k ");
            a.Append("\"" + curSeg + "\"");
            Log("=== " + DateTime.Now.ToString("HH:mm:ss") + " ffmpeg " + a);
            lock (lk) { tail.Length = 0; curStart = double.NaN; curInput = videoIn.Trim(); }
            started.Reset();
            var p = new Process();
            p.StartInfo = Psi(Ffmpeg, a.ToString(), true);
            p.ErrorDataReceived += OnErr;
            p.OutputDataReceived += OnErr;
            try { p.Start(); }
            catch (Exception ex) { LastError = "Не удалось запустить ffmpeg: " + ex.Message; return false; }
            p.BeginErrorReadLine(); p.BeginOutputReadLine();
            proc = p;
            // ждём «Press [q] to stop» = запись реально пошла
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < 8000)
            {
                if (started.WaitOne(50)) { AppLog.W("сегмент " + Segments.Count + ": " + mode + ", кадр " + CapRect + (mic ? ", звук" : ", без звука")); return true; }
                if (p.HasExited)
                {
                    p.WaitForExit();
                    lock (lk) LastError = "ffmpeg завершился с ошибкой:\n" + Tail(1500);
                    proc = null; return false;
                }
                Application.DoEvents();
            }
            // v19.10: ffmpeg жив, но кадров нет (бывает у ddagrab при удалённом рабочем столе) — раньше считали, что пишет,
            // и после «Стоп» было «Нет записанных фрагментов». Теперь снимаем процесс и отдаём false: дальше — запасной gdigrab.
            AppLog.W("сегмент " + Segments.Count + ": " + mode + " — строки Press [q] не было за 8 с, снимаю ffmpeg: " + Short(Tail(400)));
            try { if (!p.HasExited) p.Kill(); p.WaitForExit(3000); } catch { }
            lock (lk) LastError = "ffmpeg не начал запись за 8 с (" + mode + ")";
            proc = null;
            return false;
        }

        string Tail(int n) { string t = tail.ToString(); return t.Length > n ? t.Substring(t.Length - n) : t; }

        void OnErr(object s, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;
            if (e.Data.Contains("Press [q]")) started.Set();
            if (e.Data.StartsWith("frame=") || e.Data.StartsWith("size=")) return;
            if (double.IsNaN(curStart))   // v19.13: первая строка «Duration: …, start: X» — вход видео (он открывается первым)
            {
                var sm = StartRx.Match(e.Data); double st;
                if (sm.Success && double.TryParse(sm.Groups[1].Value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out st))
                    lock (lk) if (double.IsNaN(curStart)) curStart = st > 1e9 ? st : -1;   // не Unix-время (не по часам) — помечаем −1, чтобы не брать звук
            }
            lock (lk) { tail.AppendLine(e.Data); if (tail.Length > 20000) tail.Remove(0, 10000); }
            Log(e.Data);
        }

        public bool StopSegment()
        {
            var p = proc; if (p == null) return true;
            try { p.StandardInput.Write("q"); p.StandardInput.Flush(); } catch { }
            if (!p.WaitForExit(20000)) { Log("ffmpeg не ответил на q — принудительно"); try { p.Kill(); } catch { } }
            p.WaitForExit();
            proc = null;
            bool ok = File.Exists(curSeg) && new FileInfo(curSeg).Length > 1000;
            if (ok)
            {
                Segments.Add(curSeg); double st; string inp; lock (lk) { st = curStart; inp = curInput; }
                SegStarts.Add(st > 1e9 ? st : double.NaN); SegModes.Add(CapMode); SegInputs.Add(inp);
                AppLog.W("сегмент " + (Segments.Count - 1) + ": " + CapMode + ", первый кадр (start) " + (st > 1e9 ? st.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture) : "не найден (" + st.ToString(System.Globalization.CultureInfo.InvariantCulture) + ")") + ", вход: " + inp);   // v19.14
            }
            else lock (lk) LastError = "Сегмент не записан:\n" + Tail(1500);
            return ok;
        }

        public bool Finalize(string outFile)
        {
            if (Segments.Count == 0) { LastError = "Нет записанных фрагментов."; return false; }
            string args;
            if (Segments.Count == 1)
                args = "-hide_banner -y -i \"" + Segments[0] + "\" -map 0 -c copy -movflags +faststart \"" + outFile + "\"";
            else
            {
                string list = Path.Combine(WorkDir, "list.txt");
                var sb = new StringBuilder();
                foreach (var s in Segments) sb.Append("file '").Append(s.Replace('\\', '/').Replace("'", "'\\''")).Append("'\n");
                File.WriteAllText(list, sb.ToString(), new UTF8Encoding(false));
                args = "-hide_banner -y -f concat -safe 0 -i \"" + list + "\" -map 0 -c copy -movflags +faststart \"" + outFile + "\"";
            }
            string o; int rc = Run(Ffmpeg, args, out o, 600000);
            Log("=== склейка rc=" + rc + "\r\n" + o);
            bool ok = rc == 0 && File.Exists(outFile) && new FileInfo(outFile).Length > 1000;
            if (!ok) LastError = "Не удалось собрать mp4:\n" + (o.Length > 1500 ? o.Substring(o.Length - 1500) : o);
            return ok;
        }

        public void CleanupSegments()
        {
            foreach (var s in Segments) { try { File.Delete(s); } catch { } }
            try { File.Delete(Path.Combine(WorkDir, "list.txt")); } catch { }
        }
    }
    class AreaItem
    {
        public int Kind; // 0 весь экран, 1 монитор, 2 окно, 3 выбранная область (v11)
        public int Mon; public IntPtr Hwnd; public string Text; public string Key;
        public Rectangle Rect; public string Short;   // v11: для области
        public override string ToString() { return Text; }
    }

    // ---------- Микрофон через winmm (waveIn) ----------
    static class WinMM
    {
        public const uint CALLBACK_EVENT = 0x50000;
        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEFORMATEX { public ushort wFormatTag, nChannels; public uint nSamplesPerSec, nAvgBytesPerSec; public ushort nBlockAlign, wBitsPerSample, cbSize; }
        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEHDR { public IntPtr lpData; public uint dwBufferLength, dwBytesRecorded; public IntPtr dwUser; public uint dwFlags, dwLoops; public IntPtr lpNext, reserved; }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WAVEINCAPS { public ushort wMid, wPid; public uint vDriverVersion; [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szPname; public uint dwFormats; public ushort wChannels, wReserved1; }
        [DllImport("winmm.dll")] public static extern int waveInGetNumDevs();
        [DllImport("winmm.dll", CharSet = CharSet.Unicode, EntryPoint = "waveInGetDevCapsW")] public static extern int waveInGetDevCaps(IntPtr id, ref WAVEINCAPS c, int size);
        [DllImport("winmm.dll")] public static extern int waveInOpen(out IntPtr h, IntPtr id, ref WAVEFORMATEX f, IntPtr cb, IntPtr inst, uint flags);
        [DllImport("winmm.dll")] public static extern int waveInPrepareHeader(IntPtr h, IntPtr hdr, int size);
        [DllImport("winmm.dll")] public static extern int waveInUnprepareHeader(IntPtr h, IntPtr hdr, int size);
        [DllImport("winmm.dll")] public static extern int waveInAddBuffer(IntPtr h, IntPtr hdr, int size);
        [DllImport("winmm.dll")] public static extern int waveInStart(IntPtr h);
        [DllImport("winmm.dll")] public static extern int waveInReset(IntPtr h);
        [DllImport("winmm.dll")] public static extern int waveInClose(IntPtr h);
        [DllImport("winmm.dll", CharSet = CharSet.Unicode, EntryPoint = "waveInGetErrorTextW")] public static extern int waveInGetErrorText(int err, StringBuilder sb, int n);

        public static string ErrText(int r)
        {
            if (r == 0) return "ok";
            var sb = new StringBuilder(256);
            try { waveInGetErrorText(r, sb, sb.Capacity); } catch { }
            return "MMSYSERR " + r + (sb.Length > 0 ? " — " + sb : "");
        }
        public static List<string> DeviceNames()
        {
            var res = new List<string>();
            int n = 0; try { n = waveInGetNumDevs(); } catch { }
            for (int i = 0; i < n; i++)
            {
                var c = new WAVEINCAPS();
                res.Add(waveInGetDevCaps(new IntPtr(i), ref c, Marshal.SizeOf(typeof(WAVEINCAPS))) == 0 ? (c.szPname ?? "") : "");
            }
            return res;
        }
        // Имя dshow («Микрофон (Realtek High Definition Audio)») → номер waveIn. В waveIn имя обрезано до 31 знака.
        // Неоднозначно или не найдено → -1 (WAVE_MAPPER, устройство по умолчанию).
        public static int Match(string dshowName)
        {
            if (string.IsNullOrEmpty(dshowName)) return -1;
            var names = DeviceNames();
            string d = dshowName.Trim().ToLowerInvariant();
            int found = -1, cnt = 0;
            for (int i = 0; i < names.Count; i++)
            {
                string w = names[i].Trim().ToLowerInvariant();
                if (w.Length == 0) continue;
                if (w == d) return i;
                if (d.StartsWith(w) || w.StartsWith(d)) { found = i; cnt++; }
            }
            return cnt == 1 ? found : -1;
        }
    }

    // Захват 16 кГц моно 16 бит, 4 буфера по 50 мс; уровни копятся до чтения индикатором.
    class MicMeter
    {
        public const int Rate = 16000;
        const int NBuf = 4, BufBytes = Rate * 2 / 20;
        IntPtr h = IntPtr.Zero; IntPtr[] hdrs, bufs; AutoResetEvent ev; Thread th; volatile bool run;
        int hdrSize, offFlags, offRec, next;
        readonly object lk = new object();
        float accRms = -200f, accPeak = -200f; bool fresh;
        public long Buffers; public float MaxRms = -200f, MaxPeak = -200f; public bool AnyNonZero;
        public int ZeroMs;  // сколько мс подряд идут нули (Windows отдаёт тишину при запрете доступа)
        MemoryStream cap; int capLeft; Action<byte[]> capDone;
        public int Device = int.MinValue;
        // Кольцо последних отсчётов (−1…1) для осциллографа.
        public const int RingN = 8192;
        readonly float[] ring = new float[RingN]; int rp; long total;
        public int CopyLast(float[] dst)
        {
            lock (lk)
            {
                int n = (int)Math.Min(total, (long)dst.Length);
                int st = (rp - n + RingN) & (RingN - 1);
                int off = dst.Length - n;
                for (int i = 0; i < n; i++) dst[off + i] = ring[(st + i) & (RingN - 1)];
                for (int i = 0; i < off; i++) dst[i] = 0f;
                return n;
            }
        }
        public bool IsOpen { get { return h != IntPtr.Zero; } }

        public int Open(int dev)
        {
            Close();
            lock (lk) { total = 0; rp = 0; }
            Buffers = 0; MaxRms = MaxPeak = -200f; AnyNonZero = false; ZeroMs = 0; next = 0;
            var f = new WinMM.WAVEFORMATEX { wFormatTag = 1, nChannels = 1, nSamplesPerSec = Rate, nAvgBytesPerSec = Rate * 2, nBlockAlign = 2, wBitsPerSample = 16, cbSize = 0 };
            ev = new AutoResetEvent(false);
            int r;
            try { r = WinMM.waveInOpen(out h, new IntPtr(dev), ref f, ev.SafeWaitHandle.DangerousGetHandle(), IntPtr.Zero, WinMM.CALLBACK_EVENT); }
            catch (Exception) { r = 1; h = IntPtr.Zero; }
            if (r != 0) { h = IntPtr.Zero; ev.Close(); ev = null; return r; }
            Device = dev;
            hdrSize = Marshal.SizeOf(typeof(WinMM.WAVEHDR));
            offFlags = (int)Marshal.OffsetOf(typeof(WinMM.WAVEHDR), "dwFlags");
            offRec = (int)Marshal.OffsetOf(typeof(WinMM.WAVEHDR), "dwBytesRecorded");
            hdrs = new IntPtr[NBuf]; bufs = new IntPtr[NBuf];
            for (int i = 0; i < NBuf; i++)
            {
                bufs[i] = Marshal.AllocHGlobal(BufBytes);
                hdrs[i] = Marshal.AllocHGlobal(hdrSize);
                var wh = new WinMM.WAVEHDR { lpData = bufs[i], dwBufferLength = BufBytes };
                Marshal.StructureToPtr(wh, hdrs[i], false);
                r = WinMM.waveInPrepareHeader(h, hdrs[i], hdrSize);
                if (r == 0) r = WinMM.waveInAddBuffer(h, hdrs[i], hdrSize);
                if (r != 0) { Close(); return r; }
            }
            run = true;
            th = new Thread(Loop) { IsBackground = true, Name = "waveIn" };
            th.Start();
            r = WinMM.waveInStart(h);
            if (r != 0) { Close(); return r; }
            return 0;
        }

        void Loop()
        {
            while (run)
            {
                ev.WaitOne(200);
                for (int k = 0; k < NBuf && run; k++)
                {
                    IntPtr hd = hdrs[next];
                    uint fl = (uint)Marshal.ReadInt32(hd, offFlags);
                    if ((fl & 1u) == 0) break;              // WHDR_DONE — буферы идут по кругу
                    int nb = Marshal.ReadInt32(hd, offRec);
                    if (nb > 0) Process(bufs[next], nb);
                    if (!run) break;
                    Marshal.WriteInt32(hd, offFlags, (int)(fl & ~1u));
                    Marshal.WriteInt32(hd, offRec, 0);
                    WinMM.waveInAddBuffer(h, hd, hdrSize);
                    next = (next + 1) % NBuf;
                }
            }
        }

        void Process(IntPtr p, int nb)
        {
            var b = new byte[nb]; Marshal.Copy(p, b, 0, nb);
            int n = nb / 2; if (n == 0) return;
            double sum = 0; int pk = 0; bool nz = false;
            for (int i = 0; i < n; i++)
            {
                int s = (short)(b[2 * i] | (b[2 * i + 1] << 8));
                if (s != 0) nz = true;
                int a = s < 0 ? -s : s; if (a > pk) pk = a;
                sum += (double)s * s;
            }
            double rms = Math.Sqrt(sum / n) / 32768.0;
            float rDb = rms > 1e-7 ? (float)(20 * Math.Log10(rms)) : -140f;
            float pDb = pk > 0 ? (float)(20 * Math.Log10(pk / 32768.0)) : -140f;
            Action<byte[]> done = null; byte[] pcm = null;
            lock (lk)
            {
                Buffers++;
                for (int i = 0; i < n; i++) { ring[rp] = (short)(b[2 * i] | (b[2 * i + 1] << 8)) / 32768f; rp = (rp + 1) & (RingN - 1); }
                total += n;
                if (rDb > accRms) accRms = rDb; if (pDb > accPeak) accPeak = pDb; fresh = true;
                if (rDb > MaxRms) MaxRms = rDb; if (pDb > MaxPeak) MaxPeak = pDb;
                if (nz) { AnyNonZero = true; ZeroMs = 0; } else ZeroMs += n * 1000 / Rate;
                if (cap != null)
                {
                    int w = Math.Min(capLeft, nb);
                    cap.Write(b, 0, w); capLeft -= w;
                    if (capLeft <= 0) { pcm = cap.ToArray(); done = capDone; cap = null; capDone = null; }
                }
            }
            if (done != null) { try { done(pcm); } catch { } }
        }

        // Наибольшие уровни с прошлого чтения (dBFS).
        public bool Read(out float rmsDb, out float peakDb)
        {
            lock (lk)
            {
                rmsDb = accRms; peakDb = accPeak; bool f = fresh;
                accRms = accPeak = -200f; fresh = false; return f;
            }
        }

        public void BeginCapture(int ms, Action<byte[]> done)
        {
            lock (lk) { cap = new MemoryStream(); capLeft = Rate * 2 * ms / 1000; capDone = done; }
        }
        public void CancelCapture() { lock (lk) { cap = null; capDone = null; } }

        public void Close()
        {
            if (h == IntPtr.Zero) return;
            run = false;
            if (ev != null) ev.Set();
            if (th != null) { th.Join(1500); th = null; }
            try { WinMM.waveInReset(h); } catch { }
            for (int i = 0; hdrs != null && i < NBuf; i++)
            {
                if (hdrs[i] != IntPtr.Zero) { try { WinMM.waveInUnprepareHeader(h, hdrs[i], hdrSize); } catch { } Marshal.FreeHGlobal(hdrs[i]); hdrs[i] = IntPtr.Zero; }
                if (bufs[i] != IntPtr.Zero) { Marshal.FreeHGlobal(bufs[i]); bufs[i] = IntPtr.Zero; }
            }
            try { WinMM.waveInClose(h); } catch { }
            h = IntPtr.Zero; Device = int.MinValue;
            if (ev != null) { ev.Close(); ev = null; }
            CancelCapture();
        }

        public static byte[] Wav(byte[] pcm)
        {
            var ms = new MemoryStream(); var w = new BinaryWriter(ms);
            w.Write(Encoding.ASCII.GetBytes("RIFF")); w.Write(36 + pcm.Length); w.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
            w.Write(16); w.Write((short)1); w.Write((short)1); w.Write(Rate); w.Write(Rate * 2); w.Write((short)2); w.Write((short)16);
            w.Write(Encoding.ASCII.GetBytes("data")); w.Write(pcm.Length); w.Write(pcm); w.Flush();
            return ms.ToArray();
        }
        public static float PeakDb(byte[] pcm)
        {
            int pk = 0;
            for (int i = 0; i + 1 < pcm.Length; i += 2) { int s = (short)(pcm[i] | (pcm[i + 1] << 8)); if (s < 0) s = -s; if (s > pk) pk = s; }
            return pk > 0 ? (float)(20 * Math.Log10(pk / 32768.0)) : -140f;
        }
    }

    // ---------- Раскладка (логические px, координаты корпуса; холст = корпус + поля под тень) ----------
    static class Lay
    {
        public const int P = 18, CW = 636, CH = 312, GM = 14;
        public static readonly RectangleF Sel1 = new RectangleF(16, 178, 164, 26), Sel2 = new RectangleF(16, 210, 164, 26);
        public static readonly RectangleF BTest = new RectangleF(22, 244, 82, 22), BFold = new RectangleF(112, 244, 62, 22);
        public static readonly RectangleF Timer = new RectangleF(214, 30, 170, 48);
        public static readonly PointF Status = new PointF(216, 86);
        public static readonly PointF Info = new PointF(388, 40);
        public static readonly PointF BMin = new PointF(504, 48), BClose = new PointF(530, 48); public const float BR = 10;
        public static readonly RectangleF Screen = new RectangleF(212, 100, 376, 70);
        public static readonly RectangleF[] Pads = { new RectangleF(214, 182, 66, 54), new RectangleF(290, 182, 66, 54), new RectangleF(366, 182, 66, 54) };
        public static readonly PointF Toggle = new PointF(552, 216); public const float TR = 22;
        public static readonly PointF Led = new PointF(552, 186);
        public static readonly PointF Toggle2 = new PointF(492, 216);   // v8: «Облик»
        public static readonly PointF SkinTxt = new PointF(492, 186);
        public const float NutR = 18;
        // плашка
        public const int QP = 14, QW = 334, QH = 84;
        public static readonly PointF QDot = new PointF(24, 28);
        public static readonly RectangleF QTimer = new RectangleF(38, 12, 92, 32);
        public static readonly RectangleF QWave = new RectangleF(132, 12, 84, 32);
        public static readonly PointF QPause = new PointF(244, 28), QStop = new PointF(280, 28); public const float QR = 15;
    }

    enum Hit { None = 0, Sel1, Sel2, Test, Folder, Min, Close, PadRec, PadPause, PadStop, Toggle, Screen, Body, QPause, QStop, Skin, CRec, CPause, CStop, CMin, CClose }

    class UiState
    {
        public int Rec;                 // 0 ожидание, 1 запись, 2 пауза, 3 сохранение
        public float Lever = 1f;        // положение рычага 0 (вниз, выкл) … 1 (вверх, вкл), с отскоком
        public float Power = 1f;        // яркость экрана/светодиода 0…1
        public string Timer = "00:00:00";
        public string Status = ""; public bool StatusWarn;
        public string Area = "Весь экран", Mic = "Микрофон";
        public bool MicOn = true, MicBlocked, MicLive;
        public string TestText = "Проверить";
        public Hit Hot, Down;
        public bool[] En = new bool[24];
        public float CornL, CornR;      // v16: видимость угловых кнопок компактного режима 0…1 (fade ~150 мс)
        public double T;                // секунды — для пульса
        public float PeakDb = -140f; public bool Clip;
        public float Lever2 = 1f;       // v8: рычаг «Облик» 1 — вверх («Корпус»), 0 — вниз («Рамка»)
        public float SkinK = 0f;        // v8: 0 — корпус, 1 — рамка (плавный переход 0,4 с)
        public float RecK = 1f;         // v7: плавный переход в «запись» 0…1 (бирюза → красный)
        public bool Enh; public float Prog = -1f;   // v5: идёт «Обработка звука…», прогресс 0…1 (−1 — нет полоски)
        public bool IsEn(Hit h) { return En[(int)h]; }
    }

    // ---------- Синтетический голос (для --snapshot) ----------
    static class Synth
    {
        static Random rnd = new Random(7);
        public static void Fill(float[] buf, double t0, float amp)
        {
            double sr = MicMeter.Rate;
            for (int i = 0; i < buf.Length; i++)
            {
                double t = t0 + i / sr;
                double f0 = 132 + 14 * Math.Sin(2 * Math.PI * 1.3 * t);
                double ph = 2 * Math.PI * f0 * t;
                double v = Math.Sin(ph) * 0.9 + 0.55 * Math.Sin(2 * ph + 0.6) + 0.42 * Math.Sin(3 * ph + 1.1)
                         + 0.28 * Math.Sin(5 * ph + 0.2) + 0.18 * Math.Sin(7 * ph + 2.2) + 0.1 * Math.Sin(11 * ph);
                double env = 0.55 + 0.45 * Math.Sin(2 * Math.PI * 3.1 * t + 0.4);
                v = v * env * 0.42 + (rnd.NextDouble() - 0.5) * 0.04;
                buf[i] = (float)Math.Max(-1, Math.Min(1, v * amp));
            }
        }
    }

    // ---------- Осциллограф: синхронизация по переходу через ноль, послесвечение, свечение ----------
    class Scope
    {
        public const int Window = 480;      // 30 мс при 16 кГц — 3–4 периода голоса, пики читаются
        Bitmap trail, tmp; int W, H; float s;
        PointF[] cur; bool live;
        Brush[] glowBr; Brush coreBr;
        public float Gain = 4.2f;
        public float Pad = 6f;          // v12: поле по вертикали (мини-осциллограф — меньше)
        public float GlowK = 1f;        // v12: толщина свечения (мини-осциллограф — тоньше)

        public void Ensure(int w, int h, float sc)
        {
            if (trail != null && w == W && h == H) return;
            if (trail != null) { trail.Dispose(); tmp.Dispose(); }
            W = w; H = h; s = sc;
            trail = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            tmp = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            glowBr = null; cur = null;
        }

        // Цвет по амплитуде: середина бирюзовая, к пикам — светлее, у края — янтарь и красный (клиппинг).
        Brush Grad(float alpha, bool core)
        {
            var b = new LinearGradientBrush(new RectangleF(0, 0, 1, H + 1), Color.Black, Color.Black, LinearGradientMode.Vertical);
            Color t = core ? Theme.Mix(Theme.Teal, Color.White, 0.35f) : Theme.Teal;
            Color m = core ? Theme.Mix(Theme.TealHi, Color.White, 0.3f) : Theme.TealHi;
            var cb = new ColorBlend();
            cb.Colors = new Color[] { Theme.A(Theme.Red, alpha), Theme.A(Theme.Red, alpha), Theme.A(Theme.Amber, alpha), Theme.A(m, alpha), Theme.A(t, alpha),
                                      Theme.A(t, alpha), Theme.A(m, alpha), Theme.A(Theme.Amber, alpha), Theme.A(Theme.Red, alpha), Theme.A(Theme.Red, alpha) };
            cb.Positions = new float[] { 0f, 0.05f, 0.12f, 0.28f, 0.42f, 0.58f, 0.72f, 0.88f, 0.95f, 1f };
            b.InterpolationColors = cb;
            return b;
        }

        public static PointF[] Points(float[] buf, int avail, int w, int h, float pad, float gain)
        {
            int cols = Math.Max(2, (int)(w - 2 * pad));
            var pts = new PointF[cols];
            float mid = h / 2f, amp = h / 2f - pad;
            int n = buf.Length;
            if (avail < Window + 2)
            {
                for (int c = 0; c < cols; c++) pts[c] = new PointF(pad + c, mid);
                return pts;
            }
            // триггер: ищем восходящий переход через ноль (с гистерезисом) в окне перед последними 50 мс
            int lo = Math.Max(1, n - Window - 480), hi = n - Window, start = hi;
            float thr = 0.004f;
            for (int i = hi; i > lo; i--)
            {
                if (buf[i - 1] < 0 && buf[i] >= 0)
                {
                    bool ok = false;
                    for (int j = i - 1; j > Math.Max(0, i - 40); j--) if (buf[j] < -thr) { ok = true; break; }
                    if (ok) { start = i; break; }
                }
            }
            double per = (double)Window / cols;
            double th = Math.Tanh(gain);
            for (int c = 0; c < cols; c++)
            {
                int a = start + (int)(c * per), b = start + (int)((c + 1) * per);
                if (b <= a) b = a + 1;
                float best = 0;
                for (int i = a; i < b && i < n; i++) if (Math.Abs(buf[i]) > Math.Abs(best)) best = buf[i];
                float y = (float)(Math.Tanh(best * gain) / th);
                if (Math.Abs(best) >= 0.985f) y = Math.Sign(best);
                pts[c] = new PointF(pad + c, mid - y * amp);
            }
            return pts;
        }

        public void Push(float[] buf, int avail, bool isLive)
        {
            if (trail == null) return;
            live = isLive;
            float pad = Pad * s;
            cur = Points(buf, isLive ? avail : 0, W, H, pad, Gain);
            using (var g = Graphics.FromImage(tmp))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.Clear(Color.Transparent);
                g.CompositingMode = CompositingMode.SourceOver;
                using (var ia = Res.Attr(1, 1, 1, 0.64f))
                    g.DrawImage(trail, new Rectangle(0, 0, W, H), 0, 0, W, H, GraphicsUnit.Pixel, ia);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Theme.A(Theme.Teal, 0.55f), (GlowK < 1f ? 1.1f : 1.5f) * s) { LineJoin = LineJoin.Round })
                    g.DrawLines(pen, cur);
            }
            var t = trail; trail = tmp; tmp = t;
        }

        public void Draw(Graphics g, float x, float y, float power)
        {
            if (trail == null || cur == null || power <= 0.01f) return;
            using (var ia = Res.Attr(1, 1, 1, power))
                g.DrawImage(trail, new Rectangle((int)Math.Round(x), (int)Math.Round(y), W, H), 0, 0, W, H, GraphicsUnit.Pixel, ia);
            if (glowBr == null)
            {
                glowBr = new Brush[] { Grad(0.06f, false), Grad(0.13f, false), Grad(0.32f, false) };
                coreBr = Grad(1f, true);
            }
            var st = g.Save();
            g.TranslateTransform((float)Math.Round(x), (float)Math.Round(y));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float[] wd = { 10f * GlowK, 5.5f * GlowK, 2.8f * GlowK }; float[] ga = { 0.06f, 0.13f, 0.32f };
            for (int i = 0; i < 3; i++)
            {
                Pen pen = power >= 0.99f ? new Pen(glowBr[i], wd[i] * s) : new Pen(Theme.A(Theme.Teal, ga[i] * power), wd[i] * s);
                pen.LineJoin = LineJoin.Round; pen.StartCap = LineCap.Round; pen.EndCap = LineCap.Round;
                g.DrawLines(pen, cur); pen.Dispose();
            }
            using (var pen = power >= 0.99f ? new Pen(coreBr, 1.35f * s) : new Pen(Theme.A(Theme.Mix(Theme.Teal, Color.White, 0.35f), power), 1.35f * s))
            { pen.LineJoin = LineJoin.Round; g.DrawLines(pen, cur); }
            g.Restore(st);
        }
        // v12: та же волна одним цветом (пауза — янтарный приглушённый): след и свечение без градиента.
        public void DrawTint(Graphics g, float x, float y, Color c, float k)
        {
            if (trail == null || cur == null || k <= 0.01f) return;
            // след бирюзовый: яркость берём из зелёного канала и перекрашиваем в c
            var cm = new ColorMatrix(new float[][] {
                new float[] { 0, 0, 0, 0, 0 }, new float[] { c.R / 255f, c.G / 255f, c.B / 255f, 0, 0 }, new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 0.5f * k, 0 }, new float[] { 0, 0, 0, 0, 1 } });
            using (var ia = new ImageAttributes())
            {
                ia.SetColorMatrix(cm);
                g.DrawImage(trail, new Rectangle((int)Math.Round(x), (int)Math.Round(y), W, H), 0, 0, W, H, GraphicsUnit.Pixel, ia);
            }
            var st = g.Save();
            g.TranslateTransform((float)Math.Round(x), (float)Math.Round(y));
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float[] wd = { 10f * GlowK, 5.5f * GlowK, 2.8f * GlowK }; float[] ga = { 0.06f, 0.13f, 0.32f };
            for (int i = 0; i < 3; i++)
                using (var pen = new Pen(Theme.A(c, ga[i] * k), wd[i] * s) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round }) g.DrawLines(pen, cur);
            using (var pen = new Pen(Theme.A(Theme.Mix(c, Color.White, 0.25f), k), 1.35f * s) { LineJoin = LineJoin.Round }) g.DrawLines(pen, cur);
            g.Restore(st);
        }
    }

    // ---------- Отрисовка прибора ----------
    // ---------- v15: мини-осциллограф компактного режима — от звука, как большой ----------
    // Каждый кадр рисует текущий буфер сэмплов тем же Scope.Points, что и большой осциллограф (тот же массив sbuf, тот же кадр).
    // Без истории и прокрутки: тишина — ровная линия сразу, голос — волна вибрирует на месте.
    class MiniWave
    {
        public float Gain = 5.2f;      // большой — 4,2; мини немного усилен, чтобы в полосе 11 px было видно
        float[] buf; int avail; bool live;
        public void Reset() { buf = null; avail = 0; live = false; }

        public void Push(float[] b, int av, bool isLive, double t)
        {
            buf = b; avail = av; live = isLive;
        }

        // Точки кривой в прямоугольнике r (px устройства).
        public PointF[] Points(RectangleF r, float s)
        {
            int w = Math.Max(8, (int)Math.Round(r.Width)), h = Math.Max(4, (int)Math.Round(r.Height));
            PointF[] pts;
            if (buf == null) { pts = new PointF[] { new PointF(0, h / 2f), new PointF(w, h / 2f) }; }
            else pts = Scope.Points(buf, live ? avail : 0, w, h, 0.6f * s, Gain);
            for (int i = 0; i < pts.Length; i++) pts[i] = new PointF(r.X + pts[i].X, r.Y + pts[i].Y);
            return pts;
        }

        // c — цвет (запись — бирюза, пауза — приглушённый янтарь), k — яркость.
        public void Draw(Graphics g, RectangleF r, float s, Color c, float k)
        {
            var pts = Points(r, s);
            float mid = r.Y + r.Height / 2f, amp = Math.Max(1f, r.Height / 2f - 0.6f * s), me = 0f;
            for (int i = 0; i < pts.Length; i++) me = Math.Max(me, Math.Abs(pts[i].Y - mid) / amp);
            var sm = g.SmoothingMode; g.SmoothingMode = SmoothingMode.AntiAlias;
            float gk = 0.35f + 0.65f * Math.Min(1f, me);   // в тишине свечение почти гаснет — остаётся тонкая линия
            float[] wd = { 4.6f, 2.6f }; float[] ga = { 0.09f, 0.2f };
            for (int i = 0; i < 2; i++)
                using (var pen = new Pen(Theme.A(c, ga[i] * k * gk), wd[i] * s) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round })
                    g.DrawLines(pen, pts);
            using (var pen = new Pen(Theme.A(Theme.Mix(c, Color.White, 0.22f), (0.55f + 0.4f * Math.Min(1f, me * 2f)) * k), 1.15f * s) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round })
                g.DrawLines(pen, pts);
            g.SmoothingMode = sm;
        }
    }

    static class DeviceView
    {
        static RectangleF R(RectangleF r, float s) { return new RectangleF((r.X + Lay.P) * s, (r.Y + Lay.P) * s, r.Width * s, r.Height * s); }
        static PointF Pt(PointF p, float s) { return new PointF((p.X + Lay.P) * s, (p.Y + Lay.P) * s); }

        public static Hit HitTest(PointF lp)   // lp — логические координаты корпуса
        {
            if (Lay.Sel1.Contains(lp)) return Hit.Sel1;
            if (Lay.Sel2.Contains(lp)) return Hit.Sel2;
            if (Lay.BTest.Contains(lp)) return Hit.Test;
            if (Lay.BFold.Contains(lp)) return Hit.Folder;
            if (Dist(lp, Lay.BMin) <= Lay.BR + 2) return Hit.Min;
            if (Dist(lp, Lay.BClose) <= Lay.BR + 2) return Hit.Close;
            for (int i = 0; i < 3; i++) { var r = Lay.Pads[i]; r.Inflate(2, 2); if (r.Contains(lp)) return Hit.PadRec + i; }
            if (Math.Abs(lp.X - Lay.Toggle.X) <= 26 && lp.Y >= Lay.Toggle.Y - 30 && lp.Y <= Lay.Toggle.Y + 26) return Hit.Toggle;
            if (Math.Abs(lp.X - Lay.Toggle2.X) <= 26 && lp.Y >= Lay.Toggle2.Y - 30 && lp.Y <= Lay.Toggle2.Y + 26) return Hit.Skin;
            if (Lay.Screen.Contains(lp)) return Hit.Screen;
            return Hit.Body;
        }
        static float Dist(PointF a, PointF b) { float dx = a.X - b.X, dy = a.Y - b.Y; return (float)Math.Sqrt(dx * dx + dy * dy); }

        // v8: в «Рамке» (Halo = доля облика) у надписей тёмная подводка — читаются на любом фоне.
        public static float Halo, HaloS = 1f;
        static readonly PointF[] HaloOff = { new PointF(-1, 0), new PointF(1, 0), new PointF(0, -1), new PointF(0, 1), new PointF(0.7f, 0.7f) };
        static void Text(Graphics g, string t, Font f, Color c, RectangleF r, StringAlignment h, StringAlignment v)
        {
            using (var sf = new StringFormat(StringFormat.GenericTypographic))
            using (var b = new SolidBrush(c))
            {
                sf.Alignment = h; sf.LineAlignment = v; sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
                if (Halo > 0.01f)
                    using (var hb = new SolidBrush(Color.FromArgb((int)(150 * Halo * c.A / 255f), 0, 0, 0)))
                        foreach (var o in HaloOff) g.DrawString(t, f, hb, new RectangleF(r.X + o.X * HaloS, r.Y + o.Y * HaloS, r.Width, r.Height), sf);
                g.DrawString(t, f, b, r, sf);
            }
        }
        static void Spaced(Graphics g, string t, Font f, Color c, float x, float y, float sp, bool center)
        {
            using (var b = new SolidBrush(c))
            using (var sf = new StringFormat(StringFormat.GenericTypographic))
            {
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                float tot = 0; var ws = new float[t.Length];
                for (int i = 0; i < t.Length; i++) { ws[i] = g.MeasureString(t[i].ToString(), f, 1000, sf).Width; tot += ws[i] + (i < t.Length - 1 ? sp : 0); }
                float px = center ? x - tot / 2 : x;
                if (Halo > 0.01f)
                    using (var hb = new SolidBrush(Color.FromArgb((int)(150 * Halo * c.A / 255f), 0, 0, 0)))
                    {
                        float hx = px;
                        for (int i = 0; i < t.Length; i++) { foreach (var o in HaloOff) g.DrawString(t[i].ToString(), f, hb, hx + o.X * HaloS, y - f.Height / 2f + o.Y * HaloS, sf); hx += ws[i] + sp; }
                    }
                for (int i = 0; i < t.Length; i++) { g.DrawString(t[i].ToString(), f, b, px, y - f.Height / 2f, sf); px += ws[i] + sp; }
            }
        }
        static void Glow(Graphics g, GraphicsPath p, Color c, float s, float k)
        {
            float[] w = { 7f, 4f, 2f }; float[] a = { 0.07f, 0.12f, 0.22f };
            for (int i = 0; i < 3; i++) using (var pen = new Pen(Theme.A(c, a[i] * k), w[i] * s) { LineJoin = LineJoin.Round }) g.DrawPath(pen, p);
        }
        static void Dot(Graphics g, PointF c, float r, Color col, float glow)
        {
            if (glow > 0)
                for (int i = 3; i >= 1; i--)
                    using (var b = new SolidBrush(Theme.A(col, 0.10f * glow))) g.FillEllipse(b, c.X - r * (1 + i * 0.7f), c.Y - r * (1 + i * 0.7f), 2 * r * (1 + i * 0.7f), 2 * r * (1 + i * 0.7f));
            using (var b = new SolidBrush(col)) g.FillEllipse(b, c.X - r, c.Y - r, 2 * r, 2 * r);
            if (glow > 0) using (var b = new SolidBrush(Theme.A(Color.White, 0.55f * glow))) g.FillEllipse(b, c.X - r * 0.45f, c.Y - r * 0.55f, r * 0.8f, r * 0.7f);
        }

        public static void Render(Graphics g, float s, UiState st, Scope sc)
        {
            g.CompositingMode = CompositingMode.SourceCopy;
            g.Clear(Color.Transparent);
            g.CompositingMode = CompositingMode.SourceOver;
            float sk = Math.Max(0f, Math.Min(1f, st.SkinK));
            float ek = sk * sk * (3 - 2 * sk);          // v8: облик (сглаженный)
            Halo = ek; HaloS = Math.Max(1f, s);
            Res.DrawBody(g, Res.Get("body", s), 0, 0, 1f - ek);
            if (ek > 0.003f) Res.DrawBody(g, Res.Get("frame", s), 0, 0, ek);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            float pulse = (float)(0.5 + 0.5 * Math.Sin(st.T * 2 * Math.PI * 0.8));
            float pw = st.Power;

            // светящаяся кромка
            Color rimC = Theme.Teal; float rimA = 0.42f;
            if (st.Rec == 1) { rimC = Theme.Mix(Theme.Teal, Theme.Red, st.RecK); rimA = 0.42f + (0.03f + 0.4f * pulse) * st.RecK; }
            else if (st.Rec == 2) { rimC = Theme.Amber; rimA = 0.5f; }
            Res.DrawTint(g, Res.Get("rim", s), 0, 0, rimC, rimA * (0.15f + 0.85f * pw) * (1f - ek));
            // v8: светящийся контур «Рамки» (ярче кромки: он один обозначает форму прибора)
            if (ek > 0.003f) Res.DrawTint(g, Res.Get("outline", s), 0, 0, Theme.Mix(rimC, Color.White, 0.12f), Math.Min(1f, (rimA + 0.38f) * (0.35f + 0.65f * pw)) * ek);
            // v8: эмблема из аватарки — в обоих обликах
            Res.Draw(g, Res.Get("emblem", s), 0, 0, 1f);

            // ---- экран-осциллограф ----
            var scr = R(Lay.Screen, s);
            using (var clip = Theme.Round(scr, 10 * s))
            {
                g.SetClip(clip);
                if (pw > 0.01f)
                {
                    using (var pen = new Pen(Theme.A(Theme.Teal, 0.055f * pw), 1f))
                    {
                        for (int i = 1; i < 8; i++) { float x = scr.X + scr.Width * i / 8f; g.DrawLine(pen, x, scr.Y, x, scr.Bottom); }
                        for (int i = 1; i < 4; i++) { float y = scr.Y + scr.Height * i / 4f; g.DrawLine(pen, scr.X, y, scr.Right, y); }
                    }
                    using (var pen = new Pen(Theme.A(Theme.Teal, 0.12f * pw), 1f))
                    {
                        float my = scr.Y + scr.Height / 2f;
                        for (int i = 1; i < 40; i++) { float x = scr.X + scr.Width * i / 40f; g.DrawLine(pen, x, my - 2 * s, x, my + 2 * s); }
                    }
                    float dimW = string.IsNullOrEmpty(st.Status) ? 1f : st.StatusWarn ? 0.3f : 0.55f;
                    if (sc != null) sc.Draw(g, scr.X, scr.Y, pw * dimW);
                    // показания
                    var f1 = Theme.Px(Theme.UiSemi, 8.6f * s, FontStyle.Regular);
                    string left = !st.MicOn ? "ЗВУК ВЫКЛ" : st.MicBlocked ? "НЕТ ДОСТУПА К МИКРОФОНУ" : !st.MicLive ? "ВХОД —" :
                        "ВХОД  " + (st.PeakDb <= -99 ? "−∞" : st.PeakDb.ToString("0.0", System.Globalization.CultureInfo.GetCultureInfo("ru-RU")).Replace("-", "−")) + " дБ";
                    Color lc = st.MicBlocked ? Theme.Amber : Theme.Teal;
                    // подложки под показания, чтобы волна их не перекрывала
                    using (var bb = new SolidBrush(Color.FromArgb((int)(190 * pw), 5, 8, 10)))
                    {
                        float lw = g.MeasureString(left, f1, 1000, StringFormat.GenericTypographic).Width;
                        using (var p1 = Theme.Round(new RectangleF(scr.X + 5 * s, scr.Y + 4 * s, lw + 10 * s, 15 * s), 4 * s)) g.FillPath(bb, p1);
                        using (var p2 = Theme.Round(new RectangleF(scr.Right - 43 * s, scr.Y + 4 * s, 38 * s, 15 * s), 4 * s)) g.FillPath(bb, p2);
                    }
                    Text(g, left, f1, Theme.A(lc, 0.78f * pw), new RectangleF(scr.X + 10 * s, scr.Y + 6 * s, scr.Width * 0.6f, 12 * s), StringAlignment.Near, StringAlignment.Near);
                    var cr = new RectangleF(scr.Right - 40 * s, scr.Y + 6 * s, 32 * s, 12 * s);
                    using (var cp = Theme.Round(cr, 3 * s))
                    {
                        if (st.Clip) { using (var b = new SolidBrush(Theme.A(Theme.Red, 0.9f * pw))) g.FillPath(b, cp); Glow(g, cp, Theme.Red, s, pw); }
                        else using (var pen = new Pen(Theme.A(Theme.Mute, 0.35f * pw), 1f * s)) g.DrawPath(pen, cp);
                    }
                    Text(g, "CLIP", Theme.Px(Theme.UiSemi, 7.6f * s, FontStyle.Regular), st.Clip ? Color.White : Theme.A(Theme.Mute, 0.55f * pw), cr, StringAlignment.Center, StringAlignment.Center);
                    // строка состояния/подсказки — внизу экрана, до двух строк
                    if (!string.IsNullOrEmpty(st.Status))
                    {
                        var f2 = Theme.Px(Theme.UiFam, 9.2f * s, FontStyle.Regular);
                        var sr = new RectangleF(scr.X + 10 * s, scr.Y + 20 * s, scr.Width - 20 * s, scr.Height - (st.Prog >= 0 ? 33 : 24) * s);
                        using (var bg = new LinearGradientBrush(new RectangleF(scr.X, scr.Y + scr.Height * 0.3f, scr.Width, scr.Height * 0.7f + 1), Color.FromArgb(0, 4, 6, 8), Color.FromArgb(235, 4, 6, 8), LinearGradientMode.Vertical))
                            g.FillRectangle(bg, scr.X, scr.Y + scr.Height * 0.3f, scr.Width, scr.Height * 0.7f);
                        using (var sf = new StringFormat(StringFormat.GenericTypographic))
                        using (var b = new SolidBrush(Theme.A(st.StatusWarn ? Theme.Amber : Theme.Ink, 0.95f * pw)))
                        {
                            sf.LineAlignment = StringAlignment.Far; sf.Trimming = StringTrimming.EllipsisWord;
                            g.DrawString(st.Status, f2, b, sr, sf);
                        }
                    }
                    // v5: бирюзовый индикатор прогресса обработки звука
                    if (st.Prog >= 0)
                    {
                        var pr = new RectangleF(scr.X + 10 * s, scr.Bottom - 12 * s, scr.Width - 20 * s, 4 * s);
                        using (var pp = Theme.Round(pr, 2 * s))
                        using (var b = new SolidBrush(Theme.A(Theme.Teal, 0.16f * pw))) g.FillPath(b, pp);
                        float fw = Math.Max(4 * s, pr.Width * Math.Min(1f, st.Prog));
                        var fr = new RectangleF(pr.X, pr.Y, fw, pr.Height);
                        using (var fp = Theme.Round(fr, 2 * s))
                        {
                            Glow(g, fp, Theme.Teal, s, 0.8f * pw);
                            using (var b = new SolidBrush(Theme.A(Theme.Mix(Theme.Teal, Color.White, 0.15f), pw))) g.FillPath(b, fp);
                        }
                    }
                }
                g.ResetClip();
            }
            Res.Draw(g, Res.Get("gloss", s), scr.X, scr.Y, 1f);

            // ---- таймер ----
            var tr = R(Lay.Timer, s);
            Color tc = st.Rec == 1 ? Theme.Mix(Theme.Teal, Theme.Red, st.RecK) : st.Rec == 2 ? Theme.Amber : Theme.Teal;
            float ta = st.Rec == 2 ? 0.55f + 0.45f * pulse : st.Rec == 1 ? 1f : 0.92f;
            ta *= pw;
            using (var path = new GraphicsPath())
            using (var sf = new StringFormat(StringFormat.GenericTypographic))
            {
                sf.LineAlignment = StringAlignment.Center;
                path.AddString(st.Timer, Theme.DigitFam, (int)FontStyle.Regular, 40f * s, new RectangleF(tr.X, tr.Y - 20 * s, tr.Width + 40 * s, tr.Height + 40 * s), sf);
                if (ek > 0.01f)
                    using (var hp = new Pen(Color.FromArgb((int)(120 * ek), 0, 0, 0), 3.4f * s) { LineJoin = LineJoin.Round }) g.DrawPath(hp, path);
                if (ta > 0.01f)
                {
                    Glow(g, path, tc, s, ta * (st.Rec == 1 ? 1.2f : 0.8f));
                    using (var b = new SolidBrush(Theme.A(Theme.Mix(tc, Color.White, 0.12f), ta))) g.FillPath(b, path);
                }
                else using (var b = new SolidBrush(Color.FromArgb(18, 255, 255, 255))) g.FillPath(b, path);
            }
            // ---- состояние ----
            var sp = Pt(Lay.Status, s);
            string stt = pw < 0.5f ? "ВЫКЛЮЧЕНО" : st.Rec == 1 ? "ИДЁТ ЗАПИСЬ" : st.Rec == 2 ? "ПАУЗА" : st.Rec == 3 ? (st.Enh ? "ОБРАБОТКА ЗВУКА…" : "СОХРАНЯЮ…") : "ГОТОВ К ЗАПИСИ";
            Color sc2 = st.Rec == 1 ? Theme.Red : st.Rec == 2 ? Theme.Amber : Theme.Teal;
            float la = pw < 0.5f ? 0 : st.Rec == 1 ? 0.5f + 0.5f * pulse : 1f;
            Dot(g, new PointF(sp.X + 4 * s, sp.Y), 3.2f * s, la > 0 ? Theme.Mix(Color.FromArgb(40, 44, 50), sc2, la) : Color.FromArgb(40, 44, 50), la);
            Spaced(g, stt, Theme.Px(Theme.UiSemi, 8.4f * s, FontStyle.Regular), pw < 0.5f ? Theme.Dim : Theme.A(st.Rec == 0 ? Theme.Mix(Theme.Mute, Theme.Ink, 0.3f * ek) : sc2, 1f), sp.X + 13 * s, sp.Y, 1.0f * s, false);

            // ---- справка справа сверху ----
            var ip = Pt(Lay.Info, s);
            var fi = Theme.Px(Theme.UiFam, 9f * s, FontStyle.Regular);
            Text(g, "MP4 · H.264 · 15 к/с", fi, Theme.Mix(Theme.Mute, Theme.Ink, 0.35f * ek), new RectangleF(ip.X, ip.Y - 2 * s, 106 * s, 14 * s), StringAlignment.Near, StringAlignment.Center);
            Text(g, "F9 — старт / стоп", fi, Theme.Mix(Theme.Dim, Theme.Mute, 0.8f * ek), new RectangleF(ip.X, ip.Y + 13 * s, 106 * s, 14 * s), StringAlignment.Near, StringAlignment.Center);

            // ---- кнопки «в трей» и «выход» ----
            DrawRoundBtn(g, s, st, Hit.Min, Pt(Lay.BMin, s));
            DrawRoundBtn(g, s, st, Hit.Close, Pt(Lay.BClose, s));

            // ---- селекторы ----
            DrawSelector(g, s, st, Hit.Sel1, R(Lay.Sel1, s), st.Area, 0);
            DrawSelector(g, s, st, Hit.Sel2, R(Lay.Sel2, s), st.MicOn ? st.Mic : "Без звука", st.MicOn ? 1 : 2);
            DrawPill(g, s, st, Hit.Test, R(Lay.BTest, s), st.TestText);
            DrawPill(g, s, st, Hit.Folder, R(Lay.BFold, s), "Папка…");

            // ---- пэды ----
            Color[] pc = { Theme.Red, Theme.Amber, Color.FromArgb(225, 232, 240) };
            var pad = Res.Get("pad", s); var glow = Res.Get("padglow", s);
            for (int i = 0; i < 3; i++)
            {
                Hit h = Hit.PadRec + i;
                bool en = st.IsEn(h) && pw > 0.5f;
                bool down = st.Down == h && st.Hot == h && en, hot = st.Hot == h && en;
                float lit = 0;
                if (i == 0) lit = st.Rec == 1 ? (0.72f + 0.28f * pulse) * st.RecK + 0.10f * (1f - st.RecK) : (en ? 0.10f : 0f);
                if (i == 1) lit = st.Rec == 2 ? 0.55f + 0.35f * pulse : 0f;
                if (hot) lit = Math.Max(lit, i == 2 ? 0.22f : 0.3f);
                if (down) lit = Math.Max(lit, 0.95f);
                var r = Lay.Pads[i];
                float ox = (r.X + Lay.P - Lay.GM) * s, oy = (r.Y + Lay.P - Lay.GM) * s + (down ? 1.2f * s : 0);
                Res.Draw(g, pad, ox, oy, en ? 1f : 0.85f);
                Res.DrawTint(g, glow, ox, oy, pc[i], lit);
                var c = new PointF((r.X + r.Width / 2 + Lay.P) * s, (r.Y + r.Height / 2 + Lay.P) * s + (down ? 1.2f * s : 0));
                Color ic = lit > 0.5f ? Color.FromArgb(250, Theme.Mix(pc[i], Color.White, 0.75f)) : !en ? Color.FromArgb(70, 76, 84) : Theme.A(pc[i], 0.85f);
                using (var b = new SolidBrush(ic))
                {
                    if (i == 0) g.FillEllipse(b, c.X - 7 * s, c.Y - 7 * s, 14 * s, 14 * s);
                    else if (i == 1)
                    {
                        if (st.Rec == 2) g.FillPolygon(b, new[] { new PointF(c.X - 5 * s, c.Y - 7.5f * s), new PointF(c.X + 8 * s, c.Y), new PointF(c.X - 5 * s, c.Y + 7.5f * s) });
                        else { using (var p1 = Theme.Round(new RectangleF(c.X - 6.5f * s, c.Y - 7 * s, 4.4f * s, 14 * s), 1.4f * s)) g.FillPath(b, p1); using (var p2 = Theme.Round(new RectangleF(c.X + 2.1f * s, c.Y - 7 * s, 4.4f * s, 14 * s), 1.4f * s)) g.FillPath(b, p2); }
                    }
                    else using (var p3 = Theme.Round(new RectangleF(c.X - 6.5f * s, c.Y - 6.5f * s, 13 * s, 13 * s), 2.2f * s)) g.FillPath(b, p3);
                }
            }

            // ---- тумблер и светодиод питания ----
            var lp = Pt(Lay.Led, s);
            Dot(g, lp, 2.8f * s, pw > 0.02f ? Theme.Mix(Color.FromArgb(30, 40, 42), Theme.Teal, pw) : Color.FromArgb(30, 36, 38), pw);
            var nut = Res.Get("nut", s);
            Res.Draw(g, nut, (Lay.Toggle.X + Lay.P - Lay.NutR) * s, (Lay.Toggle.Y + Lay.P - Lay.NutR) * s, 1f);
            Res.Draw(g, nut, (Lay.Toggle2.X + Lay.P - Lay.NutR) * s, (Lay.Toggle2.Y + Lay.P - Lay.NutR) * s, 1f);
            DrawLeverHQ(g, s, Lay.Toggle, st.Lever, st.Hot == Hit.Toggle);
            DrawLeverHQ(g, s, Lay.Toggle2, st.Lever2, st.Hot == Hit.Skin);
            // v8: подпись положения «Облика» над тумблером
            var tp = Pt(Lay.SkinTxt, s);
            bool frameNow = st.Lever2 < 0.5f;
            Spaced(g, frameNow ? "РАМКА" : "КОРПУС", Theme.Px(Theme.UiSemi, 6.9f * s, FontStyle.Regular),
                   Theme.A(Theme.Teal, pw > 0.5f ? 0.9f : 0.35f), tp.X, tp.Y, 0.9f * s, true);
        }

        // v8: при s<1.5 рычаг рисуется в 2× во временный кадр и уменьшается качественно — насечка и блики как на @2x.
        static Bitmap levBuf; static float levBufS;
        static void DrawLeverHQ(Graphics g, float s, PointF lc, float t, bool hot)
        {
            var c = Pt(lc, s);
            if (s >= 1.5f) { DrawLever(g, s, c, t, hot); return; }
            const float half = 34f;
            int ox = (int)Math.Floor(c.X - half * s), oy = (int)Math.Floor(c.Y - half * s);
            int n = (int)Math.Ceiling(2 * half * s) + 2;
            if (levBuf == null || levBufS != s) { if (levBuf != null) levBuf.Dispose(); levBuf = new Bitmap(n * 2, n * 2, PixelFormat.Format32bppPArgb); levBufS = s; }
            using (var lg = Graphics.FromImage(levBuf))
            {
                lg.CompositingMode = CompositingMode.SourceCopy; lg.Clear(Color.Transparent); lg.CompositingMode = CompositingMode.SourceOver;
                lg.SmoothingMode = SmoothingMode.AntiAlias; lg.PixelOffsetMode = PixelOffsetMode.HighQuality;
                DrawLever(lg, 2 * s, new PointF((c.X - ox) * 2, (c.Y - oy) * 2), t, hot);
            }
            var im = g.InterpolationMode; var po = g.PixelOffsetMode; var cm = g.CompositingQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; g.PixelOffsetMode = PixelOffsetMode.HighQuality; g.CompositingQuality = CompositingQuality.HighQuality;
            using (var ia = new ImageAttributes())
            {
                ia.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(levBuf, new Rectangle(ox, oy, n, n), 0, 0, n * 2, n * 2, GraphicsUnit.Pixel, ia);
            }
            g.InterpolationMode = im; g.PixelOffsetMode = po; g.CompositingQuality = cm;
        }

        static void DrawRoundBtn(Graphics g, float s, UiState st, Hit h, PointF c)
        {
            bool hot = st.Hot == h, down = st.Down == h && hot;
            float r = Lay.BR * s;
            if (hot) using (var b = new SolidBrush(h == Hit.Close ? Color.FromArgb(down ? 200 : 150, 200, 50, 46) : Color.FromArgb(down ? 70 : 40, 255, 255, 255))) g.FillEllipse(b, c.X - r, c.Y - r, 2 * r, 2 * r);
            Color ic = hot ? Color.White : Color.FromArgb(150, 158, 168);
            using (var pen = new Pen(ic, 1.5f * s) { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                float k = 3.6f * s;
                if (h == Hit.Min) g.DrawLine(pen, c.X - k, c.Y + 0.5f * s, c.X + k, c.Y + 0.5f * s);
                else { g.DrawLine(pen, c.X - k, c.Y - k, c.X + k, c.Y + k); g.DrawLine(pen, c.X - k, c.Y + k, c.X + k, c.Y - k); }
            }
        }

        static void DrawSelector(Graphics g, float s, UiState st, Hit h, RectangleF r, string text, int icon)
        {
            bool en = st.IsEn(h) && st.Power > 0.5f, hot = st.Hot == h && en;
            if (hot) using (var p = Theme.Round(r, r.Height / 2)) using (var b = new SolidBrush(Color.FromArgb(st.Down == h ? 34 : 20, 255, 255, 255))) g.FillPath(b, p);
            Color ac = en ? Theme.Teal : Theme.Dim, tc = en ? Theme.Ink : Theme.Dim;
            float cx = r.X + 15 * s, cy = r.Y + r.Height / 2;
            using (var pen = new Pen(ac, 1.4f * s) { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                if (icon == 0)
                {
                    using (var p = Theme.Round(new RectangleF(cx - 6.5f * s, cy - 5.5f * s, 13 * s, 8.5f * s), 1.5f * s)) g.DrawPath(pen, p);
                    g.DrawLine(pen, cx, cy + 3 * s, cx, cy + 5.5f * s); g.DrawLine(pen, cx - 3.5f * s, cy + 5.8f * s, cx + 3.5f * s, cy + 5.8f * s);
                }
                else
                {
                    using (var p = Theme.Round(new RectangleF(cx - 2.6f * s, cy - 6.5f * s, 5.2f * s, 8.5f * s), 2.6f * s)) g.DrawPath(pen, p);
                    g.DrawArc(pen, cx - 5 * s, cy - 3.5f * s, 10 * s, 8 * s, 0, 180);
                    g.DrawLine(pen, cx, cy + 4.5f * s, cx, cy + 6.5f * s);
                    if (icon == 2) using (var p2 = new Pen(Theme.Amber, 1.5f * s) { StartCap = LineCap.Round, EndCap = LineCap.Round }) g.DrawLine(p2, cx - 6 * s, cy - 6 * s, cx + 6 * s, cy + 6 * s);
                }
            }
            Text(g, text, Theme.Px(Theme.UiFam, 11.2f * s, FontStyle.Regular), tc, new RectangleF(r.X + 28 * s, r.Y, r.Width - 46 * s, r.Height), StringAlignment.Near, StringAlignment.Center);
            float ax = r.Right - 13 * s;
            using (var b = new SolidBrush(ac)) g.FillPolygon(b, new[] { new PointF(ax - 4 * s, cy - 2 * s), new PointF(ax + 4 * s, cy - 2 * s), new PointF(ax, cy + 2.8f * s) });
        }

        static void DrawPill(Graphics g, float s, UiState st, Hit h, RectangleF r, string text)
        {
            bool en = st.IsEn(h) && st.Power > 0.5f, hot = st.Hot == h && en, down = st.Down == h && hot;
            if (hot) using (var p = Theme.Round(r, r.Height / 2)) using (var b = new SolidBrush(Color.FromArgb(down ? 50 : 28, 255, 255, 255))) g.FillPath(b, p);
            bool active = text != "Проверить" && h == Hit.Test;
            Text(g, text, Theme.Px(Theme.UiSemi, 10.4f * s, FontStyle.Regular), !en && !active ? Theme.Dim : active ? Theme.Teal : Theme.Ink,
                 new RectangleF(r.X, r.Y + (down ? 1 : 0) * s, r.Width, r.Height), StringAlignment.Center, StringAlignment.Center);
        }

        // Рычаг тумблера: вид спереди; при повороте конец рычага уходит вверх/вниз, у середины — смотрит на нас.
        static void DrawLever(Graphics g, float s, PointF c, float t, bool hot)
        {
            double th = (-58 + 116 * t) * Math.PI / 180;
            float L = 19f * s;
            float dy = -(float)Math.Sin(th) * L;
            float rt = (6.4f + 1.8f * (float)Math.Cos(th)) * s;
            var tip = new PointF(c.X, c.Y + dy);
            // тень наконечника
            using (var b = new SolidBrush(Color.FromArgb(70, 0, 0, 0)))
            {
                g.FillEllipse(b, tip.X - rt + 2 * s, tip.Y - rt + 3 * s, 2 * rt, 2 * rt);
            }
            // шток (сужается к основанию); основание круглое — выходит из отверстия втулки гайки (v6)
            float wb = 2.5f * s, wt = 4.4f * s;
            if (Math.Abs(dy) > 1f)
            {
                // мягкая тень штока на гайке
                using (var sp = new GraphicsPath())
                {
                    sp.AddPolygon(new[] { new PointF(c.X - wb + 1.2f * s, c.Y + 1.6f * s), new PointF(c.X + wb + 1.2f * s, c.Y + 1.6f * s), new PointF(tip.X + wt + 1.6f * s, tip.Y + 2.4f * s), new PointF(tip.X - wt + 1.6f * s, tip.Y + 2.4f * s) });
                    sp.AddEllipse(c.X - wb + 1.2f * s, c.Y - wb + 1.6f * s, 2 * wb, 2 * wb);
                    using (var b = new SolidBrush(Color.FromArgb(55, 0, 0, 0))) g.FillPath(b, sp);
                }
                var poly = new[] { new PointF(c.X - wb, c.Y), new PointF(c.X + wb, c.Y), new PointF(tip.X + wt, tip.Y), new PointF(tip.X - wt, tip.Y) };
                using (var br = new LinearGradientBrush(new RectangleF(c.X - wt, 0, 2 * wt, 10), Color.FromArgb(120, 126, 134), Color.FromArgb(40, 44, 50), LinearGradientMode.Horizontal))
                {
                    var cb = new ColorBlend();
                    cb.Colors = new[] { Color.FromArgb(60, 64, 70), Color.FromArgb(205, 212, 220), Color.FromArgb(120, 126, 134), Color.FromArgb(44, 48, 54) };
                    cb.Positions = new[] { 0f, 0.35f, 0.6f, 1f };
                    br.InterpolationColors = cb;
                    g.FillPolygon(br, poly);
                    // скруглённое основание штока: круг того же металла в центре втулки
                    g.FillEllipse(br, c.X - wb, c.Y - wb, 2 * wb, 2 * wb);
                }
                // тёмный зазор втулки вокруг основания (рисуется только с нижней стороны штока, чтобы шток «уходил» внутрь)
                using (var pen = new Pen(Color.FromArgb(150, 10, 11, 13), 0.9f * s))
                {
                    float a0 = dy < 0 ? 20f : 200f;
                    g.DrawArc(pen, c.X - wb - 0.5f * s, c.Y - wb - 0.5f * s, 2 * wb + 1f * s, 2 * wb + 1f * s, a0, 140f);
                }
            }
            // наконечник
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(tip.X - rt, tip.Y - rt, 2 * rt, 2 * rt);
                using (var br = new PathGradientBrush(path))
                {
                    br.CenterPoint = new PointF(tip.X - rt * 0.35f, tip.Y - rt * 0.45f);
                    br.CenterColor = hot ? Color.FromArgb(250, 252, 255) : Color.FromArgb(232, 237, 242);
                    br.SurroundColors = new[] { Color.FromArgb(78, 84, 92) };
                    g.FillPath(br, path);
                }
                using (var pen = new Pen(Color.FromArgb(120, 20, 22, 26), 0.8f * s)) g.DrawPath(pen, path);
            }
        }

        // ---------- плашка ----------
        public static Hit QHit(PointF lp)
        {
            if (Dist(lp, Lay.QPause) <= Lay.QR + 2) return Hit.QPause;
            if (Dist(lp, Lay.QStop) <= Lay.QR + 2) return Hit.QStop;
            return Hit.Body;
        }
        public static void RenderPlaque(Graphics g, float s, UiState st, float[] buf, int avail)
        {
            g.CompositingMode = CompositingMode.SourceCopy;
            g.Clear(Color.Transparent);
            g.CompositingMode = CompositingMode.SourceOver;
            Res.DrawBody(g, Res.Get("plaque", s), 0, 0, 1f);
            g.SmoothingMode = SmoothingMode.AntiAlias; g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            float pulse = (float)(0.5 + 0.5 * Math.Sin(st.T * 2 * Math.PI * 0.8));
            Color c = st.Rec == 2 ? Theme.Amber : Theme.Red;
            Res.DrawTint(g, Res.Get("plaque_rim", s), 0, 0, c, st.Rec == 1 ? 0.45f + 0.4f * pulse : 0.5f);
            float q = Lay.QP * s;
            Dot(g, new PointF(q + Lay.QDot.X * s, q + Lay.QDot.Y * s), 5f * s, st.Rec == 1 ? Theme.Mix(Color.FromArgb(90, 20, 18), Theme.Red, 0.35f + 0.65f * pulse) : Theme.Amber, st.Rec == 1 ? pulse : 0.5f);
            var tr = new RectangleF(q + Lay.QTimer.X * s, q + (Lay.QTimer.Y - 12) * s, Lay.QTimer.Width * s + 20 * s, (Lay.QTimer.Height + 24) * s);
            using (var path = new GraphicsPath())
            using (var sf = new StringFormat(StringFormat.GenericTypographic))
            {
                sf.LineAlignment = StringAlignment.Center;
                path.AddString(st.Timer, Theme.DigitFam, (int)FontStyle.Regular, 23f * s, tr, sf);
                Glow(g, path, c, s, 0.6f);
                using (var b = new SolidBrush(Theme.Mix(c, Color.White, 0.2f))) g.FillPath(b, path);
            }
            var wr = new RectangleF(q + Lay.QWave.X * s, q + Lay.QWave.Y * s, Lay.QWave.Width * s, Lay.QWave.Height * s);
            if (buf != null)
            {
                var pts = Scope.Points(buf, avail, (int)wr.Width, (int)wr.Height, 4 * s, 4.2f);
                var st2 = g.Save();
                using (var clip = Theme.Round(wr, 8 * s)) g.SetClip(clip);
                g.TranslateTransform(wr.X, wr.Y);
                using (var pen = new Pen(Theme.A(Theme.Teal, 0.18f), 4.5f * s) { LineJoin = LineJoin.Round }) g.DrawLines(pen, pts);
                using (var pen = new Pen(Theme.Mix(Theme.Teal, Color.White, 0.3f), 1.2f * s) { LineJoin = LineJoin.Round }) g.DrawLines(pen, pts);
                g.Restore(st2);
            }
            DrawQBtn(g, s, st, Hit.QPause, new PointF(q + Lay.QPause.X * s, q + Lay.QPause.Y * s));
            DrawQBtn(g, s, st, Hit.QStop, new PointF(q + Lay.QStop.X * s, q + Lay.QStop.Y * s));
        }
        static void DrawQBtn(Graphics g, float s, UiState st, Hit h, PointF c)
        {
            bool hot = st.Hot == h, down = hot && st.Down == h;
            float r = (Lay.QR - 1.5f) * s;
            Color col = h == Hit.QPause ? Theme.Amber : Color.FromArgb(225, 232, 240);
            if (hot || (h == Hit.QPause && st.Rec == 2))
                using (var b = new SolidBrush(Theme.A(col, down ? 0.45f : 0.22f))) g.FillEllipse(b, c.X - r, c.Y - r, 2 * r, 2 * r);
            using (var b = new SolidBrush(hot ? Color.White : Theme.A(col, 0.9f)))
            {
                if (h == Hit.QStop) using (var p = Theme.Round(new RectangleF(c.X - 5 * s, c.Y - 5 * s, 10 * s, 10 * s), 1.8f * s)) g.FillPath(b, p);
                else if (st.Rec == 2) g.FillPolygon(b, new[] { new PointF(c.X - 3.5f * s, c.Y - 6 * s), new PointF(c.X + 6.5f * s, c.Y), new PointF(c.X - 3.5f * s, c.Y + 6 * s) });
                else { g.FillRectangle(b, c.X - 5 * s, c.Y - 5.5f * s, 3.4f * s, 11 * s); g.FillRectangle(b, c.X + 1.6f * s, c.Y - 5.5f * s, 3.4f * s, 11 * s); }
            }
        }
    }

    // ---------- v11: компактный режим — плашка с эмблемой db, кнопки внутри букв ----------
    // Полости и палочки ищутся по PNG эмблемы (альфа + цвет): буквы — мятные непрозрачные пиксели (A>160, R>150),
    // полости — фон, не связанный с краем картинки (заливка от краёв), палочки — вертикальные штрихи в строке у верха букв.
    // Хит-тест — по маске (байт на пиксель холста): 1 — Запись (полость «d»), 2 — Пауза (палочки «d» и «b»), 3 — Стоп (полость «b»).
    // Геометрия эмблемы не меняется: PNG рисуется 1:1, подсветка — отдельными слоями под/над ним.
    static class CompactView
    {
        // окно компактного режима — часть холста полного прибора (логические px холста, с полем Lay.P)
        public const float X0 = 14, Y0 = 10, W = 204, H = 206;
        public static readonly RectangleF Plate = new RectangleF(26, 22, 180, 182);    // плашка (холст)
        public static readonly RectangleF TextBand = new RectangleF(40, 171, 152, 15);  // строка «BIM DESIGN» эмблемы (холст)
        public static readonly RectangleF EmbLp = new RectangleF(26, 17, 143, 147);     // эмблема в координатах корпуса (двойной щелчок)
        public const float CX = 115.5f;                                                 // середина букв по X (холст)
        // v13: мини-осциллограф = полоса уровня v11 (80 px по X, ось y=194,5 — под таймером v11 y=181; волна ±5 px);
        // в покое при включённом питании — та же полоса на месте «BIM DESIGN» (ось y=178,5 — середина строки надписи).
        public const float MW = 80, MH = 11, MYIdle = 178.5f, MYRec = 194.5f;
        // v16: угловые кнопки (холст): центры — 22 px от верхних углов плашки, радиус как у кнопок полного режима (Lay.BR);
        // зоны наведения 50×48 px в верхних углах плашки (каска и рулон начинаются с x≈79 и кончаются x≈139 — не задеваются).
        public static readonly PointF BMinC = new PointF(48, 44), BCloseC = new PointF(184, 44);
        public const float CornW = 50, CornH = 48;
        // v17: облик «Рамка» — без плашки. Каска с рулоном (холст) x 78…140, y 35…85; кнопки — слева и справа от неё.
        public static bool Bare;   // true — компакт без плашки (облик «Рамка»); ставит Render по UiState.SkinK и MainForm перед хит-тестом
        public static readonly PointF BMinB = new PointF(62, 48), BCloseB = new PointF(158, 48);
        public static readonly RectangleF ZoneMinB = new RectangleF(38, 24, 39, 54), ZoneCloseB = new RectangleF(143, 24, 39, 54);
        // 0 — вне углов, 1 — левый верхний (Свернуть), 2 — правый верхний (Закрыть); только внутри скруглённой плашки
        public static int CornerAt(float s, Point p)
        {
            float lx = p.X / s + X0, ly = p.Y / s + Y0;
            if (Bare) return ZoneMinB.Contains(lx, ly) ? 1 : ZoneCloseB.Contains(lx, ly) ? 2 : 0;   // v17
            if (ly < Plate.Y || ly > Plate.Y + CornH) return 0;
            int k = lx >= Plate.X && lx <= Plate.X + CornW ? 1 : lx <= Plate.Right && lx >= Plate.Right - CornW ? 2 : 0;
            if (k == 0) return 0;
            using (var path = Theme.Round(PlateRect(s), 24 * s)) if (!path.IsVisible(p)) return 0;
            return k;
        }
        static PointF CornC(float s, Hit h) { var c = h == Hit.CMin ? (Bare ? BMinB : BMinC) : (Bare ? BCloseB : BCloseC); return new PointF((c.X - X0) * s, (c.Y - Y0) * s); }
        static bool OnCornBtn(float s, Point p, Hit h) { var c = CornC(s, h); float dx = p.X - c.X, dy = p.Y - c.Y, r = (Lay.BR + 2) * s; return dx * dx + dy * dy <= r * r; }
        static Bitmap embNT; static float embNTs = -1f;
        // Эмблема без надписи «BIM DESIGN»: стираются только бирюзовые пиксели надписи (G−R > 55) в строке надписи;
        // беловатое свечение низа букв «db» остаётся — горизонтального среза нет.
        static string embNTp = "";
        static Bitmap EmblemNoText(float s)
        {
            var em = Res.Get("emblem", s); if (em == null) return null;
            if (embNT != null && Math.Abs(embNTs - s) < 0.0001f && embNTp == Theme.Cur.Id) return embNT;   // v19: и облик тот же
            embNTp = Theme.Cur.Id;
            if (embNT != null) { embNT.Dispose(); embNT = null; }
            var b = new Bitmap(em.Width, em.Height, PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(b)) { g.CompositingMode = CompositingMode.SourceCopy; g.DrawImage(em, new Rectangle(0, 0, em.Width, em.Height), 0, 0, em.Width, em.Height, GraphicsUnit.Pixel); }
            var r = Rectangle.Intersect(Rectangle.Round(new RectangleF(TextBand.X * s, (TextBand.Y - 2) * s, TextBand.Width * s, (TextBand.Height + 4) * s)), new Rectangle(0, 0, b.Width, b.Height));
            var d = b.LockBits(r, ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            var row = new int[r.Width];
            for (int y = 0; y < r.Height; y++)
            {
                Marshal.Copy(d.Scan0 + y * d.Stride, row, 0, r.Width);
                for (int x = 0; x < r.Width; x++)
                {
                    int px = row[x], a = (px >> 24) & 255, rr = (px >> 16) & 255, gg = (px >> 8) & 255;
                    if (a > 0 && (gg - rr) * 255 > 55 * a) row[x] = 0;
                }
                Marshal.Copy(row, 0, d.Scan0 + y * d.Stride, r.Width);
            }
            b.UnlockBits(d);
            embNT = b; embNTs = s;
            return b;
        }

        class Shape { public Bitmap Fill, Glow; public Rectangle Box; public Point GlowOff; public PointF C; }
        static float ms = -1f; static byte[] map; static int mw, mh;
        static byte[] embA;   // v17: альфа эмблемы — перетаскивание без плашки только за непрозрачные пиксели
        static Shape[] shapes = new Shape[4];
        static PointF[] stemC = new PointF[2];
        public static string Info = "";

        public static int Off(float v, float s) { return (int)Math.Round(v * s); }

        static void Ensure(float s)
        {
            if (Math.Abs(ms - s) < 0.0001f) return;
            ms = s; map = null; Info = "";
            for (int i = 0; i < 4; i++) if (shapes[i] != null) { shapes[i].Fill.Dispose(); shapes[i].Glow.Dispose(); shapes[i] = null; }
            var em = Res.Get("emblem", s); if (em == null) { Info = "нет emblem"; return; }
            int w = em.Width, h = em.Height, n = w * h; mw = w; mh = h;
            var px = new int[n];
            var bd = em.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            for (int y = 0; y < h; y++) Marshal.Copy(bd.Scan0 + y * bd.Stride, px, y * w, w);
            em.UnlockBits(bd);
            embA = new byte[n]; for (int i = 0; i < n; i++) embA[i] = (byte)((px[i] >> 24) & 255);
            var L = new bool[n]; int x0 = w, y0 = h, x1 = -1, y1 = -1;
            for (int i = 0; i < n; i++)
            {
                int a = (px[i] >> 24) & 255, r = (px[i] >> 16) & 255;
                if (a > 160 && r > 150) { L[i] = true; int x = i % w, y = i / w; if (x < x0) x0 = x; if (x > x1) x1 = x; if (y < y0) y0 = y; if (y > y1) y1 = y; }
            }
            if (x1 < 0) { Info = "буквы не найдены"; return; }
            // фон снаружи букв — заливка от краёв картинки; остальное (не буква) внутри — полости
            var lab = new int[n]; var st = new Stack<int>();
            for (int x = 0; x < w; x++) { Flood(x, L, lab, w, h, -1, st, null); Flood((h - 1) * w + x, L, lab, w, h, -1, st, null); }
            for (int y = 0; y < h; y++) { Flood(y * w, L, lab, w, h, -1, st, null); Flood(y * w + w - 1, L, lab, w, h, -1, st, null); }
            var holes = new List<List<int>>(); int id = 0;
            int minPix = Math.Max(20, (x1 - x0) * (y1 - y0) / 100);
            for (int y = y0; y <= y1; y++)
                for (int x = x0; x <= x1; x++)
                {
                    int i = y * w + x; if (L[i] || lab[i] != 0) continue;
                    var pix = new List<int>(); Flood(i, L, lab, w, h, ++id, st, pix);
                    if (pix.Count >= minPix) holes.Add(pix);
                }
            if (holes.Count < 2) { Info = "полостей найдено " + holes.Count; return; }
            holes.Sort((p, q) => q.Count.CompareTo(p.Count));
            holes = holes.GetRange(0, 2);
            holes.Sort((p, q) => (p[0] % w).CompareTo(q[0] % w));   // слева — «d», справа — «b»
            // палочки: строка на 12% высоты букв сверху пересекает только вертикальные штрихи
            int yr = y0 + (int)(0.12f * (y1 - y0));
            var runs = new List<int[]>();
            for (int x = x0; x <= x1; x++)
            {
                if (!L[yr * w + x]) continue;
                int xs = x; while (x + 1 <= x1 && L[yr * w + x + 1]) x++;
                runs.Add(new[] { xs, x });
            }
            runs.Sort((p, q) => (q[1] - q[0]).CompareTo(p[1] - p[0]));
            if (runs.Count < 2) { Info = "палочек найдено " + runs.Count; return; }
            runs = runs.GetRange(0, 2); runs.Sort((p, q) => p[0].CompareTo(q[0]));
            map = new byte[n];
            var stemA = new byte[n]; int sx0 = w, sx1 = 0;
            for (int k = 0; k < 2; k++)
            {
                int a0 = runs[k][0] - 1, a1 = runs[k][1] + 1; double sy = 0; int cnt = 0;
                // v17: маска палочки без ступенек — вес по альфе и яркости буквы и растушёвка ~1,5 px на боковых срезах
                float fe = Math.Max(1f, 1.5f * s);
                for (int y = Math.Max(0, y0 - 2); y <= Math.Min(h - 1, y1 + 2); y++)
                    for (int x = Math.Max(0, a0 - 3); x <= Math.Min(w - 1, a1 + 3); x++)
                    {
                        int i = y * w + x; int a = (px[i] >> 24) & 255, r = (px[i] >> 16) & 255;
                        if (L[i] && x >= a0 && x <= a1) { map[i] = 2; sy += y; cnt++; }
                        float dxe = Math.Min(x - (runs[k][0] - 0.5f), (runs[k][1] + 0.5f) - x);
                        float wx = Math.Max(0f, Math.Min(1f, 0.5f + dxe / fe));
                        float wr = Math.Max(0f, Math.Min(1f, (r - 70) / 70f));
                        int v = (int)(a * wx * wr + 0.5f);
                        if (v > stemA[i]) stemA[i] = (byte)v;
                    }
                stemC[k] = new PointF((runs[k][0] + runs[k][1]) / 2f, cnt > 0 ? (float)(sy / cnt) : (y0 + y1) / 2f);
                sx0 = Math.Min(sx0, a0 - 3); sx1 = Math.Max(sx1, a1 + 3);
            }
            int dil = Math.Max(1, (int)Math.Round(2 * s));
            var info = new StringBuilder();
            info.AppendFormat("буквы {0},{1}-{2},{3}; палочки x {4}-{5} и {6}-{7}", x0, y0, x1, y1, runs[0][0], runs[0][1], runs[1][0], runs[1][1]);
            for (int k = 0; k < 2; k++)
            {
                var pix = holes[k]; byte code = (byte)(k == 0 ? 1 : 3);
                int hx0 = w, hy0 = h, hx1 = 0, hy1 = 0; double cx = 0, cy = 0;
                foreach (int i in pix) { int x = i % w, y = i / w; hx0 = Math.Min(hx0, x); hx1 = Math.Max(hx1, x); hy0 = Math.Min(hy0, y); hy1 = Math.Max(hy1, y); cx += x; cy += y; }
                var fill = new byte[n];
                foreach (int i in pix)
                {
                    int x = i % w, y = i / w;
                    for (int dy = -dil; dy <= dil; dy++)
                        for (int dx = -dil; dx <= dil; dx++)
                        {
                            int xx = x + dx, yy = y + dy; if (xx < 0 || yy < 0 || xx >= w || yy >= h) continue;
                            int j = yy * w + xx; fill[j] = 255; if (map[j] == 0) map[j] = code;
                        }
                }
                var box = Rectangle.FromLTRB(Math.Max(0, hx0 - dil), Math.Max(0, hy0 - dil), Math.Min(w, hx1 + dil + 1), Math.Min(h, hy1 + dil + 1));
                shapes[code] = MakeShape(fill, w, box, s);
                shapes[code].C = new PointF((float)(cx / pix.Count), (float)(cy / pix.Count));
                info.AppendFormat("; полость {0} {1},{2}-{3},{4} ({5} px)", k == 0 ? "d" : "b", hx0, hy0, hx1, hy1, pix.Count);
            }
            shapes[2] = MakeShape(stemA, w, Rectangle.FromLTRB(Math.Max(0, sx0), Math.Max(0, y0 - 2), Math.Min(w, sx1 + 1), Math.Min(h, y1 + 3)), s);
            Info = info.ToString();
        }

        static void Flood(int start, bool[] L, int[] lab, int w, int h, int id, Stack<int> st, List<int> pix)
        {
            if (L[start] || lab[start] != 0) return;
            lab[start] = id; st.Push(start);
            while (st.Count > 0)
            {
                int i = st.Pop(); if (pix != null) pix.Add(i);
                int x = i % w, y = i / w;
                if (x > 0 && !L[i - 1] && lab[i - 1] == 0) { lab[i - 1] = id; st.Push(i - 1); }
                if (x < w - 1 && !L[i + 1] && lab[i + 1] == 0) { lab[i + 1] = id; st.Push(i + 1); }
                if (y > 0 && !L[i - w] && lab[i - w] == 0) { lab[i - w] = id; st.Push(i - w); }
                if (y < h - 1 && !L[i + w] && lab[i + w] == 0) { lab[i + w] = id; st.Push(i + w); }
            }
        }

        // Слой формы: белый с альфой маски + размытое свечение (уменьшение/увеличение ×4 дважды).
        static Shape MakeShape(byte[] am, int w, Rectangle box, float s)
        {
            var sh = new Shape { Box = box };
            int bw = Math.Max(1, box.Width), bh = Math.Max(1, box.Height);
            var data = new int[bw * bh];
            for (int y = 0; y < bh; y++) for (int x = 0; x < bw; x++) data[y * bw + x] = (am[(box.Y + y) * w + box.X + x] << 24) | 0xFFFFFF;
            var fb = new Bitmap(bw, bh, PixelFormat.Format32bppArgb);
            var d = fb.LockBits(new Rectangle(0, 0, bw, bh), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int y = 0; y < bh; y++) Marshal.Copy(data, y * bw, d.Scan0 + y * d.Stride, bw);
            fb.UnlockBits(d);
            sh.Fill = fb;
            int pad = (int)Math.Ceiling(10 * s), gw = bw + 2 * pad, gh = bh + 2 * pad;
            var gb = new Bitmap(gw, gh, PixelFormat.Format32bppPArgb);
            using (var g = Graphics.FromImage(gb)) { g.Clear(Color.Transparent); g.DrawImage(fb, new Rectangle(pad, pad, bw, bh), 0, 0, bw, bh, GraphicsUnit.Pixel); }
            for (int k = 0; k < 2; k++)
            {
                int sw = Math.Max(1, gw / 4), shh = Math.Max(1, gh / 4);
                using (var small = new Bitmap(sw, shh, PixelFormat.Format32bppPArgb))
                using (var ia = new ImageAttributes())
                {
                    ia.SetWrapMode(WrapMode.TileFlipXY);
                    using (var g = Graphics.FromImage(small)) { g.InterpolationMode = InterpolationMode.HighQualityBilinear; g.PixelOffsetMode = PixelOffsetMode.HighQuality; g.DrawImage(gb, new Rectangle(0, 0, sw, shh), 0, 0, gw, gh, GraphicsUnit.Pixel, ia); }
                    using (var g = Graphics.FromImage(gb)) { g.CompositingMode = CompositingMode.SourceCopy; g.InterpolationMode = InterpolationMode.HighQualityBicubic; g.PixelOffsetMode = PixelOffsetMode.HighQuality; g.DrawImage(small, new Rectangle(0, 0, gw, gh), 0, 0, sw, shh, GraphicsUnit.Pixel, ia); }
                }
            }
            sh.Glow = gb; sh.GlowOff = new Point(box.X - pad, box.Y - pad);
            return sh;
        }

        public static Hit HitTest(float s, Point p)
        {
            Ensure(s);
            if (OnCornBtn(s, p, Hit.CMin)) return Hit.CMin;       // v16: угловые кнопки — раньше маски и перетаскивания
            if (OnCornBtn(s, p, Hit.CClose)) return Hit.CClose;
            int cx = p.X + Off(X0, s), cy = p.Y + Off(Y0, s);
            if (map != null && cx >= 0 && cy >= 0 && cx < mw && cy < mh)
            {
                byte c = map[cy * mw + cx];
                if (c == 1) return Hit.CRec; if (c == 2) return Hit.CPause; if (c == 3) return Hit.CStop;
            }
            if (Bare)   // v17: без плашки — перетаскивание только за непрозрачные пиксели эмблемы (каска, буквы)
            {
                if (embA != null && cx >= 0 && cy >= 0 && cx < mw && cy < mh && embA[cy * mw + cx] > 90) return Hit.Body;
                return Hit.None;
            }
            using (var path = Theme.Round(PlateRect(s), 24 * s)) if (path.IsVisible(p)) return Hit.Body;
            return Hit.None;
        }
        static RectangleF PlateRect(float s) { return new RectangleF((Plate.X - X0) * s, (Plate.Y - Y0) * s, Plate.Width * s, Plate.Height * s); }

        static void DrawS(Graphics g, int code, bool glow, Color c, float a, int ox, int oy)
        {
            var sh = shapes[code]; if (sh == null || a <= 0.003f) return;
            if (glow) { Res.DrawTint(g, sh.Glow, sh.GlowOff.X - ox, sh.GlowOff.Y - oy, c, Math.Min(1f, a)); if (a > 1f) Res.DrawTint(g, sh.Glow, sh.GlowOff.X - ox, sh.GlowOff.Y - oy, c, Math.Min(1f, a - 1f)); }
            else Res.DrawTint(g, sh.Fill, sh.Box.X - ox, sh.Box.Y - oy, c, a);
        }
        static void Cavity(Graphics g, int code, Color c, float a, int ox, int oy, float s)
        {
            var sh = shapes[code]; if (sh == null || a <= 0.003f) return;
            var r = new RectangleF(sh.Box.X - ox, sh.Box.Y - oy, sh.Box.Width, sh.Box.Height);
            r.Inflate(1.5f * s, 1.5f * s);
            using (var p = new GraphicsPath())
            {
                p.AddEllipse(r);
                using (var br = new PathGradientBrush(p))
                {
                    br.CenterPoint = new PointF(sh.C.X - ox - r.Width * 0.08f, sh.C.Y - oy - r.Height * 0.12f);
                    br.CenterColor = Theme.A(Theme.Mix(c, Color.White, 0.45f), a);
                    br.SurroundColors = new[] { Theme.A(Theme.Mix(c, Color.Black, 0.25f), a) };
                    g.FillPath(br, p);
                }
            }
        }
        static void Glow(Graphics g, GraphicsPath p, Color c, float s, float k)
        {
            float[] w = { 7f, 4f, 2f }; float[] a = { 0.07f, 0.12f, 0.22f };
            for (int i = 0; i < 3; i++) using (var pen = new Pen(Theme.A(c, a[i] * k), w[i] * s) { LineJoin = LineJoin.Round }) g.DrawPath(pen, p);
        }

        // v17: мягкое нажатие — затемнение полости ПОД эмблемой: край маски уходит под буквы (ступенек и ободка не видно),
        // сверху — радиальный градиент (в центре темнее, к краю мягко сходит).
        static void Press(Graphics g, int code, float a, int ox, int oy, float s)
        {
            var sh = shapes[code]; if (sh == null || a <= 0.003f) return;
            Res.DrawTint(g, sh.Fill, sh.Box.X - ox, sh.Box.Y - oy, Color.Black, 0.30f * a);
            var r = new RectangleF(sh.Box.X - ox, sh.Box.Y - oy, sh.Box.Width, sh.Box.Height);
            r.Inflate(1.5f * s, 1.5f * s);
            using (var p = new GraphicsPath())
            {
                p.AddEllipse(r);
                using (var br = new PathGradientBrush(p))
                {
                    br.CenterPoint = new PointF(sh.C.X - ox + 0.5f, sh.C.Y - oy + 0.5f + r.Height * 0.06f);
                    br.CenterColor = Theme.A(Color.Black, 0.42f * a);
                    br.SurroundColors = new[] { Theme.A(Color.Black, 0.10f * a) };
                    g.FillPath(br, p);
                }
            }
        }
        public static void Render(Graphics g, float s, UiState st) { Render(g, s, st, null); }
        // v17: облик «Рамка» (SkinK → 1) — без плашки: содержимое рисуется в отдельный слой, под ним — мягкая тёмная тень
        // (размытая альфа слоя, ~35% у края), фон прозрачный. «Корпус» (SkinK = 0) — как в v16. Переход тумблером — плавный.
        static Bitmap lay, haloA, haloB; static int layW, layH; static float haloDiv;
        public static void Render(Graphics g, float s, UiState st, MiniWave mini)
        {
            Ensure(s);
            float sk = Math.Max(0f, Math.Min(1f, st.SkinK));
            Bare = sk >= 0.5f;
            if (sk <= 0.003f) { Content(g, s, st, mini, 1f, 0f); return; }
            int w = Off(W, s), h = Off(H, s);
            int div = Math.Max(2, (int)Math.Round(2.5f * s));
            if (lay == null || layW != w || layH != h || haloDiv != div)
            {
                if (lay != null) { lay.Dispose(); haloA.Dispose(); haloB.Dispose(); }
                layW = w; layH = h; haloDiv = div;
                lay = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
                haloA = new Bitmap(Math.Max(1, w / div), Math.Max(1, h / div), PixelFormat.Format32bppPArgb);
                haloB = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
            }
            using (var lg = Graphics.FromImage(lay)) Content(lg, s, st, mini, 1f - sk, sk);
            // тень: слой → уменьшение (усреднение) → увеличение бикубикой, дважды — мягкий ореол радиусом ~3 px (логических)
            using (var ia = new ImageAttributes())
            {
                ia.SetWrapMode(WrapMode.TileFlipXY);
                Bitmap src = lay;
                for (int k = 0; k < 2; k++)
                {
                    using (var ga = Graphics.FromImage(haloA))
                    {
                        ga.CompositingMode = CompositingMode.SourceCopy; ga.InterpolationMode = InterpolationMode.HighQualityBilinear; ga.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        ga.DrawImage(src, new Rectangle(0, 0, haloA.Width, haloA.Height), 0, 0, w, h, GraphicsUnit.Pixel, ia);
                    }
                    using (var gb = Graphics.FromImage(haloB))
                    {
                        gb.CompositingMode = CompositingMode.SourceCopy; gb.InterpolationMode = InterpolationMode.HighQualityBicubic; gb.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        if (k == 0) gb.DrawImage(haloA, new Rectangle(0, 0, w, h), 0, 0, haloA.Width, haloA.Height, GraphicsUnit.Pixel, ia);
                        else using (var ic = HaloAttr()) gb.DrawImage(haloA, new Rectangle(0, 0, w, h), 0, 0, haloA.Width, haloA.Height, GraphicsUnit.Pixel, ic);
                    }
                    src = haloB;
                }
            }
            g.CompositingMode = CompositingMode.SourceCopy; g.Clear(Color.Transparent); g.CompositingMode = CompositingMode.SourceOver;
            Res.Draw(g, haloB, 0, Math.Max(1, (int)Math.Round(0.6f * s)), 0.38f * sk);   // ~38% у края, мягко сходит наружу
            Res.Draw(g, lay, 0, 0, 1f);
        }
        // цвет тени — почти чёрный; альфа размытого слоя ×2,6 (с насыщением): у тонких линий каски, таймера и волны
        // тень такая же плотная, как у букв, а наружу — мягкий спад
        static ImageAttributes HaloAttr()
        {
            var cm = new ColorMatrix(new float[][] {
                new float[] { 0, 0, 0, 0, 0 }, new float[] { 0, 0, 0, 0, 0 }, new float[] { 0, 0, 0, 0, 0 },
                new float[] { 0, 0, 0, 2.6f, 0 }, new float[] { 8 / 255f, 10 / 255f, 12 / 255f, 0, 1 } });
            var ia = new ImageAttributes(); ia.SetColorMatrix(cm); ia.SetWrapMode(WrapMode.TileFlipXY); return ia;
        }
        // pk — видимость плашки (1 — «Корпус»), sk — облик «Рамка» (1 — без плашки)
        static void Content(Graphics g, float s, UiState st, MiniWave mini, float pk, float sk)
        {
            g.CompositingMode = CompositingMode.SourceCopy; g.Clear(Color.Transparent); g.CompositingMode = CompositingMode.SourceOver;
            g.SmoothingMode = SmoothingMode.AntiAlias; g.PixelOffsetMode = PixelOffsetMode.HighQuality; g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            int ox = Off(X0, s), oy = Off(Y0, s);
            float pulse = (float)(0.5 + 0.5 * Math.Sin(st.T * 2 * Math.PI * 0.8));
            float pw = 0.2f + 0.8f * st.Power;
            var pr = PlateRect(s);
            if (pk > 0.003f)   // v17: плашка и её обводка (рамка записи) — только в облике «Корпус»
            {
                // тень и плашка
                for (int i = 4; i >= 1; i--)
                {
                    var r = pr; r.Inflate(i * 1.5f * s, i * 1.5f * s); r.Offset(0, 2.5f * s);
                    using (var p = Theme.Round(r, (24 + i * 1.5f) * s)) using (var b = new SolidBrush(Color.FromArgb((int)(18 * pk), 0, 0, 0))) g.FillPath(b, p);
                }
                Color rc = st.Rec == 1 ? Theme.Mix(Theme.Teal, Theme.Red, st.RecK) : st.Rec == 2 ? Theme.Amber : Theme.Teal;
                float ra = (st.Rec == 1 ? 0.4f + (0.03f + 0.4f * pulse) * st.RecK : st.Rec == 2 ? 0.55f : 0.36f) * pw * pk;
                using (var p = Theme.Round(pr, 24 * s))
                {
                    using (var b = new LinearGradientBrush(new RectangleF(pr.X, pr.Y - 1, pr.Width, pr.Height + 2), Theme.A(Theme.Shade(Color.FromArgb(31, 35, 41)), pk), Theme.A(Theme.Shade(Color.FromArgb(15, 17, 21)), pk), LinearGradientMode.Vertical)) g.FillPath(b, p);
                    Glow(g, p, rc, s, ra);
                    using (var pen = new Pen(Theme.A(Theme.Mix(rc, Color.White, 0.1f), ra), 1.2f * s)) g.DrawPath(pen, p);
                }
                var hr = pr; hr.Inflate(-2f * s, -2f * s);
                using (var p = Theme.Round(hr, 22 * s)) using (var pen = new Pen(Color.FromArgb((int)(20 * pk), 255, 255, 255), 1f * s)) g.DrawPath(pen, p);
            }
            // v17: без плашки полости «d»/«b» залиты тёмным — это кнопки (ловят щелчки), знаки на них читаются на любом фоне
            if (sk > 0.003f)
            {
                DrawS(g, 1, false, Color.FromArgb(22, 25, 30), sk, ox, oy);
                DrawS(g, 3, false, Color.FromArgb(22, 25, 30), sk, ox, oy);
            }

            bool enR = st.IsEn(Hit.CRec), enP = st.IsEn(Hit.CPause), enS = st.IsEn(Hit.CStop);
            bool hotR = enR && st.Hot == Hit.CRec, hotP = enP && st.Hot == Hit.CPause, hotS = enS && st.Hot == Hit.CStop;
            bool dnR = hotR && st.Down == Hit.CRec, dnP = hotP && st.Down == Hit.CPause, dnS = hotS && st.Down == Hit.CStop;
            // под эмблемой: светящиеся полости (буквы ложатся сверху — их края не меняются)
            if (st.Rec == 1) Cavity(g, 1, Theme.Red, (0.55f + 0.45f * pulse) * Math.Max(0.15f, st.RecK), ox, oy, s);
            else if (st.Rec == 2) Cavity(g, 1, Theme.Red, 0.22f, ox, oy, s);
            else if (hotR && !dnR) Cavity(g, 1, Theme.Red, 0.32f, ox, oy, s);
            if (hotS && !dnS) Cavity(g, 3, Color.FromArgb(150, 170, 185), 0.28f, ox, oy, s);
            // v17: нажатие — мягкое затемнение под эмблемой (раньше — маска поверх букв: ступеньки и серый ободок)
            if (dnR) Press(g, 1, 1f, ox, oy, s);
            if (dnS) Press(g, 3, 1f, ox, oy, s);
            // v14: знаки «Запись»/«Стоп» видны всегда; под эмблемой — буквы закрывают всё, что вне полостей
            Marks(g, st, s, ox, oy, pulse, hotR, dnR, hotS, dnS);
            // эмблема 1:1; во время записи строка «BIM DESIGN» уступает место таймеру
            var band = new RectangleF((TextBand.X - X0) * s, (TextBand.Y - Y0) * s, TextBand.Width * s, TextBand.Height * s);
            // v12: в покое при включённом питании и контроле входа «BIM DESIGN» уступает место мини-осциллографу
            bool idleWave = mini != null && st.Rec == 0 && st.Power > 0.5f && st.MicLive;
            // v13: надпись стирается по пикселям (не прямоугольником) — свечение низа букв не срезается
            Res.Draw(g, (st.Rec != 0 || idleWave) ? EmblemNoText(s) : Res.Get("emblem", s), -ox, -oy, 1f);
            // над эмблемой: свечение, нажатие, подсказки
            if (st.Rec == 1) DrawS(g, 1, true, Theme.Red, (0.35f + 0.45f * pulse) * st.RecK, ox, oy);
            if (st.Rec == 2) { DrawS(g, 2, true, Theme.Amber, 0.55f + 0.35f * pulse, ox, oy); DrawS(g, 2, false, Theme.Amber, 0.62f + 0.2f * pulse, ox, oy); }
            if (hotP) { if (dnP) DrawS(g, 2, false, Color.Black, 0.42f, ox, oy); else { DrawS(g, 2, true, st.Rec == 2 ? Theme.Amber : Theme.TealHi, 1.3f, ox, oy); DrawS(g, 2, false, st.Rec == 2 ? Theme.Amber : Theme.TealHi, 0.22f, ox, oy); } }
            if (hotR && !dnR) DrawS(g, 1, true, Theme.Red, 0.7f, ox, oy);
            if (hotS && !dnS) DrawS(g, 3, true, Theme.TealHi, 0.9f, ox, oy);
            if (idleWave) MiniScope(g, s, mini, MYIdle, Theme.Teal, false, st.Power);   // v13
            // таймер и мини-осциллограф вместо «BIM DESIGN»
            if (st.Rec != 0)
            {
                float cx = (CX - X0) * s, cy = (181f - Y0) * s;   // v13: таймер на месте v11
                Color tc = st.Rec == 1 ? Theme.Mix(Theme.Teal, Theme.Red, st.RecK) : st.Rec == 2 ? Theme.Amber : Theme.Teal;
                if (st.Rec == 3)
                {
                    string t = st.Enh ? (st.Prog >= 0 ? "ОБРАБОТКА " + (int)(st.Prog * 100) + "%" : "ОБРАБОТКА…") : "СОХРАНЯЮ…";
                    using (var sf = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    using (var b = new SolidBrush(Theme.A(Theme.Teal, 0.95f)))
                        g.DrawString(t, Theme.Px(Theme.UiSemi, 8.6f * s, FontStyle.Regular), b, new RectangleF(cx - 90 * s, cy - 8 * s, 180 * s, 16 * s), sf);
                }
                else
                {
                    float ta = st.Rec == 2 ? 0.6f + 0.4f * pulse : 1f;
                    using (var path = new GraphicsPath())
                    using (var sf = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    {
                        path.AddString(st.Timer, Theme.DigitFam, (int)FontStyle.Regular, 16f * s, new RectangleF(cx - 90 * s, cy - 14 * s, 180 * s, 28 * s), sf);
                        Glow(g, path, tc, s, 0.8f * ta);
                        using (var b = new SolidBrush(Theme.A(Theme.Mix(tc, Color.White, 0.15f), ta))) g.FillPath(b, path);
                    }
                }
                if ((st.Rec == 1 || st.Rec == 2) && mini != null)
                    MiniScope(g, s, mini, MYRec, st.Rec == 2 ? Theme.Amber : Theme.Teal, st.Rec == 2, 1f);
            }
            // v16: угловые кнопки — поверх всего, только при наведении на угол
            CornerBtn(g, s, st, Hit.CMin, st.CornL);
            CornerBtn(g, s, st, Hit.CClose, st.CornR);
        }
        // v16: круглая тёмная кнопка как «–»/«×» полного режима; наведение — «Свернуть» янтарём (пауза), «Закрыть» красным (запись).
        static void CornerBtn(Graphics g, float s, UiState st, Hit h, float a)
        {
            if (a <= 0.003f) return;
            a = Math.Min(1f, a); a = a * a * (3 - 2 * a);
            var c = CornC(s, h); float r = Lay.BR * s;
            bool en = st.IsEn(h), hot = en && st.Hot == h, down = hot && st.Down == h;
            Color hc = h == Hit.CClose ? Theme.Red : Theme.Amber;
            var rr = new RectangleF(c.X - r, c.Y - r, 2 * r, 2 * r);
            var sr = rr; sr.Inflate(1.2f * s, 1.2f * s); sr.Offset(0, 0.8f * s);
            using (var b = new SolidBrush(Theme.A(Color.Black, 0.40f * a))) g.FillEllipse(b, sr);
            using (var p = new GraphicsPath())
            {
                p.AddEllipse(rr);
                if (hot)
                {
                    if (!down) Glow(g, p, hc, s, 1.0f * a);
                    Color c1 = down ? Theme.Mix(hc, Color.Black, 0.30f) : Theme.Mix(hc, Color.White, 0.12f);
                    Color c2 = down ? Theme.Mix(hc, Color.Black, 0.45f) : Theme.Mix(hc, Color.Black, 0.12f);
                    using (var b = new LinearGradientBrush(new RectangleF(rr.X, rr.Y - 1, rr.Width, rr.Height + 2), Theme.A(c1, a), Theme.A(c2, a), LinearGradientMode.Vertical)) g.FillPath(b, p);
                    using (var pen = new Pen(Theme.A(Theme.Mix(hc, Color.White, 0.35f), 0.85f * a), 1f * s)) g.DrawPath(pen, p);
                }
                else
                {
                    using (var b = new LinearGradientBrush(new RectangleF(rr.X, rr.Y - 1, rr.Width, rr.Height + 2), Theme.A(Theme.Shade(Color.FromArgb(46, 52, 60)), a), Theme.A(Theme.Shade(Color.FromArgb(24, 27, 32)), a), LinearGradientMode.Vertical)) g.FillPath(b, p);
                    using (var pen = new Pen(Theme.A(Color.White, 0.16f * a), 1f * s)) g.DrawPath(pen, p);
                }
            }
            Color ic = hot ? (h == Hit.CClose ? Color.White : Color.FromArgb(40, 26, 6)) : Color.FromArgb(170, 178, 188);
            float ia = en ? a : 0.4f * a;
            using (var pen = new Pen(Theme.A(ic, ia), 1.6f * s) { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                float k = 3.6f * s;
                if (h == Hit.CMin) g.DrawLine(pen, c.X - k, c.Y + 0.5f * s, c.X + k, c.Y + 0.5f * s);
                else { g.DrawLine(pen, c.X - k, c.Y - k, c.X + k, c.Y + k); g.DrawLine(pen, c.X - k, c.Y + k, c.X + k, c.Y - k); }
            }
        }
        // v13: мини-осциллограф — дорожка полосы уровня v11 (80×3, приглушённая) и бегущая волна по её оси.
        static void MiniScope(Graphics g, float s, MiniWave mini, float yc, Color c, bool dim, float power)
        {
            float x = (CX - MW / 2 - X0) * s, y = (yc - MH / 2 - Y0) * s;
            var r = new RectangleF(x, y, MW * s, MH * s);
            var lr = new RectangleF(x, (yc - 1.5f - Y0) * s, MW * s, 3 * s);
            using (var p = Theme.Round(lr, 1.5f * s)) using (var b = new SolidBrush(Theme.A(c, (dim ? 0.10f : 0.12f) * power))) g.FillPath(b, p);
            if (dim) mini.Draw(g, r, s, Theme.Mix(Theme.Amber, Color.Black, 0.18f), 0.62f);
            else mini.Draw(g, r, s, c, power);
        }
        // v14: постоянные знаки. Точка «Запись» — Theme.Red как на большой кнопке, квадрат «Стоп» — светло-серый с мятным оттенком.
        static readonly Color StopC = Theme.Mix(Color.FromArgb(225, 232, 240), Theme.TealHi, 0.22f);
        static void Marks(Graphics g, UiState st, float s, int ox, int oy, float pulse, bool hotR, bool dnR, bool hotS, bool dnS)
        {
            float pk = 0.7f + 0.3f * Math.Max(0f, Math.Min(1f, st.Power));   // питание выключено — ещё тише, но видно
            float rb, rg; Color rcol = Theme.Red;
            if (dnR) { rb = 0.42f; rg = 0.18f; }
            else if (hotR) { rb = 1f; rg = 0.9f; rcol = Theme.Mix(Theme.Red, Color.White, 0.12f); }
            else if (st.Rec == 1) { float k = Math.Max(0f, Math.Min(1f, st.RecK)); rb = 0.55f + (0.3f + 0.15f * pulse) * k; rg = 0.35f + (0.3f + 0.4f * pulse) * k; }
            else if (st.Rec == 2) { rb = 0.30f; rg = 0.10f; }
            else { rb = 0.55f; rg = 0.35f; }
            if (!hotR && st.Rec != 1) { rb *= pk; rg *= pk; }
            Mark(g, 1, ox, oy, s, true, Theme.Mix(rcol, Color.Black, 1f - rb), rg, dnR ? 0.92f : 1f);
            float sb, sg; Color scol = StopC;
            if (dnS) { sb = 0.40f; sg = 0.08f; }
            else if (hotS) { sb = 1f; sg = 0.9f; scol = Color.FromArgb(240, 246, 248); }
            else { sb = 0.55f * pk; sg = 0.22f * pk; }
            Mark(g, 3, ox, oy, s, false, Theme.Mix(scol, Color.Black, 1f - sb), sg, dnS ? 0.92f : 1f);
        }
        static void Mark(Graphics g, int code, int ox, int oy, float s, bool dot, Color col, float glow, float k)
        {
            var sh = shapes[code]; if (sh == null) return;
            int dil = Math.Max(1, (int)Math.Round(2 * s));
            float cav = Math.Min(sh.Box.Width, sh.Box.Height) - 2 * dil;   // размер самой полости (без расширения маски)
            float d = cav * (dot ? 0.43f : 0.38f) * k;   // v17: k = 0,92 при нажатии — знак «вдавливается»
            var c = new PointF(sh.C.X - ox + 0.5f, sh.C.Y - oy + 0.5f);
            var r = new RectangleF(c.X - d / 2, c.Y - d / 2, d, d);
            using (var p = dot ? new GraphicsPath() : Theme.Round(r, 2.2f * s * k))
            {
                if (dot) p.AddEllipse(r);
                if (glow > 0.01f) Glow(g, p, col, s, glow);
                using (var b = new SolidBrush(Theme.A(col, 0.97f))) g.FillPath(b, p);
            }
        }
        // подсказки: красная точка в «d» (Запись), квадрат в «b» (Стоп)
        static void Hint(Graphics g, int code, int ox, int oy, float s, bool dot)
        {
            var sh = shapes[code]; if (sh == null) return;
            float d = Math.Min(sh.Box.Width, sh.Box.Height) * (dot ? 0.46f : 0.42f);
            var c = new PointF(sh.C.X - ox + 0.5f, sh.C.Y - oy + 0.5f);
            var r = new RectangleF(c.X - d / 2, c.Y - d / 2, d, d);
            Color col = dot ? Theme.Mix(Theme.Red, Color.White, 0.12f) : Color.FromArgb(232, 238, 244);
            using (var p = dot ? new GraphicsPath() : Theme.Round(r, 2.2f * s))
            {
                if (dot) p.AddEllipse(r);
                Glow(g, p, col, s, 0.9f);
                using (var b = new SolidBrush(Theme.A(col, 0.95f))) g.FillPath(b, p);
            }
        }

        // для --snapshot-compact: маска кнопок поверх кадра и проверка хит-теста в центрах форм
        public static void DrawMask(Graphics g, float s)
        {
            Ensure(s); if (map == null) return;
            int ox = Off(X0, s), oy = Off(Y0, s);
            int w = Off(W, s), h = Off(H, s);
            var b = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            var data = new int[w * h];
            int[] col = { 0, unchecked((int)0x90FF3030), unchecked((int)0x90FFB020), unchecked((int)0x9030A0FF) };
            for (int y = 0; y < h; y++) for (int x = 0; x < w; x++)
                {
                    int cx = x + ox, cy = y + oy; if (cx >= mw || cy >= mh) continue;
                    data[y * w + x] = col[map[cy * mw + cx]];
                }
            var d = b.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int y = 0; y < h; y++) Marshal.Copy(data, y * w, d.Scan0 + y * d.Stride, w);
            b.UnlockBits(d);
            g.DrawImage(b, new Rectangle(0, 0, w, h), 0, 0, w, h, GraphicsUnit.Pixel); b.Dispose();
        }
        public static string Report(float s)
        {
            Ensure(s); if (map == null) return "маски нет: " + Info;
            int ox = Off(X0, s), oy = Off(Y0, s);
            var sb = new StringBuilder();
            Func<PointF, string> at = c => HitTest(s, new Point((int)Math.Round(c.X) - ox, (int)Math.Round(c.Y) - oy)).ToString();
            sb.Append("центр полости d → " + at(shapes[1].C));
            sb.Append("; центр полости b → " + at(shapes[3].C));
            sb.Append("; палочка d → " + at(stemC[0]) + "; палочка b → " + at(stemC[1]));
            sb.Append("; каска → " + HitTest(s, new Point(Off(CX - X0, s), Off(50 - Y0, s))));
            sb.Append("; угол плашки → " + HitTest(s, new Point(Off(Plate.X - X0 + 30, s), Off(Plate.Bottom - Y0 - 10, s))));
            sb.Append("; вне плашки → " + HitTest(s, new Point(1, 1)));
            var pm = CornC(s, Hit.CMin); var pc = CornC(s, Hit.CClose);
            sb.Append("; кнопка «свернуть» → " + HitTest(s, Point.Round(pm)) + "; кнопка «закрыть» → " + HitTest(s, Point.Round(pc)));
            sb.Append("; угол слева (зона) → " + CornerAt(s, new Point(Off(Plate.X - X0 + 8, s), Off(Plate.Y - Y0 + 40, s))) + "/" + HitTest(s, new Point(Off(Plate.X - X0 + 8, s), Off(Plate.Y - Y0 + 40, s))));
            sb.Append("; угол справа (зона) → " + CornerAt(s, new Point(Off(Plate.Right - X0 - 8, s), Off(Plate.Y - Y0 + 40, s))));
            sb.Append("; каска (зона) → " + CornerAt(s, new Point(Off(CX - X0, s), Off(50 - Y0, s))));
            sb.Append("; вне скругления → " + CornerAt(s, new Point(Off(Plate.X - X0 + 1, s), Off(Plate.Y - Y0 + 1, s))));
            if (Bare)   // v17: зоны у каски и прозрачное место рядом
            {
                Func<float, float, string> zb = (x, y) => { var q = new Point(Off(x - X0, s), Off(y - Y0, s)); return CornerAt(s, q) + "/" + HitTest(s, q); };
                sb.Append("; зона слева от каски → " + zb(50, 60) + "; зона справа от каски → " + zb(170, 60) + "; между буквами внизу → " + zb(CX, 200) + "; буква d (обвод) → " + zb(52, 140));
            }
            return sb.ToString();
        }
    }

    // ---------- Layered-окно: кадр в DIB-секции, UpdateLayeredWindow(ULW_ALPHA) ----------
    class LayeredForm : Form
    {
        protected Bitmap Frame; IntPtr dib = IntPtr.Zero, memDC = IntPtr.Zero, oldBmp, bits;
        protected int FW, FH;
        public byte Alpha = 255;
        public LayeredForm()
        {
            FormBorderStyle = FormBorderStyle.None; AutoScaleMode = AutoScaleMode.None;
            StartPosition = FormStartPosition.Manual; MaximizeBox = false; MinimizeBox = false; ControlBox = false;
        }
        protected override CreateParams CreateParams
        {
            get { var cp = base.CreateParams; cp.ExStyle |= 0x80000; return cp; }   // WS_EX_LAYERED
        }
        protected void EnsureFrame(int w, int h)
        {
            if (Frame != null && w == FW && h == FH) return;
            FreeFrame();
            FW = w; FH = h;
            var bi = new Native.BITMAPINFOHEADER { biSize = 40, biWidth = w, biHeight = -h, biPlanes = 1, biBitCount = 32, biCompression = 0 };
            memDC = Native.CreateCompatibleDC(IntPtr.Zero);
            dib = Native.CreateDIBSection(memDC, ref bi, 0, out bits, IntPtr.Zero, 0);
            oldBmp = Native.SelectObject(memDC, dib);
            Frame = new Bitmap(w, h, w * 4, PixelFormat.Format32bppPArgb, bits);
            Size = new Size(w, h);
        }
        void FreeFrame()
        {
            if (Frame != null) { Frame.Dispose(); Frame = null; }
            if (memDC != IntPtr.Zero) { Native.SelectObject(memDC, oldBmp); Native.DeleteObject(dib); Native.DeleteDC(memDC); memDC = IntPtr.Zero; }
        }
        public bool Present()
        {
            if (!IsHandleCreated || Frame == null) return false;
            Native.GdiFlush();
            IntPtr scr = Native.GetDC(IntPtr.Zero);
            var pos = new Native.POINT(Left, Top); var sz = new Native.SIZE(FW, FH); var src = new Native.POINT(0, 0);
            var bf = new Native.BLENDFUNCTION { BlendOp = 0, BlendFlags = 0, SourceConstantAlpha = Alpha, AlphaFormat = 1 };
            bool ok = Native.UpdateLayeredWindow(Handle, scr, ref pos, ref sz, memDC, ref src, 0, ref bf, 2);
            Native.ReleaseDC(IntPtr.Zero, scr);
            return ok;
        }
        public Bitmap CopyFrame() { return Frame == null ? null : Frame.Clone(new Rectangle(0, 0, FW, FH), PixelFormat.Format32bppArgb); }
        protected override void Dispose(bool disposing) { FreeFrame(); base.Dispose(disposing); }
        protected override void OnPaint(PaintEventArgs e) { }
        protected override void OnPaintBackground(PaintEventArgs e) { }
    }

    // ---------- Тёмные меню ----------
    class DarkColors : ProfessionalColorTable
    {
        static Color bg { get { return Theme.Shade(Color.FromArgb(22, 25, 30)); } }  static Color sel { get { return Theme.Mix(Color.FromArgb(22, 25, 30), Theme.Teal, 0.22f); } }  static Color line = Color.FromArgb(48, 56, 62);
        public override Color ToolStripDropDownBackground { get { return bg; } }
        public override Color ImageMarginGradientBegin { get { return bg; } }
        public override Color ImageMarginGradientMiddle { get { return bg; } }
        public override Color ImageMarginGradientEnd { get { return bg; } }
        public override Color MenuBorder { get { return line; } }
        public override Color MenuItemBorder { get { return Theme.Mix(Color.FromArgb(22, 25, 30), Theme.Teal, 0.5f); } }
        public override Color MenuItemSelected { get { return sel; } }
        public override Color MenuItemSelectedGradientBegin { get { return sel; } }
        public override Color MenuItemSelectedGradientEnd { get { return sel; } }
        public override Color SeparatorDark { get { return line; } }
        public override Color SeparatorLight { get { return bg; } }
        public override Color CheckBackground { get { return Color.Transparent; } }
        public override Color CheckSelectedBackground { get { return Color.Transparent; } }
        public override Color CheckPressedBackground { get { return Color.Transparent; } }
    }
    class DarkRenderer : ToolStripProfessionalRenderer
    {
        public DarkRenderer() : base(new DarkColors()) { RoundedEdges = false; }
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Enabled ? (e.Item.Selected ? Color.White : Theme.Ink) : Theme.Dim;
            base.OnRenderItemText(e);
        }
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = e.ImageRectangle; float d = Math.Min(r.Width, r.Height) * 0.42f;
            using (var b = new SolidBrush(Theme.Teal)) g.FillEllipse(b, r.X + r.Width / 2f - d / 2, r.Y + r.Height / 2f - d / 2, d, d);
        }
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e) { e.ArrowColor = Theme.Mute; base.OnRenderArrow(e); }
    }

    // ---------- Звук щелчка тумблера (синтез, без файлов) ----------
    static class ClickSound
    {
        static byte[] on, off;
        static byte[] Make(bool up)
        {
            int sr = 44100, n = sr * 40 / 1000;
            var pcm = new short[n]; var rnd = new Random(up ? 3 : 5);
            double f1 = up ? 2600 : 2100, f2 = up ? 210 : 170;
            for (int i = 0; i < n; i++)
            {
                double t = (double)i / sr;
                double v = (rnd.NextDouble() * 2 - 1) * Math.Exp(-t / 0.0012) * 0.55
                         + Math.Sin(2 * Math.PI * f1 * t) * Math.Exp(-t / 0.0035) * 0.45
                         + Math.Sin(2 * Math.PI * f2 * t) * Math.Exp(-t / 0.008) * 0.35;
                double t2 = t - 0.009;   // второй, тише — «отскок»
                if (t2 > 0) v += Math.Sin(2 * Math.PI * f1 * 1.15 * t2) * Math.Exp(-t2 / 0.002) * 0.18;
                pcm[i] = (short)(Math.Max(-1, Math.Min(1, v * 0.5)) * 32000);
            }
            var ms = new MemoryStream(); var w = new BinaryWriter(ms);
            w.Write(Encoding.ASCII.GetBytes("RIFF")); w.Write(36 + n * 2); w.Write(Encoding.ASCII.GetBytes("WAVEfmt "));
            w.Write(16); w.Write((short)1); w.Write((short)1); w.Write(sr); w.Write(sr * 2); w.Write((short)2); w.Write((short)16);
            w.Write(Encoding.ASCII.GetBytes("data")); w.Write(n * 2); foreach (var x in pcm) w.Write(x); w.Flush();
            return ms.ToArray();
        }
        public static void Play(bool up)
        {
            try
            {
                if (on == null) { on = Make(true); off = Make(false); }
                var data = up ? on : off;
                var th = new Thread(() => { try { using (var ms = new MemoryStream(data)) using (var sp = new System.Media.SoundPlayer(ms)) sp.PlaySync(); } catch { } });
                th.IsBackground = true; th.Start();
            }
            catch { }
        }
    }

    // ---------- Плашка во время записи (layered, исключена из захвата) ----------
    class PlaqueForm : LayeredForm
    {
        public bool AffinityOk; public UiState Q = new UiState();
        public event Action PauseClick, StopClick;
        float s;
        public PlaqueForm(float scale)
        {
            s = scale; ShowInTaskbar = false; TopMost = true;
            EnsureFrame((int)Math.Round(Lay.QW * s), (int)Math.Round(Lay.QH * s));
        }
        protected override bool ShowWithoutActivation { get { return true; } }
        protected override CreateParams CreateParams
        {
            get { var cp = base.CreateParams; cp.ExStyle |= 0x80 | 0x08000000 | 0x8; return cp; }   // TOOLWINDOW | NOACTIVATE | TOPMOST
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Present();
            AffinityOk = AppLog.Affinity(Handle, Native.WDA_EXCLUDEFROMCAPTURE, "плашка (создание)");   // v13: с проверкой и журналом
        }
        public void Render(UiState main, float[] buf, int avail)
        {
            Q.Rec = main.Rec; Q.Timer = main.Timer; Q.T = main.T;
            using (var g = Graphics.FromImage(Frame)) DeviceView.RenderPlaque(g, s, Q, buf, avail);
        }
        PointF Lp(Point p) { return new PointF(p.X / s - Lay.QP, p.Y / s - Lay.QP); }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var h = DeviceView.QHit(Lp(e.Location)); if (h == Hit.Body) h = Hit.None;
            Q.Hot = h; Cursor = h != Hit.None ? Cursors.Hand : Cursors.Default; base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e) { Q.Hot = Hit.None; base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var h = DeviceView.QHit(Lp(e.Location));
            if (h == Hit.Body) { Native.DragWindow(this); return; }
            Q.Down = h; Capture = true;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            var d = Q.Down; Q.Down = Hit.None; Capture = false;
            if (d == Hit.None || DeviceView.QHit(Lp(e.Location)) != d) return;
            if (d == Hit.QPause && PauseClick != null) PauseClick();
            if (d == Hit.QStop && StopClick != null) StopClick();
        }
    }

    // ---------- v11: выбор области записи (как в Bandicam) — затемнение на весь виртуальный экран ----------
    // Процесс DPI-aware: координаты физические, как у gdigrab. Внутри выделения альфа 1 (окно ловит мышь), снаружи — затемнение.
    class RegionPicker : LayeredForm
    {
        public event Action<Rectangle?> Done;
        public Rectangle Sel;            // выделение в координатах экрана (чётные размеры)
        Rectangle virt, hintArea; Point a; bool drag, finished; float s;
        public const int MinSize = 16;
        public RegionPicker() : this(SystemInformation.VirtualScreen) { }
        public RegionPicker(Rectangle v)
        {
            virt = v; s = Theme.K; ShowInTaskbar = false; TopMost = true; KeyPreview = true; Cursor = Cursors.Cross;
            Text = "XYZ Запись — выбор области";
            var pb = Screen.PrimaryScreen.Bounds; pb.Intersect(v); if (pb.IsEmpty) pb = v;
            hintArea = new Rectangle(pb.X - v.X, pb.Y - v.Y, pb.Width, pb.Height);
            EnsureFrame(v.Width, v.Height); Location = v.Location;
        }
        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ExStyle |= 0x80 | 0x8; return cp; } }   // TOOLWINDOW | TOPMOST
        public void ShowPicker()
        {
            var h = Handle; Bounds = virt;
            Render(); Present(); Show(); Activate(); Focus();
        }
        public void Render()
        {
            var loc = Sel.IsEmpty ? Rectangle.Empty : new Rectangle(Sel.X - virt.X, Sel.Y - virt.Y, Sel.Width, Sel.Height);
            using (var g = Graphics.FromImage(Frame)) Draw(g, loc, FW, FH, hintArea, s);
        }
        public static Rectangle EvenRect(Rectangle r) { return new Rectangle(r.X, r.Y, Math.Max(0, r.Width) & ~1, Math.Max(0, r.Height) & ~1); }

        public static void Draw(Graphics g, Rectangle sel, int w, int h, Rectangle hintArea, float s)
        {
            bool has = sel.Width > 0 && sel.Height > 0;
            g.CompositingMode = CompositingMode.SourceCopy;
            using (var dim = new SolidBrush(Color.FromArgb(125, 6, 9, 12))) g.FillRectangle(dim, 0, 0, w, h);
            if (has) using (var clr = new SolidBrush(Color.FromArgb(1, 0, 0, 0))) g.FillRectangle(clr, sel);
            g.CompositingMode = CompositingMode.SourceOver;
            g.SmoothingMode = SmoothingMode.None; g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (has)
            {
                // рамка 2 px снаружи области (в кадр не попадает) + уголки
                using (var b = new SolidBrush(Theme.Teal))
                {
                    g.FillRectangle(b, sel.X - 2, sel.Y - 2, sel.Width + 4, 2); g.FillRectangle(b, sel.X - 2, sel.Bottom, sel.Width + 4, 2);
                    g.FillRectangle(b, sel.X - 2, sel.Y, 2, sel.Height); g.FillRectangle(b, sel.Right, sel.Y, 2, sel.Height);
                }
                int cl = (int)Math.Round(Math.Min(18 * s, Math.Min(sel.Width, sel.Height) / 3f)), ct = Math.Max(2, (int)Math.Round(3 * s));
                int e = ct - 2, Lx = sel.X - 2 - e, Rx = sel.Right + 2 + e, Ty = sel.Y - 2 - e, By = sel.Bottom + 2 + e;
                using (var b = new SolidBrush(Theme.TealHi))
                {
                    g.FillRectangle(b, Lx, Ty, cl, ct); g.FillRectangle(b, Lx, Ty, ct, cl);
                    g.FillRectangle(b, Rx - cl, Ty, cl, ct); g.FillRectangle(b, Rx - ct, Ty, ct, cl);
                    g.FillRectangle(b, Lx, By - ct, cl, ct); g.FillRectangle(b, Lx, By - cl, ct, cl);
                    g.FillRectangle(b, Rx - cl, By - ct, cl, ct); g.FillRectangle(b, Rx - ct, By - cl, ct, cl);
                }
                g.SmoothingMode = SmoothingMode.AntiAlias;
                string t = sel.Width + " × " + sel.Height + " px";
                var f = Theme.Px(Theme.UiSemi, 12.5f * s, FontStyle.Regular);
                var sz = g.MeasureString(t, f, 1000, StringFormat.GenericTypographic);
                float pw = sz.Width + 20 * s, ph = sz.Height + 10 * s;
                float px = sel.X, py = sel.Bottom + 10 * s;
                if (py + ph > h - 4 * s) py = sel.Y - ph - 10 * s;
                if (py < 4 * s) py = sel.Y + 10 * s;
                px = Math.Max(4 * s, Math.Min(px, w - pw - 4 * s));
                Pill(g, new RectangleF(px, py, pw, ph), t, f, Theme.Ink, s);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var hf = Theme.Px(Theme.UiFam, 12f * s, FontStyle.Regular);
            string hint = has ? "Отпустите — запись будет с этой области  ·  Enter — готово  ·  Esc — отмена" : "Тяните рамку мышью  ·  Esc — отмена";
            var hs = g.MeasureString(hint, hf, 4000, StringFormat.GenericTypographic);
            float hw = hs.Width + 28 * s, hh = hs.Height + 14 * s;
            Pill(g, new RectangleF(hintArea.X + (hintArea.Width - hw) / 2, hintArea.Y + 28 * s, hw, hh), hint, hf, Theme.Ink, s);
        }
        static void Pill(Graphics g, RectangleF r, string t, Font f, Color c, float s)
        {
            using (var p = Theme.Round(r, r.Height / 2))
            {
                using (var b = new SolidBrush(Color.FromArgb(235, 16, 19, 23))) g.FillPath(b, p);
                using (var pen = new Pen(Theme.A(Theme.Teal, 0.7f), 1.2f * s)) g.DrawPath(pen, p);
            }
            using (var sf = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var b = new SolidBrush(c)) g.DrawString(t, f, b, r, sf);
        }

        Point Scr(Point p) { return new Point(p.X + virt.X, p.Y + virt.Y); }
        static Rectangle Norm(Point p, Point q) { return Rectangle.FromLTRB(Math.Min(p.X, q.X), Math.Min(p.Y, q.Y), Math.Max(p.X, q.X), Math.Max(p.Y, q.Y)); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { Finish(null); return; }
            if (e.Button != MouseButtons.Left) return;
            a = Scr(e.Location); drag = true; Capture = true; Sel = Rectangle.Empty; Render(); Present();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!drag) return;
            var r = Norm(a, Scr(e.Location)); r.Intersect(virt);
            Sel = EvenRect(r); Render(); Present();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!drag) return;
            drag = false; Capture = false;
            if (Sel.Width >= MinSize && Sel.Height >= MinSize) Finish(Sel);
            else { Sel = Rectangle.Empty; Render(); Present(); }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Finish(null);
            else if (e.KeyCode == Keys.Enter && Sel.Width >= MinSize && Sel.Height >= MinSize) Finish(Sel);
            base.OnKeyDown(e);
        }
        void Finish(Rectangle? r)
        {
            if (finished) return;
            finished = true; Capture = false; Hide();
            var d = Done; if (d != null) d(r);
            BeginInvoke(new Action(() => { Close(); Dispose(); }));
        }
    }

    // ---------- v11: рамка вокруг области во время записи — 2 px СНАРУЖИ области, исключена из захвата, мышь пропускает ----------
    class RegionFrame : LayeredForm
    {
        public const int T = 2;
        public bool AffinityOk;
        public RegionFrame() { ShowInTaskbar = false; TopMost = true; Text = "XYZ Запись — рамка области"; }
        protected override bool ShowWithoutActivation { get { return true; } }
        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ExStyle |= 0x80 | 0x08000000 | 0x8 | 0x20; return cp; } }   // TOOLWINDOW | NOACTIVATE | TOPMOST | TRANSPARENT
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); AffinityOk = AppLog.Affinity(Handle, Native.WDA_EXCLUDEFROMCAPTURE, "рамка области (создание)"); }
        public void Place(Rectangle area)
        {
            var b = Rectangle.Inflate(area, T, T);
            EnsureFrame(b.Width, b.Height); Location = b.Location;
            var h = Handle;
        }
        public void SetColor(bool paused)
        {
            DrawRing(Frame, FW, FH, T, Color.FromArgb(235, paused ? Theme.Amber : Theme.Red));
            if (IsHandleCreated) Present();
        }
        public static void DrawRing(Bitmap bmp, int w, int h, int t, Color c)
        {
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.Clear(Color.Transparent);
                using (var b = new SolidBrush(c))
                {
                    g.FillRectangle(b, 0, 0, w, t); g.FillRectangle(b, 0, h - t, w, t);
                    g.FillRectangle(b, 0, t, t, h - 2 * t); g.FillRectangle(b, w - t, t, t, h - 2 * t);
                }
            }
        }
    }

    // ---------- v11: «О программе» — маленькое окно в стиле прибора ----------

    // ---------- v19: окошко «Сохранено» и простая обрезка ----------
    static class Ui19
    {
        public static Control Btn(string t, bool main) { return new XyzButton(t, main); }
        static GraphicsPath Rect(RectangleF r) { var p = new GraphicsPath(); p.AddRectangle(r); return p; }
        static Dictionary<Bitmap, Bitmap> trims = new Dictionary<Bitmap, Bitmap>();
        // обрезать прозрачные поля спрайта (для маленькой эмблемы в шапке)
        public static Bitmap Trim(Bitmap b)
        {
            if (b == null) return null;
            Bitmap t; if (trims.TryGetValue(b, out t)) return t;
            int x0 = b.Width, y0 = b.Height, x1 = -1, y1 = -1;
            for (int y = 0; y < b.Height; y++) for (int x = 0; x < b.Width; x++) if (b.GetPixel(x, y).A > 24) { if (x < x0) x0 = x; if (y < y0) y0 = y; if (x > x1) x1 = x; if (y > y1) y1 = y; }
            t = x1 < 0 ? b : b.Clone(new Rectangle(x0, y0, x1 - x0 + 1, y1 - y0 + 1), b.PixelFormat);
            trims[b] = t; return t;
        }
        // общий фон окон: «корпус» прибора — тёмный градиент, скругление, кромка цвета облика
        public static void Body(Graphics g, Rectangle r, float rad)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var p = rad <= 0 ? Rect(new RectangleF(r.X + 0.5f, r.Y + 0.5f, r.Width - 1, r.Height - 1)) : Theme.Round(new RectangleF(r.X + 0.5f, r.Y + 0.5f, r.Width - 1, r.Height - 1), rad))
            {
                using (var b = new LinearGradientBrush(new RectangleF(r.X, r.Y - 1, r.Width, r.Height + 2), Theme.Shade(Color.FromArgb(34, 38, 45)), Theme.Shade(Color.FromArgb(13, 15, 19)), LinearGradientMode.Vertical)) g.FillPath(b, p);
                if (rad > 0) for (int k = 3; k >= 1; k--) using (var pen = new Pen(Theme.A(Theme.Teal, 0.10f * k), 1f + 2 * (3 - k))) g.DrawPath(pen, p);
                using (var pen = new Pen(Theme.A(Theme.Teal, 0.55f), 1.2f)) g.DrawPath(pen, p);
            }
        }
        // утопленный «экран» как у осциллографа
        public static void Screen(Graphics g, RectangleF r, float rad)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var p = Theme.Round(r, rad))
            {
                using (var b = new LinearGradientBrush(new RectangleF(r.X, r.Y - 1, r.Width, r.Height + 2), Color.FromArgb(5, 8, 10), Color.FromArgb(10, 14, 17), LinearGradientMode.Vertical)) g.FillPath(b, p);
                using (var pen = new Pen(Color.FromArgb(140, 0, 0, 0), 2f)) g.DrawPath(pen, p);
                using (var pen = new Pen(Color.FromArgb(22, 255, 255, 255), 1f)) g.DrawPath(pen, Theme.Round(new RectangleF(r.X - 1, r.Y - 1, r.Width + 2, r.Height + 2), rad + 1));
            }
        }
        public static void Gloss(Graphics g, RectangleF r, float rad)
        {
            using (var p = Theme.Round(new RectangleF(r.X + 2, r.Y + 2, r.Width - 4, r.Height * 0.35f), rad))
            using (var b = new LinearGradientBrush(new RectangleF(r.X, r.Y, r.Width, r.Height * 0.35f + 2), Color.FromArgb(18, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), LinearGradientMode.Vertical)) g.FillPath(b, p);
        }
        public static string Fmt(double sec) { if (sec < 0) sec = 0; var t = TimeSpan.FromSeconds(sec); return ((int)t.TotalMinutes).ToString("00") + ":" + t.Seconds.ToString("00") + "." + (t.Milliseconds / 100); }
        public static bool Parse(string s, out double sec)
        {
            sec = 0; s = (s ?? "").Trim().Replace(',', '.');
            try
            {
                var parts = s.Split(':'); double v = 0;
                foreach (var q in parts) v = v * 60 + double.Parse(q, System.Globalization.CultureInfo.InvariantCulture);
                sec = v; return true;
            }
            catch { return false; }
        }
    }

    class XyzButton : Control
    {
        readonly bool main; bool hot, down;
        public XyzButton(string t, bool m)
        {
            Text = t; main = m; Cursor = Cursors.Hand;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent; Font = Theme.Px(Theme.UiSemi, 13f, FontStyle.Regular);
            var sz = TextRenderer.MeasureText(t, Font); Size = new Size(sz.Width + 30, 34); Margin = new Padding(4, 4, 6, 4);
        }
        protected override void OnMouseEnter(EventArgs e) { hot = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hot = false; down = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { down = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { down = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new RectangleF(3, 3, Width - 6, Height - 6);
            using (var p = Theme.Round(r, r.Height / 2))
            {
                if (main)
                {
                    float k = !Enabled ? 0.35f : down ? 0.8f : hot ? 1f : 0.9f;
                    for (int i = 3; i >= 1; i--) using (var pen = new Pen(Theme.A(Theme.Teal, 0.12f * i * k), 2f * (4 - i))) g.DrawPath(pen, p);
                    using (var b = new LinearGradientBrush(new RectangleF(r.X, r.Y - 1, r.Width, r.Height + 2), Theme.A(Theme.Mix(Theme.Teal, Color.White, 0.25f), k), Theme.A(Theme.Teal, k), LinearGradientMode.Vertical)) g.FillPath(b, p);
                }
                else
                {
                    using (var b = new LinearGradientBrush(new RectangleF(r.X, r.Y - 1, r.Width, r.Height + 2), Theme.Shade(Color.FromArgb(down ? 30 : 50, down ? 34 : 56, down ? 40 : 64)), Theme.Shade(Color.FromArgb(22, 25, 30)), LinearGradientMode.Vertical)) g.FillPath(b, p);
                    using (var pen = new Pen(hot ? Theme.A(Theme.Teal, 0.85f) : Color.FromArgb(40, 255, 255, 255), hot ? 1.4f : 1f)) g.DrawPath(pen, p);
                }
            }
            var tc = main ? Color.FromArgb(Enabled ? 255 : 120, 12, 14, 17) : (Enabled ? (hot ? Color.White : Theme.Ink) : Theme.Dim);
            TextRenderer.DrawText(g, Text, Font, Rectangle.Round(r), tc, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }
    }

    // окно в стиле прибора: без системной рамки, своя шапка (эмблема, заголовок, ✕), перетаскивание за шапку
    class XyzForm : Form
    {
        protected string Caption = ""; protected const int HeadH = 40; Rectangle closeR; bool closeHot; Point dragFrom; bool dragging;
        public XyzForm()
        {
            FormBorderStyle = FormBorderStyle.None; DoubleBuffered = true; BackColor = Theme.Shade(Color.FromArgb(18, 21, 25));
            ForeColor = Theme.Ink; Font = Theme.Px(Theme.UiFam, 13f, FontStyle.Regular); Padding = new Padding(12, HeadH + 4, 12, 10);
            try { using (var st = Res.Raw("app.ico")) if (st != null) Icon = new Icon(st); } catch { }
        }
        protected override CreateParams CreateParams { get { var cp = base.CreateParams; cp.ClassStyle |= 0x20000; return cp; } }   // тень окна
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Theme.Shade(Color.FromArgb(18, 21, 25)));
            Ui19.Body(g, ClientRectangle, 0);
            var em = Ui19.Trim(Res.Get("emblem", 0.5f));
            int x = 14, eh = HeadH - 12;
            if (em != null) { g.InterpolationMode = InterpolationMode.HighQualityBicubic; int ew = eh * em.Width / Math.Max(1, em.Height); g.DrawImage(em, new Rectangle(x, 7, ew, eh)); x += ew + 10; }
            TextRenderer.DrawText(g, Caption, Theme.Px(Theme.UiSemi, 14f, FontStyle.Regular), new Rectangle(x, 0, Width - x - 50, HeadH + 2), Theme.Teal, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            closeR = new Rectangle(Width - 38, (HeadH - 26) / 2 + 2, 26, 26);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (closeHot) using (var b = new SolidBrush(Color.FromArgb(150, 200, 50, 46))) g.FillEllipse(b, closeR);
            using (var pen = new Pen(closeHot ? Color.White : Theme.Mute, 1.6f)) { var c = new PointF(closeR.X + 13, closeR.Y + 13); g.DrawLine(pen, c.X - 5, c.Y - 5, c.X + 5, c.Y + 5); g.DrawLine(pen, c.X - 5, c.Y + 5, c.X + 5, c.Y - 5); }
            using (var pen = new Pen(Color.FromArgb(28, 255, 255, 255))) g.DrawLine(pen, 14, HeadH + 1, Width - 14, HeadH + 1);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool h = closeR.Contains(e.Location); if (h != closeHot) { closeHot = h; Invalidate(closeR); }
            if (dragging) { var p = PointToScreen(e.Location); Location = new Point(p.X - dragFrom.X, p.Y - dragFrom.Y); }
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e) { if (closeHot) { closeHot = false; Invalidate(closeR); } base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && closeR.Contains(e.Location)) { Close(); return; }
            if (e.Button == MouseButtons.Left && e.Y < HeadH) { dragging = true; dragFrom = e.Location; }
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e) { dragging = false; base.OnMouseUp(e); }
        protected override void OnResize(EventArgs e) { base.OnResize(e); Invalidate(); }   // v19: углы прямые — скругление через Region даёт рваный край (замечание db 01:10)
    }

    class SavedToast : XyzForm
    {
        readonly string file; readonly System.Windows.Forms.Timer life = new System.Windows.Forms.Timer();
        int left = 10;
        public SavedToast(string path, string msg, bool warn, bool exclude)
        {
            file = path;
            Caption = warn ? "Сохранено (с предупреждением)" : "Сохранено";
            ShowInTaskbar = false; TopMost = true; StartPosition = FormStartPosition.Manual;
            AutoSize = true; AutoSizeMode = AutoSizeMode.GrowAndShrink; Padding = new Padding(0, 0, 16, 12); MinimumSize = new Size(340, 0);
            var col = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, BackColor = Color.Transparent, WrapContents = false };
            double dd = TrimForm.Duration(path);
            var t2 = new Label { Text = Path.GetFileName(path), AutoSize = true, BackColor = Color.Transparent, ForeColor = Theme.Ink, Font = Theme.Px(Theme.UiFam, 13f, FontStyle.Regular), Margin = new Padding(3, 0, 3, 0), MaximumSize = new Size(420, 0) };
            var t3 = new Label { Text = dd > 0 ? Ui19.Fmt(dd).Substring(0, 5) : "", AutoSize = true, BackColor = Color.Transparent, ForeColor = Theme.Teal, Font = Theme.Px(Theme.DigitFam, 26f, FontStyle.Regular), Margin = new Padding(1, 0, 3, 6) };
            var row = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, BackColor = Color.Transparent, WrapContents = false };
            var bOpen = Ui19.Btn("Открыть", true); bOpen.Click += (o, e) => { try { Process.Start(file); } catch { } Close(); };
            var bDir = Ui19.Btn("Папка", false); bDir.Click += (o, e) => { try { Process.Start("explorer.exe", "/select,\"" + file + "\""); } catch { } Close(); };
            var bCut = Ui19.Btn("Обрезать…", false); bCut.Click += (o, e) => { TrimFiles.Launch(file); Close(); };   // v19.5: редактор — отдельным процессом, прибор свободен
            row.Controls.AddRange(new Control[] { bOpen, bDir, bCut });
            col.Controls.AddRange(new Control[] { t3, t2, row });
            col.Location = new Point(12, HeadH + 6);   // под шапкой
            Controls.Add(col);
            if (exclude) HandleCreated += (o, e) => { try { Native.SetWindowDisplayAffinity(Handle, Native.WDA_EXCLUDEFROMCAPTURE); } catch { } };
            life.Interval = 1000;
            life.Tick += (o, e) => { if (Bounds.Contains(Cursor.Position)) { left = 6; return; } if (--left <= 0) Close(); };
            Shown += (o, e) => { var wa = Screen.FromPoint(Cursor.Position).WorkingArea; Location = new Point(wa.Right - Width - 16, wa.Bottom - Height - 16); life.Start(); };
        }
        protected override bool ShowWithoutActivation { get { return true; } }
        protected override void Dispose(bool d) { if (d) life.Dispose(); base.Dispose(d); }
    }

    // v19.8: показ нажатий в ролике — во время записи ловим сочетания клавиш и щелчки мыши,
    // после «Стоп» ffmpeg рисует их поверх видео. На экране у пользователя ничего нет.
    // v19.12 (замечания db): как у Bandicam — подсветка курсора мягким радиальным градиентом и без отставания
    // (время событий привязано к кадрам ролика: по каждому сегменту вычитается задержка старта ffmpeg,
    // положение курсора интерполируется на каждый кадр); щелчки — красная (левая) / синяя (правая) мягкая вспышка;
    // клавиши — крупный белый жирный текст с тёмной обводкой без плашки, 9 позиций, 3 размера, режим «все нажатия»
    // (набор склеивается в строку). Настройки — окно «Мышь и клавиатура» (MouseKeysForm), ini: mk_*.
    static class KeyLog
    {
        public class Ev { public double T; public string Text; public int X, Y, Kind; public bool Typ; }   // Kind: 0 — клавиши, 1 — левая, 2 — правая, 3 — средняя
        public static readonly List<Ev> Events = new List<Ev>();
        // v19.9: хуки — на своём потоке с циклом сообщений. Время — свой секундомер, без обращений к окну.
        static readonly Stopwatch clk = new Stopwatch(); static double accT = 0, segT0 = 0; static volatile bool active;
        static double Clock() { return accT + clk.Elapsed.TotalSeconds; }
        // v19.12: границы сегментов по секундомеру (пауза = новый сегмент ffmpeg) и длительности сегментов-файлов
        public static readonly List<double[]> SegClk = new List<double[]>();
        public static List<double> SegDur;
        // v19.13: часы Windows (Unix, с) в момент старта секундомера каждого сегмента и время первого кадра сегмента от ffmpeg
        public static readonly List<double> SegWall = new List<double>();
        public static List<double> SegFfStart;
        public static List<string> SegSrc;     // v19.14: ddagrab / gdigrab по сегментам
        public static double DdaShift = 0.1;     // v19.14: запасная поправка для ddagrab, с (ini mk_dda_shift; + — подсветка/щелчки/клавиши позже)
        static double UnixNow() { return (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds; }
        public static void Pause()
        {
            if (clk.IsRunning) { accT += clk.Elapsed.TotalSeconds; clk.Reset(); lock (SegClk) SegClk.Add(new[] { segT0, accT }); }
            active = false;
        }
        public static void Resume()
        {
            segT0 = accT; double w = UnixNow(); clk.Restart(); active = true;
            lock (SegClk) SegWall.Add(w);
            if (Halo) { POINT p; if (GetCursorPos(out p)) lock (Moves) Moves.Add(new Ev { T = accT, X = p.X, Y = p.Y }); }   // курсор виден с первого кадра
        }
        static Thread hookThread; static uint hookTid;
        [StructLayout(LayoutKind.Sequential)] struct POINT { public int X, Y; }
        [DllImport("user32.dll")] static extern bool GetCursorPos(out POINT p);
        [DllImport("kernel32.dll")] static extern uint GetCurrentThreadId();
        [DllImport("user32.dll")] static extern bool PostThreadMessage(uint tid, uint msg, IntPtr w, IntPtr l);
        [StructLayout(LayoutKind.Sequential)] struct MSG { public IntPtr h; public uint m; public IntPtr w, l; public uint t; public int x, y; }
        [DllImport("user32.dll")] static extern int GetMessage(out MSG m, IntPtr h, uint a, uint b);

        // ---- настройки (ini программы, окно «Мышь и клавиатура») ----
        public static bool Keys = true, Clicks = true, Halo = true, KeyAll = false, Cursor = true;
        public static int HaloSize = 100, HaloOp = 55, ClickSize = 100, KeyPos = 6, KeySize = 1;   // KeyPos: 0..8 слева направо, сверху вниз (6 — левый нижний)
        public static Color HaloColor = Color.FromArgb(255, 235, 59), ClickL = Color.FromArgb(255, 45, 45), ClickR = Color.FromArgb(40, 120, 255);
        public static string ColStr(Color c) { return c.R + "," + c.G + "," + c.B; }
        public static Color ColParse(string s, Color def)
        {
            try { var p = s.Split(','); if (p.Length == 3) return Color.FromArgb(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2])); } catch { }
            return def;
        }
        static int Int(Settings c, string k, int def, int lo, int hi) { int v; if (!int.TryParse(c.Get(k, ""), out v)) v = def; return Math.Max(lo, Math.Min(hi, v)); }
        public static void LoadCfg(Settings c)
        {
            Keys = c.Get("keys", "1") == "1"; Halo = c.Get("halo", "1") == "1";
            Clicks = c.Get("mk_clicks", Keys ? "1" : "0") == "1"; Cursor = c.Get("mk_cursor", "1") == "1";
            KeyAll = c.Get("mk_keyall", "0") == "1";
            HaloSize = Int(c, "mk_halo_size", 100, 30, 300); HaloOp = Int(c, "mk_halo_op", 55, 5, 100);
            ClickSize = Int(c, "mk_click_size", 100, 30, 300); KeyPos = Int(c, "mk_key_pos", 6, 0, 8); KeySize = Int(c, "mk_key_size", 1, 0, 2);
            HaloColor = ColParse(c.Get("mk_halo_color", ""), Color.FromArgb(255, 235, 59));
            ClickL = ColParse(c.Get("mk_click_l", ""), Color.FromArgb(255, 45, 45)); ClickR = ColParse(c.Get("mk_click_r", ""), Color.FromArgb(40, 120, 255));
            double ds; DdaShift = double.TryParse(c.Get("mk_dda_shift", "0.1").Replace(',', '.'), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out ds) ? Math.Max(-1, Math.Min(1, ds)) : 0.1;
        }
        public static void SaveCfg(Settings c)
        {
            c.Set("keys", Keys ? "1" : "0"); c.Set("halo", Halo ? "1" : "0"); c.Set("mk_clicks", Clicks ? "1" : "0"); c.Set("mk_cursor", Cursor ? "1" : "0");
            c.Set("mk_keyall", KeyAll ? "1" : "0"); c.Set("mk_halo_size", HaloSize.ToString()); c.Set("mk_halo_op", HaloOp.ToString());
            c.Set("mk_click_size", ClickSize.ToString()); c.Set("mk_key_pos", KeyPos.ToString()); c.Set("mk_key_size", KeySize.ToString());
            c.Set("mk_halo_color", ColStr(HaloColor)); c.Set("mk_click_l", ColStr(ClickL)); c.Set("mk_click_r", ColStr(ClickR));
            c.Set("mk_dda_shift", DdaShift.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture));   // v19.14: ключ виден в ini, чтобы его можно было поправить
            c.Save();
        }

        public static readonly List<Ev> Moves = new List<Ev>();   // положение курсора для подсветки (v19.12: до ~120 раз в секунду, дальше — интерполяция по кадрам)
        static double lastMove = -1;
        static string typBuf = ""; static double typLast = -10;
        static IntPtr hk = IntPtr.Zero, hm = IntPtr.Zero;
        delegate IntPtr HookProc(int code, IntPtr w, IntPtr l);
        static HookProc kp = KbProc, mp = MsProc;   // держим ссылки — иначе сборщик мусора уберёт делегаты
        [DllImport("user32.dll")] static extern IntPtr SetWindowsHookEx(int id, HookProc fn, IntPtr mod, uint tid);
        [DllImport("user32.dll")] static extern bool UnhookWindowsHookEx(IntPtr h);
        [DllImport("user32.dll")] static extern IntPtr CallNextHookEx(IntPtr h, int code, IntPtr w, IntPtr l);
        [DllImport("user32.dll")] static extern short GetAsyncKeyState(int vk);
        [DllImport("user32.dll")] static extern int GetKeyNameText(int lParam, StringBuilder s, int n);
        [DllImport("kernel32.dll")] static extern IntPtr GetModuleHandle(string n);
        public static void Start()
        {
            lock (Events) Events.Clear(); lock (Moves) Moves.Clear(); lock (SegClk) { SegClk.Clear(); SegWall.Clear(); } SegDur = null; SegFfStart = null; SegSrc = null; lastMove = -1; typBuf = ""; typLast = -10;
            accT = 0; clk.Reset(); Resume();
            if (hookThread != null) return;
            var ready = new ManualResetEvent(false);
            hookThread = new Thread(() =>
            {
                hookTid = GetCurrentThreadId();
                if (Keys) hk = SetWindowsHookEx(13, kp, GetModuleHandle(null), 0);
                if (Halo || Clicks) hm = SetWindowsHookEx(14, mp, GetModuleHandle(null), 0);
                ready.Set();
                MSG m; while (GetMessage(out m, IntPtr.Zero, 0, 0) > 0) { }
                if (hk != IntPtr.Zero) { UnhookWindowsHookEx(hk); hk = IntPtr.Zero; }
                if (hm != IntPtr.Zero) { UnhookWindowsHookEx(hm); hm = IntPtr.Zero; }
            });
            hookThread.IsBackground = true; hookThread.Priority = ThreadPriority.Highest; hookThread.Start();
            ready.WaitOne(2000);
        }
        public static void Stop()
        {
            Pause();
            if (hookThread != null) { PostThreadMessage(hookTid, 0x0012, IntPtr.Zero, IntPtr.Zero); hookThread.Join(1000); hookThread = null; }
        }
        static bool Down(int vk) { return (GetAsyncKeyState(vk) & 0x8000) != 0; }
        static string Name(int vk, int scan)
        {
            if (vk >= 0x41 && vk <= 0x5A) return ((char)vk).ToString();
            if (vk >= 0x30 && vk <= 0x39) return ((char)vk).ToString();
            if (vk >= 0x60 && vk <= 0x69) return ((char)('0' + vk - 0x60)).ToString();   // цифры с Num
            if (vk >= 0x70 && vk <= 0x87) return "F" + (vk - 0x6F);
            switch (vk)
            {
                case 0x1B: return "Esc"; case 0x09: return "Tab"; case 0x0D: return "Enter"; case 0x2E: return "Delete"; case 0x08: return "Back Space";
                case 0x20: return "Space"; case 0x25: return "←"; case 0x26: return "↑"; case 0x27: return "→"; case 0x28: return "↓";
                case 0x24: return "Home"; case 0x23: return "End"; case 0x21: return "PgUp"; case 0x22: return "PgDn"; case 0x2D: return "Insert";
                case 0x6A: return "*"; case 0x6B: return "+"; case 0x6D: return "-"; case 0x6E: return "."; case 0x6F: return "/";
                case 0x14: return "Caps Lock"; case 0x2C: return "Print Screen"; case 0x13: return "Pause"; case 0x5D: return "Menu";
            }
            var sb = new StringBuilder(32); GetKeyNameText(scan << 16, sb, 32); return sb.Length > 0 ? sb.ToString() : "#" + vk;
        }
        static void AddKey(Ev e) { lock (Events) Events.Add(e); }
        static IntPtr KbProc(int code, IntPtr w, IntPtr l)
        {
            try
            {
                int msg = w.ToInt32();
                if (code >= 0 && Keys && active && (msg == 0x100 || msg == 0x104))
                {
                    int vk = Marshal.ReadInt32(l), scan = Marshal.ReadInt32(l, 4);
                    bool isMod = vk == 0x10 || vk == 0x11 || vk == 0x12 || vk == 0x5B || vk == 0x5C || (vk >= 0xA0 && vk <= 0xA5);
                    if (!isMod && vk != 0x78)   // F9 — наша клавиша записи
                    {
                        bool c = Down(0x11), a = Down(0x12), wn = Down(0x5B) || Down(0x5C), sh = Down(0x10);
                        bool special = vk == 0x1B || vk == 0x09 || vk == 0x0D || vk == 0x2E || vk == 0x08 || (vk >= 0x70 && vk <= 0x7B);
                        double now = Clock();
                        if (c || a || wn || special)
                        {
                            string t = (c ? "Ctrl + " : "") + (a ? "Alt + " : "") + (wn ? "Win + " : "") + (sh ? "Shift + " : "") + Name(vk, scan);
                            typBuf = ""; AddKey(new Ev { T = now, Text = t, Kind = 0 });
                        }
                        else if (KeyAll)
                        {
                            // v19.12: «все нажатия» — буквы, цифры, знаки и пробел склеиваются в строку («FGDGG»), остальное — словом
                            string n = Name(vk, scan);
                            bool glyph = vk == 0x20 || n.Length == 1;
                            if (glyph)
                            {
                                string ch = vk == 0x20 ? " " : n.ToUpperInvariant();
                                typBuf = (now - typLast < 1.6 && typBuf.Length > 0 ? typBuf : "") + ch;
                                if (typBuf.Length > 28) typBuf = typBuf.Substring(typBuf.Length - 28);
                                typLast = now;
                                AddKey(new Ev { T = now, Text = typBuf.Trim().Length == 0 ? "Space" : typBuf, Kind = 0, Typ = true });
                            }
                            else { typBuf = ""; AddKey(new Ev { T = now, Text = (sh ? "Shift + " : "") + n, Kind = 0 }); }
                        }
                    }
                }
            }
            catch { }
            return CallNextHookEx(hk, code, w, l);
        }
        static IntPtr MsProc(int code, IntPtr w, IntPtr l)
        {
            try
            {
                int msg = w.ToInt32();
                if (code >= 0 && Halo && active && msg == 0x200)
                {
                    double now = Clock();
                    if (now - lastMove >= 0.008) { lastMove = now; lock (Moves) Moves.Add(new Ev { T = now, X = Marshal.ReadInt32(l), Y = Marshal.ReadInt32(l, 4) }); }
                }
                if (code >= 0 && Clicks && active && (msg == 0x201 || msg == 0x204 || msg == 0x207))
                {
                    int x = Marshal.ReadInt32(l), y = Marshal.ReadInt32(l, 4);
                    double now = Clock();
                    lock (Events) Events.Add(new Ev { T = now, X = x, Y = y, Kind = msg == 0x201 ? 1 : msg == 0x204 ? 2 : 3 });
                }
            }
            catch { }
            return CallNextHookEx(hm, code, w, l);
        }

        // ---- картинки (общие для ролика и предпросмотра в окне настроек) ----
        // мягкое пятно: ярко в центре, к краю плавно в прозрачность (smoothstep), без контура
        public static Bitmap HaloBmp(int d, Color c, float op)
        {
            d = Math.Max(8, d); var bm = new Bitmap(d, d, PixelFormat.Format32bppArgb);
            var px = new int[d * d]; float R = d / 2f;
            for (int y = 0; y < d; y++) for (int x = 0; x < d; x++)
            {
                float dx = (x + 0.5f - R) / R, dy = (y + 0.5f - R) / R, r2 = dx * dx + dy * dy;
                if (r2 >= 1f) continue;
                float r = (float)Math.Sqrt(r2), u = Math.Max(0f, Math.Min(1f, (r - 0.15f) / 0.85f)), k = 1f - u * u * (3f - 2f * u);   // плато в центре, плавный спад к краю
                int a = (int)Math.Round(255 * op * k); if (a <= 0) continue;
                px[y * d + x] = (a << 24) | (c.R << 16) | (c.G << 8) | c.B;
            }
            var bd = bm.LockBits(new Rectangle(0, 0, d, d), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(px, 0, bd.Scan0, px.Length); bm.UnlockBits(bd);
            return bm;
        }
        // вспышка щелчка: светящееся мягкое кольцо + лёгкая заливка в центре (курсор под ней остаётся виден)
        public static Bitmap ClickBmp(int d, Color c, float op)
        {
            d = Math.Max(8, d); var bm = new Bitmap(d, d, PixelFormat.Format32bppArgb);
            var px = new int[d * d]; float R = d / 2f;
            for (int y = 0; y < d; y++) for (int x = 0; x < d; x++)
            {
                float dx = (x + 0.5f - R) / R, dy = (y + 0.5f - R) / R, r = (float)Math.Sqrt(dx * dx + dy * dy);
                if (r >= 1f) continue;
                float ring = (float)Math.Exp(-Math.Pow((r - 0.62f) / 0.2f, 2)), fill = 0.28f * (1f - r);
                float k = Math.Min(1f, ring * 0.95f + fill); if (r > 0.85f) k *= (1f - r) / 0.15f;
                int a = (int)Math.Round(255 * op * k); if (a <= 0) continue;
                px[y * d + x] = (a << 24) | (c.R << 16) | (c.G << 8) | c.B;
            }
            var bd = bm.LockBits(new Rectangle(0, 0, d, d), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(px, 0, bd.Scan0, px.Length); bm.UnlockBits(bd);
            return bm;
        }
        public static int HaloD(int H) { return Math.Max(48, H / 12); }   // 100 %: 120 px при 1440
        public static int ClickD(int H) { return Math.Max(48, H / 11); }
        public static float KeyFs(int H) { return Math.Max(KeySize == 0 ? 20 : KeySize == 1 ? 28 : 38, H / (KeySize == 0 ? 28f : KeySize == 1 ? 20f : 14f)); }
        // текст клавиш как у Bandicam: белый жирный, тёмная обводка и мягкая тень, без плашки
        public static void DrawKeyText(Graphics g, string text, float fs, float x, float y)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var fam = new FontFamily("Segoe UI"))
            using (var gp = new GraphicsPath())
            {
                gp.AddString(text, fam, (int)FontStyle.Bold, fs, new PointF(x, y), StringFormat.GenericTypographic);
                for (int i = 4; i >= 1; i--)
                    using (var pen = new Pen(Color.FromArgb(28, 0, 0, 0), fs * (0.10f + 0.07f * i)) { LineJoin = LineJoin.Round })
                    { var m = new Matrix(); m.Translate(fs * 0.04f, fs * 0.06f); using (var sh = (GraphicsPath)gp.Clone()) { sh.Transform(m); g.DrawPath(pen, sh); } }
                using (var pen = new Pen(Color.FromArgb(235, 16, 18, 22), Math.Max(2f, fs * 0.13f)) { LineJoin = LineJoin.Round }) g.DrawPath(pen, gp);
                using (var br = new SolidBrush(Color.White)) g.FillPath(br, gp);
            }
        }
        public static SizeF KeyTextSize(string text, float fs)
        {
            using (var fam = new FontFamily("Segoe UI")) using (var gp = new GraphicsPath())
            {
                gp.AddString(text, fam, (int)FontStyle.Bold, fs, new PointF(0, 0), StringFormat.GenericTypographic);
                var b = gp.GetBounds(); return new SizeF(Math.Max(1, b.Right), Math.Max(1, b.Bottom));
            }
        }

        static string Esc(string p) { return p.Replace("\\", "/").Replace(":", "\\:").Replace("'", "\\'"); }
        static string Png(string dir, string name, Bitmap b) { string f = Path.Combine(dir, name); b.Save(f, ImageFormat.Png); b.Dispose(); return f; }

        // свойства ролика по выводу ffmpeg -i: длительность, кадров/с, размер
        public static void Probe(string ff, string f, out double dur, out double fps, out int w, out int h)
        {
            dur = 0; fps = 0; w = 0; h = 0;
            try
            {
                var psi = new ProcessStartInfo(ff, "-hide_banner -i \"" + f + "\"") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardError = true, StandardErrorEncoding = Encoding.UTF8 };
                string err; using (var p = Process.Start(psi)) { err = p.StandardError.ReadToEnd(); p.WaitForExit(5000); }
                var inv = System.Globalization.CultureInfo.InvariantCulture;
                var m = System.Text.RegularExpressions.Regex.Match(err, @"Duration:\s*(\d+):(\d+):(\d+(?:\.\d+)?)");
                if (m.Success) dur = int.Parse(m.Groups[1].Value) * 3600 + int.Parse(m.Groups[2].Value) * 60 + double.Parse(m.Groups[3].Value, inv);
                var mv = System.Text.RegularExpressions.Regex.Match(err, @"Video:[^\r\n]*?(\d{2,5})x(\d{2,5})[^\r\n]*?(\d+(?:\.\d+)?) (?:fps|tbr)");
                if (mv.Success) { w = int.Parse(mv.Groups[1].Value); h = int.Parse(mv.Groups[2].Value); fps = double.Parse(mv.Groups[3].Value, inv); }
            }
            catch { }
        }

        // время секундомера → время ролика. v19.13: по каждому сегменту сдвиг = (часы в момент старта секундомера) − (часы первого
        // кадра от ffmpeg): насколько кадр 0 сегмента раньше нуля секундомера. Сегменты склеены встык.
        // v19.12 выводил сдвиг из разницы длин «секундомер − файл» и относил её целиком к началу. Но файл длиннее секундомера
        // в основном с КОНЦА (секундомер останавливается до «q», ffmpeg дописывает хвост; длина файла — по самому длинному
        // потоку, со звуком), и всё это ошибочно уходило в начало: у db подсветка, щелчки и клавиши опаздывали на 5 кадров
        // (0,33 с при 15 к/с) по всему ролику. Время первого кадра не узнали — без сдвига (ошибка не больше задержки старта ffmpeg).
        // v19.14: важное — и в ffmpeg.log (временная папка, удаляется), и в %APPDATA%\XYZ\recorder.log
        static void Both(Action<string> log, string s) { if (log != null) log(s); AppLog.W(s); }
        static Func<double, double> Mapper(double outDur, Action<string> log)
        {
            List<double[]> sc; List<double> sw; lock (SegClk) { sc = new List<double[]>(SegClk); sw = new List<double>(SegWall); }
            var sd = SegDur; var fs = SegFfStart; var src = SegSrc;
            var inv = System.Globalization.CultureInfo.InvariantCulture;
            if (sc.Count == 0) { Both(log, "=== нажатия/курсор: нет сегментов секундомера — без сдвига"); return t => Math.Max(0, t); }
            int n = sc.Count;
            bool haveDur = sd != null && sd.Count == n && !sd.Contains(0);
            var vs = new double[n]; var off = new double[n]; double acc = 0;
            var sb = new StringBuilder("кадр 0 раньше секундомера, с:");
            for (int i = 0; i < n; i++)
            {
                vs[i] = acc; acc += haveDur ? sd[i] : sc[i][1] - sc[i][0];
                double o = double.NaN;
                if (fs != null && fs.Count == n && sw.Count == n && !double.IsNaN(fs[i])) o = sw[i] - fs[i];
                if (double.IsNaN(o) || o < -0.5 || o > 20) { sb.Append(double.IsNaN(o) ? " ?" : string.Format(inv, " {0:0.000}(отброшен)", o)); o = 0; }
                else sb.AppendFormat(inv, " {0:0.000}", o);
                string m = src != null && src.Count == n ? src[i] : "?";
                if (m == "ddagrab" && DdaShift != 0) { o += DdaShift; sb.AppendFormat(inv, " +mk_dda_shift {0:0.000}", DdaShift); }   // v19.14: запасная поправка
                sb.Append(" (" + m + ")");
                off[i] = o;
                if (haveDur) sb.AppendFormat(inv, " [секундомер−файл {0:0.000}]", (sc[i][1] - sc[i][0]) - sd[i]);   // для разбора: раньше это считалось сдвигом
            }
            Both(log, "=== нажатия/курсор: " + sb);
            return t =>
            {
                int i = n - 1; for (int j = 0; j < n; j++) if (t < sc[j][1]) { i = j; break; }
                double v = t - sc[i][0] + off[i];
                return vs[i] + Math.Max(0, haveDur ? Math.Min(sd[i], v) : v);
            };
        }

        // Наложить нажатия на готовый ролик (в ffmpeg программы нет drawtext — картинки PNG и overlay; положение подсветки и вспышек меняет sendcmd,
        // надписи клавиш — отдельный поток картинок по списку ffconcat). cap — область захвата (экранные координаты) для пересчёта точек.
        public static bool Burn(string ff, string inFile, string outFile, Rectangle cap, string work, Action<string> log, out string err)
        {
            err = null; var ev = new List<Ev>(); var mv = new List<Ev>();
            lock (Events) foreach (var e in Events) ev.Add(new Ev { T = e.T, Text = e.Text, X = e.X, Y = e.Y, Kind = e.Kind, Typ = e.Typ });
            lock (Moves) foreach (var e in Moves) mv.Add(new Ev { T = e.T, X = e.X, Y = e.Y });
            if (ev.Count == 0 && mv.Count == 0) { err = "нет событий"; return false; }
            var inv = System.Globalization.CultureInfo.InvariantCulture;
            string dir = Path.Combine(work, "keys"); Directory.CreateDirectory(dir);
            double dur, fps; int VW, VH; Probe(ff, inFile, out dur, out fps, out VW, out VH);
            if (fps <= 1 || fps > 240) fps = 15; if (VW <= 0) { VW = cap.Width / 2 * 2; VH = cap.Height / 2 * 2; }
            if (dur <= 0) { var l0 = mv.Count > 0 ? mv[mv.Count - 1].T : 0; foreach (var e in ev) l0 = Math.Max(l0, e.T); dur = l0 + 2; }
            var map = Mapper(dur, log);
            foreach (var e in ev) e.T = map(e.T); foreach (var m in mv) m.T = map(m.T);
            ev.Sort((a, b) => a.T.CompareTo(b.T)); mv.Sort((a, b) => a.T.CompareTo(b.T));

            int H = Math.Max(240, cap.Height);
            int hd = Math.Max(24, (int)(HaloD(H) * HaloSize / 100.0)) / 2 * 2, rd = Math.Max(24, (int)(ClickD(H) * ClickSize / 100.0)) / 2 * 2;
            var cmds = new List<KeyValuePair<double, string>>();
            var g = new StringBuilder(); g.Append("[0:v]sendcmd=f='").Append(Esc(Path.Combine(dir, "cmd.txt"))).Append("'[v0];\n");
            int vi = 0, n = 0;
            Action<string, string> ov = (png, opts) => { g.AppendFormat("movie='{0}'[m{1}];\n[v{1}][m{1}]overlay{2}[v{3}];\n", Esc(png), vi, opts, vi + 1); vi++; };
            double half = 0.5 / fps;
            if (Halo && mv.Count > 0)
            {
                ov(Png(dir, "halo.png", HaloBmp(hd, HaloColor, HaloOp / 100f)), "@halo=x=-4000:y=-4000:eof_action=repeat");
                // v19.12: положение на каждый кадр ролика, линейно между точками хука (паузу мыши не «размазываем»)
                int lx = int.MinValue, ly = int.MinValue, j = 0, N = (int)Math.Ceiling(dur * fps) + 1;
                for (int k = 0; k < N; k++)
                {
                    double t = k / fps; if (t < mv[0].T - half) continue;
                    while (j + 1 < mv.Count && mv[j + 1].T <= t) j++;
                    double x = mv[j].X, y = mv[j].Y;
                    if (j + 1 < mv.Count && mv[j].T <= t)
                    {
                        double gap = mv[j + 1].T - mv[j].T;
                        if (gap > 0 && gap <= 0.12) { double u = (t - mv[j].T) / gap; x += (mv[j + 1].X - x) * u; y += (mv[j + 1].Y - y) * u; }
                    }
                    int ix = (int)Math.Round(x) - cap.X - hd / 2, iy = (int)Math.Round(y) - cap.Y - hd / 2;
                    if (ix == lx && iy == ly) continue; lx = ix; ly = iy;
                    cmds.Add(new KeyValuePair<double, string>(Math.Max(0, t - half), string.Format(inv, "overlay@halo x {0}, overlay@halo y {1};", ix, iy)));
                }
                n++;
            }
            // вспышки щелчков: 3 ступени (растёт и гаснет), левая — красная, правая — синяя, средняя — как левая, но светлее
            var cols = new[] { Color.Empty, ClickL, ClickR, Theme.Mix(ClickL, Color.White, 0.45f) };
            var st = new[] { new[] { 0.0, 0.10, 0.72, 1.0 }, new[] { 0.10, 0.22, 0.88, 0.8 }, new[] { 0.22, 0.38, 1.0, 0.5 } };   // начало, конец, размер, яркость
            for (int k = 1; k <= 3; k++)
            {
                var cl = ev.FindAll(e => e.Kind == k); if (cl.Count == 0) continue;
                for (int s = 0; s < st.Length; s++)
                {
                    int ds = Math.Max(8, (int)(rd * st[s][2])) / 2 * 2; string nm = "c" + k + s;
                    ov(Png(dir, nm + ".png", ClickBmp(ds, cols[k], (float)st[s][3])), "@" + nm + "=x=-4000:y=-4000:eof_action=repeat");
                    foreach (var e in cl)
                    {
                        cmds.Add(new KeyValuePair<double, string>(e.T + st[s][0], string.Format(inv, "overlay@{0} x {1}, overlay@{0} y {2};", nm, e.X - cap.X - ds / 2, e.Y - cap.Y - ds / 2)));
                        cmds.Add(new KeyValuePair<double, string>(e.T + st[s][1], string.Format(inv, "overlay@{0} x -4000;", nm)));
                    }
                }
                n += cl.Count;
            }
            // клавиши: каждая надпись — PNG во всю ширину кадра (выравнивание по позиции внутри), поток картинок по списку ffconcat
            var keys = ev.FindAll(e => e.Kind == 0);
            string kin = null; float fs = KeyFs(VH); int lh = (int)Math.Ceiling(fs * 1.9f) / 2 * 2, row = KeyPos / 3, col = KeyPos % 3;
            if (keys.Count > 0)
            {
                var pngs = new Dictionary<string, string>();
                Func<string, string> label = text =>
                {
                    string f; if (pngs.TryGetValue(text, out f)) return f;
                    f = "k" + pngs.Count + ".png";
                    using (var bm = new Bitmap(VW, lh, PixelFormat.Format32bppArgb))
                    {
                        using (var gg = Graphics.FromImage(bm))
                        {
                            var sz = KeyTextSize(text, fs); float m = fs * 0.7f;
                            float x = col == 0 ? m : col == 1 ? (VW - sz.Width) / 2 : VW - m - sz.Width;
                            DrawKeyText(gg, text, fs, Math.Max(2, x), (lh - sz.Height) / 2f);
                        }
                        bm.Save(Path.Combine(dir, f), ImageFormat.Png);
                    }
                    pngs[text] = f; return f;
                };
                using (var bm = new Bitmap(VW, lh, PixelFormat.Format32bppArgb)) bm.Save(Path.Combine(dir, "blank.png"), ImageFormat.Png);
                var cc = new StringBuilder("ffconcat version 1.0\n"); double cur = 0;
                for (int i = 0; i < keys.Count; i++)
                {
                    var e = keys[i]; double t0 = Math.Max(cur, e.T), t1 = e.T + 1.8; if (i + 1 < keys.Count) t1 = Math.Min(t1, keys[i + 1].T); if (t1 < t0 + 1.0 / fps) t1 = t0 + 1.0 / fps;   // надпись — до следующей, но не меньше кадра
                    if (t0 > cur + 0.001) cc.AppendFormat(inv, "file blank.png\nduration {0:0.######}\n", t0 - cur);
                    cc.AppendFormat(inv, "file {0}\nduration {1:0.######}\n", label(e.Text), t1 - t0); cur = t1; n++;
                }
                cc.Append("file blank.png\nduration 1\nfile blank.png\n");
                kin = Path.Combine(dir, "keys.ffconcat"); File.WriteAllText(kin, cc.ToString(), new UTF8Encoding(false));
                float mY = fs * 0.35f; int ky = row == 0 ? (int)mY : row == 1 ? (VH - lh) / 2 : VH - lh - (int)mY;
                g.AppendFormat("[1:v]format=rgba[kk];\n[v{0}][kk]overlay=x=0:y={1}:eof_action=repeat[v{2}];\n", vi, ky, vi + 1); vi++;
            }
            if (n == 0) { err = "нет событий в кадре"; return false; }
            cmds.Sort((x, y) => x.Key.CompareTo(y.Key));
            var cs = new StringBuilder(); foreach (var c in cmds) cs.AppendFormat(inv, "{0:0.####} {1}\n", c.Key, c.Value);
            if (cs.Length == 0) cs.Append("0 overlay@none x 0;\n");
            File.WriteAllText(Path.Combine(dir, "cmd.txt"), cs.ToString(), new UTF8Encoding(false));
            string gs = g.ToString(); gs = gs.Substring(0, gs.LastIndexOf("[v" + vi + "];")) + "[vout]";
            string script = Path.Combine(dir, "graph.txt"); File.WriteAllText(script, gs, new UTF8Encoding(false));
            string args = "-hide_banner -y -i \"" + inFile + "\" " + (kin != null ? "-f concat -safe 0 -i \"" + kin + "\" " : "") + "-filter_complex_script \"" + script + "\" -map \"[vout]\" -map 0:a? -c:v libx264 -preset veryfast -crf 23 -pix_fmt yuv420p -c:a copy -movflags +faststart \"" + outFile + "\"";
            try
            {
                var psi = new ProcessStartInfo(ff, args) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardError = true };
                string tail;
                using (var p = Process.Start(psi)) { tail = p.StandardError.ReadToEnd(); p.WaitForExit(); if (p.ExitCode != 0 || !File.Exists(outFile)) { err = tail.Length > 800 ? tail.Substring(tail.Length - 800) : tail; return false; } }
                Both(log, "=== нажатия/курсор: " + n + " меток наложено, кадр " + VW + "x" + VH + " " + fps.ToString("0.##", inv) + " к/с, точек курсора " + mv.Count);
                return true;
            }
            catch (Exception ex) { err = ex.Message; return false; }
        }
        // проверка без записи: --keystest <mp4> [all] — придуманные события на первые 3 с (секундомер «опережает» ролик на 0,3 с)
        public static int SelfTest(string mp4, bool all)
        {
            string ff = Engine.FindFfmpeg(); var cap = new Rectangle(0, 0, 2560, 1440);
            try { LoadCfg(Settings.Load()); } catch { }
            Keys = Clicks = Halo = true; KeyAll = all;
            string work = Path.Combine(Path.GetTempPath(), "xyz_keystest"); Directory.CreateDirectory(work);
            string cut = Path.Combine(work, "in.mp4"), outF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", all ? "keystest_all.mp4" : "keystest.mp4");
            Directory.CreateDirectory(Path.GetDirectoryName(outF));
            using (var p = Process.Start(new ProcessStartInfo(ff, "-hide_banner -y -t 3 -i \"" + mp4 + "\" -an -vf scale=2560:1440,fps=15 -c:v libx264 -preset ultrafast -crf 30 \"" + cut + "\"") { UseShellExecute = false, CreateNoWindow = true })) p.WaitForExit(30000);
            const double D = 0.3;   // как будто ffmpeg начал писать кадры на 0,3 с позже секундомера
            lock (SegClk) { SegClk.Clear(); SegClk.Add(new[] { 0.0, 3.0 + D }); SegWall.Clear(); SegWall.Add(1000.0); }
            SegFfStart = new List<double> { 1000.0 + D };   // v19.13: первый кадр — на D позже старта секундомера
            double dur, fps; int w, h; Probe(ff, cut, out dur, out fps, out w, out h); SegDur = new List<double> { dur };
            lock (Events)
            {
                Events.Clear();
                Events.Add(new Ev { T = 0.5 + D, Text = "Ctrl + Z", Kind = 0 }); Events.Add(new Ev { T = 1.3 + D, X = 800, Y = 500, Kind = 1 }); Events.Add(new Ev { T = 2.1 + D, X = 1500, Y = 760, Kind = 2 });
                if (all) { string s = ""; foreach (var ch in "FGDGG") { s += ch; Events.Add(new Ev { T = 1.4 + D + s.Length * 0.12, Text = s, Kind = 0, Typ = true }); } Events.Add(new Ev { T = 2.3 + D, Text = "Back Space", Kind = 0 }); }
                else Events.Add(new Ev { T = 1.6 + D, Text = "Esc", Kind = 0 });
            }
            lock (Moves) { Moves.Clear(); for (int i = 0; i <= 90; i++) { double t = i / 30.0; Moves.Add(new Ev { T = t + D, X = 500 + (int)(t / 1.3 * 300), Y = 380 + (int)(t / 1.3 * 120) }); } }   // в 1,3 с курсор в (800,500)
            var log = new StringBuilder();
            string err; bool ok = Burn(ff, cut, outF, cap, work, x => log.AppendLine(x), out err);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", all ? "keystest_all.log" : "keystest.log"), (ok ? "ok" : "ERR " + err) + "\r\n" + log + "\r\n" + File.ReadAllText(Path.Combine(work, "keys", "graph.txt")));
            return ok ? 0 : 1;
        }
    }

    // v19.12: окно «Мышь и клавиатура» в стиле прибора — свои переключатели, кружки цвета, сетка позиции 3×3 и живой предпросмотр
    class MkCheck : Control
    {
        public bool Checked; public event EventHandler Changed; bool hot;
        public MkCheck(string t, bool on) { Text = t; Checked = on; Cursor = Cursors.Hand; SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true); BackColor = Color.Transparent; Font = Theme.Px(Theme.UiFam, 13f, FontStyle.Regular); Size = new Size(TextRenderer.MeasureText(t, Font).Width + 30, 24); }
        protected override void OnMouseEnter(EventArgs e) { hot = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hot = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnClick(EventArgs e) { Checked = !Checked; Invalidate(); if (Changed != null) Changed(this, e); base.OnClick(e); }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new RectangleF(2, (Height - 16) / 2f, 16, 16);
            using (var p = Theme.Round(r, 4))
            {
                if (Checked) { using (var b = new SolidBrush(Theme.Teal)) g.FillPath(b, p); using (var pen = new Pen(Color.FromArgb(12, 14, 17), 2f) { LineJoin = LineJoin.Round }) g.DrawLines(pen, new[] { new PointF(r.X + 3.5f, r.Y + 8.5f), new PointF(r.X + 6.8f, r.Y + 11.5f), new PointF(r.X + 12.5f, r.Y + 4.5f) }); }
                else { using (var b = new SolidBrush(Color.FromArgb(10, 13, 16))) g.FillPath(b, p); using (var pen = new Pen(hot ? Theme.Teal : Color.FromArgb(70, 255, 255, 255), 1.2f)) g.DrawPath(pen, p); }
            }
            TextRenderer.DrawText(g, Text, Font, new Rectangle(24, 0, Width - 24, Height), Enabled ? (hot ? Color.White : Theme.Ink) : Theme.Dim, TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }
    }
    // выбор из списка стрелками ‹ значение › (щелчок слева/справа, колесо)
    class MkChoice : Control
    {
        public string[] Items; public int Index; public event EventHandler Changed; int hot = 0;
        public MkChoice(string[] items, int idx, int w) { Items = items; Index = Math.Max(0, Math.Min(items.Length - 1, idx)); Cursor = Cursors.Hand; SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true); BackColor = Color.Transparent; Font = Theme.Px(Theme.UiSemi, 13f, FontStyle.Regular); Size = new Size(w, 28); }
        void Step(int d) { int i = Math.Max(0, Math.Min(Items.Length - 1, Index + d)); if (i == Index) return; Index = i; Invalidate(); if (Changed != null) Changed(this, EventArgs.Empty); }
        protected override void OnMouseMove(MouseEventArgs e) { int h = e.X < Width / 3 ? -1 : e.X > Width * 2 / 3 ? 1 : 0; if (h != hot) { hot = h; Invalidate(); } base.OnMouseMove(e); }
        protected override void OnMouseLeave(EventArgs e) { hot = 0; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { if (e.Button == MouseButtons.Left) Step(e.X < Width / 2 ? -1 : 1); base.OnMouseDown(e); }
        protected override void OnMouseWheel(MouseEventArgs e) { Step(e.Delta > 0 ? 1 : -1); base.OnMouseWheel(e); }
        protected override void OnMouseEnter(EventArgs e) { Focus(); base.OnMouseEnter(e); }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; Ui19.Screen(g, new RectangleF(1.5f, 1.5f, Width - 3, Height - 3), 6);
            TextRenderer.DrawText(g, Items[Index], Font, new Rectangle(16, 0, Width - 32, Height), Theme.Ink, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            var f = Theme.Px(Theme.UiSemi, 15f, FontStyle.Regular);
            TextRenderer.DrawText(g, "‹", f, new Rectangle(2, -1, 16, Height), Index > 0 ? (hot < 0 ? Color.White : Theme.Teal) : Theme.Dim, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            TextRenderer.DrawText(g, "›", f, new Rectangle(Width - 18, -1, 16, Height), Index < Items.Length - 1 ? (hot > 0 ? Color.White : Theme.Teal) : Theme.Dim, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
    class MkSwatch : Control
    {
        public Color Value; public event EventHandler Changed; bool hot;
        public MkSwatch(Color c) { Value = c; Cursor = Cursors.Hand; SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true); BackColor = Color.Transparent; Size = new Size(28, 28); }
        protected override void OnMouseEnter(EventArgs e) { hot = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hot = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnClick(EventArgs e)
        {
            using (var d = new ColorDialog { Color = Value, FullOpen = true, AnyColor = true })
                if (d.ShowDialog(FindForm()) == DialogResult.OK) { Value = d.Color; Invalidate(); if (Changed != null) Changed(this, e); }
            base.OnClick(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var b = new SolidBrush(Value)) g.FillEllipse(b, 4, 4, Width - 9, Height - 9);
            using (var pen = new Pen(hot ? Color.White : Color.FromArgb(120, 255, 255, 255), hot ? 2f : 1.2f)) g.DrawEllipse(pen, 3, 3, Width - 7, Height - 7);
        }
    }
    class MkPos : Control
    {
        public int Value; public event EventHandler Changed; int hot = -1;
        public MkPos(int v) { Value = v; Cursor = Cursors.Hand; SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true); BackColor = Color.Transparent; Size = new Size(84, 54); }
        int At(Point p) { int c = Math.Min(2, Math.Max(0, p.X * 3 / Width)), r = Math.Min(2, Math.Max(0, p.Y * 3 / Height)); return r * 3 + c; }
        protected override void OnMouseMove(MouseEventArgs e) { int h = At(e.Location); if (h != hot) { hot = h; Invalidate(); } base.OnMouseMove(e); }
        protected override void OnMouseLeave(EventArgs e) { hot = -1; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { Value = At(e.Location); Invalidate(); if (Changed != null) Changed(this, e); base.OnMouseDown(e); }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; Ui19.Screen(g, new RectangleF(1.5f, 1.5f, Width - 3, Height - 3), 5);
            float cw = (Width - 8) / 3f, ch = (Height - 8) / 3f;
            for (int i = 0; i < 9; i++)
            {
                var r = new RectangleF(4 + (i % 3) * cw + 3, 4 + (i / 3) * ch + 3, cw - 6, ch - 6);
                using (var p = Theme.Round(r, 3))
                {
                    if (i == Value) using (var b = new SolidBrush(Theme.Teal)) g.FillPath(b, p);
                    else using (var pen = new Pen(i == hot ? Theme.A(Theme.Teal, 0.9f) : Color.FromArgb(55, 255, 255, 255), 1f)) g.DrawPath(pen, p);
                }
            }
        }
    }
    class MkPreview : Control
    {
        public MkPreview() { SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw, true); BackColor = Color.Transparent; }
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
            var r = new RectangleF(1.5f, 1.5f, Width - 3, Height - 3);
            using (var p = Theme.Round(r, 6))
            {
                // «экран» с окном программы: светлый фон, как чертёж в Revit
                using (var b = new SolidBrush(Color.FromArgb(236, 238, 240))) g.FillPath(b, p);
                var old = g.Clip; g.SetClip(p);
                using (var b = new SolidBrush(Color.FromArgb(205, 210, 216))) g.FillRectangle(b, 0, 0, Width, 18);
                using (var pen = new Pen(Color.FromArgb(150, 160, 170), 1f)) for (int i = 0; i < 6; i++) g.DrawLine(pen, 20 + i * 44, 34, 20 + i * 44, Height - 20);
                float H = Height * 6f;   // предпросмотр — уменьшенный кадр высотой ~6 окон
                int hd = Math.Max(10, (int)(KeyLog.HaloD((int)H) * KeyLog.HaloSize / 100.0 / 6 * 1.6)), rd = Math.Max(10, (int)(KeyLog.ClickD((int)H) * KeyLog.ClickSize / 100.0 / 6 * 1.6));
                PointF c1 = new PointF(Width * 0.36f, Height * 0.40f), c2 = new PointF(Width * 0.70f, Height * 0.55f);
                if (KeyLog.Halo) using (var hb = KeyLog.HaloBmp(hd, KeyLog.HaloColor, KeyLog.HaloOp / 100f)) g.DrawImage(hb, c1.X - hd / 2f, c1.Y - hd / 2f);
                if (KeyLog.Clicks)
                {
                    using (var cb = KeyLog.ClickBmp(rd, KeyLog.ClickL, 1f)) g.DrawImage(cb, c1.X - rd / 2f, c1.Y - rd / 2f);
                    using (var cb = KeyLog.ClickBmp(rd, KeyLog.ClickR, 1f)) g.DrawImage(cb, c2.X - rd / 2f, c2.Y - rd / 2f);
                }
                if (KeyLog.Cursor) foreach (var c in new[] { c1, c2 }) Arrow(g, c);
                if (KeyLog.Keys)
                {
                    string t = KeyLog.KeyAll ? "FGDGG" : "Ctrl + Z"; float fs = KeyLog.KeyFs((int)H) / 6f * 1.6f;
                    var sz = KeyLog.KeyTextSize(t, fs); int row = KeyLog.KeyPos / 3, col = KeyLog.KeyPos % 3; float m = fs * 0.6f;
                    float x = col == 0 ? m : col == 1 ? (Width - sz.Width) / 2 : Width - m - sz.Width, y = row == 0 ? 20 + m * 0.4f : row == 1 ? (Height - sz.Height) / 2 : Height - sz.Height - m * 0.7f;
                    KeyLog.DrawKeyText(g, t, fs, x, y);
                }
                g.Clip = old;
                using (var pen = new Pen(Theme.A(Theme.Teal, 0.55f), 1.2f)) g.DrawPath(pen, p);
            }
        }
        static void Arrow(Graphics g, PointF c)
        {
            var pts = new[] { new PointF(0, 0), new PointF(0, 17), new PointF(4.5f, 13), new PointF(7.5f, 19.5f), new PointF(10, 18.5f), new PointF(7, 12), new PointF(12.5f, 12) };
            for (int i = 0; i < pts.Length; i++) pts[i] = new PointF(c.X + pts[i].X, c.Y + pts[i].Y);
            using (var b = new SolidBrush(Color.White)) g.FillPolygon(b, pts);
            using (var pen = new Pen(Color.Black, 1.2f) { LineJoin = LineJoin.Round }) g.DrawPolygon(pen, pts);
        }
    }
    class MouseKeysForm : XyzForm
    {
        readonly Settings cfg; readonly MkPreview prev = new MkPreview();
        static readonly int[] Pcts = { 50, 75, 100, 125, 150, 200 };
        static readonly int[] Ops = { 15, 25, 35, 45, 55, 70, 85 };
        static int Near(int[] a, int v) { int bi = 0; for (int i = 0; i < a.Length; i++) if (Math.Abs(a[i] - v) < Math.Abs(a[bi] - v)) bi = i; return bi; }
        static string[] Pct(int[] a) { var s = new string[a.Length]; for (int i = 0; i < a.Length; i++) s[i] = a[i] + " %"; return s; }
        Label L(string t, int x, int y, bool head)
        {
            var l = new Label { Text = t, AutoSize = true, BackColor = Color.Transparent, ForeColor = head ? Theme.Teal : Theme.Mute, Font = Theme.Px(head ? Theme.UiSemi : Theme.UiFam, head ? 14f : 12.5f, FontStyle.Regular), Location = new Point(x, y) };
            Controls.Add(l); return l;
        }
        T Add<T>(T c, int x, int y) where T : Control { c.Location = new Point(x, y); Controls.Add(c); return c; }
        public MouseKeysForm(Settings c)
        {
            cfg = c; KeyLog.LoadCfg(c);
            Caption = "Мышь и клавиатура в записи"; Text = "Мышь и клавиатура — XYZ Запись";
            ShowInTaskbar = false; StartPosition = FormStartPosition.Manual; KeyPreview = true;
            ClientSize = new Size(760, 470);
            EventHandler upd = (o, e) => prev.Invalidate();
            int x0 = 18, y = HeadH + 14, cx = 250;
            // курсор
            L("Курсор", x0, y, true); y += 28;
            var cur = Add(new MkCheck("Показывать курсор в записи", KeyLog.Cursor), x0, y); cur.Changed += (o, e) => { KeyLog.Cursor = cur.Checked; upd(o, e); }; y += 40;
            // щелчки
            L("Эффекты щелчков", x0, y, true); y += 28;
            var clk = Add(new MkCheck("Показывать щелчки", KeyLog.Clicks), x0, y); clk.Changed += (o, e) => { KeyLog.Clicks = clk.Checked; upd(o, e); };
            L("Размер", x0 + 170, y + 3, false);
            var cs = Add(new MkChoice(Pct(Pcts), Near(Pcts, KeyLog.ClickSize), 92), x0 + 222, y - 2); cs.Changed += (o, e) => { KeyLog.ClickSize = Pcts[cs.Index]; upd(o, e); };
            L("Левая", x0 + 326, y + 3, false);
            var cl = Add(new MkSwatch(KeyLog.ClickL), x0 + 368, y - 2); cl.Changed += (o, e) => { KeyLog.ClickL = cl.Value; upd(o, e); };
            L("Правая", x0 + 404, y + 3, false);
            var cr = Add(new MkSwatch(KeyLog.ClickR), x0 + 452, y - 2); cr.Changed += (o, e) => { KeyLog.ClickR = cr.Value; upd(o, e); }; y += 46;
            // подсветка
            L("Подсветка курсора", x0, y, true); y += 28;
            var hl = Add(new MkCheck("Подсвечивать курсор", KeyLog.Halo), x0, y); hl.Changed += (o, e) => { KeyLog.Halo = hl.Checked; upd(o, e); };
            L("Размер", x0 + 170, y + 3, false);
            var hs = Add(new MkChoice(Pct(Pcts), Near(Pcts, KeyLog.HaloSize), 92), x0 + 222, y - 2); hs.Changed += (o, e) => { KeyLog.HaloSize = Pcts[hs.Index]; upd(o, e); };
            L("Цвет", x0 + 326, y + 3, false);
            var hc = Add(new MkSwatch(KeyLog.HaloColor), x0 + 368, y - 2); hc.Changed += (o, e) => { KeyLog.HaloColor = hc.Value; upd(o, e); };
            y += 36;
            L("Непрозрачность в центре", x0 + 24, y + 3, false);
            var ho = Add(new MkChoice(Pct(Ops), Near(Ops, KeyLog.HaloOp), 92), x0 + 222, y - 2); ho.Changed += (o, e) => { KeyLog.HaloOp = Ops[ho.Index]; upd(o, e); };
            y += 44;
            // клавиатура
            L("Клавиатура", x0, y, true); y += 28;
            var kb = Add(new MkCheck("Показывать нажатия клавиш", KeyLog.Keys), x0, y); kb.Changed += (o, e) => { KeyLog.Keys = kb.Checked; upd(o, e); };
            y += 34;
            L("Что показывать", x0 + 24, y + 3, false);
            var km = Add(new MkChoice(new[] { "Только сочетания и служебные", "Все нажатия" }, KeyLog.KeyAll ? 1 : 0, 250), x0 + 150, y - 2); km.Changed += (o, e) => { KeyLog.KeyAll = km.Index == 1; upd(o, e); };
            y += 36;
            L("Размер текста", x0 + 24, y + 3, false);
            var ks = Add(new MkChoice(new[] { "Мелкий", "Средний", "Крупный" }, KeyLog.KeySize, 130), x0 + 150, y - 2); ks.Changed += (o, e) => { KeyLog.KeySize = ks.Index; upd(o, e); };
            L("Позиция", x0 + 300, y + 3, false);
            var kp = Add(new MkPos(KeyLog.KeyPos), x0 + 362, y - 10); kp.Changed += (o, e) => { KeyLog.KeyPos = kp.Value; upd(o, e); };
            // предпросмотр справа
            int px = 520; L("Так будет в ролике", px, HeadH + 14, true);
            prev.Location = new Point(px, HeadH + 42); prev.Size = new Size(ClientSize.Width - px - 18, 220); Controls.Add(prev);
            var hint = L("Курсор, щелчки и клавиши рисуются только в ролике\nпосле «Стоп» — на экране во время записи их нет.\nЗапись клавиш: только пока идёт запись.", px, HeadH + 272, false);
            hint.MaximumSize = new Size(ClientSize.Width - px - 12, 0);
            // кнопки
            var ok = Ui19.Btn("Сохранить", true); var no = Ui19.Btn("Отмена", false);
            no.Location = new Point(ClientSize.Width - no.Width - 14, ClientSize.Height - no.Height - 12);
            ok.Location = new Point(no.Left - ok.Width - 6, no.Top);
            ok.Click += (o, e) => { KeyLog.SaveCfg(cfg); Engine.DrawMouse = KeyLog.Cursor; DialogResult = DialogResult.OK; Close(); };
            no.Click += (o, e) => { KeyLog.LoadCfg(cfg); Close(); };
            Controls.Add(ok); Controls.Add(no);
            FormClosed += (o, e) => { if (DialogResult != DialogResult.OK) KeyLog.LoadCfg(cfg); };
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys k) { if (k == System.Windows.Forms.Keys.Escape) { Close(); return true; } return base.ProcessCmdKey(ref msg, k); }
    }

    // v19.4: ролик с диска в «Обрезать» и пункт в меню Проводника (HKCU, без прав администратора)
    static class TrimFiles
    {
        static readonly string[] Ext = { ".mp4", ".mkv", ".mov", ".avi", ".webm" };
        const string Key = @"Software\Classes\SystemFileAssociations\{0}\shell\XYZTrim";
        const string Prog = "XYZRecord.Trim";
        public static void Launch(string f)
        {
            try { Process.Start(new ProcessStartInfo(Application.ExecutablePath, "--trim \"" + f + "\"") { UseShellExecute = false, WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath) }); }
            catch (Exception ex) { MessageBox.Show("Не удалось открыть редактор: " + ex.Message, "XYZ Запись"); }
        }
        public static bool IsVideo(string f) { try { return Array.IndexOf(Ext, Path.GetExtension(f).ToLower()) >= 0; } catch { return false; } }
        public static bool ShellOn()
        {
            try { using (var k = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(string.Format(Key, ".mp4") + "\\command")) return k != null; } catch { return false; }
        }
        public static string SetShell(bool on)
        {
            try
            {
                string exe = Application.ExecutablePath;
                foreach (var e in Ext)
                {
                    string kp = string.Format(Key, e);
                    if (!on)
                    {
                        Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(kp, false);
                        try { using (var ow = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Classes\" + e + @"\OpenWithProgids", true)) if (ow != null) ow.DeleteValue(Prog, false); } catch { }
                        continue;
                    }
                    // v19.5: ещё и «Открыть с помощью → XYZ Запись — обрезать» (видно в первом меню Windows 11)
                    using (var ow = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + e + @"\OpenWithProgids")) ow.SetValue(Prog, new byte[0], Microsoft.Win32.RegistryValueKind.None);
                    using (var k = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(kp))
                    {
                        k.SetValue("", "Обрезать в XYZ Запись"); k.SetValue("Icon", "\"" + exe + "\",0");
                        using (var c = k.CreateSubKey("command")) c.SetValue("", "\"" + exe + "\" --trim \"%1\"");
                    }
                }
                if (on)
                    using (var pk = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Classes\" + Prog))
                    {
                        pk.SetValue("", "Видео — обрезать в XYZ Запись");
                        using (var di = pk.CreateSubKey("DefaultIcon")) di.SetValue("", "\"" + exe + "\",0");
                        using (var op = pk.CreateSubKey(@"shell\open")) op.SetValue("FriendlyAppName", "XYZ Запись — обрезать");
                        using (var c = pk.CreateSubKey(@"shell\open\command")) c.SetValue("", "\"" + exe + "\" --trim \"%1\"");
                    }
                else Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\" + Prog, false);
                try { Native.SHChangeNotify(0x08000000, 0, IntPtr.Zero, IntPtr.Zero); } catch { }
                return null;
            }
            catch (Exception ex) { return "Не получилось изменить меню Проводника: " + ex.Message; }
        }
    }

    // v19: мини-редактор «как в Audition» — кадры ролика + звуковая дорожка; выделение мышью, пробел — прослушать, Delete — вырезать
    class TrimForm : XyzForm, IMessageFilter
    {
        readonly string file; double dur; string ff;
        float[] peaks = new float[0]; const int SR = 2000; short[] pcm = new short[0];
        Bitmap thumbs; WavePanel wave; VideoPanel video; Label info; Control bSave; Control topBar;
        // v19.3: окно видео — кадры в памяти (JPEG), кадр под курсором / при прослушивании
        readonly List<byte[]> frames = new List<byte[]>(); double fps = 10; public volatile bool FramesDone; Bitmap curBmp; int curIdx = -1;
        public double SelA = -1, SelB = -1, Head = 0;
        public List<double[]> Cuts = new List<double[]>();
        System.Media.SoundPlayer player; System.Windows.Forms.Timer tick = new System.Windows.Forms.Timer();
        Stopwatch playSw = new Stopwatch(); double playFrom, playTo; bool playing; string tmpDir;
        public TrimForm(string path)
        {
            file = path; ff = Engine.FindFfmpeg();
            Text = "Обрезать — XYZ Запись · " + Path.GetFileName(path); Caption = "Обрезать  ·  " + Path.GetFileName(path);
            StartPosition = FormStartPosition.CenterScreen; KeyPreview = true; Size = new Size(1100, 860); MinimumSize = new Size(700, 520);
            tmpDir = Path.Combine(Path.GetTempPath(), "xyz_trim_" + Guid.NewGuid().ToString("N").Substring(0, 8)); Directory.CreateDirectory(tmpDir);
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(2, 2, 2, 4), WrapContents = false, BackColor = Color.Transparent };
            var bPlay = Ui19.Btn("▶ Прослушать (пробел)", false); bPlay.Click += (o, e) => TogglePlay();
            var bCut = Ui19.Btn("✂ Вырезать (Delete)", false); bCut.Click += (o, e) => CutSel();
            var bUndo = Ui19.Btn("↶ Отменить (Ctrl+Z)", false); bUndo.Click += (o, e) => Undo();
            bSave = Ui19.Btn("Сохранить копию", true); bSave.Click += (o, e) => Save();
            var bOpen = Ui19.Btn("Открыть ролик", false); bOpen.Click += (o, e) => { try { Process.Start(file); } catch { } };
            top.Controls.AddRange(new Control[] { bPlay, bCut, bUndo, bSave, bOpen }); topBar = top;
            info = new Label { Dock = DockStyle.Bottom, Height = 30, ForeColor = Theme.Mute, Padding = new Padding(6, 8, 6, 0), BackColor = Color.Transparent, Font = Theme.Px(Theme.UiFam, 12.5f, FontStyle.Regular),
                Text = "Выделите кусок мышью — края прилипают к паузам (Alt — без прилипания), края выделения можно двигать. Пробел — слушать, Delete — вырезать, Ctrl+Z — вернуть." };
            wave = new WavePanel(this) { Dock = DockStyle.Fill };
            video = new VideoPanel(this) { Dock = DockStyle.Top, Height = 560 };
            Controls.Add(wave); Controls.Add(video); Controls.Add(top); Controls.Add(info);
            Resize += (o, e) => LayoutVideo(); Load += (o, e) => LayoutVideo();
            KeyDown += OnKey;
            tick.Interval = 30; tick.Tick += (o, e) => { if (playing) { double et = playFrom + playSw.Elapsed.TotalSeconds; Head = ToSrc(Math.Min(et, playTo)); if (et >= playTo) StopPlay(); wave.Invalidate(); } };
            Shown += (o, e) => { Application.AddMessageFilter(this); }; FormClosed += (o, e) => { Application.RemoveMessageFilter(this); };
            Shown += (o, e) => { Cursor = Cursors.WaitCursor; var th = new Thread(Load2); th.IsBackground = true; th.Start(); var tf = new Thread(LoadFrames); tf.IsBackground = true; tf.Start(); };
            FormClosed += (o, e) => { StopPlay(); try { Directory.Delete(tmpDir, true); } catch { } };
        }
        void LayoutVideo()
        {
            if (video == null) return;
            int avail = ClientSize.Height - (topBar != null ? topBar.Height : 40) - (info != null ? info.Height : 30) - 60;
            video.Height = Math.Max(120, avail - Math.Max(170, (int)(avail * 0.28)));
        }
        void LoadFrames()
        {
            try
            {
                double d = 0; for (int i = 0; i < 100 && (d = dur) <= 0 && !Ready; i++) Thread.Sleep(50);
                if (d <= 0) d = Duration(file);
                fps = d > 0 ? Math.Max(1, Math.Min(12, 2400 / d)) : 10;
                var inv = System.Globalization.CultureInfo.InvariantCulture;
                var psi = new ProcessStartInfo(ff, "-hide_banner -v error -i \"" + file + "\" -an -vf \"fps=" + fps.ToString("0.###", inv) + ",scale='min(960,iw)':-2\" -q:v 6 -f image2pipe -vcodec mjpeg -") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true };
                using (var p = Process.Start(psi))
                {
                    var st = p.StandardOutput.BaseStream; var buf = new MemoryStream(); var rd = new byte[65536]; int n; int prev = -1;
                    while ((n = st.Read(rd, 0, rd.Length)) > 0)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            byte c = rd[i]; buf.WriteByte(c);
                            if (prev == 0xFF && c == 0xD9) { var jb = buf.ToArray(); buf.SetLength(0); lock (frames) frames.Add(jb); prev = -1; continue; }
                            prev = c;
                        }
                        if (frames.Count % 10 == 0 && video != null) try { video.BeginInvoke(new Action(() => video.Invalidate())); } catch { }
                    }
                    p.WaitForExit(5000);
                }
            }
            catch { }
            FramesDone = true;
            try { video.BeginInvoke(new Action(() => video.Invalidate())); } catch { }
        }
        public int FramesCount { get { lock (frames) return frames.Count; } }
        public double FramesExpected { get { return dur * fps; } }
        public Bitmap FrameAt(double t)
        {
            int i; byte[] jb;
            lock (frames)
            {
                if (frames.Count == 0) return null;
                i = Math.Max(0, Math.Min(frames.Count - 1, (int)Math.Floor(t * fps)));
                if (i == curIdx && curBmp != null) return curBmp;
                jb = frames[i];
            }
            try { using (var ms = new MemoryStream(jb)) { var b = new Bitmap(Image.FromStream(ms)); if (curBmp != null) curBmp.Dispose(); curBmp = b; curIdx = i; } } catch { }
            return curBmp;
        }
        public void VideoInvalidate() { if (video != null) video.Invalidate(); }
        public double Dur { get { return dur; } }
        public volatile bool Ready;
        public float[] Peaks { get { return peaks; } }
        public Bitmap Thumbs { get { return thumbs; } }
        void Load2()
        {
            try
            {
                dur = Duration(file);
                var psi = new ProcessStartInfo(ff, "-hide_banner -v error -i \"" + file + "\" -vn -ac 1 -ar " + SR + " -f s16le -") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true };
                using (var p = Process.Start(psi))
                using (var ms = new MemoryStream())
                {
                    p.StandardOutput.BaseStream.CopyTo(ms); p.WaitForExit(60000);
                    var b = ms.ToArray(); pcm = new short[b.Length / 2];
                    Buffer.BlockCopy(b, 0, pcm, 0, pcm.Length * 2);
                }
                if (dur <= 0) dur = pcm.Length / (double)SR;
                { int mx = 1; for (int i = 0; i < pcm.Length; i++) { int v = Math.Abs((int)pcm[i]); if (v > mx) mx = v; } MaxPeak = mx / 32768f; }
                int n = 0; string tp = Path.Combine(tmpDir, "thumbs.png");
                var p2 = new ProcessStartInfo(ff, "-hide_banner -v error -y -i \"" + file + "\" -vf \"fps=" + (n / Math.Max(0.5, dur)).ToString("0.######", System.Globalization.CultureInfo.InvariantCulture) + ",scale=-2:72,tile=" + n + "x1\" -frames:v 1 \"" + tp + "\"") { UseShellExecute = false, CreateNoWindow = true };
                if (n > 0) using (var p = Process.Start(p2)) p.WaitForExit(60000);
                if (File.Exists(tp)) using (var fs = new FileStream(tp, FileMode.Open, FileAccess.Read)) thumbs = new Bitmap(Image.FromStream(fs));
            }
            catch { }
            Ready = true;
            try { BeginInvoke(new Action(() => { Cursor = Cursors.Default; wave.Invalidate(); UpdInfo(); })); } catch { }
        }
        public float MaxPeak = 1f;   // v19.5: дорожка подгоняется по самому громкому месту
        // v19.5: колесо над дорожкой — Ctrl+колесо: громче/тише (высота волны); колесо: масштаб по времени; Shift+колесо: прокрутка
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != 0x020A || wave == null || !wave.IsHandleCreated || !Visible) return false;
            var pt = wave.PointToClient(Cursor.Position);
            if (!wave.ClientRectangle.Contains(pt)) return false;
            int delta = (short)((long)m.WParam >> 16);
            wave.Wheel(delta, pt.X, ModifierKeys);
            return true;
        }
        public float PeakAt(double t0, double t1)
        {
            if (pcm.Length == 0) return 0;
            int a = Math.Max(0, (int)(t0 * SR)), b = Math.Min(pcm.Length, Math.Max(a + 1, (int)(t1 * SR)));
            int m = 0; for (int i = a; i < b; i++) { int v = Math.Abs((int)pcm[i]); if (v > m) m = v; }
            return m / 32768f;
        }
        void OnKey(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) { TogglePlay(); e.Handled = true; e.SuppressKeyPress = true; }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) { CutSel(); e.Handled = true; }
            else if (e.Control && e.KeyCode == Keys.Z) { Undo(); e.Handled = true; }
            else if (e.KeyCode == Keys.Escape) { SelA = SelB = -1; wave.Invalidate(); UpdInfo(); }
        }
        bool HasSel { get { return SelA >= 0 && SelB > SelA + 0.05; } }
        void CutSel()
        {
            if (!HasSel) { info.ForeColor = Theme.Amber; info.Text = "Сначала выделите кусок мышью на дорожке."; return; }
            StopPlay(); Cuts.Add(new[] { SelA, SelB }); Head = SelB; SelA = SelB = -1; wave.Invalidate(); UpdInfo();
        }
        void Undo() { if (Cuts.Count > 0) { StopPlay(); Cuts.RemoveAt(Cuts.Count - 1); SelA = SelB = -1; wave.Invalidate(); UpdInfo(); } }
        public bool IsCut(double t) { foreach (var c in Cuts) if (t >= c[0] && t < c[1]) return true; return false; }
        List<double[]> Keep()
        {
            var cs = new List<double[]>(Cuts); cs.Sort((x, y) => x[0].CompareTo(y[0]));
            var res = new List<double[]>(); double cur = 0;
            foreach (var c in cs) { if (c[0] > cur + 0.02) res.Add(new[] { cur, c[0] }); cur = Math.Max(cur, c[1]); }
            if (dur > cur + 0.02) res.Add(new[] { cur, dur });
            return res;
        }
        // v19.2: дорожка «смыкается» после выреза (как Ripple Delete) — время на экране = время ролика после обрезки
        public double EdDur { get { double s = 0; foreach (var k in Keep()) s += k[1] - k[0]; return s; } }
        public double ToEd(double t)
        {
            double e = 0;
            foreach (var k in Keep()) { if (t >= k[1]) e += k[1] - k[0]; else { if (t > k[0]) e += t - k[0]; break; } }
            return e;
        }
        public double ToSrc(double e)
        {
            var ks = Keep(); if (ks.Count == 0) return 0;
            foreach (var k in ks) { double len = k[1] - k[0]; if (e <= len + 1e-9) return k[0] + Math.Max(0, e); e -= len; }
            return ks[ks.Count - 1][1];
        }
        public void UpdInfo()
        {
            double kept = 0; foreach (var k in Keep()) kept += k[1] - k[0];
            info.ForeColor = Theme.Mute;
            info.Text = (HasSel ? "Выделено " + Ui19.Fmt(ToEd(SelA)) + " – " + Ui19.Fmt(ToEd(SelB)) + " (" + Ui19.Fmt(ToEd(SelB) - ToEd(SelA)) + ")   ·   " : "")
                + "Вырезано кусков: " + Cuts.Count + "   ·   длина после: " + Ui19.Fmt(kept) + " из " + Ui19.Fmt(dur)
                + "      Delete — вырезать и сомкнуть · Ctrl+Z — вернуть · пробел — слушать · Ctrl+колесо — растянуть, колесо или полоса внизу — прокрутка, Ctrl+Shift+колесо — высота волны, выделение можно тянуть за середину";
        }
        void TogglePlay()
        {
            if (playing) { StopPlay(); return; }
            // v19.2: время на экране — после обрезки; вырезанное не звучит
            double ed = EdDur; if (ed <= 0.05) return;
            // v19.7: без выделения — играть от курсора до конца ролика (раньше обрывалось через 8 с)
            double ea = HasSel ? ToEd(SelA) : ToEd(Head), eb = HasSel ? ToEd(SelB) : ed;
            if (eb <= ea + 0.05) { ea = 0; eb = ed; }
            string w = Path.Combine(tmpDir, "play.wav");
            try
            {
                var inv = System.Globalization.CultureInfo.InvariantCulture;
                double sa = ToSrc(ea), sb = ToSrc(eb);
                var expr = new StringBuilder();
                foreach (var k in Keep())
                {
                    double x0 = Math.Max(k[0], sa), x1 = Math.Min(k[1], sb);
                    if (x1 <= x0 + 0.005) continue;
                    if (expr.Length > 0) expr.Append('+');
                    expr.Append("between(t," + x0.ToString("0.###", inv) + "," + x1.ToString("0.###", inv) + ")");
                }
                if (expr.Length == 0) return;
                try { if (File.Exists(w)) File.Delete(w); } catch { }
                var psi = new ProcessStartInfo(ff, "-hide_banner -v error -y -i \"" + file + "\" -vn -af \"aselect='" + expr + "',asetpts=N/SR/TB\" -ac 2 -ar 44100 \"" + w + "\"") { UseShellExecute = false, CreateNoWindow = true };
                using (var p = Process.Start(psi)) p.WaitForExit(20000);
                player = new System.Media.SoundPlayer(w); player.Load(); player.Play();
                playFrom = ea; playTo = eb; Head = sa; playing = true; playSw.Restart(); tick.Start();
            }
            catch { playing = false; }
        }
        void StopPlay() { playing = false; tick.Stop(); try { if (player != null) { player.Stop(); player.Dispose(); player = null; } } catch { } if (wave != null) wave.Invalidate(); }
        void Save()
        {
            var keep = Keep();
            if (Cuts.Count == 0) { info.ForeColor = Theme.Amber; info.Text = "Ничего не вырезано — выделите кусок и нажмите Delete."; return; }
            if (keep.Count == 0) { info.ForeColor = Theme.Red; info.Text = "Вырезано всё — нечего сохранять."; return; }
            var inv = System.Globalization.CultureInfo.InvariantCulture;
            var expr = new StringBuilder();
            foreach (var k in keep) { if (expr.Length > 0) expr.Append('+'); expr.Append("between(t," + k[0].ToString("0.###", inv) + "," + k[1].ToString("0.###", inv) + ")"); }
            string outF = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + " обрезка.mp4");
            for (int i = 2; File.Exists(outF); i++) outF = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + " обрезка " + i + ".mp4");
            string fs = "[0:v]select='" + expr + "',setpts=N/FRAME_RATE/TB[v];[0:a]aselect='" + expr + "',asetpts=N/SR/TB[a]";
            string args = "-hide_banner -y -i \"" + file + "\" -filter_complex \"" + fs + "\" -map \"[v]\" -map \"[a]\" -c:v libx264 -preset veryfast -crf 20 -c:a aac -b:a 160k -movflags +faststart \"" + outF + "\"";
            bool hasAudio = pcm.Length > 0;
            if (!hasAudio) args = "-hide_banner -y -i \"" + file + "\" -vf \"select='" + expr + "',setpts=N/FRAME_RATE/TB\" -c:v libx264 -preset veryfast -crf 20 -movflags +faststart \"" + outF + "\"";
            bSave.Enabled = false; info.ForeColor = Theme.Mute; info.Text = "Сохраняю копию…"; Cursor = Cursors.WaitCursor;
            var th = new Thread(() =>
            {
                bool ok = false;
                try
                {
                    var psi = new ProcessStartInfo(ff, args) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardError = true };
                    using (var p = Process.Start(psi)) { p.StandardError.ReadToEnd(); p.WaitForExit(); ok = p.ExitCode == 0 && File.Exists(outF); }
                }
                catch { }
                try
                {
                    BeginInvoke(new Action(() =>
                    {
                        bSave.Enabled = true; Cursor = Cursors.Default;
                        if (ok) { info.ForeColor = Theme.Teal; info.Text = "Готово: " + Path.GetFileName(outF) + " — рядом с оригиналом."; try { Process.Start("explorer.exe", "/select,\"" + outF + "\""); } catch { } }
                        else { info.ForeColor = Theme.Red; info.Text = "Не получилось сохранить."; }
                    }));
                }
                catch { }
            });
            th.IsBackground = true; th.Start();
        }
        public static double Duration(string f)
        {
            try
            {
                string ff = Engine.FindFfmpeg(); if (ff == null) return 0;
                var psi = new ProcessStartInfo(ff, "-hide_banner -i \"" + f + "\"") { UseShellExecute = false, CreateNoWindow = true, RedirectStandardError = true, StandardErrorEncoding = Encoding.UTF8 };
                using (var p = Process.Start(psi))
                {
                    string err = p.StandardError.ReadToEnd(); p.WaitForExit(5000);
                    var m = System.Text.RegularExpressions.Regex.Match(err, @"Duration:\s*(\d+):(\d+):(\d+(?:\.\d+)?)");
                    if (m.Success) return int.Parse(m.Groups[1].Value) * 3600 + int.Parse(m.Groups[2].Value) * 60 + double.Parse(m.Groups[3].Value, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch { }
            return 0;
        }
    }

    // v19.3: окно видео над дорожкой — кадр в точке курсора (во время прослушивания — бегущий)
    class VideoPanel : Panel
    {
        readonly TrimForm ed;
        public VideoPanel(TrimForm e) { ed = e; DoubleBuffered = true; ResizeRedraw = true; BackColor = Color.Transparent; SetStyle(ControlStyles.Selectable, false); }
        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics; const int M = 14;
            var r = new Rectangle(M, 8, Math.Max(10, Width - 2 * M), Math.Max(10, Height - 12));
            Ui19.Screen(g, new RectangleF(r.X - 3, r.Y - 3, r.Width + 6, r.Height + 6), 10);
            using (var bk = new SolidBrush(Color.FromArgb(8, 10, 12))) g.FillRectangle(bk, r);
            var bmp = ed.FrameAt(ed.Head);
            if (bmp != null)
            {
                float k = Math.Min(r.Width / (float)bmp.Width, r.Height / (float)bmp.Height);
                float w = bmp.Width * k, h = bmp.Height * k;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.DrawImage(bmp, r.X + (r.Width - w) / 2, r.Y + (r.Height - h) / 2, w, h);
            }
            string tt = ed.Dur > 0 ? Ui19.Fmt(ed.ToEd(ed.Head)) : "";
            if (!ed.FramesDone)
            {
                double ex = ed.FramesExpected; int c = ed.FramesCount;
                tt += (tt.Length > 0 ? "   ·   " : "") + "загружаю кадры" + (ex > 1 ? " " + Math.Min(99, (int)(c * 100 / ex)) + "%" : "…");
            }
            if (tt.Length > 0)
            {
                var f = Theme.Px(Theme.DigitFam, 13f, FontStyle.Regular);
                var sz = TextRenderer.MeasureText(tt, f);
                var rb = new Rectangle(r.X + 10, r.Bottom - sz.Height - 14, sz.Width + 14, sz.Height + 6);
                using (var b = new SolidBrush(Color.FromArgb(170, 0, 0, 0))) g.FillRectangle(b, rb);
                TextRenderer.DrawText(g, tt, f, new Point(rb.X + 7, rb.Y + 3), Theme.TealHi);
            }
        }
    }

    class WavePanel : Panel
    {
        readonly TrimForm ed; bool drag; double dragT0; int edge;   // edge: 0 — новое выделение, 1 — тянем начало, 2 — тянем конец
        public WavePanel(TrimForm e)
        {
            ed = e; DoubleBuffered = true; ResizeRedraw = true; BackColor = Color.Transparent; Cursor = Cursors.IBeam;
            SetStyle(ControlStyles.Selectable, false);
        }
        const int M = 14, TH = 72;
        // v19.5: окно просмотра по времени (vOff, vSpan — время после обрезки) и усиление волны
        double vOff = 0, vSpan = -1; float gain = 1f;
        double Span { get { double D = ed.EdDur; return vSpan <= 0 || vSpan > D ? D : vSpan; } }
        double Off { get { double D = ed.EdDur, sp = Span; return Math.Max(0, Math.Min(vOff, D - sp)); } }
        public void Wheel(int delta, int x, Keys mods)
        {
            double D = ed.EdDur; if (D <= 0) return;
            // v19.6: Ctrl+колесо — растянуть по времени; Ctrl+Shift+колесо — высота волны; колесо — прокрутка
            if ((mods & Keys.Control) == Keys.Control && (mods & Keys.Shift) == Keys.Shift) { gain = Math.Max(0.5f, Math.Min(30f, gain * (delta > 0 ? 1.25f : 0.8f))); }
            else if ((mods & Keys.Control) != Keys.Control) { vOff = Off - Math.Sign(delta) * Span * 0.15; vOff = Math.Max(0, Math.Min(vOff, D - Span)); }
            else
            {
                double w = Math.Max(1, Width - 2 * M), sp = Span, off = Off;
                double at = off + (x - M) / w * sp;
                double ns = Math.Max(0.5, Math.Min(D, sp * (delta > 0 ? 0.8 : 1.25)));
                vSpan = ns >= D ? -1 : ns;
                vOff = Math.Max(0, Math.Min(D - ns, at - (x - M) / w * ns));
            }
            Invalidate();
        }
        // v19.2: ось — время после обрезки (вырезанное сомкнуто); Cuts/Sel/Head хранятся во времени исходника
        double T(int x) { double w = Math.Max(1, Width - 2 * M), D = ed.EdDur; return ed.ToSrc(Math.Max(0, Math.Min(D, Off + (x - M) / w * Span))); }
        float X(double t) { double sp = Span; return (float)(M + (sp <= 0 ? 0 : (ed.ToEd(t) - Off) / sp) * (Width - 2 * M)); }
        // v19.6: полоса прокрутки под шкалой времени
        RectangleF ScrollR { get { return new RectangleF(M, Height - 13, Math.Max(10, Width - 2 * M), 9); } }
        RectangleF ThumbR { get { var r = ScrollR; double D = ed.EdDur; if (D <= 0) return r; return new RectangleF(r.X + (float)(Off / D * r.Width), r.Y, Math.Max(24f, (float)(Span / D * r.Width)), r.Height); } }
        bool scrollDrag; float scrollGrab; double moveT0, moveA, moveB;
        int EdgeAt(int x)
        {
            if (ed.SelA < 0 || ed.SelB <= ed.SelA) return 0;
            if (Math.Abs(x - X(ed.SelA)) <= 6) return 1;
            if (Math.Abs(x - X(ed.SelB)) <= 6) return 2;
            return 0;
        }
        // прилипание: к тишине рядом (пауза между словами), к краям вырезанных кусков; Alt — без прилипания
        double Snap(double t)
        {
            if ((ModifierKeys & Keys.Alt) == Keys.Alt || ed.Dur <= 0) return t;
            double px = Span / Math.Max(1, Width - 2 * M);
            foreach (var c in ed.Cuts) { if (Math.Abs(t - c[0]) < 6 * px) return c[0]; if (Math.Abs(t - c[1]) < 6 * px) return c[1]; }
            double win = Math.Max(0.12, 10 * px), best = t; float bestV = float.MaxValue, loud = 0.0001f;
            for (double q = t - win; q <= t + win; q += 0.01)
            {
                if (q < 0 || q > ed.Dur || ed.IsCut(q)) continue;
                float v = ed.PeakAt(q - 0.025, q + 0.025); if (v > loud) loud = v;
                float w = v + (float)(Math.Abs(q - t) / win) * 0.02f;
                if (w < bestV) { bestV = w; best = q; }
            }
            return ed.PeakAt(best - 0.025, best + 0.025) < Math.Max(0.02f, loud * 0.15f) ? best : t;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || ed.EdDur <= 0) return;
            if (e.Y >= ScrollR.Y - 3)
            {
                var th = ThumbR; scrollDrag = true;
                if (th.Contains(e.X, th.Y + 1)) scrollGrab = e.X - th.X;
                else { scrollGrab = th.Width / 2; ScrollTo(e.X); }
                return;
            }
            drag = true; edge = EdgeAt(e.X);
            if (edge == 0 && ed.SelA >= 0 && ed.SelB > ed.SelA && e.X > X(ed.SelA) + 6 && e.X < X(ed.SelB) - 6)
            { edge = 3; moveT0 = ed.ToEd(T(e.X)); moveA = ed.ToEd(ed.SelA); moveB = ed.ToEd(ed.SelB); Cursor = Cursors.SizeAll; return; }   // v19.6: тянуть выделение целиком
            if (edge == 0) { dragT0 = Snap(T(e.X)); ed.Head = dragT0; ed.SelA = ed.SelB = -1; }
            Invalidate(); ed.UpdInfo();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (scrollDrag) { ScrollTo(e.X - scrollGrab + ThumbR.Width / 2); return; }
            if (!drag)
            {
                if (e.Y >= ScrollR.Y - 3) { Cursor = Cursors.Hand; return; }
                bool inSel = ed.SelA >= 0 && ed.SelB > ed.SelA && e.X > X(ed.SelA) + 6 && e.X < X(ed.SelB) - 6;
                Cursor = EdgeAt(e.X) != 0 ? Cursors.SizeWE : inSel ? Cursors.SizeAll : Cursors.IBeam; return;
            }
            double t = Snap(T(e.X));
            if (edge == 3)
            {
                double D = ed.EdDur, len = moveB - moveA, na = Math.Max(0, Math.Min(D - len, moveA + ed.ToEd(T(e.X)) - moveT0));
                ed.SelA = ed.ToSrc(na); ed.SelB = ed.ToSrc(na + len); ed.Head = ed.SelA; Invalidate(); ed.UpdInfo(); return;
            }
            if (edge == 1) { ed.SelA = Math.Min(t, ed.SelB - 0.05); }
            else if (edge == 2) { ed.SelB = Math.Max(t, ed.SelA + 0.05); }
            else { ed.SelA = Math.Min(dragT0, t); ed.SelB = Math.Max(dragT0, t); }
            ed.Head = t;   // v19.3: кадр в окне видео идёт за мышью
            Invalidate(); ed.UpdInfo();
        }
        protected override void OnMouseUp(MouseEventArgs e) { drag = false; scrollDrag = false; }
        void ScrollTo(float cx)
        {
            var r = ScrollR; double D = ed.EdDur; if (D <= 0 || vSpan <= 0) return;
            double c = (cx - r.X) / r.Width * D;
            vOff = Math.Max(0, Math.Min(D - Span, c - Span / 2)); Invalidate();
        }
        protected override void OnInvalidated(InvalidateEventArgs e) { base.OnInvalidated(e); ed.VideoInvalidate(); }
        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics; g.SmoothingMode = SmoothingMode.None;
            int w = Width - 2 * M; if (w < 10) return;
            // v19: без полосы кадров (замечание db 01:10 — это копия экрана, места занимает много, пользы мало)
            var rWv = new Rectangle(M, 10, w, Math.Max(40, Height - 54));
            var rTh = new Rectangle(M, rWv.Y, w, 0);
            Ui19.Screen(g, new RectangleF(rWv.X - 3, rWv.Y - 3, rWv.Width + 6, rWv.Height + 6), 10);
            g.SmoothingMode = SmoothingMode.None;
            using (var pen = new Pen(Theme.A(Theme.Teal, 0.12f))) g.DrawLine(pen, rWv.X, rWv.Y + rWv.Height / 2, rWv.Right, rWv.Y + rWv.Height / 2);
            if (ed.Dur <= 0) { TextRenderer.DrawText(g, "Загружаю дорожку…", Font, rWv, Theme.Mute); return; }
            var digF = Theme.Px(Theme.DigitFam, 12f, FontStyle.Regular);
            int mid = rWv.Y + rWv.Height / 2;
            double D = ed.EdDur, sp0 = Span, of0 = Off;
            float norm = gain / Math.Max(0.02f, ed.MaxPeak) * 0.9f;
            g.SetClip(rWv);
            using (var pk = new Pen(Theme.Teal))
                for (int x = 0; x < w; x++)
                {
                    double e0 = of0 + x / (double)w * sp0, e1 = of0 + (x + 1) / (double)w * sp0, s0 = ed.ToSrc(e0);
                    float a = Math.Min(1f, ed.PeakAt(s0, s0 + (e1 - e0)) * norm); int h = (int)(a * (rWv.Height / 2 - 4));
                    g.DrawLine(pk, M + x, mid - h, M + x, mid + h);
                }
            // стыки на месте вырезанного: красная черта с треугольниками
            using (var pr = new Pen(Theme.Red, 2f)) using (var br = new SolidBrush(Theme.Red))
                foreach (var c in ed.Cuts)
                {
                    float xs = X(c[0]);
                    g.DrawLine(pr, xs, rWv.Y, xs, rWv.Bottom);
                    g.FillPolygon(br, new[] { new PointF(xs - 5, rWv.Y), new PointF(xs + 5, rWv.Y), new PointF(xs, rWv.Y + 7) });
                    g.FillPolygon(br, new[] { new PointF(xs - 5, rWv.Bottom), new PointF(xs + 5, rWv.Bottom), new PointF(xs, rWv.Bottom - 7) });
                }
            if (ed.SelA >= 0 && ed.SelB > ed.SelA)
            {
                float x0 = X(ed.SelA), x1 = X(ed.SelB);
                using (var b = new SolidBrush(Theme.A(Theme.TealHi, 0.22f))) g.FillRectangle(b, x0, rTh.Y, x1 - x0, rWv.Bottom - rTh.Y);
                using (var p = new Pen(Theme.TealHi)) { g.DrawLine(p, x0, rTh.Y, x0, rWv.Bottom); g.DrawLine(p, x1, rTh.Y, x1, rWv.Bottom); }
            }
            g.ResetClip();
            Ui19.Gloss(g, rWv, 10);
            {   // полоса прокрутки: вся длина ролика, светлая часть — видимый кусок
                var sr = ScrollR; var th = ThumbR;
                using (var b = new SolidBrush(Theme.A(Theme.Mute, 0.18f))) g.FillRectangle(b, sr);
                using (var b = new SolidBrush(Theme.A(Theme.Teal, vSpan > 0 ? 0.75f : 0.3f))) g.FillRectangle(b, th);
                using (var pr = new Pen(Theme.Red, 1f)) foreach (var c in ed.Cuts) { float xs = sr.X + (float)(ed.ToEd(c[0]) / Math.Max(1e-6, D) * sr.Width); g.DrawLine(pr, xs, sr.Y, xs, sr.Bottom); }
            }
            if (vSpan > 0 || Math.Abs(gain - 1f) > 0.01f)
            {
                string zt = (vSpan > 0 ? "×" + (D / Span).ToString("0.#") + " по времени" : "") + (Math.Abs(gain - 1f) > 0.01f ? (vSpan > 0 ? "  ·  " : "") + "волна ×" + gain.ToString("0.#") : "");
                TextRenderer.DrawText(g, zt, Theme.Px(Theme.UiFam, 11f, FontStyle.Regular), new Point(rWv.Right - 220, rWv.Y + 6), Theme.A(Theme.Teal, 0.8f));

            }
            using (var p = new Pen(Theme.Amber, 1.5f)) { float hx = X(ed.Head); g.DrawLine(p, hx, rTh.Y - 4, hx, rWv.Bottom + 4); }
            // шкала времени
            double SP = Span, OF = Off;
            double step = SP <= 3 ? 0.25 : SP <= 8 ? 0.5 : SP <= 20 ? 1 : SP <= 60 ? 5 : SP <= 300 ? 15 : 60;
            for (double t = Math.Ceiling(OF / step) * step; t <= OF + SP + 0.001; t += step)
            {
                float x = (float)(M + (SP <= 0 ? 0 : (t - OF) / SP) * w); using (var p = new Pen(Theme.A(Theme.Mute, 0.5f))) g.DrawLine(p, x, rWv.Bottom, x, rWv.Bottom + 5);
                TextRenderer.DrawText(g, step < 1 ? Ui19.Fmt(t) : Ui19.Fmt(t).Substring(0, 5), digF, new Point((int)x + 2, rWv.Bottom + 4), Theme.A(Theme.Teal, 0.7f));
            }
        }
    }

    class AboutForm : Form
    {
        public const string Version = "v19.15";
        public const float W = 500, H = 268;
        public const string UrlTg = "https://t.me/xyz_for_revit", UrlGh = "https://github.com/Budzhak/XYZ", UrlFf = "https://ffmpeg.org";
        float s; public bool Exclude;
        List<KeyValuePair<RectangleF, string>> links = new List<KeyValuePair<RectangleF, string>>();
        RectangleF closeR; public string Hot = ""; bool closeDown;
        public static string BuildDate
        {
            get { try { return File.GetLastWriteTime(Application.ExecutablePath).ToString("dd.MM.yyyy"); } catch { return ""; } }
        }
        public AboutForm(float scale)
        {
            s = scale; FormBorderStyle = FormBorderStyle.None; AutoScaleMode = AutoScaleMode.None; ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual; KeyPreview = true; DoubleBuffered = true; MaximizeBox = false; MinimizeBox = false;
            BackColor = Color.FromArgb(18, 21, 25); Text = "О программе — XYZ Запись";
            ClientSize = new Size((int)Math.Round(W * s), (int)Math.Round(H * s));
            try { using (var st = Res.Raw("app.ico")) if (st != null) Icon = new Icon(st); } catch { }
        }
        protected override void OnHandleCreated(EventArgs e) { base.OnHandleCreated(e); if (Exclude) Native.SetWindowDisplayAffinity(Handle, Native.WDA_EXCLUDEFROMCAPTURE); }
        protected override void OnPaint(PaintEventArgs e) { Draw(e.Graphics); }
        protected override void OnPaintBackground(PaintEventArgs e) { }

        void T(Graphics g, string t, Font f, Color c, float x, float y)
        {
            using (var b = new SolidBrush(c)) g.DrawString(t, f, b, x * s, y * s, StringFormat.GenericTypographic);
        }
        float Wd(Graphics g, string t, Font f) { return g.MeasureString(t, f, 4000, StringFormat.GenericTypographic).Width; }
        void Link(Graphics g, string t, Font f, float x, float y, string url)
        {
            bool hot = Hot == url;
            var c = hot ? Theme.TealHi : Theme.Teal;
            T(g, t, f, c, x, y);
            float w = Wd(g, t, f);
            var r = new RectangleF(x * s, y * s - 2 * s, w, f.Height + 4 * s);
            if (hot) using (var pen = new Pen(c, Math.Max(1f, s))) g.DrawLine(pen, x * s, y * s + f.Height - 1 * s, x * s + w, y * s + f.Height - 1 * s);
            links.Add(new KeyValuePair<RectangleF, string>(r, url));
        }
        public void Draw(Graphics g)
        {
            links.Clear();
            g.SmoothingMode = SmoothingMode.AntiAlias; g.PixelOffsetMode = PixelOffsetMode.HighQuality; g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            var r = new RectangleF(0, 0, W * s, H * s);
            using (var b = new LinearGradientBrush(new RectangleF(0, -1, r.Width, r.Height + 2), Color.FromArgb(28, 32, 38), Color.FromArgb(13, 15, 19), LinearGradientMode.Vertical)) g.FillRectangle(b, r);
            using (var pen = new Pen(Theme.A(Theme.Teal, 0.55f), Math.Max(1f, s))) g.DrawRectangle(pen, 0.5f * s, 0.5f * s, r.Width - 1.5f * s, r.Height - 1.5f * s);
            // эмблема — тот же PNG, только равномерный масштаб (геометрия не меняется)
            float k = 0.74f;
            var em = Res.Get("emblem", s * k);
            if (em != null)
            {
                var src = new Rectangle((int)(38 * s * k), (int)(28 * s * k), (int)(156 * s * k), (int)(160 * s * k));
                g.DrawImage(em, new Rectangle((int)Math.Round(20 * s), (int)Math.Round(34 * s), src.Width, src.Height), src, GraphicsUnit.Pixel);
            }
            var fT = Theme.Px(Theme.UiSemi, 23f * s, FontStyle.Regular);
            var f1 = Theme.Px(Theme.UiFam, 11f * s, FontStyle.Regular);
            var fS = Theme.Px(Theme.UiSemi, 8.4f * s, FontStyle.Regular);
            float x = 156;
            T(g, "XYZ Запись", fT, Theme.Ink, x - 1, 24);
            string bd = BuildDate;
            T(g, "версия " + Version + (bd.Length > 0 ? "  ·  сборка " + bd : ""), f1, Theme.Mute, x, 60);
            using (var pen = new Pen(Theme.A(Theme.Teal, 0.22f), Math.Max(1f, s))) g.DrawLine(pen, x * s, 86 * s, (W - 24) * s, 86 * s);
            T(g, "Бесплатно  ·  лицензия GPL-3.0  ·  без логотипов на видео", f1, Theme.Ink, x, 98);
            T(g, "Автор: Dmitry Budzhak / db / BIM Design", f1, Theme.Mix(Theme.Mute, Theme.Ink, 0.5f), x, 120);
            T(g, "TELEGRAM", fS, Theme.Dim, x, 152); Link(g, "Канал «XYZ для Revit» — t.me/xyz_for_revit", f1, x + 62, 150, UrlTg);
            T(g, "GITHUB", fS, Theme.Dim, x, 174); Link(g, "github.com/Budzhak/XYZ", f1, x + 62, 172, UrlGh);
            T(g, "FFMPEG", fS, Theme.Dim, x, 196);
            string ff = "использует FFmpeg (GPL) — ";
            T(g, ff, f1, Theme.Mute, x + 62, 194); Link(g, "ffmpeg.org", f1, x + 62 + Wd(g, ff, f1) / s + 3, 194, UrlFf);
            // кнопка «Закрыть»
            closeR = new RectangleF((W - 124) * s, (H - 46) * s, 100 * s, 28 * s);
            bool hc = Hot == "close";
            using (var p = Theme.Round(closeR, 14 * s))
            {
                using (var b = new SolidBrush(hc ? Theme.A(Theme.Teal, (closeDown ? 70 : 44) / 255f) : Color.FromArgb(14, 255, 255, 255))) g.FillPath(b, p);
                using (var pen = new Pen(Theme.A(Theme.Teal, hc ? 0.95f : 0.6f), Math.Max(1f, 1.1f * s))) g.DrawPath(pen, p);
            }
            using (var sf = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var b = new SolidBrush(hc ? Color.White : Theme.Ink))
                g.DrawString("Закрыть", Theme.Px(Theme.UiSemi, 11f * s, FontStyle.Regular), b, closeR, sf);
            T(g, "F9 — старт / стоп записи из любого окна", Theme.Px(Theme.UiFam, 9.5f * s, FontStyle.Regular), Theme.Dim, 22, H - 38);
        }
        string HitAt(Point p)
        {
            if (closeR.Contains(p)) return "close";
            foreach (var l in links) if (l.Key.Contains(p)) return l.Value;
            return "";
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            string h = HitAt(e.Location);
            if (h != Hot) { Hot = h; Cursor = h.Length > 0 ? Cursors.Hand : Cursors.Default; Invalidate(); }
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e) { if (Hot.Length > 0) { Hot = ""; Invalidate(); } base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            string h = HitAt(e.Location);
            if (h == "close") { closeDown = true; Invalidate(); return; }
            if (h.Length > 0) { try { Process.Start(h); } catch { } return; }
            Native.DragWindow(this);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (closeDown) { closeDown = false; if (HitAt(e.Location) == "close") Close(); else Invalidate(); }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) Close();
            base.OnKeyDown(e);
        }
    }

    class Anim
    {
        public string Tag;
        public double T0, Dur; public float From, To; public Func<double, double> Ease; public Action<float> Set; public Action Done;
    }

    // ---------- Главное окно-прибор ----------
    class MainForm : LayeredForm
    {
        enum St { Idle, Rec, Paused, Saving }
        St state = St.Idle;
        Settings cfg; Engine eng = new Engine();
        string ffmpeg;
        float s;
        UiState ui = new UiState();
        Scope scope = new Scope();
        MiniWave mini = new MiniWave();   // v15: мини-осциллограф компактного режима — от звука, как большой
        MicMeter mm = new MicMeter();
        float[] sbuf = new float[2048];
        List<AreaItem> areas = new List<AreaItem>(); AreaItem area;
        List<MicDev> mics = new List<MicDev>(); MicDev mic; bool micOn = true;
        bool voiceOn = true;   // v5: «Улучшить голос» (ini voice, по умолчанию ВКЛ)
        bool showInRec;        // v7: «Показывать прибор в записи» (ini showInRec, по умолчанию ВЫКЛ)
        bool foldRec;          // v7: «Сворачивать в плашку при записи» (ini foldRec, по умолчанию ВЫКЛ)
        bool skinFrame;        // v8: облик «Рамка» (ini skin=frame), иначе «Корпус»
        bool starting;         // v7: идёт запуск ffmpeg (прибор уже в состоянии записи, повторный старт запрещён)
        bool excl, exclFail;   // v7: сейчас прибор исключён из захвата / Windows не дала исключить
        public uint LastAff;   // v7: GetWindowDisplayAffinity главного окна после последней смены
        string outDirOverride; // --selftest: папка для роликов
        public static MainForm Current;
        bool testing; DateTime testT0;
        PlaqueForm ov; bool ovShown;
        System.Windows.Forms.Timer frame = new System.Windows.Forms.Timer();
        Stopwatch clock = Stopwatch.StartNew();
        Stopwatch sw = new Stopwatch(); TimeSpan acc = TimeSpan.Zero;
        bool hotkeyOk, closeAfterSave, exiting; Rectangle capRect; string outFile;
        NotifyIcon tray; ContextMenuStrip trayMenu; bool balloonShown;
        bool powered = true;
        List<Anim> anims = new List<Anim>();
        float peakHold = -140f; double peakAge; double lastT; DateTime clipT = DateTime.MinValue; bool silentFired;
        int msgShow;
        ToolTip tip = new ToolTip();
        public string Log = "";
        bool compact;          // v11: компактный режим (ini compact=1)
        RegionFrame rframe;    // v11: рамка вокруг выбранной области во время записи
        RegionPicker picker;
        DateTime lastBodyDown = DateTime.MinValue; Point lastBodyPt;
        int corner;            // v16: угол компактного режима под курсором (0 — нет, 1 — свернуть, 2 — закрыть)

        public MainForm()
        {
            cfg = Settings.Load();
            s = Theme.K;
            Text = "XYZ Запись";
            ShowInTaskbar = true;
            try { using (var st = Res.Raw("app.ico")) if (st != null) Icon = new Icon(st); } catch { }
            compact = cfg.Get("compact", "0") == "1";
            ApplyLayout();
            scope.Ensure((int)Math.Round(Lay.Screen.Width * s), (int)Math.Round(Lay.Screen.Height * s), s);
            micOn = cfg.Get("micOn", "1") == "1";
            voiceOn = cfg.Get("voice", "1") == "1";
            showInRec = cfg.Get("showInRec", "0") == "1";
            // v13: у db стояло «Показывать прибор в записи» = ВКЛ — из-за этого прибор попадал в ролики. Один раз при первом запуске v13
            // галочка выключается (вернуть можно в меню корпуса); событие — в журнал.
            if (!Settings.ReadOnly && cfg.Get("v13", "") != "1")
            {
                cfg.Set("v13", "1");
                if (showInRec)
                {
                    showInRec = false; cfg.Set("showInRec", "0");
                    AppLog.W("v13: «Показывать прибор в записи» было ВКЛ — выключено, прибор и плашка больше не попадают в запись");
                    SetStatus("Прибор скрыт из записи (вернуть: меню → «Показывать прибор в записи»).", false);
                }
                cfg.Save();
            }
            foldRec = cfg.Get("foldRec", "0") == "1";
            skinFrame = cfg.Get("skin", "body") == "frame";
            Theme.ApplyPal(cfg.Get("palette", "mint"));   // v19
            ui.SkinK = skinFrame ? 1f : 0f; ui.Lever2 = skinFrame ? 0f : 1f;
            Current = this;
            TopMost = cfg.Get("top", "0") == "1";
            ffmpeg = Engine.FindFfmpeg();
            eng.Ffmpeg = ffmpeg;
            msgShow = Native.RegisterWindowMessage("XYZ_Recorder_v4_show");
            PlaceWindow();
            frame.Interval = 33; frame.Tick += (o, e) => OnFrame();
            ov = new PlaqueForm(s);
            ov.PauseClick += () => TogglePause();
            ov.StopClick += () => StopRec();
            if (!Program.SnapshotMode && !Program.SelfTestMode)
            {
                BuildTray();
                // при запуске прибор «включается»: проявление, щелчок тумблера вверх, экран разгорается
                Alpha = 0; ui.Lever = 0f; ui.Power = 0f;
                Shown += (o, e) =>
                {
                    RefreshAreas(); LoadMics(); UpdateButtons(); StartMeter(false);
                    Run(0f, 1f, 220, 0, OutCubic, v => Alpha = (byte)(255 * v), null);
                    Run(0f, 1f, 170, 240, OutBack, v => ui.Lever = v, null);
                    anims.Add(new Anim { T0 = clock.Elapsed.TotalSeconds + 0.24, Dur = 0.001, From = 0, To = 0, Set = v => { }, Done = () => { if (cfg.Get("click", "1") == "1") ClickSound.Play(true); } });
                    Run(0f, 1f, 300, 320, OutCubic, v => ui.Power = v, null);
                    frame.Start();
                };
            }
            UpdateButtons();
        }

        void PlaceWindow()
        {
            var wa = Screen.PrimaryScreen.WorkingArea;
            int x = wa.X + (wa.Width - FW) / 2, y = wa.Y + (wa.Height - FH) / 2;
            int cx, cy;
            if (int.TryParse(cfg.Get("x", ""), out cx) && int.TryParse(cfg.Get("y", ""), out cy))
            {
                cx += OffX; cy += OffY;   // v11: в ini — начало холста полного прибора
                foreach (var sc in Screen.AllScreens)
                    if (sc.WorkingArea.IntersectsWith(new Rectangle(cx + FW / 4, cy + FH / 4, FW / 2, FH / 2))) { x = cx; y = cy; break; }
            }
            Location = new Point(x, y);
        }
        void SavePos() { if (Visible) { cfg.Set("x", (Left - OffX).ToString()); cfg.Set("y", (Top - OffY).ToString()); cfg.Save(); } }

        // ---------- v11: компактный режим ----------
        int OffX { get { return compact ? CompactView.Off(CompactView.X0, s) : 0; } }
        int OffY { get { return compact ? CompactView.Off(CompactView.Y0, s) : 0; } }
        void ApplyLayout()
        {
            if (compact) EnsureFrame((int)Math.Round(CompactView.W * s), (int)Math.Round(CompactView.H * s));
            else EnsureFrame((int)Math.Round(Lay.CW * s), (int)Math.Round(Lay.CH * s));
        }
        // Эмблема остаётся на месте: окно сдвигается на начало вырезки холста.
        void SetCompact(bool on)
        {
            if (on == compact) return;
            int ox = CompactView.Off(CompactView.X0, s), oy = CompactView.Off(CompactView.Y0, s);
            compact = on; cfg.Set("compact", on ? "1" : "0"); cfg.Save();
            ui.Hot = Hit.None; ui.Down = Hit.None; Cursor = Cursors.Default; tip.SetToolTip(this, "");
            corner = 0; ui.CornL = 0f; ui.CornR = 0f;   // v16
            ApplyLayout();
            Location = on ? new Point(Left + ox, Top + oy) : new Point(Left - ox, Top - oy);
            RenderMain(); Present();
            SavePos();
            if (!frame.Enabled) frame.Start();
        }
        Hit HitAt(Point p) { if (compact) CompactView.Bare = ui.SkinK >= 0.5f; return compact ? CompactView.HitTest(s, p) : DeviceView.HitTest(Lp(p)); }
        // v17: компакт без плашки — курсор опрашивается каждый кадр: зоны у каски показывают кнопки «свернуть»/«закрыть»
        // (сами зоны прозрачны и щелчков не ловят), а над тенью и прочими местами без кнопок и эмблемы окно получает
        // WS_EX_TRANSPARENT — щелчок уходит в окно под ним, как у рамки области.
        bool passThru;
        void SetPassThrough(bool on)
        {
            if (on == passThru || !IsHandleCreated) return;
            passThru = on;
            int ex = Native.GetWindowLong(Handle, -20);
            Native.SetWindowLong(Handle, -20, on ? ex | 0x20 : ex & ~0x20);
        }
        void PollBare()
        {
            if (!(compact && Visible && ui.SkinK >= 0.5f) || Capture || ui.Down != Hit.None) { if (!(compact && ui.SkinK >= 0.5f)) SetPassThrough(false); return; }
            var p = PointToClient(Cursor.Position);
            bool inside = p.X >= 0 && p.Y >= 0 && p.X < Width && p.Y < Height;
            Hit h = Hit.None; int c = 0;
            if (inside)
            {
                CompactView.Bare = true;
                h = CompactView.HitTest(s, p); c = CompactView.CornerAt(s, p);
                if (h == Hit.CMin) c = 1; else if (h == Hit.CClose) c = 2;
            }
            corner = c;
            if (h == Hit.None && ui.Hot != Hit.None) { ui.Hot = Hit.None; Cursor = Cursors.Default; }
            SetPassThrough(inside && h == Hit.None);
        }

        // ---------- трей ----------
        void BuildTray()
        {
            trayMenu = NewMenu();
            var miShow = new ToolStripMenuItem("Показать") { Font = new Font(trayMenu.Font, FontStyle.Bold) };
            miShow.Click += (o, e) => PowerOn();
            var miRec = new ToolStripMenuItem("Начать запись (F9)");
            miRec.Click += (o, e) => { if (state == St.Idle) StartRec(); else if (state == St.Rec || state == St.Paused) StopRec(); };
            var miExit = new ToolStripMenuItem("Выход");
            miExit.Click += (o, e) => ExitApp();
            trayMenu.Items.AddRange(new ToolStripItem[] { miShow, miRec, new ToolStripSeparator(), miExit });
            trayMenu.Opening += (o, e) =>
            {
                miRec.Text = state == St.Idle ? "Начать запись (F9)" : state == St.Saving ? "Сохраняю…" : "Остановить и сохранить (F9)";
                miRec.Enabled = state != St.Saving && (state != St.Idle || ffmpeg != null);
            };
            tray = new NotifyIcon();
            try { using (var st = Res.Raw("app.ico")) if (st != null) tray.Icon = new Icon(st, SystemInformation.SmallIconSize); } catch { }
            if (tray.Icon == null) tray.Icon = SystemIcons.Application;
            tray.Text = "XYZ Запись — F9 старт / стоп";
            tray.ContextMenuStrip = trayMenu;
            tray.MouseClick += (o, e) => { if (e.Button == MouseButtons.Left && !Visible && !ovShown) PowerOn(); };
            tray.Visible = true;
        }

        public static ContextMenuStrip NewMenu()
        {
            var m = new ContextMenuStrip();
            m.Renderer = new DarkRenderer();
            m.Font = new Font("Segoe UI", 9.5f);
            m.ShowImageMargin = false; m.ShowCheckMargin = true;
            m.BackColor = Color.FromArgb(22, 25, 30); m.ForeColor = Theme.Ink;
            // v7: у меню свой hwnd — исключение из захвата ставим и до показа (Opening), и после (Opened)
            m.Opening += (o, e) => ApplyMenuAffinity(m);
            m.Opened += (o, e) => ApplyMenuAffinity(m);
            return m;
        }
        // v7: меню прибора на время записи не попадают в ролик; подсказки пунктов (отдельные окна) во время записи выключены.
        public static uint ApplyMenuAffinity(ToolStripDropDown m)
        {
            uint a = 0;
            try
            {
                bool ex = Current != null && Current.excl;
                if (m.ShowItemToolTips == ex) m.ShowItemToolTips = !ex;
                IntPtr h = m.Handle;
                Native.SetWindowDisplayAffinity(h, ex ? Native.WDA_EXCLUDEFROMCAPTURE : 0u);
                Native.GetWindowDisplayAffinity(h, out a);
                if (ex && a != Native.WDA_EXCLUDEFROMCAPTURE) AppLog.W("affinity меню: Get=0x" + a.ToString("X") + "  <-- НЕ СТОИТ");
            }
            catch { }
            return a;
        }
        public bool ExcludeActive { get { return excl; } }

        // v7: прибор (главное окно и плашка) исключается из захвата на время записи, если не включено «Показывать прибор в записи».
        void ApplyExclude(bool rec)
        {
            excl = rec && !showInRec;
            uint want = excl ? Native.WDA_EXCLUDEFROMCAPTURE : 0u;
            bool ok = false; uint now = 0;
            if (IsHandleCreated)
            {
                ok = AppLog.AffinityRetry(this, want, compact ? "прибор (компакт)" : "прибор (полный)");   // v13: с проверкой, журналом и повтором
                Native.GetWindowDisplayAffinity(Handle, out now);
            }
            LastAff = now;
            exclFail = excl && (!ok || now != want);
            if (ov != null && ov.IsHandleCreated)
                AppLog.Affinity(ov.Handle, rec && showInRec ? 0u : Native.WDA_EXCLUDEFROMCAPTURE, "плашка");
        }

        // ---------- кадр ----------
        void OnFrame()
        {
            double now = clock.Elapsed.TotalSeconds;
            double dt = Math.Min(0.25, now - lastT); lastT = now;
            ui.T = now;
            StepAnims(now);
            int avail = 0; bool live = false;
            if (mm.IsOpen && micOn)
            {
                avail = mm.CopyLast(sbuf); live = true;
                float r, p; bool got = mm.Read(out r, out p);
                peakAge += dt;
                if (got && p >= peakHold) { peakHold = p; peakAge = 0; }
                if (peakAge > 1.5) peakHold -= (float)(24 * dt);
                if (peakHold < -140f) peakHold = -140f;
                if (got && p >= -0.5f) clipT = DateTime.Now;
                if (!silentFired && mm.ZeroMs > 2500 && !mm.AnyNonZero) { silentFired = true; if (state == St.Idle && !testing) SetStatus(Engine.MicNoAccess, true); }
            }
            if (state == St.Rec || state == St.Paused) UpdateTime();
            // обычные сообщения гаснут сами через 7 с, предупреждения остаются
            if (!ui.StatusWarn && ui.Status.Length > 0 && state == St.Idle && !testing && (DateTime.Now - statusT).TotalSeconds > 7) ui.Status = "";
            ui.MicLive = live; ui.PeakDb = peakHold; ui.Clip = (DateTime.Now - clipT).TotalSeconds < 2.5;
            PollBare();   // v17
            // v16: угловые кнопки компакта — плавное появление/исчезание ~150 мс
            {
                float stp = (float)(dt / 0.15);
                bool wl = compact && (corner == 1 || ui.Down == Hit.CMin), wr = compact && (corner == 2 || ui.Down == Hit.CClose);
                ui.CornL = wl ? Math.Min(1f, ui.CornL + stp) : Math.Max(0f, ui.CornL - stp);
                ui.CornR = wr ? Math.Min(1f, ui.CornR + stp) : Math.Max(0f, ui.CornR - stp);
            }
            if (Visible && Alpha > 0)
            {
                scope.Push(sbuf, avail, live && powered);
                if (compact) mini.Push(sbuf, avail, live && powered, now);   // v15: тот же массив sbuf и тот же кадр, что у scope.Push
                RenderMain();
                Present();
            }
            if (ovShown) { ov.Render(ui, live ? sbuf : null, avail); ov.Present(); }
            if (!Visible && !ovShown && anims.Count == 0 && state != St.Rec && state != St.Paused) frame.Stop();
        }

        void RenderMain()
        {
            ui.Mic = mic != null ? ShortMic(mic.Name) : "Микрофон";
            ui.MicOn = micOn && mics.Count > 0;
            ui.Area = area != null ? (area.Short ?? area.Text) : "Весь экран";
            using (var g = Graphics.FromImage(Frame)) { if (compact) CompactView.Render(g, s, ui, mini); else DeviceView.Render(g, s, ui, scope); }
        }

        // «Микрофон (Realtek High Definition Audio)» → «Realtek High Definition Audio» (значок микрофона уже есть)
        static string ShortMic(string n)
        {
            int a = n.IndexOf('('), b = n.LastIndexOf(')');
            if (a > 0 && b > a + 1 && n.Substring(0, a).Trim().ToLower().StartsWith("микрофон")) return n.Substring(a + 1, b - a - 1).Trim();
            return n;
        }

        void Run(float from, float to, double ms, double delayMs, Func<double, double> ease, Action<float> set, Action done)
        {
            anims.Add(new Anim { T0 = clock.Elapsed.TotalSeconds + delayMs / 1000.0, Dur = ms / 1000.0, From = from, To = to, Ease = ease, Set = set, Done = done });
            if (!frame.Enabled) frame.Start();
        }
        void StepAnims(double now)
        {
            for (int i = 0; i < anims.Count; i++)
            {
                var a = anims[i];
                double t = (now - a.T0) / a.Dur; if (t < 0) continue;
                if (t > 1) t = 1;
                a.Set(a.From + (a.To - a.From) * (float)(a.Ease == null ? t : a.Ease(t)));
                if (t >= 1) { anims.RemoveAt(i); i--; if (a.Done != null) a.Done(); }
            }
        }
        static double OutBack(double x) { double c1 = 2.2, c3 = c1 + 1; return 1 + c3 * Math.Pow(x - 1, 3) + c1 * Math.Pow(x - 1, 2); }
        static double OutCubic(double x) { return 1 - Math.Pow(1 - x, 3); }

        // ---------- v8: облик — второй тумблер ----------
        void ToggleSkin()
        {
            skinFrame = !skinFrame;
            cfg.Set("skin", skinFrame ? "frame" : "body"); cfg.Save();
            anims.RemoveAll(a => a.Tag == "skin");
            if (cfg.Get("click", "1") == "1") ClickSound.Play(!skinFrame);
            float l0 = ui.Lever2, k0 = ui.SkinK;
            Run(l0, skinFrame ? 0f : 1f, 170, 0, OutBack, v => ui.Lever2 = v, null); anims[anims.Count - 1].Tag = "skin";
            Run(k0, skinFrame ? 1f : 0f, Math.Max(1.0, 400 * Math.Abs((skinFrame ? 1f : 0f) - k0)), 60, InOutSine, v => ui.SkinK = v, null); anims[anims.Count - 1].Tag = "skin";
        }
        static double InOutSine(double x) { return -(Math.Cos(Math.PI * x) - 1) / 2; }

        // ---------- питание: тумблер ----------
        void PowerOff()
        {
            if (!powered || state == St.Saving) return;
            powered = false;
            if (testing) { mm.CancelCapture(); testing = false; ui.TestText = "Проверить"; }
            if (cfg.Get("click", "1") == "1") ClickSound.Play(false);
            Run(1f, 0f, 170, 0, OutBack, v => ui.Lever = v, null);
            Run(1f, 0f, 220, 60, OutCubic, v => ui.Power = v, null);
            Run(1f, 0f, 300, 260, OutCubic, v => Alpha = (byte)(255 * v), () => HideToTray());
            UpdateButtons();
        }
        void HideToTray()
        {
            SavePos();
            Hide(); Alpha = 0;
            if (state == St.Idle) StopMeter();
            if (!balloonShown && tray != null)
            {
                balloonShown = true;
                try { tray.ShowBalloonTip(3000, "XYZ Запись — в трее", "F9 — начать / остановить запись. Щёлкните значок, чтобы включить прибор.", ToolTipIcon.None); } catch { }
            }
        }
        void PowerOn()
        {
            if (powered && Visible) { Activate(); return; }
            anims.Clear();
            powered = true;
            ui.Lever = 0f; ui.Power = 0f; Alpha = 0;
            RenderMain(); Present();
            Show(); Activate();
            UpdateButtons();
            Run(0f, 1f, 200, 0, OutCubic, v => Alpha = (byte)(255 * v), null);
            Run(0f, 1f, 170, 180, OutBack, v => ui.Lever = v, () => { });
            anims.Add(new Anim { T0 = clock.Elapsed.TotalSeconds + 0.18, Dur = 0.001, From = 0, To = 0, Set = v => { }, Done = () => { if (cfg.Get("click", "1") == "1") ClickSound.Play(true); } });
            Run(0f, 1f, 260, 260, OutCubic, v => ui.Power = v, null);
            StartMeter(false);
        }
        void ExitApp()
        {
            exiting = true;
            if (state == St.Rec || state == St.Paused || state == St.Saving) { closeAfterSave = true; if (state != St.Saving) StopRec(); return; }
            Close();
        }

        // ---------- мышь ----------
        PointF Lp(Point p) { return new PointF(p.X / s - Lay.P, p.Y / s - Lay.P); }
        static bool Interactive(Hit h) { return h != Hit.None && h != Hit.Body && h != Hit.Screen; }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var h = HitAt(e.Location);
            corner = compact ? CompactView.CornerAt(s, e.Location) : 0;   // v16
            if (h == Hit.CMin) corner = 1; else if (h == Hit.CClose) corner = 2;
            var hot = Interactive(h) && ui.IsEn(h) ? h : Hit.None;
            if (hot != ui.Hot) { ui.Hot = hot; Cursor = hot != Hit.None ? Cursors.Hand : Cursors.Default; }
            if (h == Hit.Screen && ui.Status.Length > 70 && !excl) { if (tip.GetToolTip(this) != ui.Status) tip.SetToolTip(this, ui.Status); }
            else if (tip.GetToolTip(this) != "") tip.SetToolTip(this, "");
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e) { ui.Hot = Hit.None; corner = 0; base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var h = HitAt(e.Location);
            if (e.Button == MouseButtons.Right) { ShowBodyMenu(e.Location); return; }
            if (e.Button != MouseButtons.Left) return;
            if (Interactive(h)) { if (ui.IsEn(h)) { ui.Down = h; Capture = true; } return; }
            if (h == Hit.Screen && ui.Clip) { clipT = DateTime.MinValue; peakHold = -140f; return; }
            // v11: двойной щелчок по эмблеме (вне кнопок) — компактный режим вкл/выкл
            if (h == Hit.Body && (compact || CompactView.EmbLp.Contains(Lp(e.Location))))
            {
                var now = DateTime.Now;
                if ((now - lastBodyDown).TotalMilliseconds <= SystemInformation.DoubleClickTime && Math.Abs(e.X - lastBodyPt.X) <= 5 && Math.Abs(e.Y - lastBodyPt.Y) <= 5)
                { lastBodyDown = DateTime.MinValue; SetCompact(!compact); return; }
                lastBodyDown = now; lastBodyPt = e.Location;
            }
            if (h == Hit.None) return;
            Native.DragWindow(this);
            SavePos();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            var d = ui.Down; ui.Down = Hit.None; Capture = false;
            if (d == Hit.None) return;
            if (HitAt(e.Location) != d) return;
            switch (d)
            {
                case Hit.Sel1: ShowAreaMenu(); break;
                case Hit.Sel2: ShowMicMenu(); break;
                case Hit.Test: TestMic(); break;
                case Hit.Folder: ChooseFolder(); break;
                case Hit.Min: PowerOff(); break;
                case Hit.Close: ExitApp(); break;
                case Hit.PadRec: StartRec(); break;
                case Hit.PadPause: TogglePause(); break;
                case Hit.PadStop: StopRec(); break;
                case Hit.Toggle: PowerOff(); break;
                case Hit.Skin: ToggleSkin(); break;
                case Hit.CRec: StartRec(); break;
                case Hit.CPause: TogglePause(); break;
                case Hit.CStop: StopRec(); break;
                case Hit.CMin: PowerOff(); break;     // v16: как «–» полного режима
                case Hit.CClose: ExitApp(); break;    // v16: как «×» полного режима
            }
        }
        Point Px(float lx, float ly) { return new Point((int)Math.Round((lx + Lay.P) * s), (int)Math.Round((ly + Lay.P) * s)); }

        // ---------- меню ----------
        public ContextMenuStrip BuildAreaMenu()
        {
            RefreshAreas();
            var m = NewMenu();
            bool pickDone = false;
            var pick = new ToolStripMenuItem("Выбрать область…");
            pick.ToolTipText = "Тянуть рамку мышью по любому монитору; Esc — отмена.";
            pick.Click += (o, e) => BeginInvoke(new Action(PickRegion));
            foreach (var a in areas)
            {
                var it = a;
                if (a.Kind == 2 && !pickDone) { m.Items.Add(pick); m.Items.Add(new ToolStripSeparator()); pickDone = true; }
                var mi = new ToolStripMenuItem(a.Text) { Checked = area != null && area.Key == a.Key };
                mi.Click += (o, e) => { area = it; if (it.Kind != 2) { cfg.Set("area", it.Key); cfg.Save(); } };
                m.Items.Add(mi);
            }
            if (!pickDone) m.Items.Add(pick);
            return m;
        }
        public ContextMenuStrip BuildMicMenu()
        {
            var m = NewMenu();
            if (mics.Count == 0) m.Items.Add(new ToolStripMenuItem("Микрофоны не найдены") { Enabled = false });
            foreach (var d in mics)
            {
                var it = d;
                var mi = new ToolStripMenuItem(d.Name) { Checked = micOn && mic == d };
                mi.Click += (o, e) => { mic = it; micOn = true; cfg.Set("mic", it.Arg); cfg.Set("micOn", "1"); cfg.Save(); silentFired = false; SetStatus("", false); UpdateButtons(); StartMeter(false); };
                m.Items.Add(mi);
            }
            m.Items.Add(new ToolStripSeparator());
            var off = new ToolStripMenuItem("Без звука") { Checked = !micOn };
            off.Click += (o, e) => { micOn = false; cfg.Set("micOn", "0"); cfg.Save(); UpdateButtons(); StartMeter(false); };
            m.Items.Add(off);
            var rf = new ToolStripMenuItem("Обновить список");
            rf.Click += (o, e) => { LoadMics(); UpdateButtons(); StartMeter(false); };
            m.Items.Add(rf);
            m.Items.Add(new ToolStripSeparator());
            m.Items.Add(VoiceItem());
            return m;
        }
        // v5: переключатель «Улучшить голос» (галочка), сохраняется в ini (voice=1/0)
        ToolStripMenuItem VoiceItem()
        {
            var v = new ToolStripMenuItem("Улучшить голос") { Checked = voiceOn };
            v.ToolTipText = "После «Стоп» звук обрабатывается: шумодав, выравнивание громкости, студийный тембр.\nОригинал хранится 7 дней — меню «Открыть исходники».";
            v.Click += (o, e) =>
            {
                voiceOn = !voiceOn; cfg.Set("voice", voiceOn ? "1" : "0"); cfg.Save();
                if (state == St.Idle) SetStatus(voiceOn ? "Улучшение голоса включено — после «Стоп» звук будет обработан." : "Улучшение голоса выключено — звук как с микрофона.", false);
            };
            return v;
        }
        void ShowAreaMenu() { var m = BuildAreaMenu(); m.Closed += (o, e) => BeginInvoke(new Action(() => m.Dispose())); m.Show(this, Px(Lay.Sel1.X, Lay.Sel1.Bottom + 3)); }
        void ShowMicMenu() { var m = BuildMicMenu(); m.Closed += (o, e) => BeginInvoke(new Action(() => m.Dispose())); m.Show(this, Px(Lay.Sel2.X, Lay.Sel2.Bottom + 3)); }
        void ShowBodyMenu(Point at)
        {
            var m = BuildBodyMenu();
            m.Closed += (o, e) => BeginInvoke(new Action(() => m.Dispose()));
            m.Show(this, at);
        }
        public ContextMenuStrip BuildBodyMenu()
        {
            var m = NewMenu();
            var f1 = new ToolStripMenuItem("Папка для записей…"); f1.Click += (o, e) => ChooseFolder(); f1.Enabled = state == St.Idle;
            var f2 = new ToolStripMenuItem("Открыть папку"); f2.Click += (o, e) => { try { Process.Start("explorer.exe", "\"" + Folder + "\""); } catch { } };
            var c1 = new ToolStripMenuItem("Щелчок тумблера") { Checked = cfg.Get("click", "1") == "1" };
            c1.Click += (o, e) => { cfg.Set("click", c1.Checked ? "0" : "1"); cfg.Save(); };
            var c2 = new ToolStripMenuItem("Поверх всех окон") { Checked = TopMost };
            c2.Click += (o, e) => { TopMost = !TopMost; cfg.Set("top", TopMost ? "1" : "0"); cfg.Save(); };
            var r1 = new ToolStripMenuItem("Показывать прибор в записи") { Checked = showInRec };
            r1.ToolTipText = "ВЫКЛ — прибор и его меню в ролик не попадают, их видите только вы.\nВКЛ — прибор виден в записи (для демонстраций).";
            r1.Click += (o, e) =>
            {
                showInRec = !showInRec; cfg.Set("showInRec", showInRec ? "1" : "0"); cfg.Save();
                bool rec = state == St.Rec || state == St.Paused;
                ApplyExclude(rec);
                SetStatus(showInRec ? "Прибор будет виден в записи." : "Прибор и его меню не попадут в запись.", false);
            };
            var r2 = new ToolStripMenuItem("Сворачивать в плашку при записи") { Checked = foldRec, Enabled = state == St.Idle };
            r2.ToolTipText = "ВКЛ — при старте записи прибор сворачивается в маленькую плашку (как в v6).";
            r2.Click += (o, e) => { foldRec = !foldRec; cfg.Set("foldRec", foldRec ? "1" : "0"); cfg.Save(); };
            var sk1 = new ToolStripMenuItem("Облик «Рамка» (без корпуса)") { Checked = skinFrame };
            sk1.ToolTipText = "То же, что тумблер «Облик»: вверх — корпус, вниз — рамка.";
            sk1.Click += (o, e) => ToggleSkin();
            var aft = new ToolStripMenuItem("После записи");   // v19
            foreach (var kv in new[] { new[] { "toast", "Показать окошко «Сохранено»" }, new[] { "folder", "Открыть папку" }, new[] { "none", "Ничего не делать" } })
            {
                var key = kv[0];
                var it = new ToolStripMenuItem(kv[1]) { Checked = cfg.Get("afterRec", "toast") == key };
                it.Click += (o, e) => { cfg.Set("afterRec", key); cfg.Save(); };
                aft.DropDownItems.Add(it);
            }
            var pal = new ToolStripMenuItem("Цвет облика");   // v19
            foreach (var p in Theme.Pals)
            {
                var pp = p;
                var it = new ToolStripMenuItem(p.Name) { Checked = Theme.Cur.Id == p.Id };
                it.Click += (o, e) => { Theme.ApplyPal(pp.Id); cfg.Set("palette", pp.Id); cfg.Save(); Invalidate(); try { Refresh(); } catch { } };
                pal.DropDownItems.Add(it);
            }
            var t1 = new ToolStripMenuItem("Выключить (в трей)"); t1.Click += (o, e) => PowerOff();
            var x1 = new ToolStripMenuItem("Выход"); x1.Click += (o, e) => ExitApp();
            var cm = new ToolStripMenuItem("Компактный режим") { Checked = compact };
            cm.ToolTipText = "Остаётся плашка с эмблемой: Запись — кружок «d», Пауза — палочки «d» и «b», Стоп — кружок «b».\nДвойной щелчок по эмблеме — переключить.";
            cm.Click += (o, e) => SetCompact(!compact);
            var rw = new ToolStripMenuItem("Открыть исходники"); rw.ToolTipText = "Записи до «Улучшить голос» (хранятся 7 дней).";
            rw.Click += (o, e) => { try { Directory.CreateDirectory(Engine.RawDir); Process.Start("explorer.exe", "\"" + Engine.RawDir + "\""); } catch { } };
            var ab = new ToolStripMenuItem("О программе…"); ab.Click += (o, e) => BeginInvoke(new Action(ShowAbout));
            // v19.4: обрезать любой записанный ролик
            var tr = new ToolStripMenuItem("Обрезать ролик…"); tr.ToolTipText = "Открыть уже записанное видео в редакторе «Обрезать».";
            tr.Click += (o, e) => BeginInvoke(new Action(TrimPick));
            var shx = new ToolStripMenuItem("«Обрезать» в меню Проводника") { Checked = TrimFiles.ShellOn() };
            shx.ToolTipText = "Правой кнопкой по видео (mp4, mkv, mov, avi) → «Обрезать в XYZ Запись».\nВ Windows 11 — в «Показать дополнительные параметры».";
            shx.Click += (o, e) => { bool on = !TrimFiles.ShellOn(); string err = TrimFiles.SetShell(on); if (err != null) MessageBox.Show(err, "XYZ Запись"); };
            var mk = new ToolStripMenuItem("Мышь и клавиатура в записи…");   // v19.12: вместо двух флажков v19.8 — окно как у Bandicam
            mk.ToolTipText = "Подсветка курсора, вспышки щелчков (левая — красная, правая — синяя), нажатия клавиш крупным текстом.\nРисуется только в ролике после «Стоп».";
            mk.Click += (o, e) => BeginInvoke(new Action(ShowMouseKeys));
            m.Items.AddRange(new ToolStripItem[] { cm, new ToolStripSeparator(), f1, f2, rw, tr, shx, new ToolStripSeparator(), VoiceItem(), new ToolStripSeparator(), r1, r2, mk, aft, new ToolStripSeparator(), sk1, pal, c1, c2, new ToolStripSeparator(), t1, ab, x1 });
            return m;
        }

        void TrimPick()
        {
            using (var d = new OpenFileDialog { Title = "Обрезать ролик — XYZ Запись", Filter = "Видео (*.mp4;*.mkv;*.mov;*.avi;*.webm)|*.mp4;*.mkv;*.mov;*.avi;*.webm|Все файлы|*.*" })
            {
                try { if (Directory.Exists(Folder)) d.InitialDirectory = Folder; } catch { }
                if (d.ShowDialog(this) != DialogResult.OK) return;
                TrimFiles.Launch(d.FileName);   // v19.5: отдельное окно, запись можно вести параллельно
            }
        }

        void ShowMouseKeys()
        {
            if (state != St.Idle) { SetStatus("Настройки мыши и клавиатуры — после записи", true); return; }
            using (var f = new MouseKeysForm(cfg))
            {
                var wa = Screen.FromControl(this).WorkingArea;
                int x = Left + Width / 2 - f.Width / 2, y = Top + Height / 2 - f.Height / 2;
                f.Location = new Point(Math.Max(wa.X, Math.Min(x, wa.Right - f.Width)), Math.Max(wa.Y, Math.Min(y, wa.Bottom - f.Height)));
                f.TopMost = TopMost;
                f.ShowDialog(this);
            }
        }

        void ShowAbout()
        {
            using (var f = new AboutForm(s))
            {
                var wa = Screen.FromControl(this).WorkingArea;
                int x = Left + Width / 2 - f.Width / 2, y = Top + Height / 2 - f.Height / 2;
                f.Location = new Point(Math.Max(wa.X, Math.Min(x, wa.Right - f.Width)), Math.Max(wa.Y, Math.Min(y, wa.Bottom - f.Height)));
                f.TopMost = TopMost; f.Exclude = excl;
                f.ShowDialog(this);
            }
        }

        // ---------- v11: выбор области ----------
        void PickRegion()
        {
            if (state != St.Idle || starting || picker != null) return;
            picker = new RegionPicker();
            picker.Done += r =>
            {
                picker = null;
                if (!r.HasValue) { SetStatus("Выбор области отменён.", false); return; }
                var v = r.Value;
                cfg.Set("region", string.Format("{0},{1},{2},{3}", v.X, v.Y, v.Width, v.Height)); cfg.Set("area", "region"); cfg.Save();
                area = null; RefreshAreas();
                SetStatus(string.Format("Область {0}×{1} — F9 или «Запись» начнёт запись с неё.", v.Width, v.Height), false);
                if (Visible) Activate();
            };
            picker.ShowPicker();
        }
        static bool TryRect(string t, out Rectangle r)
        {
            r = Rectangle.Empty;
            var p = (t ?? "").Split(',');
            int x, y, w, h;
            if (p.Length != 4 || !int.TryParse(p[0], out x) || !int.TryParse(p[1], out y) || !int.TryParse(p[2], out w) || !int.TryParse(p[3], out h)) return false;
            if (w < RegionPicker.MinSize || h < RegionPicker.MinSize) return false;
            r = new Rectangle(x, y, w, h); return true;
        }
        void ShowRegionFrame()
        {
            if (area == null || area.Kind != 3 || (Program.SelfTestMode && !Program.CaptureTestMode)) return;
            try
            {
                if (rframe == null) rframe = new RegionFrame();
                rframe.Place(capRect); rframe.SetColor(false); rframe.Show(); rframe.SetColor(false);
                AppLog.AffinityRetry(rframe, Native.WDA_EXCLUDEFROMCAPTURE, "рамка области (показ)");   // v13
            }
            catch { }
        }
        void HideRegionFrame() { try { if (rframe != null) rframe.Hide(); } catch { } }

        string Folder
        {
            get
            {
                if (outDirOverride != null) return outDirOverride;
                string f = cfg.Get("folder", "");
                if (f.Length == 0 || !Directory.Exists(f)) f = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                return f;
            }
        }
        void ChooseFolder()
        {
            if (state != St.Idle) return;
            using (var d = new FolderBrowserDialog())
            {
                d.Description = "Куда сохранять записи"; d.SelectedPath = Folder;
                if (d.ShowDialog(this) == DialogResult.OK) { cfg.Set("folder", d.SelectedPath); cfg.Save(); SetStatus("Папка: " + d.SelectedPath, false); }
            }
        }

        void LoadMics()
        {
            string raw; mics = Engine.ListMics(ffmpeg, out raw);
            string saved = cfg.Get("mic", "");
            mic = mics.Count > 0 ? mics[0] : null;
            foreach (var m in mics) if (m.Arg == saved || m.Name == saved) mic = m;
            if (ffmpeg == null) SetStatus("Не найден ffmpeg — положите ffmpeg.exe рядом с программой.", true);
            else
            {
                string why = Engine.MicBlockReason();
                if (why != null && micOn) SetStatus("Звук не запишется: " + why + " (" + Engine.MicHint + ").", true);
            }
        }

        void RefreshAreas()
        {
            string keep = area != null ? area.Key : cfg.Get("area", "full");
            areas.Clear();
            var scr = Screen.AllScreens;
            areas.Add(new AreaItem { Kind = 0, Key = "full", Text = scr.Length > 1 ? "Весь экран (основной монитор)" : "Весь экран" });   // v13: ddagrab пишет один монитор
            if (scr.Length > 1)
                for (int i = 0; i < scr.Length; i++)
                    areas.Add(new AreaItem { Kind = 1, Mon = i, Key = "mon:" + i,
                        Text = string.Format("Монитор {0} — {1}×{2}{3}", i + 1, scr[i].Bounds.Width, scr[i].Bounds.Height, scr[i].Primary ? " (основной)" : "") });
            Rectangle rr;   // v11: последняя выбранная область
            if (TryRect(cfg.Get("region", ""), out rr))
                areas.Add(new AreaItem { Kind = 3, Key = "region", Rect = rr, Text = string.Format("Область {0}×{1} (последняя)", rr.Width, rr.Height), Short = string.Format("Область {0}×{1}", rr.Width, rr.Height) });
            uint myPid = (uint)Process.GetCurrentProcess().Id;
            Native.EnumWindows((h, l) =>
            {
                if (!Native.IsWindowVisible(h)) return true;
                if (Native.GetWindow(h, 4) != IntPtr.Zero) return true;
                if ((Native.GetWindowLong(h, -20) & 0x80) != 0) return true;
                int cloaked; if (Native.DwmGetWindowAttribute(h, 14, out cloaked, 4) == 0 && cloaked != 0) return true;
                uint pid; Native.GetWindowThreadProcessId(h, out pid); if (pid == myPid) return true;
                int n = Native.GetWindowTextLength(h); if (n == 0) return true;
                var sb = new StringBuilder(n + 1); Native.GetWindowText(h, sb, n + 1);
                string t = sb.ToString(); if (t == "Program Manager") return true;
                if (t.Length > 70) t = t.Substring(0, 67) + "…";
                areas.Add(new AreaItem { Kind = 2, Hwnd = h, Key = "win:" + h.ToInt64(), Text = "Окно: " + t });
                return true;
            }, IntPtr.Zero);
            area = areas[0];
            foreach (var a in areas) if (a.Key == keep) area = a;
        }

        static Rectangle Even(Rectangle r)
        {
            var v = SystemInformation.VirtualScreen;
            r = Rectangle.Intersect(r, v);
            return new Rectangle(r.X, r.Y, r.Width & ~1, r.Height & ~1);
        }

        bool ResolveArea(out string err)
        {
            err = null;
            var it = area;
            if (it == null || it.Kind == 0) { eng.Area = null; capRect = SystemInformation.VirtualScreen; cfg.Set("area", "full"); cfg.Save(); return true; }
            if (it.Kind == 1)
            {
                var scr = Screen.AllScreens; if (it.Mon >= scr.Length) { err = "Монитор не найден — откройте список заново."; return false; }
                capRect = Even(scr[it.Mon].Bounds); eng.Area = capRect; cfg.Set("area", it.Key); cfg.Save(); return true;
            }
            if (it.Kind == 3)
            {
                var rc3 = Even(it.Rect);
                if (rc3.Width < RegionPicker.MinSize || rc3.Height < RegionPicker.MinSize) { err = "Область вне экрана — выберите её заново."; return false; }
                capRect = rc3; eng.Area = rc3; cfg.Set("area", "region"); cfg.Save(); return true;
            }
            if (!Native.IsWindow(it.Hwnd)) { err = "Окно уже закрыто — выберите область заново."; return false; }
            if (Native.IsIconic(it.Hwnd)) Native.ShowWindow(it.Hwnd, 9);
            Native.SetForegroundWindow(it.Hwnd);
            Thread.Sleep(350);
            Native.RECT r;
            if (Native.DwmGetWindowAttribute(it.Hwnd, 9, out r, Marshal.SizeOf(typeof(Native.RECT))) != 0) Native.GetWindowRect(it.Hwnd, out r);
            var rc = Even(new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top));
            if (rc.Width < 16 || rc.Height < 16) { err = "Окно вне экрана или слишком маленькое."; return false; }
            capRect = rc; eng.Area = rc; return true;
        }

        DateTime statusT;
        void SetStatus(string t, bool warn) { ui.Status = t ?? ""; ui.StatusWarn = warn; statusT = DateTime.Now; }

        void UpdateButtons()
        {
            bool idle = state == St.Idle && !starting;
            ui.Rec = state == St.Rec || starting ? 1 : state == St.Paused ? 2 : state == St.Saving ? 3 : 0;
            ui.En[(int)Hit.PadRec] = idle && ffmpeg != null && !testing;
            ui.En[(int)Hit.PadPause] = state == St.Rec || state == St.Paused;
            ui.En[(int)Hit.PadStop] = state == St.Rec || state == St.Paused;
            ui.En[(int)Hit.Sel1] = idle;
            ui.En[(int)Hit.Sel2] = idle && !testing;
            ui.En[(int)Hit.Folder] = idle;
            ui.En[(int)Hit.Test] = idle && micOn && mics.Count > 0 && !testing;
            ui.En[(int)Hit.Min] = state != St.Saving;
            ui.En[(int)Hit.Close] = true;
            ui.En[(int)Hit.CMin] = ui.En[(int)Hit.Min];         // v16: угловые кнопки компакта = «–»/«×»
            ui.En[(int)Hit.CClose] = ui.En[(int)Hit.Close];
            ui.En[(int)Hit.Toggle] = state != St.Saving && powered;
            ui.En[(int)Hit.Skin] = powered;
            ui.En[(int)Hit.CRec] = ui.En[(int)Hit.PadRec];      // v11: кнопки компактного режима
            ui.En[(int)Hit.CPause] = ui.En[(int)Hit.PadPause];
            ui.En[(int)Hit.CStop] = ui.En[(int)Hit.PadStop];
        }

        // ---------- уровень, осциллограф и проба ----------
        MicDev CurMic() { return micOn && mics.Count > 0 ? mic : null; }

        void StopMeter()
        {
            if (testing) { mm.CancelCapture(); testing = false; ui.TestText = "Проверить"; }
            mm.Close(); ui.MicBlocked = false; peakHold = -140f;
        }

        // force — открыть даже при скрытом окне.  Во время записи waveIn работает параллельно с ffmpeg (общий доступ Windows).
        public void StartMeter(bool force)
        {
            var m = CurMic();
            bool want = m != null && (force || (Visible && powered) || ovShown);
            if (!want) { if (!testing) { mm.Close(); ui.MicBlocked = false; } return; }
            int dev = WinMM.Match(m.Name);
            if (mm.IsOpen && mm.Device == dev) return;
            if (testing) return;
            int r = mm.Open(dev);
            if (r != 0 && dev != -1) r = mm.Open(dev = -1);
            string why = Engine.MicBlockReason();
            silentFired = false;
            if (r != 0 || why != null)
            {
                mm.Close(); ui.MicBlocked = true;
                if (state == St.Idle) SetStatus(Engine.MicNoAccess, true);
                return;
            }
            ui.MicBlocked = false;
        }

        void TestMic()
        {
            if (testing || state != St.Idle) return;
            if (!mm.IsOpen) StartMeter(true);
            if (!mm.IsOpen) { SetStatus(Engine.MicNoAccess, true); return; }
            testing = true; testT0 = DateTime.Now; UpdateButtons();
            SetStatus("Проба 3 с: скажите что-нибудь в микрофон…", false);
            var t = new System.Windows.Forms.Timer { Interval = 100 };
            t.Tick += (o, e) =>
            {
                if (!testing || ui.TestText.StartsWith("▶")) { t.Stop(); t.Dispose(); return; }
                int left = 3 - (int)(DateTime.Now - testT0).TotalSeconds; if (left < 1) left = 1;
                ui.TestText = "● " + left + " с";
            };
            ui.TestText = "● 3 с"; t.Start();
            mm.BeginCapture(3000, pcm => { try { BeginInvoke(new Action(() => PlayTest(pcm))); } catch { } });
        }

        void PlayTest(byte[] pcm)
        {
            if (!testing) return;
            float pk = MicMeter.PeakDb(pcm);
            if (pk <= -100f)
            {
                testing = false; ui.TestText = "Проверить"; UpdateButtons();
                ui.MicBlocked = true;
                SetStatus(Engine.MicNoAccess, true);
                return;
            }
            ui.TestText = "▶ слушаем";
            string pks = pk.ToString("0.0", System.Globalization.CultureInfo.GetCultureInfo("ru-RU")).Replace("-", "−");
            SetStatus("Проигрываю пробу (пик " + pks + " дБ)…", false);
            var th = new Thread(() =>
            {
                string err = null;
                try { using (var ms = new MemoryStream(MicMeter.Wav(pcm))) using (var sp = new System.Media.SoundPlayer(ms)) sp.PlaySync(); }
                catch (Exception ex) { err = ex.Message; }
                try { BeginInvoke(new Action(() => TestDone(pk, pks, err))); } catch { }
            });
            th.IsBackground = true; th.Start();
        }

        void TestDone(float pk, string pks, string err)
        {
            testing = false; ui.TestText = "Проверить"; UpdateButtons();
            if (err != null) SetStatus("Не удалось проиграть пробу: " + err, true);
            else if (pk < -45f) SetStatus("Очень тихо (пик " + pks + " дБ) — говорите ближе или выберите другой микрофон.", true);
            else if (pk >= -0.5f) SetStatus("Перегруз (пик " + pks + " дБ) — уменьшите громкость микрофона.", true);
            else SetStatus("Проба: пик " + pks + " дБ. Если себя не слышно — выберите другой микрофон.", false);
        }

        TimeSpan Elapsed { get { return acc + (sw.IsRunning ? sw.Elapsed : TimeSpan.Zero); } }

        void UpdateTime()
        {
            var t = Elapsed;
            string sx = string.Format("{0:00}:{1:00}:{2:00}", (int)t.TotalHours, t.Minutes, t.Seconds);
            ui.Timer = sx;
            if (tray != null) tray.Text = state == St.Rec ? "XYZ Запись — идёт запись " + sx : state == St.Paused ? "XYZ Запись — пауза " + sx : "XYZ Запись — F9 старт / стоп";
            Text = state == St.Rec || state == St.Paused ? (state == St.Paused ? "❚❚ " : "● ") + sx + " — XYZ Запись" : "XYZ Запись";
        }

        // ---------- запись (логика прежняя) ----------
        void ShowOverlay()
        {
            var ovh = ov.Handle;   // v7: плашка исключена из захвата, если не включено «Показывать прибор в записи»
            bool ok = showInRec ? Native.SetWindowDisplayAffinity(ovh, 0) || true : Native.SetWindowDisplayAffinity(ovh, Native.WDA_EXCLUDEFROMCAPTURE);
            var cands = new List<Point>();
            var scr = new List<Screen>(Screen.AllScreens);
            scr.Sort((a, b) => b.Primary.CompareTo(a.Primary));
            int w = ov.Width, h = ov.Height, m = (int)(4 * s);
            foreach (var sc in scr)
            {
                var wa = sc.WorkingArea;
                cands.Add(new Point(wa.Right - w - m, wa.Bottom - h - m));
                cands.Add(new Point(wa.Right - w - m, wa.Top + m));
            }
            Point? pos = null;
            foreach (var p in cands) if (ok || !capRect.IntersectsWith(new Rectangle(p, ov.Size))) { pos = p; break; }
            if (Visible) SavePos();
            if (pos.HasValue)
            {
                ov.Location = pos.Value; ov.Render(ui, null, 0); ov.Present(); ov.Show(); ovShown = true; Hide();
                AppLog.AffinityRetry(ov, showInRec ? 0u : Native.WDA_EXCLUDEFROMCAPTURE, "плашка (показ)");   // v13
            }
            else { ovShown = false; Hide(); }   // плашку некуда деть так, чтобы она не попала в запись — работаем из трея (F9)
            if (!frame.Enabled) frame.Start();
        }

        void HideOverlay()
        {
            if (ovShown) ov.Hide(); ovShown = false;
            if (powered && !exiting && !Visible) { Alpha = 255; RenderMain(); Present(); Show(); if (!Program.SelfTestMode) Activate(); }
            if (!frame.Enabled) frame.Start();
        }

        void StartRec()
        {
            if (state != St.Idle || ffmpeg == null || starting) return;
            string err;
            if (!ResolveArea(out err)) { SetStatus(err, true); return; }
            var m = CurMic();
            eng.Mic = m != null ? m.Arg : null;
            eng.MicName = m != null ? m.Name : null;
            if (testing) return;
            KeyLog.LoadCfg(cfg); Engine.DrawMouse = KeyLog.Cursor;   // v19.12
            eng.Segments.Clear(); eng.SegStarts.Clear(); eng.SegModes.Clear(); eng.SegInputs.Clear();
            eng.WorkDir = Path.Combine(Path.GetTempPath(), "xyz_rec", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(eng.WorkDir);
            acc = TimeSpan.Zero; sw.Reset();
            SetStatus("Запуск…", false);
            ui.Rec = 1; ui.Timer = "00:00:00";
            // v7: по умолчанию прибор остаётся на месте и переходит в состояние записи; плашка — только по галочке
            // «Сворачивать в плашку при записи» или когда прибор выключен в трей (запуск по F9).
            if (foldRec || !Visible) { ApplyExclude(true); ShowOverlay(); }
            else
            {
                ApplyExclude(true);   // до старта ffmpeg — первый же кадр без прибора
                ui.RecK = 0f; Run(0f, 1f, 450, 0, OutCubic, v => ui.RecK = v, null);
                starting = true; UpdateButtons();
                if (!frame.Enabled) frame.Start();
            }
            AppLog.W("старт записи: область «" + (area != null ? area.Text : "весь экран") + "», showInRec=" + (showInRec ? 1 : 0) + ", foldRec=" + (foldRec ? 1 : 0) + ", compact=" + (compact ? 1 : 0) + ", звук=" + (eng.Mic != null ? "да" : "нет"));
            Application.DoEvents();
            bool started = eng.StartSegment();
            string micWarn = null;
            if (!started && eng.Mic != null && mm.IsOpen)
            {
                // вдруг устройство не делится — отпускаем индикатор и пробуем ещё раз со звуком
                mm.Close();
                started = eng.StartSegment();
            }
            if (!started && eng.Mic != null)
            {
                micWarn = "Пишу без звука: микрофон недоступен. " + Engine.MicHint + ".";
                eng.Mic = null; eng.MicName = null;
                started = eng.StartSegment();
            }
            starting = false;
            if (!started)
            {
                ApplyExclude(false); ui.RecK = 1f;
                HideOverlay();
                state = St.Idle; UpdateButtons(); ui.Timer = "00:00:00";
                SetStatus("Ошибка запуска записи", true);
                if (Program.SelfTestMode) Program.StLog("ОШИБКА запуска записи: " + eng.LastError);
                else MessageBox.Show(this, eng.LastError, "XYZ Запись", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (exclFail) SetStatus("Windows не дала скрыть прибор из записи — он попадёт в ролик.", true);
            else SetStatus(micWarn ?? "", micWarn != null);
            sw.Start(); state = St.Rec;
            KeyLog.LoadCfg(cfg);
            keysRec = KeyLog.Keys || KeyLog.Clicks || KeyLog.Halo;   // v19.8: показ нажатий и подсветка курсора (v19.12: + щелчки отдельно)
            if (keysRec) { try { KeyLog.Start(); } catch { keysRec = false; } }
            UpdateButtons(); UpdateTime();
            StartMeter(false);
            ShowRegionFrame();   // v11
        }

        void TogglePause()
        {
            if (state == St.Rec)
            {
                sw.Stop(); acc += sw.Elapsed; sw.Reset();
                if (keysRec) KeyLog.Pause();
                state = St.Paused; UpdateButtons(); UpdateTime();
                eng.StopSegment();
            }
            else if (state == St.Paused)
            {
                if (!eng.StartSegment()) { SetStatus("Не удалось продолжить: " + eng.LastError, true); return; }
                sw.Start(); state = St.Rec; UpdateButtons();
                if (keysRec) KeyLog.Resume();
            }
            if (rframe != null && rframe.Visible) rframe.SetColor(state == St.Paused);   // v11
        }

        bool keysRec;
        void StopRec()
        {
            if (state != St.Rec && state != St.Paused) return;
            try { KeyLog.Stop(); } catch { }
            if (state == St.Rec) { sw.Stop(); acc += sw.Elapsed; sw.Reset(); eng.StopSegment(); }
            ApplyExclude(false);   // v7: запись остановлена — прибор снова виден скриншотам
            HideRegionFrame();     // v11
            ui.RecK = 1f;
            state = St.Saving; UpdateTime(); UpdateButtons();
            HideOverlay();
            SetStatus("Сохраняю…", false);
            string dir = Folder;
            string baseName = "zamechaniya_" + DateTime.Now.ToString("yyyy-MM-dd_HHmm");
            outFile = Path.Combine(dir, baseName + ".mp4");
            for (int i = 2; File.Exists(outFile); i++) outFile = Path.Combine(dir, baseName + "_" + i + ".mp4");
            bool doVoice = voiceOn && !string.IsNullOrEmpty(eng.Mic);   // без звуковой дорожки обрабатывать нечего
            double dur = acc.TotalSeconds;
            string of = outFile;
            var th = new Thread(() =>
            {
                bool ok = eng.Finalize(of);
                if (ok && keysRec)   // v19.12: длительности сегментов — для привязки событий к кадрам (до удаления сегментов)
                { try { var sd = new List<double>(); foreach (var sg in eng.Segments) sd.Add(TrimForm.Duration(sg)); KeyLog.SegDur = sd; KeyLog.SegFfStart = new List<double>(eng.SegStarts); KeyLog.SegSrc = new List<string>(eng.SegModes); } catch (Exception sx) { AppLog.W("нажатия: длительности сегментов: " + sx.Message); } }
                if (ok) eng.CleanupSegments();
                if (ok && keysRec && (KeyLog.Events.Count > 0 || KeyLog.Moves.Count > 0))   // v19.8: нажатия и курсор поверх видео
                {
                    try { BeginInvoke(new Action(() => SetStatus("Добавляю нажатия клавиш…", false))); } catch { }
                    string kt = Path.Combine(eng.WorkDir, "keys_" + Path.GetFileName(of)); string kerr;
                    if (KeyLog.Burn(ffmpeg, of, kt, eng.CapRect, eng.WorkDir, eng.Log, out kerr))
                    { try { File.Delete(of); File.Move(kt, of); } catch (Exception kx) { eng.Log("нажатия: не заменил файл: " + kx.Message); AppLog.W("нажатия: не заменил файл: " + kx.Message); } }
                    else { eng.Log("нажатия: не наложены: " + kerr); AppLog.W("нажатия: не наложены: " + kerr); }   // v19.14
                }
                int voice = 0;   // 0 — не обрабатывали, 1 — звук улучшен, −1 — ошибка (оставлен исходный)
                if (ok && doVoice)
                {
                    BeginInvoke(new Action(() => { ui.Enh = true; ui.Prog = 0f; SetStatus("Обработка звука…", false); }));
                    string tmp = Path.Combine(eng.WorkDir, "voice_" + Path.GetFileName(of));
                    int lastPct = -1; string err;
                    bool v = Engine.Enhance(ffmpeg, of, tmp, dur, pr =>
                    {
                        int pct = (int)(pr * 100);
                        if (pct == lastPct) return; lastPct = pct;
                        try { BeginInvoke(new Action(() => { ui.Prog = (float)pr; SetStatus("Обработка звука… " + pct + "%", false); })); } catch { }
                    }, eng.Log, out err);
                    string rawF = null;
                    if (v) v = Engine.SwapEnhanced(of, tmp, out rawF, out err);
                    if (v) eng.Log("=== улучшение: готово, оригинал -> " + rawF);
                    else { eng.Log("=== улучшение НЕ удалось, оставлен исходный файл: " + err); try { if (File.Exists(tmp)) File.Delete(tmp); } catch { } }
                    voice = v ? 1 : -1;
                }
                BeginInvoke(new Action(() => SaveDone(ok, voice)));
            });
            th.IsBackground = true; th.Start();
        }

        void SaveDone(bool ok, int voice)
        {
            state = St.Idle; acc = TimeSpan.Zero; ui.Enh = false; ui.Prog = -1f; UpdateButtons(); UpdateTime();
            if (Visible) StartMeter(false); else if (!ovShown) StopMeter();
            if (ok)
            {
                string msg = voice == 1 ? "Сохранено, голос улучшен: " + Path.GetFileName(outFile)
                           : voice == -1 ? "Сохранено: " + Path.GetFileName(outFile) + " — звук без обработки (ошибка улучшения)"
                           : "Сохранено: " + Path.GetFileName(outFile);
                SetStatus(msg, voice == -1);
                if (!Visible && tray != null) try { tray.ShowBalloonTip(3000, "XYZ Запись", msg, voice == -1 ? ToolTipIcon.Warning : ToolTipIcon.None); } catch { }
                if (!Program.SelfTestMode)
                {
                    string after = cfg.Get("afterRec", "toast");   // v19: toast — окошко «Сохранено», folder — открыть папку, none — ничего
                    if (after == "folder") { try { Process.Start("explorer.exe", "/select,\"" + outFile + "\""); } catch { } }
                    else if (after == "toast") { try { var tf = new SavedToast(outFile, msg, voice == -1, !showInRec); tf.Show(); } catch { } }
                }
            }
            else
            {
                SetStatus("Ошибка сохранения — фрагменты в " + eng.WorkDir, true);
                if (Program.SelfTestMode) Program.StLog("ОШИБКА сохранения: " + eng.LastError);
                else MessageBox.Show(this, eng.LastError + "\n\nФрагменты записи сохранены в:\n" + eng.WorkDir, "XYZ Запись", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (closeAfterSave) Close();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            RenderMain(); Present();
            if (Program.SnapshotMode || Program.SelfTestMode) return;
            hotkeyOk = Native.RegisterHotKey(Handle, 1, 0x4000, 0x78); // MOD_NOREPEAT, F9
            if (!hotkeyOk) SetStatus("F9 занята другой программой — пользуйтесь кнопками.", true);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == 1)
            {
                if (state == St.Idle) StartRec();
                else if (state == St.Rec || state == St.Paused) StopRec();
                return;
            }
            if (msgShow != 0 && m.Msg == msgShow) { if (!ovShown) PowerOn(); return; }
            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (state == St.Rec || state == St.Paused) { e.Cancel = true; closeAfterSave = true; exiting = true; StopRec(); return; }
            if (state == St.Saving) { e.Cancel = true; closeAfterSave = true; exiting = true; return; }
            SavePos();
            if (hotkeyOk) Native.UnregisterHotKey(Handle, 1);
            StopMeter();
            frame.Stop();
            if (tray != null) { tray.Visible = false; tray.Dispose(); tray = null; }
            if (ov != null) { ov.Dispose(); ov = null; }
            if (rframe != null) { rframe.Dispose(); rframe = null; }
            base.OnFormClosing(e);
        }

        protected override bool ShowWithoutActivation { get { return Program.SelfTestMode; } }

        // ---------- для --selftest (v7): реальный путь StartRec/StopRec, окно на экране без активации ----------
        public void StPrep(string outDir, bool show)
        {
            RefreshAreas(); area = areas[0];          // весь экран; чужие окна не трогаем
            micOn = false; voiceOn = false;           // звук для проверки захвата не нужен
            showInRec = show; foldRec = false;
            outDirOverride = outDir; TopMost = true;
            var wa = Screen.PrimaryScreen.WorkingArea;
            Location = new Point(wa.X + (wa.Width - FW) / 2, wa.Y + (wa.Height - FH) / 2);
            UpdateButtons();
        }
        public bool StShowInRec { set { showInRec = value; } }
        public void StShow() { powered = true; Alpha = 255; ui.Lever = 1f; ui.Power = 1f; RenderMain(); Show(); Present(); frame.Start(); }
        public void StStart() { StartRec(); }
        public void StStop() { StopRec(); }
        public bool StIdle { get { return state == St.Idle; } }
        public bool StRec { get { return state == St.Rec; } }
        public bool OvShown { get { return ovShown; } }
        public string OutFile { get { return outFile; } }
        public string StatusText { get { return ui.Status; } }
        public float RecK { get { return ui.RecK; } }

        // ---------- v13: --capturetest — прибор в правом нижнем углу основного монитора, настоящий StartRec/StopRec ----------
        public string CtMode = "";
        public void CtPrep(string mode, string outDir)
        {
            CtMode = mode;
            RefreshAreas(); area = areas[0];
            micOn = false; voiceOn = false; showInRec = false;
            foldRec = mode == "plate";
            outDirOverride = outDir; TopMost = true;
            compact = mode == "compact" || mode == "region"; ApplyLayout();
            var wa = Screen.PrimaryScreen.WorkingArea; int m = (int)(8 * s);
            Location = new Point(wa.Right - FW - m, wa.Bottom - FH - m);
            if (mode == "plate") exiting = true;   // после «Стоп» главное окно не показывать
            if (mode == "region")
            {
                var r = Rectangle.Inflate(new Rectangle(Location, Size), (int)(140 * s), (int)(110 * s));
                r = RegionPicker.EvenRect(Rectangle.Intersect(r, Screen.PrimaryScreen.Bounds));
                area = new AreaItem { Kind = 3, Key = "region", Rect = r, Text = "Область (проверка)", Short = string.Format("Область {0}×{1}", r.Width, r.Height) };
            }
            UpdateButtons();
        }
        // Прямоугольник, где окажется прибор (для плашки — место из ShowOverlay).
        public Rectangle CtDeviceRect()
        {
            if (CtMode == "plate")
            {
                var wa = Screen.PrimaryScreen.WorkingArea; int m = (int)(4 * s);
                return new Rectangle(wa.Right - ov.Width - m, wa.Bottom - ov.Height - m, ov.Width, ov.Height);
            }
            return new Rectangle(Location, Size);
        }
        public Bitmap CtDeviceFrame() { return CtMode == "plate" ? ov.CopyFrame() : CopyFrame(); }
        public Engine CtEngine { get { return eng; } }
        public IntPtr CtOvHandle { get { return ov != null && ov.IsHandleCreated ? ov.Handle : IntPtr.Zero; } }
        public IntPtr CtFrameHandle { get { return rframe != null && rframe.IsHandleCreated ? rframe.Handle : IntPtr.Zero; } }
        public bool CtFrameVisible { get { return rframe != null && rframe.Visible; } }

        // ---------- для --snapshot: кадры без показа окна ----------
        public float SnapRecK = 1f;
        public void SnapPrep()
        {
            RefreshAreas(); LoadMics(); micOn = true;
            if (mics.Count == 0) { mics.Add(new MicDev { Name = "Микрофон (Realtek High Definition Audio)" }); mic = mics[0]; }
            SetStatus("", false);
        }
        // st: 0 ожидание, 1 запись, 2 пауза; lever/power — положение тумблера; hot — наведение; frames — кадров синтетической волны.
        public Bitmap Snap(int st, float lever, float power, Hit hot, Hit down, float amp, string status, bool warn, double t)
        {
            state = st == 1 ? St.Rec : st == 2 ? St.Paused : St.Idle;
            UpdateButtons();
            ui.Lever = lever; ui.Power = power; ui.Hot = hot; ui.Down = down; ui.T = t; ui.RecK = SnapRecK;
            ui.Timer = st == 0 ? "00:00:00" : st == 1 ? "00:01:27" : "00:03:12";
            SetStatus(status, warn);
            ui.MicLive = amp > 0; ui.PeakDb = amp > 0 ? (float)(20 * Math.Log10(Math.Min(1.0, amp * 0.9))) : -140f; ui.Clip = amp >= 0.9f;
            var buf = new float[2048];
            var sw2 = Stopwatch.StartNew();
            for (int i = 0; i < 8; i++)
            {
                Synth.Fill(buf, t - 0.033 * (7 - i), amp);
                scope.Push(buf, amp > 0 ? buf.Length : 0, amp > 0 && power > 0.5f);
            }
            mini.Reset();
            mini.Push(buf, amp > 0 ? buf.Length : 0, amp > 0 && power > 0.5f, t);   // v15: тот же последний буфер, что получил scope
            RenderMain();
            Log += string.Format("frame st={0} lever={1:0.00}: {2} ms\r\n", st, lever, sw2.ElapsedMilliseconds / 8.0);
            return CopyFrame();
        }
        public Bitmap SnapProc(float prog, double t)
        {
            state = St.Saving; UpdateButtons();
            ui.Lever = 1f; ui.Power = 1f; ui.Hot = Hit.None; ui.Down = Hit.None; ui.T = t; ui.Timer = "00:00:00";
            ui.Enh = true; ui.Prog = prog; SetStatus("Обработка звука… " + (int)(prog * 100) + "%", false);
            ui.MicLive = false; ui.PeakDb = -140f; ui.Clip = false;
            RenderMain();
            var b = CopyFrame();
            ui.Enh = false; ui.Prog = -1f; state = St.Idle; UpdateButtons(); SetStatus("", false);
            return b;
        }
        public Bitmap SnapPlaque(int st, float amp, double t)
        {
            ui.Rec = st; ui.Timer = st == 1 ? "00:01:27" : "00:03:12"; ui.T = t;
            var buf = new float[2048]; Synth.Fill(buf, t, amp);
            ov.Render(ui, buf, buf.Length);
            return ov.CopyFrame();
        }
        public void SnapSkin(float k, float lever2) { ui.SkinK = k; ui.Lever2 = lever2; }
        public PlaqueForm Plaque { get { return ov; } }
        public void SnapCompact(bool on) { compact = on; ApplyLayout(); }
        public void SnapCorner(float l, float r) { ui.CornL = l; ui.CornR = r; }   // v16
        public float SnapScale { get { return s; } }
        public Bitmap SnapMask()
        {
            var b = Snap(0, 1f, 1f, Hit.None, Hit.None, 0f, "", false, 10.0);
            using (var g = Graphics.FromImage(b)) CompactView.DrawMask(g, s);
            return b;
        }
        public void SetScale(float sc)
        {
            s = sc;
            ApplyLayout();
            scope = new Scope(); scope.Ensure((int)Math.Round(Lay.Screen.Width * s), (int)Math.Round(Lay.Screen.Height * s), s);
            ov.Dispose(); ov = new PlaqueForm(s);
        }
    }

    // ---------- Запуск ----------
    static class Program
    {
        public static bool SnapshotMode;
        public static bool CaptureTestMode;   // v13

        [STAThread]
        static int Main(string[] args)
        {
            try { Native.SetProcessDPIAware(); } catch { }
            using (var g = Graphics.FromHwnd(IntPtr.Zero)) Theme.K = g.DpiX / 96f;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            foreach (var a in args) if (a.Trim().ToLower() == "--selftest") return SelfTest();
            foreach (var a in args) if (a.Trim().ToLower() == "--selftest-audio") return SelfTestAudio();
            foreach (var a in args) if (a.Trim().ToLower() == "--snapshot") return Snapshot();
            foreach (var a in args) if (a.Trim().ToLower() == "--mictest") return MicTest();
            for (int i = 0; i < args.Length; i++)
            {
                string k = args[i].Trim().ToLower();
                if (k == "--snapshot-compact") return Snapshot11("compact");
                if (k == "--snapshot-about") return Snapshot11("about");
                if (k == "--snapshot-palettes") return SnapshotPalettes();   // v19
                if (k == "--snapshot-toast") return SnapshotToast(i + 1 < args.Length ? args[i + 1] : "test.mp4");   // v19
                if (k == "--keystest") return KeyLog.SelfTest(i + 1 < args.Length ? args[i + 1] : "test.mp4", i + 2 < args.Length && args[i + 2].Trim().ToLower() == "all");   // v19.8, v19.12: all — «все нажатия»
                if (k == "--snapshot-mousekeys") return SnapshotMouseKeys();   // v19.12
                if (k == "--snapshot-region") return Snapshot11("region");
                if (k == "--regiontest") return RegionTest(i + 1 < args.Length ? args[i + 1] : "100,100,640,360");
                if (k == "--capturetest") return CaptureTest(i + 1 < args.Length ? args[i + 1].Trim().ToLower() : "full", i + 2 < args.Length ? args[i + 2] : null);   // v13
            }
            // v19.4: обрезать уже записанный ролик — «XYZ Запись.exe --trim файл» или просто путь к видео (меню Проводника, перетаскивание на exe)
            for (int i = 0; i < args.Length; i++)
            {
                string a0 = args[i].Trim(), f0 = null;
                if (a0.ToLower() == "--trim") { if (i + 1 < args.Length) f0 = args[i + 1]; }
                else if (TrimFiles.IsVideo(a0) && File.Exists(a0)) f0 = a0;
                if (f0 != null)
                {
                    try { Theme.ApplyPal(Settings.Load().Get("palette", "mint")); } catch { }
                    if (!File.Exists(f0)) { MessageBox.Show("Файл не найден:\n" + f0, "XYZ Запись"); return 1; }
                    if (Engine.FindFfmpeg() == null) { MessageBox.Show("Не найден ffmpeg — положите папку ffmpeg рядом с программой.", "XYZ Запись"); return 1; }
                    Application.Run(new TrimForm(f0));
                    return 0;
                }
            }
            try { var ct = new Thread(() => Engine.CleanRaw(7)); ct.IsBackground = true; ct.Start(); } catch { }   // v11
            bool created;
            using (var mx = new Mutex(true, "XYZ_Recorder_single", out created))
            {
                if (!created)
                {
                    // уже запущена (возможно, в трее) — просим показать прибор
                    int msg = Native.RegisterWindowMessage("XYZ_Recorder_v4_show");
                    Native.PostMessage((IntPtr)0xFFFF, msg, IntPtr.Zero, IntPtr.Zero);
                    return 0;
                }
                Application.ThreadException += (s, e) => MessageBox.Show(e.Exception.ToString(), "XYZ Запись — ошибка");
                try { if (TrimFiles.ShellOn()) TrimFiles.SetShell(true); } catch { }   // v19.5: обновить пункт Проводника (путь exe, «Открыть с помощью»)
                Application.Run(new MainForm());
            }
            return 0;
        }

        static void Save(Bitmap b, string dir, string name) { if (b == null) return; b.Save(Path.Combine(dir, name), ImageFormat.Png); b.Dispose(); }

        // Ключ --snapshot (v8): кадры прибора в test\v8_*.png (окно не показывается), кадр ARGB с прозрачностью.
        static int SnapshotMouseKeys()   // v19.12: окно «Мышь и клавиатура» в PNG (настройки из ini, не сохраняются)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", "mk19"); Directory.CreateDirectory(dir);
            Settings.ReadOnly = true; var cfg = Settings.Load();
            try { Theme.ApplyPal(cfg.Get("palette", "mint")); } catch { }
            using (var frm = new MouseKeysForm(cfg))
            {
                frm.Opacity = 0.01; frm.Show(); Application.DoEvents();
                frm.Location = new Point(-5000, -5000); frm.Opacity = 1; frm.Refresh(); Application.DoEvents(); Thread.Sleep(150); Application.DoEvents();
                using (var bm = new Bitmap(frm.Width, frm.Height)) { frm.DrawToBitmap(bm, new Rectangle(0, 0, frm.Width, frm.Height)); bm.Save(Path.Combine(dir, "mousekeys.png")); }
                frm.Close();
            }
            return 0;
        }

        static int SnapshotToast(string mp4)   // v19: окошко «Сохранено» и «Обрезать» в каждом облике — в PNG
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", "toast19");
            Directory.CreateDirectory(dir);
            var log = new StringBuilder();
            foreach (var id in new[] { "mint", "noir", "blackred" })
            {
                try
                {
                    Theme.ApplyPal(id);
                    foreach (var frm in new Form[] { new SavedToast(mp4, "Сохранено", false, false), new TrimForm(mp4) })
                    {
                        using (frm)
                        {
                            frm.StartPosition = FormStartPosition.Manual; frm.Opacity = 0.01; frm.Show(); Application.DoEvents();
                            frm.Location = new Point(-5000, -5000); frm.Opacity = 1; frm.Refresh(); Application.DoEvents(); Thread.Sleep(150); Application.DoEvents();
                            var tfm = frm as TrimForm;
                            if (tfm != null)
                            {
                                for (int w8 = 0; w8 < 300 && (!tfm.Ready || !tfm.FramesDone); w8++) { Thread.Sleep(100); Application.DoEvents(); }
                                double D = tfm.Dur; tfm.Cuts.Add(new[] { D * 0.05, D * 0.15 }); tfm.SelA = D * 0.55; tfm.SelB = D * 0.7; tfm.Head = D * 0.55; tfm.UpdInfo();
                                frm.Refresh(); Application.DoEvents();
                            }
                            using (var bm = new Bitmap(frm.Width, frm.Height)) { frm.DrawToBitmap(bm, new Rectangle(0, 0, frm.Width, frm.Height)); bm.Save(Path.Combine(dir, id + (frm is TrimForm ? "_trim.png" : "_toast.png"))); }
                            frm.Close();
                        }
                    }
                    log.AppendLine("ok " + id);
                }
                catch (Exception ex) { log.AppendLine("ERR " + id + " " + ex); }
            }
            File.WriteAllText(Path.Combine(dir, "toast.log"), log.ToString());
            return 0;
        }

        static int SnapshotPalettes()   // v19: каждый облик — корпус в покое и при записи, рамка в покое
        {
            SnapshotMode = true;
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", "pal19");
            Directory.CreateDirectory(dir);
            var log = new StringBuilder("palettes v19 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            try
            {
                using (var f = new MainForm())
                {
                    f.SnapPrep();
                    f.SetScale(1f);
                    foreach (var p in Theme.Pals)
                    {
                        Theme.ApplyPal(p.Id);
                        f.SnapRecK = 1f;
                        f.SnapSkin(0f, 1f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, p.Id + "_idle.png");
                        Save(f.Snap(1, 1f, 1f, Hit.None, Hit.None, 0.9f, "", false, 12.8125), dir, p.Id + "_rec.png");
                        f.SnapSkin(1f, 0f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, p.Id + "_frame.png");
                        log.AppendLine("ok " + p.Id);
                    }
                }
            }
            catch (Exception ex) { log.AppendLine("ERR " + ex); }
            File.WriteAllText(Path.Combine(dir, "palettes.log"), log.ToString());
            return 0;
        }

        static int Snapshot()
        {
            SnapshotMode = true;
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test", "snap8");
            Directory.CreateDirectory(dir);
            string logf = Path.Combine(dir, "snapshot_v8.log");
            var log = new StringBuilder("snapshot v8 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", DPI k=" + Theme.K + "\r\n");
            try
            {
                log.AppendLine("fonts: digits=" + Theme.DigitFam.Name + ", ui=" + Theme.UiFam.Name + ", semi=" + Theme.UiSemi.Name);
                foreach (var r in new[] { "body", "rim", "frame", "outline", "emblem", "nut", "pad", "gloss" })
                    log.AppendLine("res " + r + " @1x=" + (Res.Get(r, 1f) != null) + " @2x=" + (Res.Get(r, 2f) != null));
                using (var f = new MainForm())
                {
                    f.SnapPrep();
                    float[] scales = { 1f, 2f, 1.25f };
                    foreach (float sc in scales)
                    {
                        f.SetScale(sc);
                        string suf = sc == 1f ? "" : sc == 2f ? "@2x" : "@125";
                        f.SnapRecK = 1f;
                        f.SnapSkin(0f, 1f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "v8_body_idle" + suf + ".png");
                        Save(f.Snap(1, 1f, 1f, Hit.None, Hit.None, 0.9f, "", false, 12.8125), dir, "v8_body_rec" + suf + ".png");
                        f.SnapSkin(1f, 0f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "v8_frame_idle" + suf + ".png");
                        Save(f.Snap(1, 1f, 1f, Hit.None, Hit.None, 0.9f, "", false, 12.8125), dir, "v8_frame_rec" + suf + ".png");
                        if (sc == 1.25f) continue;
                        Save(f.Snap(2, 1f, 1f, Hit.None, Hit.None, 0.12f, "", false, 12.8125), dir, "v8_frame_paused" + suf + ".png");
                        Save(f.Snap(0, 1f, 1f, Hit.Skin, Hit.None, 0.3f, "", false, 15.0), dir, "v8_frame_hover" + suf + ".png");
                        f.SnapSkin(0.5f, 0f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "v8_skin_mid" + suf + ".png");
                        f.SnapSkin(0.25f, 0.4f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "v8_skin_q1" + suf + ".png");
                        f.SnapSkin(0.75f, 0f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "v8_skin_q3" + suf + ".png");
                        f.SnapSkin(0f, 1f);
                        Save(f.Snap(0, 0.5f, 0.5f, Hit.None, Hit.None, 0.28f, "", false, 18.1), dir, "v8_toggle_mid" + suf + ".png");
                        Save(f.Snap(0, 0f, 0f, Hit.None, Hit.None, 0f, "", false, 18.2), dir, "v8_toggle_down" + suf + ".png");
                        f.SnapSkin(1f, 0f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.3f, Engine.MicNoAccess, true, 15.5), dir, "v8_frame_warn" + suf + ".png");
                        f.SnapSkin(0f, 1f);
                    }
                    try
                    {
                        var m = f.BuildBodyMenu();
                        var sz = m.GetPreferredSize(Size.Empty); m.Size = sz;
                        var b = new Bitmap(sz.Width, sz.Height);
                        m.DrawToBitmap(b, new Rectangle(0, 0, sz.Width, sz.Height));
                        Save(b, dir, "v8_menu_body.png"); m.Dispose();
                    }
                    catch (Exception ex) { log.AppendLine("menu: " + ex.Message); }
                    log.Append(f.Log);
                }
                log.AppendLine("ok");
                File.WriteAllText(logf, log.ToString(), Encoding.UTF8);
                return 0;
            }
            catch (Exception ex) { log.AppendLine("FAIL " + ex); try { File.WriteAllText(logf, log.ToString(), Encoding.UTF8); } catch { } return 1; }
        }
        // ---------- v11: снимки без показа окон (test\snap11) ----------
        static int Snapshot11(string which)
        {
            SnapshotMode = true; Settings.ReadOnly = true;
            string dir = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test"), "snap18");
            Directory.CreateDirectory(dir);
            string logf = Path.Combine(dir, "snapshot_v11_" + which + ".log");
            var log = new StringBuilder("snapshot v11 " + which + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", DPI k=" + Theme.K + "\r\n");
            try
            {
                if (which == "compact")
                {
                    using (var f = new MainForm())
                    {
                        f.SnapPrep();
                        foreach (float sc in new[] { 1f, 2f, 1.25f })
                        {
                            f.SnapCompact(true); f.SetScale(sc);
                            string suf = sc == 1f ? "" : sc == 2f ? "@2x" : "@125";
                            f.SnapRecK = 1f;
                            Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "c_idle" + suf + ".png");
                            Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.002f, "", false, 10.0), dir, "c_idle_silence" + suf + ".png");   // v13: вход жив, тишина
                            Save(f.Snap(1, 1f, 1f, Hit.None, Hit.None, 0.002f, "", false, 12.8125), dir, "c_rec_silence" + suf + ".png");   // v13
                            Save(f.Snap(0, 0f, 0f, Hit.None, Hit.None, 0f, "", false, 10.0), dir, "c_off" + suf + ".png");             // v12
                            Save(f.Snap(0, 1f, 1f, Hit.CRec, Hit.None, 0.28f, "", false, 10.0), dir, "c_hover_rec" + suf + ".png");
                            Save(f.Snap(0, 1f, 1f, Hit.CStop, Hit.None, 0.28f, "", false, 10.0), dir, "c_hover_stop_idle" + suf + ".png");   // v14
                            Save(f.Snap(1, 1f, 1f, Hit.None, Hit.None, 0.5f, "", false, 12.8125), dir, "c_rec" + suf + ".png");
                            Save(f.Snap(2, 1f, 1f, Hit.None, Hit.None, 0.12f, "", false, 12.8125), dir, "c_paused" + suf + ".png");
                            // v16: угловые кнопки — наведение на «свернуть» (янтарь), на «закрыть» (красный), курсор в углу, полпути fade, покой
                            f.SnapCorner(1f, 0f); Save(f.Snap(0, 1f, 1f, Hit.CMin, Hit.None, 0.28f, "", false, 10.0), dir, "c_corner_min" + suf + ".png");
                            f.SnapCorner(0f, 1f); Save(f.Snap(0, 1f, 1f, Hit.CClose, Hit.None, 0.28f, "", false, 10.0), dir, "c_corner_close" + suf + ".png");
                            f.SnapCorner(1f, 0f); Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "c_corner_zone_min" + suf + ".png");
                            f.SnapCorner(0f, 1f); Save(f.Snap(1, 1f, 1f, Hit.CClose, Hit.None, 0.5f, "", false, 12.8125), dir, "c_corner_close_rec" + suf + ".png");
                            f.SnapCorner(0f, 0.5f); Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "c_corner_fade" + suf + ".png");
                            f.SnapCorner(0f, 0f); Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "c_corner_rest" + suf + ".png");
                            log.AppendLine("scale " + sc + ": " + CompactView.Info);
                            log.AppendLine("  хит-тест: " + CompactView.Report(sc));
                            if (sc == 1.25f) continue;
                            Save(f.Snap(0, 1f, 1f, Hit.CRec, Hit.CRec, 0.28f, "", false, 10.0), dir, "c_down_rec" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CPause, Hit.None, 0.5f, "", false, 11.5), dir, "c_hover_pause" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CStop, Hit.None, 0.5f, "", false, 11.5), dir, "c_hover_stop" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CStop, Hit.CStop, 0.5f, "", false, 11.5), dir, "c_down_stop" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CPause, Hit.CPause, 0.5f, "", false, 11.5), dir, "c_down_pause" + suf + ".png");   // v17
                            Save(f.Snap(2, 1f, 1f, Hit.CPause, Hit.None, 0.12f, "", false, 12.8125), dir, "c_hover_pause_paused" + suf + ".png");
                            Save(f.SnapProc(0.42f, 14.0), dir, "c_saving" + suf + ".png");
                            Save(f.SnapMask(), dir, "c_mask" + suf + ".png");
                            // v17: облик «Рамка» — компакт без плашки (кадры с прозрачностью, фон подкладывается при склейке)
                            f.SnapSkin(1f, 0f);
                            Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "b_idle" + suf + ".png");
                            Save(f.Snap(0, 1f, 1f, Hit.CRec, Hit.None, 0.28f, "", false, 10.0), dir, "b_hover_rec" + suf + ".png");
                            Save(f.Snap(0, 1f, 1f, Hit.CRec, Hit.CRec, 0.28f, "", false, 10.0), dir, "b_down_rec" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CStop, Hit.None, 0.5f, "", false, 11.5), dir, "b_hover_stop" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CStop, Hit.CStop, 0.5f, "", false, 11.5), dir, "b_down_stop" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.None, Hit.None, 0.5f, "", false, 12.8125), dir, "b_rec" + suf + ".png");
                            Save(f.Snap(2, 1f, 1f, Hit.None, Hit.None, 0.12f, "", false, 12.8125), dir, "b_paused" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CPause, Hit.None, 0.5f, "", false, 11.5), dir, "b_hover_pause" + suf + ".png");
                            Save(f.Snap(1, 1f, 1f, Hit.CPause, Hit.CPause, 0.5f, "", false, 11.5), dir, "b_down_pause" + suf + ".png");
                            f.SnapCorner(1f, 0f); Save(f.Snap(0, 1f, 1f, Hit.CMin, Hit.None, 0.28f, "", false, 10.0), dir, "b_corner_min" + suf + ".png");
                            f.SnapCorner(0f, 1f); Save(f.Snap(0, 1f, 1f, Hit.CClose, Hit.None, 0.28f, "", false, 10.0), dir, "b_corner_close" + suf + ".png");
                            f.SnapCorner(1f, 0f); Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "b_corner_zone_min" + suf + ".png");
                            f.SnapCorner(0f, 0f);
                            Save(f.SnapMask(), dir, "b_mask" + suf + ".png");
                            log.AppendLine("  без плашки, хит-тест: " + CompactView.Report(sc));
                            f.SnapSkin(0f, 1f);
                            CompactView.Bare = false;
                        }
                        f.SnapCompact(false); f.SetScale(1f);
                        Save(f.Snap(0, 1f, 1f, Hit.None, Hit.None, 0.28f, "", false, 10.0), dir, "full_idle.png");
                        try
                        {
                            foreach (var kv in new[] { new KeyValuePair<string, ContextMenuStrip>("menu_body.png", f.BuildBodyMenu()), new KeyValuePair<string, ContextMenuStrip>("menu_area.png", f.BuildAreaMenu()) })
                            {
                                var m = kv.Value; var sz = m.GetPreferredSize(Size.Empty); m.Size = sz;
                                var b = new Bitmap(sz.Width, sz.Height);
                                m.DrawToBitmap(b, new Rectangle(0, 0, sz.Width, sz.Height));
                                Save(b, dir, kv.Key); m.Dispose();
                            }
                        }
                        catch (Exception ex) { log.AppendLine("menu: " + ex.Message); }
                        log.Append(f.Log);
                    }
                }
                else if (which == "about")
                {
                    foreach (float sc in new[] { 1f, 2f })
                        using (var a = new AboutForm(sc))
                        {
                            var b = new Bitmap(a.ClientSize.Width, a.ClientSize.Height, PixelFormat.Format32bppArgb);
                            using (var g = Graphics.FromImage(b)) a.Draw(g);
                            Save(b, dir, "about" + (sc == 1f ? "" : "@2x") + ".png");
                            a.Hot = AboutForm.UrlTg;
                            b = new Bitmap(a.ClientSize.Width, a.ClientSize.Height, PixelFormat.Format32bppArgb);
                            using (var g = Graphics.FromImage(b)) a.Draw(g);
                            Save(b, dir, "about_hover" + (sc == 1f ? "" : "@2x") + ".png");
                        }
                    log.AppendLine("build date: " + AboutForm.BuildDate);
                }
                else if (which == "region")
                {
                    int W = 1280, H = 720;
                    var sel = new Rectangle(300, 170, 640, 360);
                    using (var bg = FakeDesktop(W, H))
                    {
                        using (var p = new RegionPicker(new Rectangle(0, 0, W, H)))
                        {
                            p.Sel = Rectangle.Empty; p.Render();
                            using (var fr = p.CopyFrame()) Save(Over(bg, fr), dir, "region_start.png");
                            p.Sel = sel; p.Render();
                            using (var fr = p.CopyFrame()) Save(Over(bg, fr), dir, "region_pick.png");
                        }
                        var o = Rectangle.Inflate(sel, RegionFrame.T, RegionFrame.T);
                        using (var ring = new Bitmap(o.Width, o.Height, PixelFormat.Format32bppPArgb))
                        {
                            RegionFrame.DrawRing(ring, o.Width, o.Height, RegionFrame.T, Color.FromArgb(235, Theme.Red));
                            var b = new Bitmap(bg);
                            using (var g = Graphics.FromImage(b)) g.DrawImage(ring, new Rectangle(o.X, o.Y, o.Width, o.Height), 0, 0, o.Width, o.Height, GraphicsUnit.Pixel);
                            Save(b, dir, "region_rec.png");
                        }
                    }
                    log.AppendLine("virtual screen: " + SystemInformation.VirtualScreen + ", even(641x361)=" + RegionPicker.EvenRect(new Rectangle(0, 0, 641, 361)).Size);
                }
                log.AppendLine("ok");
                File.WriteAllText(logf, log.ToString(), Encoding.UTF8);
                return 0;
            }
            catch (Exception ex) { log.AppendLine("FAIL " + ex); try { File.WriteAllText(logf, log.ToString(), Encoding.UTF8); } catch { } return 1; }
        }
        static Bitmap FakeDesktop(int w, int h)
        {
            var b = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(b))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var br = new LinearGradientBrush(new Rectangle(0, 0, w, h), Color.FromArgb(52, 86, 120), Color.FromArgb(150, 170, 160), 35f)) g.FillRectangle(br, 0, 0, w, h);
                var wins = new[] { new Rectangle(120, 80, 760, 470), new Rectangle(640, 260, 520, 380) };
                for (int i = 0; i < wins.Length; i++)
                {
                    var r = wins[i];
                    using (var sh = new SolidBrush(Color.FromArgb(60, 0, 0, 0))) g.FillRectangle(sh, r.X + 6, r.Y + 8, r.Width, r.Height);
                    using (var wb = new SolidBrush(Color.FromArgb(246, 247, 249))) g.FillRectangle(wb, r);
                    using (var tb = new SolidBrush(i == 0 ? Color.FromArgb(32, 64, 110) : Color.FromArgb(70, 74, 80))) g.FillRectangle(tb, r.X, r.Y, r.Width, 30);
                    using (var tf = new Font("Segoe UI", 10f)) g.DrawString(i == 0 ? "Revit 2025 — план 1 этажа (фиктивный фон)" : "Проводник", tf, Brushes.White, r.X + 10, r.Y + 6);
                    using (var pen = new Pen(Color.FromArgb(150, 160, 170), 1f))
                        for (int k = 1; k < 9; k++) g.DrawLine(pen, r.X + 20, r.Y + 30 + k * (r.Height - 40) / 9, r.Right - 20, r.Y + 30 + k * (r.Height - 40) / 9);
                }
            }
            return b;
        }
        // Ключ --regiontest x,y,w,h: запись области 2 с без звука и без окон (тот же Engine, что у прибора) → test\snap11\region_test.mp4
        static int RegionTest(string spec)
        {
            string dir = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test"), "snap11");
            Directory.CreateDirectory(dir);
            string logf = Path.Combine(dir, "regiontest.log");
            var log = new StringBuilder("regiontest " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + spec + ", virtual=" + SystemInformation.VirtualScreen + "\r\n");
            int code = 1;
            try
            {
                var p = spec.Split(',');
                var r = new Rectangle(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]), int.Parse(p[3]));
                r = Rectangle.Intersect(r, SystemInformation.VirtualScreen);
                r = RegionPicker.EvenRect(r);
                var eng = new Engine();
                eng.Ffmpeg = Engine.FindFfmpeg();
                eng.WorkDir = Path.Combine(dir, "rt_" + DateTime.Now.ToString("HHmmss"));
                eng.Area = r;
                log.AppendLine("ffmpeg " + eng.Ffmpeg + ", area " + r);
                bool ok = eng.StartSegment();
                log.AppendLine("start " + ok + " " + eng.LastError);
                if (ok)
                {
                    Thread.Sleep(2000);
                    ok = eng.StopSegment(); log.AppendLine("stop " + ok);
                    string outF = Path.Combine(dir, "region_test.mp4");
                    ok = ok && eng.Finalize(outF); log.AppendLine("finalize " + ok + " " + (File.Exists(outF) ? new FileInfo(outF).Length.ToString() : "-"));
                    if (ok) { eng.CleanupSegments(); code = 0; }
                    else log.AppendLine(eng.LastError);
                }
            }
            catch (Exception ex) { log.AppendLine("FAIL " + ex); }
            try { File.WriteAllText(logf, log.ToString(), Encoding.UTF8); } catch { }
            return code;
        }
        // Ключ --mictest: открыть микрофон через waveIn на 1 с, записать, что вернула Windows.
        static int MicTest()
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test");
            Directory.CreateDirectory(dir);
            string logf = Path.Combine(dir, "mictest_wavein.log");
            var log = new StringBuilder();
            Action<string> W = s => { log.AppendLine(s); try { File.WriteAllText(logf, log.ToString(), Encoding.UTF8); } catch { } };
            int code = 1;
            try
            {
                W("mictest (waveIn) " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", " + (IntPtr.Size * 8) + " бит");
                string why = Engine.MicBlockReason();
                W("Реестр доступа к микрофону: " + (why ?? "запрета нет"));
                var names = WinMM.DeviceNames();
                W("waveIn устройств: " + names.Count);
                for (int i = 0; i < names.Count; i++) W("  [" + i + "] " + names[i]);
                string ff = Engine.FindFfmpeg(); string raw;
                var mics = Engine.ListMics(ff, out raw);
                var cfg = Settings.Load(); string saved = cfg.Get("mic", "");
                MicDev mic = null;
                foreach (var m in mics) { W("  dshow: " + m.Name + " → waveIn " + WinMM.Match(m.Name)); if (m.Arg == saved || m.Name == saved) mic = m; }
                if (mic == null && mics.Count > 0) mic = mics[0];
                var devs = new List<int>();
                if (mic != null) devs.Add(WinMM.Match(mic.Name));
                if (!devs.Contains(-1)) devs.Add(-1);
                bool anyOpen = false, anySignal = false; int lastErr = 0;
                foreach (int dev in devs)
                {
                    var mm = new MicMeter();
                    var t0 = Stopwatch.StartNew();
                    int r = mm.Open(dev);
                    W("waveInOpen(" + (dev == -1 ? "WAVE_MAPPER" : dev.ToString()) + ", 16 кГц моно) = " + WinMM.ErrText(r) + " за " + t0.ElapsedMilliseconds + " мс");
                    if (r != 0) { lastErr = r; continue; }
                    anyOpen = true;
                    Thread.Sleep(1000);
                    float rr, pp; mm.Read(out rr, out pp);
                    W(string.Format(System.Globalization.CultureInfo.InvariantCulture, "  за 1 с: буферов {0} (ожидается ~20), RMS макс {1:0.0} dBFS, пик {2:0.0} dBFS, ненулевые отсчёты: {3}",
                        mm.Buffers, mm.MaxRms, mm.MaxPeak, mm.AnyNonZero ? "да" : "нет"));
                    if (mm.AnyNonZero) anySignal = true;
                    mm.Close();
                    W("  закрыто");
                }
                string verdict;
                if (!anyOpen) { verdict = "доступ запрещён / устройство не открылось (" + WinMM.ErrText(lastErr) + ")" + (why != null ? " — " + why : ""); code = 2; }
                else if (!anySignal) { verdict = "устройство открылось, но Windows отдаёт ровную тишину" + (why != null ? " — доступ запрещён: " + why : " (микрофон выключен или без звука)"); code = 3; }
                else { verdict = "звук идёт — индикатор будет живым"; code = 0; }
                W("Приложение покажет: " + (code == 0 ? "живую полоску" : "серую полоску и «" + Engine.MicNoAccess + "»"));
                W("ИТОГ: " + verdict);
            }
            catch (Exception ex) { W("FAIL исключение: " + ex); code = 9; }
            return code;
        }

        // ---------- v7: --selftest — прибор НЕ скрывается при записи и исключён из захвата ----------
        public static bool SelfTestMode;
        public static Action<string> StLogger;
        public static void StLog(string s) { if (StLogger != null) StLogger(s); }
        static void Pump(int ms) { var t = Stopwatch.StartNew(); while (t.ElapsedMilliseconds < ms) { Application.DoEvents(); Thread.Sleep(10); } }
        static Bitmap Grab(Rectangle r)
        {
            var b = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(b)) g.CopyFromScreen(r.Location, Point.Empty, r.Size, CopyPixelOperation.SourceCopy);
            return b;
        }
        static int[] Px(Bitmap b)
        {
            var d = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var a = new int[b.Width * b.Height];
            for (int y = 0; y < b.Height; y++) Marshal.Copy(d.Scan0 + y * d.Stride, a, y * b.Width, b.Width);
            b.UnlockBits(d); return a;
        }
        // Средняя разница RGB (0…255) двух картинок по маске (непрозрачные пиксели прибора).
        static double Diff(int[] a, int[] b, int[] mask)
        {
            double s = 0; long n = 0;
            for (int i = 0; i < a.Length && i < b.Length && i < mask.Length; i++)
            {
                if (((mask[i] >> 24) & 255) < 250) continue;
                int x = a[i], y = b[i];
                s += Math.Abs(((x >> 16) & 255) - ((y >> 16) & 255)) + Math.Abs(((x >> 8) & 255) - ((y >> 8) & 255)) + Math.Abs((x & 255) - (y & 255));
                n++;
            }
            return n == 0 ? -1 : s / (3.0 * n);
        }
        static Bitmap Crop(Bitmap src, Rectangle r)
        {
            var b = new Bitmap(r.Width, r.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(b)) { g.InterpolationMode = InterpolationMode.HighQualityBicubic; g.DrawImage(src, new Rectangle(0, 0, r.Width, r.Height), r, GraphicsUnit.Pixel); }
            return b;
        }
        static Bitmap Over(Bitmap under, Bitmap top)
        {
            var b = new Bitmap(under.Width, under.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(b)) { g.DrawImageUnscaled(under, 0, 0); g.DrawImage(top, new Rectangle(0, 0, under.Width, under.Height), 0, 0, top.Width, top.Height, GraphicsUnit.Pixel); }
            return b;
        }
        static Bitmap FrameAt(string ff, string mp4, double t, string png)
        {
            string o;
            int rc = Engine.Run(ff, "-hide_banner -y -ss " + t.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + " -i \"" + mp4 + "\" -frames:v 1 \"" + png + "\"", out o, 60000);
            if (rc != 0 || !File.Exists(png)) { StLog("  кадр не извлечён (rc=" + rc + "): " + (o.Length > 600 ? o.Substring(o.Length - 600) : o)); return null; }
            using (var im = Image.FromFile(png)) return new Bitmap(im);
        }
        // Запись через настоящий StartRec/StopRec; возвращает путь к mp4 или null.
        static string RecordOnce(MainForm f, int ms, Action duringRec)
        {
            f.StStart();
            if (!f.StRec) { StLog("  запись не началась: " + f.StatusText); return null; }
            var t = Stopwatch.StartNew();
            Pump(Math.Min(ms, 900));
            if (duringRec != null) duringRec();
            Pump(Math.Max(0, ms - (int)t.ElapsedMilliseconds));
            f.StStop();
            var w = Stopwatch.StartNew();
            while (!f.StIdle && w.ElapsedMilliseconds < 120000) Pump(50);
            if (!f.StIdle) { StLog("  сохранение не закончилось за 120 с"); return null; }
            return f.OutFile;
        }

        static int SelfTest()
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test");
            Directory.CreateDirectory(dir);
            string logf = Path.Combine(dir, "selftest_v7.log");
            var log = new StringBuilder();
            Action<string> W = s => { log.AppendLine(s); try { File.WriteAllText(logf, log.ToString(), Encoding.UTF8); } catch { } };
            StLogger = W;
            SelfTestMode = true; Settings.ReadOnly = true;
            var IC = System.Globalization.CultureInfo.InvariantCulture;
            bool pass = false;
            try
            {
                W("selftest v7 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", DPI k=" + Theme.K);
                try
                {
                    var rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                    W("Windows: " + rk.GetValue("ProductName") + " build " + rk.GetValue("CurrentBuildNumber") + " (" + rk.GetValue("DisplayVersion") + ")");
                }
                catch { }
                var vs = SystemInformation.VirtualScreen;
                W("VirtualScreen: " + vs + ", мониторов: " + Screen.AllScreens.Length);
                string ff = Engine.FindFfmpeg(); W("ffmpeg: " + ff);
                if (ff == null) { W("FAIL: нет ffmpeg"); return 2; }
                var ini = Settings.Load();
                W("ini пользователя (не меняется): showInRec=" + ini.Get("showInRec", "(нет → 0)") + ", foldRec=" + ini.Get("foldRec", "(нет → 0)"));
                W("Проверка — по умолчанию: showInRec=0, foldRec=0; весь экран; без звука; окно поверх всех, без активации.");

                using (var f = new MainForm())
                {
                    f.StPrep(dir, false);
                    var wr = Rectangle.Intersect(new Rectangle(f.Location, f.Size), vs);
                    W("Окно прибора: " + wr);
                    var desk0 = Grab(wr);                      // рабочий стол под прибором до показа
                    f.StShow(); Pump(700);
                    uint a0; Native.GetWindowDisplayAffinity(f.Handle, out a0);
                    W("До записи: окно видно=" + Native.IsWindowVisible(f.Handle) + ", affinity=0x" + a0.ToString("X"));

                    // --- запись 1: по умолчанию (прибор исключён) ---
                    bool visRec = false, ovRec = true; uint aRec = 0, aMenu = 0, aMain2 = 0; Bitmap dev = null, gdi = null; float recK = 0; string stRec = "";
                    W("Запись 1 (по умолчанию), 3 с…");
                    string mp1 = RecordOnce(f, 3000, () =>
                    {
                        visRec = f.Visible && Native.IsWindowVisible(f.Handle) && f.Alpha == 255;
                        ovRec = f.OvShown;
                        Native.GetWindowDisplayAffinity(f.Handle, out aRec);
                        using (var m = f.BuildBodyMenu()) aMenu = MainForm.ApplyMenuAffinity(m);   // тот же вызов, что в Opening/Opened
                        Pump(400);
                        recK = f.RecK; stRec = f.StatusText;
                        dev = f.CopyFrame();
                        gdi = Grab(wr);                         // GDI-снимок экрана во время записи
                        Native.GetWindowDisplayAffinity(f.Handle, out aMain2);
                    });
                    uint aStop; Native.GetWindowDisplayAffinity(f.Handle, out aStop);
                    W("  во время записи: окно НЕ скрыто=" + visRec + " (плашка показана=" + ovRec + "), переход RecK=" + recK.ToString("0.00", IC) + ", строка состояния «" + stRec + "»");
                    W("  GetWindowDisplayAffinity главного окна = 0x" + aRec.ToString("X") + " (повторно 0x" + aMain2.ToString("X") + "), меню корпуса = 0x" + aMenu.ToString("X"));
                    W("  после «Стоп»: affinity = 0x" + aStop.ToString("X") + ", окно видно=" + Native.IsWindowVisible(f.Handle) + ", ролик: " + mp1);

                    // --- запись 2: контроль — «Показывать прибор в записи» ВКЛ (прибор должен попасть в кадр) ---
                    uint aCtl = 99;
                    f.StShowInRec = true;
                    W("Запись 2 (контроль: «Показывать прибор в записи» ВКЛ), 1,8 с…");
                    string mp2 = RecordOnce(f, 1800, () => { Native.GetWindowDisplayAffinity(f.Handle, out aCtl); });
                    W("  affinity во время контрольной записи = 0x" + aCtl.ToString("X") + ", ролик: " + mp2);
                    f.Hide();

                    // --- сравнение кадров ---
                    Bitmap fr1 = mp1 == null ? null : FrameAt(ff, mp1, 1.6, Path.Combine(dir, "v7_frame_excl_full.png"));
                    Bitmap fr2 = mp2 == null ? null : FrameAt(ff, mp2, 1.0, Path.Combine(dir, "v7_frame_ctl_full.png"));
                    bool capOk = false, ctlOk = false;
                    if (fr1 != null && dev != null)
                    {
                        double k = fr1.Width / (double)vs.Width;
                        var cr = new Rectangle((int)Math.Round((wr.X - vs.X) * k), (int)Math.Round((wr.Y - vs.Y) * k), (int)Math.Round(wr.Width * k), (int)Math.Round(wr.Height * k));
                        cr = Rectangle.Intersect(cr, new Rectangle(0, 0, fr1.Width, fr1.Height));
                        W("Кадр mp4: " + fr1.Width + "×" + fr1.Height + ", масштаб " + k.ToString("0.000", IC) + ", область окна в кадре " + cr);
                        var devWr = new Bitmap(wr.Width, wr.Height, PixelFormat.Format32bppArgb);
                        using (var g = Graphics.FromImage(devWr)) g.DrawImage(dev, new Rectangle(0, 0, wr.Width, wr.Height), (wr.X - f.Left), (wr.Y - f.Top), wr.Width, wr.Height, GraphicsUnit.Pixel);
                        var expVisible = Over(desk0, devWr);    // как выглядел бы кадр, если бы прибор попал в запись
                        int[] mask = Px(devWr), pDesk = Px(desk0), pVis = Px(expVisible);
                        var c1 = Crop(fr1, cr); var c1s = new Bitmap(c1, wr.Width, wr.Height); int[] p1 = Px(c1s);
                        double d1Desk = Diff(p1, pDesk, mask), d1Dev = Diff(p1, pVis, mask);
                        double gDesk = gdi == null ? -1 : Diff(Px(gdi), pDesk, mask), gDev = gdi == null ? -1 : Diff(Px(gdi), pVis, mask);
                        W(string.Format(IC, "  запись 1, область окна: разница с рабочим столом {0:0.0}, с прибором {1:0.0} (0…255, по непрозрачным пикселям корпуса)", d1Desk, d1Dev));
                        W(string.Format(IC, "  GDI-снимок экрана во время записи: с рабочим столом {0:0.0}, с прибором {1:0.0}", gDesk, gDev));
                        capOk = d1Desk >= 0 && d1Dev > 0 && d1Desk < d1Dev * 0.5;
                        Bitmap c2s = null;
                        if (fr2 != null)
                        {
                            var c2 = Crop(fr2, cr); c2s = new Bitmap(c2, wr.Width, wr.Height); c2.Dispose();
                            int[] p2 = Px(c2s);
                            double d2Desk = Diff(p2, pDesk, mask), d2Dev = Diff(p2, pVis, mask);
                            W(string.Format(IC, "  запись 2 (контроль), область окна: с рабочим столом {0:0.0}, с прибором {1:0.0}", d2Desk, d2Dev));
                            ctlOk = d2Dev >= 0 && d2Dev < d2Desk * 0.5;
                        }
                        // картинки
                        Save(new Bitmap(desk0), dir, "v7_desk_before.png");
                        Save(new Bitmap(devWr), dir, "v7_device_rec.png");
                        Save(new Bitmap(c1s), dir, "v7_crop_excl.png");
                        if (c2s != null) Save(new Bitmap(c2s), dir, "v7_crop_ctl.png");
                        if (gdi != null) Save(new Bitmap(gdi), dir, "v7_gdi_during_rec.png");
                        // сводная картинка 2×2
                        int W2 = wr.Width, H2 = wr.Height, pad = 12, lab = 26;
                        using (var cmp = new Bitmap(W2 * 2 + pad * 3, (H2 + lab) * 2 + pad * 3, PixelFormat.Format32bppArgb))
                        {
                            using (var g = Graphics.FromImage(cmp))
                            using (var fnt = new Font("Segoe UI", 11f, FontStyle.Bold, GraphicsUnit.Pixel))
                            using (var wb = new SolidBrush(Color.White))
                            {
                                g.Clear(Color.FromArgb(30, 32, 36)); g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                Bitmap[] ims = { desk0, expVisible, c1s, c2s };
                                string[] ls = { "1. Рабочий стол под прибором (до показа)", "2. Прибор на экране во время записи (так видит db)",
                                    string.Format(IC, "3. Кадр mp4, запись по умолчанию: {0}", capOk ? "прибора НЕТ — виден рабочий стол" : "ПРИБОР ВИДЕН"),
                                    string.Format(IC, "4. Кадр mp4, контроль «Показывать прибор в записи»: {0}", ctlOk ? "прибор виден" : "?") };
                                for (int i = 0; i < 4; i++)
                                {
                                    int x = pad + (i % 2) * (W2 + pad), y = pad + (i / 2) * (H2 + lab + pad);
                                    g.DrawString(ls[i], fnt, wb, x, y + 4);
                                    if (ims[i] != null) g.DrawImage(ims[i], x, y + lab, W2, H2);
                                    using (var pen = new Pen(i == 2 ? (capOk ? Color.FromArgb(95, 211, 192) : Color.FromArgb(255, 72, 64)) : Color.FromArgb(70, 76, 84), 2)) g.DrawRectangle(pen, x - 1, y + lab - 1, W2 + 1, H2 + 1);
                                }
                            }
                            cmp.Save(Path.Combine(dir, "v7_capture_compare.png"), ImageFormat.Png);
                        }
                        W("Картинки: v7_capture_compare.png (сводка), v7_crop_excl.png, v7_crop_ctl.png, v7_desk_before.png, v7_device_rec.png, v7_gdi_during_rec.png, v7_frame_*_full.png");
                    }
                    bool affOk = aRec == Native.WDA_EXCLUDEFROMCAPTURE && aMenu == Native.WDA_EXCLUDEFROMCAPTURE && aStop == 0 && aCtl == 0;
                    pass = visRec && !ovRec && affOk && capOk;
                    W(string.Format("ИТОГ: окно не скрыто={0}, affinity 0x11 в записи (окно и меню)={1}, после Стоп 0={2}, в ролике вместо прибора рабочий стол={3}, контроль (прибор виден при ВКЛ)={4} -> {5}",
                        visRec && !ovRec, aRec == 0x11 && aMenu == 0x11, aStop == 0, capOk, ctlOk, pass ? "PASS" : "FAIL"));
                }
            }
            catch (Exception ex) { W("FAIL исключение: " + ex); }
            return pass ? 0 : 1;
        }

        // ---------- v13: --capturetest full|compact|plate|region <out.mp4> ----------
        // Показывает прибор (режим mode) в правом нижнем углу основного монитора, пишет ~3 с тем же модулем захвата (StartRec/StopRec,
        // без звука), закрывается. Рядом с out.mp4: out.png (кадр 2 с), out_desk.png (под прибором до показа), out_crop.png (место прибора
        // в кадре), out_dev.png (прибор, как его видит db), out_cmp.png (сводка), out.log, out_ffmpeg.log. Код 0 — в кадре прибора нет.
        static int CaptureTest(string mode, string outMp4)
        {
            if (mode != "full" && mode != "compact" && mode != "plate" && mode != "region") mode = "full";
            if (string.IsNullOrEmpty(outMp4)) outMp4 = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test"), "capture_" + mode + ".mp4");
            outMp4 = Path.GetFullPath(outMp4);
            string dir = Path.GetDirectoryName(outMp4), bn = Path.Combine(dir, Path.GetFileNameWithoutExtension(outMp4));
            Directory.CreateDirectory(dir);
            var log = new StringBuilder();
            Action<string> W = x => { log.AppendLine(x); try { File.WriteAllText(bn + ".log", log.ToString(), Encoding.UTF8); } catch { } };
            StLogger = W;
            SelfTestMode = true; CaptureTestMode = true; Settings.ReadOnly = true; AppLog.Tag = " capturetest " + mode;
            var IC = System.Globalization.CultureInfo.InvariantCulture;
            int code = 1;
            try
            {
                W("capturetest " + mode + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", DPI k=" + Theme.K + ", мониторов " + Screen.AllScreens.Length + ", основной " + Screen.PrimaryScreen.Bounds);
                string derr; var outs = Dxgi.Outputs(out derr);
                W("DXGI адаптер 0: " + string.Join("; ", outs.ConvertAll(o => o.ToString()).ToArray()) + (derr != null ? " (" + derr + ")" : ""));
                string ff = Engine.FindFfmpeg(); W("ffmpeg: " + ff);
                if (ff == null) { W("FAIL: нет ffmpeg"); return 2; }
                string tmpOut = Path.Combine(Path.GetTempPath(), "xyz_capturetest_" + DateTime.Now.ToString("HHmmss"));
                Directory.CreateDirectory(tmpOut);
                using (var f = new MainForm())
                {
                    f.CtPrep(mode, tmpOut);
                    var dr = Rectangle.Intersect(f.CtDeviceRect(), SystemInformation.VirtualScreen);
                    W("Место прибора: " + dr);
                    var desk0 = Grab(dr);                       // что под прибором до показа
                    if (mode != "plate") { f.StShow(); Pump(500); }
                    Bitmap dev = null; uint aMain = 0xFFFF, aOv = 0xFFFF, aFr = 0xFFFF; string capMode = ""; Rectangle capR = Rectangle.Empty; bool frVis = false;
                    W("Запись 3 с…");
                    string mp = RecordOnce(f, 3000, () =>
                    {
                        Pump(300);
                        dev = f.CtDeviceFrame();
                        Native.GetWindowDisplayAffinity(f.Handle, out aMain);
                        if (f.CtOvHandle != IntPtr.Zero) Native.GetWindowDisplayAffinity(f.CtOvHandle, out aOv);
                        if (f.CtFrameHandle != IntPtr.Zero) Native.GetWindowDisplayAffinity(f.CtFrameHandle, out aFr);
                        frVis = f.CtFrameVisible;
                        capMode = f.CtEngine.CapMode; capR = f.CtEngine.CapRect;
                    });
                    f.Hide();
                    W("  захват: " + capMode + ", кадр (экран) " + capR + ", ролик " + mp);
                    W("  affinity во время записи: прибор=0x" + aMain.ToString("X") + (mode == "plate" ? " (скрыт)" : "") + ", плашка=0x" + aOv.ToString("X") + (mode == "region" ? ", рамка области=0x" + aFr.ToString("X") + " видна=" + frVis : ""));
                    try { var wd = f.CtEngine.WorkDir; if (wd != null && File.Exists(Path.Combine(wd, "ffmpeg.log"))) File.Copy(Path.Combine(wd, "ffmpeg.log"), bn + "_ffmpeg.log", true); } catch { }
                    if (mp == null || !File.Exists(mp)) { W("FAIL: ролик не записан"); return 3; }
                    File.Copy(mp, outMp4, true);
                    try { File.Delete(mp); Directory.Delete(tmpOut, true); } catch { }
                    var fr = FrameAt(ff, outMp4, 2.0, bn + ".png");
                    if (fr == null || dev == null) { W("FAIL: нет кадра"); return 4; }
                    W("Кадр mp4: " + fr.Width + "×" + fr.Height);
                    // место прибора в кадре: кадр начинается в capR.Location (масштаб 1:1, чётные размеры)
                    var cr = new Rectangle(dr.X - capR.X, dr.Y - capR.Y, dr.Width, dr.Height);
                    var crIn = Rectangle.Intersect(cr, new Rectangle(0, 0, fr.Width, fr.Height));
                    if (crIn.Width != dr.Width || crIn.Height != dr.Height) W("  ВНИМАНИЕ: прибор частично вне кадра: " + cr + " ∩ кадр = " + crIn);
                    var crop = new Bitmap(dr.Width, dr.Height, PixelFormat.Format32bppArgb);
                    using (var g = Graphics.FromImage(crop)) { g.Clear(Color.Magenta); g.DrawImage(fr, new Rectangle(crIn.X - cr.X, crIn.Y - cr.Y, crIn.Width, crIn.Height), crIn, GraphicsUnit.Pixel); }
                    var devR = new Bitmap(dr.Width, dr.Height, PixelFormat.Format32bppArgb);
                    var full = f.CtDeviceRect();
                    using (var g = Graphics.FromImage(devR)) g.DrawImage(dev, new Rectangle(0, 0, dr.Width, dr.Height), dr.X - full.X, dr.Y - full.Y, dr.Width, dr.Height, GraphicsUnit.Pixel);
                    var expVis = Over(desk0, devR);
                    int[] mask = Px(devR);
                    double dDesk = Diff(Px(crop), Px(desk0), mask), dDev = Diff(Px(crop), Px(expVis), mask);
                    long nm = 0; foreach (int v in mask) if (((v >> 24) & 255) >= 250) nm++;
                    W(string.Format(IC, "  место прибора в кадре ({0} непрозрачных px прибора): разница с тем, что под прибором, {1:0.0}; с прибором {2:0.0} (0…255)", nm, dDesk, dDev));
                    bool ok = dDesk >= 0 && dDev > 0 && dDesk < dDev * 0.5 && dDesk < 25;
                    Save(new Bitmap(desk0), Path.GetDirectoryName(bn), Path.GetFileName(bn) + "_desk.png");
                    Save(new Bitmap(crop), Path.GetDirectoryName(bn), Path.GetFileName(bn) + "_crop.png");
                    Save(new Bitmap(expVis), Path.GetDirectoryName(bn), Path.GetFileName(bn) + "_dev.png");
                    int w2 = dr.Width, h2 = dr.Height, pad = 10, lab = 22;
                    using (var cmp = new Bitmap(w2 * 3 + pad * 4, h2 + lab + pad * 2, PixelFormat.Format32bppArgb))
                    {
                        using (var g = Graphics.FromImage(cmp))
                        using (var fnt = new Font("Segoe UI", 11f, FontStyle.Bold, GraphicsUnit.Pixel))
                        using (var wb = new SolidBrush(Color.White))
                        {
                            g.Clear(Color.FromArgb(30, 32, 36)); g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            Bitmap[] ims = { desk0, expVis, crop };
                            string[] ls = { "под прибором (до показа)", "экран db (прибор виден)", "кадр mp4: " + (ok ? "прибора НЕТ" : "ПРИБОР ВИДЕН") };
                            for (int i = 0; i < 3; i++)
                            {
                                int x = pad + i * (w2 + pad);
                                g.DrawString(ls[i], fnt, wb, x, pad);
                                g.DrawImage(ims[i], x, pad + lab, w2, h2);
                            }
                        }
                        cmp.Save(bn + "_cmp.png", ImageFormat.Png);
                    }
                    W("ИТОГ " + mode + ": " + capMode + ", в кадре на месте прибора " + (ok ? "прибора НЕТ (виден рабочий стол) -> PASS" : "ПРИБОР ЕСТЬ -> FAIL"));
                    code = ok ? 0 : 1;
                }
            }
            catch (Exception ex) { W("FAIL исключение: " + ex); code = 9; }
            return code;
        }

        // Прежняя звуковая самопроверка v5 (ключ --selftest-audio).
        static int SelfTestAudio()
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test");
            Directory.CreateDirectory(dir);
            string logf = Path.Combine(dir, "selftest_v5.log");
            var log = new StringBuilder();
            Action<string> W = s => { log.AppendLine(s); try { File.WriteAllText(logf, log.ToString(), Encoding.UTF8); } catch { } };
            bool pass = false;
            try
            {
                W("selftest v5 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", DPI k=" + Theme.K);
                try
                {
                    var rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                    W("Windows: " + rk.GetValue("ProductName") + " build " + rk.GetValue("CurrentBuildNumber") + " (" + rk.GetValue("DisplayVersion") + ")");
                }
                catch { }
                W("VirtualScreen: " + SystemInformation.VirtualScreen + ", мониторов: " + Screen.AllScreens.Length);
                string ff = Engine.FindFfmpeg(); W("ffmpeg: " + ff);
                if (ff == null) { W("FAIL: нет ffmpeg"); return 2; }
                string raw; var mics = Engine.ListMics(ff, out raw);
                W("Микрофоны: " + mics.Count);
                foreach (var m in mics) W("  " + m.Name + " | " + m.Alt);
                var cfg = Settings.Load(); string saved = cfg.Get("mic", "");
                MicDev mic = null;
                foreach (var m in mics) if (m.Arg == saved || m.Name == saved) mic = m;
                if (mic == null && mics.Count > 0) mic = mics[0];

                // Плашка (layered): исключение из захвата; окно не показывается
                var ov = new PlaqueForm(1f); var h = ov.Handle; uint aff;
                Native.GetWindowDisplayAffinity(h, out aff);
                W("Плашка layered: SetWindowDisplayAffinity(WDA_EXCLUDEFROMCAPTURE) = " + ov.AffinityOk + ", значение 0x" + aff.ToString("X") + ", UpdateLayeredWindow = " + ov.Present());
                ov.Dispose();

                // Осциллограф: waveIn открыт ДО ffmpeg и работает во время записи
                var mm = new MicMeter(); int mr = -1; int dev = -1;
                if (mic != null) { dev = WinMM.Match(mic.Name); mr = mm.Open(dev); if (mr != 0 && dev != -1) mr = mm.Open(dev = -1); }
                W("waveIn для волны: " + (mic == null ? "нет микрофона" : WinMM.ErrText(mr) + " (устройство " + dev + ")"));

                var eng = new Engine { Ffmpeg = ff, WorkDir = Path.Combine(dir, "seg_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")), Mic = mic == null ? null : mic.Arg, MicName = mic == null ? null : mic.Name };
                W("Блокировка микрофона Windows: " + (Engine.MicBlockReason() ?? "нет"));
                W("Сегмент 1 (1,5 с)…");
                var t0 = Stopwatch.StartNew();
                bool st = eng.StartSegment();
                bool closedForFfmpeg = false;
                if (!st && eng.Mic != null && mm.IsOpen)
                {
                    W("  со звуком не стартовало при открытом waveIn — закрываю waveIn и повторяю (как сделает приложение):\n" + eng.LastError);
                    mm.Close(); closedForFfmpeg = true; st = eng.StartSegment();
                }
                if (!st && eng.Mic != null)
                {
                    W("  с микрофоном не стартовало:\n" + eng.LastError + "\n  -> повтор без звука (как сделает приложение)");
                    eng.Mic = null; mic = null; st = eng.StartSegment();
                }
                if (!st) { W("FAIL старт: " + eng.LastError); mm.Close(); return 3; }
                W("  запуск занял " + t0.ElapsedMilliseconds + " мс, звук в ffmpeg: " + (eng.Mic != null ? eng.Mic : "нет"));
                long b0 = mm.Buffers;
                var probeBuf = new float[2048];
                Thread.Sleep(1500);
                long b1 = mm.Buffers; int got = mm.IsOpen ? mm.CopyLast(probeBuf) : 0;
                float pk = 0; foreach (var v in probeBuf) pk = Math.Max(pk, Math.Abs(v));
                W(string.Format(System.Globalization.CultureInfo.InvariantCulture, "  waveIn во время записи: открыт={0}, буферов за 1,5 с: {1} (ожидается ~30), отсчётов в кольце {2}, пик {3:0.0000}, ненулевые: {4}{5}",
                    mm.IsOpen, b1 - b0, got, pk, mm.AnyNonZero ? "да" : "нет", closedForFfmpeg ? " (закрыт ради ffmpeg)" : ""));
                eng.StopSegment();
                W("Пауза 0,5 с, сегмент 2 (1,5 с)…");
                Thread.Sleep(500);
                if (!eng.StartSegment()) { W("FAIL старт 2: " + eng.LastError); mm.Close(); return 3; }
                Thread.Sleep(1500); eng.StopSegment();
                mm.Close();
                W("Сегментов: " + eng.Segments.Count);
                string outf = Path.Combine(dir, "selftest_v5_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".mp4");
                bool ok = eng.Finalize(outf);
                W("Склейка: " + ok + " -> " + outf + (ok ? " (" + new FileInfo(outf).Length + " байт)" : "\n" + eng.LastError));
                if (!ok) return 4;
                string probe = Path.Combine(Path.GetDirectoryName(ff), "ffprobe.exe");
                string o;
                bool hasV = false, hasA = false; double dur = 0;
                if (File.Exists(probe))
                {
                    Engine.Run(probe, "-v error -show_entries stream=codec_type,codec_name,width,height,sample_rate,channels:format=duration -of default=nw=1 \"" + outf + "\"", out o, 30000);
                    W("ffprobe:\n" + o.Trim());
                    hasV = o.Contains("codec_type=video"); hasA = o.Contains("codec_type=audio");
                    foreach (var l in o.Split('\n'))
                        if (l.StartsWith("duration=")) double.TryParse(l.Substring(9).Trim(), System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out dur);
                }
                else
                {
                    Engine.Run(ff, "-hide_banner -i \"" + outf + "\"", out o, 30000);
                    W("ffprobe нет, ffmpeg -i:\n" + o.Trim());
                    hasV = o.Contains("Video:"); hasA = o.Contains("Audio:");
                    int i = o.IndexOf("Duration: ");
                    if (i >= 0) { TimeSpan ts; if (TimeSpan.TryParse(o.Substring(i + 10, 11), System.Globalization.CultureInfo.InvariantCulture, out ts)) dur = ts.TotalSeconds; }
                }
                pass = hasV && dur > 2.0 && dur < 4.5;
                // v5: второй проход «Улучшить голос» (как в приложении после «Стоп»)
                if (hasA)
                {
                    W("Улучшить голос (ini voice=" + cfg.Get("voice", "1") + "; в самопроверке — всегда): второй проход…");
                    string tmp = Path.Combine(eng.WorkDir, "voice_" + Path.GetFileName(outf));
                    int nP = 0; double lastP = -1; var tE = Stopwatch.StartNew();
                    var vlog = new StringBuilder();
                    string err;
                    bool v = Engine.Enhance(ff, outf, tmp, dur, pr => { nP++; lastP = pr; }, x => { vlog.AppendLine(x); eng.Log(x); }, out err);
                    W(string.Format(System.Globalization.CultureInfo.InvariantCulture, "  ffmpeg: {0} за {1} мс, событий прогресса {2}, последнее {3:0.00}", v ? "OK" : "ОШИБКА", tE.ElapsedMilliseconds, nP, lastP));
                    W("  лог второго прохода:\n" + vlog.ToString().TrimEnd());
                    string rawF = null;
                    if (v) v = Engine.SwapEnhanced(outf, tmp, out rawF, out err);
                    if (!v) W("  FAIL улучшение: " + err);
                    else
                    {
                        W("  оригинал -> " + rawF + ", итог -> " + outf);
                        string o2 = "";
                        if (File.Exists(probe)) Engine.Run(probe, "-v error -show_entries stream=codec_type,codec_name,channels,sample_rate,bit_rate:format=duration -of default=nw=1 \"" + outf + "\"", out o2, 30000);
                        W("ffprobe после улучшения:\n" + o2.Trim());
                        bool v2 = o2.Contains("codec_type=video"), a2 = o2.Contains("codec_type=audio"), mono = o2.Contains("channels=1");
                        W(string.Format("  после улучшения: видео={0}, аудио={1}, моно={2}", v2, a2, mono));
                        v = v2 && a2 && mono;
                    }
                    pass = pass && v;
                }
                if (!hasA) W("ВНИМАНИЕ: звуковой дорожки нет — микрофон недоступен (" + (Engine.MicBlockReason() ?? "см. ошибку выше") + ").");
                W(string.Format("ИТОГ: видео={0}, аудио={1}, длительность={2:0.00} с -> {3}", hasV, hasA, dur, pass ? (hasA ? "PASS" : "PASS (только видео)") : "FAIL"));
            }
            catch (Exception ex) { W("FAIL исключение: " + ex); }
            return pass ? 0 : 1;
        }
    }
}
