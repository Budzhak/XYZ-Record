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
        [DllImport("user32.dll")] public static extern bool SetWindowDisplayAffinity(IntPtr h, uint a);
        [DllImport("user32.dll")] public static extern bool GetWindowDisplayAffinity(IntPtr h, out uint a);
        public delegate bool EnumProc(IntPtr h, IntPtr l);
        [DllImport("user32.dll")] public static extern bool EnumWindows(EnumProc p, IntPtr l);
        [DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr h);
        [DllImport("user32.dll")] public static extern bool IsIconic(IntPtr h);
        [DllImport("user32.dll")] public static extern bool IsWindow(IntPtr h);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetWindowText(IntPtr h, StringBuilder s, int n);
        [DllImport("user32.dll")] public static extern int GetWindowTextLength(IntPtr h);
        [DllImport("user32.dll")] public static extern int GetWindowLong(IntPtr h, int i);
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
        public static Bitmap Get(string name, float s)
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
        public string LastError = "";
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
            foreach (var f in c) { try { if (File.Exists(f)) return f; } catch { } }
            return null;
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

        // Обработанный tmp готов: оригинал → <папка>\_raw\<имя>_raw.mp4, tmp → на место итогового файла.
        public static bool SwapEnhanced(string outFile, string tmp, out string rawFile, out string err)
        {
            rawFile = null; err = null;
            try
            {
                string rawDir = Path.Combine(Path.GetDirectoryName(outFile), "_raw");
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
            Directory.CreateDirectory(WorkDir);
            curSeg = Path.Combine(WorkDir, string.Format("seg_{0:000}.mkv", Segments.Count));
            var a = new StringBuilder("-hide_banner -y -thread_queue_size 1024 -f gdigrab -framerate 15 -draw_mouse 1 ");
            if (Area.HasValue)
            {
                var r = Area.Value;
                a.AppendFormat("-offset_x {0} -offset_y {1} -video_size {2}x{3} ", r.X, r.Y, r.Width, r.Height);
            }
            a.Append("-i desktop ");
            bool mic = !string.IsNullOrEmpty(Mic);
            if (mic) a.Append("-thread_queue_size 1024 -f dshow -audio_buffer_size 50 -i audio=\"" + Mic + "\" ");
            a.Append("-map 0:v ");
            if (mic) a.Append("-map 1:a ");
            a.Append("-vf \"scale=trunc(iw/2)*2:trunc(ih/2)*2\" -c:v libx264 -preset veryfast -crf 28 -pix_fmt yuv420p ");
            if (mic) a.Append("-af aresample=async=1 -c:a aac -b:a 96k ");
            a.Append("\"" + curSeg + "\"");
            Log("=== " + DateTime.Now.ToString("HH:mm:ss") + " ffmpeg " + a);
            lock (lk) tail.Length = 0;
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
                if (started.WaitOne(50)) return true;
                if (p.HasExited)
                {
                    p.WaitForExit();
                    lock (lk) LastError = "ffmpeg завершился с ошибкой:\n" + Tail(1500);
                    proc = null; return false;
                }
                Application.DoEvents();
            }
            return true; // не дождались строки, но процесс жив — считаем, что пишет
        }

        string Tail(int n) { string t = tail.ToString(); return t.Length > n ? t.Substring(t.Length - n) : t; }

        void OnErr(object s, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;
            if (e.Data.Contains("Press [q]")) started.Set();
            if (e.Data.StartsWith("frame=") || e.Data.StartsWith("size=")) return;
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
            if (ok) Segments.Add(curSeg); else lock (lk) LastError = "Сегмент не записан:\n" + Tail(1500);
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
        public int Kind; // 0 весь экран, 1 монитор, 2 окно
        public int Mon; public IntPtr Hwnd; public string Text; public string Key;
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

    enum Hit { None = 0, Sel1, Sel2, Test, Folder, Min, Close, PadRec, PadPause, PadStop, Toggle, Screen, Body, QPause, QStop, Skin }

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
        public bool[] En = new bool[20];
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
            float pad = 6 * s;
            cur = Points(buf, isLive ? avail : 0, W, H, pad, Gain);
            using (var g = Graphics.FromImage(tmp))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.Clear(Color.Transparent);
                g.CompositingMode = CompositingMode.SourceOver;
                using (var ia = Res.Attr(1, 1, 1, 0.64f))
                    g.DrawImage(trail, new Rectangle(0, 0, W, H), 0, 0, W, H, GraphicsUnit.Pixel, ia);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Theme.A(Theme.Teal, 0.55f), 1.5f * s) { LineJoin = LineJoin.Round })
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
            float[] wd = { 10f, 5.5f, 2.8f }; float[] ga = { 0.06f, 0.13f, 0.32f };
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
    }

    // ---------- Отрисовка прибора ----------
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
            Res.Draw(g, Res.Get("body", s), 0, 0, 1f - ek);
            if (ek > 0.003f) Res.Draw(g, Res.Get("frame", s), 0, 0, ek);
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
            Res.Draw(g, Res.Get("plaque", s), 0, 0, 1f);
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
        static Color bg = Color.FromArgb(22, 25, 30), sel = Color.FromArgb(30, 58, 60), line = Color.FromArgb(48, 56, 62);
        public override Color ToolStripDropDownBackground { get { return bg; } }
        public override Color ImageMarginGradientBegin { get { return bg; } }
        public override Color ImageMarginGradientMiddle { get { return bg; } }
        public override Color ImageMarginGradientEnd { get { return bg; } }
        public override Color MenuBorder { get { return line; } }
        public override Color MenuItemBorder { get { return Color.FromArgb(60, 120, 112); } }
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
            AffinityOk = Native.SetWindowDisplayAffinity(Handle, Native.WDA_EXCLUDEFROMCAPTURE);
            Present();
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

        public MainForm()
        {
            cfg = Settings.Load();
            s = Theme.K;
            Text = "XYZ Запись";
            ShowInTaskbar = true;
            try { using (var st = Res.Raw("app.ico")) if (st != null) Icon = new Icon(st); } catch { }
            EnsureFrame((int)Math.Round(Lay.CW * s), (int)Math.Round(Lay.CH * s));
            scope.Ensure((int)Math.Round(Lay.Screen.Width * s), (int)Math.Round(Lay.Screen.Height * s), s);
            micOn = cfg.Get("micOn", "1") == "1";
            voiceOn = cfg.Get("voice", "1") == "1";
            showInRec = cfg.Get("showInRec", "0") == "1";
            foldRec = cfg.Get("foldRec", "0") == "1";
            skinFrame = cfg.Get("skin", "body") == "frame";
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
                var r = new Rectangle(cx, cy, FW, FH);
                foreach (var sc in Screen.AllScreens)
                    if (sc.WorkingArea.IntersectsWith(new Rectangle(cx + FW / 4, cy + FH / 4, FW / 2, FH / 2))) { x = cx; y = cy; break; }
            }
            Location = new Point(x, y);
        }
        void SavePos() { if (Visible) { cfg.Set("x", Left.ToString()); cfg.Set("y", Top.ToString()); cfg.Save(); } }

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
                ok = Native.SetWindowDisplayAffinity(Handle, want);
                Native.GetWindowDisplayAffinity(Handle, out now);
            }
            LastAff = now;
            exclFail = excl && (!ok || now != want);
            if (ov != null && ov.IsHandleCreated)
                Native.SetWindowDisplayAffinity(ov.Handle, rec && showInRec ? 0u : Native.WDA_EXCLUDEFROMCAPTURE);
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
            if (Visible && Alpha > 0)
            {
                scope.Push(sbuf, avail, live && powered);
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
            ui.Area = area != null ? area.Text : "Весь экран";
            using (var g = Graphics.FromImage(Frame)) DeviceView.Render(g, s, ui, scope);
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
            var h = DeviceView.HitTest(Lp(e.Location));
            var hot = Interactive(h) && ui.IsEn(h) ? h : Hit.None;
            if (hot != ui.Hot) { ui.Hot = hot; Cursor = hot != Hit.None ? Cursors.Hand : Cursors.Default; }
            if (h == Hit.Screen && ui.Status.Length > 70 && !excl) { if (tip.GetToolTip(this) != ui.Status) tip.SetToolTip(this, ui.Status); }
            else if (tip.GetToolTip(this) != "") tip.SetToolTip(this, "");
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e) { ui.Hot = Hit.None; base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var h = DeviceView.HitTest(Lp(e.Location));
            if (e.Button == MouseButtons.Right) { ShowBodyMenu(e.Location); return; }
            if (e.Button != MouseButtons.Left) return;
            if (Interactive(h)) { if (ui.IsEn(h)) { ui.Down = h; Capture = true; } return; }
            if (h == Hit.Screen && ui.Clip) { clipT = DateTime.MinValue; peakHold = -140f; return; }
            Native.DragWindow(this);
            SavePos();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            var d = ui.Down; ui.Down = Hit.None; Capture = false;
            if (d == Hit.None) return;
            if (DeviceView.HitTest(Lp(e.Location)) != d) return;
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
            }
        }
        Point Px(float lx, float ly) { return new Point((int)Math.Round((lx + Lay.P) * s), (int)Math.Round((ly + Lay.P) * s)); }

        // ---------- меню ----------
        public ContextMenuStrip BuildAreaMenu()
        {
            RefreshAreas();
            var m = NewMenu();
            foreach (var a in areas)
            {
                var it = a;
                var mi = new ToolStripMenuItem(a.Text) { Checked = area != null && area.Key == a.Key };
                mi.Click += (o, e) => { area = it; if (it.Kind != 2) { cfg.Set("area", it.Key); cfg.Save(); } };
                m.Items.Add(mi);
            }
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
            v.ToolTipText = "После «Стоп» звук обрабатывается: шумодав, выравнивание громкости, студийный тембр.\nОригинал сохраняется в папке _raw.";
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
            var t1 = new ToolStripMenuItem("Выключить (в трей)"); t1.Click += (o, e) => PowerOff();
            var x1 = new ToolStripMenuItem("Выход"); x1.Click += (o, e) => ExitApp();
            m.Items.AddRange(new ToolStripItem[] { f1, f2, new ToolStripSeparator(), VoiceItem(), new ToolStripSeparator(), r1, r2, new ToolStripSeparator(), sk1, c1, c2, new ToolStripSeparator(), t1, x1 });
            return m;
        }

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
            areas.Add(new AreaItem { Kind = 0, Key = "full", Text = scr.Length > 1 ? "Весь экран (все мониторы)" : "Весь экран" });
            if (scr.Length > 1)
                for (int i = 0; i < scr.Length; i++)
                    areas.Add(new AreaItem { Kind = 1, Mon = i, Key = "mon:" + i,
                        Text = string.Format("Монитор {0} — {1}×{2}{3}", i + 1, scr[i].Bounds.Width, scr[i].Bounds.Height, scr[i].Primary ? " (основной)" : "") });
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
            ui.En[(int)Hit.Toggle] = state != St.Saving && powered;
            ui.En[(int)Hit.Skin] = powered;
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
            eng.Segments.Clear();
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
            UpdateButtons(); UpdateTime();
            StartMeter(false);
        }

        void TogglePause()
        {
            if (state == St.Rec)
            {
                sw.Stop(); acc += sw.Elapsed; sw.Reset();
                state = St.Paused; UpdateButtons(); UpdateTime();
                eng.StopSegment();
            }
            else if (state == St.Paused)
            {
                if (!eng.StartSegment()) { SetStatus("Не удалось продолжить: " + eng.LastError, true); return; }
                sw.Start(); state = St.Rec; UpdateButtons();
            }
        }

        void StopRec()
        {
            if (state != St.Rec && state != St.Paused) return;
            if (state == St.Rec) { sw.Stop(); acc += sw.Elapsed; sw.Reset(); eng.StopSegment(); }
            ApplyExclude(false);   // v7: запись остановлена — прибор снова виден скриншотам
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
                if (ok) eng.CleanupSegments();
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
                if (!Program.SelfTestMode) try { Process.Start("explorer.exe", "/select,\"" + outFile + "\""); } catch { }
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
        public void SetScale(float sc)
        {
            s = sc;
            EnsureFrame((int)Math.Round(Lay.CW * s), (int)Math.Round(Lay.CH * s));
            scope = new Scope(); scope.Ensure((int)Math.Round(Lay.Screen.Width * s), (int)Math.Round(Lay.Screen.Height * s), s);
            ov.Dispose(); ov = new PlaqueForm(s);
        }
    }

    // ---------- Запуск ----------
    static class Program
    {
        public static bool SnapshotMode;

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
                Application.Run(new MainForm());
            }
            return 0;
        }

        static void Save(Bitmap b, string dir, string name) { if (b == null) return; b.Save(Path.Combine(dir, name), ImageFormat.Png); b.Dispose(); }

        // Ключ --snapshot (v8): кадры прибора в test\v8_*.png (окно не показывается), кадр ARGB с прозрачностью.
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
