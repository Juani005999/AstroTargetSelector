using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ApplicationTools
{
    /// <summary>
    /// Composant permettant de généré un rendu de contrôle en mode Jour / Nuit <see cref="ModeNuit"/>
    /// </summary>
    public class ATSToolStripRenderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        /// Affichage en mode Jour/Nuit
        /// </summary>
        public bool ModeNuit { get; set; }

        /// <summary>
        /// Back Color du mode Nuit
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Back Color Light du mode Nuit
        /// </summary>
        public Color BackColorLight { get; set; }

        /// <summary>
        /// Fore Color du mode Nuit
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public ATSToolStripRenderer() { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (ModeNuit)
            {
                Rectangle rectangle = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                Brush brush = new LinearGradientBrush(rectangle, BackColor, BackColorLight, LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brush, rectangle);
            }
            else
                base.OnRenderToolStripBackground(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (ModeNuit && !e.Item.Selected)
                e.TextColor = ForeColor;
            else
                e.TextColor = SystemColors.MenuText;
            base.OnRenderItemText(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            if (ModeNuit)
            {
                Rectangle rectangle = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                Brush brush = new SolidBrush(BackColor);
                e.Graphics.FillRectangle(brush, rectangle);
            }
            else
                base.OnRenderImageMargin(e);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (ModeNuit)
            {
                Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                if (!e.Item.Selected)
                {
                    Brush brush = new SolidBrush(BackColor);
                    e.Graphics.FillRectangle(brush, rectangle);
                }
                else
                {
                    Brush brush = new SolidBrush(ForeColor);
                    e.Graphics.FillRectangle(brush, rectangle);
                }
            }
            else
                base.OnRenderMenuItemBackground(e);
        }
    }
}
