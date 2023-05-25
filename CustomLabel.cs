using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavmaxiaGame
{
    public class CustomLabel:Label
    {
        public CustomLabel(Point location,String text)
        {
            this.Font = new Font("Calibri", 14);
            this.ForeColor = Color.Black;
            this.Size = new Size(25, 25);
            this.Text = text;
            this.Location= location;
        }
    }
}
