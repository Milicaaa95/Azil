using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace VetCentar
{
    class VetCentarUtilities
    {
        public static int selectedTheme = 1;
        public static int selectedLanguage = 2;
        public static MenuItem englishMenuItem;
        public static MenuItem serbianMenuItem;
        public static MenuItem style1MenuItem;
        public static MenuItem style2MenuItem;
        public static MenuItem style3MenuItem;

        public static string GetSHA256(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "").ToLower();
            }
        }

        public static void ChangeLook(zaposleni current)
        {
            if(current.tema == 1)
            {
                SetStyle1(current);
            }
            else if(current.tema == 2)
            {
                SetStyle2(current);
            }
            else if(current.tema == 3)
            {
                SetStyle3(current);
            }

            if(current.jezik == 1)
            {
                SetEnglish(current);
            }
            else if(current.jezik == 2)
            {
                SetSerbian(current);
            }
        }

        public static void SetEnglish(zaposleni current)
        {
            selectedLanguage = 1;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

            ChangeLanguage(current);

            ResetLanguages();
            englishMenuItem.IsChecked = true;

            if(current != null)
            {
                current.jezik = 1;
            }
        }

        public static void SetSerbian(zaposleni current)
        {
            selectedLanguage = 2;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-Latn-RS");

            ChangeLanguage(current);

            ResetLanguages();
            serbianMenuItem.IsChecked = true;

            if(current != null)
            {
                current.jezik = 2;
            }
        }

        public static void SetStyle1(zaposleni current)
        {
            selectedTheme = 1;
            ChangeToStyle1();

            ResetStyles();
            style1MenuItem.IsChecked = true;

            if(current != null)
            {
                current.tema = 1;
            }
        }

        public static void SetStyle2(zaposleni current)
        {
            selectedTheme = 2;
            ChangeToStyle2();

            ResetStyles();
            style2MenuItem.IsChecked = true;

            if(current != null)
            {
                current.tema = 2;
            }
        }

        public static void SetStyle3(zaposleni current)
        {
            selectedTheme = 3;
            ChangeToStyle3();

            ResetStyles();
            style3MenuItem.IsChecked = true;

            if(current != null)
            {
                current.tema = 3;
            }
        }

        private static void ChangeLanguage(zaposleni current)
        {
            foreach (Window form in Application.Current.Windows)
            {
                if (form is VetWindow)
                {
                    new VetWindow(current).Show();
                }
                else if (form is AdminWindow)
                {
                    new AdminWindow(current).Show();
                }
                else if (form is MainWindow)
                {
                    new MainWindow().Show();
                }
                form.Close();
            }

        }

        private static void ChangeToStyle1()
        {
            var brush = new SolidColorBrush();
            brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ccffe6"));
            var font = new FontFamily("Microsoft Sans Serif");

            GetStyle(brush, font);
        }

        private static void ChangeToStyle2()
        {
            var brush = new SolidColorBrush();
            brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#80ffff"));
            var font = new FontFamily("Lucida Sans Unicode");

            GetStyle(brush, font);
        }

        private static void ChangeToStyle3()
        {
            var brush = new SolidColorBrush();
            brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#00ffff"));
            var font = new FontFamily("Arial");

            GetStyle(brush, font);
        }

        private static void GetStyle(SolidColorBrush brush, FontFamily font)
        {
            Style windowStyle = new Style
            {
                TargetType = typeof(Window)
            };
            Style labelStyle = new Style
            {
                TargetType = typeof(Label)
            };
            Style textBoxStyle = new Style
            {
                TargetType = typeof(TextBox)
            };
            Style menuStyle = new Style
            {
                TargetType = typeof(Menu)
            };
            Style buttonStyle = new Style
            {
                TargetType = typeof(Button)
            };
            Style passwordBoxStyle = new Style
            {
                TargetType = typeof(PasswordBox)
            };
            Style dataGridStyle = new Style
            {
                TargetType = typeof(DataGrid)
            };
            Style comboBoxStyle = new Style
            {
                TargetType = typeof(ComboBox)
            };
            Style radioButtonStyle = new Style
            {
                TargetType = typeof(RadioButton)
            };
            Style datePickerStyle = new Style
            {
                TargetType = typeof(DatePicker)
            };

            windowStyle.Setters.Add(new Setter(Control.BackgroundProperty, brush));
            windowStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            menuStyle.Setters.Add(new Setter(Control.BackgroundProperty, brush));
            menuStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            labelStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            textBoxStyle.Setters.Add(new Setter(Control.BackgroundProperty, (SolidColorBrush)(new BrushConverter().ConvertFrom("#e0ebeb"))));
            textBoxStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            buttonStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            passwordBoxStyle.Setters.Add(new Setter(Control.BackgroundProperty, (SolidColorBrush)(new BrushConverter().ConvertFrom("#e0ebeb"))));

            dataGridStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            comboBoxStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            radioButtonStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            datePickerStyle.Setters.Add(new Setter(Control.FontFamilyProperty, font));

            Application.Current.Resources["MyWindowStyle"] = windowStyle;
            Application.Current.Resources["MyLabelStyle"] = labelStyle;
            Application.Current.Resources["MyButtonStyle"] = buttonStyle;
            Application.Current.Resources["MyTextBoxStyle"] = textBoxStyle;
            Application.Current.Resources["MyMenuStyle"] = menuStyle;
            Application.Current.Resources["MyPasswordBoxStyle"] = passwordBoxStyle;
            Application.Current.Resources["MyDataGridStyle"] = dataGridStyle;
            Application.Current.Resources["MyComboBoxStyle"] = comboBoxStyle;
            Application.Current.Resources["MyRadioButtonStyle"] = radioButtonStyle;
            Application.Current.Resources["MyDatePickerStyle"] = datePickerStyle;
        }

        private static void ResetLanguages()
        {
            englishMenuItem.IsChecked = false;
            serbianMenuItem.IsChecked = false;
        }

        private static void ResetStyles()
        {
            style1MenuItem.IsChecked = false;
            style2MenuItem.IsChecked = false;
            style3MenuItem.IsChecked = false;
        }
    }
}
