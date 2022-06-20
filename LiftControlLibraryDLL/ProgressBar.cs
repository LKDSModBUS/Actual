using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing.Text;
namespace MyCtrl
{
    /// <summary>
    /// Represents a Windows progress bar control. 
    /// </summary>
    /// <copyright>Copyright © 2008 Uwe Eichkorn
    /// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
    /// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
    /// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
    /// PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
    /// REMAINS UNCHANGED.
    /// </copyright>
    [ToolboxBitmap(typeof(System.Windows.Forms.ProgressBar))]
    public partial class ProgressBar : Control
    {
        #region Events
        /// <summary>
        /// Occurs when the value of the BorderColor property changes.
        /// </summary>
        [Description("Occurs when the value of the BorderColor property is changed on the control.")]
        public event EventHandler<EventArgs> BorderColorChanged;
        /// <summary>
        /// Occurs when the value of the BackgroundColor property changes.
        /// </summary>
        [Description("Occurs when the value of the BackgroundColor property is changed on the control.")]
        public event EventHandler<EventArgs> BackgroundColorChanged;
        /// <summary>
        /// Occurs when the value of the ValueColor property changes.
        /// </summary>
        [Description("Occurs when the value of the ValueColor property is changed on the control.")]
        public event EventHandler<EventArgs> ValueColorChanged;
        #endregion

        #region FieldsPrivate
        private Color m_backgroundColor;
        private Color m_valueColor;
        private Color m_borderColor;
        private int m_iMinimum;
        private int m_iMaximum;
        private int m_iValue;
        private int m_RadiusCorner = 5;
        private bool m_isBattary = false;
        private bool m_isCharged = false;
        private bool m_isBad = false;
        private bool m_isNo = false;
        #endregion

        #region Properties
        [Browsable(true)]
        [Description("Draw battary style")]
        public bool isCharged
        {
            get
            {
                return m_isCharged;
            }
            set
            {
                m_isCharged = value;
            }
        }
        [Browsable(true)]
        [Description("bad battary style")]
        public bool isBad
        {
            get
            {
                return m_isBad;
            }
            set
            {
                m_isBad = value;
            }
        }
        [Browsable(true)]
        [Description("No battary style")]
        public bool isNo
        {
            get
            {
                return m_isNo;
            }
            set
            {
                m_isNo = value;
            }
        }
        [Browsable(true)]
        [Description("Draw battary style")]
        public bool isBattary
        {
            get
            {
                return m_isBattary;
            }
            set
            {
                m_isBattary = value;
            }
        }
        [Browsable(true)]
        [Description("Radius for corner")]
        public int RadiusCorner
        {
            get
            {
                return m_RadiusCorner;
            }
            set
            {
                m_RadiusCorner = value;
            }
        }

