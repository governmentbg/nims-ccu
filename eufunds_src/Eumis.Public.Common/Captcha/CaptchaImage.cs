using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Web;

namespace Eumis.Public.Common.Captcha
{
    /// <summary>
    /// Amount of curved line noise to add to rendered image.
    /// </summary>
    public enum LineNoiseLevel
    {
        /// <summary>
        ///
        /// </summary>
        None,

        /// <summary>
        ///
        /// </summary>
        Low,

        /// <summary>
        ///
        /// </summary>
        Medium,

        /// <summary>
        ///
        /// </summary>
        High,

        /// <summary>
        ///
        /// </summary>
        Extreme,
    }

    /// <summary>
    /// Amount of background noise to add to rendered image.
    /// </summary>
    public enum BackgroundNoiseLevel
    {
        /// <summary>
        ///
        /// </summary>
        None,

        /// <summary>
        ///
        /// </summary>
        Low,

        /// <summary>
        ///
        /// </summary>
        Medium,

        /// <summary>
        ///
        /// </summary>
        High,

        /// <summary>
        ///
        /// </summary>
        Extreme,
    }

    /// <summary>
    /// Amount of random font warping to apply to rendered text.
    /// </summary>
    public enum FontWarpFactor
    {
        /// <summary>
        ///
        /// </summary>
        None,

        /// <summary>
        ///
        /// </summary>
        Low,

        /// <summary>
        ///
        /// </summary>
        Medium,

        /// <summary>
        ///
        /// </summary>
        High,

        /// <summary>
        ///
        /// </summary>
        Extreme,
    }

    /// <summary>
    /// CAPTCHA Image.
    /// </summary>
    /// <seealso href="http://www.codinghorror.com">Original By Jeff Atwood</seealso>
    public class CaptchaImage
    {
        /// <summary>
        /// RandomFontFamily.
        /// </summary>
        private static readonly string[] RandomFontFamily = { "arial", "arial black", "comic sans ms", "courier new", "estrangelo edessa", "franklin gothic medium", "georgia", "lucida console", "lucida sans unicode", "mangal", "microsoft sans serif", "palatino linotype", "sylfaen", "tahoma", "times new roman", "trebuchet ms", "verdana" };

        /// <summary>
        /// RandomColor.
        /// </summary>
        // private static readonly Color[] RandomColor = { Color.Red, Color.Green, Color.Blue, Color.Black, Color.Purple, Color.Orange };
        private static readonly Color[] RandomColor = { ColorTranslator.FromHtml("#f16848") };

        private int height;
        private int width;
        private Random rand;

        /// <summary>
        /// Initializes static members of the <see cref="CaptchaImage"/> class.
        /// Initializes the <see cref="CaptchaImage"/> class.
        /// </summary>
        static CaptchaImage()
        {
            FontWarp = FontWarpFactor.None;
            BackgroundNoise = BackgroundNoiseLevel.High;
            LineNoise = LineNoiseLevel.Extreme;
            TextLength = 4;
            TextChars = "2346789";
            CacheTimeOut = 600D;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaImage"/> class.
        /// </summary>
        public CaptchaImage()
        {
            this.rand = new Random();
            this.Width = 180;
            this.Height = 50;
            this.Text = this.GenerateRandomText();
            this.RenderedAt = DateTime.Now;
            this.UniqueId = Guid.NewGuid().ToString("N");
        }

        #region Static

        /// <summary>
        /// Gets or sets a string of available text characters for the generator to use.
        /// </summary>
        /// <value>The text chars.</value>
        public static string TextChars { get; set; }

        /// <summary>
        /// Gets or sets the length of the text.
        /// </summary>
        /// <value>The length of the text.</value>
        public static int TextLength { get; set; }

        /// <summary>
        /// Gets or sets amount of random warping to apply to the <see cref="CaptchaImage"/> instance.
        /// </summary>
        /// <value>The font warp.</value>
        public static FontWarpFactor FontWarp { get; set; }

        /// <summary>
        /// Gets or sets amount of background noise to apply to the <see cref="CaptchaImage"/> instance.
        /// </summary>
        /// <value>The background noise.</value>
        public static BackgroundNoiseLevel BackgroundNoise { get; set; }

        /// <summary>
        /// Gets or sets amount of line noise to apply to the <see cref="CaptchaImage"/> instance.
        /// </summary>
        /// <value>The line noise.</value>
        public static LineNoiseLevel LineNoise { get; set; }

        /// <summary>
        /// Gets or sets the cache time out.
        /// </summary>
        /// <value>The cache time out.</value>
        public static double CacheTimeOut { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a GUID that uniquely identifies this Captcha.
        /// </summary>
        /// <value>The unique id.</value>
        public string UniqueId { get; private set; }

        /// <summary>
        /// Gets the date and time this image was last rendered.
        /// </summary>
        /// <value>The rendered at.</value>
        public DateTime RenderedAt { get; private set; }

        /// <summary>
        /// Gets the randomly generated Captcha text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; private set; }

        /// <summary>
        /// Gets or sets width of Captcha image to generate, in pixels.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (value <= 60)
                {
                    throw new ArgumentOutOfRangeException("width", value, "width must be greater than 60.");
                }

                this.width = value;
            }
        }

