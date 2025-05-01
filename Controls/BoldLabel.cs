using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor_Application.Controls
{
    public class BoldLabel : Label
    {
        public BoldLabel()
        {
            this.FontAttributes = FontAttributes.Bold;
            this.TextColor = Color.FromArgb("#000000");
        }
        public static readonly BindableProperty TextContentProperty =
            BindableProperty.Create(nameof(TextContent), typeof(string), typeof(BoldLabel), default(string), propertyChanged: OnTextContentChanged);

        public string TextContent
        {
            get => (string)GetValue(TextContentProperty);
            set => SetValue(TextContentProperty, value);
        }

        private static void OnTextContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is BoldLabel boldLabel && newValue is string text)
            {
                boldLabel.FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span
                        {
                            Text = text,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = boldLabel.FontSize,
                            TextColor = boldLabel.TextColor
                        }
                    }
                };
            }
        }
    }
}