        /// <summary>
        /// Gets or sets the color used for the background rectangle of this control.
        /// </summary>
        /// <value>
        /// Type: <see cref="System.Drawing.Color"/>
        /// A Color used for the background rectangle of this control.
        /// </value>
        [Browsable(true)]
        [Description("The color used for the background rectangle of this control.")]
        public Color BackgroundColor
        {
            get { return this.m_backgroundColor; }
            set
            {
                if (this.m_backgroundColor != value)
                {
                    this.m_backgroundColor = value;
                    OnBackgroundColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the color used for the value rectangle of this control.
        /// </summary>
        /// <value>
        /// Type: <see cref="System.Drawing.Color"/>
        /// A Color used for the value rectangle of this control.
        /// </value>
        [Browsable(true)]
        [Description("The color used for the value rectangle of this control.")]
        public Color ValueColor
        {
            get { return this.m_valueColor; }
            set
            {
                if (this.m_valueColor != value)
                {
                    this.m_valueColor = value;
                    OnValueColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the border color for the control.
        /// </summary>
        /// <value>
        /// Type: <see cref="System.Drawing.Color "/>
        /// A Color that represents the border color of the control.
        /// </value>
        public Color BorderColor
        {
            get { return this.m_borderColor; }
            set
            {
                if (this.m_borderColor != value)
                {
                    this.m_borderColor = value;
                    OnBorderColorChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }
        /// <summary>
        /// Gets or sets the maximum value of the range of the control.
        /// </summary>
        /// <value>
        /// Type: <see cref="System.Int32"/>
        /// The maximum value of the range. The default is 100.
        /// </value>
        [Browsable(true)]
        [Description("The upper bound of range this ProgressBar is working with.")]
        public int Maximum
        {
            get { return this.m_iMaximum; }
            set
            {
                if (this.m_iMaximum != value)
                {
                    if (value < 0)
                    {
                        object[] args = new object[] { "Maximum", value.ToString(CultureInfo.CurrentCulture), "Maximum" };
                        throw new ArgumentOutOfRangeException("Maximum", string.Format(CultureInfo.InvariantCulture, "Value of '{0}' is not valid for '{1}'. 'Maximum' must be greater than or equal to 0.", args));
                    }
                    if (this.m_iMinimum > value)
                    {
                        this.m_iMinimum = value;
                    }
                    this.m_iMaximum = value;
                    if (this.m_iValue > this.m_iMaximum)
                    {
                        this.m_iValue = this.m_iMaximum;
                    }
                    this.Invalidate(true);
                }
            }
        }
        /// <summary>
        /// Gets or sets the minimum value of the range of the control.
        /// </summary>
        /// <value>
        /// Type: <see cref="System.Int32"/>
        /// The minimum value of the range. The default is 0.
        /// </value>
        [Browsable(true)]
        [Description("The lower bound of range this ProgressBar is working with.")]
        public int Minimum
        {
            get { return this.m_iMinimum; }
            set
            {
                if (this.m_iMinimum != value)
                {
                    if (value < 0)
                    {
                        object[] args = new object[] { "Minimum", value.ToString(CultureInfo.CurrentCulture), "Minimum" };
                        throw new ArgumentOutOfRangeException("Minimum", string.Format(CultureInfo.InvariantCulture, "Value of '{0}' is not valid for '{1}'. 'Maximum' must be greater than or equal to 0.", args));
                    }
                    if (this.m_iMaximum < value)
                    {
                        this.m_iMaximum = value;
                    }
                    this.m_iMinimum = value;
                    if (this.m_iValue < this.m_iMinimum)
                    {
                        this.m_iValue = this.m_iMinimum;
                    }
                    this.Invalidate(true);
                }
            }
        }
        /// <summary>
        /// Gets or sets the current position of the progress bar.
        /// </summary>
        /// <value>
        /// Type: <see cref="System.Int32"/>
        /// The position within the range of the progress bar. The default is 0.
        /// </value>
        [Browsable(true)]
        [Description("The current value for the ProgressBar, in the range specified by the minimum and maximum properties.")]
        public int Value
        {
            get { return this.m_iValue; }
            set
            {
                if (this.m_iValue != value)
                {
                    if ((value < this.m_iMinimum) || (value > this.m_iMaximum))
                    {
                        throw new ArgumentOutOfRangeException("Value", string.Format(CultureInfo.InvariantCulture, "Value of '{0}' is not valid for '{1}'. 'Value' should be between '{2}' and '{3}'.", new object[] { "Value", value.ToString(CultureInfo.CurrentCulture), "'minimum'", "'maximum'" }));
                    }
                    this.m_iValue = value;
                    this.Invalidate(true);
                }
            }
        }
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initializes a new instance of the ProgressBar class.
        /// </summary>
        public ProgressBar()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();

            this.m_iMinimum = 0;
            this.m_iMaximum = 100;
            this.m_backgroundColor = Color.Silver;
            this.m_valueColor = Color.DeepSkyBlue;
            this.m_borderColor = SystemColors.ActiveBorder;
            this.BackColor = Color.Transparent;
        }
        #endregion

        #region MethodsProtected
        /// <summary>
        /// Raises the Paint event.
        /// </summary>
        /// <param name="e">A PaintEventArgs that contains the event data.</param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////// 
        protected override void OnPaint(PaintEventArgs e)
        {
            using (UseAntiAlias antiAlias = new UseAntiAlias(e.Graphics))
            {
                Graphics graphics = e.Graphics;

                //Rectangle Rect = ClientRectangle;
                //Rect.Inflate(-1, -1);
                ////vertical
                //if (Rect.Width < Rect.Height)
                //{
                //    using (GraphicsPath outerRectangleGraphicsPath = GetBackgroundPath(Rect, RadiusCorner - 1, isBattary))
                //    {
                //        if (outerRectangleGraphicsPath != null)
                //        {
                //            using (LinearGradientBrush gradientBrush = GetGradientBackBrush(Rect, m_backgroundColor, true))
                //            {
                //                if (gradientBrush != null)
                //                {
                //                    graphics.FillPath(gradientBrush, outerRectangleGraphicsPath);
                //                }
                //            }
                //        }
                //    }
                //}
                //else
                //{

                //}



                DrawProgressBar(
                             graphics,
                             this.ClientRectangle,
                             isBad ? Color.LightCoral : this.m_backgroundColor,
                             this.m_valueColor,
                             this.m_borderColor,
                             this.RightToLeft,
                             this.Minimum,
                             this.Maximum,
                             this.Value,
                             this.RadiusCorner,
                             this.isBattary
                             );
                //if (isNo)
                //{
                //    Rectangle chargeRectangle = ClientRectangle;
                //    chargeRectangle.Inflate(-chargeRectangle.Width / 20, -chargeRectangle.Height / 20);
                //    graphics.DrawImage(global::LKDS.Controls.Properties.Resources.battery_no, chargeRectangle);
                //}
                //else if (isBad)
                //{
                //    Rectangle chargeRectangle = ClientRectangle;
                //    chargeRectangle.Inflate(-chargeRectangle.Width / 20, -chargeRectangle.Height / 20);
                //    graphics.DrawImage(global::LKDS.Controls.Properties.Resources.battery_error, chargeRectangle);

                //}
                //else if (isCharged)
                //{
                //    Rectangle chargeRectangle = ClientRectangle;
                //    chargeRectangle.Inflate(-chargeRectangle.Width / 20, -chargeRectangle.Height / 20);
                //    graphics.DrawImage(global::LKDS.Controls.Properties.Resources.battery_charging, chargeRectangle);
                //}
                if (string.IsNullOrEmpty(this.Text) == false)
                {
                    using (UseClearTypeGridFit useClearTypeGridFit = new UseClearTypeGridFit(graphics))
                    {
                        using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
                        {
                            using (StringFormat stringFormat = new StringFormat())
                            {
                                stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                                if (this.RightToLeft == RightToLeft.Yes)
                                {
                                    stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                                }
                                stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                                stringFormat.LineAlignment = StringAlignment.Center;
                                stringFormat.Alignment = StringAlignment.Center;

                                Rectangle stringRectangle = this.ClientRectangle;
                                if (Text.Equals("%"))
                                {
                                    int pr = 0;
                                    if (Value != 0)
                                        pr = (int)(((float)(Value - Minimum) / (float)(Maximum - Minimum)) * 100f);
                                    graphics.DrawString($"{ pr}%", this.Font, textBrush, stringRectangle, stringFormat);
                                }
                                else
                                    graphics.DrawString(this.Text, this.Font, textBrush, stringRectangle, stringFormat);
                            }
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////// 

        /// <summary>
        /// Raises the BorderColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
		protected virtual void OnBorderColorChanged(object sender, EventArgs e)
        {
            this.Invalidate(true);
            if (this.BorderColorChanged != null)
            {
                this.BorderColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the BackgroundColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnBackgroundColorChanged(object sender, EventArgs e)
        {
            Invalidate();
            if (this.BackgroundColorChanged != null)
            {
                this.BackgroundColorChanged(sender, e);
            }
        }
        /// <summary>
        /// Raises the ValueColor changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A EventArgs that contains the event data.</param>
        protected virtual void OnValueColorChanged(object sender, EventArgs e)
        {
            Invalidate(true);
            if (this.ValueColorChanged != null)
            {
                this.ValueColorChanged(sender, e);
            }
        }
        #endregion

        #region MethodsPrivate

        ////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private static void DrawProgressBar(Graphics graphics, Rectangle clientRectangle, Color colorBackgroundEnd, Color colorValueEnd, Color borderColor,
                                             RightToLeft rightToLeft, int iMinimum, int iMaximum, int iValue, int RadiusCorner, bool isBattary)
        {
            Rectangle outerRectangle = clientRectangle;
            outerRectangle.Inflate(-1, -1);
            Rectangle orientRectangle = outerRectangle;
            if (outerRectangle.Width < outerRectangle.Height)
                orientRectangle = new Rectangle(outerRectangle.X, outerRectangle.Y, outerRectangle.Height, outerRectangle.Width);

            using (GraphicsPath outerRectangleGraphicsPath = GetBackgroundPath(orientRectangle, RadiusCorner - 1, isBattary))
            {
                if (outerRectangleGraphicsPath != null)
                {
                    Region reg = new Region(outerRectangleGraphicsPath);
                    int iProgressRange = iMaximum - iMinimum;
                    int iValueRange = iValue - iMinimum;
                    Rectangle valRectangle = orientRectangle;
                    valRectangle.Width = (int)((float)iValueRange / (float)iProgressRange * valRectangle.Width);
                    reg.Intersect(valRectangle);

                    if (outerRectangle.Width < outerRectangle.Height)
                    {
                        Matrix mat = new Matrix();
                        mat.RotateAt(270, outerRectangleGraphicsPath.PathPoints[0]);
                        mat.Translate(-outerRectangle.Height + RadiusCorner / 2, RadiusCorner / 2 - 1);
                        outerRectangleGraphicsPath.Transform(mat);
                        reg.Transform(mat);
                    }

                    using (LinearGradientBrush gradientBrush = GetGradientBackBrush(outerRectangle, colorBackgroundEnd, (outerRectangle.Width < outerRectangle.Height)))
                    {
                        if (gradientBrush != null)
                            graphics.FillPath(gradientBrush, outerRectangleGraphicsPath);
                    }

                    if (iValue > 0)
                    {
                        using (LinearGradientBrush gradientBrush = GetGradientBackBrush(outerRectangle, colorValueEnd, (outerRectangle.Width < outerRectangle.Height)))
                        {
                            if (gradientBrush != null)
                                graphics.FillRegion(gradientBrush, reg);
                        }
                    }
                    using (Pen borderPen = new Pen(borderColor))
                    {
                        graphics.DrawPath(borderPen, outerRectangleGraphicsPath);
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////// 

        ////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private static GraphicsPath GetBackgroundPath(Rectangle bounds, int radius, bool isBattary)
        {

            int x = bounds.X;
            int y = bounds.Y;
            int width = bounds.Width;
            int widthpip = width / 20;
            int height = bounds.Height;
            int heightpip = height / 2;
            GraphicsPath graphicsPath = new GraphicsPath();
            if (isBattary)
            {
                width -= widthpip;
                graphicsPath.AddArc(x, y, radius, radius, 180, 90);                                 //Upper left corner
                graphicsPath.AddArc(x + width - radius, y, radius, radius, 270, 90);                //Upper right corner

                graphicsPath.AddLine(x + width, y + heightpip / 2, x + width + widthpip - radius / 2, y + heightpip / 2);
                graphicsPath.AddArc(x + width + widthpip - radius / 2, y + heightpip / 2, radius / 2, radius / 2, 270, 90);    //Upper right corner
                graphicsPath.AddArc(x + width + widthpip - radius / 2, y + height - heightpip / 2 - radius / 2, radius / 2, radius / 2, 0, 90);    //Upper right corner
                graphicsPath.AddLine(x + width + widthpip - radius / 2, y + height - heightpip / 2, x + width, y + height - heightpip / 2);

                graphicsPath.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);//Lower right corner
                graphicsPath.AddArc(x, y + height - radius, radius, radius, 90, 90);                //Lower left corner
            }
            else
            {
                graphicsPath.AddArc(x, y, radius, radius, 180, 90);                                 //Upper left corner
                graphicsPath.AddArc(x + width - radius, y, radius, radius, 270, 90);                //Upper right corner
                graphicsPath.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);//Lower right corner
                graphicsPath.AddArc(x, y + height - radius, radius, radius, 90, 90);                //Lower left corner
            }
            graphicsPath.CloseFigure();

            return graphicsPath;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////// 

        ////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private static LinearGradientBrush GetGradientBackBrush(Rectangle bounds, Color backColor, bool isVertical)
        {
            if (bounds.IsEmpty)
                return null;
            LinearGradientMode mode = LinearGradientMode.Vertical;
            if (isVertical)
                mode = LinearGradientMode.Horizontal;
            LinearGradientBrush linearGradientBrush = linearGradientBrush = new LinearGradientBrush(bounds, Color.White, backColor, mode);
            if (linearGradientBrush != null)
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0.0F, 0.2F, 0.3F, 0.5F, 0.6F, 0.8F, 1.0F };
                blend.Factors = new float[] { 0.3F, 0.4F, 0.5F, 0.8F, 1.0F, 1.0F, 0.9F };
                linearGradientBrush.Blend = blend;
            }
            return linearGradientBrush;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////// 
        #endregion
    }
    public class UseClearTypeGridFit : IDisposable
    {
        #region FieldsPrivate
        private Graphics m_graphics;
        private TextRenderingHint m_textRenderingHint;
        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initialize a new instance of the UseClearTypeGridFit class.
        /// </summary>
		/// <param name="graphics">Graphics instance.</param>
        public UseClearTypeGridFit(Graphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics",
                    string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "Parameter { 0 } can't be null",
                    "graphics"));
            }

            this.m_graphics = graphics;
            this.m_textRenderingHint = this.m_graphics.TextRenderingHint;
            this.m_graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        }
        /// <summary>
        /// destructor of the UseClearTypeGridFit class.
        /// </summary>
        ~UseClearTypeGridFit()
        {
            Dispose(false);
        }
        /// <summary>
        /// Releases all resources used by the class. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region MethodsProtected
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //Revert the TextRenderingHint back to original setting.
                this.m_graphics.TextRenderingHint = this.m_textRenderingHint;
            }
        }
        #endregion
    }


    public class UseAntiAlias : IDisposable
    {
        #region FieldsPrivate

        private Graphics m_graphics;
        private SmoothingMode m_smoothingMode;

        #endregion

        #region MethodsPublic
        /// <summary>
        /// Initialize a new instance of the UseAntiAlias class.
        /// </summary>
        /// <param name="graphics">Graphics instance.</param>
        public UseAntiAlias(Graphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics",
                    string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "Parameter {0} can't be null",
                    "graphics"));
            }

            this.m_graphics = graphics;
            this.m_smoothingMode = m_graphics.SmoothingMode;
            this.m_graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }
        /// <summary>
        /// destructor of the UseAntiAlias class.
        /// </summary>
        ~UseAntiAlias()
        {
            Dispose(false);
        }
        /// <summary>
        /// Releases all resources used by the class. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region MethodsProtected
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //Revert the SmoothingMode back to original setting.
                this.m_graphics.SmoothingMode = this.m_smoothingMode;
            }
        }
        #endregion
    }

}
