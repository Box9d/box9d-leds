using System.Windows.Forms;

namespace Box9.Leds.Manager.Extensions
{
    public static class TextBoxExtensions
    {
        public static void ValidateAsInteger(this TextBox textBox)
        {
            if (!textBox.Text.IsInteger())
            {
                var validatedText = string.Empty;
                var letters = textBox.Text.ToCharArray();
                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i].ToString().IsInteger())
                    {
                        validatedText += letters[i];
                    }
                }

                textBox.Text = validatedText;
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        public static void ValidateTextLength(this TextBox textBox, int maxLength)
        {
            if (textBox.Text.Length > maxLength)
            {
                textBox.Text = textBox.Text.Substring(0, maxLength);
            }

            textBox.SelectionStart = textBox.Text.Length;
        }
    }
}