        /// <summary>
        /// Gets or setsHeight of Captcha image to generate, in pixels.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                if (value <= 30)
                {
                    throw new ArgumentOutOfRangeException("height", value, "height must be greater than 30.");
                }

                this.height = value;
            }
        }

        #endregion

        /// <summary>
        /// Gets the cached captcha.
        /// </summary>
        /// <param name="guid">The GUID.</param>
        /// <returns>CaptchaImage.</returns>
        public static CaptchaImage GetCachedCaptcha(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return null;
            }

            return (CaptchaImage)HttpRuntime.Cache.Get(guid);
        }

        /// <summary>
        /// Forces a new Captcha image to be generated using current property value settings.
        /// </summary>
        /// <returns>Bitmap.</returns>
        public Bitmap RenderImage()
        {
            return this.GenerateImagePrivate();
        }

        /// <summary>
        /// Returns a random font family from the font whitelist.
        /// </summary>
        /// <returns>string.</returns>
        private string GetRandomFontFamily()
        {
            return RandomFontFamily[this.rand.Next(0, RandomFontFamily.Length)];
        }

        /// <summary>
        /// generate random text for the CAPTCHA.
        /// </summary>
        /// <returns>string.</returns>
        private string GenerateRandomText()
        {
            StringBuilder sb = new StringBuilder(TextLength);
            int maxLength = TextChars.Length;
            for (int n = 0; n <= TextLength - 1; n++)
            {
                sb.Append(TextChars.Substring(this.rand.Next(maxLength), 1));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a random point within the specified x and y ranges.
        /// </summary>
        /// <param name="xmin">The xmin.</param>
        /// <param name="xmax">The xmax.</param>
        /// <param name="ymin">The ymin.</param>
        /// <param name="ymax">The ymax.</param>
        /// <returns>PointF.</returns>
        private PointF RandomPoint(int xmin, int xmax, int ymin, int ymax)
        {
            return new PointF(this.rand.Next(xmin, xmax), this.rand.Next(ymin, ymax));
        }

        /// <summary>
        /// Randoms the color.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetRandomColor()
        {
            return RandomColor[this.rand.Next(0, RandomColor.Length)];
        }

        /// <summary>
        /// Returns a random point within the specified rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>PointF.</returns>
        private PointF RandomPoint(Rectangle rect)
        {
            return this.RandomPoint(rect.Left, rect.Width, rect.Top, rect.Bottom);
        }

        /// <summary>
        /// Returns a GraphicsPath containing the specified string and font.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="f">The function.</param>
        /// <param name="r">The r.</param>
        /// <returns>GraphicsPath.</returns>
        private GraphicsPath TextPath(string s, Font f, Rectangle r)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            GraphicsPath gp = new GraphicsPath();
            gp.AddString(s, f.FontFamily, (int)f.Style, f.Size, r, sf);
            return gp;
        }

        /// <summary>
        /// Returns the CAPTCHA font in an appropriate size.
        /// </summary>
        /// <returns>Font.</returns>
        private Font GetFont()
        {
            float fsize;
            string fname = this.GetRandomFontFamily();

            switch (FontWarp)
            {
                case FontWarpFactor.None:
                    goto default;
                case FontWarpFactor.Low:
                    fsize = Convert.ToInt32(this.height * 0.8);
                    break;
                case FontWarpFactor.Medium:
                    fsize = Convert.ToInt32(this.height * 0.85);
                    break;
                case FontWarpFactor.High:
                    fsize = Convert.ToInt32(this.height * 0.9);
                    break;
                case FontWarpFactor.Extreme:
                    fsize = Convert.ToInt32(this.height * 0.95);
                    break;
                default:
                    fsize = Convert.ToInt32(this.height * 0.7);
                    break;
            }

            return new Font(fname, fsize, FontStyle.Bold);
        }

        /// <summary>
        /// Renders the CAPTCHA image.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap GenerateImagePrivate()
        {
            Bitmap bmp = new Bitmap(this.width, this.height);

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                gr.Clear(Color.White);

                int charOffset = 0;
                double charWidth = this.width / TextLength;
                Rectangle rectChar;

                foreach (char c in this.Text)
                {
                    // establish font and draw area
                    using (Font fnt = this.GetFont())
                    {
                        using (Brush fontBrush = new SolidBrush(this.GetRandomColor()))
                        {
                            rectChar = new Rectangle(Convert.ToInt32(charOffset * charWidth), 0, Convert.ToInt32(charWidth), this.height);

                            // warp the character
                            GraphicsPath gp = this.TextPath(c.ToString(), fnt, rectChar);
                            this.WarpText(gp, rectChar);

                            // draw the character
                            gr.FillPath(fontBrush, gp);

                            charOffset += 1;
                        }
                    }
                }

                Rectangle rect = new Rectangle(new Point(0, 0), bmp.Size);
                this.AddNoise(gr, rect);
                this.AddLine(gr, rect);
            }

            return bmp;
        }

        /// <summary>
        /// Warp the provided text GraphicsPath by a variable amount.
        /// </summary>
        /// <param name="textPath">The text path.</param>
        /// <param name="rect">The rect.</param>
        private void WarpText(GraphicsPath textPath, Rectangle rect)
        {
            float warpDivisor;
            float rangeModifier;

            switch (FontWarp)
            {
                case FontWarpFactor.None:
                    goto default;
                case FontWarpFactor.Low:
                    warpDivisor = 6F;
                    rangeModifier = 1F;
                    break;
                case FontWarpFactor.Medium:
                    warpDivisor = 5F;
                    rangeModifier = 1.3F;
                    break;
                case FontWarpFactor.High:
                    warpDivisor = 4.5F;
                    rangeModifier = 1.4F;
                    break;
                case FontWarpFactor.Extreme:
                    warpDivisor = 4F;
                    rangeModifier = 1.5F;
                    break;
                default:
                    return;
            }

            RectangleF rectF;
            rectF = new RectangleF(Convert.ToSingle((int)rect.Left), 0, Convert.ToSingle((int)rect.Width), rect.Height);

            int hrange = Convert.ToInt32(rect.Height / warpDivisor);
            int wrange = Convert.ToInt32(rect.Width / warpDivisor);
            int left = rect.Left - Convert.ToInt32(wrange * rangeModifier);
            int top = rect.Top - Convert.ToInt32(hrange * rangeModifier);
            int width = rect.Left + rect.Width + Convert.ToInt32(wrange * rangeModifier);
            int height = rect.Top + rect.Height + Convert.ToInt32(hrange * rangeModifier);

            if (left < 0)
            {
                left = 0;
            }

            if (top < 0)
            {
                top = 0;
            }

            if (width > this.Width)
            {
                width = this.Width;
            }

            if (height > this.Height)
            {
                height = this.Height;
            }

            PointF leftTop = this.RandomPoint(left, left + wrange, top, top + hrange);
            PointF rightTop = this.RandomPoint(width - wrange, width, top, top + hrange);
            PointF leftBottom = this.RandomPoint(left, left + wrange, height - hrange, height);
            PointF rightBottom = this.RandomPoint(width - wrange, width, height - hrange, height);

            PointF[] points = new PointF[] { leftTop, rightTop, leftBottom, rightBottom };
            Matrix m = new Matrix();
            m.Translate(0, 0);
            textPath.Warp(points, rectF, m, WarpMode.Perspective, 0);
        }

        /// <summary>
        /// Add a variable level of graphic noise to the image.
        /// </summary>
        /// <param name="g">The graphics1.</param>
        /// <param name="rect">The rect.</param>
        private void AddNoise(Graphics g, Rectangle rect)
        {
            int density;
            int size;

            switch (BackgroundNoise)
            {
                case BackgroundNoiseLevel.None:
                    goto default;
                case BackgroundNoiseLevel.Low:
                    density = 90;
                    size = 40;
                    break;
                case BackgroundNoiseLevel.Medium:
                    density = 18;
                    size = 40;
                    break;
                case BackgroundNoiseLevel.High:
                    density = 16;
                    size = 39;
                    break;
                case BackgroundNoiseLevel.Extreme:
                    density = 12;
                    size = 38;
                    break;
                default:
                    return;
            }

            SolidBrush br = new SolidBrush(this.GetRandomColor());
            int max = Convert.ToInt32(Math.Max((int)rect.Width, (int)rect.Height) / size);

            for (int i = 0; i <= Convert.ToInt32((rect.Width * rect.Height) / density); i++)
            {
                g.FillEllipse(br, this.rand.Next(rect.Width), this.rand.Next(rect.Height), this.rand.Next(max), this.rand.Next(max));
            }

            br.Dispose();
        }

        /// <summary>
        /// Add variable level of curved lines to the image.
        /// </summary>
        /// <param name="g">The graphics1.</param>
        /// <param name="rect">The rect.</param>
        private void AddLine(Graphics g, Rectangle rect)
        {
            int length;
            float width;
            int linecount;

            switch (LineNoise)
            {
                case LineNoiseLevel.None:
                    goto default;
                case LineNoiseLevel.Low:
                    length = 4;
                    width = Convert.ToSingle(this.height / 31.25);
                    linecount = 1;
                    break;
                case LineNoiseLevel.Medium:
                    length = 5;
                    width = Convert.ToSingle(this.height / 27.7777);
                    linecount = 1;
                    break;
                case LineNoiseLevel.High:
                    length = 3;
                    width = Convert.ToSingle(this.height / 25);
                    linecount = 2;
                    break;
                case LineNoiseLevel.Extreme:
                    length = 3;
                    width = Convert.ToSingle(this.height / 22.7272);
                    linecount = 3;
                    break;
                default:
                    return;
            }

            PointF[] pf = new PointF[length + 1];
            using (Pen p = new Pen(this.GetRandomColor(), width))
            {
                for (int l = 1; l <= linecount; l++)
                {
                    for (int i = 0; i <= length; i++)
                    {
                        pf[i] = this.RandomPoint(rect);
                    }

                    g.DrawCurve(p, pf, 1.75F);
                }
            }
        }
    }
}